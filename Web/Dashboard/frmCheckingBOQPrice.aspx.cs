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

public partial class Dashboard_frmCheckingBOQPrice : System.Web.UI.Page
{
    LOIController loiControllerr = new LOIController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            bindSuppDoc();
        }
    }

    private void BindData(int pgIndex = 0)
    {
        int RequestId = int.Parse(Request.QueryString["RequestId"]);
        gvLOIData.DataSource = loiControllerr.LOI_Detail_by_RequestId(RequestId);
        gvLOIData.PageIndex = pgIndex;
        gvLOIData.DataBind();
    }

    protected void gvLOIData_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void gvLOIData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected void gvLOIData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindData(e.NewPageIndex);
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        bool isAllChecked = true;
        foreach (GridViewRow item in gvSupDoc.Rows)
        {
            TextBox txtAvailibility = (TextBox)item.FindControl("txtAvailibility");
            if (string.IsNullOrEmpty(txtAvailibility.Text))
            {
                isAllChecked = false;
                break;
            }
        }

        if (isAllChecked)
        {
            int RequestId = int.Parse(Request.QueryString["RequestId"]);
            if (loiControllerr.LOI_Update_BOQ_Pricing(RequestId))
            {
                try
                {
                    SendEmail(RequestId);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    string querystr = Request.QueryString["isRejected"] == "1" ? "location.href = '../dashboard/frmListLOISSPRejected.aspx';" : "location.href = '../dashboard/frmListLOISSP.aspx';";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Submit checking data Succcessfully');" + querystr, true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed Submit checking data');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Not all data has been checked yet');", true);
        }
    }

    protected void gvSupDoc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string str_availibility = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "availibility"));
            TextBox txtRemarksNoConfirm = (TextBox)e.Row.FindControl("txtRemarksNoConfirm");
            txtRemarksNoConfirm.Enabled = true;

            if (!string.IsNullOrEmpty(str_availibility))
            {
                bool availibility = Convert.ToBoolean(str_availibility);
                Button btnYes = (Button)e.Row.FindControl("btnYes");
                Button btnNoConfirm = (Button)e.Row.FindControl("btnNoConfirm");
                Button btnSubmitNoConfirm = (Button)e.Row.FindControl("btnSubmitNoConfirm");
                Button btnCancelNoConfirm = (Button)e.Row.FindControl("btnCancelNoConfirm");
                TextBox txtAvailibility = (TextBox)e.Row.FindControl("txtAvailibility");
                ImageButton imgCanceled = (ImageButton)e.Row.FindControl("imgCanceled");
                txtAvailibility.Text = "Available";

                btnYes.Visible = false;
                btnNoConfirm.Visible = false;
                btnSubmitNoConfirm.Visible = false;
                btnCancelNoConfirm.Visible = false;
                txtRemarksNoConfirm.Visible = false;
                txtAvailibility.Visible = true;
                imgCanceled.Visible = true;

                if (!availibility)
                {
                    txtRemarksNoConfirm.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "remarks"));
                    txtRemarksNoConfirm.Visible = true;
                    txtRemarksNoConfirm.Enabled = false;
                    txtAvailibility.Text = "Not Available";
                }
            }
        }
    }

    private void bindSuppDoc()
    {
        int RequestId = int.Parse(Request.QueryString["RequestId"]);
        gvSupDoc.DataSource = loiControllerr.ssp_checking_document_getall(RequestId);
        gvSupDoc.DataBind();
    }

    protected void gvSupDoc_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("ConfirmYes"))
        {
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            int SupDocId = int.Parse(e.CommandArgument.ToString());
            int RequestId = int.Parse(Request.QueryString["RequestId"]);

            loiControllerr.loi_supdoc_availibility_iu(SupDocId, RequestId, true, "");

            bindSuppDoc();
        }
        else if (e.CommandName.Equals("ConfirmNo"))
        {
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            Button btnYes = (Button)row.FindControl("btnYes");
            Button btnNoConfirm = (Button)row.FindControl("btnNoConfirm");
            Button btnSubmitNoConfirm = (Button)row.FindControl("btnSubmitNoConfirm");
            Button btnCancelNoConfirm = (Button)row.FindControl("btnCancelNoConfirm");
            TextBox txtRemarksNoConfirm = (TextBox)row.FindControl("txtRemarksNoConfirm");
            TextBox txtAvailibility = (TextBox)row.FindControl("txtAvailibility");
            ImageButton imgCanceled = (ImageButton)row.FindControl("imgCanceled");

            btnYes.Visible = false;
            btnNoConfirm.Visible = false;
            btnSubmitNoConfirm.Visible = true;
            btnCancelNoConfirm.Visible = true;
            txtRemarksNoConfirm.Visible = true;
            txtAvailibility.Visible = false;
            imgCanceled.Visible = false;
        }
        else if (e.CommandName.Equals("CancelConfirmNo"))
        {
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            Button btnYes = (Button)row.FindControl("btnYes");
            Button btnNoConfirm = (Button)row.FindControl("btnNoConfirm");
            Button btnSubmitNoConfirm = (Button)row.FindControl("btnSubmitNoConfirm");
            Button btnCancelNoConfirm = (Button)row.FindControl("btnCancelNoConfirm");
            TextBox txtRemarksNoConfirm = (TextBox)row.FindControl("txtRemarksNoConfirm");
            TextBox txtAvailibility = (TextBox)row.FindControl("txtAvailibility");
            ImageButton imgCanceled = (ImageButton)row.FindControl("imgCanceled");

            btnYes.Visible = true;
            btnNoConfirm.Visible = true;
            btnSubmitNoConfirm.Visible = false;
            btnCancelNoConfirm.Visible = false;
            txtRemarksNoConfirm.Visible = false;
            txtAvailibility.Visible = false;
            imgCanceled.Visible = false;
        }
        else if (e.CommandName.Equals("SubmitConfirmNo"))
        {
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            TextBox txtRemarksNoConfirm = (TextBox)row.FindControl("txtRemarksNoConfirm");

            if (string.IsNullOrEmpty(txtRemarksNoConfirm.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Remarks is Empty');", true);
                return;
            }

            int SupDocId = int.Parse(e.CommandArgument.ToString());
            int RequestId = int.Parse(Request.QueryString["RequestId"]);

            loiControllerr.loi_supdoc_availibility_iu(SupDocId, RequestId, false, txtRemarksNoConfirm.Text);
            bindSuppDoc();
        }
        else if (e.CommandName.Equals("CancelAvailibility"))
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int SupDocId = int.Parse(e.CommandArgument.ToString());
            int RequestId = int.Parse(Request.QueryString["RequestId"]);

            loiControllerr.loi_supdoc_availibility_D(SupDocId, RequestId);
            bindSuppDoc();
        }
    }

    private void SendEmail(int ReqId)
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
            loiControllerr.sendMail(ReceiverName, ReceiverMail, htmltabledetail, "Checking Supporting Doc", ContentSession.FULLNAME, ReqId);

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