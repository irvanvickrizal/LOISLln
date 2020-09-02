using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eLoi.Controller;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Data;

public partial class Dashboard_frmApprovalByEmailLink : System.Web.UI.Page
{
    LOIController loiControllerr = new LOIController();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('LOI has been Approved Successfully');window.open('../autoclose.html')", true);
            string param = Request.QueryString["param"];
            param = EnDecController.DecryptLinkUrlApproval(param);
            string[] paramarr = param.Split(';');
            bool isApprove = bool.Parse(paramarr[0]);
            int RequestId = int.Parse(paramarr[1]);
            string ApproverRole = paramarr[2];
            hdapproverrole.Value = ApproverRole;
            hdnReqID.Value = RequestId.ToString();
            HttpContext.Current.Session[Resources.Resources.ses_userid] = int.Parse(paramarr[3]);

            if(loiControllerr.checking_linkapproval_validity(RequestId))
            {
                if (isApprove)
                {
                    if (loiControllerr.LOI_Update_Approval_Status(RequestId, true, ApproverRole, ""))
                    {
                        try
                        {
                            string nextApproval = loiControllerr.get_nextApproverRole_byApproverRole(ApproverRole);
                            SendEmail(RequestId, nextApproval);
                        }
                        catch (Exception ex)
                        {

                        }
                        finally
                        {
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('LOI has been Approved Successfully'); window.close();", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to Approved LOI'); window.close();", true);
                    }
                }
                else
                {
                    pnlRejection.Visible = true;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('The URL is no longer valid'); window.close();", true);
            }

            
        }
        catch(Exception ex)
        {

        }     
    }

    protected void btnSubmitReject_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtReasonRejection.Value))
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Remarks rejection is empty');", true);
            return;
        }

        int RequestId = int.Parse(hdnReqID.Value);
        if (loiControllerr.LOI_Update_Approval_Status(RequestId, false, hdapproverrole.Value, txtReasonRejection.Value))
        {
            try
            {
                SendEmailRejected(RequestId);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('LOI has been Rejected Successfully'); location.href = '../dashboard/frmListLOICPMApproval.aspx';", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to Rejected LOI');", true);
        }
    }

    private void SendEmail(int ReqId, string ApproverRole)
    {
        DataTable dtReceiverMail = loiControllerr.loi_get_receivermail_workflow(ReqId);
        string htmltabledetail = string.Empty;
        DataTable dt = loiControllerr.LOI_Detail_getEmailData(ReqId);
        htmltabledetail = CreateHtmlTableApproved(dt);
        if (dtReceiverMail.Rows.Count > 0)
        {
            string ReceiverMail = string.Join(",", dtReceiverMail.AsEnumerable()
                                  .Select(s => s.Field<string>("Email")).ToArray());
            string ReceiverName = string.Join(",", dtReceiverMail.AsEnumerable()
                                  .Select(s => s.Field<string>("Name")).ToArray());
            loiControllerr.sendMailApproval(dtReceiverMail, htmltabledetail, "PBM Approval", ContentSession.FULLNAME, ReqId, ApproverRole);

        }
    }
    private void SendEmailRejected(int ReqId)
    {
        DataTable dtReceiverMail = loiControllerr.loi_get_receivermail_rejected(ReqId);
        string htmltabledetail = string.Empty;
        DataTable dt = loiControllerr.LOI_Detail_getEmailData(ReqId);
        htmltabledetail = CreateHtmlTableApproved(dt);
        if (dtReceiverMail.Rows.Count > 0)
        {
            string ReceiverMail = string.Join(",", dtReceiverMail.AsEnumerable()
                                  .Select(s => s.Field<string>("Email")).ToArray());
            string ReceiverName = string.Join(",", dtReceiverMail.AsEnumerable()
                                  .Select(s => s.Field<string>("Name")).ToArray());
            loiControllerr.sendMail(ReceiverName, ReceiverMail, htmltabledetail, "Rejected", ContentSession.FULLNAME, ReqId);

        }
    }

    private string CreateHtmlTableApproved(DataTable dt)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(strhtmlemailapproveheader());
        int no = 0;
        foreach (DataRow dr in dt.Rows)
        {
            no += 1;
            DateTime Customer_PO_Date = Convert.ToDateTime(dr["Customer_PO_Date"]);
            DateTime Closing_Plan_Date = Convert.ToDateTime(dr["Closing_Plan_Date"]);

            sb.Append("<tr>");
            sb.Append("<td>"); sb.Append(no.ToString()); sb.Append("</td>");
            sb.Append("<td>"); sb.Append(dr["workpackageid"]); sb.Append("</td>");
            sb.Append("<td>"); sb.Append(dr["Customer_PO"]); sb.Append("</td>");
            sb.Append("<td>"); sb.Append(Customer_PO_Date.ToString("d-MMM-yy")); sb.Append("</td>");
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
}