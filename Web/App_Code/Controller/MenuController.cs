using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using EBOQ_Lib_New.BOL;

/// <summary>
/// Summary description for MenuController
/// </summary>
public static class MenuController
{
    public static DataTable Menu_GetParentMenuListBaseonRoleAuthorized(int roleid, string lang) {
        return new BOL_Menu().BOL_Menu_GetParentMenuListBaseonRoleAuthorized(roleid, lang);
    }

    public static DataTable Menu_GetChildMenuListBaseonRoleAuthorized(int roleid, int parentid, string lang) {
        return new BOL_Menu().BOL_Menu_GetChildMenuListBaseonRoleAuthorized(roleid, parentid, lang);
    }
}