

var chat = $.connection.chatHub;
chat.connection.url ="http://localhost:60999/signalr/hubs"

$.connection.hub.start().done(function () {
    chat.server.hello("hello");
});
chat.client.sayHello2 = function (msg) {
    alert(msg);
};


//使用生成的代理 $.connection.hub
//不使用生成的代理
//var connection = $.hubConnection();
//查询字符串参数 
//$.connection.hub.qs = { 'version': '1.0' };
//使用指定传输方式
//$.connection.hub.start({ transport: 'longPolling' });
//使用自定义传输方式
//webSockets
//foreverFrame
//serverSentEvents
//longPolling
//connection.start({ transport: ['webSockets', 'longPolling'] });
//查看是使用什么方式建立的连接
//connection.hub.start().done(function () {
//    console.log("Connected, transport = " + connection.transport.name);
//});

//var contosoChatHubProxy = $.connection.contosoChatHub;
//contosoChatHubProxy.client.addContosoChatMessageToPage = function (message) {
//    console.log(userName + ' ' + message);
//};
//$.connection.hub.start()
//    .done(function () { console.log('Now connected, connection ID=' + $.connection.hub.id); })
//    .fail(function () { console.log('Could not Connect!'); });

//$.extend(contosoChatHubProxy.client, {
//    addContosoChatMessageToPage: function (userName, message) {
//        console.log(userName + ' ' + message);
//    };
//});
//contosoChatHubProxy.client.addMessageToPage = function (message) {
//    console.log(message.UserName + ' ' + message.Message);
//});

//contosoChatHubProxy.server.newContosoChatMessage({ UserName: userName, Message: message }).done(function () {
//    console.log('Invocation of NewContosoChatMessage succeeded');
//}).fail(function (error) {
//    console.log('Invocation of NewContosoChatMessage failed. Error: ' + error);
//    });

//contosoChatHubProxy.invoke('newContosoChatMessage', { UserName: userName, Message: message }).done(function () {
//    console.log('Invocation of NewContosoChatMessage succeeded');
//}).fail(function (error) {
//    console.log('Invocation of NewContosoChatMessage failed. Error: ' + error);
//});

//contosoChatHubProxy.server.NewContosoChatMessage(userName, message).done(function () {
//    console.log('Invocation of NewContosoChatMessage succeeded');
//}).fail(function (error) {
//    console.log('Invocation of NewContosoChatMessage failed. Error: ' + error);
//});
//stockTickerProxy.server.newContosoChatMessage("张三", "哈哈哈").done(function (message) {
//    $.each(stocks, function () {
//        var stock = this;
//        console.log("userName=" + message.userName + " message=" + message.message);
//    });
//}).fail(function (error) {
//    console.log('Error: ' + error);
//});