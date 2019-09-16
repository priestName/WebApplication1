
if (cookie("UserName").length > 0) {
    $('#UserName').val(cookie("UserName"))
} else {
    location.href="Login.html"
}
// 声明一个代理以引用集线器
var chat = $.connection.chatHub;
// 创建接收消息的方法
chat.client.broadcastMessage = function (name, message) {
    var Htmls = "<div><span class='Msg'>" + name + "：" + message + "</span></div>";
    $("#ShowMessage").append(Htmls)
};
//创建接收所有在线用户的方法
chat.client.getUsers = function (data) {
    if (data) {
        var json = $.parseJSON(data);
        console.info(json);
        $("#UserList").html("<option value='-1'>在线用户</option>");
        $("#UserNum").html(json.length + "人在线");
        for (var i = 0; i < json.length; i++) {
            if (json[i].Name == $('#UserName').val()) {
                break;
            }
            $("#UserList").append("<option value='" + json[i].ConnectionID + "'>" + json[i].Name + "</option>");
        }
    }
}
// 启动连接
$.connection.hub.start().done(function () {
    chat.server.getName($('#UserName').val());
});

$('#sendmessage').click(function () {
    // 调用集线器上的send方法
    var msg = $("#message").html().replace(/<div><br><\/div>/g, "")
    chat.server.send($('#UserName').val(), msg);
    $("#message").html("")
});



function cookie(name) {
    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
    if (arr = document.cookie.match(reg))
        return arr[2];
    else
        return "";

}
