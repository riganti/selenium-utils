<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HyperLink.aspx.cs" Inherits="WebApplication1.HyperLink" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <a id="RelativeLink" href="/path/test?query=test#fragment"> test of href attribute</a>
    <a id="AbsoluteLink" href="https://www.google.com/path/test?query=test#fragment"> test of href attribute</a>
    <a id="AbsoluteSameSchema" href="//localhost:1234/path/test?query=test#fragment"> test of href attribute</a>
    </div>
    </form>
</body>
</html>
