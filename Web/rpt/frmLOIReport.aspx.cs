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

public partial class rpt_frmLOIReport : System.Web.UI.Page
{
    LOIController loiControllerr = new LOIController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["ReportTypeSender"] != null)
            {
                ddlReportType.SelectedValue = Session["ReportTypeSender"].ToString();
                Session["ReportTypeSender"] = null;
            }
            BindSubcon();
            BindData();
            BindDataMyHistory();            
        }
    }

    private void BindData(int pgIndex = 0)
    {
        int subconid = int.Parse(DdlSubcon.SelectedValue);
        string periodfrom = txtperiodfrom.Value;
        string periodto = txtperiodto.Value;
        gvListLOI.DataSource = loiControllerr.report_loi_getdata(subconid, periodfrom, periodto, ddlReportType.SelectedValue);
        gvListLOI.PageIndex = pgIndex;
        gvListLOI.DataBind();
        //ColumnShowSelection();
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
        int subconid = int.Parse(DdlSubcon.SelectedValue);
        string periodfrom = txtperiodfrom.Value;
        string periodto = txtperiodto.Value;
        DataTable dtResult = loiControllerr.report_loi_detail_getdata(string.Empty, string.Empty, 0, ddlReportType.SelectedValue);
        //DataTable dtResult = new DataTable();
        //switch (ddlReportType.SelectedValue)
        //{
        //    case "Approval Tracking":
        //        dtResult = loiControllerr.report_loi_approvaltracking_detail_getdata(subconid, periodfrom, periodto, "", "", 0);
        //        break;
        //    case "LOI Rejected":
        //        dtResult = loiControllerr.report_loi_rejection_detail_getdata(subconid, periodfrom, periodto, "", "", 0);
        //        break;
        //    case "LOI Open":
        //        dtResult = loiControllerr.report_loi_open_detail_getdata(subconid, periodfrom, periodto, "", "", 0);
        //        break;
        //    case "LOI Overdue":
        //        dtResult = loiControllerr.report_loi_overdue_detail_getdata(subconid, periodfrom, periodto, "", "", 0);
        //        break;
        //    case "LOI Cancelled":
        //        dtResult = loiControllerr.report_loi_rejection_detail_getdata(subconid, periodfrom, periodto, "", "", 0);
        //        break;
        //    case "LOI Done":
        //        dtResult = loiControllerr.report_loi_done_detail_getdata(subconid, periodfrom, periodto, "", "", 0);
        //        break;
        //}        
        if (dtResult.Rows.Count > 0)
            ExportToExcel(ddlReportType.SelectedValue + string.Format("{0:ddMMMyyy}", DateTime.Now), dtResult, GeneralConfig.Def_sheetname);
    }

    protected void gvListLOI_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("downloadPDF"))
        {
            //string path = ConfigurationManager.AppSettings["DirectoryResultPath"] + e.CommandArgument.ToString();
            string path = e.CommandArgument.ToString();
            if(string.IsNullOrEmpty(path))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(UpdatePanel), "Script", "alert(\'No File Exists With This Site No\');", true);
                return;
            }
            //string pathdetail = ConfigurationManager.AppSettings["DirectoryResultPath"] + e.CommandArgument.ToString().Replace("LOIDoc", "LOIDoc_Detail");
            //path = path.Replace("https://eloinokia.nsnebast.com/", @"D:\\");
            if(!File.Exists(path))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(UpdatePanel), "Script", "alert(\'No File Exists With This Site No\');", true);
                return;
            }
            DownloadFile(path, true);
            //DownloadFile(pathdetail, true);



            //using (System.Net.WebClient Client = new System.Net.WebClient())
            //{
            //    Client.DownloadFile(path, "LoiDocument.pdf");
            //}
        }
        else if (e.CommandName.Equals("viewDetail"))
        {
            Response.Redirect("~/rpt/frmLOIReportDetail.aspx?RequestId=" + e.CommandArgument.ToString() + "&ReportType=" + ddlReportType.SelectedValue);
        }
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


    protected void imgDownloadCert_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnk = ((ImageButton)(sender));
        GridViewRow drow = ((GridViewRow)(lnk.NamingContainer));
        int i;
        i = drow.RowIndex;
        string strPath = lnk.CommandArgument;
        string fpath = strPath;
        if ((strPath != ""))
        {
            if (File.Exists(fpath))
            {
                DownloadFile(fpath, true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(UpdatePanel), "Script", "alert(\'No File Exists With This Site No\');", true);
            }

        }
        else
        {
            lnk.Enabled = false;
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

    private void BindSubcon()
    {
        DdlSubcon.DataSource = loiControllerr.subcon_get_all_byctName();
        DdlSubcon.DataTextField = "SCon_Name";
        DdlSubcon.DataValueField = "SCon_Id";
        DdlSubcon.DataBind();
        DdlSubcon.Items.Insert(0, new ListItem("All Subcon", "0"));
    }

    private void ColumnShowSelection()
    {
        //gvListLOI.Columns[2].Visible = ddlReportType.SelectedValue == "LOI Done" || ddlReportType.SelectedValue == "LOI Overdue";
        //gvListLOI.Columns[3].Visible = ddlReportType.SelectedValue == "LOI Done" || ddlReportType.SelectedValue == "LOI Overdue";
        //gvListLOI.Columns[8].Visible = ddlReportType.SelectedValue != "LOI Open";
        //gvListLOI.Columns[9].Visible = ddlReportType.SelectedValue == "LOI Rejected";
        //gvListLOI.Columns[10].Visible = ddlReportType.SelectedValue == "LOI Rejected";
        //gvListLOI.Columns[11].Visible = ddlReportType.SelectedValue == "LOI Rejected";
        //gvListLOI.Columns[12].Visible = ddlReportType.SelectedValue != "LOI Open" && ddlReportType.SelectedValue != "LOI Rejected";
        //gvListLOI.Columns[14].Visible = ddlReportType.SelectedValue != "Approval Tracking" && ddlReportType.SelectedValue != "LOI Rejected" && ddlReportType.SelectedValue != "LOI Cancelled";
        //gvListLOI.Columns[15].Visible = ddlReportType.SelectedValue == "LOI Done" || ddlReportType.SelectedValue == "LOI Overdue";
        //gvListLOI.Columns[16].Visible = ddlReportType.SelectedValue != "LOI Done" && ddlReportType.SelectedValue != "LOI Rejected" && ddlReportType.SelectedValue != "LOI Cancelled";
        //gvListLOI.Columns[17].Visible = ddlReportType.SelectedValue == "Approval Tracking";
        //gvListLOI.Columns[18].Visible = ddlReportType.SelectedValue == "Approval Tracking";
        //gvListLOI.Columns[19].Visible = ddlReportType.SelectedValue == "LOI Done";
        //gvListLOI.Columns[20].Visible = ddlReportType.SelectedValue == "LOI Cancelled";
        //gvListLOI.Columns[21].Visible = ddlReportType.SelectedValue == "LOI Cancelled";
    }

    protected void gvListLOI_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[2].Visible = ddlReportType.SelectedValue == "LOI Done" || ddlReportType.SelectedValue == "LOI Overdue";
            e.Row.Cells[3].Visible = ddlReportType.SelectedValue == "LOI Done" || ddlReportType.SelectedValue == "LOI Overdue";
            e.Row.Cells[8].Visible = ddlReportType.SelectedValue != "LOI Open";
            e.Row.Cells[9].Visible = ddlReportType.SelectedValue == "LOI Rejected";
            e.Row.Cells[10].Visible = ddlReportType.SelectedValue == "LOI Rejected";
            e.Row.Cells[11].Visible = ddlReportType.SelectedValue == "LOI Rejected";
            e.Row.Cells[12].Visible = ddlReportType.SelectedValue != "LOI Open" && ddlReportType.SelectedValue != "LOI Rejected";
            e.Row.Cells[14].Visible = ddlReportType.SelectedValue != "Approval Tracking" && ddlReportType.SelectedValue != "LOI Rejected" && ddlReportType.SelectedValue != "LOI Cancelled";
            e.Row.Cells[15].Visible = ddlReportType.SelectedValue == "LOI Done" || ddlReportType.SelectedValue == "LOI Overdue";
            e.Row.Cells[16].Visible = ddlReportType.SelectedValue != "LOI Done" && ddlReportType.SelectedValue != "LOI Rejected" && ddlReportType.SelectedValue != "LOI Cancelled";
            e.Row.Cells[17].Visible = ddlReportType.SelectedValue == "Approval Tracking";
            e.Row.Cells[18].Visible = ddlReportType.SelectedValue == "Approval Tracking";
            e.Row.Cells[19].Visible = ddlReportType.SelectedValue == "LOI Done";
            e.Row.Cells[20].Visible = ddlReportType.SelectedValue == "LOI Cancelled";
            e.Row.Cells[21].Visible = ddlReportType.SelectedValue == "LOI Cancelled";
            //e.Row.Cells[22].Visible = ddlReportType.SelectedValue != "LOI Done";

        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Visible = ddlReportType.SelectedValue == "LOI Done" || ddlReportType.SelectedValue == "LOI Overdue";
            e.Row.Cells[3].Visible = ddlReportType.SelectedValue == "LOI Done" || ddlReportType.SelectedValue == "LOI Overdue";
            e.Row.Cells[8].Visible = ddlReportType.SelectedValue != "LOI Open";
            e.Row.Cells[9].Visible = ddlReportType.SelectedValue == "LOI Rejected";
            e.Row.Cells[10].Visible = ddlReportType.SelectedValue == "LOI Rejected";
            e.Row.Cells[11].Visible = ddlReportType.SelectedValue == "LOI Rejected";
            e.Row.Cells[12].Visible = ddlReportType.SelectedValue != "LOI Open" && ddlReportType.SelectedValue != "LOI Rejected";
            e.Row.Cells[14].Visible = ddlReportType.SelectedValue != "Approval Tracking" && ddlReportType.SelectedValue != "LOI Rejected" && ddlReportType.SelectedValue != "LOI Cancelled";
            e.Row.Cells[15].Visible = ddlReportType.SelectedValue == "LOI Done" || ddlReportType.SelectedValue == "LOI Overdue";
            e.Row.Cells[16].Visible = ddlReportType.SelectedValue != "LOI Done" && ddlReportType.SelectedValue != "LOI Rejected" && ddlReportType.SelectedValue != "LOI Cancelled";
            e.Row.Cells[17].Visible = ddlReportType.SelectedValue == "Approval Tracking";
            e.Row.Cells[18].Visible = ddlReportType.SelectedValue == "Approval Tracking";
            e.Row.Cells[19].Visible = ddlReportType.SelectedValue == "LOI Done";
            e.Row.Cells[20].Visible = ddlReportType.SelectedValue == "LOI Cancelled";
            e.Row.Cells[21].Visible = ddlReportType.SelectedValue == "LOI Cancelled";
            //e.Row.Cells[22].Visible = ddlReportType.SelectedValue != "LOI Done";

        }
    }

    private void BindDataMyHistory(int pgIndex = 0)
    {
        gvhistory.DataSource = loiControllerr.getloihistory_byusrid();
        gvhistory.DataBind();
    }

    protected void gvhistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindDataMyHistory(e.NewPageIndex);
    }
}