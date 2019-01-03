<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm20.aspx.cs" Inherits="WebApplication1.WebForm20" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="https://cdn.staticfile.org/jquery/2.1.1/jquery.min.js"></script>
    <style>
        #TextBox1 {
            overflow:auto
        }
        #TextBox1 div {
            width:100%;
        }
        .MsgGo {
            background-color:darkturquoise;
            float:right
        }
        .SiMsg {
            background-color:#00ffff;
        }
        .Msg{
        }
        .SysSuccess {
            background-color:green;
            text-align:center;
        }
        .SysFailure {
            background-color:red;
            text-align:center;
        }
        .SysWarning {
            background-color:yellow;
            text-align:center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server"> 
        <div>
            <label>用户昵称：</label><input type="text" id="UserName" readonly="readonly" style="border:none"/><input type="button" value="确认" style="display:none" id="EditName"/>
            <select id="UserList">
                <option value="-1">在线用户</option>
            </select>
            <label id="UserNum">当前在线人数：0人</label>
        </div>
        <div> 
            <div id="TextBox1" style="height:206px;width:700px;resize:none;border:solid 1px #808080">
            </div> 
            <hr />  
            <textarea id="TextBox2" style="height:197px;width:550px;resize:none"></textarea>
            <input id="Button1" type="button" style="width:140px;height:203px"/>
        </div>
        <script>
            $(function () {
                // 打开一个 web socket 
                var ws = new WebSocket("ws://127.0.0.1:7788");
                ws.onopen = function()//连接成功时触发
                {
                    $("#TextBox1").html($("#TextBox1").html() + "<div class='SysSuccess'>连接成功</div>");
                    $("#TextBox1").scrollTop($("#TextBox1")[0].scrollHeight);
                };

                ws.onmessage = function (evt)//接收消息时触发
                {
                    var Msg = evt.data.split(':');
                    switch (Msg[0]) {
                        case "Echo":
                            var Htmls = "<div><span class='Msg'>" + Msg[1] + "</span></div><br />";
                            $("#TextBox1").html($("#TextBox1").html() + Htmls);
                            return;
                        case "username":
                            $("#UserName").val(Msg[1]);
                            return
                        case "serverlist":
                            var UserList = Msg[1].split('|');
                            var Htmls = "<option value='-1'>在线用户</option>";
                            $("#UserNum").html("当前在线人数：" + UserList.length + "人");
                            for (var i = 0; i < UserList.length; i++) {
                                if (UserList[i] != $("#UserName").val())
                                {
                                    Htmls += "<option value='" + i + "'>" + UserList[i] + "</option>";
                                }
                            }
                            $("#UserList").html(Htmls);
                            return
                        case "EchoGo":
                            var Htmls = "<span class='SiMsg'>" + Msg[1] + "</span>";
                            $("#TextBox1").html($("#TextBox1").val() + Htmls + "<br />");
                            return;
                        default:
                    }
                    $("#TextBox1").scrollTop($("#TextBox1")[0].scrollHeight);
                };

                ws.onclose = function(evt)
                {
                    $("#TextBox1").html($("#TextBox1").html() + "<div class='SysFailure'>服务器已关闭</div>");//关闭时触发
                    $("#TextBox1").scrollTop($("#TextBox1")[0].scrollHeight);
                };

                ws.onerror = function(evt)
                {
                    $("#TextBox1").html($("#TextBox1").html() + "<div class='SysWarning'>服务器未开启</div>");//连接出错时触发
                    $("#TextBox1").scrollTop($("#TextBox1")[0].scrollHeight);
                };
                $("#Button1").click(function () {
                    var msg = $("#TextBox2").val();
                    if ($("#UserList").val() == -1) {
                        var Htmls = "<span class='MsgGo'>" + msg + "</span>";
                        $("#TextBox1").html($("#TextBox1").html() + Htmls + "<br />")
                        ws.send("Msg:" + msg);
                    }
                    else {
                        var Htmls = "<span class='MsgGo'>" +"==>>"+ $("#UserList").val()+"：" + msg + "</span>";
                        $("#TextBox1").html($("#TextBox1").html() + Htmls + "<br />")
                        ws.send($("#UserList").val() + ":" + msg);
                    }
                    $("#TextBox1").scrollTop($("#TextBox1")[0].scrollHeight);
                })

                $("#UserName").dblclick(function () {
                    $("#UserName").removeAttr("readonly");
                    $("#EditName").removeAttr("style");
                })
                $("#EditName").click(function () {
                    ws.send("EditName:" + $("#UserName").val());
                    $("#UserName").attr("readonly", "readonly");
                    $("#EditName").css("display", "none")
                })
            })
        </script>
    </form>
</body>
</html>
