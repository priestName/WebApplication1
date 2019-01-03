using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using Fleck;

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

            var server = new WebSocketServer("ws://172.18.250.7:7788");

            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    i++;
                    allSockets.Add(socket);
                    SerName.Add("User"+ i);
                    Console.WriteLine(socket.ConnectionInfo.ClientPort+ "连接成功  当前在线人数"+ allSockets.Count);
                    socket.Send("username:User" + i);
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
                    if (message.Split(':')[0] == "Msg")
                    {
                        foreach (var item in allSockets.ToList())
                        {
                            if (item != socket)
                            {
                                item.Send("Echo:" + SerName[allSockets.IndexOf(socket)] + "：" + message.Split(':')[1]);
                            }
                        }
                    } else if (message.Split(':')[0] == "EditName")
                    {
                        SerName[allSockets.IndexOf(socket)] = message.Split(':')[1];
                        allSockets.ToList().ForEach(s => s.Send("serverlist:" + string.Join("|", SerName)));
                    }
                    else {
                        var socketGo = allSockets[Convert.ToInt32(message.Split(':')[0])];
                        socketGo.Send("EchoGo:" + SerName[allSockets.IndexOf(socket)] + "：" + message.Split(':')[1]);
                    }
                };
            });
        }
    }
}
