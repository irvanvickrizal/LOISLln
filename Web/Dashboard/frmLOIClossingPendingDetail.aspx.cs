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

public partial class Dashboard_frmLOIClossingPendingDetail : System.Web.UI.Page
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
        gvLOIData.DataSource = loiControllerr.LOI_Detail_ClossingPending_by_RequestId(RequestId);
        gvLOIData.PageIndex = pgIndex;
        gvLOIData.DataBind();
    }

    protected void gvLOIData_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void gvLOIData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }

    protected void gvLOIData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindData(e.NewPageIndex);
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {       
        int successcount = 0;
        int failedcount = 0;
        try
        {
            int totalchecked = gvLOIData.Rows.Cast<GridViewRow>().Count(r => ((HtmlInputCheckBox)r.FindControl("ChkReview")).Checked);
            if (totalchecked < 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('no data checked');", true);
                return;
            }
            int RequestId = int.Parse(Request.QueryString["RequestId"]);
            foreach (GridViewRow gvr in gvLOIData.Rows)
            {
                HtmlInputCheckBox ChkReview = (HtmlInputCheckBox)gvr.FindControl("ChkReview");
                string workpackageid = gvr.Cells[2].Text;
                int LOI_Detail_ID = int.Parse(gvr.Cells[15].Text);
                if (ChkReview != null)
                {
                    if (ChkReview.Checked == true)
                    {
                        if (loiControllerr.LOI_Detail_Update_Approval_Status(workpackageid, LOI_Detail_ID, RequestId, "PCM Close"))
                        {
                            successcount += 1;
                        }
                        else
                        {
                            failedcount += 1;
                        }
                    }
                }
            }

            if (failedcount == 0 && successcount > 0)
            {
                //sendemail(gvr.Cells[4].Text, gvr.Cells[9].Text, gvr.Cells[7].Text, password);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('LOI has been clossed Successfully'); location.href = '../dashboard/frmLOIClossingPending.aspx';", true);
            }
            else if (successcount > 0 && failedcount > 0)
            {
                //sendemail(gvr.Cells[4].Text, gvr.Cells[9].Text, gvr.Cells[7].Text, password);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + successcount.ToString() + " LOI has been clossed Successfully and " + failedcount.ToString() + " Failed to clossed LOI');", true);
            }
            else if (successcount == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to clossed LOI');", true);
            }

        }
        catch (Exception ex)
        {

        }

        //int RequestId = int.Parse(Request.QueryString["RequestId"]);
        //if (loiControllerr.LOI_Update_Approval_Status(RequestId, true, "PCM Close", ""))
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('LOI has been clossed Successfully'); location.href = '../dashboard/frmLOIClossingPending.aspx';", true);
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to clossed LOI');", true);
        //}
    }

    private void bindSuppDoc()
    {
        int RequestId = int.Parse(Request.QueryString["RequestId"]);
        gvSupDoc.DataSource = loiControllerr.loi_supportingdoc_getall(RequestId);
        gvSupDoc.DataBind();
    }

    protected void chkall_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox mainchk = (CheckBox)gvLOIData.HeaderRow.Cells[0].FindControl("chkall");
        for (int i = 0; (i <= (gvLOIData.Rows.Count - 1)); i++)

        {
            HtmlInputCheckBox chk = (HtmlInputCheckBox)gvLOIData.Rows[i].Cells[0].FindControl("ChkReview");
            if (chk != null)
            {
                if ((mainchk.Checked == true))
                {
                    if ((chk.Disabled == false))
                    {
                        chk.Checked = true;
                    }
                    else
                    {
                        chk.Checked = false;
                    }

                }
                else
                {
                    chk.Checked = false;
                }
            }
        }
    }

    protected void btnChkReviewChecked_Click(object sender, EventArgs e)
    {
        CheckBox mainchk = (CheckBox)gvLOIData.HeaderRow.Cells[0].FindControl("chkall");
        int totalcount = gvLOIData.Rows.Count;
        int totalchecked = gvLOIData.Rows.Cast<GridViewRow>().Count(r => ((HtmlInputCheckBox)r.FindControl("ChkReview")).Checked);
        if (totalcount == totalchecked && totalcount > 0 && totalchecked > 0)
        {
            mainchk.Checked = true;
        }
        else
        {
            mainchk.Checked = false;
        }
    }
}