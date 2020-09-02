<%@ Page Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="frmUploadLOIPrice.aspx.cs" Inherits="Dashboard_frmUploadLOIPrice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="https://cdn.syncfusion.com/js/assets/external/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="https://cdn.syncfusion.com/16.1.0.24/js/web/ej.web.all.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="Server">
    <div class="panel panel-info">
        <div class="panel-heading">
            <asp:Label ID="Label1" runat="server" Font-Bold="true" Text='Upload LOI Price' CssClass="h5" />
        </div>
        <div class="panel-body">
            <div class="row form-group">
                <div class="col-xs-5">
                    <asp:FileUpload ID="uploadFile" accept=".xlsx, .xls" runat="server" />
                </div>
            </div>
            <div class="row form-group">
                <div class="col-xs-5" style="text-align: left;">
                    <asp:Button ID="btnUploadFile" CssClass="btn btn-block btn-primary" Text="Upload File" runat="server" OnClick="btnUploadFile_Click" />
                    <asp:Button ID="btnDownloadTemplate" CssClass="btn btn-block btn-info" Text="Download Template Format (.xlsx)" runat="server" OnClick="btnDownloadTemplate_Click" />
                    <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false" />
                    <div class="clearfix">
                        <h4 />
                    </div>
                    <div class="row">
                        <asp:LinkButton ID="lblErrorUpload" Text="Total Failed: 0" runat="server" ForeColor="Red" Visible="false" OnClick="lblErrorUpload_Click"></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>