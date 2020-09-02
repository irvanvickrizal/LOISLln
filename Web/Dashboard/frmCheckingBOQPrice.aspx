<%@ Page Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="frmCheckingBOQPrice.aspx.cs" Inherits="Dashboard_frmCheckingBOQPrice" %>

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


        function Checkboxchecked() {
            document.getElementById("btnChkReviewChecked").click();
        }

        function getQueryStringValue(key) {
            return decodeURIComponent(window.location.search.replace(new RegExp("^(?:.*[&\\?]" + encodeURIComponent(key).replace(/[\.\+\*]/g, "\\$&") + "(?:\\=([^&]*))?)?.*$", "i"), "$1"));
        }

        $(document).ready(function () {
            if (getQueryStringValue("isRejected") == 1) {
                $('h1.page-header').html('LOI rejection');
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="Server">
    <h4 class="text-info text-right"><i class="fa fa-list fa-fw"></i>Checking BOQ Pricing</h4>
    <div class="clearfix"></div>
    <asp:UpdateProgress ID="upgViewPrgoress" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="up1">
        <ProgressTemplate>
            <div id="blur">
                <div style="position: relative; top: 30%; text-align: center; background-color: #ffffff;">
                    <img src="../images/loading.gif" style="vertical-align: middle" alt="Processing" height="180" width="180" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <h4></h4>
            <div class="clearfix"></div>
            <div class="pull-right">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info" OnClick="btnSubmit_Click" />
            </div>
            <h4></h4>
            <div class="clearfix"></div>
            <h4></h4>
            <div class="clearfix"></div>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <i class="fa fa-tasks fa-fw"></i>Supporting Document Availibility
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <div class="form-inline">
                            <asp:GridView ID="gvSupDoc" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="30" CssClass="Grid" Width="100%" EmptyDataText="No Data Recorded" Font-Size="13px"
                                OnRowDataBound="gvSupDoc_RowDataBound" OnRowCommand="gvSupDoc_RowCommand">
                                <EmptyDataRowStyle CssClass="alert-warning" ForeColor="Red" />
                                <HeaderStyle CssClass="gridheader" />
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemStyle Width="20px" VerticalAlign="Middle"></ItemStyle>
                                        <ItemTemplate>
                                            <div style="vertical-align: middle;">
                                                <%# Container.DataItemIndex + 1 %>.
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="document_name" HeaderText="Document Name" HeaderStyle-Width="75%" />
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Availability
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="col-lg-6">
                                                <asp:Button ID="btnYes" runat="server" Width="100%" Text="Yes" CssClass="btn btn-primary" CommandName="ConfirmYes" CommandArgument='<%#Eval("master_supportingdocument_id") %>' />
                                            </div>
                                            <div class="col-lg-6">
                                                <asp:Button ID="btnNoConfirm" Width="100%" runat="server" Text="No" CssClass="btn btn-danger" CommandName="ConfirmNo" />
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtAvailibility" runat="server" CssClass="form-control" Enabled="false" ForeColor="Red" Visible="false" placeholder="Availibility" Font-Size="12px" />
                                                <asp:ImageButton ID="imgCanceled" runat="server" ImageUrl="~/images/reject-icon.png" Width="25px" Height="25px" Visible="false" CommandName="CancelAvailibility" CommandArgument='<%#Eval("master_supportingdocument_id") %>' />
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtRemarksNoConfirm" Width="100%" runat="server" CssClass="form-control" TextMode="MultiLine" Height="60px" ForeColor="Red" Visible="false" placeholder="Remarks" Font-Size="12px" />
                                            </div>
                                            <div class="col-lg-6">
                                                <asp:Button ID="btnSubmitNoConfirm" Width="100%" runat="server" Text="Confirm" CssClass="btn btn-primary" CommandName="SubmitConfirmNo" Visible="false" CommandArgument='<%#Eval("master_supportingdocument_id") %>' />
                                            </div>
                                            <div class="col-lg-6">
                                                <asp:Button ID="btnCancelNoConfirm" Width="100%" runat="server" Text="Back" CssClass="btn btn-primary" CommandName="CancelConfirmNo" Visible="false" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <i class="fa fa-tasks fa-fw"></i>Uploaded Data
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <asp:GridView ID="gvLOIData" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="30" CssClass="Grid" Width="100%" OnRowCommand="gvLOIData_RowCommand"
                            EmptyDataText="No Data Recorded" Font-Size="13px" OnRowDataBound="gvLOIData_RowDataBound" OnPageIndexChanging="gvLOIData_PageIndexChanging">
                            <EmptyDataRowStyle CssClass="alert-warning" ForeColor="Red" />
                            <HeaderStyle CssClass="gridheader" />
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemStyle Width="20px" VerticalAlign="Middle"></ItemStyle>
                                    <ItemTemplate>
                                        <div style="vertical-align: middle;">
                                            <%# Container.DataItemIndex + 1 %>.
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="workpackageid" HeaderText="workpackageid" />
                                <asp:BoundField DataField="Customer_PO" HeaderText="Customer PO" />
                                <asp:BoundField DataField="Customer_PO_Date" HeaderText="Customer PO Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                <asp:BoundField DataField="PO_Description" HeaderText="PO Description" />
                                <asp:BoundField DataField="Region" HeaderText="Region" />
                                <asp:BoundField DataField="Site_ID" HeaderText="Site ID" />
                                <asp:BoundField DataField="ScopeOfWork" HeaderText="Scope Of Work" />
                                <asp:BoundField DataField="Subcone_Name" HeaderText="Subcon" />
                                <asp:BoundField DataField="Site_Model" HeaderText="Site Model" />
                                <asp:BoundField DataField="Unit_Price" HeaderText="Unit Price" DataFormatString="{0:n0}" />
                                <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                <asp:BoundField DataField="Total_Price" HeaderText="Total Price" DataFormatString="{0:n0}" />
                                <asp:BoundField DataField="Currency" HeaderText="Currency" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="gvLOIData" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
