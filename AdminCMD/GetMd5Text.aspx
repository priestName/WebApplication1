<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetMd5Text.aspx.cs" Inherits="AdminCMD.GetMd5Text" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="scripts/md5.js"></script>
    <script src="scripts/jquery-3.3.1.min.js"></script>
    <style>
        .Button1 {position:absolute;
            top: 27px;
            left: 425px;
            width:100px;
            height:50px;
        }
        .Button2 {
            position:absolute;
            top: 87px;
            left: 425px;
            width:100px;
            height:50px;
        }
        textarea{height:500px;width:400px}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <textarea runat="server" id="KeyText"></textarea>
            <input type="button" id="Button1" class="Button1" value="加密-->"/>
            <asp:Button ID="Button2" CssClass="Button2" runat="server" Text="<--解密" OnClick="Button2_Click"/>
            <textarea runat="server" id="ValueText" style="width:800px;margin-left:110px"></textarea>
        </div>
        <script type="text/javascript">
            $(function () {
                $("#Button1").click(function () {
                    $("#ValueText").val(hex_md5($("#KeyText").val()))
                })
            })
        </script>
    </form>
</body>
</html>
