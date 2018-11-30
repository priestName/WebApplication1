<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm12.aspx.cs" Inherits="WebApplication1.WebForm12" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="scripts/jquery-3.3.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input type="button" value="zi" id="Button1" runat="server" onserverclick="Button1_ServerClick"/>
        <div runat="server" id="divs" style="width:100%;height:100%;"></div>
        <br />
    </div>
    </form>
    <script>
        function fil() {
            var a = document.getElementById("files")
            alert(this.files[0])
        }
    </script>
</body>
</html>
