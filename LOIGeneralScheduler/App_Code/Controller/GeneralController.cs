using LOIGeneralScheduler.App_Code.DataAccess;
using ClosedXML.Excel;
using EBoqProvider;
using Nokia.Eboq.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Transactions;


namespace LOIGeneralScheduler.App_Code.Controller
{
    public class GeneralController
    {
        GeneralDataAccess da;
        public GeneralController()
        {
            da = new GeneralDataAccess();
        }

        public DataTable loi_supporting_document_notavailable_data()
        {
            return da.loi_supporting_document_notavailable_data();
        }

        public DataTable LOI_Detail_getEmailData(int RequestId)
        {
            return da.LOI_Detail_getEmailData(RequestId);
        }

        public void sendMail(string fullname, string email, string htmlstringtable, string loi_status, string executorname, int requestid)
        {
            try
            {
                StringBuilder sbResult = new StringBuilder();
                string emailsubject = string.Empty;

                DataTable dtEmail = loi_get_email_data(loi_status, requestid);
                if (dtEmail.Rows.Count > 0)
                {
                    emailsubject = dtEmail.Select("config_key like '%subject%'")[0].ItemArray[1].ToString();
                    string emailbody = dtEmail.Select("config_key like '%body%'")[0].ItemArray[1].ToString();
                    emailbody = emailbody.Replace("[name]", fullname).Replace("[sourcename]", executorname);
                    emailbody = emailbody.Replace("[tabledetail]", htmlstringtable);
                    sbResult.Append(emailbody);
                    MailController.SendMail(email, emailsubject, sbResult.ToString(), GeneralConfig.MailConfigType());
                }
            }
            catch (Exception ex)
            {
                EBOQ_Lib_New.DAL.DAL_AppLog.ErrLogInsert("LOIController:SendEmail", ex.Message, "NON-SP");
            }
        }

        public DataTable loi_get_email_data(string loi_status, int requestid)
        {
            return da.loi_get_email_data(loi_status, requestid);
        }

        public bool checking_loi_overdue()
        {
            return da.checking_loi_overdue();
        }

        public DataTable report_loi_approvaltracking_detail_getdata(int userid, string CTName)
        {
            return da.report_loi_approvaltracking_detail_getdata(userid, CTName);
        }

        public DataTable Email_CDM_getdata()
        {
            return da.Email_CDM_getdata();
        }
    }
}
