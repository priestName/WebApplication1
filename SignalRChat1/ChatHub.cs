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
        public void Send(string name, string message)
        {
            //调用BroadcastMessage方法来更新客户端
            Clients.All.broadcastMessage(name, message);
        }
        public void GetName(string name)
        {
            //查询用户  
            var user = users.SingleOrDefault(u => u.ConnectionID == Context.ConnectionId);
            if (user != null)
            {
                user.Name = name;
                SockedUser sockedUser = UserDLL.SetUser(string.Empty, user.Name);
                sockedUser.ClientId = Context.ConnectionId;
                UserDLL.UpdateUser(sockedUser);
            }
            GetUsers();
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
        #endregion
        //获取所有用户在线列表  
        private void GetUsers()
        {
            var list = users.Select(s => new { s.Name, s.ConnectionID }).ToList();
            string jsonList = JsonConvert.SerializeObject(list);
            Clients.All.getUsers(jsonList);
        }

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