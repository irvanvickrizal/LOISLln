<%@ Page Title="" Language="C#" MasterPageFile="~/login.master" AutoEventWireup="true" CodeFile="Logout.aspx.cs" Inherits="Logout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="Server">
    <div class="container">
        <div class="row">
            <div class="col-md-4 col-md-offset-4">
                <div class="login-panel panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Logout</h3>
                    </div>
                    <div class="panel-body">
                        <form role="form">
                            <fieldset>
                                <asp:MultiView ID="mvPanelLogout" runat="server">
                                    <asp:View ID="vwSessionTimeout" runat="server">
                                        <div class="form-group">
                                            <span class="h3">Your Session is time out, Please <a href="Login.aspx"> click here</a> to go to Login Form</span>
                                        </div>
                                    </asp:View>
                                    <asp:View ID="vwLogout" runat="server">
                                        <div class="form-group">
                                            <span class="h3">
                                                <span class="h3">Thank you for using eLoi, If you want to go to your dashboard, Please <a href="Login.aspx"> click here</a> to go to Login Form</span>
                                            </span>
                                        </div>
                                    </asp:View>
                                </asp:MultiView>
                            </fieldset>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

