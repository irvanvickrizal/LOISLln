using eLoi.Controller;
using ClosedXML.Excel;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using System.Transactions;

public partial class Dashboard_frmUploadLOIRejected : System.Web.UI.Page
{
    LOIController loiControllerr = new LOIController();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnDownloadTemplate_Click(object sender, EventArgs e)
    {
        string filenameSource = Server.MapPath("~/template_file/Template_Upload_FM_Error.xlsx");

        string TempPath = string.Concat("~/TemporaryFile/", DateTime.Now.ToString("ddMMyyyy"), ContentSession.FULLNAME, "/");
        TempPath = Server.MapPath(TempPath);
        if (!Directory.Exists(TempPath)) Directory.CreateDirectory(TempPath);

        string filename = string.Concat(TempPath, "Template LOI Rejected.xlsx");
        try
        {
            File.Copy(filenameSource, filename, true);
            int RequestId = int.Parse(Request.QueryString["RequestId"]);
            DataTable dt = loiControllerr.LOI_Detail_by_RequestId(RequestId);
            DataTable dtSCon = loiControllerr.SubconName_getList();
            DataTable dtSOW = loiControllerr.master_sow_getList_detail();
            DataTable dtRegion = loiControllerr.get_region_by_userid();
            insertTemplateFileValue(filenameSource, filename, dt, dtSCon, dtSOW, dtRegion);
            System.IO.FileInfo file = new System.IO.FileInfo(filename);
            if (file.Exists)
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(file.FullName);
                Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                Response.Write("This file does not exist.");
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to download template');", true);
        }
        finally
        {
            if (File.Exists(filename))
                File.Delete(filename);
            if (Directory.Exists(TempPath))
                Directory.Delete(TempPath);
        }
    }

