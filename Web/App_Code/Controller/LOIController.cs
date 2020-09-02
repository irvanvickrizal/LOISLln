using eLoi.DataAccess;
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

namespace eLoi.Controller
{
    public class LOIController
    {
        LOIDataAccess da;
        public LOIController()
        {
            da = new LOIDataAccess();
        }

        public int LOI_Upload_by_FM(int subcon_Id, int scope_type_id)
        {
            return da.LOI_Upload_by_FM(subcon_Id, scope_type_id);
        }

        public bool LOI_Detail_Upload_by_FM(DataRow drLOI, int RequestId, int RegionId, int ARA_ID)
        {
            return da.LOI_Detail_Upload_by_FM(drLOI, RequestId, RegionId, ARA_ID);
        }

        public DataTable LOI_get_Uploaded(int seqno = 0)
        {
            return da.LOI_get_Uploaded(seqno);
        }
        public int LOI_get_UploadedCount(int seqno = 0)
        {
            return da.LOI_get_UploadedCount(seqno);
        }

        public DataTable LOI_Detail_by_RequestId(int RequestId)
        {
            return da.LOI_Detail_by_RequestId(RequestId);
        }

        public DataTable LOI_Detail_ClossingPending_by_RequestId(int RequestId)
        {
            return da.LOI_Detail_ClossingPending_by_RequestId(RequestId);
        }

        public bool LOI_Update_Price(DataRow drLOI, int RequestId)
        {
            return da.LOI_Update_Price(drLOI, RequestId);
        }

        public bool LOI_Update_BOQ_Pricing(int RequestId)
        {
            return da.LOI_Update_BOQ_Pricing(RequestId);
        }
        public bool LOI_Update_CPMDocAvailability(DataRow drLOI, bool isCPMDocAvailable)
        {
            return da.LOI_Update_CPMDocAvailability(drLOI, isCPMDocAvailable);
        }

        public int CheckLOI_Upload_by_FM(DataRow drLOI)
        {
            return da.CheckLOI_Upload_by_FM(drLOI);
        }

        public DataTable SubconName_getList()
        {
            return da.SubconName_getList();
        }
        public int Validate_SubconName(string SCon_Name)
        {
            return da.Validate_SubconName(SCon_Name);
        }



        public DataTable initiateLOITable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("workpackageid");
            dt.Columns.Add("Customer_PO");
            dt.Columns.Add("Customer_PO_Date");
            dt.Columns.Add("PO_Description");
            dt.Columns.Add("Region");
            dt.Columns.Add("Site_ID");
            dt.Columns.Add("ScopeOfWork");
            dt.Columns.Add("Subcone_Name");
            dt.Columns.Add("Site_Model");
            dt.Columns.Add("Unit_Price");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Total_Price");
            dt.Columns.Add("Closing_Plan_Date");
            dt.Columns.Add("Remarks");
            dt.Columns.Add("Currency");
            dt.Columns.Add("LOI_Detail_ID");
            return dt;
        }

        public bool LOI_Update_SupDoc_Availibility(int RequestId)
        {
            return da.LOI_Update_SupDoc_Availibility(RequestId);
        }

        public bool LOI_Update_Approval_Status(int RequestId, bool isApprove, string ApproverRole, string Reject_Remarks)
        {
            return da.LOI_Update_Approval_Status(RequestId, isApprove, ApproverRole, Reject_Remarks);
        }
        public DataTable ssp_checking_document_getall(int RequestId)
        {
            return da.ssp_checking_document_getall(RequestId);
        }

        public bool loi_supdoc_availibility_iu(int supportingdocument_id, int RequestId, bool availibility, string remarks)
        {
            return da.loi_supdoc_availibility_iu(supportingdocument_id, RequestId, availibility, remarks);
        }

        public bool loi_supdoc_availibility_D(int supportingdocument_id, int RequestId)
        {
            return da.loi_supdoc_availibility_D(supportingdocument_id, RequestId);
        }

        public DataTable cpm_checking_document_getall(int RequestId)
        {
            return da.cpm_checking_document_getall(RequestId);
        }

        public DataTable loi_supportingdoc_getall(int RequestId)
        {
            return da.loi_supportingdoc_getall(RequestId);
        }

        public int Loi_NotAvailable_Price_Count()
        {
            return da.Loi_NotAvailable_Price_Count();
        }

