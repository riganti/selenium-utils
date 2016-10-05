<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Confirm.aspx.cs" Inherits="WebApplication1.Confirm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <span id="message"></span>
                <input id="button" type="button" value="click" />
            </div>
            <script>
                function click(e) {
                    e.preventDefault();
                    var span = document.querySelector("#message");
                    if (confirm("confirm test")) {
                        span.textContent = span.innerText = "Accept";
                        
                    } else {
                        span.textContent = span.innerText = "Dismiss";
                    }
                    return null;
                }
                document.getElementById("button").onclick = click;
            </script>
        </div>
    </form>
</body>
</html>
