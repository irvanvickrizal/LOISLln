﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/admin.master" CodeFile="frmUploadAgreement.aspx.cs" Inherits="Dashboard_frmUploadAgreement" %>

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

        /*.hiddencol {
            display: none;
        }*/

        /*input[type="file"] {
            -webkit-appearance: none;
            text-align: left;
            -webkit-rtl-ordering: left;
        }*/

        /*#ContentSection_uploadFile {
            text-align: left;
            margin: auto;
        }*/

        input[type="file"]::-webkit-file-upload-button {
            opacity:0;
            /*display:none;*/
            /*-webkit-appearance: none;
                float: right;
                margin: 0 0 0 10px;
                border: 1px solid #aaaaaa;
                border-radius: 4px;
                background-image: -webkit-gradient(linear, left bottom, left top, from(#d2d0d0), to(#f0f0f0));
                background-image: -moz-linear-gradient(90deg, #d2d0d0 0%, #f0f0f0 100%);*/
        }

        .form-control2 {
            height: 34px;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
        }
    </style>
    <script src="https://cdn.syncfusion.com/js/assets/external/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="https://cdn.syncfusion.com/16.1.0.24/js/web/ej.web.all.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function Checkboxchecked() {
            document.getElementById("btnChkReviewChecked").click();
        }

        function submitconfirmation() {
            return confirm('Are you sure you want to save it?');
        }

        $(document).ready(function () {
            $('h1.page-header').html('Agreement');
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="Server">
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div class="panel panel-info" style="width: 100%">
                <div class="panel-heading">
                    <asp:Label ID="Label7" runat="server" Font-Bold="true" Text='Project Agreement' CssClass="h5" />
                </div>
                <div class="panel-body">
                    <div class="row" style="padding-top: 10px;">
                        <div class="col-md-2" style="text-align: right">
                            <asp:Label runat="server" ID="Label1" CssClass="control-label h5" Text='Project Agreement No' Style="text-align: left;" />
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox type="text" CssClass="form-control " runat="server" ID="txtProjectAgreementNo" Font-Size="14px" />
                        </div>
                        <div class="col-md-2" style="text-align: right">
                            <asp:Label runat="server" ID="Label2" CssClass="control-label h5" Text='Project Name' Style="text-align: right" />
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control" Width="100%">
                                <asp:ListItem Text="-- All Project --" Value="ALL" Selected></asp:ListItem>
                                <asp:ListItem Text="ISAT" Value="ISAT"></asp:ListItem>
                                <asp:ListItem Text="H3I" Value="H3I"></asp:ListItem>
                                <asp:ListItem Text="TSEL" Value="TSEL"></asp:ListItem>
                                <asp:ListItem Text="SF" Value="SF"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row" style="padding-top: 10px">
                        <div class="col-md-2" style="text-align: right">
                            <asp:Label runat="server" ID="Label3" CssClass="control-label h5" Text='Valid From' Style="text-align: left" />
                        </div>
                        <div class="col-md-4">
                            <input type="text" id="txtvalidfrom" runat="server" onblur="(this.type='text')" onfocus="(this.type='date')" placeholder="valid from" class="form-control" style="width: 100%" />
                        </div>
                        <div class="col-md-2" style="text-align: right">
                            <asp:Label runat="server" ID="Label4" CssClass="control-label h5" Text='Valid To' Style="text-align: right" />
                        </div>
                        <div class="col-md-4">
                            <input type="text" id="txtvalidto" runat="server" onblur="(this.type='text')" onfocus="(this.type='date')" placeholder="valid to" class="form-control" style="width: 100%" />
                        </div>
                    </div>
                    <div class="row" style="padding-top: 10px">
                        <div class="text-right" style="padding-right: 12px;">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" OnClientClick="return submitconfirmation();" Width="100" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="panel panel-info" style="width: 100%">
                <div class="panel-heading">
                    <asp:Label ID="Label5" runat="server" Font-Bold="true" Text='Price Agreement' CssClass="h5" />
                </div>
                <div class="panel-body">
                    <div class="row" style="padding-top: 10px;">
                        <div class="col-md-2" style="text-align: right">
                            <asp:Label runat="server" ID="Label6" CssClass="control-label h5" Text='Attach Price Agreement' Style="text-align: left;" />
                        </div>
                        <div class="col-md-4">
<%--                            <input type="file" id="uploadFile" accept=".pdf" style="width: 100%" onchange="$('#ContentSection_txtfilename').val($('#uploadFile').val());" />--%>
<%--                            <input type="text" id="txtfilename" runat="server" placeholder="pilih file" class="form-control2" style="width: 80%" />
                            <button class="form-control2" style="width: 18%; background-color: ActiveCaption" onclick="$('#ContentSection_uploadFile').trigger('click');">...</button>--%>
                            <asp:FileUpload ID="uploadFile" accept=".pdf" runat="server" class="form-control" />
                        </div>
                        <div class="col-md-2" style="text-align: right">
                            <asp:Label runat="server" ID="Label8" CssClass="control-label h5" Text='Project Name' Style="text-align: right" />
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlProjectNamePriceAgrr" runat="server" CssClass="form-control" Width="100%">
                                <asp:ListItem Text="-- All Project --" Value="ALL" Selected></asp:ListItem>
                                <asp:ListItem Text="ISAT" Value="ISAT"></asp:ListItem>
                                <asp:ListItem Text="H3I" Value="H3I"></asp:ListItem>
                                <asp:ListItem Text="TSEL" Value="TSEL"></asp:ListItem>
                                <asp:ListItem Text="SF" Value="SF"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row" style="padding-top: 10px">
                        <div class="col-md-2" style="text-align: right">
                            <asp:Label runat="server" ID="Label9" CssClass="control-label h5" Text='Valid From' Style="text-align: left" />
                        </div>
                        <div class="col-md-4">
                            <input type="text" id="txtValidFromPriceAgrr" runat="server" onblur="(this.type='text')" onfocus="(this.type='date')" placeholder="valid from" class="form-control" style="width: 100%" />
                        </div>
                        <div class="col-md-2" style="text-align: right">
                            <asp:Label runat="server" ID="Label10" CssClass="control-label h5" Text='Valid To' Style="text-align: right" />
                        </div>
                        <div class="col-md-4">
                            <input type="text" id="txtValidToPriceAgrr" runat="server" onblur="(this.type='text')" onfocus="(this.type='date')" placeholder="valid to" class="form-control" style="width: 100%" />
                        </div>
                    </div>
                    <div class="row" style="padding-top: 10px">
                        <div class="text-right" style="padding-right: 12px;">
                            <asp:Button ID="btnSubmitPriceAgrr" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmitPriceAgrr_Click" OnClientClick="return submitconfirmation();" Width="100" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>