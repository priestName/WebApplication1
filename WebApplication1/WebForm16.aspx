<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm16.aspx.cs" Inherits="WebApplication1.WebForm16" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script src="scripts/jquery-3.3.1.min.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="fom">
        <div class="left"><div class="left1"></div></div>
        <div class="right"><div class="right1"></div></div>
        <div class="box"></div>
    </div>
        <div class="jieju" ><div class="jus"></div></div>
    <div>
        <span>规格(单行的格数)：</span><input type="text" id="sizes" />
        <span>&nbsp&nbsp&nbsp</span>
        <input type="button" value="开  始" id="ok"/>
    </div>
    </form>
    <style>
        body {margin:0px;padding:0px;width:100%;height:100%}
        .fom { width:600px;height:550px;margin:0px auto;margin-top:5%}
        .box {width:500px;height:500px;border:inset 5px #808080;margin:20px 45px}
        .box div {float:right;background-color:rgba(242, 145, 40, 0.5);border:solid 1px #808080;box-sizing: border-box;}
        .left { float:left;background-color:#fff}
        .right { float:right;background-color:#000}
        .left1 { background-color:#fff}
        .right1 { background-color:#000;display:none}
        .left1, .right1 { width:30px;height:30px;float:left;position:absolute; top: -50px; left: 0px;border:solid 1px #000;border-radius: 45px;}
        .left, .right{ width:30px; height:150px; margin:0px auto; margin-top:180px; border:inset #808080 1px;position:relative}
        .qz { position:absolute;z-index:9999; border:solid 1px #000;border-radius: 45px;}
        .jieju {width:100%;height:100%;position:absolute;z-index:9999;top:0px;left:0px;background-color:rgba(0, 0, 0, 0.1);font-size:100px;text-align:center;display:none;}
        .jus {padding-top:70%;font-family:"华文行楷"}
        .dian{border-color:red !important}
    </style>
    <script type="text/javascript">
        $(function () {
            buju();
        })
        $("#ok").click(function () {
            buju();
        })
        function buju() {
            var gg = 10;
            if ($("#sizes").val() !== "") {
                gg = $("#sizes").val();
            }
            var wh = 500 / gg;
            var qzsz = wh / 2 + 5
            var htm = "";
            var qzclo = "";
            var QzArray = new Array();
            htm = "";
            for (var i = 0; i < gg; i++) {
                var array1 = new Array
                for (var r = 0; r < gg; r++) {
                    var cc = Math.floor(Math.random() * 10)
                    htm += "<div class='dv' id='dv"+i+r+"' style='width:" + wh + "px;height:" + wh + "px'>" + cc + "</div>"
                }
            }
            $(".box").html(htm)
            var x="", y="";

            $(".dv").click(function (e) {
                $(this).addClass("dian")
                if (x != "") {
                    var ht = $(this).html()
                    $(this).html($("#dv" + x).html())
                    $("#dv" + x).html(ht)
                }
                var ggaa=gg<=10?10:100;
                if (parseInt($(this).attr("id").substring(2, 4)) - 1 == parseInt(x)) {
                    alert($(this).html())
                }
                else if (parseInt($(this).attr("id").substring(2, 4)) + 1 == parseInt(x)) {
                    alert($(this).html())
                }
                else if (parseInt($(this).attr("id").substring(2, 4)) - ggaa == parseInt(x)) {
                    alert($(this).html())
                }
                else if (parseInt($(this).attr("id").substring(2, 4)) + ggaa == parseInt(x)) {
                    alert($(this).html())
                }
                
                if (x != "")
                {
                    $(".dian").removeClass("dian")
                }
                if (x == "") {
                    x = $(this).attr("id").substring(2, $(this).attr("id").length)
                }
                else { x=""}
            })
        }
    </script>
</body>
</html>
