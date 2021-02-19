using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eLoi.Controller;
using System.Data;
using ClosedXML.Excel;
using System.IO;

public partial class Dashboard_frmDashboardMU : System.Web.UI.Page
{
    LOIController loiControllerr = new LOIController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindSubcon();
            BindAgenda();
            BindTasklist();
            BindSummaryCT();
        }
    }

    private void BindAgenda()
    {
        int ApprovalCount = loiControllerr.LOI_get_UploadedCount();
        if (ApprovalCount > 0)
        {
            ltrApprovalPendingCount.Text = "<span class=\"pull-right text-muted medium\">" + ApprovalCount + "</span>";
            aViewApproval.HRef = "frmListLOIMU.aspx";
        }

        int RejectedApprovalCount = loiControllerr.LOI_get_UploadRejectedCount();
        if (RejectedApprovalCount > 0)
        {
            ltrRejectedApprovalPendingCount.Text = "<span class=\"pull-right text-muted medium\">" + RejectedApprovalCount + "</span>";
            aViewApprovalRejected.HRef = "frmListLOIMUApprovalRejected.aspx";
        }
    }

    private void BindTasklist()
    {
        DataTable dt = loiControllerr.getTasklistSummary_Report(int.Parse(ddlSubcon.SelectedValue), ddlType.SelectedValue, txtperiodfromreqdate.Value, txtperiodtoreqdate.Value, txtperiodfromapprdate.Value, txtperiodtoapprdate.Value);
        pieChart.DataSource = dt;
        pieChart.DataBind();
        pieChart.PrimaryYAxis.Title.Text = ddlType.SelectedValue == "LOI request" ? "LOI Request Tracking" : "Site List Tracking";
    }

    protected void ddlSubcon_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindTasklist();
    }

    private void BindSubcon()
    {
        ddlSubcon.DataSource = loiControllerr.subcon_get_all_byctName();
        ddlSubcon.DataTextField = "SCon_Name";
        ddlSubcon.DataValueField = "SCon_Id";
        ddlSubcon.DataBind();
        ddlSubcon.Items.Insert(0, new ListItem("All Subcon", "0"));
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindTasklist();
    }

    private void BindSummaryCT()
    {
        gvLOISummary.DataSource = loiControllerr.getLOISummary_Count();
        gvLOISummary.DataBind();
    }


    private void ExportToExcel(string savename, DataTable dtSource, string sheetname)
    {
        dtSource.TableName = sheetname;
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dtSource);
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=" + savename + ".xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
    protected void btnDownloadList_Click(object sender, EventArgs e)
    {
        
        DataTable dtResult = loiControllerr.getLOISummary_Detail();
               if (dtResult.Rows.Count > 0)
                        ExportToExcel("LOISummary_" + string.Format("{0:ddMMMyyy}", DateTime.Now), dtResult, GeneralConfig.Def_sheetname);
    }

}