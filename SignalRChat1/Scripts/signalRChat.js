var UserName = Cookies.get("UserName");
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
//接收私聊消息
chat.client.broadcastMessageGo = function (name, message) {
    var Htmls = "<li class=\"Msg\"><span>" + name + "：<span class=\"MsgContent\">" + message + "</span></span></li>";
    $("#ShowMessage ul").append(Htmls)
};
//接收所有在线用户的方法
chat.client.getUsers = function (data) {
    if (data) {
        var json = $.parseJSON(data);
        var html = "";
        //$("#UserNum").html(json.length + 1 + "人在线");
        for (var i = 0; i < json.length; i++) {
            if (json[i].Name == $('#UserName').val()) {
                break;
            }

            html += "<li class=\"mui - table - view - cell\"><a class=\"mui - navigate - right\">";
            html += json[i].Name;
            html += "</a></li>";
        }
        $("#offCanvasSideScroll .contentList .UserLisr").html(html)
    }
}
//重连时接收昵称
chat.client.getName = function (name) {
    UserName = name;
    $('#UserName').val(name);
}
//接收所有分组
chat.client.getGroups = function (data) {
    if (data) {
        var json = $.parseJSON(data);
        var html = "";
        //$("#UserNum").html(json.length + 1 + "人在线");
        for (var i = 0; i < json.length; i++) {
            html += "<li class=\"mui - table - view - cell\"><a class=\"mui - navigate - right\">";
            html += json[i].Name + "("+json[i][1]+")";
            html += "</a></li>";
        }
        $("#offCanvasSideScroll .contentList .UserLisr").html(html)
    }
}
//接收系统提示
chat.client.systemNotification = function (text) {
    alert(text)
}
//接收群组系统提示
chat.client.groupSystem = function (text) {
    var html = "<li class=\"AddGroupTitle\"><span class=\"GroupTitleContent\">" + text + "</span></li>"
    $("#ShowMessage ul").append(html)
}
// 启动连接
$.connection.hub.start().done(function () {
    
});
//发送消息
function SendMessage(msg) {
    chat.server.send(msg);
}
//发送私聊消息
function SendMessageGo(msg,name) {
    chat.server.sendGo(msg, name);
}
//修改昵称
function EditName(name) {
    chat.server.editName(name);
}
//创建群组
function CreatGroup(name) {
    chat.server.creatGroup(name);
}
//加入群组
function AddGroup(name) {
    chat.server.addGroup(name);
}
//退出组，当全部退出时删除组
function DelGroup(name) {
    chat.server.delGroup(name);
}



