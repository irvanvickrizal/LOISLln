using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EBOQ_Lib_New.BOL;
using EBOQ_Lib_New.Entities;

/// <summary>
/// Summary description for DashboardController
/// </summary>
public static class DashboardController
{
	public static DashboardRoleInfo DashboardViewUser(int userid){
        return new BOL_Dashboard().User_GetDashboard(userid);
    }

    public static string DashboardCheckingURL() {
        return "~/frmDashboardChecking.aspx";
    }

    public static string DashboardDefURL() {
        return "~/dashboard/frmDashboardGeneral.aspx";
    }
}