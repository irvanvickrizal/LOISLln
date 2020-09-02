<%@ Page Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="frmDashboardMU.aspx.cs" Inherits="Dashboard_frmDashboardMU" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="https://cdn.syncfusion.com/js/assets/external/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="https://cdn.syncfusion.com/16.1.0.24/js/web/ej.web.all.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="Server">
    <div class="col-sm-8">
        <div class="panel panel-info">
            <div class="panel-heading">
                <i class="fa fa-bar-chart-o fa-fw"></i>LOI Progress Summary                            
            </div>
            <div class="panel-body">
                <div class="col-sm-12">
                    <div class="form-horizontal">
                        <div class="row" style="padding-top: 10px;">
                            <div class="col-md-6">
                                <label for="filter">View Graphic Chart Based on :</label>
                            </div>
                            <div class="col-md-6">
                                <label for="filter">Subcon :</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control" Width="100%">
                                    <asp:ListItem Text="LOI request" Value="LOI request" Selected></asp:ListItem>
                                    <asp:ListItem Text="Site list" Value="Site list"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <asp:DropDownList ID="ddlSubcon" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSubcon_SelectedIndexChanged" Width="100%">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row" style="padding-top: 15px;">
                            <div class="col-md-6">
                                <label for="filter">Request period from :</label>
                            </div>                            
                            <div class="col-md-6">
                                <label for="filter">Request period to :</label>
                            </div>                            
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <input type="text" id="txtperiodfromreqdate" runat="server" onblur="(this.type='text')" onfocus="(this.type='date')" placeholder="request period from" class="form-control" style="width: 100%" />
                            </div>
                            <div class="col-md-6">
                                <input type="text" id="txtperiodtoreqdate" runat="server" onblur="(this.type='text')" onfocus="(this.type='date')" placeholder="request period to" class="form-control" style="width: 100%" />
                            </div>
                        </div>
                        <div class="row" style="padding-top: 15px;">
                            <div class="col-md-6">
                                <label for="filter">Approve period from :</label>
                            </div>
                            <div class="col-md-6">
                                <label for="filter">Approve period to :</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <input type="text" id="txtperiodfromapprdate" runat="server" onblur="(this.type='text')" onfocus="(this.type='date')" placeholder="approve period from" class="form-control" style="width: 100%" />
                            </div>
                            <div class="col-md-6">
                                <input type="text" id="txtperiodtoapprdate" runat="server" onblur="(this.type='text')" onfocus="(this.type='date')" placeholder="approve period to" class="form-control" style="width: 100%" />
                            </div>
                        </div>
                        <div class="pull-right" style="padding-top: 10px;">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-info" OnClick="btnSearch_Click" />
                        </div>
                        <%--<div class="form-group">
                            <div class="form-inline">
                                <label for="filter">Type</label>&nbsp;&nbsp;
                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" Width="85%">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="form-inline">
                                <label for="filter">Subcon</label>&nbsp;&nbsp;
                                <asp:DropDownList ID="ddlSubcon" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSubcon_SelectedIndexChanged" Width="85%">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="form-inline">
                                <input type="text" id="txtperiodfromreqdate" runat="server" onblur="(this.type='text')" onfocus="(this.type='date')" placeholder="request period from" class="form-control" style="width: 25%" />
                                <input type="text" id="txtperiodtoreqdate" runat="server" onblur="(this.type='text')" onfocus="(this.type='date')" placeholder="request period to" class="form-control" style="width: 25%" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="form-inline">
                                <input type="text" id="txtperiodfromapprdate" runat="server" onblur="(this.type='text')" onfocus="(this.type='date')" placeholder="request period from" class="form-control" style="width: 25%" />
                                <input type="text" id="txtperiodtoapprdate" runat="server" onblur="(this.type='text')" onfocus="(this.type='date')" placeholder="request period to" class="form-control" style="width: 25%" />
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-info" OnClick="btnSearch_Click" />
                            </div>
                        </div>--%>
                    </div>
                </div>
                <div class="col-sm-10">
                    <ej:Chart ClientIDMode="Static" ID="pieChart" runat="server" OnClientLoad="onChartLoad" Height="300px" IsResponsive="true" OnClientPointRegionClick="onclick">
                        <PrimaryYAxis LabelFormat="{value}" RangePadding="None" Title-Text="Site List Tracking" />
                        <ChartArea>
                            <Border Color="transparent" />
                        </ChartArea>
                        <CommonSeriesOptions Type="Column" DoughnutSize="1" Tooltip-Visible="true" Marker-Size-Height="10" Marker-Size-Width="5" Marker-Border-Width="2" Marker-Visible="false" EnableAnimation="True" />
                        <Series>
                            <ej:Series Name="Total LOI" XName="task_to_confirm" YName="TotalLOI" Fill="#337AB7">
                                <Marker>
                                    <DataLabel Angle="300" EnableContrastColor="true" Visible="true" TextPosition="Middle" />
                                </Marker>
                            </ej:Series>
                            <ej:Series Name="Total OnProgress" XName="task_to_confirm" YName="TotalOnProgress" Fill="#FFFF00">
                                <Marker>
                                    <DataLabel Angle="300" EnableContrastColor="true" Visible="true" TextPosition="Middle" />
                                </Marker>
                            </ej:Series>
                            <ej:Series Name="Total Rejected not rectified yet" XName="task_to_confirm" YName="TotalRejected" Fill="#808080">
                                <Marker>
                                    <DataLabel Angle="300" EnableContrastColor="true" Visible="true" TextPosition="Middle" />
                                </Marker>
                            </ej:Series>
                            <ej:Series Name="Total Approved" XName="task_to_confirm" YName="TotalApproved" Fill="#22B14C">
                                <Marker>
                                    <DataLabel Angle="300" EnableContrastColor="true" Visible="true" TextPosition="Middle" />
                                </Marker>
                            </ej:Series>
                            <ej:Series Name="Total Closed" XName="task_to_confirm" YName="TotalClosse" Fill="#FF8040">
                                <Marker>
                                    <DataLabel Angle="300" EnableContrastColor="true" Visible="true" TextPosition="Middle" />
                                </Marker>
                            </ej:Series>
                            <ej:Series Name="Total Overdue" XName="task_to_confirm" YName="TotalOverdue" Fill="#ED1C24">
                                <Marker>
                                    <DataLabel Angle="300" EnableContrastColor="true" Visible="true" TextPosition="Middle" />
                                </Marker>
                            </ej:Series>
                            <ej:Series Name="Total Canceled" XName="task_to_confirm" YName="TotalCancel" Fill="#9B4330">
                                <Marker>
                                    <DataLabel Angle="300" EnableContrastColor="true" Visible="true" TextPosition="Middle" />
                                </Marker>
                            </ej:Series>
                        </Series>
                        <Legend Visible="true" Position="Bottom" Shape="Rectangle" TextOverflow="None" Alignment="Center"></Legend>
                    </ej:Chart>
                </div>
            </div>
        </div>
    </div>


    <div class="col-lg-4">
        <div class="panel panel-danger">
            <div class="panel-heading">
                <i class="fa fa-tasks fa-fw"></i>My Agenda
            </div>
            <div class="panel-body">
                <div class="list-group">
                    <a href="#" id="aViewApproval" runat="server" class="list-group-item">
                        <i class="fa fa-edit fa-fw"></i><span class="text-danger">LOI Approval</span>
                        <span class="pull-right text-danger medium">
                            <asp:Literal ID="ltrApprovalPendingCount" runat="server">0</asp:Literal>
                        </span>
                    </a>
                    <a href="#" id="aViewApprovalRejected" runat="server" class="list-group-item">
                        <i class="fa fa-edit fa-fw"></i><span class="text-danger">LOI Rejected Approval Pending</span>
                        <span class="pull-right text-danger medium">
                            <asp:Literal ID="ltrRejectedApprovalPendingCount" runat="server">0</asp:Literal>
                        </span>
                    </a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
