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
        public void Send(string message)
        {
            Clients.All.broadcastMessage(GetUser(string.Empty).Name, message);
        }
        public void SendGo(string message, string GoUserName)
        {
            Clients.Client(GetUser(GoUserName).ConnectionID).broadcastMessageGo(GetUser(string.Empty).Name, message);
        }
        public void EditName(string UserName)
        {
            users.Where(w => w.ConnectionID == Context.ConnectionId).SingleOrDefault().Name = UserName;
            GetUsers();
        }
        //获取所有用户在线列表  
        private void GetUsers()
        {
            var list = users.Where(u => !string.IsNullOrEmpty(u.Name) && u.ConnectionID != Context.ConnectionId)
                            .Select(s => new { s.Name, s.ConnectionID }).ToList();
            string jsonList = JsonConvert.SerializeObject(list);
            Clients.All.getUsers(jsonList);
        }
        public void Loging(string name)
        {
            var user = users.Where(w => w.ConnectionID == Context.ConnectionId).SingleOrDefault();
            if (user == null)
            {
                user = new User(name, Context.ConnectionId,1);
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

        #region
        public User GetUser(string name)
        {
            return users.Where(u => (string.IsNullOrEmpty(name) && u.ConnectionID == Context.ConnectionId) || (!string.IsNullOrEmpty(name) && u.Name == name)).SingleOrDefault();
        }
        #endregion
    }
    public class User
    {
        public string ConnectionID { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public User(string name, string connectionId,int Status)
        {
            this.Name = name;
            this.ConnectionID = connectionId;
            this.Status = Status;
        }
    }
}