using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class controls_navbar : System.Web.UI.UserControl
{   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Label1.Text = DateTime.Now.ToString("dd MMM yyyy hh:mm tt") + " (UTC + 07:00)";
            setNavBarTop(); 
            setNavBarLeft();
        }
    }

    private void setNavBarTop()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(getUser());
        ltrnavbartop.Text = sb.ToString();
    }

    private string getUser()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<li class=\"dropdown\">");
        sb.Append("	<a class=\"dropdown-toggle\" data-toggle=\"dropdown\" href=\"#\"><span style=\"color:#fff\">");
        sb.Append(  Greeting() + ContentSession.FULLNAME + ", " + ContentSession.SIGNTITLE + " [ " + ContentSession.USERGROUP + " ] <i class=\"fa fa-user fa-fw\"></i> <i class=\"fa fa-caret-down\"></i></span>");
        sb.Append("	</a>");
        sb.Append("	<ul class=\"dropdown-menu dropdown-user\">");
        sb.Append("		<li><a href=\"" + ResolveUrl("~/usr/frmUserProfile.aspx") + "\"><i class=\"fa fa-user fa-fw\"></i> User Profile</a>");
        sb.Append("		</li>");
        //sb.Append("		<li><a href=\"#\"><i class=\"fa fa-gear fa-fw\"></i> Settings</a>");
        //sb.Append("		</li>");
        sb.Append("		<li class=\"divider\"></li>");
        sb.Append("		<li><a href=\"" + ResolveUrl("~/Logout.aspx") +"\"><i class=\"fa fa-sign-out fa-fw\"></i> Logout</a>");
        sb.Append("		</li>");
        sb.Append("	</ul>");
        sb.Append("</li>");
        return sb.ToString();
    }

    private void setNavBarLeft()
    {
        DataTable dt = MenuController.Menu_GetParentMenuListBaseonRoleAuthorized(ContentSession.RoleID, "en");
        StringBuilder sb = new StringBuilder();
        string hostUrl = Request.Url.GetLeftPart(UriPartial.Authority) + (Request.ApplicationPath == "/" ? Request.ApplicationPath : Request.ApplicationPath + "/");
        //dt = getMenu();
        
        foreach (DataRow dr in dt.Rows)
        {
            int xCount = dt.Rows.Count;          
            if (dr["parent_menu_id"].ToString().Equals("0"))
            {
                sb.Append(string.Format(" <li><a href=\"{0}{1}\" data=\"{3}\">{2} {3}{4}</a>", "", ResolveUrl(dr["menu_url"].ToString()), dr["icon"].ToString(), dr["menu_name"].ToString(), int.Parse(dr["childmenucount"].ToString()) > 0 ? "<span class=\"fa arrow\"></span>" : ""));
                if (int.Parse(dr["childmenucount"].ToString()) > 0) {
                    DataTable dtchild = MenuController.Menu_GetChildMenuListBaseonRoleAuthorized(ContentSession.RoleID, int.Parse(dr["menu_id"].ToString()), "en");
                    if (dtchild.Rows.Count > 0) {
                        sb.Append("<ul class=\"nav nav-second-level collapse\" aria-expanded=\"false\" >");
                        foreach (DataRow drwchild in dtchild.Rows) {
                            int yCount = dt.Rows.Count;
                            xCount--;                            
                            sb.Append(string.Format(" <li><a href=\"{0}{1}\" data=\"{3}\">{2} {3}{4}</a>", "", ResolveUrl(drwchild["menu_url"].ToString()), drwchild["icon"].ToString(), drwchild["menu_name"].ToString(), int.Parse(drwchild["childmenucount"].ToString()) > 0 ? "<span class=\"fa arrow\"></span>" : ""));                            
                        }
                        sb.Append("</ul>");
                    }
                }
                sb.Append("</li>");
            }
        }

        ltrnavbarleft.Text = sb.ToString();
    }    

    private string Greeting() {
        if (DateTime.Now.Hour < 12) {
            return "Good Morning, ";
        }
        else if (DateTime.Now.Hour < 17){
            return "Good Afternoon, ";
        }
        else
            return "Good Evening, ";
    }


    protected void Timer1_Tick(object sender, EventArgs e)
    {
        //Label1.Text = DateTime.Now.ToString("dd MMM yyyy hh:mm tt") + " (UTC + 07:00)";
    }
    protected void lbtHomeDashboard_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/frmDashboardChecking.aspx"));
    }
}