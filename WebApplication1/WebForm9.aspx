<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm9.aspx.cs" Inherits="WebApplication1.WebForm9" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        *{
            margin:0px;
            padding:0px;
        }
        html,body{height:100%; width:100%; overflow:hidden; margin:0;padding:0;}
        body > form{width:100%; height:100%; overflow:hidden; margin:0;padding:0;}
        #msgbox {
        height: 100%;
        width: 100%;
        }
        #msgss{
            height:70%;
            width:100%;
            display:block;
            
        }
        #msgshw{
            height:30%;
            width:79%;
            display:inline;
        }
        #msgok {
        width:20%;
        height:30%;
        display:inline;
        float:right;
        }
        #Button1 {
            height:100%;
            width:76%;
        }
        #Button2 {
            height:100%;
            width:20%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="msgbox">
            <textarea id="msgss" readonly="readonly" runat="server"></textarea>
            <textarea id="msgshw" runat="server"></textarea>
            <div id="msgok">
                <asp:Button ID="Button1" runat="server" Text="go" OnClick="Button1_Click"/>
                <asp:Button ID="Button2" runat="server" Text="ok" OnClick="button2_Click"/>
            </div>
        </div>
    </form>
    <script>


    </script>
</body>
</html>
