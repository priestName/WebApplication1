var UserName = Cookies.get("UserName");
while (UserName == null || UserName == "" || UserName == false) {
    UserName = prompt('输入你的昵称:', '');
}
$('#UserName').val(UserName);
Cookies.set("UserName", UserName)

// 声明一个代理以引用集线器
var chat = $.connection.chatHub;
// 创建接收消息的方法
chat.client.broadcastMessage = function (name, message,isgo) {
    var Htmls = "";
    if (isgo == 0) {
        if (name = $('#UserName').val())
            Htmls = "<li><span class='MsgGo'>" + name + "：" + message + "</span></li>";
        else
            Htmls = "<li><span class='Msg'>" + name + "：" + message + "</span></li>";
    } else {
        Htmls = "<li  class='SiMsg'><span class='Msg'>" + name + "==>" + message + "</span></li>";
    }
    $("#ShowMessage ul").append(Htmls)
};
//创建接收所有在线用户的方法
chat.client.getUsers = function (data) {
    if (data) {
        var json = $.parseJSON(data);
        $("#UserList").html("<option value='-1'>在线用户</option>");
        $("#UserNum").html(json.length + 1 + "人在线");
        for (var i = 0; i < json.length; i++) {
            if (json[i].Name == $('#UserName').val()) {
                break;
            }
            $("#UserList").append("<option value='" + json[i].ConnectionID + "'>" + json[i].Name + "</option>");
        }
    }
}
chat.client.getName = function (name) {
    UserName = name;
    $('#UserName').val(name);
}
chat.client.closeSignalr = function (stopCalled) {
    chat.server.closeSignalr(stopCalled);
}
// 启动连接
$.connection.hub.start().done(function () {
    
});

$('#sendmessage').click(function () {
    // 调用集线器上的send方法
    var msg = $("#message").html().replace(/<div><br><\/div>/g, "")
    if (msg.length == 0) {
        $("#TextBox2").html("")
        return;
    }
    if ($("#UserList").val() == -1) {
        chat.server.send($('#UserName').val(), msg,"");
    }
    else {
        var Htmls = "<li class='MsgGo'><span class='MsgGo'>" + "==>>" + $("#UserList").text().replace(/^\s+|\s+$/g, "") + "：" + msg + "</span></li>";
        $("#ShowMessage ul").append(Htmls)
        chat.server.send($('#UserName').val(), msg, $("#UserList").val())
    }
    $("#ShowMessage").scrollTop($("#ShowMessage")[0].scrollHeight);
    
    $("#message").html("")
});
$("#EditName").click(function () {
    chat.server.editName($('#UserName').val());
    $("#UserName").attr("readonly", "readonly");
    $("#EditName").css("display", "none")
})


//function cookie(name) {
//    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
//    if (arr = document.cookie.match(reg))
//        return arr[2];
//    else
//        return "";
//}
