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

public partial class rpt_frmRPTLOIOverdue : System.Web.UI.Page
{
    LOIController loiControllerr = new LOIController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindSubcon();
            BindData();
        }
    }

    private void BindData(int pgIndex = 0)
    {
        int subconid = int.Parse(DdlSubcon.SelectedValue);
        string periodfrom = txtperiodfrom.Value;
        string periodto = txtperiodto.Value;
        gvListLOI.DataSource = loiControllerr.usp_report_loi_overdue_getdata(subconid, periodfrom, periodto);
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


    protected void btnDownloadList_Click(object sender, EventArgs e)
    {
        int subconid = int.Parse(DdlSubcon.SelectedValue);
        string periodfrom = txtperiodfrom.Value;
        string periodto = txtperiodto.Value;
        DataTable dtResult = loiControllerr.report_loi_overdue_detail_getdata(subconid, periodfrom, periodto, "", "", 0);
        if (dtResult.Rows.Count > 0)
            ExportToExcel("LOI_Overdue_" + string.Format("{0:ddMMMyyy}", DateTime.Now), dtResult, GeneralConfig.Def_sheetname);
    }

    protected void gvListLOI_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("viewDetail"))
        {
            Response.Redirect("~/rpt/frmRPTLOIOverdueDetail.aspx?RequestId=" + e.CommandArgument.ToString());
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
}