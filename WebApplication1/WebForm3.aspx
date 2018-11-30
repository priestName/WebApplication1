<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="WebApplication1.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="scripts/jquery-3.3.1.min.js"></script>
    <style>
        body{
            margin:0px;padding:0px;
        }
        #aa {
        width:310px;
        height:335px;
        margin:0px auto;
        margin-top:10%;
        }
        #box {
        width:300px;
        height:300px;
        border:inset 5px #808080;
        margin:0px auto;
        padding:0px
        }
        .ds {
        float:left;
        width:100px;
        height:100px;
        line-height:100px;
        text-align:center;
        font-size:24px;
        }
        #f {
        width:310px;height:24px;padding:0px;margin:0px;
        }
        /*.ds:nth-child(n) {background-color:#f8bb78}
        .ds:nth-child(2n+1) {background-color:#f29128}*/
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="aa">
        <p id="f">0</p>
        <div id="box">
            <div name="s" class="ds"></div>
            <div name="s" class="ds"></div>
            <div name="s" class="ds"></div>

            <div name="s" class="ds"></div>
            <div name="s" class="ds"></div>
            <div name="s" class="ds"></div>

            <div name="s" class="ds"></div>
            <div name="s" class="ds"></div>
            <div name="s" class="ds"></div>
        </div>
    </div>
    </form>
    <script>
       $(function () {
           lc();
        })
        $(".ds").click(function () {
            if (this.innerText == "1") {
                $("#f").text = Number($("#f").text) + 1
                this.innerText = "";
                lc();
            }
        })

        function lc () {
            var mf = Math.floor(Math.random() * 9 + 1)
            var dds = document.getElementsByName("s");
            dds[mf].innerText="1";
        }
    </script>
</body>
</html>
