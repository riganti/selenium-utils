<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClickTest.aspx.cs" Inherits="WebApplication1.ClickTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <script>
            function increase() {
                var input = document.getElementById("testInput");
                var value = parseInt(input.value);
                input.value = ++value;
            }
        </script>
        <button onclick="javascript: increase(); return false;">
           Click to test
        </button>
        <input type="number" id="testInput" value="0"/>
    </div>
    </form>
</body>
</html>
