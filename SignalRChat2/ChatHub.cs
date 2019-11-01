using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRChat2
{
    [HubName("chatHub")]
    public class ChatHub : Hub
    {
        public override Task OnConnected()
        {
            var version = Context.QueryString["version"];
            Clients.Caller.sayHello("连接成功");
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }

        [HubMethodName("hello")]
        public void Hello(string name)
        {
            Clients.All.sayHello2(Context.ConnectionId);
        }
    }
}