using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin : System.Web.UI.MasterPage
{
    private string hosturl;
    public string HostUrl { get { return hosturl; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        hosturl = Request.Url.GetLeftPart(UriPartial.Authority) + (Request.ApplicationPath == "/" ? Request.ApplicationPath : Request.ApplicationPath + "/");
        if (Session[Resources.Resources.ses_userid] == null) { 
            Response.Redirect(ResolveUrl("~/Logout.aspx"));
        }
    }
}
