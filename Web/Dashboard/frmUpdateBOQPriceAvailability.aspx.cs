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

public partial class Dashboard_frmUpdateBOQPriceAvailability : System.Web.UI.Page
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
        gvSupDoc.DataSource = loiControllerr.boq_price_getNotAvailable(RequestId);
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
}