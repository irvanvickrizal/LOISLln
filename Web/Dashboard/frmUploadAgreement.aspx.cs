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
using System.IO;
using System.Configuration;

public partial class Dashboard_frmUploadAgreement : System.Web.UI.Page
{
    LOIController loiControllerr = new LOIController();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtProjectAgreementNo.Text.Trim()))
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

        if (loiControllerr.project_agreement_iud(txtProjectAgreementNo.Text, DateTime.Parse(txtvalidfrom.Value), DateTime.Parse(txtvalidto.Value), ddlProjectName.SelectedValue, 0, false))
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('project agreement has been created successfully');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to creat project agreement');", true);
        }
    }

    protected void btnSubmitPriceAgrr_Click(object sender, EventArgs e)
    {

        if (string.IsNullOrEmpty(uploadFile.FileName))
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('price agreement document no is empty');", true);
            return;
        }

        if (string.IsNullOrEmpty(txtValidFromPriceAgrr.Value.Trim()))
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('valid from is empty');", true);
            return;
        }

        if (string.IsNullOrEmpty(txtValidToPriceAgrr.Value.Trim()))
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('valit to is empty');", true);
            return;
        }

        string docfilename = uploadpriceattachment();
        if (!string.IsNullOrEmpty(docfilename))
        {
            if (loiControllerr.price_agreement_iud(docfilename, DateTime.Parse(txtValidFromPriceAgrr.Value), DateTime.Parse(txtValidToPriceAgrr.Value), ddlProjectName.SelectedValue, 0, false))
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('price agreement has been uploaded successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to upload price agreement');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to upload price agreement');", true);
        }
    }

    private string uploadpriceattachment()
    {
        string result = string.Empty;
        string path = ConfigurationManager.AppSettings["PriceAgreemenDocPath"];
        path = Server.MapPath(path);
        try
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            path += uploadFile.FileName;
            uploadFile.SaveAs(path);
            result = path;
        }
        catch (Exception ex)
        {
            result = string.Empty;
        }
        return result;
    }
}