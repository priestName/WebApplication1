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
        public static List<UserList> users = new List<UserList>();//用户
        public static List<GroupList> groups = new List<GroupList>();//组
        /// <summary>
        /// 大厅消息
        /// </summary>
        /// <param name="message"></param>
        public void Send(string message)
        {
            Clients.All.broadcastMessage(GetUser(string.Empty).Name, message);
        }
        /// <summary>
        /// 私聊消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="GoUserName"></param>
        public void SendGo(string message, string GoUserName)
        {
            Clients.Client(GetUser(GoUserName).ConnectionID).broadcastMessageGo(GetUser(string.Empty).Name, message);
        }
        /// <summary>
        /// 组消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="GroupName"></param>
        public void SendGroup(string message, string GroupName)
        {
            Clients.Group(GroupName, new string[0]).groupMessageGo(GetUser(string.Empty).Name, GroupName, message);
        }
        /// <summary>
        /// 改名
        /// </summary>
        /// <param name="UserName"></param>
        public void EditName(string UserName)
        {
            users.Where(w => w.ConnectionID == Context.ConnectionId).SingleOrDefault().Name = UserName;
            GetUsers();
        }
        /// <summary>
        /// 创建组
        /// </summary>
        /// <param name="GroupName"></param>
        public void CreatGroup(string GroupName)
        {
            var group = groups.Where(g => g.GroupName == GroupName).SingleOrDefault();
            var user = users.Where(w => w.ConnectionID == Context.ConnectionId).SingleOrDefault();
            if (group == null)
            {
                group = new GroupList(GroupName, new List<UserList> { user }, 1);
                groups.Add(group);
                GetGroups();
            }
            else {
                Clients.All.SystemNotification("分组名称已存在");
            }
        }
        /// <summary>
        /// 加入组
        /// </summary>
        /// <param name="GroupName"></param>
        public void AddGroup(string GroupName)
        {
            var group = groups.Where(g => g.GroupName == GroupName).SingleOrDefault();
            var user = users.Where(w => w.ConnectionID == Context.ConnectionId).SingleOrDefault();
            if (group != null)
            {
                group.user.Add(user);
                Groups.Add(user.ConnectionID, group.GroupName);
                Clients.Group(GroupName,new string[0]).groupSystem(user.Name+"加入群聊");
                GetGroups();
            }
            else
            {
                Clients.All.SystemNotification("分组不存在");
            }
        }
        /// <summary>
        /// 退出组(组人数为0时删除组)
        /// </summary>
        /// <param name="GroupName"></param>
        public void DelGroup(string GroupName)
        {
            var group = groups.Where(g => g.GroupName == GroupName).SingleOrDefault();
            if (group != null)
            {
                groups.Where(g => g.GroupName == GroupName).SingleOrDefault().user = group.user.Where(s => s.ConnectionID != Context.ConnectionId).ToList();
                Groups.Remove(Context.ConnectionId, group.GroupName);
                Clients.Group(GroupName, new string[0]).groupSystem(GetUser(string.Empty).Name + "退出群聊");
                group = groups.Where(g => g.GroupName == GroupName).SingleOrDefault();
                //当组中没有用户时删除组
                if (group.user.Count == 0)
                {
                    groups.Remove(group);
                }
                GetGroups();
            }
            else
            {
                Clients.All.SystemNotification("分组不存在");
            }
        }
        /// <summary>
        /// 获取所有用户在线列表
        /// </summary>
        private void GetUsers()
        {
            var list = users.Where(u => !string.IsNullOrEmpty(u.Name) && u.Status == 1)
                            .Select(s => new { s.Name, s.ConnectionID }).ToList();
            string jsonList = JsonConvert.SerializeObject(list);
            Clients.All.getUsers(jsonList);
        }
        /// <summary>
        /// 获取所有组
        /// </summary>
        private void GetGroups()
        {
            var list = groups.Where(g =>g.Status == 1).Select(s => new { s.GroupName, s.user.Count }).ToList();
            string jsonList = JsonConvert.SerializeObject(list);
            Clients.All.getGroups(jsonList);
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="name"></param>
        private void Loging(string name)
        {
            if (users.Where(w => w.Name == name).Count() == 0)
            {
                Clients.Client(Context.ConnectionId).getName(name);
                var user = users.Where(w => w.ConnectionID == Context.ConnectionId).SingleOrDefault();
                if (user == null)
                {
                    user = new UserList(name, Context.ConnectionId, 1);
                    users.Add(user);
                }
                GetUsers();
            }
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
        public UserList GetUser(string name)
        {
            return users.Where(u => (string.IsNullOrEmpty(name) && u.ConnectionID == Context.ConnectionId) || (!string.IsNullOrEmpty(name) && u.Name == name)).SingleOrDefault();
        }
        #endregion
    }
    public class UserList
    {
        public string ConnectionID { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public UserList(string name, string connectionId,int Status)
        {
            this.Name = name;
            this.ConnectionID = connectionId;
            this.Status = Status;
        }
    }
    public class GroupList
    {
        public string GroupName { get; set; }
        public List<UserList> user { get; set; }
        public int Status { get; set; }
        public GroupList(string GroupName, List<UserList> user, int Status)
        {
            this.GroupName = GroupName;
            this.user = user;
            this.Status = Status;
        }
    }
}