        public DataTable Loi_NotAvailable_Price()
        {
            return da.Loi_NotAvailable_Price();
        }

        public int Loi_NotAvailable_SuppDoc_Count()
        {
            return da.Loi_NotAvailable_SuppDoc_Count();
        }

        public DataTable Loi_NotAvailable_SuppDoc()
        {
            return da.Loi_NotAvailable_SuppDoc();
        }

        public DataTable supporting_document_getNotAvailable(int RequestId)
        {
            return da.supporting_document_getNotAvailable(RequestId);
        }

        public DataTable report_loi_approvaltracking_getdata(int subconid, string periodfrom, string periodto)
        {
            return da.report_loi_approvaltracking_getdata(subconid, periodfrom, periodto);
        }
        public DataTable report_loi_done_getdata(int subconid, string periodfrom, string periodto)
        {
            return da.report_loi_done_getdata(subconid, periodfrom, periodto);
        }

        public DataTable report_loi_rejection_getdata(int subconid, string periodfrom, string periodto)
        {
            return da.report_loi_rejection_getdata(subconid, periodfrom, periodto);
        }

        public DataTable report_loi_open_getdata(int subconid, string periodfrom, string periodto)
        {
            return da.report_loi_open_getdata(subconid, periodfrom, periodto);
        }

        public bool change_loigroup_by_maxtotalprice(int RequestId)
        {
            return da.change_loigroup_by_maxtotalprice(RequestId);
        }

        public bool LOI_Update_status_uploadPrice(int RequestId, DateTime Closing_Plan_Date)
        {
            return da.LOI_Update_status_uploadPrice(RequestId, Closing_Plan_Date);
        }

        public DataTable LOI_Cancellation_getList()
        {
            return da.LOI_Cancellation_getList();
        }

        public int LOI_get_UploadRejectedCount(int seqno = 0)
        {
            return da.LOI_get_UploadRejectedCount(seqno);
        }

        public DataTable LOI_get_UploadRejected(int seqno = 0)
        {
            return da.LOI_get_UploadRejected(seqno);
        }
        public bool LOI_UploadRejected_by_FM(int ReqId)
        {
            return da.LOI_UploadRejected_by_FM(ReqId);
        }

        public DataTable LOI_Overdue()
        {
            return da.LOI_Overdue();
        }

        public DataTable master_sow_getList_detail()
        {
            return da.master_sow_getList_detail();
        }

        public int Validate_sowdetail(string detail_sow)
        {
            return da.Validate_sowdetail(detail_sow);
        }

        public bool LOIDetail_Checking_duplicate_sow_siteid_byReqno(int RequestId, string Site_ID, string ScopeOfWork)
        {
            return da.LOIDetail_Checking_duplicate_sow_siteid_byReqno(RequestId, Site_ID, ScopeOfWork);
        }

        public DataTable boq_price_getNotAvailable(int RequestId)
        {
            return da.boq_price_getNotAvailable(RequestId);
        }

        public string LOIDetail_validate(string workpackageid, int Subcon_Id, string scopeofwork, string SCon_Name)
        {
            return da.LOIDetail_validate(workpackageid, Subcon_Id, scopeofwork, SCon_Name);
        }

        public DataTable LOI_get_Uploaded_byStatus(string LOI_Status)
        {
            return da.LOI_get_Uploaded_byStatus(LOI_Status);
        }

        public int LOI_get_UploadedCount_byStatus(string LOI_Status)
        {
            return da.LOI_get_UploadedCount_byStatus(LOI_Status);
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
            catch(Exception ex)
            {
                EBOQ_Lib_New.DAL.DAL_AppLog.ErrLogInsert("LOIController:SendEmail", ex.Message, "NON-SP");
            }
        }

