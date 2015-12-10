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
        <button id="dis-button" disabled>disabled button</button>
        <input id="submit-button" type="submit" value="Submit Button Test"/>
        
            <div id="content" class="test">
            Lorem Ipsum je demonstrativní výplňový text používaný v tiskařském a knihařském průmyslu. Lorem Ipsum je považováno za standard v této oblasti už od začátku 16. století, kdy dnes neznámý tiskař vzal kusy textu a na jejich základě vytvořil speciální vzorovou knihu. Jeho odkaz nevydržel pouze pět století, on přežil i nástup elektronické sazby v podstatě beze změny. Nejvíce popularizováno bylo Lorem Ipsum v šedesátých letech 20. století, kdy byly vydávány speciální vzorníky s jeho pasážemi a později pak díky počítačovým DTP programům jako Aldus PageMaker.
        </div>
    </form>
</body>
</html>