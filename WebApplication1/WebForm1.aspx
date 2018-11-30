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
    <div class="fom">
        <span id="fs">分数：2048</span>
        <div class="top"></div>
        <div class="left"></div>
        <div class="right"></div>
        <div class="box"></div>
        <div class="bom"></div>
    </div>
    </form>
    <style>
        body {margin:0px;padding:0px;width:100%;height:100%}
        .fom { width:600px;height:650px;margin:0px auto;margin-top:5%}
        .box {width:500px;height:500px;border:inset 5px #808080;margin:20px 45px}
            .box div {width:100px;height:100px;float:right;line-height:100px;text-align:center;font-size:42px;background-color:rgba(242, 145, 40, 0.5);border:solid 1px #808080;box-sizing: border-box;}
            /*.box div span{ width:100%;height:100%}
                .box div span:nth-child(n) {color:#f8bb78}
                .box div span:nth-child(2n+1) {color:#f29128}*/
        .left { width:30px;height:150px; float:left;margin:0px auto;margin-top:225px}
        .right { width:30px;height:150px; float:right;margin:0px auto;margin-top:225px}
        .top { width:150px;height:30px;margin-left:225px;}
        .bom { width:150px;height:30px;margin-left:225px}
        .bom, .top, .right, .left { background-color:rgba(242, 145, 40, 0.5)}
        #fs {height:20px;line-height:20px;margin-left:80%;margin-bottom:30px;font-family:"微软雅黑"}
    </style>
    <script type="text/javascript">

        $(document).keydown(function (event) {
            switch (event.which) {
                case 38:
                    $(".top").click()
                    break;
                case 40:
                    $(".bom").click()
                    break;
                case 37:
                    $(".left").click()
                    break;
                case 39:
                    $(".right").click()
                    break;
                default:
            }
        });
        $(function () {
            var htm = "";
            for (var i = 0; i < 5; i++) {
                for (var r = 0; r < 5; r++) {
                    htm += "<div name='dv'></div>"
                }
            }
            if (htm != "") {
                $(".box").html(htm)
            }

            aa()
        })
        
        $(".left").click(
            function () {
                var ddd = document.getElementsByName("dv")
                var r1="";
                for (var i = ddd.length-1; i >= 0; i--) {
                    if ((i + 1) % 5 != "") {
                        if (ddd[i + 1].innerText != "") {
                            if (ddd[i].innerText == ddd[i + 1].innerText) {
                                ddd[i + 1].innerText = ddd[i + 1].innerText * 1 + ddd[i].innerText * 1
                                r1 = i + 1;
                                ddd[i].innerText = ""
                            } 
                        }
                        else if (ddd[i].innerText == "")
                        {
                            continue;
                        }
                        else if (ddd[r1].innerText == "")
                        {
                            ddd[r1].innerText = ddd[i].innerText
                            ddd[i].innerText = ""
                        }
                        else if (ddd[r1].innerText == ddd[i].innerText) {
                            ddd[r1].innerText = ddd[i].innerText * 1 + ddd[r1].innerText * 1
                            ddd[i].innerText = ""
                        }
                        else if (ddd[r1 - 1].innerText == "")
                        {
                            ddd[r1 - 1].innerText = ddd[i].innerText
                            ddd[i].innerText = ""
                            r1 -= 1;
                        }
                        else
                        {
                            ddd[i + 1].innerText = ddd[i].innerText; ddd[i].innerText = ""
                        }
                    } 
                    else 
                    {
                        r1 = i;
                    }
                }
                aa()
            })
        
        $(".right").click(
            function () {
                var ddd = document.getElementsByName("dv")
                var r1 = "";
                for (var i = 0; i < ddd.length; i++) {
                    if ((i + 1) % 5 != 1) {
                        if (ddd[i - 1].innerText != "") {
                            if (ddd[i].innerText == ddd[i - 1].innerText) {
                                ddd[i - 1].innerText = ddd[i - 1].innerText * 1 + ddd[i].innerText * 1
                                r1 = i - 1;
                                ddd[i].innerText = ""
                            }
                        }
                        else if (ddd[i].innerText == "") {
                            continue;
                        }
                        else if (ddd[r1].innerText == "") {
                            ddd[r1].innerText = ddd[i].innerText
                            ddd[i].innerText = ""
                        }
                        else if (ddd[r1].innerText == ddd[i].innerText) {
                            ddd[r1].innerText = ddd[i].innerText * 1 + ddd[r1].innerText * 1
                            ddd[i].innerText = ""
                        }
                        else if (ddd[r1 + 1].innerText == "") {
                            ddd[r1 + 1].innerText = ddd[i].innerText
                            ddd[i].innerText = ""
                            r1 += 1;
                        }
                        else {
                            ddd[i - 1].innerText = ddd[i].innerText; ddd[i].innerText = ""
                        }
                    }
                    else {
                        r1 = i;
                    }
                }
                aa()
            })

        $(".top").click(
            function () {
                var ddd = document.getElementsByName("dv")
                var r1 = "";
                for (var j = 0; j < 5; j++) {
                    for (var i = j; i < ddd.length; i+=5) {
                        if (i > 4) {
                            if (ddd[i - 5].innerText != "") {
                                if (ddd[i].innerText == ddd[i - 5].innerText) {
                                    ddd[i - 5].innerText = ddd[i - 5].innerText * 1 + ddd[i].innerText * 1
                                    r1 = i - 5;
                                    ddd[i].innerText = ""
                                }
                            }
                            else if (ddd[i].innerText == "") {
                                continue;
                            }
                            else if (ddd[r1].innerText == "") {
                                ddd[r1].innerText = ddd[i].innerText
                                ddd[i].innerText = ""
                            }
                            else if (ddd[r1].innerText == ddd[i].innerText) {
                                ddd[r1].innerText = ddd[i].innerText * 1 + ddd[r1].innerText * 1
                                ddd[i].innerText = ""
                            }
                            else if (ddd[r1 + 5].innerText == "") {
                                ddd[r1 + 5].innerText = ddd[i].innerText
                                ddd[i].innerText = ""
                                r1 += 5;
                            }
                            else {
                                ddd[i - 5].innerText = ddd[i].innerText; ddd[i].innerText = ""
                            }
                        }
                        else {
                            r1 = i;
                        }
                    }
                }
                aa()
            })

        $(".bom").click(
            function () {
                var ddd = document.getElementsByName("dv")
                var r1 = "";
                for (var j = ddd.length - 1; j > ddd.length - 6; j --) {
                    for (var i = j; i >=0; i-=5) {
                        if (i < ddd.length - 5) {
                            if (ddd[i + 5].innerText != "") {
                                if (ddd[i].innerText == ddd[i + 5].innerText) {
                                    ddd[i + 5].innerText = ddd[i + 5].innerText * 1 + ddd[i].innerText * 1
                                    r1 = i + 5;
                                    ddd[i].innerText = ""
                                }
                            }
                            else if (ddd[i].innerText == "") {
                                continue;
                            }
                            else if (ddd[r1].innerText == "") {
                                ddd[r1].innerText = ddd[i].innerText
                                ddd[i].innerText = ""
                            }
                            else if (ddd[r1].innerText == ddd[i].innerText) {
                                ddd[r1].innerText = ddd[i].innerText * 1 + ddd[r1].innerText * 1
                                ddd[i].innerText = ""
                            }
                            else if (ddd[r1 - 5].innerText == "") {
                                ddd[r1 - 5].innerText = ddd[i].innerText
                                ddd[i].innerText = ""
                                r1 -= 5;
                            }
                            else {
                                ddd[i + 5].innerText = ddd[i].innerText; ddd[i].innerText = ""
                            }
                        }
                        else {
                            r1 = i;
                        }
                    }
                }
                aa()
            })

        function aa() {
            var ddd = document.getElementsByName("dv")
            var dd1 = new Array();
            var tes=new Array([2,4,8,16,32,64,128,256,512,1024,2048])
            var d2 = 0;
            var d3 = 2;
            for (var i = 0; i <= ddd.length-1; i++) {
                if (ddd[i].innerText == "") {
                    dd1.push([i])
                } else { if (ddd[i].innerText > d3) { d3 = ddd[i].innerText } }
            }
            if (dd1.length == ddd.length) {
                d2 = 5
            } else if (dd1.length > 1) {
                d2 = 2
            } else { d2 = dd1.length }
            var rs = 0;
            for (var i = 0; i >= 0; i++) {
                var sr=0;
                if (dd1.length != 2) {
                    sr = Math.floor(Math.random() * (dd1.length - 1));
                } else { sr=i}
                var sj = dd1[sr]
                if (rs != d2) {
                    if (ddd[sj].innerText == "") {
                        for (var j = 1; j > 0; j++) {
                            var cc = Math.floor(Math.random() * d3+1)
                            var testStr = ',' + tes.join(",") + ",";
                            if (testStr.indexOf("," + cc + ",") != -1) {
                                ddd[sj].innerText = cc;
                                rs++
                                j = -1;
                            }
                        }
                    }
                } else { break}
            }
        }
    </script>
</body>
</html>
