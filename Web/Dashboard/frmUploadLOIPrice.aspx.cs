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

public partial class Dashboard_frmUploadLOIPrice : System.Web.UI.Page
{
    LOIController loiControllerr = new LOIController();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnDownloadTemplate_Click(object sender, EventArgs e)
    {
        string filenameSource = Server.MapPath("~/template_file/Template_Upload_PCM.xlsx");

        string TempPath = string.Concat("~/TemporaryFile/", DateTime.Now.ToString("ddMMyyyy"), ContentSession.FULLNAME, "/");
        TempPath = Server.MapPath(TempPath);
        if (!Directory.Exists(TempPath)) Directory.CreateDirectory(TempPath);

        string filename = string.Concat(TempPath, "Template_Upload_PCM.xlsx");
        try
        {
            File.Copy(filenameSource, filename, true);
            int RequestId = int.Parse(Request.QueryString["RequestId"]);
            DataTable dt = loiControllerr.LOI_Detail_by_RequestId(RequestId);
            insertTemplateFileValue(filenameSource, filename, dt);
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
                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                            bool isSuccess = false;
                            int RequestId = int.Parse(Request.QueryString["RequestId"]);
                            int errorcount = 0;
                            DateTime PlanClossingDate = new DateTime();
                            int scope_type_id = loiControllerr.Validate_sowdetail(dtLOI.Rows[0]["ScopeOfWork"].ToString());
                            foreach (DataRow dr in dtLOI.Rows)
                            {                               

                                string ErrorRemarks = ValidateLOIData(dr, RequestId, scope_type_id);
                                if (string.IsNullOrEmpty(ErrorRemarks))
                                {
                                    PlanClossingDate = Convert.ToDateTime(dr["Closing_Plan_Date"]);
                                    if (!loiControllerr.LOI_Update_Price(dr, RequestId))
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
                                //    isSuccess = loiControllerr.LOI_Update_Price(dr, RequestId);
                                //if (!isSuccess)
                                //    break;
                            }
                            lblErrorUpload.Visible = false;
                            Session["dtLOIPriceError"] = null;
                            if (errorcount == 0)
                            {
                                if (loiControllerr.LOI_Update_status_uploadPrice(RequestId, PlanClossingDate))
                                {
                                    if (loiControllerr.change_loigroup_by_maxtotalprice(RequestId))
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
                                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('price hase been uploaded successfully');location.href = '../dashboard/frmDashboardPCM.aspx';", true);
                                        }
                                    }
                                    else
                                    {
                                        scope.Dispose();
                                        Session["dtLOIPriceError"] = dtLOI;
                                        lblErrorUpload.Text = "Total Failed: " + errorcount.ToString();
                                        lblErrorUpload.Visible = true;
                                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to upload data');", true);
                                    }
                                }
                                else
                                {
                                    scope.Dispose();
                                    Session["dtLOIPriceError"] = dtLOI;
                                    lblErrorUpload.Text = "Total Failed: " + errorcount.ToString();
                                    lblErrorUpload.Visible = true;
                                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to upload data');", true);
                                }
                            }
                            else
                            {
                                scope.Dispose();
                                Session["dtLOIPriceError"] = dtLOI;
                                lblErrorUpload.Text = "Total Failed: " + errorcount.ToString();
                                lblErrorUpload.Visible = true;
                                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to upload data');", true);
                            }
                            //if (isSuccess)
                            //{
                            //    scope.Complete();
                            //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('price hase been uploaded successfully');location.href = '../dashboard/frmDashboardPCM.aspx';", true);
                            //}
                            //else
                            //{
                            //    scope.Dispose();
                            //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to upload data');", true);
                            //}
                        }
                        catch (Exception ex)
                        {
                            scope.Dispose();
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to upload data');", true);
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
                    string strUnit_Price = workSheet.Row(row).Cell(11).Value.ToString();
                    string strQty = workSheet.Row(row).Cell(12).Value.ToString();
                    //string strTotalPrice = workSheet.Row(row).Cell(13).Value.ToString();
                    string Currency = workSheet.Row(row).Cell(13).Value.ToString();
                    string strClosingPlanDate = workSheet.Row(row).Cell(14).Value.ToString();

                    DateTime Customer_PO_Date;
                    DateTime.TryParse(strCustomer_PO_Date, out Customer_PO_Date);

                    DateTime ClosingPlanDate;
                    DateTime.TryParse(strClosingPlanDate, out ClosingPlanDate);

                    int Qty = 0;
                    int.TryParse(strQty, out Qty);

                    decimal Unit_Price = 0;
                    decimal.TryParse(strUnit_Price, out Unit_Price);

                    decimal TotalPrice = Unit_Price * Qty;
                    //decimal TotalPrice = 0;
                    //decimal.TryParse(strTotalPrice, out TotalPrice);

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
                    dr[14] = Currency;

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

    public bool insertTemplateFileValue(string path, string newpath, DataTable dt)
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
        string filenameSource = Server.MapPath("~/template_file/Template_Upload_PCM_Error.xlsx");

        string TempPath = string.Concat("~/TemporaryFile/", DateTime.Now.ToString("ddMMyyyy"), ContentSession.FULLNAME, "/");
        TempPath = Server.MapPath(TempPath);
        if (!Directory.Exists(TempPath)) Directory.CreateDirectory(TempPath);

        string filename = string.Concat(TempPath, "List_Error.xlsx");
        try
        {
            File.Copy(filenameSource, filename, true);

            DataTable dt = (DataTable)Session["dtLOIPriceError"];
            insertTemplateFileErrorValue(filenameSource, filename, dt);
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

    public bool insertTemplateFileErrorValue(string path, string newpath, DataTable dt)
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
                    workSheet.Cell(i + 3, 11).Value = dt.Rows[i]["Unit_Price"].ToString();
                    workSheet.Cell(i + 3, 12).Value = dt.Rows[i]["Qty"].ToString();
                    //workSheet.Cell(i + 3, 13).Value = dt.Rows[i]["Total_Price"].ToString();
                    workSheet.Cell(i + 3, 13).Value = dt.Rows[i]["Currency"].ToString();
                    workSheet.Cell(i + 3, 14).Value = dt.Rows[i]["Closing_Plan_Date"].ToString();
                    workSheet.Cell(i + 3, 15).Value = dt.Rows[i]["Remarks"].ToString();
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

    private string ValidateLOIData(DataRow dr, int reqid, int scope_type_id)
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

        if (string.IsNullOrEmpty(dr["Unit_Price"].ToString()))
            ErrorMessage += "Unit_Price is empty; ";

        if (Convert.ToInt32(dr["Qty"]) == 0)
            ErrorMessage += "Qty is not valid; ";

        //if (Convert.ToDecimal(dr["Total_Price"]) == 0)
        //    ErrorMessage += "Total Price is not valid; ";
        
        if (Convert.ToDecimal(dr["Unit_Price"]) == 0)
            ErrorMessage += "Unit Price is not valid; ";

        if (Convert.ToDateTime(dr["Closing_Plan_Date"]) == new DateTime())
            ErrorMessage += "Closing Plan Date is not valid; ";

        if (Convert.ToDateTime(dr["Customer_PO_Date"]) == new DateTime())
            ErrorMessage += "Customer PO Date is not valid; ";

        //if (Convert.ToDecimal(dr["Total_Price"]) > 500000000)
        //    ErrorMessage += "Total price more than 500.000.000; ";

        decimal maxtotalprice = loiControllerr.get_max_totalprice();
        if (maxtotalprice < 1)
        {
            ErrorMessage += "Max Total price is not valid; ";
        }
        else
        {
            if (Convert.ToDecimal(dr["Total_Price"]) > maxtotalprice)
                ErrorMessage += "Total price more than " + maxtotalprice.ToString() + "; ";
        }


        //if (loiControllerr.CheckLOI_Upload_by_FM(dr) == 1)
        //    ErrorMessage += "LOI already exists; ";

        if (loiControllerr.Validate_SubconName(dr["Subcone_Name"].ToString()) == 0)
            ErrorMessage += "Subcon is not registered; ";

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
        if (dtReceiverMail.Rows.Count > 0)
        {
            string ReceiverMail = string.Join(",", dtReceiverMail.AsEnumerable()
                                  .Select(s => s.Field<string>("Email")).ToArray());
            string ReceiverName = string.Join(",", dtReceiverMail.AsEnumerable()
                                  .Select(s => s.Field<string>("Name")).ToArray());
            loiControllerr.sendMail(ReceiverName, ReceiverMail, htmltabledetail, "Checking BOQ Price", ContentSession.FULLNAME, ReqId);

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