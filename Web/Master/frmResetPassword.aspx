<%@ Page Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="frmResetPassword.aspx.cs" Inherits="Master_frmResetPassword" %>

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
            $('h1.page-header').html('Master User');
        });
        function Checkboxchecked() {
            document.getElementById("btnChkReviewChecked").click();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="Server">
    <div class="clearfix"></div>
    <asp:UpdateProgress ID="upgViewPrgoress" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="upUser">
        <ProgressTemplate>
            <div id="blur">
                <div style="position: relative; top: 30%; text-align: center; background-color: #ffffff;">
                    <img src="../images/loading.gif" style="vertical-align: middle" alt="Processing" height="180" width="180" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="upUser" runat="server">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <i class="fa fa-tasks fa-fw"></i>User search parameter
                    </div>
                    <div class="panel-body">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="WFType">User Type</label>
                                <asp:DropDownList ID="ddlUserType" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Nokia" Value="N"></asp:ListItem>
                                    <asp:ListItem Text="Customer" Value="C"></asp:ListItem>
                                    <asp:ListItem Text="Subcon" Value="S"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="UserDesc">Name</label>
                                <input type="text" class="form-control" id="txtName" runat="server" />
                            </div>
                            <div class="form-group">
                                <label for="WFType">Level</label>
                                <asp:DropDownList ID="ddlLevel" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                    <asp:ListItem Text="National" Value="N"></asp:ListItem>
                                    <asp:ListItem Text="Area" Value="A"></asp:ListItem>
                                    <asp:ListItem Text="region" Value="R"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="WFType">Role</label>
                                <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="UserDesc">User Login</label>
                                <input type="text" class="form-control" id="txtUserId" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="UserDesc">Email</label>
                                <input type="text" class="form-control" id="txtEmail" runat="server" />
                            </div>
                            <div class="form-group">
                                <label for="UserDesc">Phone No</label>
                                <input type="text" class="form-control" id="txtPhone" runat="server" />
                            </div>
                            <div class="form-group">
                                <label for="UserDesc">Sign Title</label>
                                <input type="text" class="form-control" id="txtSigTitle" runat="server" />
                            </div>
                            <div class="form-group">
                                <label for="WFType">CT Name</label>
                                <asp:DropDownList ID="ddlCTName" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                    <asp:ListItem Text="ALL CT" Value="ALL"></asp:ListItem>
                                    <asp:ListItem Text="Indosat" Value="ISAT"></asp:ListItem>
                                    <asp:ListItem Text="telkomsel" Value="TSEL"></asp:ListItem>
                                    <asp:ListItem Text="H3I" Value="H3I"></asp:ListItem>
                                    <asp:ListItem Text="Smartfren" Value="SF"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="WFType">Status</label>
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Active" Value="A"></asp:ListItem>
                                    <asp:ListItem Text="InActive" Value="I"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="text-right">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <asp:Button ID="btnChkReviewChecked" ClientIDMode="Static" runat="server" OnClick="btnChkReviewChecked_Click" Style="display: none;" />
                <div class="text-right">
                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-primary" OnClick="btnReset_Click" />
                </div>
                <asp:GridView ID="gvUser" runat="server" AutoGenerateColumns="false" CssClass="Grid" Width="100%" Font-Size="12px"
                    EmptyDataText="No data" OnPageIndexChanging="gvUser_PageIndexChanging">
                    <HeaderStyle CssClass="gridheader" />
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkall" runat="server" AutoPostBack="true" OnCheckedChanged="chkall_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <input id="ChkReview" runat="server" type="checkbox" onclick="Checkboxchecked();" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No.">
                            <ItemStyle Width="35px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="USR_ID" HeaderText="USR_ID" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                        <asp:BoundField DataField="USRType" HeaderText="User Type" />
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="LVLCode" HeaderText="Level Code" />
                        <asp:BoundField DataField="USRRole" HeaderText="Role" />
                        <asp:BoundField DataField="USRLogin" HeaderText="USER Login" />
                        <asp:BoundField DataField="USRPassword" HeaderText="Password" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="PhoneNo" HeaderText="Phone No" />
                        <asp:BoundField DataField="ACC_Status" HeaderText="Status" />
                        <asp:BoundField DataField="SignTitle" HeaderText="Sign Title" />
                        <asp:BoundField DataField="CTName" HeaderText="CT Name" />
                        <%--<asp:BoundField DataField="lmdt" HeaderText="Modify Date" HtmlEncode="false"
                            DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />--%>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gvUser" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
