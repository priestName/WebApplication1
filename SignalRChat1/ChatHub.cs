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
        UserDLL UserDLL = new UserDLL();
        public static List<User> users = new List<User>();
        public void Send(string name, string message,string GoUserId)
        {
            //调用BroadcastMessage方法来更新客户端
            if(string.IsNullOrEmpty(GoUserId))
                Clients.All.broadcastMessage(name, message,0);
            else
                Clients.Client(GoUserId).broadcastMessage(name, message,1);
        }
        public void GetName()
        {
            int id = Convert.ToInt32(Context.RequestCookies["UserId"].Value);
            //查询用户
            SockedUser sockedUser = UserDLL.SetUser(u => u.ID == id);
            if (sockedUser != null)
            {
                var user = users.SingleOrDefault(u => u.Name == sockedUser.Name);
                users.SingleOrDefault(u => u.ConnectionID == Context.ConnectionId).Name = sockedUser.Name;
                if (user != null)
                {
                    Clients.Client(user.ConnectionID).closesignalr(true);
                    //Clients.Client(user.ConnectionID)

                    //Clients.Client
                }
                sockedUser.ClientId = Context.ConnectionId;
                UserDLL.UpdateUser(sockedUser);
                
                Clients.Client(Context.ConnectionId).showId(sockedUser.Name);
            }
            GetUsers();
        }
        //获取所有用户在线列表  
        private void GetUsers()
        {
            var list = users.Where(u=>!string.IsNullOrEmpty(u.Name) && u.ConnectionID!=Context.ConnectionId)
                            .Select(s => new { s.Name, s.ConnectionID }).ToList();
            string jsonList = JsonConvert.SerializeObject(list);
            Clients.All.getUsers(jsonList);
        }
        public void EditName(string name)
        {
            users.SingleOrDefault(u => u.ConnectionID == Context.ConnectionId).Name = name;
            SockedUser sockedUser = UserDLL.SetUser(u => u.ClientId == Context.ConnectionId);
            if (sockedUser != null)
            {
                sockedUser.Name = name;
                UserDLL.UpdateUser(sockedUser);
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
            //查询用户  
            var user = users.Where(w => w.ConnectionID == Context.ConnectionId).SingleOrDefault();
            //判断用户是否存在，否则添加集合  
            if (user == null)
            {
                user = new User("", Context.ConnectionId);
                users.Add(user);
            }
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