        public void sendMailApproval(DataTable dtReceiver, string htmlstringtable, string loi_status, string executorname, int requestid, string ApproverRole)
        {
            try
            {
                StringBuilder sbResult = new StringBuilder();
                string emailsubject = string.Empty;
                string fullname = dtReceiver.Rows[0]["Name"].ToString();
                string email = dtReceiver.Rows[0]["Email"].ToString();
                string paramapproveinemail = string.Concat("true;", requestid.ToString(), ";", ApproverRole, ";", dtReceiver.Rows[0]["USR_ID"].ToString());
                string paramrejectinemail = string.Concat("false;", requestid.ToString(), ";", ApproverRole, ";", dtReceiver.Rows[0]["USR_ID"].ToString());
                paramapproveinemail = EnDecController.EncryptLinkUrlApproval(paramapproveinemail);
                paramrejectinemail = EnDecController.EncryptLinkUrlApproval(paramrejectinemail);
                DataTable dtEmail = loi_get_email_data(loi_status, requestid);
                DataTable dtDocumentAvailibility = loi_supportingdoc_getall(requestid);
                string HtmlTableSupportingDocAvailibility = CreateHtmlTableSupportingDocAvailibility(dtDocumentAvailibility);
                if (dtEmail.Rows.Count > 0)
                {
                    emailsubject = dtEmail.Select("config_key like '%subject%'")[0].ItemArray[1].ToString();
                    string emailbody = dtEmail.Select("config_key like '%body%'")[0].ItemArray[1].ToString();
                    emailbody = emailbody.Replace("[name]", fullname).Replace("[sourcename]", executorname);
                    emailbody = emailbody.Replace("[tabledetail]", htmlstringtable);
                    emailbody = emailbody.Replace("[param approve]", paramapproveinemail);
                    emailbody = emailbody.Replace("[param reject]", paramrejectinemail);
                    emailbody = emailbody.Replace("[document_avilibility]", HtmlTableSupportingDocAvailibility);
                    sbResult.Append(emailbody);
                    MailController.SendMail(email, emailsubject, sbResult.ToString(), GeneralConfig.MailConfigType());
                }
            }
            catch (Exception ex)
            {
                EBOQ_Lib_New.DAL.DAL_AppLog.ErrLogInsert("LOIController:SendEmail", ex.Message, "NON-SP");
            }
        }

