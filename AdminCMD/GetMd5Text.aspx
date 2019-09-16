<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetMd5Text.aspx.cs" Inherits="SignalRChat1.GetMd5Text" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="scripts/jquery-3.3.1.min.js"></script>
    <%--<script src="scripts/md5.js"></script>--%>
    <script src="scripts/Md5UTF8.js"></script>
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
        .hide{display:none}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <textarea runat="server" id="KeyText"></textarea>
            <asp:Button ID="Button3" runat="server" Text="加密-->" CssClass="Button1" OnClick="Button3_Click"/>
            <asp:Button ID="Button2" CssClass="Button2" runat="server" Text="<--解密" OnClick="Button2_Click"/>
            <textarea runat="server" id="ValueText" style="width:800px;margin-left:110px"></textarea>
        </div>
        <script type="text/javascript">
            $(function () {
                $("#Button3").click(function () {
                    var Key = $("#KeyText").val()
                    $("#ValueText").val(
                        md5(Key)
                    )
                })
            })
        </script>
    </form>
</body>
</html>
