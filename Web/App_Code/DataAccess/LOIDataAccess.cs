using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using EBOQ_Lib_New.DAL;

namespace eLoi.DataAccess
{
    public class LOIDataAccess : DBConfiguration
    {
        public LOIDataAccess()
        {

        }

        public int LOI_Upload_by_FM(int subcon_Id, int scope_type_id)
        {
            try
            {
                Command = new SqlCommand("usp_LOI_Upload_by_FM", Connection);
                Command.CommandType = CommandType.StoredProcedure;

                SqlParameter prm_lmby = new SqlParameter("@lmby", SqlDbType.Int);
                prm_lmby.Value = ContentSession.USERID;
                Command.Parameters.Add(prm_lmby);

                SqlParameter prm_subcon_Id = new SqlParameter("@subcon_Id", SqlDbType.Int);
                prm_subcon_Id.Value = subcon_Id;
                Command.Parameters.Add(prm_subcon_Id);

                SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
                prm_ProjectName.Value = ContentSession.CTName;
                Command.Parameters.Add(prm_ProjectName);

                SqlParameter prm_scope_type_id = new SqlParameter("@scope_type_id", SqlDbType.Int);
                prm_scope_type_id.Value = scope_type_id;
                Command.Parameters.Add(prm_scope_type_id);

                object dt = AdditionalExecuteCommand.ExecuteScalar(Command, "LOIDataAccess:LOI_Upload_by_FM", "usp_LOI_Upload_by_FM");
                int dtInt = 0;
                if (dt != null)
                    dtInt = Convert.ToInt32(dt);
                return dtInt;

                //return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "LOIDataAccess:LOI_Upload_by_FM", "usp_LOI_Upload_by_FM");
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public bool LOI_Detail_Upload_by_FM(DataRow drLOI, int RequestId, int RegionId, int ARA_ID)
        {
            Command = new SqlCommand("usp_LOI_Detail_Upload_by_FM", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
            prm_RequestId.Value = RequestId;
            Command.Parameters.Add(prm_RequestId);

            SqlParameter prm_workpackageid = new SqlParameter("@workpackageid", SqlDbType.VarChar, 50);
            prm_workpackageid.Value = drLOI["workpackageid"];
            Command.Parameters.Add(prm_workpackageid);

            SqlParameter prm_Customer_PO = new SqlParameter("@Customer_PO", SqlDbType.VarChar, 100);
            prm_Customer_PO.Value = drLOI["Customer_PO"];
            Command.Parameters.Add(prm_Customer_PO);

            SqlParameter prm_Customer_PO_Date = new SqlParameter("@Customer_PO_Date", SqlDbType.DateTime);
            prm_Customer_PO_Date.Value = drLOI["Customer_PO_Date"];
            Command.Parameters.Add(prm_Customer_PO_Date);

            SqlParameter prm_PO_Description = new SqlParameter("@PO_Description", SqlDbType.VarChar, 500);
            prm_PO_Description.Value = drLOI["PO_Description"];
            Command.Parameters.Add(prm_PO_Description);

            SqlParameter prm_Region = new SqlParameter("@Region", SqlDbType.VarChar, 100);
            prm_Region.Value = drLOI["Region"];
            Command.Parameters.Add(prm_Region);

            SqlParameter prm_Site_ID = new SqlParameter("@Site_ID", SqlDbType.VarChar, 20);
            prm_Site_ID.Value = drLOI["Site_ID"];
            Command.Parameters.Add(prm_Site_ID);

            SqlParameter prm_ScopeOfWork = new SqlParameter("@ScopeOfWork", SqlDbType.VarChar, 500);
            prm_ScopeOfWork.Value = drLOI["ScopeOfWork"];
            Command.Parameters.Add(prm_ScopeOfWork);

            SqlParameter prm_Subcone_Name = new SqlParameter("@Subcone_Name", SqlDbType.VarChar, 100);
            prm_Subcone_Name.Value = drLOI["Subcone_Name"];
            Command.Parameters.Add(prm_Subcone_Name);

            SqlParameter prm_Site_Model = new SqlParameter("@Site_Model", SqlDbType.VarChar, 100);
            prm_Site_Model.Value = drLOI["Site_Model"];
            Command.Parameters.Add(prm_Site_Model);

            SqlParameter prm_lmby = new SqlParameter("@lmby", SqlDbType.Int);
            prm_lmby.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_lmby);

            SqlParameter prm_RGN_ID = new SqlParameter("@RGN_ID", SqlDbType.Int);
            prm_RGN_ID.Value = RegionId;
            Command.Parameters.Add(prm_RGN_ID);

            SqlParameter prm_ARA_ID = new SqlParameter("@ARA_ID", SqlDbType.Int);
            prm_ARA_ID.Value = ARA_ID;
            Command.Parameters.Add(prm_ARA_ID);

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "LOIDataAccess:LOI_Detail_Upload_by_FM", "usp_LOI_Detail_Upload_by_FM");
        }

        public DataTable LOI_get_Uploaded(int seqno)
        {
            Command = new SqlCommand("usp_LOI_get_Uploaded", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userId = new SqlParameter("@userId", SqlDbType.Int);
            prm_userId.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userId);

            if (seqno > 0)
            {
                SqlParameter prm_seqno = new SqlParameter("@seqno", SqlDbType.Int);
                prm_seqno.Value = seqno;
                Command.Parameters.Add(prm_seqno);
            }

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:LOI_get_Uploaded", "usp_LOI_get_Uploaded");
        }
        public DataTable LOI_Detail_by_RequestId(int RequestId)
        {
            Command = new SqlCommand("usp_LOI_Detail_by_RequestId", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
            prm_RequestId.Value = RequestId;
            Command.Parameters.Add(prm_RequestId);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:LOI_Detail_by_RequestId", "usp_LOI_Detail_by_RequestId");
        }

        public DataTable LOI_Detail_ClossingPending_by_RequestId(int RequestId)
        {
            Command = new SqlCommand("usp_LOI_Detail_ClossingPending_by_RequestId", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
            prm_RequestId.Value = RequestId;
            Command.Parameters.Add(prm_RequestId);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:LOI_Detail_ClossingPending_by_RequestId", "usp_LOI_Detail_ClossingPending_by_RequestId");
        }

        public int LOI_get_UploadedCount(int seqno)
        {
            Command = new SqlCommand("usp_LOI_get_UploadedCount", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userId = new SqlParameter("@userId", SqlDbType.Int);
            prm_userId.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userId);

            if (seqno > 0)
            {
                SqlParameter prm_seqno = new SqlParameter("@seqno", SqlDbType.Int);
                prm_seqno.Value = seqno;
                Command.Parameters.Add(prm_seqno);
            }

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            object dt = AdditionalExecuteCommand.ExecuteScalar(Command, "LOIDataAccess:LOI_get_UploadedCount", "usp_LOI_get_UploadedCount");
            int dtInt = 0;
            if (dt != null)
                dtInt = (int)dt;
            return dtInt;
        }

        public bool LOI_Update_Price(DataRow drLOI, int RequestId)
        {
            Command = new SqlCommand("usp_LOI_Update_Price", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_workpackageid = new SqlParameter("@workpackageid", SqlDbType.VarChar, 50);
            prm_workpackageid.Value = drLOI["workpackageid"];
            Command.Parameters.Add(prm_workpackageid);

            SqlParameter prm_Customer_PO = new SqlParameter("@Customer_PO", SqlDbType.VarChar, 100);
            prm_Customer_PO.Value = drLOI["Customer_PO"];
            Command.Parameters.Add(prm_Customer_PO);

            SqlParameter prm_Site_ID = new SqlParameter("@Site_ID", SqlDbType.VarChar, 20);
            prm_Site_ID.Value = drLOI["Site_ID"];
            Command.Parameters.Add(prm_Site_ID);

            SqlParameter prm_ScopeOfWork = new SqlParameter("@ScopeOfWork", SqlDbType.VarChar, 500);
            prm_ScopeOfWork.Value = drLOI["ScopeOfWork"];
            Command.Parameters.Add(prm_ScopeOfWork);

            SqlParameter prm_Subcone_Name = new SqlParameter("@Subcone_Name", SqlDbType.VarChar, 100);
            prm_Subcone_Name.Value = drLOI["Subcone_Name"];
            Command.Parameters.Add(prm_Subcone_Name);

            SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
            prm_RequestId.Value = RequestId;
            Command.Parameters.Add(prm_RequestId);

            SqlParameter prm_Unit_Price = new SqlParameter("@Unit_Price", SqlDbType.VarChar, 10);
            prm_Unit_Price.Value = drLOI["Unit_Price"];
            Command.Parameters.Add(prm_Unit_Price);

            SqlParameter prm_Qtyl = new SqlParameter("@Qty", SqlDbType.Int);
            prm_Qtyl.Value = drLOI["Qty"];
            Command.Parameters.Add(prm_Qtyl);

            SqlParameter prm_Total_Price = new SqlParameter("@Total_Price", SqlDbType.Decimal);
            prm_Total_Price.Value = drLOI["Total_Price"];
            Command.Parameters.Add(prm_Total_Price);

            SqlParameter prm_Currency = new SqlParameter("@Currency", SqlDbType.VarChar, 10);
            prm_Currency.Value = drLOI["Currency"];
            Command.Parameters.Add(prm_Currency);

            SqlParameter prm_lmby = new SqlParameter("@lmby", SqlDbType.Int);
            prm_lmby.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_lmby);

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "LOIDataAccess:LOI_Update_Price", "usp_LOI_Update_Price");
        }

        public bool LOI_Update_CPMDocAvailability(DataRow drLOI, bool isCPMDocAvailable)
        {
            Command = new SqlCommand("usp_LOI_Update_CPMDocAvailability", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_workpackageid = new SqlParameter("@workpackageid", SqlDbType.VarChar, 50);
            prm_workpackageid.Value = drLOI["workpackageid"];
            Command.Parameters.Add(prm_workpackageid);

            SqlParameter prm_Customer_PO = new SqlParameter("@Customer_PO", SqlDbType.VarChar, 100);
            prm_Customer_PO.Value = drLOI["Customer_PO"];
            Command.Parameters.Add(prm_Customer_PO);

            SqlParameter prm_Site_ID = new SqlParameter("@Site_ID", SqlDbType.VarChar, 20);
            prm_Site_ID.Value = drLOI["Site_ID"];
            Command.Parameters.Add(prm_Site_ID);

            SqlParameter prm_ScopeOfWork = new SqlParameter("@ScopeOfWork", SqlDbType.VarChar, 500);
            prm_ScopeOfWork.Value = drLOI["ScopeOfWork"];
            Command.Parameters.Add(prm_ScopeOfWork);

            SqlParameter prm_Subcone_Name = new SqlParameter("@Subcone_Name", SqlDbType.VarChar, 100);
            prm_Subcone_Name.Value = drLOI["Subcone_Name"];
            Command.Parameters.Add(prm_Subcone_Name);

            SqlParameter prm_isCPMDocAvailableg = new SqlParameter("@isCPMDocAvailable", SqlDbType.Bit);
            prm_isCPMDocAvailableg.Value = isCPMDocAvailable;
            Command.Parameters.Add(prm_isCPMDocAvailableg);

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "LOIDataAccess:LOI_Update_CPMDocAvailability", "usp_LOI_Update_CPMDocAvailability");
        }

        public int CheckLOI_Upload_by_FM(DataRow drLOI)
        {
            Command = new SqlCommand("usp_CheckLOI_Upload_by_FM", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_workpackageid = new SqlParameter("@workpackageid", SqlDbType.VarChar, 50);
            prm_workpackageid.Value = drLOI["workpackageid"];
            Command.Parameters.Add(prm_workpackageid);

            SqlParameter prm_Customer_PO = new SqlParameter("@Customer_PO", SqlDbType.VarChar, 100);
            prm_Customer_PO.Value = drLOI["Customer_PO"];
            Command.Parameters.Add(prm_Customer_PO);

            SqlParameter prm_Site_ID = new SqlParameter("@Site_ID", SqlDbType.VarChar, 20);
            prm_Site_ID.Value = drLOI["Site_ID"];
            Command.Parameters.Add(prm_Site_ID);

            SqlParameter prm_ScopeOfWork = new SqlParameter("@ScopeOfWork", SqlDbType.VarChar, 500);
            prm_ScopeOfWork.Value = drLOI["ScopeOfWork"];
            Command.Parameters.Add(prm_ScopeOfWork);

            SqlParameter prm_Subcone_Name = new SqlParameter("@Subcone_Name", SqlDbType.VarChar, 100);
            prm_Subcone_Name.Value = drLOI["Subcone_Name"];
            Command.Parameters.Add(prm_Subcone_Name);

            object dt = AdditionalExecuteCommand.ExecuteScalar(Command, "LOIDataAccess:CheckLOI_Upload_by_FM", "usp_CheckLOI_Upload_by_FM");
            int dtInt = 0;
            if (dt != null)
                dtInt = (int)dt;
            return dtInt;
        }

        public DataTable SubconName_getList()
        {
            Command = new SqlCommand("usp_SubconName_getList", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userid = new SqlParameter("@userid", SqlDbType.Int);
            prm_userid.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userid);

            SqlParameter prm_CTName = new SqlParameter("@CTName", SqlDbType.VarChar, 50);
            prm_CTName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_CTName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:SubconName_getList", "usp_SubconName_getList");
        }

        public int Validate_SubconName(string SCon_Name)
        {
            Command = new SqlCommand("usp_Validate_SubconName", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_SCon_Name = new SqlParameter("@SCon_Name", SqlDbType.VarChar, 150);
            prm_SCon_Name.Value = SCon_Name;
            Command.Parameters.Add(prm_SCon_Name);

            SqlParameter prm_userid = new SqlParameter("@userid", SqlDbType.Int);
            prm_userid.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userid);

            SqlParameter prm_CTName = new SqlParameter("@CTName", SqlDbType.VarChar, 50);
            prm_CTName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_CTName);

            object dt = AdditionalExecuteCommand.ExecuteScalar(Command, "LOIDataAccess:Validate_SubconName", "usp_Validate_SubconName");
            int dtInt = 0;
            if (dt != null)
                dtInt = (int)dt;
            return dtInt;
        }
        public bool LOI_Update_BOQ_Pricing(int RequestId)
        {
            Command = new SqlCommand("usp_LOI_Update_BOQ_Pricing", Connection);
            Command.CommandType = CommandType.StoredProcedure;


            SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
            prm_RequestId.Value = RequestId;
            Command.Parameters.Add(prm_RequestId);

            SqlParameter prm_lmby = new SqlParameter("@lmby", SqlDbType.Int);
            prm_lmby.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_lmby);

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "LOIDataAccess:LOI_Update_BOW_Pricing", "usp_LOI_Update_BOW_Pricing");
        }

