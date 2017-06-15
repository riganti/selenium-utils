<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CookiesTest.aspx.cs" Inherits="WebApplication1.CookiesTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label Text="" ID="CookieIndicator" runat="server" />
        <asp:Button Text="Set Cookies" id="SetCookies" OnClick="SetCookies_OnClick" runat="server" />
    </div>
    </form>
</body>
</html>
