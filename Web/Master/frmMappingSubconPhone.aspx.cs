using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using eLoi.Controller;

public partial class Master_frmMappingSubconPhone : System.Web.UI.Page
{
    MasterController mc = new MasterController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            bindDataSubcon();
        }
    }

    private void BindData()
    {
        gvSubconPhone.DataSource = mc.mapping_subcon_phone_getdata();
        gvSubconPhone.DataBind();
    }

    private void bindDataSubcon()
    {
        ddlSubcon.DataSource = mc.Subcon_getdata();
        ddlSubcon.DataTextField = "SCon_Name";
        ddlSubcon.DataValueField = "EPM_Vendor_ID";
        ddlSubcon.DataBind();
        ddlSubcon.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        if (ddlSubcon.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('please select subcon');", true);
            return;
        }

        if (string.IsNullOrEmpty(txtPhone.Value))
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Phone no is empty');", true);
            return;
        }

        if (string.IsNullOrEmpty(txtFax.Value))
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('fax is empty');", true);
            return;
        }

        if (mc.mapping_subcon_phone_iud(int.Parse(ddlSubcon.SelectedValue), txtPhone.Value, txtFax.Value, hdnsconphoneid.Value, false))
        {
            hdnsconphoneid.Value = string.Empty;
            btnCancel.Visible = false;
            btnConfirm.Text = "Create";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('mapping subcon and phone has been created Successfully');", true);
            BindData();
            cleartext();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to mapping subcon and phone');", true);
        }
    }

    private void cleartext()
    {
        ddlSubcon.SelectedIndex = 0;
        txtFax.Value = string.Empty;
        txtPhone.Value = string.Empty;
    }

    protected void gvSubconPhone_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("editsubconphone"))
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            hdnsconphoneid.Value = e.CommandArgument.ToString();
            ddlSubcon.SelectedValue = row.Cells[2].Text;
            txtPhone.Value = row.Cells[3].Text;
            txtFax.Value = row.Cells[4].Text;
            btnCancel.Visible = true;
            btnConfirm.Text = "Update";
        }
        else if (e.CommandName.Equals("deletesubconphone"))
        {
            if (mc.mapping_subcon_phone_iud(0, string.Empty, string.Empty, e.CommandArgument.ToString(), true))
            {
                hdnsconphoneid.Value = string.Empty;
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
        hdnsconphoneid.Value = string.Empty;
        btnCancel.Visible = false;
        btnConfirm.Text = "Create";
    }
}