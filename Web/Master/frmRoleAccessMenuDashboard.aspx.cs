using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using eLoi.Controller;

public partial class Master_frmRoleAccessMenuDashboard : System.Web.UI.Page
{
    MasterController mc = new MasterController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BinddataRole();
            BindDashboard();
        }
    }

    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRole.SelectedIndex == 0)
        {

            return;
        }
        int roleid = int.Parse(ddlRole.SelectedValue);
        gvRoleAccess.DataSource = mc.getmenuAccess_Role(roleid);
        gvRoleAccess.DataBind();

        int dashboardid = mc.getdashboardid_byroleid(roleid);
        if (dashboardid > 0)
        {
            ddlDashboard.SelectedValue = dashboardid.ToString();
        }
    }

    private void BinddataRole()
    {
        ddlRole.DataSource = mc.TRole_getData();
        ddlRole.DataTextField = "RoleDesc";
        ddlRole.DataValueField = "RoleID";
        ddlRole.DataBind();
        ddlRole.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void gvRoleAccess_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkMenu = (CheckBox)e.Row.FindControl("chkMenu");
            Label lblMenuName = (Label)e.Row.FindControl("lblMenuName");
            GridView gvRoleAccessChild = (GridView)e.Row.FindControl("gvRoleAccessChild");
            bool isAccess = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "isAccess"));
            int parent_menu_id = Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "menu_id"));
            chkMenu.Checked = isAccess;
            gvRoleAccessChild.DataSource = mc.getmenuAccess_Role_Child(int.Parse(ddlRole.SelectedValue), parent_menu_id);
            gvRoleAccessChild.DataBind();



        }
    }

    protected void chkMenu_CheckedChanged(object sender, EventArgs e)
    {
        if (ddlRole.SelectedIndex == 0)
        {

            return;
        }
        GridViewRow row = (GridViewRow)(((CheckBox)sender).NamingContainer);
        HiddenField hdnmenuid = (HiddenField)row.FindControl("hdnmenuid");
        CheckBox chkMenu = (CheckBox)row.FindControl("chkMenu");
        mc.menu_accessrights_iud(int.Parse(ddlRole.SelectedValue), int.Parse(hdnmenuid.Value), !chkMenu.Checked);
        gvRoleAccess.DataSource = mc.getmenuAccess_Role(int.Parse(ddlRole.SelectedValue));
        gvRoleAccess.DataBind();
    }

    protected void ddlDashboard_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlRole.SelectedIndex == 0)
        {
            return;
        }
        if (ddlDashboard.SelectedIndex == 0)
        {
            return;
        }
        mc.role_dashboard_access_iu(int.Parse(ddlRole.SelectedValue), int.Parse(ddlDashboard.SelectedValue));
    }

    private void BindDashboard()
    {
        ddlDashboard.DataSource = mc.master_dashboard_getdata();
        ddlDashboard.DataTextField = "dashboard_name";
        ddlDashboard.DataValueField = "dashboard_id";
        ddlDashboard.DataBind();
        ddlDashboard.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void gvRoleAccessChild_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkMenuchild = (CheckBox)e.Row.FindControl("chkMenuchild");
            Label lblMenuNamechild = (Label)e.Row.FindControl("lblMenuNamechild");
            bool isAccess = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "isAccess"));
            chkMenuchild.Checked = isAccess;
        }
    }

    protected void chkMenuchild_CheckedChanged(object sender, EventArgs e)
    {
        if (ddlRole.SelectedIndex == 0)
        {

            return;
        }
        GridViewRow row = (GridViewRow)(((CheckBox)sender).NamingContainer);
        HiddenField hdnmenuidchild = (HiddenField)row.FindControl("hdnmenuidchild");
        CheckBox chkMenuchild = (CheckBox)row.FindControl("chkMenuchild");
        mc.menu_accessrights_iud(int.Parse(ddlRole.SelectedValue), int.Parse(hdnmenuidchild.Value), !chkMenuchild.Checked);
        gvRoleAccess.DataSource = mc.getmenuAccess_Role(int.Parse(ddlRole.SelectedValue));
        gvRoleAccess.DataBind();
    }
}