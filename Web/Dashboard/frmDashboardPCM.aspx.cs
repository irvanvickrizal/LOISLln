using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eLoi.Controller;
using System.Data;

public partial class Dashboard_frmDashboardPCM : System.Web.UI.Page
{
    LOIController loiControllerr = new LOIController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            BindSubcon();
            BindAgenda();
            BindTasklist();
        }
    }

    private void BindAgenda()
    {
        int UploadedCount = loiControllerr.LOI_get_UploadedCount();
        if (UploadedCount > 0)
        {
            ltrInputPricePendingCount.Text = "<span class=\"pull-right text-muted medium\">" + UploadedCount + "</span>";
            aViewInputPricePending.HRef = "frmListLOIPCM.aspx";
        }

        int ClossingPendingCount = loiControllerr.LOI_get_UploadedCount_byStatus("Approved");
        if (ClossingPendingCount > 0)
        {
            ltrLoiClossingPending.Text = "<span class=\"pull-right text-muted medium\">" + ClossingPendingCount + "</span>";
            aLoiClossingPending.HRef = "frmLOIClossingPending.aspx";
        }

        int RejectedCount = loiControllerr.LOI_get_UploadRejectedCount();
        if (RejectedCount > 0)
        {
            ltrRejectedPendingCount.Text = "<span class=\"pull-right text-muted medium\">" + RejectedCount + "</span>";
            aRejectedPending.HRef = "frmListLOIPCMRejected.aspx";
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
}