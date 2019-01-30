using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Fleck;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ConsoleApplication2
{
    class SocketService
    {
        public void Start()
        {
            List<string> SerName = new List<string> { };
            int i = 0;
            FleckLog.Level = LogLevel.Debug;

            var allSockets = new List<IWebSocketConnection>();

            var server = new WebSocketServer("ws://172.18.250.7:7788");//172.18.250.7  192.168.3.251

            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    i++;
                    var Name = SelName(socket.ConnectionInfo.ClientIpAddress);
                    Name = string.IsNullOrEmpty(Name) ? "User" + i : Name;
                    var OldSocket = allSockets.Where(s => s.ConnectionInfo.ClientIpAddress == socket.ConnectionInfo.ClientIpAddress);
                    if (!string.IsNullOrEmpty(Name))
                    {
                        if (OldSocket.Count()>0)
                        {
                            OldSocket.First().Send("SysMsg:您已经在其他地方登录");
                            OldSocket.First().Close();
                        }
                    }
                    else {
                        insertIpName(socket.ConnectionInfo.ClientIpAddress, Name);
                    }
                    SerName.Add(Name);
                    allSockets.Add(socket);
                    i = allSockets.Count();
                    #region 原判断
                    //var Name = SelName(socket.ConnectionInfo.ClientIpAddress);
                    //if (string.IsNullOrEmpty(Name))
                    //{
                    //    insertIpName(socket.ConnectionInfo.ClientIpAddress, "User" + i);
                    //    i++;
                    //    SerName.Add("User" + i);
                    //}
                    //else {
                    //    if (SerName.IndexOf(Name)<0)
                    //    {
                    //        SerName.Add(Name);
                    //        i = SerName.Count;
                    //    }

                    //    var OldSocket = allSockets.Where(s => s.ConnectionInfo.ClientIpAddress == socket.ConnectionInfo.ClientIpAddress);
                    //    if (OldSocket.Count() > 0)
                    //    {
                    //        OldSocket.First().Send("SysMsg:您已经在其他地方登录");
                    //        OldSocket.First().Close();
                    //        allSockets.Remove(OldSocket.First());
                    //    }
                    //}
                    //allSockets.Add(socket);
                    #endregion
                    InsertLogText("IP：" + socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort + " 连接成功");
                    Console.WriteLine(socket.ConnectionInfo.ClientPort+ "连接成功  当前在线人数"+ SerName.Count);
                    socket.Send("username:" + SerName[i-1]);
                    socket.Send("serverlist:"+ string.Join("|", SerName));
                    allSockets.ToList().ForEach(s => s.Send("serverlist:" + string.Join("|", SerName)));
                };
                socket.OnClose = () =>
                {
                    Console.WriteLine("Close!");
                    SerName.Remove(SerName[allSockets.IndexOf(socket)]);
                    allSockets.Remove(socket);
                    allSockets.ToList().ForEach(s => s.Send("serverlist:" + string.Join("|", SerName)));
                };
                socket.OnMessage = message =>
                {
                    //Console.WriteLine(message);
                    string MsgEcho = message.Substring(0, message.IndexOf(':'));
                    string Messages = message.Substring(message.IndexOf(':')+1);
                    if (MsgEcho == "Msg")
                    {
                        foreach (var item in allSockets.ToList())
                        {
                            if (item != socket)
                            {
                                item.Send("Echo:" + SerName[allSockets.IndexOf(socket)] + "：" + Messages);
                            }
                        }
                        InsertLogText(SerName[allSockets.IndexOf(socket)] + "==>>发送消息==>>" + Messages);
                    }
                    else if (MsgEcho == "MsgImg") {
                        foreach (var item in allSockets.ToList())
                        {
                            if (item != socket)
                            {
                                item.Send("EchoImg:" + SerName[allSockets.IndexOf(socket)] + "：" + Messages);
                            }
                        }
                        InsertLogText(SerName[allSockets.IndexOf(socket)] + "==>>发送图片==>>" + Messages);
                    }
                    else if (MsgEcho == "EditName")
                    {
                        InsertLogText(SerName[allSockets.IndexOf(socket)] + "==>>修改名字==>>" + Messages);
                        SerName[allSockets.IndexOf(socket)] = Messages;
                        allSockets.ToList().ForEach(s => s.Send("serverlist:" + string.Join("|", SerName)));
                        EditName(socket.ConnectionInfo.ClientIpAddress, Messages);
                    }
                    else
                    {
                        var socketGo = allSockets[Convert.ToInt32(MsgEcho)];
                        socketGo.Send("EchoGo:" + SerName[allSockets.IndexOf(socket)] + "：" + Messages);
                        InsertLogText(SerName[allSockets.IndexOf(socket)] + "==>>发送消息到[" + SerName[Convert.ToInt32(MsgEcho)] + "]==>>" + Messages);
                    }
                };
            });
        }
        public void InsertLogText(string msg)
        {
            byte[] myByte = System.Text.Encoding.UTF8.GetBytes(msg+ "\r\n");
            string TextName = DateTime.Now.Date.ToString("yyyy_MM_dd") + "_log.txt";
            Directory.CreateDirectory(@"Log\");
            using (FileStream fsWrite = new FileStream(@"Log\" + TextName, FileMode.Append))
            {
                fsWrite.Write(myByte, 0, myByte.Length);
            };
        }
        public void insertIpName(string Ip,string name)
        {
            Ip = name + ":{ip:'" + Ip + "',name:'" + name + "'},";
            byte[] myByte = System.Text.Encoding.UTF8.GetBytes("\r\n" + Ip);
            Directory.CreateDirectory(@"Log\");
            using (FileStream fsWrite = new FileStream(@"Log\User_Ip_Name.txt", FileMode.Append))
            {
                if (fsWrite.Length == 0)
                {
                    fsWrite.Write(Encoding.UTF8.GetBytes("{"), 0, Encoding.UTF8.GetBytes("{").Length);
                }
                fsWrite.Write(myByte, 0, myByte.Length);
            };
        }
        public string SelName(string ips)
        {
            string myStr = ReadJson();
            myStr = myStr.Replace("\r\n", "");
            if (myStr!="{")
            {
                myStr = myStr.Substring(0, myStr.Length - 1) + "}";
                JObject job = JObject.Parse(myStr);
                foreach (var item in job)
                {
                    if (item.Value["ip"].ToString() == ips)
                    {
                        return item.Value["name"].ToString();
                    }
                }
            }
            return string.Empty;
        }
        public void EditName(string Ip, string Name)
        {
            string myStr = ReadJson();
            myStr = myStr.Replace("\r\n", "");
            myStr = myStr.Substring(0, myStr.Length - 1) + "}";
            JObject job = JObject.Parse(myStr);
            foreach (var item in job)
            {
                if (item.Value["ip"].ToString() == Ip)
                {
                    item.Value["name"]= Name;
                }
            }
            WriteJson(job.ToString().Substring(0, job.ToString().Length - 1), "User_Ip_Name.txt");
        }
        public string ReadJson()
        {
            string myStr = string.Empty;
            //c#文件流读文件 
            Directory.CreateDirectory(@"Log\");
            using (FileStream fsWrite = new FileStream(@"Log\User_Ip_Name.txt", FileMode.Append))
            {
                if (fsWrite.Length == 0)
                {
                    fsWrite.Write(Encoding.UTF8.GetBytes("{"), 0, Encoding.UTF8.GetBytes("{").Length);
                }
            }
            using (FileStream fsRead = new FileStream(@"Log\User_Ip_Name.txt", FileMode.Open))
            {
                int fsLen = (int)fsRead.Length;
                byte[] heByte = new byte[fsLen];
                int r = fsRead.Read(heByte, 0, heByte.Length);
                myStr = System.Text.Encoding.UTF8.GetString(heByte);
            }
            return myStr;
        }
        public void WriteJson(string Text,string TextName)
        {
            byte[] myByte = System.Text.Encoding.UTF8.GetBytes(Text + ",\r\n");
            using (FileStream fsWrite = new FileStream(@"Log\" + TextName, FileMode.Create, FileAccess.Write))
            {
                fsWrite.Write(myByte, 0, myByte.Length);
            };
        }
    }
}
