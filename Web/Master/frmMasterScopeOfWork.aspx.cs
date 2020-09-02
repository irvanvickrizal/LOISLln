using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using eLoi.Controller;

public partial class Master_frmMasterScopeOfWork : System.Web.UI.Page
{
    MasterController mc = new MasterController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindDataScope();
            BindData();
        }
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtGEneralSOW.Value))
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('general sow is empty');", true);
            return;
        }

        if (string.IsNullOrEmpty(txtDetailSOW.Value))
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('detail sow is empty');", true);
            return;
        }

        if (ddlScopetype.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('please select scope type');", true);
            return;
        }

        if (mc.master_sow_iud(int.Parse(ddlScopetype.SelectedValue), txtGEneralSOW.Value, txtDetailSOW.Value, hdnSOWid.Value, false))
        {
            hdnSOWid.Value = string.Empty;
            btnCancel.Visible = false;
            btnConfirm.Text = "Save";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Master Scope of Work has been saved Successfully');", true);
            BindData();
            cleartext();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to created master Scope of Work');", true);
        }
    }

    private void BindData()
    {
        gvSOW.DataSource = mc.master_sow_getall();
        gvSOW.DataBind();
    }

    private void cleartext()
    {
        txtGEneralSOW.Value = string.Empty;
        txtDetailSOW.Value = string.Empty;
        ddlScopetype.SelectedIndex = 0;
    }

    protected void gvSOW_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("editSOW"))
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            hdnSOWid.Value = e.CommandArgument.ToString();
            ddlScopetype.SelectedValue = row.Cells[4].Text;
            txtGEneralSOW.Value = row.Cells[2].Text;
            txtDetailSOW.Value = row.Cells[3].Text;
            btnCancel.Visible = true;
            btnConfirm.Text = "Update";
        }
        else if (e.CommandName.Equals("deleteSOW"))
        {
            if (mc.master_sow_iud(0, string.Empty, string.Empty, e.CommandArgument.ToString(), true))
            {
                hdnSOWid.Value = string.Empty;
                btnCancel.Visible = false;
                btnConfirm.Text = "Save";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Scope of Work has been deleted Successfully');", true);
                BindData();
                cleartext();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to delete Scope of Work');", true);
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        cleartext();
        hdnSOWid.Value = string.Empty;
        btnCancel.Visible = false;
        btnConfirm.Text = "Save";
    }

    private void bindDataScope()
    {
        ddlScopetype.DataSource = mc.scope_type_getData();
        ddlScopetype.DataTextField = "scope_type";
        ddlScopetype.DataValueField = "scope_type_id";
        ddlScopetype.DataBind();
        ddlScopetype.Items.Insert(0, new ListItem("--Select--", "0"));
    }
}