<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script src="scripts/jquery-3.3.1.min.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input type="text" id="word" size="40"/> <input type="button" id="ok" value="搜索"/>
    </div>
    </form>
    <script>
        $("#ok").click(function () {
            location.href = "https://www.baidu.com/s?word=" + $("#word").val();
        })
    </script>
</body>
</html>
