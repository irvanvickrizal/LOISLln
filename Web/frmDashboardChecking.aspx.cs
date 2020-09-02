using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmDashboardChecking : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            var getdashoard = DashboardController.DashboardViewUser(ContentSession.USERID);
            if (getdashoard != null)
                Response.Redirect(ResolveUrl(getdashoard.DashboardURL));
            else
                Response.Redirect(ResolveUrl(DashboardController.DashboardDefURL()));
        }
    }
}