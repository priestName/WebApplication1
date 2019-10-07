var UserName = "test"//Cookies.get("UserName");
while (UserName == null || UserName == "" || UserName == false) {
    UserName = prompt('输入你的昵称:', '');
}
$('.UserName').val(UserName);
Cookies.set("UserName", UserName)

// 声明一个代理以引用集线器
var chat = $.connection.chatHub;
// 创建接收消息的方法
chat.client.broadcastMessage = function (name, message) {
    var Htmls = "<li class=\"Msg\"><span>" + name + "：<span class=\"MsgContent\">" + message + "</span></span></li>";
    $("#ShowMessage ul").append(Htmls)
};
chat.client.broadcastMessageGo = function (name, message) {
    var Htmls = "<li class=\"Msg\"><span>" + name + "：<span class=\"MsgContent\">" + message + "</span></span></li>";
    $("#ShowMessage ul").append(Htmls)
};
//创建接收所有在线用户的方法
chat.client.getUsers = function (data) {
    if (data) {
        var json = $.parseJSON(data);
        var html = "";
        //$("#UserNum").html(json.length + 1 + "人在线");
        for (var i = 0; i < json.length; i++) {
            if (json[i].Name == $('#UserName').val()) {
                break;
            }
            html += "<li class=\"mui-table-view-cell\"><a class=\"mui - navigate - right\">";
            html += json[i].Name;
            html += "</a></li>";
        }
        $("#offCanvasSideScroll .contentList .UserLisr").html(html)
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
function SendMessage(msg) {
    chat.server.send(msg);
}
function SendMessageGo(msg,name) {
    chat.server.sendGo(msg, name);
}
function EditName(name) {
    chat.server.editName(name);
}


//function cookie(name) {
//    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
//    if (arr = document.cookie.match(reg))
//        return arr[2];
//    else
//        return "";
//}