    protected void btnUploadFile_Click(object sender, EventArgs e)
    {
        string path = Server.MapPath("~/TemporaryFile/");
        try
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            path += uploadFile.FileName;
            uploadFile.SaveAs(path);
            DataTable dtLOI = loiControllerr.initiateLOITable();
            //dtLOI.Columns.Add("ErrorRemarks");
            int successuploadedcount = 0;
            if (readLOIExcelFile(dtLOI, path))
            {
                if (dtLOI.Rows.Count > 0)
                {
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                    {
                        try
                        {
                            int errorcount = 0;
                            int Subcon_Id = loiControllerr.Validate_SubconName(dtLOI.Rows[0]["Subcone_Name"].ToString());
                            int scope_type_id = loiControllerr.Validate_sowdetail(dtLOI.Rows[0]["ScopeOfWork"].ToString());
                            int RequestId = int.Parse(Request.QueryString["RequestId"]);
                            if (loiControllerr.LOI_UploadRejected_by_FM(RequestId))
                            {
                                foreach (DataRow dr in dtLOI.Rows)
                                {
                                    int RegionId = 0;
                                    int ARA_ID = 0;
                                    DataTable dtRegion = loiControllerr.Validate_Region(dr["Region"].ToString());
                                    if (dtRegion.Rows.Count > 0)
                                    {
                                        RegionId = Convert.ToInt32(dtRegion.Rows[0]["RGN_ID"]);
                                        ARA_ID = Convert.ToInt32(dtRegion.Rows[0]["ARA_ID"]);
                                    }
                                    string ErrorRemarks = ValidateLOIData(dr, RequestId, Subcon_Id, RegionId, scope_type_id);
                                    if (string.IsNullOrEmpty(ErrorRemarks))
                                    {
                                        if (!loiControllerr.LOI_Detail_Upload_by_FM(dr, RequestId, RegionId, ARA_ID))
                                        {
                                            errorcount += 1;
                                            dr["Remarks"] = "Failed when upload data to db";
                                        }
                                    }
                                    else
                                    {
                                        errorcount += 1;
                                        dr["Remarks"] = ErrorRemarks;
                                    }
                                }
                            }
                            else
                            {
                                errorcount += 1;
                            }

                            lblErrorUpload.Visible = false;
                            Session["dtLOIError"] = null;
                            if (errorcount == 0)
                            {
                                if(loiControllerr.update_loi_price_fromTemp(RequestId))
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
                                        scope.Complete();
                                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('File completely uploaded');location.href = '../dashboard/frmDashboardFM.aspx';", true);
                                    }
                                }
                                else
                                {
                                    scope.Dispose();
                                    Session["dtLOIError"] = dtLOI;
                                    lblErrorUpload.Text = "Total Failed: " + errorcount.ToString();
                                    lblErrorUpload.Visible = true;
                                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('upload data failed');", true);
                                }

                            }
                            else
                            {
                                scope.Dispose();
                                Session["dtLOIError"] = dtLOI;
                                lblErrorUpload.Text = "Total Failed: " + errorcount.ToString();
                                lblErrorUpload.Visible = true;
                                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('upload data failed');", true);
                            }
                        }
                        catch
                        {
                            scope.Dispose();
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('upload data failed');", true);
                        }
                    }
                }
                else
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('There is no data to be uploaded');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to read excel file');", true);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to upload data');", true);
        }
        finally
        {
            if (File.Exists(path))
                File.Delete(path);
        }
    }

    public bool readLOIExcelFile(DataTable dtLOI, string path)
    {
        bool result = true;
        int totalRow = 0;
        int totalColumn = 0;
        DataRow dr = null;

        try
        {
            using (XLWorkbook workBook = new XLWorkbook(path))
            {
                IXLWorksheet workSheet = workBook.Worksheet(1);
                totalRow = workSheet.RangeUsed().RowCount();
                totalColumn = workSheet.RangeUsed().ColumnCount();

                for (int row = 3; row <= totalRow; row++)
                {
                    dr = dtLOI.NewRow();
                    string workpackageid = workSheet.Row(row).Cell(2).Value.ToString();
                    string Customer_PO = workSheet.Row(row).Cell(3).Value.ToString();
                    string strCustomer_PO_Date = workSheet.Row(row).Cell(4).Value.ToString();
                    string PO_Description = workSheet.Row(row).Cell(5).Value.ToString();
                    string Region = workSheet.Row(row).Cell(6).Value.ToString();
                    string Site_ID = workSheet.Row(row).Cell(7).Value.ToString();
                    string ScopeOfWork = workSheet.Row(row).Cell(8).Value.ToString();
                    string Subcone_Name = workSheet.Row(row).Cell(9).Value.ToString();
                    string Site_Model = workSheet.Row(row).Cell(10).Value.ToString();
                    string Unit_Price = workSheet.Row(row).Cell(11).Value.ToString();
                    string strQty = workSheet.Row(row).Cell(12).Value.ToString();
                    string strTotalPrice = workSheet.Row(row).Cell(13).Value.ToString();
                    string strClosingPlanDate = workSheet.Row(row).Cell(14).Value.ToString();

                    DateTime Customer_PO_Date;
                    DateTime.TryParse(strCustomer_PO_Date, out Customer_PO_Date);

                    DateTime ClosingPlanDate;
                    DateTime.TryParse(strClosingPlanDate, out ClosingPlanDate);

                    int Qty = 0;
                    int.TryParse(strQty, out Qty);

                    decimal TotalPrice = 0;
                    decimal.TryParse(strTotalPrice, out TotalPrice);

                    dr[0] = workpackageid;
                    dr[1] = Customer_PO;
                    dr[2] = Customer_PO_Date;
                    dr[3] = PO_Description;
                    dr[4] = Region;
                    dr[5] = Site_ID;
                    dr[6] = ScopeOfWork;
                    dr[7] = Subcone_Name;
                    dr[8] = Site_Model;
                    dr[9] = Unit_Price;
                    dr[10] = Qty;
                    dr[11] = TotalPrice;
                    dr[12] = ClosingPlanDate;

                    dtLOI.Rows.Add(dr);
                }
            }
        }
        catch (Exception e)
        {
            result = false;
        }
        finally
        {
        }
        return result;
    }

    public bool insertTemplateFileValue(string path, string newpath, DataTable dt, DataTable dtSCON, DataTable dtSOW, DataTable dtRegion)
    {
        bool result = false;
        try
        {
            using (XLWorkbook workBook = new XLWorkbook(path))
            {
                IXLWorksheet workSheet = workBook.Worksheet(1);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    workSheet.Cell(i + 3, 1).Value = (i + 1).ToString();
                    workSheet.Cell(i + 3, 2).Value = dt.Rows[i]["workpackageid"].ToString();
                    workSheet.Cell(i + 3, 3).Value = dt.Rows[i]["Customer_PO"].ToString();
                    workSheet.Cell(i + 3, 4).Value = dt.Rows[i]["Customer_PO_Date"].ToString();
                    workSheet.Cell(i + 3, 5).Value = dt.Rows[i]["PO_Description"].ToString();
                    workSheet.Cell(i + 3, 6).Value = dt.Rows[i]["Region"].ToString();
                    workSheet.Cell(i + 3, 7).Value = dt.Rows[i]["Site_ID"].ToString();
                    workSheet.Cell(i + 3, 8).Value = dt.Rows[i]["ScopeOfWork"].ToString();
                    workSheet.Cell(i + 3, 9).Value = dt.Rows[i]["Subcone_Name"].ToString();
                    workSheet.Cell(i + 3, 10).Value = dt.Rows[i]["Site_Model"].ToString();
                    workSheet.Cell(i + 3, 11).Value = dt.Rows[i]["Remarks"].ToString();
                    workSheet.Range("K3:K" + (i + 3).ToString()).Merge();
                    workSheet.Range("K3:K" + (i + 3).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    workSheet.Range("K3:K" + (i + 3).ToString()).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                }

                IXLWorksheet workSheetScon = workBook.Worksheet(2);
                for (int i = 0; i < dtSCON.Rows.Count; i++)
                {
                    workSheetScon.Cell(i + 2, 1).Value = dtSCON.Rows[i]["SCon_Name"].ToString();
                }

                IXLWorksheet workSheetSOW = workBook.Worksheet(3);
                string merge_start = "";
                string merge_finish = "";
                for (int i = 0; i < dtSOW.Rows.Count; i++)
                {
                    workSheetSOW.Cell(i + 2, 1).Value = dtSOW.Rows[i]["general_sow"].ToString();
                    workSheetSOW.Cell(i + 2, 2).Value = dtSOW.Rows[i]["detail_sow"].ToString();

                    if (i > 0)
                    {
                        if (dtSOW.Rows[i]["general_sow"].ToString() == dtSOW.Rows[i - 1]["general_sow"].ToString())
                        {
                            merge_start = string.IsNullOrEmpty(merge_start) ? "A" + (i + 1).ToString() : merge_start;
                            merge_finish = ":A" + ((i + 2).ToString());
                            workSheetSOW.Range(merge_start + merge_finish).Merge();
                            workSheetSOW.Range(merge_start + merge_finish).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        }
                        else
                        {
                            merge_start = "";
                        }
                    }
                }

                IXLWorksheet workSheetRegion = workBook.Worksheet(4);
                for (int i = 0; i < dtRegion.Rows.Count; i++)
                {
                    workSheetRegion.Cell(i + 2, 1).Value = dtRegion.Rows[i]["RGNName"].ToString();
                }
                workBook.SaveAs(newpath);
                result = true;
            }
        }
        catch (Exception e)
        {

        }
        finally
        {
        }
        return result;
    }

    protected void lblErrorUpload_Click(object sender, EventArgs e)
    {
        string filenameSource = Server.MapPath("~/template_file/Template_Upload_FM_Error.xlsx");

        string TempPath = string.Concat("~/TemporaryFile/", DateTime.Now.ToString("ddMMyyyy"), ContentSession.FULLNAME, "/");
        TempPath = Server.MapPath(TempPath);
        if (!Directory.Exists(TempPath)) Directory.CreateDirectory(TempPath);

        string filename = string.Concat(TempPath, "List_Error.xlsx");
        try
        {
            File.Copy(filenameSource, filename, true);

            DataTable dt = (DataTable)Session["dtLOIPriceError"];
            DataTable dtSCon = loiControllerr.SubconName_getList();
            DataTable dtSOW = loiControllerr.master_sow_getList_detail();
            DataTable dtRegion = loiControllerr.get_region_by_userid();
            insertTemplateFileErrorValue(filenameSource, filename, dt, dtSCon, dtSOW, dtRegion);
            System.IO.FileInfo file = new System.IO.FileInfo(filename);
            if (file.Exists)
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(file.FullName);
                Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                Response.Write("This file does not exist.");
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to download error data');", true);
        }
        finally
        {
            if (File.Exists(filename))
                File.Delete(filename);
            if (Directory.Exists(TempPath))
                Directory.Delete(TempPath);
        }
    }

    public bool insertTemplateFileErrorValue(string path, string newpath, DataTable dt, DataTable dtSCon, DataTable dtSOW, DataTable dtRegion)
    {
        bool result = false;
        try
        {
            using (XLWorkbook workBook = new XLWorkbook(path))
            {
                IXLWorksheet workSheet = workBook.Worksheet(1);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    workSheet.Cell(i + 3, 1).Value = (i + 1).ToString();
                    workSheet.Cell(i + 3, 2).Value = dt.Rows[i]["workpackageid"].ToString();
                    workSheet.Cell(i + 3, 3).Value = dt.Rows[i]["Customer_PO"].ToString();
                    workSheet.Cell(i + 3, 4).Value = dt.Rows[i]["Customer_PO_Date"].ToString();
                    workSheet.Cell(i + 3, 5).Value = dt.Rows[i]["PO_Description"].ToString();
                    workSheet.Cell(i + 3, 6).Value = dt.Rows[i]["Region"].ToString();
                    workSheet.Cell(i + 3, 7).Value = dt.Rows[i]["Site_ID"].ToString();
                    workSheet.Cell(i + 3, 8).Value = dt.Rows[i]["ScopeOfWork"].ToString();
                    workSheet.Cell(i + 3, 9).Value = dt.Rows[i]["Subcone_Name"].ToString();
                    workSheet.Cell(i + 3, 10).Value = dt.Rows[i]["Site_Model"].ToString();
                    workSheet.Cell(i + 3, 11).Value = dt.Rows[i]["Remarks"].ToString();
                }

                IXLWorksheet workSheetSCon = workBook.Worksheet(2);
                for (int i = 0; i < dtSCon.Rows.Count; i++)
                {
                    workSheetSCon.Cell(i + 2, 1).Value = dtSCon.Rows[i]["SCon_Name"].ToString();
                }

                IXLWorksheet workSheetSOW = workBook.Worksheet(3);
                string merge_start = "";
                string merge_finish = "";
                for (int i = 0; i < dtSOW.Rows.Count; i++)
                {
                    workSheetSOW.Cell(i + 2, 1).Value = dtSOW.Rows[i]["general_sow"].ToString();
                    workSheetSOW.Cell(i + 2, 2).Value = dtSOW.Rows[i]["detail_sow"].ToString();

                    if (i > 0)
                    {
                        if (dtSOW.Rows[i]["general_sow"].ToString() == dtSOW.Rows[i - 1]["general_sow"].ToString())
                        {
                            merge_start = string.IsNullOrEmpty(merge_start) ? "A" + (i + 1).ToString() : merge_start;
                            merge_finish = ":A" + ((i + 2).ToString());
                            workSheetSOW.Range(merge_start + merge_finish).Merge();
                            workSheetSOW.Range(merge_start + merge_finish).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        }
                        else
                        {
                            merge_start = "";
                        }
                    }
                }

                IXLWorksheet workSheetRegion = workBook.Worksheet(4);
                for (int i = 0; i < dtRegion.Rows.Count; i++)
                {
                    workSheetRegion.Cell(i + 2, 1).Value = dtRegion.Rows[i]["RGNName"].ToString();
                }

                workBook.SaveAs(newpath);
                result = true;
            }
        }
        catch (Exception e)
        {

        }
        finally
        {
        }
        return result;
    }

    private string ValidateLOIData(DataRow dr, int reqid, int Subcon_Id, int RegionId, int scope_type_id)
    {
        string ErrorMessage = string.Empty;

        if (string.IsNullOrEmpty(dr["workpackageid"].ToString()))
            ErrorMessage += "workpackageid is empty; ";

        if (string.IsNullOrEmpty(dr["Customer_PO"].ToString()))
            ErrorMessage += "Customer PO is empty; ";

        if (string.IsNullOrEmpty(dr["PO_Description"].ToString()))
            ErrorMessage += "PO Description is empty; ";

        if (string.IsNullOrEmpty(dr["Region"].ToString()))
            ErrorMessage += "Region is empty; ";

        if (string.IsNullOrEmpty(dr["Site_ID"].ToString()))
            ErrorMessage += "Site ID is empty; ";

        if (string.IsNullOrEmpty(dr["ScopeOfWork"].ToString()))
            ErrorMessage += "Scope Of Work is empty; ";

        if (string.IsNullOrEmpty(dr["Subcone_Name"].ToString()))
            ErrorMessage += "Subcone Name is empty; ";

        if (string.IsNullOrEmpty(dr["Site_Model"].ToString()))
            ErrorMessage += "Site Model is empty; ";

        if (Convert.ToDateTime(dr["Customer_PO_Date"]) == new DateTime())
            ErrorMessage += "Customer PO Date is not valid; ";

        if (Subcon_Id == 0)
            ErrorMessage += "Subcon is not registered; ";

        if (RegionId == 0)
            ErrorMessage += "Region is not registered; ";

        //if (loiControllerr.CheckLOI_Upload_by_FM(dr) == 1)
        //    ErrorMessage += "LOI already exists; ";

        //if (loiControllerr.Validate_SubconName(dr["Subcone_Name"].ToString()) == 0)
        //    ErrorMessage += "Subcon is not registered; ";

        //if (!loiControllerr.Validate_sowdetail(dr["ScopeOfWork"].ToString()))
        //    ErrorMessage += "Scope of Work is not valid registerd; ";

        if (scope_type_id == 0)
            ErrorMessage += "Scope of Work is not valid registerd; ";

        //if (loiControllerr.LOIDetail_Checking_duplicate_sow_siteid_byReqno(reqid, dr["Site_ID"].ToString(), dr["ScopeOfWork"].ToString()))
        //    ErrorMessage += "Scope of Work duplicate in same siteid; ";

        return ErrorMessage;
    }

    private void SendEmail(int ReqId)
    {
        DataTable dtReceiverMail = loiControllerr.loi_get_receivermail_workflow(ReqId);
        string htmltabledetail = string.Empty;
        DataTable dt = loiControllerr.LOI_Detail_getEmailData(ReqId);
        htmltabledetail = CreateHtmlTableApproved(dt);
        DataTable dtProcCM = loiControllerr.getProcCM_Email();
        if (dtReceiverMail.Rows.Count > 0)
        {
            string ReceiverMail = string.Join(",", dtReceiverMail.AsEnumerable()
                                  .Select(s => s.Field<string>("Email")).ToArray());
            string ReceiverName = string.Join(",", dtReceiverMail.AsEnumerable()
                                  .Select(s => s.Field<string>("Name")).ToArray());
            ReceiverMail += "," + string.Join(",", dtProcCM.AsEnumerable()
                                 .Select(s => s.Field<string>("Email")).ToArray());
            ReceiverName += "," + string.Join(",", dtProcCM.AsEnumerable()
                                  .Select(s => s.Field<string>("Name")).ToArray());
            loiControllerr.sendMail(ReceiverName, ReceiverMail, htmltabledetail, "Uploaded", ContentSession.FULLNAME, ReqId);

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
                    </tr>";
    }
}