using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using eLoi.Controller;

public partial class Master_frmMasterScopeType : System.Web.UI.Page
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
        if (string.IsNullOrEmpty(txtScopeType.Value))
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('scope type is empty');", true);
            return;
        }       

        if (mc.scope_type_iud(txtScopeType.Value, hdnScopeTypeId.Value, false))
        {
            hdnScopeTypeId.Value = string.Empty;
            btnCancel.Visible = false;
            btnConfirm.Text = "Save";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Master scope type has been saved Successfully');", true);
            BindData();
            cleartext();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to created master scope type');", true);
        }
    }

    private void BindData()
    {
        gvScopeType.DataSource = mc.scope_type_getData();
        gvScopeType.DataBind();
    }

    private void cleartext()
    {
        txtScopeType.Value = string.Empty;
    }

    protected void gvScopeType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("editscopetype"))
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            hdnScopeTypeId.Value = e.CommandArgument.ToString();
            txtScopeType.Value = row.Cells[1].Text;
            btnCancel.Visible = true;
            btnConfirm.Text = "Update";
        }
        else if (e.CommandName.Equals("deletescopetype"))
        {
            if (mc.scope_type_iud(string.Empty, e.CommandArgument.ToString(), true))
            {
                hdnScopeTypeId.Value = string.Empty;
                btnCancel.Visible = false;
                btnConfirm.Text = "Save";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('scope type has been deleted Successfully');", true);
                BindData();
                cleartext();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to delete scope type');", true);
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        cleartext();
        hdnScopeTypeId.Value = string.Empty;
        btnCancel.Visible = false;
        btnConfirm.Text = "Save";
    }
}