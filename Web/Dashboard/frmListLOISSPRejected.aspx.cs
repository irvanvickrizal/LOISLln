﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eLoi.Controller;

public partial class Dashboard_frmListLOISSPRejected : System.Web.UI.Page
{
    LOIController loiControllerr = new LOIController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void gvListLOI_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("UploadPrice"))
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            Response.Redirect("frmCheckingBOQPrice.aspx?RequestId=" + e.CommandArgument.ToString() + "&isRejected=1");
        }
    }

    protected void gvListLOI_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvListLOI_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindData(e.NewPageIndex);
    }

    private void BindData(int pgIndex = 0)
    {
        gvListLOI.DataSource = loiControllerr.LOI_get_UploadRejected();
        gvListLOI.PageIndex = pgIndex;
        gvListLOI.DataBind();
    }
}