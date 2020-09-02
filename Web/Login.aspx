<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login"
    MasterPageFile="~/login.master" %>


<asp:Content ID="ContentSection" runat="server" ContentPlaceHolderID="ContentSection">
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <input class="form-control" placeholder="Username" id="txtUsername" runat="server"
                type="text" />
            <input class="form-control" placeholder="Password" id="txtPassword" runat="server"
                type="password" value="" />
            <button ID="lbtLogin" runat="server" type="submit"  onserverclick="lbtLogin_Click">Login</button>
            <asp:Label ID="lbltest" runat="server"></asp:Label>
            <div class="messagealert" id="alert_container">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
