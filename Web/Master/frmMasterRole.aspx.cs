using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using eLoi.Controller;

public partial class Master_frmMasterRole : System.Web.UI.Page
{
    MasterController mc = new MasterController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtRoleCode.Value))
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('role code is empty');", true);
            return;
        }

        if (string.IsNullOrEmpty(txtRoleDesc.Value))
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('role description is empty');", true);
            return;
        }

        if (ddlLevelCode.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('please select level');", true);
            return;
        }

        if (mc.TRole_iud(ddlLevelCode.SelectedValue, txtRoleCode.Value, txtRoleDesc.Value, hdnroleid.Value, false))
        {
            hdnroleid.Value = string.Empty;
            btnCancel.Visible = false;
            btnConfirm.Text = "Save";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Master Role has been saved Successfully');", true);
            BindData();
            cleartext();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to created master Role');", true);
        }
    }

    private void BindData()
    {
        gvRole.DataSource = mc.TRole_getData();
        gvRole.DataBind();
    }
        
    private void cleartext()
    {
        txtRoleCode.Value = string.Empty;
        txtRoleDesc.Value = string.Empty;
        ddlLevelCode.SelectedIndex = 0;
    }

    protected void gvRole_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("editrole"))
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            hdnroleid.Value = e.CommandArgument.ToString();
            txtRoleCode.Value = row.Cells[1].Text;
            txtRoleDesc.Value = row.Cells[2].Text;
            ddlLevelCode.SelectedValue = row.Cells[3].Text;
            btnCancel.Visible = true;
            btnConfirm.Text = "Update";
        }
        else if (e.CommandName.Equals("deleterole"))
        {
            if (mc.TRole_iud(string.Empty, string.Empty, string.Empty, e.CommandArgument.ToString(), true))
            {
                hdnroleid.Value = string.Empty;
                btnCancel.Visible = false;
                btnConfirm.Text = "Save";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Role has been deleted Successfully');", true);
                BindData();
                cleartext();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to delete Role');", true);
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        cleartext();
        hdnroleid.Value = string.Empty;
        btnCancel.Visible = false;
        btnConfirm.Text = "Save";
    }
}