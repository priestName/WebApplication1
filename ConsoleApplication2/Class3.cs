using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fleck;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ConsoleApplication2
{
    class Class3
    {
        public void Start()
        {
            FleckLog.Level = LogLevel.Debug;
            var allSockets = new List<IWebSocketConnection>();
            var server = new WebSocketServer("ws://192.168.3.10:7788");//172.18.250.7  192.168.3.10

            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    allSockets.Add(socket);
                    var title = "IP：" + socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort + " 连接成功   当前在线人数" + allSockets.Count.ToString();
                    Console.WriteLine(title);
                    allSockets.ToList().ForEach(s => s.Send(title));
                };
                socket.OnClose = () =>
                {
                    if (allSockets.IndexOf(socket) != -1)
                    {
                        Console.WriteLine("Close!");
                        allSockets.Remove(socket);
                        var title = "IP：" + socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort + " 断开连接   当前在线人数" + allSockets.Count.ToString();
                        allSockets.ToList().ForEach(s => s.Send(title));
                    }  
                };
                socket.OnError = (e) =>
                {
                    Console.WriteLine(e.Message);
                };
                socket.OnMessage = message =>
                {
                    //Console.WriteLine(message);
                    var id = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                    allSockets.ToList().ForEach(s => s.Send(id + "：" +message));
                };
            });
        }
        public void InsertLogText(string msg)
        {
            byte[] myByte = System.Text.Encoding.UTF8.GetBytes(msg + "\r\n");
            string TextName = DateTime.Now.Date.ToString("yyyy_MM_dd") + "_log.txt";
            Directory.CreateDirectory(@"Log\");
            using (FileStream fsWrite = new FileStream(@"Log\" + TextName, FileMode.Append))
            {
                fsWrite.Write(myByte, 0, myByte.Length);
            };
        }
    }
}
