<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm11.aspx.cs" Inherits="WebApplication1.WebForm11" %>

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
        body > form{width:100%; height:100%; overflow:hidden; margin:0;padding:0;}
        .leftlist {
            width:25%;
            height:100%;
            float:left
        }
            .leftlist > ul {
                height:100%;
                width:100%
            }
            .leftlist > ul > li {
                list-style: none;
                height:10%;
                width:100%;
                line-height:400%;
                background-color:#808080;
                box-sizing:border-box;
            }
                .leftlist > ul > li:hover {
                background-color:#fff;
                
                }
        .rightMain {
            height:100%;
            width:70%;
            float:right;
            overflow:auto
            
        }
        /*.main {
        height:auto
        }*/
        .cli {
        background-color:#fff !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="leftlist">
            <ul>
                <li class="a0">0</li>
                <li class="a1">1</li>
                <li class="a2">2</li>
                <li class="a3">3</li>
                <li class="a4">4</li>
                <li class="a5">5</li>
                <li class="a6">6</li>
                <li class="a7">7</li>
                <li class="a8">8</li>
                <li class="a9">9</li>
            </ul>
        </div>

        <div class="rightMain">
                <h2 id="a0">00000</h2>
                <div style="height:500px;width:100%">00000</div>
                <h2 id="a1">11111</h2>
                <div style="height:500px;width:100%">11111</div>
                <h2 id="a2">22222</h2>
                <div style="height:500px;width:100%">22222</div>
                <h2 id="a3">33333</h2>
                <div style="height:500px;width:100%">33333</div>
                <h2 id="a4">44444</h2>
                <div style="height:500px;width:100%">44444</div>
                <h2 id="a5">55555</h2>
                <div style="height:500px;width:100%">55555</div>
                <h2 id="a6">66666</h2>
                <div style="height:500px;width:100%">66666</div>
                <h2 id="a7">77777</h2>
                <div style="height:500px;width:100%">77777</div>
                <h2 id="a8">88888</h2>
                <div style="height:500px;width:100%">88888</div>
                <h2 id="a9">99999</h2>
                <div style="height:500px;width:100%">99999</div>
        </div>
    </form>
    <script>
        $(function () {
            var TopHight = 0;
            var TopHights = [];
            var stop;
            $(".leftlist>ul>li").eq(0).addClass("cli");
            $(".leftlist>ul>li").click(function () {
                var ul = $(".leftlist>ul").filter(function () {return $(this).find('li:last').hasClass('cli')});
                if (ul.length == 1) {
                    var clis = $(".cli").attr("class")
                    TopHight -= $("#" + clis.substring(0, clis.length - 4)).offset().top
                }
                var clis = $(".cli").attr("class")
                //alert($("#" + clis.substring(0, clis.length - 4)).offset().top);
                $(this).siblings().removeClass("cli")
                var mid = $(this).attr("class")
                TopHight += $("#" + mid).offset().top
                $(".rightMain").animate({ scrollTop: TopHight }, 500)
                $(this).addClass("cli")
            })
        })
        $(".rightMain").scroll(function() {
            $(".rightMain>h2").each(function () {
                var mids = $(this).attr("id");
                if ($(this).offset().top > -50) {
                    //alert(mids + "#" + $(this).offset().top)
                    $(".leftlist>ul>li").removeClass("cli");
                    $("." + mids).addClass("cli")
                    return false;
                }
            })
        })
    </script>
</body>
</html>
