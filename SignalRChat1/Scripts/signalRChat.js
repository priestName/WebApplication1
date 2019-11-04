//Cookies.set("UserName", "Test")
var UserName = Cookies.get("UserName");

//大厅消息，私聊消息，群组消息
var msgText = new Array(), msgGoText = new Array(), msgGroupText = new Array();
// 声明一个代理以引用集线器
var chat = $.connection.chatHub;
// 创建接收消息的方法
chat.client.broadcastMessage = function (name, message) {
    if (name == UserName)
        return;
    msgText.push("{\"name\":\"" + name + "\",\"message\":\"" + message + "\",\"status\":\"1\"}");
    if ($(".SendName").val() == "") {
        var Htmls = "<li class=\"Msg\"><span>" + name + "：<span class=\"MsgContent\">" + message + "</span></span></li>";
        $(".MessageList ul").append(Htmls)
    } else {
        titlespan($(".Lobby"), "")
    }
};
//接收私聊消息
chat.client.broadcastMessageGo = function (name, message) {
    msgGoText.push("{\"Name\":\"" + name + "\",\"SendName\":\"" + UserName + "\",\"message\":\"" + message + "\",\"status\":\"1\"}");
    if ($(".SendName").val() == name) {
        var Htmls = "<li class=\"Msg\"><span>" + name + "：<span class=\"MsgContent\">" + message + "</span></span></li>";
        $(".MessageList ul").append(Htmls)
    } else {
        titlespan($(".User"), name)
    }
};
//接收群组消息
chat.client.groupMessageGo = function (name, groupname, message) {
    msgGroupText.push("{\"Name\":\"" + name + "\",\"GroupName\":\"" + groupname + "\",\"message\":\"" + message + "\",\"status\":\"1\"}");
    if ($(".SendName").val() == groupname) {
        var Htmls = "<li class=\"Msg\"><span>" + name + "：<span class=\"MsgContent\">" + message + "</span></span></li>";
        $(".MessageList ul").append(Htmls)
    } else {
        titlespan($(".Group"), name)
    }
    
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
            html += "<li class=\"mui-table-view-cell\" name=\"" + json[i].Name + "\"><a class=\"mui-navigate-right\">" + json[i].Name + "</a></li>";
        }
        $("#offCanvasSideScroll .contentList .UserList").html(html)
    }
}
//接收昵称
chat.client.getName = function (name) {
    if (name == "false") {
        alert("名称已被使用")
    } else {
        UserName = name;
        $('.UserName').val(UserName);
        Cookies.set("UserName", UserName)
        $(".LoginPage").hide();
    }
    
}
//接收所有分组
chat.client.getGroups = function (data) {
    if (data) {
        var json = $.parseJSON(data);
        var html = "";
        for (var i = 0; i < json.length; i++) {
            html += "<li class=\"mui-table-view-cell\" data-name=\"" + json[i].Name + "\" date-status=\"" + json[i].Status + "\" date-num=" + json[i].Count + ">"
            html += "<a class=\"mui-navigate-right\">" + json[i].Name + "(" + json[i].Count + ")";
            //html += "<input data-naem=\"" + json[i].Name+"\" date-status=\"0\" type=\"button\" class=\"addgroup\" value=\"加入\"/>";
            html += "</a></li>";
        }
        $("#offCanvasSideScroll .contentList .GroupList").html(html)
    }
}
//接收系统提示
chat.client.systemNotification = function (text) {
    alert(text)
}
//接收群组系统提示
chat.client.groupSystem = function (text) {
    var html = "<li class=\"AddGroupTitle\"><span class=\"GroupTitleContent\">" + text + "</span></li>"
    $(".MessageList ul").append(html)
}
// 启动连接
$.connection.hub.start().done(function () {
    console.log(UserName);
    if (UserName != null && UserName != "" && UserName != false && UserName != undefined && UserName.length > 0) {
        chat.server.loging(UserName);
    } 
});
function GoName(name) { chat.server.loging(name); }
//发送消息
function SendMessage(type, msg, name) {
    switch (type) {
        case "Lobby"://发送大厅消息
            msgText.push("{\"name\":\"" + UserName + "\",\"message\":\"" + msg + "\",\"status\":\"0\"}");
            chat.server.send(msg);
            break;
        case "User"://发送私聊消息
            msgGoText.push("{\"Name\":\"" + UserName + "\",\"SendName\":\"" + name + "\",\"message\":\"" + msg + "\",\"status\":\"0\"}");
            chat.server.sendGo(msg, name);
            break;
        case "Group"://发送群组消息
            msgGroupText.push("{\"Name\":\"" + UserName + "\",\"GroupName\":\"" + name + "\",\"message\":\"" + msg + "\",\"status\":\"0\"}");
            chat.server.sendGroup(msg, name);
            break;

        default:
    }
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
//msgText, msgGoText, msgGroupText
function setMsg(type,name) {
    var msgJson = new Array();
    if (type == "msgText") {
        if (msgText.length == 0)
            return;
        msgJson = msgText;
    } else if (type == "msgGoText") {
        if (msgGoText.length == 0)
            return;
        for (var i = 0; i < msgGoText.length; i++) {
            if (msgGoText[i].SendName == name || msgGoText[i].Name == name) {
                msgJson.push(msgGoText[i])
            }
        }
    } else if (type == "msgGroupText") {
        if (msgGroupText.length == 0)
            return;
        for (var i = 0; i < msgGroupText.length; i++) {
            if (msgGroupText[i].GroupName == name) {
                msgJson.push(msgGroupText[i])
            }
        }
    }
    $(".MessageList ul li").remove();
    for (var j = 0; j < msgJson.length; j++) {
        var JItem = JSON.parse(msgJson[j])
        if (JItem.status == 1) {
            $(".MessageList ul").append("<li class=\"Msg\"><span>" + JItem.name + "：<span class=\"MsgContent\">" + JItem.message + "</span></span></li>")
        } else {
            $(".MessageList ul").append("<li class=\"MsgGo\"><span class=\"MsgContent\">" + JItem.message + "</span></li>")
        }
    }
    $(".MessageList").scrollTop($(".MessageList")[0].scrollHeight);
}

function titlespan(item, name) {//!$(item).hasClass("mui-active") 
    if ($(".SendName").val() != name) {
        if ($(item).children("a").children(".titleNum").length == 0) {
            $(item).children("a").append("<span class=\"titleNum\">1</span>");
        } else {
            var i = parseInt($(item).children("a").children(".titleNum").text())
            var num1 = i >= 99 ? "99+" : i + 1;
            $(item).children("a").children(".titleNum").text(num1);
        }
        if ($(".OnlineList .HeaderTitleNum").length == 0) {
            $(".OnlineList").append("<span class=\"HeaderTitleNum\">1</span>");
        } else {
            var j = parseInt($(".OnlineList .HeaderTitleNum").text())
            var num2 = j >= 99 ? "99+" : j + 1;
            $(".OnlineList .HeaderTitleNum").text(num2);
        }
        if (name == "" || $(".SendName").val() == name)
            return;
        var li_item = $(item).find("ul li[data-name=" + name + "]").children("a");
        if ($(li_item).children(".titleNum").length == 0) {
            $(li_item).append("<span class=\"titleNum\">1</span>");
        } else {
            var i = parseInt($(li_item).children(".titleNum").text())
            var num1 = i >= 99 ? "99+" : i + 1;
            $(li_item).children(".titleNum").text(num1);
        }
    }
}

