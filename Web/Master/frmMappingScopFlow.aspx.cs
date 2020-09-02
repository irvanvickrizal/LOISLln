using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using eLoi.Controller;

public partial class Master_frmMappingScopFlow : System.Web.UI.Page
{
    MasterController mc = new MasterController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            bindDataScope();
            BinddataFlow();
        }
    }

    private void BindData()
    {
        gvFlowScope.DataSource = mc.grouping_scope_Flow_getdata();
        gvFlowScope.DataBind();
    }

    private void bindDataScope()
    {
        ddlScope.DataSource = mc.scope_type_getData();
        ddlScope.DataTextField = "scope_type";
        ddlScope.DataValueField = "scope_type_id";
        ddlScope.DataBind();
        ddlScope.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void BinddataFlow()
    {
        ddlFlow.DataSource = mc.master_workflow_getdata();
        ddlFlow.DataTextField = "wf_desc";
        ddlFlow.DataValueField = "wf_id";
        ddlFlow.DataBind();
        ddlFlow.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        if (ddlFlow.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('please select Flow');", true);
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

        if (mc.grouping_scope_Flow_iud(int.Parse(ddlScope.SelectedValue), int.Parse(ddlFlow.SelectedValue), ddlCTName.SelectedValue, hdngroupid.Value, false))
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('mapping scope and Flow has been saved Successfully');", true);
            hdngroupid.Value = string.Empty;
            btnCancel.Visible = false;
            btnConfirm.Text = "Create";
            BindData();
            cleartext();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to mapping scope and Flow');", true);
        }
    }

    private void cleartext()
    {
        ddlFlow.SelectedIndex = 0;
        ddlScope.SelectedIndex = 0;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        cleartext();
        hdngroupid.Value = string.Empty;
        btnCancel.Visible = false;
        btnConfirm.Text = "Create";
    }

    protected void gvFlowScope_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("editgroup"))
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            hdngroupid.Value = e.CommandArgument.ToString();
            ddlScope.SelectedValue = row.Cells[1].Text;
            ddlFlow.SelectedValue = row.Cells[2].Text;
            ddlCTName.SelectedValue = row.Cells[3].Text;
            btnCancel.Visible = true;
            btnConfirm.Text = "Update";
        }
        else if (e.CommandName.Equals("deletegroup"))
        {
            if (mc.grouping_scope_Flow_iud(0, 0, string.Empty, e.CommandArgument.ToString(), true))
            {
                hdngroupid.Value = string.Empty;
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
}