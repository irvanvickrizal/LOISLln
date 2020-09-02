using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using eLoi.Controller;

public partial class Master_frmMasterUser : System.Web.UI.Page
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

    private void BindLOIRole()
    {
        ddlRole.DataSource = mc.TRole_getData();
        ddlRole.DataTextField = "RoleDesc";
        ddlRole.DataValueField = "RoleID";
        ddlRole.DataBind();
        ddlRole.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        //if (string.IsNullOrEmpty(txtRoleCode.Value))
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('role code is empty');", true);
        //    return;
        //}

        //if (string.IsNullOrEmpty(txtRoleDesc.Value))
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('role description is empty');", true);
        //    return;
        //}

        //if (ddlLevelCode.SelectedIndex == 0)
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('please select level');", true);
        //    return;
        //}

        if (mc.ebastusers_loi_iud(ddlUserType.SelectedValue, txtName.Value, int.Parse(ddlRole.SelectedValue), txtUserId.Value, txtEmail.Value, txtPhone.Value, txtSigTitle.Value, ddlCTName.SelectedValue, hdnUserId.Value, false))
        {
            hdnUserId.Value = string.Empty;
            btnCancel.Visible = false;
            btnConfirm.Text = "Create";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('User has been created Successfully');", true);
            BindData();
            cleartext();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to created User');", true);
        }
    }

    private void BindData()
    {
        gvUser.DataSource = mc.loi_ebastusers_getdata();
        gvUser.DataBind();
    }



    protected void gvUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("edituser"))
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            hdnUserId.Value = e.CommandArgument.ToString();
            ddlUserType.SelectedValue = row.Cells[1].Text;
            txtName.Value = row.Cells[2].Text;
            ddlRole.SelectedValue = row.Cells[3].Text;
            txtUserId.Value = row.Cells[5].Text;
            txtEmail.Value = row.Cells[6].Text;
            txtPhone.Value = row.Cells[7].Text;
            txtSigTitle.Value = row.Cells[8].Text;
            ddlCTName.SelectedValue = row.Cells[9].Text;
            btnCancel.Visible = true;
            btnConfirm.Text = "Update";
        }
        else if (e.CommandName.Equals("deleteuser"))
        {
            if (mc.ebastusers_loi_iud(string.Empty, string.Empty, 0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, e.CommandArgument.ToString(), true))
            {
                hdnUserId.Value = string.Empty;
                btnCancel.Visible = false;
                btnConfirm.Text = "Create";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('User has been deleted Successfully');", true);
                BindData();
                cleartext();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to delete User');", true);
            }
        }
    }

    private void cleartext()
    {
        ddlUserType.SelectedIndex = 0;
        txtName.Value = string.Empty;
        ddlRole.SelectedIndex = 0;
        txtUserId.Value = string.Empty;
        txtEmail.Value = string.Empty;
        txtPhone.Value = string.Empty;
        txtSigTitle.Value = string.Empty;
        ddlCTName.SelectedIndex = 0;       
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        cleartext();
        hdnUserId.Value = string.Empty;
        btnCancel.Visible = false;
        btnConfirm.Text = "Create";
    }
}