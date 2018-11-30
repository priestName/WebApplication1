<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm15.aspx.cs" Inherits="WebApplication1.WebForm15" %>

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
        <div class="jieju"><div class="jus"></div></div>
    <div>
        <span>棋盘规格(单行的格数)：</span><input type="text" id="sizes" />
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
    </style>
    <script type="text/javascript">
        $(function () {
            buju();
        })
        $("#ok").click(function () {
            buju();
        })
        function buju () {
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
                for (var r = 0; r < gg; r++) {
                    htm += "<div class='dv' style='width:" + wh + "px;height:" + wh + "px'></div>"
                }
            }
            $(".box").html(htm)
            var x, y;
            $(".dv").click(function (e) {
                var positionX = e.pageX - $(this).offset().left;
                var positionY = e.pageY - $(this).offset().top;
                if (positionX > wh / 2) {
                    if (positionY > wh / 2)
                    {
                        x = ($(this).offset().left + wh);
                        y = $(this).offset().top + wh;
                    }
                    else
                    {
                        x = $(this).offset().left + wh;
                        y = $(this).offset().top;
                    }
                }
                else {
                    if (positionY > wh / 2)
                    { x = $(this).offset().left; y = $(this).offset().top + wh }
                    else
                    { x = $(this).offset().left; y = $(this).offset().top }
                }
                x -= qzsz / 2; y -= qzsz / 2;
                var is_not1 = $.inArray(qzclo + "|" + x + "|" + y, QzArray)
                var is_not2 = $.inArray(qzclo + "|" + x + "|" + y, QzArray)
                if (is_not1 == -1 && is_not2 == -1) {
                    if (QzArray.length % 2 == 0) {
                        qzclo = "000"
                        QzArray[QzArray.length] = qzclo + "|" + x + "|" + y
                        setTimeout(function () {
                            $(".left1").css("display", "none");
                            $(".right1").css("display", "block")
                        }, 100)
                    } else {
                        qzclo = "fff"
                        QzArray[QzArray.length] = qzclo + "|" + x + "|" + y
                        setTimeout(function () {
                            $(".right1").css("display", "none")
                            $(".left1").css("display", "block");
                        }, 100)
                    }
                    var qz = "<div class='qz' style='left:" + x + "px;top:" + y + "px;background-color:#" + qzclo + ";width:" + qzsz + "px;height:" + qzsz + "px;'></div>"
                    $(".box").append(qz);
                    var h1 = h2 = s1 = s2 = zx1 = zx2 = yx1 = yx2 = 0;
                    for (var s = 0; s < 4; s++) {
                        h1 += h1 != s ? 0 : ($.inArray(qzclo + "|" + (x + parseInt(wh * (s + 1))) + "|" + y, QzArray) == -1 ? 0 : 1)//横排右
                        h2 += h2 != s ? 0 : ($.inArray(qzclo + "|" + (x - parseInt(wh * (s + 1))) + "|" + y, QzArray) == -1 ? 0 : 1)//横排左
                        s1 += s1 != s ? 0 : ($.inArray(qzclo + "|" + x + "|" + (y + parseInt(wh * (s + 1))), QzArray) == -1 ? 0 : 1)//竖排下
                        s2 += s2 != s ? 0 : ($.inArray(qzclo + "|" + x + "|" + (y - parseInt(wh * (s + 1))), QzArray) == -1 ? 0 : 1)//竖排上
                        zx1 += zx1 != s ? 0 : ($.inArray(qzclo + "|" + (x + parseInt(wh * (s + 1))) + "|" + (y + parseInt(wh * (s + 1))), QzArray) == -1 ? 0 : 1)//左斜下
                        zx2 += zx2 != s ? 0 : ($.inArray(qzclo + "|" + (x - parseInt(wh * (s + 1))) + "|" + (y - parseInt(wh * (s + 1))), QzArray) == -1 ? 0 : 1)//左斜上
                        yx1 += yx1 != s ? 0 : ($.inArray(qzclo + "|" + (x - parseInt(wh * (s + 1))) + "|" + (y + parseInt(wh * (s + 1))), QzArray) == -1 ? 0 : 1)//右斜下
                        yx2 += yx2 != s ? 0 : ($.inArray(qzclo + "|" + (x + parseInt(wh * (s + 1))) + "|" + (y - parseInt(wh * (s + 1))), QzArray) == -1 ? 0 : 1)//右斜上
                    }
                    if(h1+h2>=4 || s1+s2>=4 || zx1+zx2>=4 || yx1+yx2>=4)
                    {
                        if (QzArray.length % 2==0)
                        {
                            setTimeout(function () {
                                $(".jieju").show();
                                $(".jus").html("白子胜");
                                $(".jus").animate({ paddingTop: "25%" }, 500);
                            }, 100)
                        } else {
                            setTimeout(function () {
                                $(".jieju").show();
                                $(".jus").html("黑子胜");
                                $(".jus").animate({ paddingTop: "25%" }, 500);
                            }, 100)
                        }
                    }
                }
            })
        }
    </script>
</body>
</html>
