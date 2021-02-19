using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using LOIGeneralScheduler.App_Code.Controller;

namespace LOIGeneralScheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            Program prog = new Program();
            string RunningProgramName = ConfigurationManager.AppSettings["RunningProgramName"];
            switch (RunningProgramName)
            {
                case "NotifSupDocAvailibility":
                    prog.SendNotifSupportingDocumentNotAvailable();
                    break;
                case "SendNotifApprovalTracking":
                    prog.SendNotifApprovalTracking();
                    break;
                    


            }

            prog.checkingloiiverdue();
        }

        private void SendNotifSupportingDocumentNotAvailable()
        {
            GeneralController gc = new GeneralController();
            DataTable dt = gc.loi_supporting_document_notavailable_data();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string htmltabledetail = string.Empty;
                    DataTable dtemail = gc.LOI_Detail_getEmailData(Convert.ToInt32(dr["RequestId"]));
                    htmltabledetail = CreateHtmlTableApproved(dtemail);
                    if (dtemail.Rows.Count > 0)
                    {
                        string ReceiverMail = dr["Email"].ToString();
                        string ReceiverName = dr["Name"].ToString();
                        gc.sendMail(ReceiverName, ReceiverMail, htmltabledetail, "notifnotavailabledoc", "System", Convert.ToInt32(dr["RequestId"]));

                    }
                }                
            }
        }

        private void checkingloiiverdue()
        {
            GeneralController gc = new GeneralController();
            gc.checking_loi_overdue();
        }

        private string CreateHtmlTableApproved(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(strhtmlemailapproveheader());
            int no = 0;
            foreach (DataRow dr in dt.Rows)
            {
                no += 1;
                DateTime Upload_Date = Convert.ToDateTime(dr["Upload_Date"]);
                DateTime Closing_Plan_Date = Convert.ToDateTime(dr["Closing_Plan_Date"]);

                sb.Append("<tr>");
                sb.Append("<td>"); sb.Append(no.ToString()); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(dr["workpackageid"]); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(dr["Customer_PO"]); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(Upload_Date.ToString("d-MMM-yy")); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(dr["PO_Description"]); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(dr["Region"]); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(dr["Site_ID"]); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(dr["ScopeOfWork"]); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(dr["Subcone_Name"]); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(dr["Site_Model"]); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(String.Format("{0:n0}", dr["Unit_Price"])); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(dr["Qty"]); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(String.Format("{0:n0}", dr["Total_Price"])); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(dr["Currency"]); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(Closing_Plan_Date.ToString("d-MMM-yy")); sb.Append("</td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            return sb.ToString();
        }

        private string strhtmlemailapproveheader()
        {
            return @"<table class='Grid' cellspacing='0' rules='all' border='1' style='border-collapse:collapse;'>
                	<tr>
                    	<th style='background-color:darkblue; color:white;'>No</th>
                    	<th style='background-color:darkblue; color:white;'>WPID/SMPID</th>
                    	<th style='background-color:darkblue; color:white;'>Customer PO</th>
                    	<th style='background-color:darkblue; color:white;'>Customer PO Date</th>
                    	<th style='background-color:darkblue; color:white;'>PO Description</th>
                    	<th style='background-color:darkblue; color:white;'>Region/Area</th>
                    	<th style='background-color:darkblue; color:white;'>Site ID</th>
                    	<th style='background-color:darkblue; color:white;'>Scope of Work</th>
                    	<th style='background-color:darkblue; color:white;'>Subcon Name</th>
                    	<th style='background-color:darkblue; color:white;'>Site Model</th>
                        <th style='background-color:darkblue; color:white;'>Unit Price</th>
                    	<th style='background-color:darkblue; color:white;'>Qty</th>
                    	<th style='background-color:darkblue; color:white;'>Total Price</th>
                    	<th style='background-color:darkblue; color:white;'>Currency</th>
                    	<th style='background-color:darkblue; color:white;'>Closing Plan Date</th>
                    </tr>";
        }

        private void SendNotifApprovalTracking()
        {
            GeneralController gc = new GeneralController();
            DataTable dtreceiver = gc.Email_CDM_getdata();
            foreach(DataRow dr in dtreceiver.Rows)
            {
                DataTable dtemail = gc.report_loi_approvaltracking_detail_getdata(Convert.ToInt32(dr["USR_ID"]), dr["CTName"].ToString());
                if (dtemail.Rows.Count > 0)
                {
                    string htmltabledetail = string.Empty;
                    htmltabledetail = CreateHtmlTableApprovealTracking(dtemail);
                    if (dtemail.Rows.Count > 0)
                    {
                        string ReceiverMail = dr["Email"].ToString();
                        string ReceiverName = dr["Name"].ToString();
                        gc.sendMail(ReceiverName, ReceiverMail, htmltabledetail, "ApprovalTracking", "System", 0);

                    }
                }
            }

            
        }

        private string CreateHtmlTableApprovealTracking(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<table class='Grid' cellspacing='0' rules='all' border='1' style='border-collapse:collapse;'>
                	<tr>
                    	<th style='background-color:darkblue; color:white;'>No</th>
                    	<th style='background-color:darkblue; color:white;'>WPID/SMPID</th>
                    	<th style='background-color:darkblue; color:white;'>Customer PO</th>
                    	<th style='background-color:darkblue; color:white;'>Customer PO Date</th>
                    	<th style='background-color:darkblue; color:white;'>PO Description</th>
                    	<th style='background-color:darkblue; color:white;'>Region/Area</th>
                    	<th style='background-color:darkblue; color:white;'>Site ID</th>
                    	<th style='background-color:darkblue; color:white;'>Scope of Work</th>
                    	<th style='background-color:darkblue; color:white;'>Subcon Name</th>
                    	<th style='background-color:darkblue; color:white;'>Site Model</th>
                        <th style='background-color:darkblue; color:white;'>Unit Price</th>
                    	<th style='background-color:darkblue; color:white;'>Qty</th>
                    	<th style='background-color:darkblue; color:white;'>Total Price</th>
                    	<th style='background-color:darkblue; color:white;'>Currency</th>
                    	<th style='background-color:darkblue; color:white;'>Closing Plan Date</th>
                        <th style='background-color:darkblue; color:white;'>PIC</th>
                    	<th style='background-color:darkblue; color:white;'>Aging</th>
                    </tr>");
            int no = 0;
            foreach (DataRow dr in dt.Rows)
            {
                no += 1;
                DateTime Upload_Date = Convert.ToDateTime(dr["Upload_Date"]);
                string strClosing_Plan_Date = dr["Closing_Plan_Date"].ToString();
                DateTime Closing_Plan_Date;
                DateTime.TryParse(strClosing_Plan_Date, out Closing_Plan_Date);
                if (Closing_Plan_Date != new DateTime())
                    strClosing_Plan_Date = Closing_Plan_Date.ToString("d-MMM-yy");
                sb.Append("<tr>");
                sb.Append("<td>"); sb.Append(no.ToString()); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(dr["workpackageid"]); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(dr["Customer_PO"]); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(Upload_Date.ToString("d-MMM-yy")); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(dr["PO_Description"]); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(dr["Region"]); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(dr["Site_ID"]); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(dr["ScopeOfWork"]); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(dr["Subcone_Name"]); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(dr["Site_Model"]); sb.Append("</td>");
                //sb.Append("<td>"); sb.Append(String.Format("{0:n0}", dr["Unit_Price"])); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(dr["Qty"]); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(String.Format("{0:n0}", dr["Total_Price"])); sb.Append("</td>");
                //sb.Append("<td>"); sb.Append(dr["Currency"]); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(strClosing_Plan_Date); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(dr["PIC"]); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(dr["Aging"]); sb.Append("</td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            return sb.ToString();
        }

    }
}
