<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmApprovalByEmailLink.aspx.cs" Inherits="Dashboard_frmApprovalByEmailLink" %>

<form runat="server">
    <style type="text/css">
        

        .modal-open {
            overflow: hidden;
        }

        .modal {
            position: fixed;
            top: 20%;
            right: 0;
            bottom: 0;
            left: 25%;
            z-index: 1050;
            /*display: none;*/
            overflow: hidden;
            -webkit-overflow-scrolling: touch;
            outline: 0;
        }

            .modal .modal-dialog {
                -webkit-transition: -webkit-transform .3s ease-out;
                -o-transition: -o-transform .3s ease-out;
                transition: transform .3s ease-out;
                -webkit-transform: translate(0, -25%);
                -ms-transform: translate(0, -25%);
                -o-transform: translate(0, -25%);
                transform: translate(0, -25%);
            }

            .modal.in .modal-dialog {
                -webkit-transform: translate(0, 0);
                -ms-transform: translate(0, 0);
                -o-transform: translate(0, 0);
                transform: translate(0, 0);
            }

        .modal-open .modal {
            overflow-x: hidden;
            overflow-y: auto;
        }

        .modal-dialog {
            position: fixed;
            width: 50%;
            margin: 10px;
            top: 20%;
        }

        .modal-content {
            position: relative;
            background-color: #fff;
            -webkit-background-clip: padding-box;
            background-clip: padding-box;
            border: 1px solid #999;
            border: 1px solid rgba(0, 0, 0, .2);
            border-radius: 6px;
            outline: 0;
            -webkit-box-shadow: 0 3px 9px rgba(0, 0, 0, .5);
            box-shadow: 0 3px 9px rgba(0, 0, 0, .5);
        }

        .modal-backdrop {
            position: fixed;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            z-index: 1040;
            background-color: #000;
        }

            .modal-backdrop.fade {
                filter: alpha(opacity=0);
                opacity: 0;
            }

            .modal-backdrop.in {
                filter: alpha(opacity=50);
                opacity: .5;
            }

        .modal-header {
            padding: 15px;
            border-bottom: 1px solid #e5e5e5;
        }

            .modal-header .close {
                margin-top: 20%;
            }

        .modal-title {
            margin: 0;
            line-height: 1.42857143;
        }

        .modal-body {
            position: relative;
            padding: 15px;
        }

        .modal-footer {
            padding: 15px;
            text-align: right;
            border-top: 1px solid #e5e5e5;
        }

            .modal-footer .btn + .btn {
                margin-bottom: 0;
                margin-left: 5px;
            }

            .modal-footer .btn-group .btn + .btn {
                margin-left: -1px;
            }

            .modal-footer .btn-block + .btn-block {
                margin-left: 0;
            }

        .modal-scrollbar-measure {
            position: absolute;
            top: -9999px;
            width: 50px;
            height: 50px;
            overflow: scroll;
        }
    </style>
    <asp:ScriptManager runat="server"></asp:ScriptManager>

    <div id="pnlRejection" runat="server" class="modal" visible="false">
        <asp:HiddenField id="hdapproverrole" runat="server"></asp:HiddenField>
        <asp:HiddenField id="hdnReqID" runat="server"></asp:HiddenField>
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <asp:Literal ID="ltrRejectLabel" runat="server">Reason of Rejection:</asp:Literal></h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <textarea class="form-control" rows="5" id="txtReasonRejection" runat="server" style="width: 100%;"></textarea>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnSubmitReject" runat="server" Text="Submit Rejection" OnClick="btnSubmitReject_Click" OnClientClick="return ValidateRemarks();" />
<%--                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                </div>
            </div>
        </div>
    </div>
</form>
