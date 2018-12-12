<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm18.aspx.cs" Inherits="WebApplication1.WebForm18" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script src="https://cdn.staticfile.org/jquery/2.1.1/jquery.min.js"></script>
    <title></title>
    <style>
        html,body,form{height:100%; width:100%; margin:0;padding:0;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="height:100%;width:100%;">
            <div id="mune">
                <span url="WebForm1.aspx">2048</span><br />
                <span url="WebForm15.aspx">55555</span><br />
                <span url="https://www.baidu.com">bbbbb</span>
            </div>
            <iframe src="WebForm1.aspx" style="height:100%;width:100%" id="mas" runat="server" onscroll="yes" frameborder="0" ></iframe>
        </div>
    </form>
    <script>
        $("#mune span").click(function () {
            $("#mas").attr("src", $(this).attr("url"))
        })
    </script>
</body>
</html>
