using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Fleck;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Timers;

namespace ConsoleApplication2
{
    class SocketServiceWuZi
    {
        public void Start()
        {
            List<string> SerName = new List<string> { };
            FleckLog.Level = LogLevel.Debug;
            string users = ReadJson();
            var allSockets = new List<IWebSocketConnection>();
            var PkSockets = new List<IWebSocketConnection>();

            var server = new WebSocketServer("ws://192.168.3.10.7:7788");//172.18.250.7  192.168.3.10
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    try
                    {
                        var Name = SelName(socket.ConnectionInfo.ClientIpAddress);
                        var OldSocketList = allSockets.Where(s => s.ConnectionInfo.ClientIpAddress == socket.ConnectionInfo.ClientIpAddress);
                        if (!string.IsNullOrEmpty(Name))
                        {
                            if (OldSocketList.Count() > 0)
                            {
                                var OldSocket = OldSocketList.First();
                                OldSocket.Send("SysMsg:您已经在其他地方登录");
                                SerName.Remove(Name);
                                allSockets.Remove(OldSocket);
                                OldSocket.Close();
                            }
                        }
                        else
                        {
                            Name = "User" + JObject.Parse(users + "}").Count;
                            insertIpName(socket.ConnectionInfo.ClientIpAddress, Name);
                        }
                        SerName.Add(Name);
                        allSockets.Add(socket);
                        InsertLogText("IP：" + socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort + " 连接成功");
                        Console.WriteLine(socket.ConnectionInfo.ClientPort + "连接成功  当前在线人数" + allSockets.Count());
                        socket.Send("username:" + SerName[allSockets.Count() - 1]);
                        socket.Send("serverlist:" + string.Join("|", SerName));
                        allSockets.ToList().ForEach(s => s.Send("serverlist:" + string.Join("|", SerName)));
                    }
                    catch (Exception e)
                    {
                        WriteJson(e.ToString(), "UserErro.txt");
                    }
                };
                socket.OnClose = () =>
                {
                    try
                    {
                        Console.WriteLine("Close!");
                        if (allSockets.IndexOf(socket) != -1)
                        {
                            SerName.Remove(SerName[allSockets.IndexOf(socket)]);
                            allSockets.Remove(socket);
                            allSockets.ToList().ForEach(s => s.Send("serverlist:" + string.Join("|", SerName)));
                        }
                    }
                    catch (Exception e)
                    {
                        WriteJson(e.ToString(), "UserErro.txt");
                    }
                };
                socket.OnMessage = message =>
                {
                    try
                    {
                        //Console.WriteLine(message);
                        string MsgEcho = message.Substring(0, message.IndexOf(':'));
                        string Messages = message.Substring(message.IndexOf(':') + 1);
                        if (MsgEcho == "EditName")
                        {
                            EditName(socket.ConnectionInfo.ClientIpAddress, Messages);
                            InsertLogText(SerName[allSockets.IndexOf(socket)] + "==>>修改名字==>>" + Messages);
                            SerName[allSockets.IndexOf(socket)] = Messages;
                            allSockets.ToList().ForEach(s => s.Send("serverlist:" + string.Join("|", SerName)));
                        }
                        else if (MsgEcho == "qizixy")
                        {
                            IWebSocketConnection socketGo;
                            int UserPkindex = PkSockets.IndexOf(socket);
                            if (UserPkindex != -1)
                            {
                                if (UserPkindex % 2 == 0)
                                {
                                    socketGo = PkSockets[UserPkindex + 1];
                                }
                                else
                                {
                                    socketGo = PkSockets[UserPkindex - 1];
                                }
                                socketGo.Send(message);
                                InsertLogText("[" + SerName[allSockets.IndexOf(socket)] + "]VS[" + SerName[allSockets.IndexOf(socket)] + "]：[" + SerName[allSockets.IndexOf(socket)] + "]" + Messages);
                            }
                        }
                        else if (MsgEcho == "UserGo")
                        {
                            var socketGo = allSockets[Convert.ToInt32(Messages)];
                            PkSockets.Add(socket);
                            PkSockets.Add(socketGo);
                            socketGo.Send("UserGo:" + SerName[allSockets.IndexOf(socket)]);
                            InsertLogText("[" + SerName[allSockets.IndexOf(socket)] + "]==>>发起挑战到[" + SerName[Convert.ToInt32(Messages)] + "]");
                        }
                        else if (MsgEcho == "UserIsGo")
                        {
                            IWebSocketConnection socketGo;
                            int UserPkindex = PkSockets.IndexOf(socket);
                            if (UserPkindex != -1)
                            {
                                if (UserPkindex % 2 == 0)
                                {
                                    socketGo = PkSockets[UserPkindex + 1];
                                }
                                else
                                {
                                    socketGo = PkSockets[UserPkindex - 1];
                                }
                                socketGo.Send("UserIsGo:" + SerName[allSockets.IndexOf(socket)]);
                                InsertLogText("[" + SerName[allSockets.IndexOf(socket)] + "]接受[" + SerName[allSockets.IndexOf(socket)] + "]的挑战");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        WriteJson(e.ToString(), "UserErro.txt");
                    }
                };
            });
        }
        public void InsertLogText(string msg)
        {
            byte[] myByte = System.Text.Encoding.UTF8.GetBytes("（DT:" + DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss")+"）"+msg+ "\r\n");
            string TextName = DateTime.Now.Date.ToString("yyyy_MM_dd") + "_log.txt";
            Directory.CreateDirectory(@"Log\");
            using (FileStream fsWrite = new FileStream(@"Log\" + TextName, FileMode.Append))
            {
                fsWrite.Write(myByte, 0, myByte.Length);
            };
        }
        public void insertIpName(string Ip, string name)
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
            if (myStr != "{")
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
                    item.Value["name"] = Name;
                    break;
                }
            }
            WriteJson(job.ToString().Substring(0, job.ToString().Length - 1)+",", "User_Ip_Name.txt");
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
        public void WriteJson(string Text, string TextName)
        {
            
            if (TextName != "User_Ip_Name.txt")
            {
                Text += ",\r\n" + DateTime.Now;
            }
            byte[] myByte = System.Text.Encoding.UTF8.GetBytes(Text);
            using (FileStream fsWrite = new FileStream(@"Log\" + TextName, FileMode.Create, FileAccess.Write))
            {
                fsWrite.Write(myByte, 0, myByte.Length);

            };
        }
    }
}
