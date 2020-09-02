using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ContentSession.USERID > 0)
        { 
            mvPanelLogout.SetActiveView(vwLogout);
            Session.Clear();
            Session.Abandon();
        }
        else
            mvPanelLogout.SetActiveView(vwSessionTimeout);
    }
}