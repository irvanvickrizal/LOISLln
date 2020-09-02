using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using EBOQ_Lib_New.BOL;
using EBOQ_Lib_New.Entities;

/// <summary>
/// Summary description for UserController
/// </summary>
public static class UserController
{	
    
    public static DataTable User_LoginValidation(string username, string password) {
        return new BOL_User().ValidateUserLogin(username, password);    
    }

    public static DataTable UserGetDetail(int userid) {
        return new BOL_User().UserGetDetail(userid);
    }

    public static bool UserChangePassword(int userid, string strPassword) {
        return new BOL_User().UserChangePassword(userid, strPassword);
    }

    public static bool UserChangeProfile(UserInfo info) {
        return new BOL_User().UserChangeProfile(info);
    }

    public static DataTable Role_GetRegisteredPermission(string rolenamefilter) {
        return new BOL_User().Role_GetRegisteredPermission(rolenamefilter);
    }

    public static DataTable Role_GetNotRegisteredPermission() {
        return new BOL_User().Role_GetNotRegisteredPermission();
    }

    public static bool Role_Permission_I(int roleid, int lmby) {
        return new BOL_User().Role_Permission_I(roleid, lmby);
    }

    public static bool Role_RegisteredPermission_D(int roleaccessid) {
        return new BOL_User().Role_RegisteredPermission_D(roleaccessid);
    }
}