using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eLoi.Controller;
using System.Data;
using System.IO;
using ClosedXML.Excel;
using System.Configuration;

public partial class rpt_frmLOIReportDetail : System.Web.UI.Page
{
    LOIController loiControllerr = new LOIController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            BindDataLog();
            //BindDataMyHistory();
        }
    }

    private void BindData(int pgIndex = 0)
    {
        int reqid = int.Parse(Request.QueryString["RequestId"]);
        string ReportType = Request.QueryString["ReportType"];
        gvListLOI.DataSource = loiControllerr.report_loi_detail_getdata(txtSiteNo.Value, txtCPO.Value, reqid, ReportType);
        gvListLOI.PageIndex = pgIndex;
        gvListLOI.DataBind();
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

    protected void gvListLOI_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    private void DownloadFile(string filename, bool forcedownload)
    {
        string ext = Path.GetExtension(filename);
        string strFilename = (Path.GetFileName(filename));
        string type = "";
        if ((ext != ""))
        {
            switch (ext.ToLower())
            {
                case ".pdf":
                    type = "text/pdf";
                    break;
                case ".htm":
                    type = "text/HTML";
                    break;
                case ".txt":
                    type = "text/plain";
                    break;
                case ".tif":
                    type = "text/tif";
                    break;
                case ".doc":
                    break;
                case ".rtf":
                    type = "Application/msword";
                    break;
                case ".xls":
                    type = "text/xls";
                    break;
                case ".xlsx":
                    type = "text/xlsx";
                    break;
                case ".zip":
                    type = "application/zip";
                    break;
            }
        }

        if (forcedownload)
        {
            Response.AppendHeader("content-disposition", ("attachment; filename=" + strFilename));
        }

        if ((type != ""))
        {
            Response.WriteFile(filename);
            Response.End();
        }

    }

    protected void gvListLOI_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindData(e.NewPageIndex);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["ReportTypeSender"] = Request.QueryString["ReportType"];
        Response.Redirect("~/rpt/frmLOIReport.aspx");
    }

    protected void gvListLOI_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string ReportType = Request.QueryString["ReportType"];
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[10].Visible = ReportType == "LOI Done" || ReportType == "LOI Overdue";
            e.Row.Cells[11].Visible = ReportType == "LOI Done" || ReportType == "LOI Overdue";
            e.Row.Cells[12].Visible = ReportType != "LOI Open";
            e.Row.Cells[13].Visible = ReportType == "LOI Done" || ReportType == "LOI Overdue";
            e.Row.Cells[14].Visible = ReportType == "LOI Done" || ReportType == "LOI Overdue";
            e.Row.Cells[15].Visible = ReportType == "LOI Done" || ReportType == "LOI Overdue";
            e.Row.Cells[16].Visible = ReportType == "LOI Done" || ReportType == "LOI Overdue" || ReportType == "LOI Open";
            e.Row.Cells[17].Visible = ReportType == "LOI Done" || ReportType == "LOI Overdue";
            e.Row.Cells[18].Visible = ReportType == "LOI Done" || ReportType == "LOI Overdue" || ReportType == "LOI Open";
            e.Row.Cells[19].Visible = ReportType == "Approval Tracking";
            e.Row.Cells[20].Visible = ReportType == "Approval Tracking";
            e.Row.Cells[21].Visible = ReportType == "LOI Rejected";
            e.Row.Cells[22].Visible = ReportType == "LOI Rejected";
            e.Row.Cells[23].Visible = ReportType == "LOI Rejected";
            e.Row.Cells[24].Visible = ReportType == "LOI Cancelled";
            e.Row.Cells[25].Visible = ReportType == "LOI Cancelled";
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[10].Visible = ReportType == "LOI Done" || ReportType == "LOI Overdue";
            e.Row.Cells[11].Visible = ReportType == "LOI Done" || ReportType == "LOI Overdue";
            e.Row.Cells[12].Visible = ReportType != "LOI Open";
            e.Row.Cells[13].Visible = ReportType == "LOI Done" || ReportType == "LOI Overdue";
            e.Row.Cells[14].Visible = ReportType == "LOI Done" || ReportType == "LOI Overdue";
            e.Row.Cells[15].Visible = ReportType == "LOI Done" || ReportType == "LOI Overdue";
            e.Row.Cells[16].Visible = ReportType == "LOI Done" || ReportType == "LOI Overdue" || ReportType == "LOI Open";
            e.Row.Cells[17].Visible = ReportType == "LOI Done" || ReportType == "LOI Overdue";
            e.Row.Cells[18].Visible = ReportType == "LOI Done" || ReportType == "LOI Overdue" || ReportType == "LOI Open";
            e.Row.Cells[19].Visible = ReportType == "Approval Tracking";
            e.Row.Cells[20].Visible = ReportType == "Approval Tracking";
            e.Row.Cells[21].Visible = ReportType == "LOI Rejected";
            e.Row.Cells[22].Visible = ReportType == "LOI Rejected";
            e.Row.Cells[23].Visible = ReportType == "LOI Rejected";
            e.Row.Cells[24].Visible = ReportType == "LOI Cancelled";
            e.Row.Cells[25].Visible = ReportType == "LOI Cancelled";

        }
    }

    protected void gvLog_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindDataLog(e.NewPageIndex);
    }

    

    private void BindDataLog(int pgIndex = 0)
    {
        int reqid = int.Parse(Request.QueryString["RequestId"]);
        gvLog.DataSource = loiControllerr.getloilog_byrequestid(reqid);
        gvLog.DataBind();
    }    
}