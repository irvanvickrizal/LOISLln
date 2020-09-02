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

public partial class Dashboard_frmUploadAttchPriceAgreement : System.Web.UI.Page
{
    LOIController loiControllerr = new LOIController();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(uploadFile.FileName))
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('project agreement no is empty');", true);
            return;
        }

        if (string.IsNullOrEmpty(txtvalidfrom.Value.Trim()))
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('valid from is empty');", true);
            return;
        }

        if (string.IsNullOrEmpty(txtvalidto.Value.Trim()))
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('valit to is empty');", true);
            return;
        }

        if (loiControllerr.project_agreement_iud(uploadFile.FileName, DateTime.Parse(txtvalidfrom.Value), DateTime.Parse(txtvalidto.Value), ddlProjectName.SelectedValue, 0, false))
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('project agreement has been created successfully');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to creat project agreement');", true);
        }
    }
}