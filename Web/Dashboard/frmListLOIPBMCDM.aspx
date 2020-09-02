<%@ Page Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="frmListLOIPBMCDM.aspx.cs" Inherits="Dashboard_frmListLOIPBMCDM" %>

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

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="Server">
    <div class="clearfix"></div>
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div class="clearfix"></div>
            <div style="height: 20px;"></div>
                        <asp:GridView ID="gvListLOI" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="30" CssClass="table table-condensed table-bordered" OnRowCommand="gvListLOI_RowCommand"
                 OnPageIndexChanging="gvListLOI_PageIndexChanging" EmptyDataText="You have 0 Task Pending" Font-Size="12px" OnRowDataBound="gvListLOI_RowDataBound">
                <HeaderStyle BackColor="#c7c5ff" HorizontalAlign="Center" />
                <EmptyDataRowStyle CssClass="alert-warning" ForeColor="Red" />
                <Columns>
                    <asp:TemplateField HeaderText="No." HeaderStyle-CssClass="gridheader">
                        <ItemStyle Width="20px" VerticalAlign="Middle"></ItemStyle>
                        <ItemTemplate>
                            <div style="vertical-align: middle;">
                                <%# Container.DataItemIndex + 1 %>.
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="LOI_Code" HeaderText="LOI Code" HeaderStyle-CssClass="gridheader" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Uploaded_ByName" HeaderText="Uploaded By" HeaderStyle-CssClass="gridheader" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Upload_Date" HeaderText="Upload Date" HeaderStyle-CssClass="gridheader" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                    <asp:BoundField DataField="total" HeaderText="Total Site" HeaderStyle-CssClass="gridheader" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="totalPrice" HeaderText="Total Price" HeaderStyle-CssClass="gridheader" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}"/>
                    <asp:BoundField DataField="Currency" HeaderText="Currency" HeaderStyle-CssClass="gridheader" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="ProjectName" HeaderText="Project Name" HeaderStyle-CssClass="gridheader" ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField ItemStyle-Width="50px" HeaderStyle-CssClass="gridheader">
                        <ItemTemplate>
                            <div style="width: 100%; text-align: center;">
                                <asp:HiddenField ID="hdnRequestId" runat="server" Value='<%#Eval("RequestId") %>' />
                                <asp:ImageButton ID="imgUploadPrice" runat="server" ImageUrl="~/images/submission-icon.png" Width="25px" Height="25px" ToolTip="View" CommandName="UploadPrice" CommandArgument='<%#Eval("RequestId") %>' />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <div class="clearfix"></div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
