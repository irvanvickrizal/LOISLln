using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using eLoi.Controller;

public partial class Master_frmChangePassword : System.Web.UI.Page
{
    MasterController mc = new MasterController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtuserId.Value = mc.getusrlogin_byuserid();
        }
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtuserId.Value))
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('User Login is Empty');", true);
            return;
        }

        if (string.IsNullOrEmpty(txtOldPassword.Value))
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Old Password is Empty');", true);
            return;
        }

        if (string.IsNullOrEmpty(txtNewPassword.Value))
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('New Password is Empty');", true);
            return;
        }

        if (mc.ebastusers_changepassword(txtuserId.Value, txtOldPassword.Value, txtNewPassword.Value))
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Password has been changed Successfully');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to Change Password');", true);
        }
    }
}