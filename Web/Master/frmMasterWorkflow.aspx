<%@ Page Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="frmMasterWorkflow.aspx.cs" Inherits="Master_frmMasterWorkflow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        #PleaseWait {
            z-index: 200;
            position: absolute;
            top: 0pt;
            left: 0pt;
            text-align: center;
            height: 50px;
            width: 50px;
            background-image: url(../images/loading.gif);
            background-repeat: no-repeat;
            margin: 0 10%;
            margin-top: 10px;
        }

        #blur {
            width: 100%;
            background-color: #ffffff;
            moz-opacity: 0.7;
            khtml-opacity: .7;
            opacity: .7;
            filter: alpha(opacity=70);
            z-index: 120;
            height: 100%;
            position: absolute;
            top: 0;
            left: 0;
        }

        .messagealert {
            width: 100%;
            position: relative;
            top: 20px;
            z-index: 100000;
            padding: 0;
            font-size: 15px;
        }

        .Grid {
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            font-family: Calibri;
            color: #474747;
        }

            .Grid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
            }

            .Grid th {
                padding: 4px 2px;
                color: #fff;
                background-color: #2841d1;
                border-left: solid 1px #525252;
                font-size: 1.2em;
                text-align: center;
            }

        .gridheader {
            background: #0094ff;
            text-align: center;
        }

        table .header-fixed {
            position: fixed;
            top: 40px;
            z-index: 1020; /* 10 less than .navbar-fixed to prevent any overlap */
            border-bottom: 1px solid #d5d5d5;
            -webkit-border-radius: 0;
            -moz-border-radius: 0;
            border-radius: 0;
            -webkit-box-shadow: inset 0 1px 0 #fff, 0 1px 5px rgba(0,0,0,.1);
            -moz-box-shadow: inset 0 1px 0 #fff, 0 1px 5px rgba(0,0,0,.1);
            box-shadow: inset 0 1px 0 #fff, 0 1px 5px rgba(0,0,0,.1);
            filter: progid:DXImageTransform.Microsoft.gradient(enabled=false); /* IE6-9 */
        }

        .hiddencol {
            display: none;
        }
    </style>
    <script src="https://cdn.syncfusion.com/js/assets/external/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="https://cdn.syncfusion.com/16.1.0.24/js/web/ej.web.all.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('h1.page-header').html('Workflow Creation');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="Server">
    <div class="clearfix"></div>
    <asp:UpdateProgress ID="upgViewPrgoress" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="upWorkflow">
        <ProgressTemplate>
            <div id="blur">
                <div style="position: relative; top: 30%; text-align: center; background-color: #ffffff;">
                    <img src="../images/loading.gif" style="vertical-align: middle" alt="Processing" height="180" width="180" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="upWorkflow" runat="server">
        <ContentTemplate>
            <div class="col-md-5">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <i class="fa fa-tasks fa-fw"></i>Workflow Creation
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <label for="WFName">Workflow Name</label><span class="text-capitalize" style="color: red;"> *</span>
                            <input type="text" class="form-control" id="txtWFName" runat="server" />
                        </div>
                        <div class="form-group">
                            <label for="WFDesc">Workflow Desc</label>
                            <input type="text" class="form-control" id="txtWFDesc" runat="server" />
                        </div>                        
                        <div class="text-right">
                            <asp:Button ID="btnConfirm" runat="server" Text="Create" CssClass="btn btn-primary" OnClick="btnConfirm_Click" />
                            <asp:Button ID="btnCancel1" runat="server" Text="Cancel" CssClass="btn btn-primary" OnClick="btnCancel_Click1" visible="false" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-7">
                <asp:MultiView ID="mvFlow" runat="server">
                    <asp:View ID="vwList" runat="server">
                        <%--<div class="form-inline">
                    <div class="form-group">
                        <label for="filter">Filtered by </label>
                        <asp:DropDownList ID="DdlStatus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DdlStatus_SelectedIndexChanged">
                            <asp:ListItem Text="--All Status--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                            <asp:ListItem Text="In-Active" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="DdlDefinedStatus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DdlDefinedStatus_SelectedIndexChanged">
                            <asp:ListItem Text="--All Defined Status--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Not Defined" Value="notdefined"></asp:ListItem>
                            <asp:ListItem Text="Draft" Value="draft"></asp:ListItem>
                            <asp:ListItem Text="Done" Value="done"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>--%>
                        <asp:HiddenField ID="hdnwfid1" runat="server" />
                        <asp:GridView ID="gvWorkflows" OnRowDataBound="gvWorkflows_RowDataBound" runat="server" AutoGenerateColumns="false" CssClass="Grid" Width="100%" Font-Size="12px"
                            OnRowCommand="gvWorkflows_RowCommand" EmptyDataText="No data">
                            <HeaderStyle CssClass="gridheader" />
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemStyle Width="35px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:BoundField DataField="wf_name" HeaderText="Workflow" />
                                <asp:BoundField DataField="wf_desc" HeaderText="Description" />
                                <asp:TemplateField HeaderText="Define Workflow">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefineStatus" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="lmdt" HeaderText="Modify Date" HtmlEncode="false"
                                    DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div style="width: 100%; text-align: center;">
                                            <asp:Label ID="lblWFName" runat="server" Text='<%#Eval("wf_name") %>' Visible="false"></asp:Label>
                                            <asp:ImageButton ID="imgEditWF" runat="server" ImageUrl="~/images/edit-icon.png" CommandName="editflow" CommandArgument='<%#Eval("wf_id") %>' Width="18px" Height="18px" ToolTip="Edit Flow" />
                                            <asp:ImageButton ID="imgDelWF" runat="server" ImageUrl="~/images/trash.png" Width="18px" Height="18px" CommandName="deleteflow" CommandArgument='<%#Eval("wf_id") %>' ToolTip="Delete Flow" />
                                            <asp:ImageButton ID="imgWFDef" runat="server" ImageUrl="~/images/workflow-icon.png" Width="18px" Height="18px" CommandName="defineflow" CommandArgument='<%#Eval("wf_id") %>' ToolTip="Define Flow" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:View>
                    <asp:View ID="vwDetail" runat="server">
                        <asp:HiddenField ID="hdnWFID" runat="server" />
                        <asp:HiddenField ID="hdnIsDef" runat="server" />
                        <asp:HiddenField ID="hdnDefinedStatus" runat="server" />
                        <div class="h4">
                            <span class="text-primary">Flow :
                                <asp:Label ID="lblWFName" runat="server"></asp:Label></span>
                        </div>
                        <asp:GridView ID="gvWFDefined" DataKeyNames="wfdef_id" runat="server" AutoGenerateColumns="false" Width="100%"
                            ShowFooter="true" HeaderStyle-Font-Bold="true" EmptyDataText="No data"
                            OnRowCancelingEdit="gvWFDefined_RowCancelingEdit" OnRowDeleting="gvWFDefined_RowDeleting"
                            OnRowEditing="gvWFDefined_RowEditing" OnRowUpdating="gvWFDefined_RowUpdating" Font-Size="12px"
                            OnRowCommand="gvWFDefined_RowCommand" OnRowDataBound="gvWFDefined_RowDataBound" CssClass="table-condensed">
                            <HeaderStyle CssClass="gridheader" />
                            <Columns>
                                <asp:TemplateField ShowHeader="false" HeaderStyle-BackColor="white" ItemStyle-VerticalAlign="Top"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" ImageUrl="~/Images/gridview/update-icon.png"
                                            ToolTip="Update" Height="16px" Width="16px" />
                                        <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/gridview/reject-icon.png"
                                            ToolTip="Cancel" Height="16px" Width="16px" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgUpIcon" CommandName="MoveUp" runat="server" ImageUrl="~/Images/up-icon.png"
                                            ToolTip="Move Up" Height="16px" Width="16px" CommandArgument='<%#Eval("wfdef_id") %>' />
                                        <asp:ImageButton ID="imgDownIcon" CommandName="MoveDown" runat="server" ImageUrl="~/Images/down-icon.png"
                                            ToolTip="Move Down" Height="16px" Width="16px" CommandArgument='<%#Eval("wfdef_id") %>' />
                                        <asp:ImageButton ID="imgbtnDelete" CommandName="deleteflow" CommandArgument='<%#Eval("wfdef_id") %>' runat="server" ImageUrl="~/Images/trash.png"
                                            ToolTip="Delete" Height="16px" Width="16px" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="vertical-align: top; text-align: center;">
                                            <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/add-icon.png"
                                                CommandName="addNew" Width="16px" Height="16px" ToolTip="Add new step" ValidationGroup="validation2" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Seqno" HeaderText="Seq.No" ReadOnly="true" ItemStyle-Width="5%" />
                                <asp:TemplateField HeaderText="Role" HeaderStyle-ForeColor="black" ItemStyle-Width="25%">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="DdlEditRoles" runat="server" CssClass="list-group-item"></asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblRoles" runat="server" Text='<%#Eval("RoleDetailDesc")%>' CssClass="lblText" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="margin-top: 5px;">
                                            <div>
                                                <asp:DropDownList ID="DdlRoles" runat="server" CssClass="form-control" Font-Size="11px"></asp:DropDownList>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblRoleDefine" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Task" HeaderStyle-ForeColor="black" ItemStyle-Width="20%">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="DdlEditTasks" runat="server" CssClass="list-group-item"></asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTaskDesc" runat="server" Text='<%#Eval("taskdesc")%>' CssClass="lblText" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="margin-top: 5px;">
                                            <div>
                                                <asp:DropDownList ID="DdlTasks" runat="server" CssClass="form-control" Font-Size="11px"></asp:DropDownList>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblTaskDefine" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div class="text-right">
                            <div style="padding-top: 20px;">
                                <asp:Button ID="btnEditFlow" runat="server" Text="Edit Flow" CssClass="btn btn-danger" OnClick="btnEditFlow_Click" OnClientClick="return confirm('Are you sure you want to Edit Flow?')" />
                                <asp:Button ID="btnFlowConfirm" runat="server" Text="Confirm" CssClass="btn btn-primary" OnClick="btnFlowConfirm_Click" OnClientClick="return confirm('Are you sure you want to confirm as final defined Flow?')" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default right" OnClick="btnCancel_Click" />
                            </div>
                        </div>

                    </asp:View>
                </asp:MultiView>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gvWorkflows" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
