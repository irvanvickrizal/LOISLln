using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using EBOQ_Lib_New.DAL;

namespace eLoi.DataAccess
{    public class MasterDataAccess : DBConfiguration
    {
        public MasterDataAccess()
        {

        }

        public DataTable master_workflow_getdata()
        {
            Command = new SqlCommand("usp_master_workflow_getdata", Connection);
            Command.CommandType = CommandType.StoredProcedure;            

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "MasterDataAccess:master_workflow_getdata", "usp_master_workflow_getdata");
        }
        public bool master_workflow_iud(string wf_name, string wf_desc, string wf_id, bool isDelete)
        {
            Command = new SqlCommand("usp_master_workflow_iud", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_wf_name = new SqlParameter("@wf_name", SqlDbType.VarChar, 100);
            prm_wf_name.Value = wf_name;
            Command.Parameters.Add(prm_wf_name);

            SqlParameter prm_wf_desc = new SqlParameter("@wf_desc", SqlDbType.VarChar, 200);
            prm_wf_desc.Value = wf_desc;
            Command.Parameters.Add(prm_wf_desc);  
            
            if(!string.IsNullOrEmpty(wf_id))
            {
                SqlParameter prm_wf_id = new SqlParameter("@wf_id", SqlDbType.Int);
                prm_wf_id.Value = int.Parse(wf_id);
                Command.Parameters.Add(prm_wf_id);
            }

            if(isDelete)
            {
                SqlParameter prm_isDelete = new SqlParameter("@isDelete", SqlDbType.Bit);
                prm_isDelete.Value = isDelete;
                Command.Parameters.Add(prm_isDelete);
            }

            SqlParameter prm_lmby = new SqlParameter("@lmby", SqlDbType.Int);
            prm_lmby.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_lmby);           

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "MasterDataAccess:master_workflow_iud", "usp_master_workflow_iud");
        }

        public DataTable scope_type_getData()
        {
            Command = new SqlCommand("usp_scope_type_getData", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "MasterDataAccess:scope_type_getData", "usp_scope_type_getData");
        }

        public DataTable TRole_getData()
        {
            Command = new SqlCommand("usp_TRole_getData", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "MasterDataAccess:TRole_getData", "usp_TRole_getData");
        }
        public DataTable Task_GetAll()
        {
            Command = new SqlCommand("uspTask_GetAll", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "MasterDataAccess:Task_GetAll", "uspTask_GetAll");
        }

        public DataTable WFDef_GetDetail(int wfid)
        {
            Command = new SqlCommand("uspWFDef_GetDetail", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_wfid = new SqlParameter("@wfid", SqlDbType.Int);
            prm_wfid.Value = wfid;
            Command.Parameters.Add(prm_wfid);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "MasterDataAccess:WFDef_GetDetail", "uspWFDef_GetDetail");
        }

        public bool WFDef_IU(Int32 wfdefid, int roleid, int wfid, int seqno, int lmby, int taskid)
        {
            Command = new SqlCommand("uspWFDef_IU", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            SqlParameter prmWFDefID = new SqlParameter("@wfdefid", SqlDbType.BigInt);
            SqlParameter prmWFID = new SqlParameter("@wfid", SqlDbType.Int);
            SqlParameter prmRoleID = new SqlParameter("@roleid", SqlDbType.Int);
            SqlParameter prmLMBY = new SqlParameter("@lmby", SqlDbType.Int);
            SqlParameter prmSeqno = new SqlParameter("@seqno", SqlDbType.Int);
            SqlParameter prmTaskID = new SqlParameter("@taskid", SqlDbType.Int);

            prmWFDefID.Value = wfdefid;
            prmWFID.Value = wfid;
            prmRoleID.Value = roleid;
            prmLMBY.Value = lmby;
            prmSeqno.Value = seqno;
            prmTaskID.Value = taskid;
            Command.Parameters.Add(prmWFDefID);
            Command.Parameters.Add(prmWFID);
            Command.Parameters.Add(prmRoleID);
            Command.Parameters.Add(prmLMBY);
            Command.Parameters.Add(prmSeqno);
            Command.Parameters.Add(prmTaskID);

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "MasterDataAccess:WFDef_IU", "uspWFDef_IU");
        }

        public bool WFDef_Seqno_U(int wfdefid, string stepflag)
        {
            Command = new SqlCommand("uspWFDef_Seqno_U", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            SqlParameter prmWFDefID = new SqlParameter("@wfdefid", SqlDbType.BigInt);
            SqlParameter prmStepFlag = new SqlParameter("@stepflag", SqlDbType.VarChar, 10);
            SqlParameter prmLMBY = new SqlParameter("@lmby", SqlDbType.Int);
            prmWFDefID.Value = wfdefid;
            prmStepFlag.Value = stepflag;
            prmLMBY.Value = ContentSession.USERID;
            Command.Parameters.Add(prmWFDefID);
            Command.Parameters.Add(prmStepFlag);
            Command.Parameters.Add(prmLMBY);
            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "MasterDataAccess:WFDef_Seqno_U", "uspWFDef_Seqno_U");
        }

        public bool WFDef_D(Int32 wfdefid)
        {
            Command = new SqlCommand("uspWFDef_D", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            SqlParameter prmWFDefID = new SqlParameter("@wfdefid", SqlDbType.BigInt);
            SqlParameter prmLMBY = new SqlParameter("@lmby", SqlDbType.Int);
            prmWFDefID.Value = wfdefid;
            prmLMBY.Value = ContentSession.USERID;
            Command.Parameters.Add(prmWFDefID);
            Command.Parameters.Add(prmLMBY);
            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "MasterDataAccess:WFDef_D", "uspWFDef_D");
        }

        public bool WF_WFDefinedStatus_U(int wfid, int lmby, string definedstatus)
        {
            Command = new SqlCommand("uspWF_WFDefinedStatus_U", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            SqlParameter prmWFID = new SqlParameter("@wfid", SqlDbType.Int);
            SqlParameter prmDefinedStatus = new SqlParameter("@wfdefinedstatus", SqlDbType.VarChar, 20);
            SqlParameter prmLMBY = new SqlParameter("@lmby", SqlDbType.Int);
            prmWFID.Value = wfid;
            prmDefinedStatus.Value = definedstatus;
            prmLMBY.Value = lmby;
            Command.Parameters.Add(prmWFID);
            Command.Parameters.Add(prmDefinedStatus);
            Command.Parameters.Add(prmLMBY);
            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "MasterDataAccess:WF_WFDefinedStatus_U", "uspWF_WFDefinedStatus_U");
        }

        public bool TRole_iud(string LVLCode, string Rolecode, string RoleDesc, string roleid, bool isdelete)
        {
            Command = new SqlCommand("usp_TRole_iud", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            SqlParameter prmLVLCode = new SqlParameter("@LVLCode", SqlDbType.VarChar, 2);
            SqlParameter prmRolecode = new SqlParameter("@Rolecode", SqlDbType.VarChar, 2);
            SqlParameter prmRoleDesc = new SqlParameter("@RoleDesc", SqlDbType.VarChar, 50);
            SqlParameter prmlmby = new SqlParameter("@lmby", SqlDbType.Int);

            prmLVLCode.Value = LVLCode;
            prmRolecode.Value = Rolecode;
            prmRoleDesc.Value = RoleDesc;
            prmlmby.Value = ContentSession.USERID;

            Command.Parameters.Add(prmLVLCode);
            Command.Parameters.Add(prmRolecode);
            Command.Parameters.Add(prmRoleDesc);
            Command.Parameters.Add(prmlmby);

            if (!string.IsNullOrEmpty(roleid))
            {
                SqlParameter prmRoleID = new SqlParameter("@RoleID", SqlDbType.Int);
                prmRoleID.Value = int.Parse(roleid);
                Command.Parameters.Add(prmRoleID);
            }

            if (isdelete)
            {
                SqlParameter prmisDelete = new SqlParameter("@isDelete", SqlDbType.Bit);
                prmisDelete.Value = isdelete;
                Command.Parameters.Add(prmisDelete);
            }

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "MasterDataAccess:TRole_iud", "usp_TRole_iud");
        }

        public bool grouping_scope_Flow_iud(int scope_type_id, int wf_id, string CTName, string groupingid, bool isdelete)
        {
            Command = new SqlCommand("usp_grouping_scope_Flow_iud", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            SqlParameter prmscope_type_id = new SqlParameter("@scope_type_id", SqlDbType.Int);
            SqlParameter prmwf_id = new SqlParameter("@wf_id", SqlDbType.Int);
            SqlParameter prmlmby = new SqlParameter("@lmby", SqlDbType.Int);
            SqlParameter prmCTName = new SqlParameter("@CTName", SqlDbType.VarChar, 50);

            prmscope_type_id.Value = scope_type_id;
            prmwf_id.Value = wf_id;
            prmlmby.Value = ContentSession.USERID;
            prmCTName.Value = CTName;

            Command.Parameters.Add(prmscope_type_id);
            Command.Parameters.Add(prmwf_id);
            Command.Parameters.Add(prmlmby);
            Command.Parameters.Add(prmCTName);

            if(!string.IsNullOrEmpty(groupingid))
            {
                SqlParameter prmgrouping_scope_Flow_id = new SqlParameter("@grouping_scope_Flow_id", SqlDbType.Int);
                prmgrouping_scope_Flow_id.Value = int.Parse(groupingid);
                Command.Parameters.Add(prmgrouping_scope_Flow_id);
            }

            if(isdelete)
            {
                SqlParameter prmisDelete = new SqlParameter("@isDelete", SqlDbType.Bit);
                prmisDelete.Value = isdelete;
                Command.Parameters.Add(prmisDelete);
            }

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "MasterDataAccess:grouping_scope_Flow_iud", "usp_grouping_scope_Flow_iud");
        }

        public DataTable grouping_scope_Flow_getdata()
        {
            Command = new SqlCommand("usp_grouping_scope_Flow_getdata", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "MasterDataAccess:grouping_scope_Flow_getdata", "usp_grouping_scope_Flow_getdata");
        }

        public DataTable Subcon_getdata()
        {
            Command = new SqlCommand("usp_Subcon_getdata", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "MasterDataAccess:Subcon_getdata", "usp_Subcon_getdata");
        }

        public bool mapping_subcon_phone_iud(int EPM_Vendor_ID, string SCon_Phone, string SCon_Fax, string mapping_subcon_phone_ID, bool isDelete)
        {
            Command = new SqlCommand("usp_mapping_subcon_phone_iud", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            SqlParameter prmEPM_Vendor_ID = new SqlParameter("@EPM_Vendor_ID", SqlDbType.Int);
            SqlParameter prmSCon_Phone = new SqlParameter("@SCon_Phone", SqlDbType.VarChar, 200);
            SqlParameter prmSCon_Fax = new SqlParameter("@SCon_Fax", SqlDbType.VarChar, 200);
            SqlParameter prmlmby = new SqlParameter("@lmby", SqlDbType.Int);

            prmEPM_Vendor_ID.Value = EPM_Vendor_ID;
            prmSCon_Phone.Value = SCon_Phone;
            prmSCon_Fax.Value = SCon_Fax;
            prmlmby.Value = ContentSession.USERID;

            Command.Parameters.Add(prmEPM_Vendor_ID);
            Command.Parameters.Add(prmSCon_Phone);
            Command.Parameters.Add(prmSCon_Fax);
            Command.Parameters.Add(prmlmby);

            if(!string.IsNullOrEmpty(mapping_subcon_phone_ID))
            {
                SqlParameter prmmapping_subcon_phone_ID = new SqlParameter("@mapping_subcon_phone_ID", SqlDbType.Int);
                prmmapping_subcon_phone_ID.Value = mapping_subcon_phone_ID;
                Command.Parameters.Add(prmmapping_subcon_phone_ID);
            }

            if(isDelete)
            {
                SqlParameter prmisDelete = new SqlParameter("@isDelete", SqlDbType.Bit);
                prmisDelete.Value = isDelete;
                Command.Parameters.Add(prmisDelete);
            }

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "MasterDataAccess:mapping_subcon_phone_iud", "usp_mapping_subcon_phone_iud");
        }

        public DataTable mapping_subcon_phone_getdata()
        {
            Command = new SqlCommand("usp_mapping_subcon_phone_getdata", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "MasterDataAccess:mapping_subcon_phone_getdata", "usp_mapping_subcon_phone_getdata");
        }

        public DataTable Project_TRole_getByCTName(string CTName)
        {
            Command = new SqlCommand("usp_Project_TRole_getByCTName", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_CTName = new SqlParameter("@CTName", SqlDbType.VarChar, 50);
            prm_CTName.Value = CTName;
            Command.Parameters.Add(prm_CTName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "MasterDataAccess:Project_TRole_getByCTName", "usp_Project_TRole_getByCTName");
        }

        public DataTable Role_Grouping_getdata()
        {
            Command = new SqlCommand("usp_Role_Grouping_getdata", Connection);
            Command.CommandType = CommandType.StoredProcedure;            

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "MasterDataAccess:Role_Grouping_getdata", "usp_Role_Grouping_getdata");
        }

        public bool Role_Grouping_iud(int LOI_RoleID, int RoleID, string CTName, string Role_Grouping_ID, bool isdelete)
        {
            Command = new SqlCommand("usp_Role_Grouping_iud", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            SqlParameter prmLOI_RoleID = new SqlParameter("@LOI_RoleID", SqlDbType.Int);
            SqlParameter prmRoleID = new SqlParameter("@RoleID", SqlDbType.Int);
            SqlParameter prmCTName = new SqlParameter("@CTName", SqlDbType.VarChar, 50);
            SqlParameter prmlmby = new SqlParameter("@lmby", SqlDbType.Int);

            prmLOI_RoleID.Value = LOI_RoleID;
            prmRoleID.Value = RoleID;
            prmCTName.Value = CTName;
            prmlmby.Value = ContentSession.USERID;

            Command.Parameters.Add(prmLOI_RoleID);
            Command.Parameters.Add(prmRoleID);
            Command.Parameters.Add(prmCTName);
            Command.Parameters.Add(prmlmby);

            if(!string.IsNullOrEmpty(Role_Grouping_ID))
            {
                SqlParameter prmRole_Grouping_ID = new SqlParameter("@Role_Grouping_ID", SqlDbType.Int);
                prmRole_Grouping_ID.Value = int.Parse(Role_Grouping_ID);
                Command.Parameters.Add(prmRole_Grouping_ID);
            }

            if(isdelete)
            {
                SqlParameter prmisDelete = new SqlParameter("@isDelete", SqlDbType.Bit);
                prmisDelete.Value = isdelete;
                Command.Parameters.Add(prmisDelete);
            }

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "MasterDataAccess:Role_Grouping_iud", "usp_Role_Grouping_iud");
        }
        public DataTable loi_ebastusers_getdata()
        {
            Command = new SqlCommand("usp_loi_ebastusers_getdata", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "MasterDataAccess:loi_ebastusers_getdata", "usp_loi_ebastusers_getdata");
        }

        public bool ebastusers_loi_iud(string USRType, string Name, int USRRole, string USRLogin, string Email, string PhoneNo, string SignTitle, string CTName, string USRPassword, string USR_ID, bool isDelete)
        {
            Command = new SqlCommand("usp_ebastusers_loi_iud", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            SqlParameter prmUSRType = new SqlParameter("@USRType", SqlDbType.VarChar, 1);
            SqlParameter prmName = new SqlParameter("@Name", SqlDbType.VarChar, 30);
            SqlParameter prmUSRRole = new SqlParameter("@USRRole", SqlDbType.BigInt);
            SqlParameter prmUSRLogin = new SqlParameter("@USRLogin", SqlDbType.VarChar, 30);
            SqlParameter prmUSRPassword = new SqlParameter("@USRPassword", SqlDbType.VarChar, 100);
            SqlParameter prmEmail = new SqlParameter("@Email", SqlDbType.VarChar, 50);
            SqlParameter prmPhoneNo = new SqlParameter("@PhoneNo", SqlDbType.VarChar, 20);
            SqlParameter prmLMBY = new SqlParameter("@LMBY", SqlDbType.VarChar, 50);
            SqlParameter prmSignTitle = new SqlParameter("@SignTitle", SqlDbType.VarChar, 100);
            SqlParameter prmCTName = new SqlParameter("@CTName", SqlDbType.VarChar, 50);

            prmUSRType.Value = USRType;
            prmName.Value = Name;
            prmUSRRole.Value = USRRole;
            prmUSRLogin.Value = USRLogin;
            prmUSRPassword.Value = USRPassword;
            prmEmail.Value = Email;
            prmPhoneNo.Value = PhoneNo;
            prmLMBY.Value = ContentSession.FULLNAME;
            prmSignTitle.Value = SignTitle;
            prmCTName.Value = CTName;

            Command.Parameters.Add(prmUSRType);
            Command.Parameters.Add(prmName);
            Command.Parameters.Add(prmUSRRole);
            Command.Parameters.Add(prmUSRLogin);
            Command.Parameters.Add(prmUSRPassword);
            Command.Parameters.Add(prmEmail);
            Command.Parameters.Add(prmPhoneNo);
            Command.Parameters.Add(prmLMBY);
            Command.Parameters.Add(prmSignTitle);
            Command.Parameters.Add(prmCTName);

            if(!string.IsNullOrEmpty(USR_ID))
            {
                SqlParameter prmUSR_ID = new SqlParameter("@USR_ID", SqlDbType.BigInt);
                prmUSR_ID.Value = int.Parse(USR_ID);
                Command.Parameters.Add(prmUSR_ID);
            }

            if(isDelete)
            {
                SqlParameter prmisDelete = new SqlParameter("@isDelete", SqlDbType.Bit);
                prmisDelete.Value = isDelete;
                Command.Parameters.Add(prmisDelete);
            }
            

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "MasterDataAccess:ebastusers_loi_iud", "usp_ebastusers_loi_iud");
        }

        public bool ebastusers_changepassword(string usr_login, string oldPassword, string newPassword)
        {
            Command = new SqlCommand("usp_ebastusers_changepassword", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prmusr_login = new SqlParameter("@USRLogin", SqlDbType.VarChar, 30);
            SqlParameter prmoldPassword = new SqlParameter("@oldPassword", SqlDbType.VarChar, 100);
            SqlParameter prmnewPassword = new SqlParameter("@newPassword", SqlDbType.VarChar, 100);
            SqlParameter prmCTName = new SqlParameter("@CTName", SqlDbType.VarChar, 50);
            SqlParameter prmlmby = new SqlParameter("@lmby", SqlDbType.Int);

            prmusr_login.Value = usr_login;
            prmoldPassword.Value = oldPassword;
            prmnewPassword.Value = newPassword;
            prmCTName.Value = ContentSession.CTName;
            prmlmby.Value = ContentSession.USERID;


            Command.Parameters.Add(prmusr_login);
            Command.Parameters.Add(prmoldPassword);
            Command.Parameters.Add(prmnewPassword);
            Command.Parameters.Add(prmCTName);
            Command.Parameters.Add(prmlmby);

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "MasterDataAccess:ebastusers_changepassword", "usp_ebastusers_changepassword");
        }

        public DataTable mapping_scope_role_getdata()
        {
            Command = new SqlCommand("usp_mapping_scope_role_getdata", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "MasterDataAccess:mapping_scope_role_getdata", "usp_mapping_scope_role_getdata");
        }

        public bool mapping_scope_role_iud(int scope_type_id, int roleid)
        {
            Command = new SqlCommand("usp_mapping_scope_role_iud", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prmscope_type_id = new SqlParameter("@scope_type_id", SqlDbType.Int);
            SqlParameter prmRoleID = new SqlParameter("@RoleID", SqlDbType.Int);
            SqlParameter prmlmby = new SqlParameter("@lmby", SqlDbType.Int);

            prmscope_type_id.Value = scope_type_id;
            prmRoleID.Value = roleid;
            prmlmby.Value = ContentSession.USERID;

            Command.Parameters.Add(prmscope_type_id);
            Command.Parameters.Add(prmRoleID);
            Command.Parameters.Add(prmlmby);

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "MasterDataAccess:ebastusers_changepassword", "usp_ebastusers_changepassword");
        }

        public DataTable getmenuAccess_Role(int RoleId)
        {
            Command = new SqlCommand("usp_getmenuAccess_Role", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_RoleId = new SqlParameter("@RoleId", SqlDbType.Int);
            prm_RoleId.Value = RoleId;
            Command.Parameters.Add(prm_RoleId);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "MasterDataAccess:getmenuAccess_Role", "usp_getmenuAccess_Role");
        }

        public bool menu_accessrights_iud(int role_id, int menu_id, bool isdelete)
        {
            Command = new SqlCommand("usp_menu_accessrights_iud", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prmrole_id = new SqlParameter("@role_id", SqlDbType.Int);
            SqlParameter prmmenu_id = new SqlParameter("@menu_id", SqlDbType.Int);
            SqlParameter prmlmby = new SqlParameter("@lmby", SqlDbType.Int);

            prmrole_id.Value = role_id;
            prmmenu_id.Value = menu_id;
            prmlmby.Value = ContentSession.USERID;

            Command.Parameters.Add(prmrole_id);
            Command.Parameters.Add(prmmenu_id);
            Command.Parameters.Add(prmlmby);

            if (isdelete)
            {
                SqlParameter prmisDelete = new SqlParameter("@isDelete", SqlDbType.Bit);
                prmisDelete.Value = isdelete;
                Command.Parameters.Add(prmisDelete);
            }

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "MasterDataAccess:menu_accessrights_iud", "usp_menu_accessrights_iud");
        }

        public DataTable master_dashboard_getdata()
        {
            Command = new SqlCommand("usp_master_dashboard_getdata", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "MasterDataAccess:master_dashboard_getdata", "usp_master_dashboard_getdata");
        }

        public int getdashboardid_byroleid(int roleid)
        {
            Command = new SqlCommand("usp_getdashboardid_byroleid", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_role_id = new SqlParameter("@role_id", SqlDbType.Int);
            prm_role_id.Value = roleid;
            Command.Parameters.Add(prm_role_id);
                        
            object dt = AdditionalExecuteCommand.ExecuteScalar(Command, "MasterDataAccess:getdashboardid_byroleid", "usp_getdashboardid_byroleid");
            int dtInt = 0;
            if (dt != null)
                dtInt = (int)dt;
            return dtInt;
        }

        public bool role_dashboard_access_iu(int role_id, int dashboard_id)
        {
            Command = new SqlCommand("usp_role_dashboard_access_iu", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prmrole_id = new SqlParameter("@role_id", SqlDbType.Int);
            SqlParameter prmdashboard_id = new SqlParameter("@dashboard_id", SqlDbType.Int);
            SqlParameter prmlmby = new SqlParameter("@lmby", SqlDbType.Int);

            prmrole_id.Value = role_id;
            prmdashboard_id.Value = dashboard_id;
            prmlmby.Value = ContentSession.USERID;

            Command.Parameters.Add(prmrole_id);
            Command.Parameters.Add(prmdashboard_id);
            Command.Parameters.Add(prmlmby);                       

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "MasterDataAccess:role_dashboard_access_iu", "usp_role_dashboard_access_iu");
        }

        public DataTable usp_getuserdata_all(string usertype, string name, string level, string role, string userlogin, string email, string phone, string status, string title, string CTName)
        {
            Command = new SqlCommand("usp_getuserdata_all", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            if(!string.IsNullOrEmpty(usertype))
            {
                SqlParameter prm_USRType = new SqlParameter("@USRType", SqlDbType.VarChar, 1);
                prm_USRType.Value = usertype;
                Command.Parameters.Add(prm_USRType);
            }

            if (!string.IsNullOrEmpty(name))
            {
                SqlParameter prm_name = new SqlParameter("@Name", SqlDbType.VarChar, 30);
                prm_name.Value = name;
                Command.Parameters.Add(prm_name);
            }

            if (!string.IsNullOrEmpty(level))
            {
                SqlParameter prm_level = new SqlParameter("@LVLCode", SqlDbType.VarChar, 5);
                prm_level.Value = level;
                Command.Parameters.Add(prm_level);
            }

            if (role != "0")
            {
                SqlParameter prm_role = new SqlParameter("@USRRole", SqlDbType.BigInt);
                prm_role.Value = int.Parse(role);
                Command.Parameters.Add(prm_role);
            }

            if (!string.IsNullOrEmpty(userlogin))
            {
                SqlParameter prm_userlogin = new SqlParameter("@USRLogin", SqlDbType.VarChar, 30);
                prm_userlogin.Value = userlogin;
                Command.Parameters.Add(prm_userlogin);
            }

            if (!string.IsNullOrEmpty(email))
            {
                SqlParameter prm_email = new SqlParameter("@Email", SqlDbType.VarChar, 50);
                prm_email.Value = email;
                Command.Parameters.Add(prm_email);
            }

            if (!string.IsNullOrEmpty(phone))
            {
                SqlParameter prm_PhoneNo = new SqlParameter("@PhoneNo", SqlDbType.VarChar, 20);
                prm_PhoneNo.Value = phone;
                Command.Parameters.Add(prm_PhoneNo);
            }

            if (!string.IsNullOrEmpty(status))
            {
                SqlParameter prm_status = new SqlParameter("@ACC_Status", SqlDbType.Char, 1);
                prm_status.Value = status;
                Command.Parameters.Add(prm_status);
            }

            if (!string.IsNullOrEmpty(title))
            {
                SqlParameter prm_title = new SqlParameter("@SignTitle", SqlDbType.VarChar, 100);
                prm_title.Value = title;
                Command.Parameters.Add(prm_title);
            }

            if (!string.IsNullOrEmpty(CTName))
            {
                SqlParameter prm_CTName = new SqlParameter("@CTName", SqlDbType.VarChar, 50);
                prm_CTName.Value = CTName;
                Command.Parameters.Add(prm_CTName);
            }

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "MasterDataAccess:getuserdata_all", "usp_getuserdata_all");
        }

        public bool reset_password_all_ct(int USR_ID, string USRPassword, string CTName)
        {
            Command = new SqlCommand("usp_reset_password_all_ct", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prmUSR_ID = new SqlParameter("@USR_ID", SqlDbType.BigInt);
            SqlParameter prmUSRPassword = new SqlParameter("@USRPassword", SqlDbType.VarChar, 100);
            SqlParameter prmCTName = new SqlParameter("@CTName", SqlDbType.VarChar, 50);
            SqlParameter prmlmby = new SqlParameter("@lmby", SqlDbType.VarChar, 50);

            prmUSR_ID.Value = USR_ID;
            prmUSRPassword.Value = USRPassword;
            prmCTName.Value = CTName;
            prmlmby.Value = ContentSession.FULLNAME;

            Command.Parameters.Add(prmUSR_ID);
            Command.Parameters.Add(prmUSRPassword);
            Command.Parameters.Add(prmCTName);
            Command.Parameters.Add(prmlmby);

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "MasterDataAccess:reset_password_all_ct", "usp_reset_password_all_ct");
        }

        public DataTable getmenuAccess_Role_Child(int RoleId, int parentmenuid)
        {
            Command = new SqlCommand("usp_getmenuAccess_Role_Child", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_RoleId = new SqlParameter("@RoleId", SqlDbType.Int);
            prm_RoleId.Value = RoleId;
            Command.Parameters.Add(prm_RoleId);

            SqlParameter prm_parentmenuid = new SqlParameter("@parent_menu_id", SqlDbType.Int);
            prm_parentmenuid.Value = parentmenuid;
            Command.Parameters.Add(prm_parentmenuid);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "MasterDataAccess:getmenuAccess_Role_Child", "usp_getmenuAccess_Role_Child");
        }

        public string getusrlogin_byuserid()
        {
            Command = new SqlCommand("usp_getusrlogin_byuserid", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_USR_ID = new SqlParameter("@USR_ID", SqlDbType.BigInt);
            prm_USR_ID.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_USR_ID);

            SqlParameter prm_CTName = new SqlParameter("@CTName", SqlDbType.VarChar, 50);
            prm_CTName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_CTName);

            object dt = AdditionalExecuteCommand.ExecuteScalar(Command, "MasterDataAccess:getusrlogin_byuserid", "usp_getusrlogin_byuserid");
            string result = string.Empty;
            if (dt != null)
                result = dt.ToString();
            return result;
        }

        public bool scope_type_iud(string scope_type, string scope_type_id, bool isdelete)
        {
            Command = new SqlCommand("usp_scope_type_iud", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            SqlParameter prmscope_type = new SqlParameter("@scope_type", SqlDbType.VarChar, 50);
            SqlParameter prmlmby = new SqlParameter("@LMBY", SqlDbType.Int);

            prmscope_type.Value = scope_type;
            prmlmby.Value = ContentSession.USERID;

            Command.Parameters.Add(prmscope_type);
            Command.Parameters.Add(prmlmby);

            if (!string.IsNullOrEmpty(scope_type_id))
            {
                SqlParameter prmscope_type_id = new SqlParameter("@scope_type_id", SqlDbType.Int);
                prmscope_type_id.Value = int.Parse(scope_type_id);
                Command.Parameters.Add(prmscope_type_id);
            }

            if (isdelete)
            {
                SqlParameter prmisDelete = new SqlParameter("@isDelete", SqlDbType.Bit);
                prmisDelete.Value = isdelete;
                Command.Parameters.Add(prmisDelete);
            }

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "MasterDataAccess:scope_type_iud", "usp_scope_type_iud");
        }

        public DataTable master_sow_getall()
        {
            Command = new SqlCommand("usp_master_sow_getall", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "MasterDataAccess:master_sow_getall", "usp_master_sow_getall");
        }

        public bool master_sow_iud(int scope_type_id, string general_sow, string detail_sow, string master_sow_id, bool isdelete)
        {
            Command = new SqlCommand("usp_master_sow_iud", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            SqlParameter prmscope_type_id = new SqlParameter("@scope_type_id", SqlDbType.Int);
            SqlParameter prmgeneral_sow = new SqlParameter("@general_sow", SqlDbType.VarChar, 200);
            SqlParameter prmdetail_sow = new SqlParameter("@detail_sow", SqlDbType.VarChar, 200);
            SqlParameter prmlmby = new SqlParameter("@LMBY", SqlDbType.Int);

            prmscope_type_id.Value = scope_type_id;
            prmgeneral_sow.Value = general_sow;
            prmdetail_sow.Value = detail_sow;
            prmlmby.Value = ContentSession.USERID;

            Command.Parameters.Add(prmscope_type_id);
            Command.Parameters.Add(prmgeneral_sow);
            Command.Parameters.Add(prmdetail_sow);
            Command.Parameters.Add(prmlmby);

            if (!string.IsNullOrEmpty(master_sow_id))
            {
                SqlParameter prmmaster_sow_id = new SqlParameter("@master_sow_id", SqlDbType.Int);
                prmmaster_sow_id.Value = int.Parse(master_sow_id);
                Command.Parameters.Add(prmmaster_sow_id);
            }

            if (isdelete)
            {
                SqlParameter prmisDelete = new SqlParameter("@isDelete", SqlDbType.Bit);
                prmisDelete.Value = isdelete;
                Command.Parameters.Add(prmisDelete);
            }

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "MasterDataAccess:master_sow_iud", "usp_master_sow_iud");
        }
    }
}
