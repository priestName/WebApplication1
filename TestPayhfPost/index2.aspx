<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index2.aspx.cs" Inherits="TestPayhfPost.index2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        p {
        margin:0 0;
        }
        a {
        margin-left:20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <a href="#" onclick="PathS('Log')">Log</a><a href="#" onclick="PathS('OldLog')">OldLog</a><a href="#" onclick="PathS('ErroLog')">ErroLog</a><br />
            <asp:HiddenField ID="PathText" runat="server" Value=""/>
            <asp:TextBox runat="server" ID="LogDate" />
            <asp:Button ID="Button1" runat="server" Text="查询" OnClick="Page_Load"/>
        </div><br />
        <div id="Log" runat="server">
            
        </div>
        <script>
            function PathS(Text) {
                document.getElementById("PathText").value = Text;
            }
        </script>
    </form>
</body>
</html>
