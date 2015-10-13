<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="WebApplication1._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="displayed">
            visible element
            <div>
                <p>
                    a
                </p>
                <p>
                    b
                           <span></span>
                </p>
                <p>
                    c
                    <span>This is what I want.
                    </span>
                </p>
            </div>
        </div>
        <div id="non-displayed" style="display: none;">
            Invisible element
        </div>
    </form>
</body>
</html>