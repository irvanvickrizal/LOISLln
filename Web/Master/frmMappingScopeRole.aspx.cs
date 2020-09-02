using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using eLoi.Controller;

public partial class Master_frmMappingScopeRole : System.Web.UI.Page
{
    MasterController mc = new MasterController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            bindDataScope();
            BinddataRole();
        }
    }

    private void BindData()
    {
        gvRoleScope.DataSource = mc.mapping_scope_role_getdata();
        gvRoleScope.DataBind();
    }

    private void bindDataScope()
    {
        ddlScope.DataSource = mc.scope_type_getData();
        ddlScope.DataTextField = "scope_type";
        ddlScope.DataValueField = "scope_type_id";
        ddlScope.DataBind();
        ddlScope.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void BinddataRole()
    {
        ddlRole.DataSource = mc.TRole_getData();
        ddlRole.DataTextField = "RoleDesc";
        ddlRole.DataValueField = "RoleID";
        ddlRole.DataBind();
        ddlRole.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        if (ddlRole.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('please select role');", true);
            return;
        }

        if (ddlScope.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('please select scope');", true);
            return;
        }

        if (ddlCTName.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('please select CT Name');", true);
            return;
        }

        if (mc.mapping_scope_role_iud(int.Parse(ddlScope.SelectedValue), int.Parse(ddlRole.SelectedValue)))
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('mapping scope and role has been created Successfully');", true);
            BindData();
            cleartext();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to mapping scope and role');", true);
        }
    }

    private void cleartext()
    {
        ddlRole.SelectedIndex = 0;
        ddlScope.SelectedIndex = 0;
        ddlCTName.SelectedIndex = 0;
    }
}