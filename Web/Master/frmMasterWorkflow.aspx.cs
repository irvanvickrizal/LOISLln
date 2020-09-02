using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using eLoi.Controller;

public partial class Master_frmMasterWorkflow : System.Web.UI.Page
{
    MasterController mc = new MasterController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }


    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtWFName.Value))
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('workflow name is empty');", true);
            return;
        }

        if (string.IsNullOrEmpty(txtWFDesc.Value))
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('workflow description is empty');", true);
            return;
        }


        if (mc.master_workflow_iud(txtWFName.Value, txtWFDesc.Value, hdnwfid1.Value, false))
        {
            if (string.IsNullOrEmpty(hdnwfid1.Value))
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Master workflow has been created Successfully');", true);
            else
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Master workflow has been updated Successfully');", true);

            hdnwfid1.Value = string.Empty;
            BindData();
            cleartext();
            btnCancel1.Visible = false;
            btnConfirm.Text = "Create";
        }
        else
        {
            if (string.IsNullOrEmpty(hdnwfid1.Value))
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to created master workflow');", true);
            else
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to updated master workflow');", true);
        }
    }

    private void BindData()
    {
        mvFlow.SetActiveView(vwList);
        gvWorkflows.DataSource = mc.master_workflow_getdata();
        gvWorkflows.DataBind();
    }

    private void cleartext()
    {
        txtWFName.Value = string.Empty;
        txtWFDesc.Value = string.Empty;
    }

    protected void gvWFDefined_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gvWFDefined_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void gvWFDefined_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void gvWFDefined_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("addNew"))
        {
            bool isvalid = true;
            DropDownList DdlRoles = (DropDownList)gvWFDefined.FooterRow.FindControl("DdlRoles");
            DropDownList DdlTasks = (DropDownList)gvWFDefined.FooterRow.FindControl("DdlTasks");
            Label lblRoleDefine = (Label)gvWFDefined.FooterRow.FindControl("lblRoleDefine");
            Label lblTaskDefine = (Label)gvWFDefined.FooterRow.FindControl("lblTaskDefine");

            if (DdlRoles != null && lblRoleDefine != null)
            {
                if (DdlRoles.SelectedValue.Equals("0"))
                { lblRoleDefine.Text = Resources.ResourcesError.err_role_notdefined; isvalid = false; }
            }
            if (DdlTasks != null && lblTaskDefine != null)
            {
                if (DdlTasks.SelectedValue.Equals("0"))
                { lblTaskDefine.Text = Resources.ResourcesError.err_task_notdefined; isvalid = false; }
            }

            if (mc.WFDef_IU(0, int.Parse(DdlRoles.SelectedValue), int.Parse(hdnWFID.Value), 0, ContentSession.USERID, int.Parse(DdlTasks.SelectedValue)))
                BindDefinedWorkflow(int.Parse(hdnWFID.Value), gvWFDefined);
        }
        else if (e.CommandName.Equals("MoveUp"))
        {
            if (mc.WFDef_Seqno_U(int.Parse(e.CommandArgument.ToString()), "up"))
                BindDefinedWorkflow(int.Parse(hdnWFID.Value), gvWFDefined);
        }
        else if (e.CommandName.Equals("MoveDown"))
        {
            if (mc.WFDef_Seqno_U(int.Parse(e.CommandArgument.ToString()), "down"))
                BindDefinedWorkflow(int.Parse(hdnWFID.Value), gvWFDefined);
        }
        else if (e.CommandName.Equals("deleteflow"))
        {
            if (mc.WFDef_D(int.Parse(e.CommandArgument.ToString())))
                BindDefinedWorkflow(int.Parse(hdnWFID.Value), gvWFDefined);
        }
    }

    protected void gvWFDefined_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void gvWFDefined_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            DropDownList DdlRoles = (DropDownList)e.Row.FindControl("DdlRoles");
            DropDownList DdlTasks = (DropDownList)e.Row.FindControl("DdlTasks");
            ImageButton imgbtnAdd = (ImageButton)e.Row.FindControl("imgbtnAdd");

            if (DdlRoles != null)
            {
                DdlRoles.DataSource = mc.TRole_getData();
                DdlRoles.DataTextField = "RoleDesc";
                DdlRoles.DataValueField = "RoleID";
                DdlRoles.DataBind();
                DdlRoles.Items.Insert(0, new ListItem("--Select--", "0"));

            }

            if (DdlTasks != null)
            {
                DdlTasks.DataSource = mc.Task_GetAll();
                DdlTasks.DataTextField = "taskdesc";
                DdlTasks.DataValueField = "task_id";
                DdlTasks.DataBind();
                DdlTasks.Items.Insert(0, new ListItem("--Select--", "0"));
            }

            if (imgbtnAdd != null && DdlRoles != null && DdlTasks != null)
            {
                if (btnEditFlow.Visible == true || (btnEditFlow.Visible == false && btnFlowConfirm.Visible == false && hdnDefinedStatus.Value == "Done"))
                {
                    imgbtnAdd.Visible = false;
                    DdlRoles.Enabled = false;
                    DdlTasks.Enabled = false;
                }
            }
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            ImageButton imgUpIcon = (ImageButton)e.Row.FindControl("imgUpIcon");
            ImageButton imgDownIcon = (ImageButton)e.Row.FindControl("imgDownIcon");
            ImageButton imgbtnDelete = (ImageButton)e.Row.FindControl("imgbtnDelete");
            int seqno = 0;
            if (!string.IsNullOrEmpty(Convert.ToString(dr["seqno"])))
                seqno = int.Parse(dr["seqno"].ToString());


            int totalFlow = 0;
            if (!string.IsNullOrEmpty(Convert.ToString(dr["totalflows"])))
                totalFlow = int.Parse(dr["totalflows"].ToString());

            string wfdefinedstatus = Convert.ToString(dr["wf_defined_status"]);
            if (imgUpIcon != null && imgDownIcon != null)
            {
                if (totalFlow == 1)
                {
                    imgUpIcon.Visible = false;
                    imgDownIcon.Visible = false;
                }
                else
                {
                    if (seqno == 1)
                    {
                        imgUpIcon.Visible = false;
                        imgDownIcon.Visible = true;
                    }
                    else
                    {
                        if (totalFlow == seqno)
                        {
                            imgUpIcon.Visible = true;
                            imgDownIcon.Visible = false;
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(wfdefinedstatus))
            {
                if (wfdefinedstatus.ToLower().Equals(GeneralConfig.WORKFLOW_DEFINED_STATUS_DONE.ToLower()))
                {
                    btnEditFlow.Visible = true;
                    btnFlowConfirm.Visible = false;
                    if (imgbtnDelete != null && imgUpIcon != null && imgDownIcon != null)
                    {
                        //gvWFDefined.Columns[0].Visible = false;
                        imgbtnDelete.Visible = false;
                        imgUpIcon.Visible = false;
                        imgDownIcon.Visible = false;
                    }

                }
                else
                {
                    //gvWFDefined.Columns[0].Visible = true;
                    btnEditFlow.Visible = false;
                    btnFlowConfirm.Visible = true;
                }

            }

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnEditFlow_Click(object sender, EventArgs e)
    {
        if (mc.WF_WFDefinedStatus_U(int.Parse(hdnWFID.Value), ContentSession.USERID, GeneralConfig.WORKFLOW_DEFINED_STATUS_DRAFT))
        {
            BindDefinedWorkflow(int.Parse(hdnWFID.Value), gvWFDefined);
        }
    }

    protected void btnFlowConfirm_Click(object sender, EventArgs e)
    {
        if (mc.WF_WFDefinedStatus_U(int.Parse(hdnWFID.Value), ContentSession.USERID, GeneralConfig.WORKFLOW_DEFINED_STATUS_DONE))
        {
            hdnWFID.Value = "0";
            BindData();

        }
    }

    protected void gvWorkflows_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            string definestatus = dr["wf_defined_status"].ToString();
            Label lblDefineStatus = (Label)e.Row.FindControl("lblDefineStatus");

            if (lblDefineStatus != null)
            {
                if (definestatus == GeneralConfig.WORKFLOW_DEFINED_STATUS_NY)
                {
                    lblDefineStatus.Text = "Not Defined";
                    lblDefineStatus.ForeColor = System.Drawing.Color.Red;
                    btnFlowConfirm.Visible = true;
                }
                else if (definestatus == GeneralConfig.WORKFLOW_DEFINED_STATUS_DRAFT)
                {
                    lblDefineStatus.Text = "Draft";
                    lblDefineStatus.ForeColor = System.Drawing.Color.Gray;
                    btnFlowConfirm.Visible = true;
                }
                else if (definestatus == GeneralConfig.WORKFLOW_DEFINED_STATUS_DONE)
                {
                    lblDefineStatus.Text = "Done";
                    lblDefineStatus.ForeColor = System.Drawing.Color.Blue;
                    btnFlowConfirm.Visible = false;
                }
            }

        }

    }

    protected void gvWorkflows_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("editflow"))
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            hdnwfid1.Value = e.CommandArgument.ToString();
            txtWFName.Value = row.Cells[1].Text;
            txtWFDesc.Value = row.Cells[2].Text;
            btnCancel1.Visible = true;
            btnConfirm.Text = "Update";
        }
        else if (e.CommandName.Equals("deleteflow"))
        {
            if (mc.master_workflow_iud(txtWFName.Value, txtWFDesc.Value, e.CommandArgument.ToString(), true))
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Master workflow has been deleted Successfully');", true);
                BindData();
                cleartext();
                hdnwfid1.Value = string.Empty;
                btnCancel1.Visible = false;
                btnConfirm.Text = "Create";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Failed to delete master workflow');", true);
            }
        }
        else if (e.CommandName.Equals("defineflow"))
        {
            //Access Cell values.
            GridViewRow clickedRow = ((ImageButton)e.CommandSource).NamingContainer as GridViewRow;
            Label lblWFName = (Label)clickedRow.FindControl("lblWFName");
            Label lblDefineStatus = (Label)clickedRow.FindControl("lblDefineStatus");
            BindDefineForm(e.CommandArgument.ToString(), lblWFName.Text, lblDefineStatus.Text);
        }
    }

    private void BindDefineForm(string wfid, string wfname, string definedstatus)
    {
        hdnWFID.Value = wfid;
        hdnDefinedStatus.Value = definedstatus;
        lblWFName.Text = wfname;
        mvFlow.SetActiveView(vwDetail);
        BindDefinedWorkflow(int.Parse(hdnWFID.Value), gvWFDefined);
    }

    private void BindDefinedWorkflow(int wfid, GridView gv)
    {
        DataTable dt = mc.WFDef_GetDetail(wfid);
        //if (dt.Rows.Count > 0)
        if (!string.IsNullOrEmpty(dt.Rows[0]["wfdef_id"].ToString()))
        {
            gv.DataSource = dt;
            gv.DataBind();
        }
        else
        {

            btnEditFlow.Visible = false;
            dt.Rows.Clear();
            dt.Rows.Add(dt.NewRow());
            gv.DataSource = dt;
            gv.DataBind();
            int columncount = gv.Rows[0].Cells.Count;
            gv.Rows[0].Cells.Clear();
            gv.Rows[0].Cells.Add(new TableCell());
            gv.Rows[0].Cells[0].ColumnSpan = columncount;
            gv.Rows[0].Cells[0].Text = "No Records Found";
        }
    }

    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        cleartext();
        hdnwfid1.Value = string.Empty;
        btnCancel1.Visible = false;
        btnConfirm.Text = "Create";
    }
}