        public void sendMailResetPassword(string fullname, string email, string loi_status, string userlogin, string password)
        {
            try
            {
                StringBuilder sbResult = new StringBuilder();
                string emailsubject = string.Empty;

                DataTable dtEmail = loi_get_email_data(loi_status, 0);
                if (dtEmail.Rows.Count > 0)
                {
                    emailsubject = dtEmail.Select("config_key like '%subject%'")[0].ItemArray[1].ToString();
                    string emailbody = dtEmail.Select("config_key like '%body%'")[0].ItemArray[1].ToString();
                    emailbody = emailbody.Replace("[name]", fullname);
                    emailbody = emailbody.Replace("[userlogin]", userlogin);
                    emailbody = emailbody.Replace("[password]", password);
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

        public DataTable loi_get_receivermail_workflow(int RequestId)
        {
            return da.loi_get_receivermail_workflow(RequestId);
        }

        public DataTable LOI_Detail_getEmailData(int RequestId)
        {
            return da.LOI_Detail_getEmailData(RequestId);
        }

        public DataTable loi_get_receivermail_rejected(int RequestId)
        {
            return da.loi_get_receivermail_rejected(RequestId);
        }

        public bool create_loi_refference(int RequestId)
        {
            return da.create_loi_refference(RequestId);
        }

        public DataTable getTasklistSummary_Report(int subconid, string DashboardType, string ReqPeriodfrom, string ReqPeriodto, string ApprovePeriodfrom, string ApprovePeriodto)
        {
            return da.getTasklistSummary_Report(subconid, DashboardType, ReqPeriodfrom, ReqPeriodto, ApprovePeriodfrom, ApprovePeriodto);
        }
        public DataTable report_loi_done_detail_getdata(int subconid, string periodfrom, string periodto, string siteid, string cpo, int reqid)
        {
            return da.report_loi_done_detail_getdata(subconid, periodfrom, periodto, siteid, cpo, reqid);
        }

        public DataTable report_loi_approvaltracking_detail_getdata(int subconid, string periodfrom, string periodto, string siteid, string cpo, int reqid)
        {
            return da.report_loi_approvaltracking_detail_getdata(subconid, periodfrom, periodto, siteid, cpo, reqid);
        }

        public DataTable report_loi_rejection_detail_getdata(int subconid, string periodfrom, string periodto, string siteid, string cpo, int reqid)
        {
            return da.report_loi_rejection_detail_getdata(subconid, periodfrom, periodto, siteid, cpo, reqid);
        }

        public DataTable subcon_get_all_byctName()
        {
            return da.subcon_get_all_byctName();
        }

        public DataTable getmonth()
        {
            return da.getmonth();
        }

        public DataTable usp_report_loi_overdue_getdata(int subconid, string periodfrom, string periodto)
        {
            return da.report_loi_overdue_getdata(subconid, periodfrom, periodto);
        }

        public DataTable report_loi_overdue_detail_getdata(int subconid, string periodfrom, string periodto, string siteid, string cpo, int reqid)
        {
            return da.report_loi_overdue_detail_getdata(subconid, periodfrom, periodto, siteid, cpo, reqid);
        }

        public DataTable report_loi_open_detail_getdata(int subconid, string periodfrom, string periodto, string siteid, string cpo, int reqid)
        {
            return da.report_loi_open_detail_getdata(subconid, periodfrom, periodto, siteid, cpo, reqid);
        }

        public bool update_loi_price_fromTemp(int RequestId)
        {
            return da.update_loi_price_fromTemp(RequestId);
        }

        public DataTable get_region_by_userid()
        {
            return da.get_region_by_userid();
        }

        public DataTable Validate_Region(string RGNName)
        {
            return da.Validate_Region(RGNName);
        }

        public DataTable report_loi_getdata(int subconid, string periodfrom, string periodto, string reporttype)
        {
            return da.report_loi_getdata(subconid, periodfrom, periodto, reporttype);
        }

        public DataTable report_loi_detail_getdata(string siteid, string cpo, int reqid, string ReportType)
        {
            return da.report_loi_detail_getdata(siteid, cpo, reqid, ReportType);
        }

        public DataTable report_loi_cancelled_detail_getdata(int subconid, string periodfrom, string periodto, string siteid, string cpo, int reqid)
        {
            return da.report_loi_cancelled_detail_getdata(subconid, periodfrom, periodto, siteid, cpo, reqid);
        }

        public DataTable getloilog_byrequestid(int requestid)
        {
            return da.getloilog_byrequestid(requestid);
        }

        public DataTable getloihistory_byusrid()
        {
            return da.getloihistory_byusrid();
        }

        public decimal get_max_totalprice()
        {
            return da.get_max_totalprice();
        }

        public DataTable project_agreement_getbyprojectname()
        {
            return da.project_agreement_getbyprojectname();
        }

        public bool project_agreement_iud(string project_agreement_no, DateTime valid_from, DateTime valid_to, string project_name, int project_agreement_id, bool isDelete)
        {
            return da.project_agreement_iud(project_agreement_no, valid_from, valid_to, project_name, project_agreement_id, isDelete);
        }

        public DataTable price_agreement_getbyprojectname()
        {
            return da.price_agreement_getbyprojectname();
        }

        public bool price_agreement_iud(string price_agreement_doc, DateTime valid_from, DateTime valid_to, string project_name, int project_agreement_id, bool isDelete)
        {
            return da.price_agreement_iud(price_agreement_doc, valid_from, valid_to, project_name, project_agreement_id, isDelete);
        }

        private string CreateHtmlTableSupportingDocAvailibility(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<table class='Grid' cellspacing='0' rules='all' border='1' style='border-collapse:collapse;'>
                	<tr>
                    	<th style='background-color:darkblue; color:white;'>No</th>
                    	<th style='background-color:darkblue; color:white;'>Document Name</th>
                    	<th style='background-color:darkblue; color:white;'>Availibility</th>
                    	<th style='background-color:darkblue; color:white;'>Remarks</th>                    	
                    </tr>");
            int no = 0;

            foreach (DataRow dr in dt.Rows)
            {
                no += 1;
                sb.Append("<tr>");
                sb.Append("<td>"); sb.Append(no.ToString()); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(dr["document_name"]); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(dr["availibilityName"]); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(dr["remarks"]); sb.Append("</td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            return sb.ToString();
        }

        public int validate_loi_scope(int RequestId)
        {
            return da.validate_loi_scope(RequestId);
        }

        public bool checking_linkapproval_validity(int RequestId)
        {
            return da.checking_linkapproval_validity(RequestId);
        }

        public DataTable getProcCM_Email()
        {
            return da.getProcCM_Email();
        }

        public string get_nextApproverRole_byApproverRole(string ApproverRole)
        {
            return da.get_nextApproverRole_byApproverRole(ApproverRole);
        }

        public bool LOI_Detail_Update_Approval_Status(string workpackageid, int LOI_Detail_ID, int RequestId, string ApproverRole)
        {
            return da.LOI_Detail_Update_Approval_Status(workpackageid, LOI_Detail_ID, RequestId, ApproverRole);
        }
    }
}