<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ElementSelectionTest.aspx.cs" Inherits="WebApplication1.ElementSelectionTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
.second-child, .third-child {
    padding-left: 20px;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="top">
            <div class="top-child">
                level 1
            </div>
            <div class="top-child">
                level 1
            </div>
            <div class="top-child" id="top-element">
                <div class="second-child">
                    level 2
                </div>
                <div class="second-child">
                    level 2
                </div>
                <div class="second-child">
                    <div class="third-child">
                        level 3
                    </div>
                    <div class="third-child">
                        level 3
                    </div>
                </div>
                <div class="second-child">
                    level 2
                </div>
            </div>
            <div class="top-child">
                level 1
            </div>
            <div class="top-child">
                level 1
            </div>
        </div>
    </form>
</body>
</html>
