using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using EBOQ_Lib_New.DAL;

namespace LOIGeneralScheduler.App_Code.DataAccess
{
    public class GeneralDataAccess : DBConfiguration
    {
        public GeneralDataAccess()
        {
        }

        public DataTable loi_supporting_document_notavailable_data()
        {
            Command = new SqlCommand("usp_loi_supporting_document_notavailable_data", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "GeneralSchedDataAccess:loi_supporting_document_notavailable_data", "usp_loi_supporting_document_notavailable_data");
        }

        public DataTable LOI_Detail_getEmailData(int RequestId)
        {
            Command = new SqlCommand("usp_LOI_Detail_getEmailData", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
            prm_RequestId.Value = RequestId;
            Command.Parameters.Add(prm_RequestId);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "GeneralSchedDataAccess:LOI_Detail_getEmailData", "usp_LOI_Detail_getEmailData");
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

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "GeneralSchedDataAccess:atp_endorse_get_email_data", "usp_atp_endorse_get_email_data");
        }

        public bool checking_loi_overdue()
        {
            Command = new SqlCommand("usp_checking_loi_overdue", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "GeneralSchedDataAccess:checking_loi_overdue", "usp_checking_loi_overdue");
        }

        public DataTable report_loi_approvaltracking_detail_getdata(int userid, string CTName)
        {
            Command = new SqlCommand("usp_report_loi_approvaltracking_detail_getdata", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_userId = new SqlParameter("@userId", SqlDbType.BigInt);
            prm_userId.Value = userid;
            Command.Parameters.Add(prm_userId);           

            SqlParameter prm_ProjectName = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
            prm_ProjectName.Value = CTName;
            Command.Parameters.Add(prm_ProjectName);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:report_loi_approvaltracking_detail_getdata", "usp_report_loi_approvaltracking_detail_getdata");
        }

        public DataTable Email_CDM_getdata()
        {
            Command = new SqlCommand("usp_Email_CDM_getdata", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:Email_CDM_getdata", "usp_Email_CDM_getdata");
        }
    }


}
