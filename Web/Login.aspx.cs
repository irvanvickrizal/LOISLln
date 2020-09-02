using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

        }
    }


    protected void lbtLogin_Click(object sender, EventArgs e)
    {
        DataTable getresult = UserController.User_LoginValidation(txtUsername.Value, txtPassword.Value);
        if (getresult.Rows.Count > 0)
        {
            foreach (DataRow drw in getresult.Rows)
            {
                //if (getresult.Rows.Count > 1)
                //{
                //    ShowMessage("Duplicate user login different CTName", GeneralConfig.MessageType.Error);
                //    if (Session[Resources.Resources.ses_userid] != null)
                //        Session.Clear();
                //    return;
                //}

                if (drw["loginstatus"].ToString() == "valid")
                {
                    Session[Resources.Resources.ses_userid] = drw["usr_id"].ToString();
                    Session[Resources.Resources.ses_roleid] = drw["usrRole"].ToString();
                    Session[Resources.Resources.ses_fullname] = drw["name"].ToString();
                    Session[Resources.Resources.ses_usergroup] = drw["usergroup"].ToString();
                    Session[Resources.Resources.ses_signtitle] = drw["SignTitle"].ToString();
                    Session["LoginerCTName"] = drw["CTName"].ToString();
                    Response.Redirect(ResolveUrl(DashboardController.DashboardCheckingURL()));
                }
                else
                {
                    ShowMessage(Resources.ResourcesError.err_wrong_userlogin, GeneralConfig.MessageType.Error);
                    if (Session[Resources.Resources.ses_userid] != null)
                        Session.Clear();
                }
            }
        }
        else
            ShowMessage(Resources.ResourcesError.err_login, GeneralConfig.MessageType.Error);
    }

    protected void ShowMessage(string Message, GeneralConfig.MessageType type)
    {
        //ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
    }


}