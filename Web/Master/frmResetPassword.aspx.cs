using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using eLoi.Controller;

public partial class Master_frmResetPassword : System.Web.UI.Page
{
    MasterController mc = new MasterController();
    LOIController lc = new LOIController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindLOIRole();
            BindData();
        }
    }

    private void BindLOIRole()
    {
        ddlRole.DataSource = mc.TRole_getData();
        ddlRole.DataTextField = "RoleDesc";
        ddlRole.DataValueField = "RoleID";
        ddlRole.DataBind();
        ddlRole.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void BindData()
    {
        gvUser.DataSource = mc.usp_getuserdata_all(ddlUserType.SelectedValue, txtName.Value, ddlLevel.SelectedValue, ddlRole.SelectedValue, txtUserId.Value, txtEmail.Value, txtPhone.Value, txtSigTitle.Value, ddlCTName.SelectedValue, ddlStatus.SelectedValue);
        //gvUser.PageIndex = pgIndex;
        gvUser.DataBind();


    }

    protected void gvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void chkall_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox mainchk = (CheckBox)gvUser.HeaderRow.Cells[0].FindControl("chkall");
        for (int i = 0; (i <= (gvUser.Rows.Count - 1)); i++)

        {
            HtmlInputCheckBox chk = (HtmlInputCheckBox)gvUser.Rows[i].Cells[0].FindControl("ChkReview");
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
        CheckBox mainchk = (CheckBox)gvUser.HeaderRow.Cells[0].FindControl("chkall");
        int totalcount = gvUser.Rows.Count;
        int totalchecked = gvUser.Rows.Cast<GridViewRow>().Count(r => ((HtmlInputCheckBox)r.FindControl("ChkReview")).Checked);
        if (totalcount == totalchecked && totalcount > 0 && totalchecked > 0)
        {
            mainchk.Checked = true;
        }
        else
        {
            mainchk.Checked = false;
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        int successcount = 0;
        int failedcount = 0;
        try
        {
            int totalchecked = gvUser.Rows.Cast<GridViewRow>().Count(r => ((HtmlInputCheckBox)r.FindControl("ChkReview")).Checked);
            if (totalchecked < 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('no data checked');", true);
                return;
            }
            
            foreach (GridViewRow gvr in gvUser.Rows)
            {
                HtmlInputCheckBox ChkReview = (HtmlInputCheckBox)gvr.FindControl("ChkReview");
                if (ChkReview != null)
                {
                    if (ChkReview.Checked == true)
                    {
                        string password = mc.getrandomtext();
                        if (mc.reset_password_all_ct(int.Parse(gvr.Cells[2].Text), password, gvr.Cells[13].Text))
                        {
                            sendemail(gvr.Cells[4].Text, gvr.Cells[9].Text, gvr.Cells[7].Text, password);
                            successcount += 1;
                        }
                        else
                        {
                            failedcount += 1;
                        }

                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
        string notif = string.Concat("alert('", successcount.ToString()," password has been reset successfully & ", failedcount.ToString(), " password failed to reset');");
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), notif, true);
    }

    private void sendemail(string fullname, string email, string userlogin, string password)
    {
        lc.sendMailResetPassword(fullname, email, "resetpassword", userlogin, password);
    }

    
}