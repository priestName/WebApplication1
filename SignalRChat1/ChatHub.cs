using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using SignalRChat1.Models;

namespace SignalRChat1
{
    public class ChatHub : Hub
    {
        public static List<User> users = new List<User>();
        public static List<List<User>> UsersList = new List<List<User>>();
        public void Send(string name, string message,string GoUserId)
        {
            //调用BroadcastMessage方法来更新客户端
            if(string.IsNullOrEmpty(GoUserId))
                Clients.All.broadcastMessage(name, message,0);
            else
                Clients.Client(GoUserId).broadcastMessage(name, message,1);
        }
        //获取所有用户在线列表  
        private void GetUsers()
        {
            var list = users.Where(u=>!string.IsNullOrEmpty(u.Name) && u.ConnectionID!=Context.ConnectionId)
                            .Select(s => new { s.Name, s.ConnectionID }).ToList();
            string jsonList = JsonConvert.SerializeObject(list);
            Clients.All.getUsers(jsonList);
        }
        public void Loging(string name)
        {
            var user = users.Where(w => w.ConnectionID == Context.ConnectionId).SingleOrDefault();
            if (user == null)
            {
                user = new User(name, Context.ConnectionId);
                users.Add(user);
            }

            GetUsers();
        }
        public void CloseSignalr(bool stopCalled)
        {
            OnDisconnected(stopCalled);
        }

        #region 重写连接事件
        public override Task OnConnected()
        {
            var user = users.Where(w => w.ConnectionID == Context.ConnectionId).SingleOrDefault();
            if (user != null)
            {
                Clients.Client(Context.ConnectionId).getName(user.Name);
            }
            GetUsers();
            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            var user = users.Where(p => p.ConnectionID == Context.ConnectionId).FirstOrDefault();
            //判断用户是否存在，存在则删除  
            if (user != null)
            {
                //删除用户  
                users.Remove(user);
            }
            GetUsers();//获取所有用户的列表  
            return base.OnDisconnected(stopCalled);
        }
        public override Task OnReconnected()
        {
            string id = Context.ConnectionId;
            
            return base.OnReconnected();
        }
        #endregion
    }
    public class User
    {
        public string ConnectionID { get; set; }
        public string Name { get; set; }
        public User(string name, string connectionId)
        {
            this.Name = name;
            this.ConnectionID = connectionId;
        }
    }
}