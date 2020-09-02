<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!--  jquery script  -->
    <script src="http://cdn.syncfusion.com/js/assets/external/jquery-1.10.2.min.js"></script>

    <!-- Essential JS UI widget -->
    <script src="http://cdn.syncfusion.com/16.1.0.24/js/web/ej.web.all.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="sm1" runat="server"></asp:ScriptManager>
        <div>
            test
            <ej:Chart ID="Chart1" runat="server" Locale="en-US">
                <Series>
                    <ej:Series Type="Line" XName="Month" YName="Sales"></ej:Series>
                </Series>
            </ej:Chart>

        </div>
    </form>
</body>
</html>
