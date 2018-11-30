﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm5.aspx.cs" Inherits="WebApplication1.WebForm5" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="scripts/jquery-3.3.1.min.js"></script>
    <style>
        *{
            margin:0px;
            padding:0px;
        }
        html,body{height:100%; width:100%; overflow:hidden; margin:0;padding:0;}
        body > div{width:100%; height:100%; overflow:hidden; margin:0;padding:0;}
        #login {
        width:260px;
        height:50px;
        }
        #logins {
        height:50px;
        width:84px;
        float:right;
        }
        #psd {
        margin-top:10px
        }
        
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
    </style>
</head>
<body>
    <div>
        <div id="login">
            <input type="button" id="logins"/><input type="text" id="user"/>
            <input type="password" id="psd"/>
        </div>
        <div id="msgbox" style="display:none">
            <textarea id="msgss" readonly="readonly"></textarea>
            <textarea id="msgshw"></textarea><input id="msgok" type="button" value="ok"/>
        </div>
    </div>
    <script>
        var socket;
        
        $(function () {
            alert(cookie("Uid") + "|" + cookie("Psd"))
            if (cookie("Uid")!=null && cookie("Psd")!=null)
            {
                $("#login").css("display", "none")
                $("#msgbox").css("display", "block")
                var myDate = new Date()
                $("#msgss").text(myDate.toLocaleString())

                socket = new WebSocket("192.168.3.251:6000")
                socket.onopen;
                socket.onmessage = function (msg) { var aa = $("#msgss").val(); $("#msgss").val(aa + "\r\n" + cookie("Uid") + "：" + msg) }
                socket.onclose;
            }
        })

        $("#msgok").click(function () {
            var aa = $("#msgss").val();
            var bb = $("#msgshw").val();
            socket.send(bb);
            $("#msgss").val(aa + "\r\n" + cookie("Uid") + "：" + bb)
        })

        $("#logins").click(function () {
            $.ajax({
                url: "WebForm4.aspx",
                type: "post",
                data: {
                    ale:"login",
                    user: $("#user").val(),
                    password: $("#psd").val()
                },
                success: function (resule) {
                    if (parseInt(resule) == 1) {
                        $("#login").css("display", "none")
                        $("#msgbox").css("display", "block")
                        var myDate = new Date()
                        $("#msgss").text(myDate.toLocaleString())
                        
                        socket = new WebSocket("192.168.3.251:6000")
                        socket.onopen;
                        socket.onmessage = function (msg) { var aa = $("#msgss").val(); $("#msgss").val(aa + "\r\n" + cookie("Uid") + "：" + msg) }
                        socket.onclose;
                    } else { alert(parseInt(resule)) }
                }
            })
        })

        function cookie(name) {
            var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
            if (arr = document.cookie.match(reg))
            return arr[2];
            else
            return null;

        }
    </script>
</body>
</html>
