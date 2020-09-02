using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eLoi.DataAccess;
using System.Data;

namespace eLoi.Controller
{
    public class MasterController
    {
        MasterDataAccess da;
        public MasterController()
        {
            da = new MasterDataAccess();
        }

        public DataTable master_workflow_getdata()
        {
            return da.master_workflow_getdata();
        }

        public bool master_workflow_iud(string wf_name, string wf_desc, string wfid, bool isDelete)
        {
            return da.master_workflow_iud(wf_name, wf_desc, wfid, isDelete);
        }

        public DataTable scope_type_getData()
        {
            return da.scope_type_getData();
        }

        public DataTable TRole_getData()
        {
            return da.TRole_getData();
        }

        public DataTable Task_GetAll()
        {
            return da.Task_GetAll();
        }

        public DataTable WFDef_GetDetail(int wfid)
        {
            return da.WFDef_GetDetail(wfid);
        }

        public bool WFDef_IU(Int32 wfdefid, int roleid, int wfid, int seqno, int lmby, int taskid)
        {
            return da.WFDef_IU(wfdefid, roleid, wfid, seqno, lmby, taskid);
        }

        public bool WFDef_Seqno_U(int wfdefid, string stepflag)
        {
            return da.WFDef_Seqno_U(wfdefid, stepflag);
        }
        public bool WFDef_D(Int32 wfdefid)
        {
            return da.WFDef_D(wfdefid);
        }

        public bool WF_WFDefinedStatus_U(int wfid, int lmby, string definedstatus)
        {
            return da.WF_WFDefinedStatus_U(wfid, lmby, definedstatus);
        }
        public bool TRole_iud(string LVLCode, string Rolecode, string RoleDesc, string roleid, bool isdelete)
        {
            return da.TRole_iud(LVLCode, Rolecode, RoleDesc, roleid, isdelete);
        }

        public bool grouping_scope_Flow_iud(int scope_type_id, int wf_id, string CTName, string groupingid, bool isdelete)
        {
            return da.grouping_scope_Flow_iud(scope_type_id, wf_id, CTName, groupingid, isdelete);
        }

        public DataTable grouping_scope_Flow_getdata()
        {
            return da.grouping_scope_Flow_getdata();
        }

        public DataTable Subcon_getdata()
        {
            return da.Subcon_getdata();
        }

        public bool mapping_subcon_phone_iud(int EPM_Vendor_ID, string SCon_Phone, string SCon_Fax, string mapping_subcon_phone_ID, bool isDelete)
        {
            return da.mapping_subcon_phone_iud(EPM_Vendor_ID, SCon_Phone, SCon_Fax, mapping_subcon_phone_ID, isDelete);
        }

        public DataTable mapping_subcon_phone_getdata()
        {
            return da.mapping_subcon_phone_getdata();
        }

        public DataTable Project_TRole_getByCTName(string CTName)
        {
            return da.Project_TRole_getByCTName(CTName);
        }

        public DataTable Role_Grouping_getdata()
        {
            return da.Role_Grouping_getdata();
        }

        public bool Role_Grouping_iud(int LOI_RoleID, int RoleID, string CTName, string Role_Grouping_ID, bool isdelete)
        {
            return da.Role_Grouping_iud(LOI_RoleID, RoleID, CTName, Role_Grouping_ID, isdelete);
        }

        public DataTable loi_ebastusers_getdata()
        {
            return da.loi_ebastusers_getdata();
        }

        public bool ebastusers_loi_iud(string USRType, string Name, int USRRole, string USRLogin, string Email, string PhoneNo, string SignTitle, string CTName, string USR_ID, bool isDelete)
        {
            string usrPassword = getrandomtext();
            usrPassword = EnDecController.newhaspassword(usrPassword); //EnDecController.EncryptLinkUrlApproval(usrPassword);
            return da.ebastusers_loi_iud(USRType, Name, USRRole, USRLogin, Email, PhoneNo, SignTitle, CTName, usrPassword, USR_ID, isDelete);
        }

        public bool ebastusers_changepassword(string usr_login, string oldPassword, string newPassword)
        {
            oldPassword = EnDecController.newhaspassword(oldPassword); //EnDecController.EncryptLinkUrlApproval(oldPassword);
            newPassword = EnDecController.newhaspassword(newPassword); //EnDecController.EncryptLinkUrlApproval(newPassword);
            return da.ebastusers_changepassword(usr_login, oldPassword, newPassword);
        }

        public DataTable mapping_scope_role_getdata()
        {
            return da.mapping_scope_role_getdata();
        }

        public bool mapping_scope_role_iud(int scope_type_id, int roleid)
        {
            return da.mapping_scope_role_iud(scope_type_id, roleid);
        }

        public DataTable getmenuAccess_Role(int RoleId)
        {
            return da.getmenuAccess_Role(RoleId);
        }

        public bool menu_accessrights_iud(int role_id, int menu_id, bool isdelete)
        {
            return da.menu_accessrights_iud(role_id, menu_id, isdelete);
        }

        public DataTable master_dashboard_getdata()
        {
            return da.master_dashboard_getdata();
        }

        public int getdashboardid_byroleid(int roleid)
        {
            return da.getdashboardid_byroleid(roleid);
        }

        public bool role_dashboard_access_iu(int role_id, int dashboard_id)
        {
            return da.role_dashboard_access_iu(role_id, dashboard_id);
        }

        public DataTable usp_getuserdata_all(string usertype, string name, string level, string role, string userlogin, string email, string phone, string status, string title, string CTName)
        {
            return da.usp_getuserdata_all(usertype, name, level, role, userlogin, email, phone, status, title, CTName);
        }

        public bool reset_password_all_ct(int USR_ID, string USRPassword, string CTName)
        {
            USRPassword = EnDecController.newhaspassword(USRPassword);
            return da.reset_password_all_ct(USR_ID, USRPassword, CTName);
        }

        public string getrandomtext()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 6)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public DataTable getmenuAccess_Role_Child(int RoleId, int parentmenuid)
        {
            return da.getmenuAccess_Role_Child(RoleId, parentmenuid);
        }

        public string getusrlogin_byuserid()
        {
            return da.getusrlogin_byuserid();
        }

        public bool scope_type_iud(string scope_type, string scope_type_id, bool isdelete)
        {
            return da.scope_type_iud(scope_type, scope_type_id, isdelete);
        }

        public DataTable master_sow_getall()
        {
            return da.master_sow_getall();
        }

        public bool master_sow_iud(int scope_type_id, string general_sow, string detail_sow, string master_sow_id, bool isdelete)
        {
            return da.master_sow_iud(scope_type_id, general_sow, detail_sow, master_sow_id, isdelete);
        }
    }
}
