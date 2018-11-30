<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="WebApplication1.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="scripts/jquery-3.3.1.min.js"></script>
    <style>
        body {
        padding:0px;margin:0px;
        }
        #box {
            height:10px;
            width:50px;
            background-color:black;
            position:absolute;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="bod">
        <div id="box" class="box" style="left:20px;top:10px"></div>
    </div>
    </form>
    <script>
        $(function () {
            $(document).mousemove(function (e) {
                var e = e || event, x = e.pageX,y = e.pageY;
                //$("#p1").text(x);
                //$("#p2").text(y);
                $(".box").css('left', x)
                $(".box").css('top', y)
                //var a = $("#box")
                //alert(a.length+"|"+document.getElementById('bod').innerHTML)
                //var doc = "<div id='box' class='box" + a.length + "'style='left:" + x + "px;top:" + y + "px'></div>";
                //document.getElementById('bod').innerHTML+=doc
            });
        });
    </script>
</body>
</html>
