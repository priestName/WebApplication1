var chat = $.connection.chatHub;
chat.connection.url = "http://qy.priest.ink:8050/signalr/hubs";

$.connection.hub.start().done(function () {
    chat.server.hello("hello");
});
chat.client.sayHello = function (msg) {
    alert(msg);
};
chat.client.sayHello2 = function (msg) {
    alert(msg);
};