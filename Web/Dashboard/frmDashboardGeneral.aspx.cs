using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard_frmDashboardGeneral : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindAgenda(ContentSession.USERID);
        }
    }    

    private void BindAgenda(int userid)
    {
        //ltrCRPendingCount.Text = "<span class=\"pull-right text-danger medium\">" + taskpending.ToString() + "</span>";
        //aViewCRPending.HRef = ResolveUrl("~/approval/frmTaskPendingCR.aspx");
    }   

}