        public bool LOI_Update_SupDoc_Availibility(int RequestId)
        {
            Command = new SqlCommand("usp_LOI_Update_SupDoc_Availibility", Connection);
            Command.CommandType = CommandType.StoredProcedure;


            SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
            prm_RequestId.Value = RequestId;
            Command.Parameters.Add(prm_RequestId);

            SqlParameter prm_lmby = new SqlParameter("@lmby", SqlDbType.Int);
            prm_lmby.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_lmby);

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "LOIDataAccess:LOI_Update_SupDoc_Availibility", "usp_LOI_Update_SupDoc_Availibility");
        }

        public bool LOI_Update_Approval_Status(int RequestId, bool isApprove, string ApproverRole, string Reject_Remarks)
        {
            Command = new SqlCommand("usp_LOI_Update_Approval_Status", Connection);
            Command.CommandType = CommandType.StoredProcedure;


            SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
            prm_RequestId.Value = RequestId;
            Command.Parameters.Add(prm_RequestId);

            SqlParameter prm_isApprove = new SqlParameter("@isApprove", SqlDbType.Bit);
            prm_isApprove.Value = isApprove;
            Command.Parameters.Add(prm_isApprove);

            SqlParameter prm_ApproverRole = new SqlParameter("@ApproverRole", SqlDbType.VarChar, 50);
            prm_ApproverRole.Value = ApproverRole;
            Command.Parameters.Add(prm_ApproverRole);

            SqlParameter prm_lmby = new SqlParameter("@lmby", SqlDbType.Int);
            prm_lmby.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_lmby);

            if (!string.IsNullOrEmpty(Reject_Remarks))
            {
                SqlParameter prm_Reject_Remarks = new SqlParameter("@Reject_Remarks", SqlDbType.VarChar);
                prm_Reject_Remarks.Value = Reject_Remarks;
                Command.Parameters.Add(prm_Reject_Remarks);
            }

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "LOIDataAccess:LOI_Update_Approval_Status", "usp_LOI_Update_Approval_Status");
        }

        public DataTable ssp_checking_document_getall(int RequestId)
        {
            Command = new SqlCommand("usp_ssp_checking_document_getall", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
            prm_RequestId.Value = RequestId;
            Command.Parameters.Add(prm_RequestId);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:ssp_checking_document_getall", "usp_ssp_checking_document_getall");
        }

        public bool loi_supdoc_availibility_iu(int supportingdocument_id, int RequestId, bool availibility, string remarks)
        {
            Command = new SqlCommand("usp_loi_supdoc_availibility_iu", Connection);
            Command.CommandType = CommandType.StoredProcedure;


            SqlParameter prm_supportingdocument_id = new SqlParameter("@supportingdocument_id", SqlDbType.Int);
            prm_supportingdocument_id.Value = supportingdocument_id;
            Command.Parameters.Add(prm_supportingdocument_id);

            SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
            prm_RequestId.Value = RequestId;
            Command.Parameters.Add(prm_RequestId);

            SqlParameter prm_availibility = new SqlParameter("@availibility", SqlDbType.Bit);
            prm_availibility.Value = availibility;
            Command.Parameters.Add(prm_availibility);

            if (!string.IsNullOrEmpty(remarks))
            {
                SqlParameter prm_remarks = new SqlParameter("@remarks", SqlDbType.VarChar);
                prm_remarks.Value = remarks;
                Command.Parameters.Add(prm_remarks);
            }

            SqlParameter prm_lmby = new SqlParameter("@lmby", SqlDbType.Int);
            prm_lmby.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_lmby);

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "LOIDataAccess:loi_supdoc_availibility_iu", "usp_loi_supdoc_availibility_iu");
        }

        public bool loi_supdoc_availibility_D(int supportingdocument_id, int RequestId)
        {
            Command = new SqlCommand("usp_loi_supdoc_availibility_D", Connection);
            Command.CommandType = CommandType.StoredProcedure;


            SqlParameter prm_supportingdocument_id = new SqlParameter("@supportingdocument_id", SqlDbType.Int);
            prm_supportingdocument_id.Value = supportingdocument_id;
            Command.Parameters.Add(prm_supportingdocument_id);

            SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
            prm_RequestId.Value = RequestId;
            Command.Parameters.Add(prm_RequestId);

            SqlParameter prm_lmby = new SqlParameter("@lmby", SqlDbType.Int);
            prm_lmby.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_lmby);

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "LOIDataAccess:loi_supdoc_availibility_D", "usp_loi_supdoc_availibility_D");
        }

        public DataTable cpm_checking_document_getall(int RequestId)
        {
            Command = new SqlCommand("usp_cpm_checking_document_getall", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
            prm_RequestId.Value = RequestId;
            Command.Parameters.Add(prm_RequestId);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:cpm_checking_document_getall", "usp_cpm_checking_document_getall");
        }

        public DataTable loi_supportingdoc_getall(int RequestId)
        {
            Command = new SqlCommand("usp_loi_supportingdoc_getall", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
            prm_RequestId.Value = RequestId;
            Command.Parameters.Add(prm_RequestId);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:loi_supportingdoc_getall", "usp_loi_supportingdoc_getall");
        }

        public int Loi_NotAvailable_Price_Count()
        {
            Command = new SqlCommand("usp_Loi_NotAvailable_Price_Count", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userId = new SqlParameter("@userId", SqlDbType.Int);
            prm_userId.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userId);

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            object dt = AdditionalExecuteCommand.ExecuteScalar(Command, "LOIDataAccess:Loi_NotAvailable_Price_Count", "usp_Loi_NotAvailable_Price_Count");
            int dtInt = 0;
            if (dt != null)
                dtInt = (int)dt;
            return dtInt;
        }

        public DataTable Loi_NotAvailable_Price()
        {
            Command = new SqlCommand("usp_Loi_NotAvailable_Price", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userId = new SqlParameter("@userId", SqlDbType.Int);
            prm_userId.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userId);

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:Loi_NotAvailable_Price", "usp_Loi_NotAvailable_Price");
        }

        public int Loi_NotAvailable_SuppDoc_Count()
        {
            Command = new SqlCommand("usp_Loi_NotAvailable_SuppDoc_Count", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userId = new SqlParameter("@userId", SqlDbType.Int);
            prm_userId.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userId);

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            object dt = AdditionalExecuteCommand.ExecuteScalar(Command, "LOIDataAccess:Loi_NotAvailable_SuppDoc_Count", "usp_Loi_NotAvailable_SuppDoc_Count");
            int dtInt = 0;
            if (dt != null)
                dtInt = (int)dt;
            return dtInt;
        }

        public DataTable Loi_NotAvailable_SuppDoc()
        {
            Command = new SqlCommand("usp_Loi_NotAvailable_SuppDoc", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userId = new SqlParameter("@userId", SqlDbType.Int);
            prm_userId.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userId);

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:Loi_NotAvailable_SuppDoc", "usp_Loi_NotAvailable_SuppDoc");
        }

        public DataTable supporting_document_getNotAvailable(int RequestId)
        {
            Command = new SqlCommand("usp_supporting_document_getNotAvailable", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
            prm_RequestId.Value = RequestId;
            Command.Parameters.Add(prm_RequestId);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:supporting_document_getNotAvailable", "usp_supporting_document_getNotAvailable");
        }

        public DataTable report_loi_approvaltracking_getdata(int subconid, string periodfrom, string periodto)
        {
            Command = new SqlCommand("usp_report_loi_approvaltracking_getdata", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userId = new SqlParameter("@userId", SqlDbType.BigInt);
            prm_userId.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userId);

            if (subconid > 0)
            {
                SqlParameter prm_subconid = new SqlParameter("@subconid", SqlDbType.Int);
                prm_subconid.Value = subconid;
                Command.Parameters.Add(prm_subconid);
            }

            if (!string.IsNullOrEmpty(periodfrom))
            {
                SqlParameter prm_periodfrom = new SqlParameter("@periodfrom", SqlDbType.DateTime);
                prm_periodfrom.Value = DateTime.Parse(periodfrom);
                Command.Parameters.Add(prm_periodfrom);
            }

            if (!string.IsNullOrEmpty(periodto))
            {
                SqlParameter prm_periodto = new SqlParameter("@periodto", SqlDbType.DateTime);
                prm_periodto.Value = DateTime.Parse(periodto);
                Command.Parameters.Add(prm_periodto);
            }

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:report_loi_approvaltracking_getdata", "usp_report_loi_approvaltracking_getdata");
        }

        public DataTable report_loi_done_getdata(int subconid, string periodfrom, string periodto)
        {
            Command = new SqlCommand("usp_report_loi_done_getdata", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userId = new SqlParameter("@userId", SqlDbType.Int);
            prm_userId.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userId);

            if (subconid > 0)
            {
                SqlParameter prm_subconid = new SqlParameter("@subconid", SqlDbType.Int);
                prm_subconid.Value = subconid;
                Command.Parameters.Add(prm_subconid);
            }

            if (!string.IsNullOrEmpty(periodfrom))
            {
                SqlParameter prm_periodfrom = new SqlParameter("@periodfrom", SqlDbType.DateTime);
                prm_periodfrom.Value = DateTime.Parse(periodfrom);
                Command.Parameters.Add(prm_periodfrom);
            }

            if (!string.IsNullOrEmpty(periodto))
            {
                SqlParameter prm_periodto = new SqlParameter("@periodto", SqlDbType.DateTime);
                prm_periodto.Value = DateTime.Parse(periodto);
                Command.Parameters.Add(prm_periodto);
            }

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:report_loi_done_getdata", "usp_report_loi_done_getdata");
        }

        public DataTable report_loi_rejection_getdata(int subconid, string periodfrom, string periodto)
        {
            Command = new SqlCommand("usp_report_loi_rejection_getdata", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userId = new SqlParameter("@userId", SqlDbType.Int);
            prm_userId.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userId);

            if (subconid > 0)
            {
                SqlParameter prm_subconid = new SqlParameter("@subconid", SqlDbType.Int);
                prm_subconid.Value = subconid;
                Command.Parameters.Add(prm_subconid);
            }

            if (!string.IsNullOrEmpty(periodfrom))
            {
                SqlParameter prm_periodfrom = new SqlParameter("@periodfrom", SqlDbType.DateTime);
                prm_periodfrom.Value = DateTime.Parse(periodfrom);
                Command.Parameters.Add(prm_periodfrom);
            }

            if (!string.IsNullOrEmpty(periodto))
            {
                SqlParameter prm_periodto = new SqlParameter("@periodto", SqlDbType.DateTime);
                prm_periodto.Value = DateTime.Parse(periodto);
                Command.Parameters.Add(prm_periodto);
            }

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:report_loi_rejection_getdata", "usp_report_loi_rejection_getdata");
        }

        public DataTable report_loi_open_getdata(int subconid, string periodfrom, string periodto)
        {
            Command = new SqlCommand("usp_report_loi_open_getdata", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userId = new SqlParameter("@userId", SqlDbType.Int);
            prm_userId.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userId);

            if (subconid > 0)
            {
                SqlParameter prm_subconid = new SqlParameter("@subconid", SqlDbType.Int);
                prm_subconid.Value = subconid;
                Command.Parameters.Add(prm_subconid);
            }

            if (!string.IsNullOrEmpty(periodfrom))
            {
                SqlParameter prm_periodfrom = new SqlParameter("@periodfrom", SqlDbType.DateTime);
                prm_periodfrom.Value = DateTime.Parse(periodfrom);
                Command.Parameters.Add(prm_periodfrom);
            }

            if (!string.IsNullOrEmpty(periodto))
            {
                SqlParameter prm_periodto = new SqlParameter("@periodto", SqlDbType.DateTime);
                prm_periodto.Value = DateTime.Parse(periodto);
                Command.Parameters.Add(prm_periodto);
            }

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:report_loi_open_getdata", "usp_report_loi_open_getdata");
        }
        public bool change_loigroup_by_maxtotalprice(int RequestId)
        {
            Command = new SqlCommand("usp_change_loigroup_by_maxtotalprice", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
            prm_RequestId.Value = RequestId;
            Command.Parameters.Add(prm_RequestId);

            SqlParameter prm_lmby = new SqlParameter("@lmby", SqlDbType.Int);
            prm_lmby.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_lmby);

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "LOIDataAccess:change_loigroup_by_maxtotalprice", "usp_change_loigroup_by_maxtotalprice");
        }

        public bool LOI_Update_status_uploadPrice(int RequestId, DateTime Closing_Plan_Date)
        {
            Command = new SqlCommand("usp_LOI_Update_status_uploadPrice", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
            prm_RequestId.Value = RequestId;
            Command.Parameters.Add(prm_RequestId);

            SqlParameter prm_Closing_Plan_Date = new SqlParameter("@Closing_Plan_Date", SqlDbType.DateTime);
            prm_Closing_Plan_Date.Value = Closing_Plan_Date;
            Command.Parameters.Add(prm_Closing_Plan_Date);

            SqlParameter prm_lmby = new SqlParameter("@lmby", SqlDbType.Int);
            prm_lmby.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_lmby);

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "LOIDataAccess:LOI_Update_status_uploadPrice", "usp_LOI_Update_status_uploadPrice");
        }

        public DataTable LOI_Cancellation_getList()
        {
            Command = new SqlCommand("usp_LOI_Cancellation_getList", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userId = new SqlParameter("@userId", SqlDbType.Int);
            prm_userId.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userId);

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:LOI_Cancellation_getList", "usp_LOI_Cancellation_getList");
        }

        public int LOI_get_UploadRejectedCount(int seqno)
        {
            try
            {
                Command = new SqlCommand("usp_LOI_get_UploadRejectedCount", Connection);
                Command.CommandType = CommandType.StoredProcedure;

                SqlParameter prm_userId = new SqlParameter("@userId", SqlDbType.Int);
                prm_userId.Value = ContentSession.USERID;
                Command.Parameters.Add(prm_userId);

                if (seqno > 0)
                {
                    SqlParameter prm_seqno = new SqlParameter("@seqno", SqlDbType.Int);
                    prm_seqno.Value = seqno;
                    Command.Parameters.Add(prm_seqno);
                }

                SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
                prm_ProjectName.Value = ContentSession.CTName;
                Command.Parameters.Add(prm_ProjectName);

                object dt = AdditionalExecuteCommand.ExecuteScalar(Command, "LOIDataAccess:LOI_get_UploadRejectedCount", "usp_LOI_get_UploadRejectedCount");
                int dtInt = 0;
                if (dt != null)
                    dtInt = (int)dt;
                return dtInt;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public DataTable LOI_get_UploadRejected(int seqno)
        {
            Command = new SqlCommand("usp_LOI_get_UploadedRejected", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userId = new SqlParameter("@userId", SqlDbType.Int);
            prm_userId.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userId);

            if (seqno > 0)
            {
                SqlParameter prm_seqno = new SqlParameter("@seqno", SqlDbType.Int);
                prm_seqno.Value = seqno;
                Command.Parameters.Add(prm_seqno);
            }

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:LOI_get_UploadRejected", "usp_LOI_get_UploadedRejected");
        }

        public bool LOI_UploadRejected_by_FM(int RequestId)
        {
            Command = new SqlCommand("usp_LOI_UploadRejected_by_FM", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_RequestId = new SqlParameter("@ReqId", SqlDbType.BigInt);
            prm_RequestId.Value = RequestId;
            Command.Parameters.Add(prm_RequestId);

            SqlParameter prm_lmby = new SqlParameter("@lmby", SqlDbType.Int);
            prm_lmby.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_lmby);

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "LOIDataAccess:LOI_UploadRejected_by_FM", "usp_LOI_UploadRejected_by_FM");
        }

        public DataTable LOI_Overdue()
        {
            Command = new SqlCommand("usp_LOI_Overdue", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userId = new SqlParameter("@userId", SqlDbType.Int);
            prm_userId.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userId);

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:LOI_Overdue", "usp_LOI_Overdue");
        }

        public DataTable master_sow_getList_detail()
        {
            Command = new SqlCommand("usp_master_sow_getList_detail", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userId = new SqlParameter("@userId", SqlDbType.Int);
            prm_userId.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userId);

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:master_sow_getList_detail", "usp_master_sow_getList_detail");
        }

        public int Validate_sowdetail(string detail_sow)
        {
            Command = new SqlCommand("usp_Validate_sowdetail", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_detail_sow = new SqlParameter("@detail_sow", SqlDbType.VarChar, 200);
            prm_detail_sow.Value = detail_sow;
            Command.Parameters.Add(prm_detail_sow);

            object dt = AdditionalExecuteCommand.ExecuteScalar(Command, "LOIDataAccess:Validate_sowdetail", "usp_Validate_sowdetail");
            int dtResult = 0;
            if (dt != null)
                dtResult = (int)dt;
            return dtResult;
        }

        public bool LOIDetail_Checking_duplicate_sow_siteid_byReqno(int RequestId, string Site_ID, string ScopeOfWork)
        {
            Command = new SqlCommand("usp_LOIDetail_Checking_duplicate_sow_siteid_byReqno", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
            prm_RequestId.Value = RequestId;
            Command.Parameters.Add(prm_RequestId);

            SqlParameter prm_Site_ID = new SqlParameter("@Site_ID", SqlDbType.VarChar, 20);
            prm_Site_ID.Value = Site_ID;
            Command.Parameters.Add(prm_Site_ID);

            SqlParameter prm_ScopeOfWork = new SqlParameter("@ScopeOfWork", SqlDbType.VarChar, 500);
            prm_ScopeOfWork.Value = ScopeOfWork;
            Command.Parameters.Add(prm_ScopeOfWork);

            object dt = AdditionalExecuteCommand.ExecuteScalar(Command, "LOIDataAccess:LOIDetail_Checking_duplicate_sow_siteid_byReqno", "usp_LOIDetail_Checking_duplicate_sow_siteid_byReqno");
            bool dtResult = false;
            if (dt != null)
                dtResult = (bool)dt;
            return dtResult;
        }

        public DataTable boq_price_getNotAvailable(int RequestId)
        {
            Command = new SqlCommand("usp_boq_price_getNotAvailable", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
            prm_RequestId.Value = RequestId;
            Command.Parameters.Add(prm_RequestId);

            SqlParameter prm_userId = new SqlParameter("@userid", SqlDbType.Int);
            prm_userId.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userId);

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:boq_price_getNotAvailable", "usp_boq_price_getNotAvailable");
        }

        public string LOIDetail_validate(string workpackageid, int Subcon_Id, string scopeofwork, string SCon_Name)
        {
            Command = new SqlCommand("usp_LOIDetail_validate", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_workpackageid = new SqlParameter("@workpackageid", SqlDbType.VarChar, 50);
            prm_workpackageid.Value = workpackageid;
            Command.Parameters.Add(prm_workpackageid);

            SqlParameter prm_Subcon_Id = new SqlParameter("@Subcon_Id", SqlDbType.Int);
            prm_Subcon_Id.Value = Subcon_Id;
            Command.Parameters.Add(prm_Subcon_Id);

            SqlParameter prm_detail_sow = new SqlParameter("@detail_sow", SqlDbType.VarChar, 500);
            prm_detail_sow.Value = scopeofwork;
            Command.Parameters.Add(prm_detail_sow);

            SqlParameter prm_SCon_Name = new SqlParameter("@SCon_Name", SqlDbType.VarChar, 150);
            prm_SCon_Name.Value = SCon_Name;
            Command.Parameters.Add(prm_SCon_Name);

            SqlParameter prm_CTName = new SqlParameter("@CTName", SqlDbType.VarChar, 50);
            prm_CTName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_CTName);

            object dt = AdditionalExecuteCommand.ExecuteScalar(Command, "LOIDataAccess:LOIDetail_validate", "usp_LOIDetail_validate");
            string dtStr = "Error";
            if (dt != null)
                dtStr = dt.ToString();
            return dtStr;
        }

        public DataTable LOI_get_Uploaded_byStatus(string LOI_Status)
        {
            Command = new SqlCommand("usp_LOI_get_Uploaded_byStatus", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userId = new SqlParameter("@userId", SqlDbType.Int);
            prm_userId.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userId);

            SqlParameter prm_LOI_Status = new SqlParameter("@LOI_Status", SqlDbType.VarChar, 50);
            prm_LOI_Status.Value = LOI_Status;
            Command.Parameters.Add(prm_LOI_Status);

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:LOI_get_Uploaded_byStatus", "usp_LOI_get_Uploaded_byStatus");
        }

        public int LOI_get_UploadedCount_byStatus(string LOI_Status)
        {
            Command = new SqlCommand("usp_LOI_get_UploadedCount_byStatus", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userId = new SqlParameter("@userId", SqlDbType.Int);
            prm_userId.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userId);

            SqlParameter prm_LOI_Status = new SqlParameter("@LOI_Status", SqlDbType.VarChar, 50);
            prm_LOI_Status.Value = LOI_Status;
            Command.Parameters.Add(prm_LOI_Status);

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            object dt = AdditionalExecuteCommand.ExecuteScalar(Command, "LOIDataAccess:LOI_get_UploadedCount_byStatus", "usp_LOI_get_UploadedCount_byStatus");
            int dtInt = 0;
            if (dt != null)
                dtInt = (int)dt;
            return dtInt;
        }

        public DataTable loi_get_email_data(string loi_status, int requestid)
        {
            Command = new SqlCommand("usp_loi_get_email_data", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_loi_status = new SqlParameter("@LOI_Status", SqlDbType.VarChar, 50);
            prm_loi_status.Value = loi_status;
            Command.Parameters.Add(prm_loi_status);

            SqlParameter prm_requestid = new SqlParameter("@requestid", SqlDbType.BigInt);
            prm_requestid.Value = requestid;
            Command.Parameters.Add(prm_requestid);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:atp_endorse_get_email_data", "usp_atp_endorse_get_email_data");
        }

        public DataTable loi_get_receivermail_workflow(int RequestId)
        {
            Command = new SqlCommand("usp_loi_get_receivermail_workflow", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
            prm_RequestId.Value = RequestId;
            Command.Parameters.Add(prm_RequestId);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:loi_get_receivermail_workflow", "usp_loi_get_receivermail_workflow");
        }

        public DataTable LOI_Detail_getEmailData(int RequestId)
        {
            Command = new SqlCommand("usp_LOI_Detail_getEmailData", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
            prm_RequestId.Value = RequestId;
            Command.Parameters.Add(prm_RequestId);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:LOI_Detail_getEmailData", "usp_LOI_Detail_getEmailData");
        }

        public DataTable loi_get_receivermail_rejected(int RequestId)
        {
            Command = new SqlCommand("usp_loi_get_receivermail_rejected", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
            prm_RequestId.Value = RequestId;
            Command.Parameters.Add(prm_RequestId);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:loi_get_receivermail_rejected", "usp_loi_get_receivermail_rejected");
        }

        public bool create_loi_refference(int RequestId)
        {
            Command = new SqlCommand("usp_create_loi_refference", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
            prm_RequestId.Value = RequestId;
            Command.Parameters.Add(prm_RequestId);

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "LOIDataAccess:create_loi_refference", "usp_create_loi_refference");
        }

        public DataTable getTasklistSummary_Report(int subconid, string DashboardType, string ReqPeriodfrom, string ReqPeriodto, string ApprovePeriodfrom, string ApprovePeriodto)
        {
            Command = new SqlCommand("usp_getTasklistSummary_Report", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userId = new SqlParameter("@userid", SqlDbType.Int);
            prm_userId.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userId);

            if (subconid > 0)
            {
                SqlParameter prm_subconid = new SqlParameter("@subconid", SqlDbType.BigInt);
                prm_subconid.Value = subconid;
                Command.Parameters.Add(prm_subconid);
            }

            if (!string.IsNullOrEmpty(DashboardType))
            {
                SqlParameter prm_DashboardType = new SqlParameter("@DashboardType", SqlDbType.VarChar, 50);
                prm_DashboardType.Value = DashboardType;
                Command.Parameters.Add(prm_DashboardType);
            }

            if (!string.IsNullOrEmpty(ReqPeriodfrom))
            {
                SqlParameter prm_ReqPeriodfrom = new SqlParameter("@requestperiodfrom", SqlDbType.DateTime);
                prm_ReqPeriodfrom.Value = DateTime.Parse(ReqPeriodfrom);
                Command.Parameters.Add(prm_ReqPeriodfrom);
            }

            if (!string.IsNullOrEmpty(ReqPeriodto))
            {
                SqlParameter prm_ReqPeriodto = new SqlParameter("@requestperiodto", SqlDbType.DateTime);
                prm_ReqPeriodto.Value = DateTime.Parse(ReqPeriodto);
                Command.Parameters.Add(prm_ReqPeriodto);
            }

            if (!string.IsNullOrEmpty(ApprovePeriodfrom))
            {
                SqlParameter prm_ApprovePeriodfrom = new SqlParameter("@Approveperiodfrom", SqlDbType.DateTime);
                prm_ApprovePeriodfrom.Value = DateTime.Parse(ApprovePeriodfrom);
                Command.Parameters.Add(prm_ApprovePeriodfrom);
            }

            if (!string.IsNullOrEmpty(ApprovePeriodto))
            {
                SqlParameter prm_ApprovePeriodto = new SqlParameter("@Approveperiodto", SqlDbType.DateTime);
                prm_ApprovePeriodto.Value = DateTime.Parse(ApprovePeriodto);
                Command.Parameters.Add(prm_ApprovePeriodto);
            }

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:getTasklistSummary_Report", "usp_getTasklistSummary_Report");
        }

        public DataTable report_loi_done_detail_getdata(int subconid, string periodfrom, string periodto, string siteid, string cpo, int reqid)
        {
            Command = new SqlCommand("usp_report_loi_done_detail_getdata", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userId = new SqlParameter("@userId", SqlDbType.Int);
            prm_userId.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userId);

            if (subconid > 0)
            {
                SqlParameter prm_subconid = new SqlParameter("@subconid", SqlDbType.Int);
                prm_subconid.Value = subconid;
                Command.Parameters.Add(prm_subconid);
            }

            if (!string.IsNullOrEmpty(periodfrom))
            {
                SqlParameter prm_periodfrom = new SqlParameter("@periodfrom", SqlDbType.DateTime);
                prm_periodfrom.Value = DateTime.Parse(periodfrom);
                Command.Parameters.Add(prm_periodfrom);
            }

            if (!string.IsNullOrEmpty(periodto))
            {
                SqlParameter prm_periodto = new SqlParameter("@periodto", SqlDbType.DateTime);
                prm_periodto.Value = DateTime.Parse(periodto);
                Command.Parameters.Add(prm_periodto);
            }

            if (!string.IsNullOrEmpty(siteid))
            {
                SqlParameter prm_siteid = new SqlParameter("@Site_ID", SqlDbType.VarChar, 20);
                prm_siteid.Value = siteid;
                Command.Parameters.Add(prm_siteid);
            }

            if (!string.IsNullOrEmpty(cpo))
            {
                SqlParameter prm_cpo = new SqlParameter("@Customer_PO", SqlDbType.VarChar, 100);
                prm_cpo.Value = cpo;
                Command.Parameters.Add(prm_cpo);
            }

            if (reqid > 0)
            {
                SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
                prm_RequestId.Value = reqid;
                Command.Parameters.Add(prm_RequestId);
            }

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:report_loi_done_detail_getdata", "usp_report_loi_done_detail_getdata");
        }

        public DataTable report_loi_approvaltracking_detail_getdata(int subconid, string periodfrom, string periodto, string siteid, string cpo, int reqid)
        {
            Command = new SqlCommand("usp_report_loi_approvaltracking_detail_getdata", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userId = new SqlParameter("@userId", SqlDbType.BigInt);
            prm_userId.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userId);

            if (subconid > 0)
            {
                SqlParameter prm_subconid = new SqlParameter("@subconid", SqlDbType.Int);
                prm_subconid.Value = subconid;
                Command.Parameters.Add(prm_subconid);
            }

            if (!string.IsNullOrEmpty(periodfrom))
            {
                SqlParameter prm_periodfrom = new SqlParameter("@periodfrom", SqlDbType.DateTime);
                prm_periodfrom.Value = DateTime.Parse(periodfrom);
                Command.Parameters.Add(prm_periodfrom);
            }

            if (!string.IsNullOrEmpty(periodto))
            {
                SqlParameter prm_periodto = new SqlParameter("@periodto", SqlDbType.DateTime);
                prm_periodto.Value = DateTime.Parse(periodto);
                Command.Parameters.Add(prm_periodto);
            }

            if (!string.IsNullOrEmpty(siteid))
            {
                SqlParameter prm_siteid = new SqlParameter("@Site_ID", SqlDbType.VarChar, 20);
                prm_siteid.Value = siteid;
                Command.Parameters.Add(prm_siteid);
            }

            if (!string.IsNullOrEmpty(cpo))
            {
                SqlParameter prm_cpo = new SqlParameter("@Customer_PO", SqlDbType.VarChar, 100);
                prm_cpo.Value = cpo;
                Command.Parameters.Add(prm_cpo);
            }

            if (reqid > 0)
            {
                SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
                prm_RequestId.Value = reqid;
                Command.Parameters.Add(prm_RequestId);
            }

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:report_loi_approvaltracking_detail_getdata", "usp_report_loi_approvaltracking_detail_getdata");
        }

        public DataTable report_loi_rejection_detail_getdata(int subconid, string periodfrom, string periodto, string siteid, string cpo, int reqid)
        {
            Command = new SqlCommand("usp_report_loi_rejection_detail_getdata", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userId = new SqlParameter("@userId", SqlDbType.Int);
            prm_userId.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userId);

            if (subconid > 0)
            {
                SqlParameter prm_subconid = new SqlParameter("@subconid", SqlDbType.Int);
                prm_subconid.Value = subconid;
                Command.Parameters.Add(prm_subconid);
            }

            if (!string.IsNullOrEmpty(periodfrom))
            {
                SqlParameter prm_periodfrom = new SqlParameter("@periodfrom", SqlDbType.DateTime);
                prm_periodfrom.Value = DateTime.Parse(periodfrom);
                Command.Parameters.Add(prm_periodfrom);
            }

            if (!string.IsNullOrEmpty(periodto))
            {
                SqlParameter prm_periodto = new SqlParameter("@periodto", SqlDbType.DateTime);
                prm_periodto.Value = DateTime.Parse(periodto);
                Command.Parameters.Add(prm_periodto);
            }

            if (!string.IsNullOrEmpty(siteid))
            {
                SqlParameter prm_siteid = new SqlParameter("@Site_ID", SqlDbType.VarChar, 20);
                prm_siteid.Value = siteid;
                Command.Parameters.Add(prm_siteid);
            }

            if (!string.IsNullOrEmpty(cpo))
            {
                SqlParameter prm_cpo = new SqlParameter("@Customer_PO", SqlDbType.VarChar, 100);
                prm_cpo.Value = cpo;
                Command.Parameters.Add(prm_cpo);
            }

            if (reqid > 0)
            {
                SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
                prm_RequestId.Value = reqid;
                Command.Parameters.Add(prm_RequestId);
            }

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:report_loi_rejection_detail_getdata", "usp_report_loi_rejection_detail_getdata");
        }

        public DataTable subcon_get_all_byctName()
        {
            Command = new SqlCommand("usp_subcon_get_all_byctName", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userId = new SqlParameter("@userid", SqlDbType.Int);
            prm_userId.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userId);

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:subcon_get_all_byctName", "usp_subcon_get_all_byctName");
        }

        public DataTable getmonth()
        {
            Command = new SqlCommand("usp_getmonth", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:getmonth", "usp_getmonth");
        }

        public DataTable report_loi_overdue_getdata(int subconid, string periodfrom, string periodto)
        {
            Command = new SqlCommand("usp_report_loi_overdue_getdata", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userId = new SqlParameter("@userId", SqlDbType.Int);
            prm_userId.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userId);

            if (subconid > 0)
            {
                SqlParameter prm_subconid = new SqlParameter("@subconid", SqlDbType.Int);
                prm_subconid.Value = subconid;
                Command.Parameters.Add(prm_subconid);
            }

            if (!string.IsNullOrEmpty(periodfrom))
            {
                SqlParameter prm_periodfrom = new SqlParameter("@periodfrom", SqlDbType.DateTime);
                prm_periodfrom.Value = DateTime.Parse(periodfrom);
                Command.Parameters.Add(prm_periodfrom);
            }

            if (!string.IsNullOrEmpty(periodto))
            {
                SqlParameter prm_periodto = new SqlParameter("@periodto", SqlDbType.DateTime);
                prm_periodto.Value = DateTime.Parse(periodto);
                Command.Parameters.Add(prm_periodto);
            }

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:report_loi_overdue_getdata", "usp_report_loi_overdue_getdata");
        }

        public DataTable report_loi_overdue_detail_getdata(int subconid, string periodfrom, string periodto, string siteid, string cpo, int reqid)
        {
            Command = new SqlCommand("usp_report_loi_overdue_detail_getdata", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userId = new SqlParameter("@userId", SqlDbType.Int);
            prm_userId.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userId);

            if (subconid > 0)
            {
                SqlParameter prm_subconid = new SqlParameter("@subconid", SqlDbType.Int);
                prm_subconid.Value = subconid;
                Command.Parameters.Add(prm_subconid);
            }

            if (!string.IsNullOrEmpty(periodfrom))
            {
                SqlParameter prm_periodfrom = new SqlParameter("@periodfrom", SqlDbType.DateTime);
                prm_periodfrom.Value = DateTime.Parse(periodfrom);
                Command.Parameters.Add(prm_periodfrom);
            }

            if (!string.IsNullOrEmpty(periodto))
            {
                SqlParameter prm_periodto = new SqlParameter("@periodto", SqlDbType.DateTime);
                prm_periodto.Value = DateTime.Parse(periodto);
                Command.Parameters.Add(prm_periodto);
            }

            if (!string.IsNullOrEmpty(siteid))
            {
                SqlParameter prm_siteid = new SqlParameter("@Site_ID", SqlDbType.VarChar, 20);
                prm_siteid.Value = siteid;
                Command.Parameters.Add(prm_siteid);
            }

            if (!string.IsNullOrEmpty(cpo))
            {
                SqlParameter prm_cpo = new SqlParameter("@Customer_PO", SqlDbType.VarChar, 100);
                prm_cpo.Value = cpo;
                Command.Parameters.Add(prm_cpo);
            }

            if (reqid > 0)
            {
                SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
                prm_RequestId.Value = reqid;
                Command.Parameters.Add(prm_RequestId);
            }

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:report_loi_overdue_detail_getdata", "usp_report_loi_overdue_detail_getdata");
        }

        public DataTable report_loi_open_detail_getdata(int subconid, string periodfrom, string periodto, string siteid, string cpo, int reqid)
        {
            Command = new SqlCommand("usp_report_loi_open_detail_getdata", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userId = new SqlParameter("@userId", SqlDbType.Int);
            prm_userId.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userId);

            if (subconid > 0)
            {
                SqlParameter prm_subconid = new SqlParameter("@subconid", SqlDbType.Int);
                prm_subconid.Value = subconid;
                Command.Parameters.Add(prm_subconid);
            }

            if (!string.IsNullOrEmpty(periodfrom))
            {
                SqlParameter prm_periodfrom = new SqlParameter("@periodfrom", SqlDbType.DateTime);
                prm_periodfrom.Value = DateTime.Parse(periodfrom);
                Command.Parameters.Add(prm_periodfrom);
            }

            if (!string.IsNullOrEmpty(periodto))
            {
                SqlParameter prm_periodto = new SqlParameter("@periodto", SqlDbType.DateTime);
                prm_periodto.Value = DateTime.Parse(periodto);
                Command.Parameters.Add(prm_periodto);
            }

            if (!string.IsNullOrEmpty(siteid))
            {
                SqlParameter prm_siteid = new SqlParameter("@Site_ID", SqlDbType.VarChar, 20);
                prm_siteid.Value = siteid;
                Command.Parameters.Add(prm_siteid);
            }

            if (!string.IsNullOrEmpty(cpo))
            {
                SqlParameter prm_cpo = new SqlParameter("@Customer_PO", SqlDbType.VarChar, 100);
                prm_cpo.Value = cpo;
                Command.Parameters.Add(prm_cpo);
            }

            if (reqid > 0)
            {
                SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
                prm_RequestId.Value = reqid;
                Command.Parameters.Add(prm_RequestId);
            }

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:report_loi_open_detail_getdata", "usp_report_loi_open_detail_getdata");
        }

        public bool update_loi_price_fromTemp(int RequestId)
        {
            Command = new SqlCommand("usp_update_loi_price_fromTemp", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
            prm_RequestId.Value = RequestId;
            Command.Parameters.Add(prm_RequestId);

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "LOIDataAccess:update_loi_price_fromTemp", "usp_update_loi_price_fromTemp");
        }

        public DataTable get_region_by_userid()
        {
            Command = new SqlCommand("usp_get_region_by_userid", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userId = new SqlParameter("@userid", SqlDbType.Int);
            prm_userId.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userId);

            SqlParameter prm_CTName = new SqlParameter("@CTName", SqlDbType.VarChar, 50);
            prm_CTName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_CTName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:get_region_by_userid", "usp_get_region_by_userid");
        }

        public DataTable Validate_Region(string RGNName)
        {
            Command = new SqlCommand("usp_Validate_Region", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_RGNName = new SqlParameter("@RGNName", SqlDbType.VarChar, 30);
            prm_RGNName.Value = RGNName;
            Command.Parameters.Add(prm_RGNName);

            SqlParameter prm_CTName = new SqlParameter("@CTName", SqlDbType.VarChar, 50);
            prm_CTName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_CTName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:Validate_Region", "usp_Validate_Region");
        }

        public DataTable report_loi_getdata(int subconid, string periodfrom, string periodto, string reporttype)
        {
            Command = new SqlCommand("usp_report_loi_getdata", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userId = new SqlParameter("@userId", SqlDbType.Int);
            prm_userId.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userId);

            if (subconid > 0)
            {
                SqlParameter prm_subconid = new SqlParameter("@subconid", SqlDbType.Int);
                prm_subconid.Value = subconid;
                Command.Parameters.Add(prm_subconid);
            }

            if (!string.IsNullOrEmpty(periodfrom))
            {
                SqlParameter prm_periodfrom = new SqlParameter("@periodfrom", SqlDbType.DateTime);
                prm_periodfrom.Value = DateTime.Parse(periodfrom);
                Command.Parameters.Add(prm_periodfrom);
            }

            if (!string.IsNullOrEmpty(periodto))
            {
                SqlParameter prm_periodto = new SqlParameter("@periodto", SqlDbType.DateTime);
                prm_periodto.Value = DateTime.Parse(periodto);
                Command.Parameters.Add(prm_periodto);
            }

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            SqlParameter prm_ReportType = new SqlParameter("@ReportType", SqlDbType.VarChar, -1);
            prm_ReportType.Value = reporttype;
            Command.Parameters.Add(prm_ReportType);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:report_loi_getdata", "usp_report_loi_getdata");
        }

        public DataTable report_loi_detail_getdata(string siteid, string cpo, int reqid, string ReportType)
        {
            Command = new SqlCommand("usp_report_loi_detail_getdata", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userId = new SqlParameter("@userId", SqlDbType.Int);
            prm_userId.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userId);            

            if (!string.IsNullOrEmpty(siteid))
            {
                SqlParameter prm_siteid = new SqlParameter("@Site_ID", SqlDbType.VarChar, 20);
                prm_siteid.Value = siteid;
                Command.Parameters.Add(prm_siteid);
            }

            if (!string.IsNullOrEmpty(cpo))
            {
                SqlParameter prm_cpo = new SqlParameter("@Customer_PO", SqlDbType.VarChar, 100);
                prm_cpo.Value = cpo;
                Command.Parameters.Add(prm_cpo);
            }

            if (reqid > 0)
            {
                SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
                prm_RequestId.Value = reqid;
                Command.Parameters.Add(prm_RequestId);
            }

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            SqlParameter prm_ReportType = new SqlParameter("@ReportType", SqlDbType.VarChar, 50);
            prm_ReportType.Value = ReportType;
            Command.Parameters.Add(prm_ReportType);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:report_loi_detail_getdata", "usp_report_loi_detail_getdata");
        }

        public DataTable report_loi_cancelled_detail_getdata(int subconid, string periodfrom, string periodto, string siteid, string cpo, int reqid)
        {
            Command = new SqlCommand("usp_report_loi_cancelled_detail_getdata", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userId = new SqlParameter("@userId", SqlDbType.Int);
            prm_userId.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userId);

            if (subconid > 0)
            {
                SqlParameter prm_subconid = new SqlParameter("@subconid", SqlDbType.Int);
                prm_subconid.Value = subconid;
                Command.Parameters.Add(prm_subconid);
            }

            if (!string.IsNullOrEmpty(periodfrom))
            {
                SqlParameter prm_periodfrom = new SqlParameter("@periodfrom", SqlDbType.DateTime);
                prm_periodfrom.Value = DateTime.Parse(periodfrom);
                Command.Parameters.Add(prm_periodfrom);
            }

            if (!string.IsNullOrEmpty(periodto))
            {
                SqlParameter prm_periodto = new SqlParameter("@periodto", SqlDbType.DateTime);
                prm_periodto.Value = DateTime.Parse(periodto);
                Command.Parameters.Add(prm_periodto);
            }

            if (!string.IsNullOrEmpty(siteid))
            {
                SqlParameter prm_siteid = new SqlParameter("@Site_ID", SqlDbType.VarChar, 20);
                prm_siteid.Value = siteid;
                Command.Parameters.Add(prm_siteid);
            }

            if (!string.IsNullOrEmpty(cpo))
            {
                SqlParameter prm_cpo = new SqlParameter("@Customer_PO", SqlDbType.VarChar, 100);
                prm_cpo.Value = cpo;
                Command.Parameters.Add(prm_cpo);
            }

            if (reqid > 0)
            {
                SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
                prm_RequestId.Value = reqid;
                Command.Parameters.Add(prm_RequestId);
            }

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:report_loi_cancelled_detail_getdata", "usp_report_loi_cancelled_detail_getdata");
        }

        public DataTable getloilog_byrequestid(int requestid)
        {
            Command = new SqlCommand("usp_getloilog_byrequestid", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_requestid = new SqlParameter("@RequestId", SqlDbType.BigInt);
            prm_requestid.Value = requestid;
            Command.Parameters.Add(prm_requestid);                       

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:getloilog_byrequestid", "usp_getloilog_byrequestid");
        }

        public DataTable getloihistory_byusrid()
        {
            Command = new SqlCommand("usp_getloihistory_byusrid", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            SqlParameter prm_userid = new SqlParameter("@userid", SqlDbType.Int);
            prm_userid.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userid);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:getloihistory_byusrid", "usp_getloihistory_byusrid");
        }

        public decimal get_max_totalprice()
        {
            Command = new SqlCommand("usp_get_max_totalprice", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            object dt = AdditionalExecuteCommand.ExecuteScalar(Command, "LOIDataAccess:get_max_totalprice", "usp_get_max_totalprice");
            decimal dtInt = 0;
            if (dt != null)
                dtInt = Convert.ToDecimal(dt);
            return dtInt;
            //return CustomExecuteCommand.exec(Command, Connection, "LOIDataAccess:getloihistory_byusrid", "usp_getloihistory_byusrid")
        }

        public DataTable project_agreement_getbyprojectname()
        {
            Command = new SqlCommand("usp_project_agreement_getbyprojectname", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:project_agreement_getbyprojectname", "usp_project_agreement_getbyprojectname");
        }

        public bool project_agreement_iud(string project_agreement_no, DateTime valid_from, DateTime valid_to, string project_name, int project_agreement_id, bool isDelete)
        {
            Command = new SqlCommand("usp_project_agreement_iud", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_project_agreement_no = new SqlParameter("@project_agreement_no", SqlDbType.VarChar, 300);
            prm_project_agreement_no.Value = project_agreement_no;
            Command.Parameters.Add(prm_project_agreement_no);

            SqlParameter prm_valid_from = new SqlParameter("@valid_from", SqlDbType.DateTime);
            prm_valid_from.Value = valid_from;
            Command.Parameters.Add(prm_valid_from);

            SqlParameter prm_valid_to = new SqlParameter("@valid_to", SqlDbType.DateTime);
            prm_valid_to.Value = valid_to;
            Command.Parameters.Add(prm_valid_to);

            SqlParameter prm_userid = new SqlParameter("@userid", SqlDbType.Int);
            prm_userid.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userid);

            SqlParameter prm_project_name = new SqlParameter("@project_name", SqlDbType.VarChar, 50);
            prm_project_name.Value = project_name;
            Command.Parameters.Add(prm_project_name);

            if(project_agreement_id > 0)
            {
                SqlParameter prm_project_agreement_id = new SqlParameter("@project_agreement_id", SqlDbType.BigInt);
                prm_project_agreement_id.Value = project_agreement_id;
                Command.Parameters.Add(prm_project_agreement_id);
            }
            
            if(isDelete == true)
            {
                SqlParameter prm_isDelete = new SqlParameter("@isDelete", SqlDbType.Bit);
                prm_isDelete.Value = isDelete;
                Command.Parameters.Add(prm_isDelete);
            }            

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "LOIDataAccess:project_agreement_iud", "usp_project_agreement_iud");
        }

        public DataTable price_agreement_getbyprojectname()
        {
            Command = new SqlCommand("usp_price_agreement_getbyprojectname", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:price_agreement_getbyprojectname", "usp_price_agreement_getbyprojectname");
        }

        public bool price_agreement_iud(string price_agreement_doc, DateTime valid_from, DateTime valid_to, string project_name, int project_agreement_id, bool isDelete)
        {
            Command = new SqlCommand("usp_price_agreement_iud", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_price_agreement_doc = new SqlParameter("@price_agreement_doc", SqlDbType.VarChar, 300);
            prm_price_agreement_doc.Value = price_agreement_doc;
            Command.Parameters.Add(prm_price_agreement_doc);

            SqlParameter prm_valid_from = new SqlParameter("@valid_from", SqlDbType.DateTime);
            prm_valid_from.Value = valid_from;
            Command.Parameters.Add(prm_valid_from);

            SqlParameter prm_valid_to = new SqlParameter("@valid_to", SqlDbType.DateTime);
            prm_valid_to.Value = valid_to;
            Command.Parameters.Add(prm_valid_to);

            SqlParameter prm_userid = new SqlParameter("@userid", SqlDbType.Int);
            prm_userid.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_userid);

            SqlParameter prm_project_name = new SqlParameter("@project_name", SqlDbType.VarChar, 50);
            prm_project_name.Value = project_name;
            Command.Parameters.Add(prm_project_name);

            if (project_agreement_id > 0)
            {
                SqlParameter prm_project_agreement_id = new SqlParameter("@project_agreement_id", SqlDbType.BigInt);
                prm_project_agreement_id.Value = project_agreement_id;
                Command.Parameters.Add(prm_project_agreement_id);
            }

            if (isDelete == true)
            {
                SqlParameter prm_isDelete = new SqlParameter("@isDelete", SqlDbType.Bit);
                prm_isDelete.Value = isDelete;
                Command.Parameters.Add(prm_isDelete);
            }

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "LOIDataAccess:price_agreement_iud", "usp_price_agreement_iud");
        }

        public int validate_loi_scope(int RequestId)
        {
            try
            {
                Command = new SqlCommand("usp_validate_loi_scope", Connection);
                Command.CommandType = CommandType.StoredProcedure;

                SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
                prm_RequestId.Value = RequestId;
                Command.Parameters.Add(prm_RequestId);                               

                object dt = AdditionalExecuteCommand.ExecuteScalar(Command, "LOIDataAccess:validate_loi_scope", "usp_validate_loi_scope");
                int dtInt = 0;
                if (dt != null)
                    dtInt = Convert.ToInt32(dt);
                return dtInt;

                //return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "LOIDataAccess:LOI_Upload_by_FM", "usp_LOI_Upload_by_FM");
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public bool checking_linkapproval_validity(int RequestId)
        {
            Command = new SqlCommand("usp_checking_linkapproval_validity", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
            prm_RequestId.Value = RequestId;
            Command.Parameters.Add(prm_RequestId);

            SqlParameter prm_lmby = new SqlParameter("@lmby", SqlDbType.Int);
            prm_lmby.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_lmby);

            object dt = AdditionalExecuteCommand.ExecuteScalar(Command, "LOIDataAccess:checking_linkapproval_validity", "usp_checking_linkapproval_validity");
            bool result = false;
            if (dt != null)
                result = (bool)dt;
            return result;
        }

        public DataTable getProcCM_Email()
        {
            Command = new SqlCommand("usp_getProcCM_Email", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = ContentSession.CTName;
            Command.Parameters.Add(prm_ProjectName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:getProcCM_Email", "usp_getProcCM_Email");
        }

        public string get_nextApproverRole_byApproverRole(string ApproverRole)
        {
            Command = new SqlCommand("usp_get_nextApproverRole_byApproverRole", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_ApproverRole = new SqlParameter("@ApproverRole", SqlDbType.VarChar, 5);
            prm_ApproverRole.Value = ApproverRole;
            Command.Parameters.Add(prm_ApproverRole);

            object dt = AdditionalExecuteCommand.ExecuteScalar(Command, "LOIDataAccess:get_nextApproverRole_byApproverRole", "usp_get_nextApproverRole_byApproverRole");
            string dtStr = "Error";
            if (dt != null)
                dtStr = dt.ToString();
            return dtStr;
        }

        public bool LOI_Detail_Update_Approval_Status(string workpackageid, int LOI_Detail_ID, int RequestId, string ApproverRole)
        {
            Command = new SqlCommand("usp_LOI_Detail_Update_Approval_Status", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_workpackageid = new SqlParameter("@workpackageid", SqlDbType.VarChar, 50);
            prm_workpackageid.Value = workpackageid;
            Command.Parameters.Add(prm_workpackageid);

            SqlParameter prm_LOI_Detail_ID = new SqlParameter("@LOI_Detail_ID", SqlDbType.BigInt);
            prm_LOI_Detail_ID.Value = LOI_Detail_ID;
            Command.Parameters.Add(prm_LOI_Detail_ID);

            SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
            prm_RequestId.Value = RequestId;
            Command.Parameters.Add(prm_RequestId);

           SqlParameter prm_ApproverRole = new SqlParameter("@ApproverRole", SqlDbType.VarChar, 50);
            prm_ApproverRole.Value = ApproverRole;
            Command.Parameters.Add(prm_ApproverRole);

            SqlParameter prm_lmby = new SqlParameter("@lmby", SqlDbType.Int);
            prm_lmby.Value = ContentSession.USERID;
            Command.Parameters.Add(prm_lmby);                       

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "LOIDataAccess:LOI_Detail_Update_Approval_Status", "usp_LOI_Detail_Update_Approval_Status");
        }
    }
}
