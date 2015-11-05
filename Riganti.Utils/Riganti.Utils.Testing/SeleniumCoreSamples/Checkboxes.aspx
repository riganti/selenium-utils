<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Checkboxes.aspx.cs" Inherits="WebApplication1.Checkboxes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:CheckBox Text="text" Checked="True" ID="checkbox1"  ClientIDMode="Static" runat="server" />
        <asp:CheckBox Text="text" ID="checkbox2"  ClientIDMode="Static" runat="server" />
    </div>
    </form>
</body>
</html>
