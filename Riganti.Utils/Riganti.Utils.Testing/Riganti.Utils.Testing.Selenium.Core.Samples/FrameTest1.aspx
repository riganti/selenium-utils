<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrameTest1.aspx.cs" Inherits="WebApplication1.FrameTest1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <iframe id="topframe" src="FrameTest2.aspx" frameborder="0"></iframe>
            <div id="top">
                <div id="child">child</div>
            </div>
        </div>
    </form>
</body>
</html>