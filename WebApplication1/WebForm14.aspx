<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm14.aspx.cs" Inherits="WebApplication1.WebForm14" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="scripts/jquery-3.3.1.min.js"></script>
    <style>
        #test {
        width:10px;
        height:10px;
        background-color:#000;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>hehe
    <div id="texts" runat="server"></div>
    </div>
        <div id="test"></div>
    </form>
    <script>
        setInterval('var tim = new Date();$("#test").html(tim.getHours() + ":" + tim.getMinutes() + ":" + tim.getSeconds())', 1000);
    </script>
</body>
</html>
