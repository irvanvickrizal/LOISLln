<%@ Control Language="C#" AutoEventWireup="true" CodeFile="navbar.ascx.cs" Inherits="controls_navbar" %>

<nav class="navbar navbar-default navbar-static-top" role="navigation" style="margin-bottom: 0">
    <asp:ScriptManager ID="ScriptManager1"
        runat="server">
    </asp:ScriptManager>

    <div class="navbar-header">
        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
        </button>

        <asp:UpdatePanel ID="UpdatePanel1" ChildrenAsTriggers="false" UpdateMode="Conditional"
            runat="server">
            <ContentTemplate>
                <%--<asp:Timer ID="Timer1" runat="server" Interval="60000" OnTick="Timer1_Tick">
                </asp:Timer>--%>
                <asp:LinkButton ID="lbtHomeDashboard" runat="server" class="navbar-brand" OnClick="lbtHomeDashboard_Click">
                    <span style="font-size: 12px;">Current Time : </span>
                    <%--<asp:Label ID="Label1" runat="server" Font-Size="12px" Font-Bold="true"></asp:Label>--%>
                    <label id="lbltime" style="font-size:small"/>
                    <br />
                </asp:LinkButton>

            </ContentTemplate>
            <%--<Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick"></asp:AsyncPostBackTrigger>
            </Triggers>--%>
        </asp:UpdatePanel>

    </div>
    <ul class="nav navbar-top-links navbar-right">
        <asp:Literal ID="ltrnavbartop" runat="server"></asp:Literal>
    </ul>

    <div class="navbar-default sidebar" role="navigation">
        <div class="sidebar-nav navbar-collapse">
            <ul class="nav" id="side-menu">
                <li class="sidebar-search">
                    <img src="../skins/nokia white.png" width="130px" class="pull-left" />
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lbtHomeDashboard_Click"><i class="fa fa-home fa-pull-right fa-2x"></i></asp:LinkButton>
                </li>
                <asp:Literal id="ltrnavbarleft" runat="server"></asp:Literal>
            </ul>
		<div style="bottom:0; position: fixed; background-color:#3367D5; padding:5px; border-top:solid 1px #fff; width:250px; ">
			<img src="../skins/nokia.png" width="180px" class="pull-left" />
			<%--<img src="../skins/3.png" width="25px" class="pull-right" />--%>
		</div>	
        </div>
    </div>
</nav>
