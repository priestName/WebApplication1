<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm22.aspx.cs" Inherits="WebApplication1.WebForm22" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script src="https://cdn.staticfile.org/jquery/2.1.1/jquery.min.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <iframe src ="https://www.hao123.com/" width="500" height="500" frameborder="0"></iframe>
        <div>
            <a href="#">https://tieba.baidu.com/index.html</a>
            <a href="#">https://www.baidu.com/</a>
            <a href="#">https://www.hao123.com/</a>
        </div>
    </div>
<style type="text/css">
    a {
        margin-right:20px;
    }
</style>
<script type="text/javascript">
    $("a").click(function () {
        $("iframe").attr("src", $(this).html())
    })
</script>
    </form>
</body>
</html>
