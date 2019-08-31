// 声明一个代理以引用集线器
var chat = $.connection.chatHub;
// 创建集线器可以调用以广播消息的函数
chat.client.broadcastMessage = function (name, message) {
    // HTML编码显示名称和消息
    var encodedName = $('<div />').text(name).html();
    var encodedMsg = $('<div />').text(message).html();
    // 将消息添加到页面
    $('#discussion').append('<li><strong>' + encodedName
        + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
};
// 获取用户名并将其存储为消息的前缀
$('#displayname').val(prompt('Enter your name:', ''));
// 将初始焦点设置为消息输入框
$('#message').focus();
// 启动连接
$.connection.hub.start().done(function () {
    $('#sendmessage').click(function () {
        // 调用集线器上的send方法
        chat.server.send($('#displayname').val(), $('#message').val());
        // 清除文本框并重置下一个注释的焦点
        $('#message').val('').focus();
    });
});