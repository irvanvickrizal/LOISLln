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

public partial class rpt_frmRPTLOIRejection : System.Web.UI.Page
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
        gvListLOI.DataSource = loiControllerr.report_loi_rejection_getdata(subconid, periodfrom, periodto);
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
        DataTable dtResult = loiControllerr.report_loi_rejection_detail_getdata(subconid, periodfrom, periodto, "", "", 0);
        if (dtResult.Rows.Count > 0)
            ExportToExcel("LOI_Rejection_" + string.Format("{0:ddMMMyyy}", DateTime.Now), dtResult, GeneralConfig.Def_sheetname);
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

    protected void gvListLOI_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("viewDetail"))
        {
            Response.Redirect("~/rpt/frmRPTLOIRejectionDetail.aspx?RequestId=" + e.CommandArgument.ToString());
        }
    }
}