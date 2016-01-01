<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ValueTest.aspx.cs" Inherits="WebApplication1.ValueTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1 id="title">Value Test</h1>
            <input id="input-text" type="text" value="text1" />
            <input name="input-radio" id="input-radio" checked="checked" type="radio" value="radio1" />
            <input name="input-radio" id="input-radio2" type="radio" value="radio2" />
            <textarea id="area">areavalue</textarea>
            <input name="checkbox-group" id="checkbox1" type="checkbox" checked="checked" value="checkboxvalue1" />
            <input name="checkbox-group" id="checkbox2" type="checkbox" value="checkboxvalue2" />
        </div>
    </form>
</body>
</html>
