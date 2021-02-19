<%@ Page Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="frmLOIReport.aspx.cs" Inherits="rpt_frmLOIReport" %>

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
    </style>
    <%--<script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(
            function () {
                if (document.getElementById) {
                    var progress = document.getElementById('PleaseWait');
                    var blur = document.getElementById('blur');
                    progress.style.width = '300px';
                    progress.style.height = '30px';
                    blur.style.height = document.documentElement.clientHeight;
                    progress.style.top = document.documentElement.clientHeight / 3 - progress.style.height.replace('px', '') / 2 + 'px';
                    progress.style.left = document.body.offsetWidth / 2 - progress.style.width.replace('px', '') / 2 + 'px';
                }
            }
        )

        
    </script>--%>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('h1.page-header').html('LOI Report');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="Server">
    <div class="clearfix"></div>
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div class="form-group">
                <div class="form-inline">
                    <label for="comment">Report Type:</label>
                    <asp:DropDownList ID="ddlReportType" runat="server" CssClass="form-control" Width="30%">
                        <asp:ListItem Text="LOI Done" Value="LOI Done" Selected></asp:ListItem>
                        <asp:ListItem Text="Approval Tracking" Value="Approval Tracking"></asp:ListItem>
                        <asp:ListItem Text="LOI Rejected" Value="LOI Rejected"></asp:ListItem>
                        <asp:ListItem Text="LOI Open" Value="LOI Open"></asp:ListItem>
                        <asp:ListItem Text="LOI Overdue" Value="LOI Overdue"></asp:ListItem>
                        <asp:ListItem Text="LOI Cancelled" Value="LOI Cancelled"></asp:ListItem>
                        <asp:ListItem Text="LOI Closed" Value="LOI Closed"></asp:ListItem> <%-- Yunita 13/11/20 add new report as request by ISAT NPO WCC Controller--%>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <div class="form-inline">
                    <asp:DropDownList ID="DdlSubcon" runat="server" CssClass="form-control" Width="30%">
                    </asp:DropDownList>
                    <input type="text" id="txtperiodfrom" runat="server" onblur="(this.type='text')" onfocus="(this.type='date')" placeholder="request period from" class="form-control" style="width: 25%" />
                    <input type="text" id="txtperiodto" runat="server" onblur="(this.type='text')" onfocus="(this.type='date')" placeholder="request period to" class="form-control" style="width: 25%" />
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-info" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnDownloadList" runat="server" Text="Download List" CssClass="btn btn-primary" OnClick="btnDownloadList_Click" />
                </div>
            </div>
            <h4></h4>
            <div class="clearfix"></div>
            <div style="height: 20px;"></div>
            <ul class="nav nav-tabs">
                <li class="active"><a href="#loireport" data-toggle="tab">Report</a></li>
                <li><a href="#loimyhistorical" data-toggle="tab">My History</a></li>
            </ul>
            <div class="tab-content ">
                <div class="tab-pane active" id="loireport">
                    <asp:GridView ID="gvListLOI" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="30" CssClass="table table-condensed table-bordered"
                        EmptyDataText="You have 0 Task Pending" Font-Size="12px" OnRowCommand="gvListLOI_RowCommand" OnPageIndexChanging="gvListLOI_PageIndexChanging"
                        OnRowDataBound="gvListLOI_RowDataBound">
                        <HeaderStyle BackColor="#c7c5ff" HorizontalAlign="Center" />
                        <EmptyDataRowStyle CssClass="alert-warning" ForeColor="Red" />
                        <Columns>
                            <asp:TemplateField HeaderText="No." HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="gridheader">
                                <ItemStyle Width="20px" VerticalAlign="Middle"></ItemStyle>
                                <ItemTemplate>
                                    <div style="vertical-align: middle;">
                                        <%# Container.DataItemIndex + 1 %>.
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="LOI_Code" HeaderText="LOI Request No" HeaderStyle-CssClass="gridheader" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="LOI_Reference" HeaderText="LOI Refference Number" HeaderStyle-CssClass="gridheader" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MU_Approve_Date" HeaderText="Approved Date" HeaderStyle-CssClass="gridheader" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField DataField="totalPrice" HeaderText="Total Price (IDR)" HeaderStyle-CssClass="gridheader" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}" />
                            <asp:BoundField DataField="Qty" HeaderText="Qty" HeaderStyle-CssClass="gridheader" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Currency" HeaderText="Currency" HeaderStyle-CssClass="gridheader" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="ProjectName" HeaderText="Project Name" HeaderStyle-CssClass="gridheader" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Upload_Date" HeaderText="Request Date" HeaderStyle-CssClass="gridheader" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField DataField="Rejected_Date" HeaderText="Rejection Date" HeaderStyle-CssClass="gridheader" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField DataField="Rejected_By" HeaderText="Rejector" HeaderStyle-CssClass="gridheader" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Reject_Remarks" HeaderText="Reason of Rejection" HeaderStyle-CssClass="gridheader" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Uploaded_ByName" HeaderText="Requestor" HeaderStyle-CssClass="gridheader" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Subcone_Name" HeaderText="Subcon Name" HeaderStyle-CssClass="gridheader" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Closing_Plan_Date" HeaderText="Plan Close LOI" HeaderStyle-CssClass="gridheader" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField DataField="Closing_Actual_Date" HeaderText="LOI Close Date" HeaderStyle-CssClass="gridheader" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField DataField="LOI_Status" HeaderText="Status LOI" HeaderStyle-CssClass="gridheader" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="PIC" HeaderText="PIC/Approval Location" HeaderStyle-CssClass="gridheader" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Aging" HeaderText="Aging" HeaderStyle-CssClass="gridheader" ItemStyle-HorizontalAlign="Center" />
                            
                            
                            <asp:TemplateField HeaderStyle-CssClass="gridheader">
                                <ItemTemplate>
                                    <div style="width: 100%; text-align: center;">
                                        <asp:ImageButton ID="imgDownloadCert" runat="server" ImageUrl="~/images/pdf-download-icon.png" Width="25px" Height="25px" CommandName="downloadPDF" CommandArgument='<%#Eval("LOI_Document") %>' OnClick="imgDownloadCert_Click" />
                                        <%--                                <asp:ImageButton ID="imgDetail" runat="server" ImageUrl="~/images/review-icon.png" Width="25px" Height="25px" ToolTip="view detail" CommandName="viewDetail" CommandArgument='<%#Eval("RequestId") %>' />--%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Cancelled_Date" HeaderText="Cancelled Date" HeaderStyle-CssClass="gridheader" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField DataField="Cancelled_By" HeaderText="Cancel By" HeaderStyle-CssClass="gridheader" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderStyle-CssClass="gridheader">
                                <ItemTemplate>
                                    <div style="width: 100%; text-align: center;">
                                        <asp:ImageButton ID="imgDetail" runat="server" ImageUrl="~/images/review-icon.png" Width="25px" Height="25px" ToolTip="view detail" CommandName="viewDetail" CommandArgument='<%#Eval("RequestId") %>' />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="tab-pane" id="loimyhistorical">
                    <asp:GridView ID="gvhistory" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="30" CssClass="table table-condensed table-bordered"
                        EmptyDataText="You have 0 Task Pending" Font-Size="12px" OnPageIndexChanging="gvhistory_PageIndexChanging">
                        <HeaderStyle BackColor="#c7c5ff" HorizontalAlign="Center" />
                        <EmptyDataRowStyle CssClass="alert-warning" ForeColor="Red" />
                        <Columns>
                            <asp:TemplateField HeaderText="No." HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="gridheader">
                                <ItemStyle Width="20px" VerticalAlign="Middle"></ItemStyle>
                                <ItemTemplate>
                                    <div style="vertical-align: middle;">
                                        <%# Container.DataItemIndex + 1 %>.
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="pic" HeaderText="PIC" HeaderStyle-CssClass="gridheader" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="RoleDesc" HeaderText="Role" HeaderStyle-CssClass="gridheader" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="event_desc" HeaderText="Event" HeaderStyle-CssClass="gridheader" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="remarks" HeaderText="Remarks" HeaderStyle-CssClass="gridheader" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="eventstarttime" HeaderText="Event Start" HeaderStyle-CssClass="gridheader" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy HH:mm}" />
                            <asp:BoundField DataField="eventendtime" HeaderText="Event End" HeaderStyle-CssClass="gridheader" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy  HH:mm}" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="clearfix"></div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnDownloadList" />
            <asp:PostBackTrigger ControlID="gvListLOI" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
