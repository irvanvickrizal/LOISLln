﻿<%@ Page Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="frmRoleGrouping.aspx.cs" Inherits="Master_frmRoleGrouping" %>

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
            $('h1.page-header').html('Mapping Role Grouping');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="Server">
    <div class="clearfix"></div>
    <asp:UpdateProgress ID="upgViewPrgoress" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="upRole">
        <ProgressTemplate>
            <div id="blur">
                <div style="position: relative; top: 30%; text-align: center; background-color: #ffffff;">
                    <img src="../images/loading.gif" style="vertical-align: middle" alt="Processing" height="180" width="180" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="upRole" runat="server">
        <ContentTemplate>
            <div class="col-md-5">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <i class="fa fa-tasks fa-fw"></i>Role Grouping
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <label for="WFType">Project Name</label>
                            <asp:DropDownList ID="ddlCTName" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCTName_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Indosat" Value="ISAT"></asp:ListItem>
                                <asp:ListItem Text="H3I" Value="H3I"></asp:ListItem>
                                <asp:ListItem Text="Telkomsel" Value="TSEL"></asp:ListItem>
                                <asp:ListItem Text="Smartfren" Value="SF"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="WFType">Project Role</label>
                            <asp:DropDownList ID="ddlProjectRole" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="WFType">LOI Role</label>
                            <asp:DropDownList ID="ddlLOIRole" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="text-right">
                            <asp:Button ID="btnConfirm" runat="server" Text="Create" CssClass="btn btn-primary" OnClick="btnConfirm_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary" OnClick="btnCancel_Click" Visible="false" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-7">
                <asp:HiddenField ID="hdnrg" runat="server" />
                <asp:GridView ID="gvRole" runat="server" AutoGenerateColumns="false" CssClass="Grid" Width="100%" Font-Size="12px"
                    EmptyDataText="No data" OnRowCommand="gvRole_RowCommand">
                    <HeaderStyle CssClass="gridheader" />
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemStyle Width="35px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CTName" HeaderText="CT Name" />
                        <asp:BoundField DataField="RoleID" HeaderText="" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
                        <asp:BoundField DataField="LOI_RoleID" HeaderText="" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
                        <asp:BoundField DataField="projectrole" HeaderText="Project Role" />
                        <asp:BoundField DataField="loirole" HeaderText="LOI Role" />
                        <asp:BoundField DataField="lmdt" HeaderText="Modify Date" HtmlEncode="false"
                            DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div style="width: 100%; text-align: center;">
                                    <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/images/edit-icon.png" CommandName="editrolegrouping" CommandArgument='<%#Eval("Role_Grouping_ID") %>' Width="18px" Height="18px" ToolTip="Edit" />
                                    <asp:ImageButton ID="imgDel" runat="server" ImageUrl="~/images/trash.png" Width="18px" Height="18px" CommandName="deleterolegrouping" CommandArgument='<%#Eval("Role_Grouping_ID") %>' ToolTip="Delete" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gvRole" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
