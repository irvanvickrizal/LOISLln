using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using eLoi.Controller;

public partial class Master_frmRoleGrouping : System.Web.UI.Page
{
    MasterController mc = new MasterController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            BindLOIRole();
        }
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        if (ddlCTName.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('please select CT Name');", true);
            return;
        }

        if (ddlLOIRole.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('please select LOI Role');", true);
            return;
        }

        if (ddlProjectRole.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('please select project role');", true);
            return;
        }

        if (mc.Role_Grouping_iud(int.Parse(ddlLOIRole.SelectedValue), int.Parse(ddlProjectRole.SelectedValue), ddlCTName.SelectedValue, hdnrg.Value, false))
        {
            hdnrg.Value = string.Empty;
            btnCancel.Visible = false;
            btnConfirm.Text = "Create";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Role grouping has been created Successfully');", true);
            BindData();
            cleartext();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to created Role grouping');", true);
        }
    }

    private void BindData()
    {
        gvRole.DataSource = mc.Role_Grouping_getdata();
        gvRole.DataBind();
    }

    private void BindLOIRole()
    {
        ddlLOIRole.DataSource = mc.TRole_getData();
        ddlLOIRole.DataTextField = "RoleDesc";
        ddlLOIRole.DataValueField = "RoleID";
        ddlLOIRole.DataBind();
        ddlLOIRole.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void BindRoleProject(string CTName)
    {
        ddlProjectRole.DataSource = mc.Project_TRole_getByCTName(CTName);
        ddlProjectRole.DataTextField = "RoleDesc";
        ddlProjectRole.DataValueField = "RoleID";
        ddlProjectRole.DataBind();
        ddlProjectRole.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void cleartext()
    {
        ddlCTName.SelectedIndex = 0;
        ddlLOIRole.SelectedIndex = 0;
        ddlProjectRole.DataSource = null;
        ddlProjectRole.SelectedIndex = 0;
    }

    protected void ddlCTName_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindRoleProject(ddlCTName.SelectedValue);
    }

    protected void gvRole_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("editrolegrouping"))
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            hdnrg.Value = e.CommandArgument.ToString();
            ddlCTName.SelectedValue = row.Cells[1].Text;
            BindRoleProject(ddlCTName.SelectedValue);
            ddlProjectRole.SelectedValue = row.Cells[2].Text;
            ddlLOIRole.SelectedValue = row.Cells[3].Text;
            btnCancel.Visible = true;
            btnConfirm.Text = "Update";
        }
        else if (e.CommandName.Equals("deleterolegrouping"))
        {
            if (mc.Role_Grouping_iud(0, 0, string.Empty, e.CommandArgument.ToString(), true))
            {
                hdnrg.Value = string.Empty;
                btnCancel.Visible = false;
                btnConfirm.Text = "Create";
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
        hdnrg.Value = string.Empty;
        btnCancel.Visible = false;
        btnConfirm.Text = "Create";
    }
}