﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Class_Socket
    {
        static byte[] buffer = new byte[1024];
        private static int count = 0;
        static List<Socket> clientIps = new List<Socket> { };
        static List<string> clientNames = new List<string> { };
        public void saa()
        {
            WriteLine("server:ready", ConsoleColor.Green);

            #region 启动程序
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //②将该socket绑定到主机上面的某个端口
            socket.Bind(new IPEndPoint(IPAddress.Any, 7788));

            //③启动监听，并且设置一个最大的队列长度
            socket.Listen(10000);

            //④开始接受客户端连接请求
            socket.BeginAccept(new AsyncCallback(ClientAccepted), socket);
            //Console.ReadLine();
            #endregion
        }
        #region 客户端连接成功
        /// <summary>
        /// 客户端连接成功
        /// </summary>
        /// <param name="ar"></param>
        public static void ClientAccepted(IAsyncResult ar)
        {
            #region
            //设置计数器
            count++;
            var socket = ar.AsyncState as Socket;
            //这就是客户端的Socket实例，我们后续可以将其保存起来
            var client = socket.EndAccept(ar);
            clientIps.Add(client);
            clientNames.Add("User" + count);
            //客户端IP地址和端口信息
            IPEndPoint clientipe = (IPEndPoint)client.RemoteEndPoint;
            WriteLine(clientipe + " 已上线，当前在线人数 " + count, ConsoleColor.Yellow);
            client.Send(Encoding.UTF8.GetBytes("name:User" + count));
            foreach (var item in clientIps)
            {
                item.Send(Encoding.UTF8.GetBytes("clients:" + string.Join("|", clientNames))); //默认Unicode
            }
            //接收客户端的消息(这个和在客户端实现的方式是一样的）异步
            client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), client);
            //准备接受下一个客户端请求(异步)
            socket.BeginAccept(new AsyncCallback(ClientAccepted), socket);
            #endregion
        }
        #endregion

        #region 接收客户端的信息
        /// <summary>
        /// 接收某一个客户端的消息
        /// </summary>
        /// <param name="ar"></param>
        public static void ReceiveMessage(IAsyncResult ar)
        {
            int length = 0;
            string message = "";
            var socket = ar.AsyncState as Socket;
            //客户端IP地址和端口信息
            IPEndPoint clientipe = (IPEndPoint)socket.RemoteEndPoint;
            try
            {
                #region
                length = socket.EndReceive(ar);
                //读取出来消息内容
                message = Encoding.UTF8.GetString(buffer, 0, length);
                //string[] amsg = message.Split('：');
                //string msg = amsg[0].Substring(1, amsg[0].Length + 1);
                //输出接收信息
                if (!string.IsNullOrEmpty(message))
                {
                    string clientName = clientNames[clientIps.IndexOf(socket)];
                    foreach (var item in clientIps)
                    {
                        if (item != socket) {
                            item.Send(Encoding.UTF8.GetBytes(clientName + " ：" + message));
                        }
                    }
                    WriteLine(clientipe + " ：" + message, ConsoleColor.White);
                }
                //服务器发送消息
                ///socket.Send(Encoding.UTF8.GetBytes(clientipe + " ：" + message));
                //接收下一个消息(因为这是一个递归的调用，所以这样就可以一直接收消息）异步
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), socket);
                #endregion
            }
            catch (Exception ex)
            {
                //设置计数器
                count--;
                //断开连接
                WriteLine(clientipe + " 已下线，剩余人数 " + (count), ConsoleColor.Red);
                clientNames.Remove(clientNames[clientIps.IndexOf(socket)]);
                clientIps.Remove(socket);
                foreach (var item in clientIps)
                {
                    item.Send(Encoding.UTF8.GetBytes("clients:" + string.Join("|", clientNames))); //默认Unicode
                }
            }
        }
        #endregion

        #region 扩展方法
        public static void WriteLine(string str, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("[{0}] {1}", DateTime.Now.ToString("MM-dd HH:mm:ss"), str);
        }
        #endregion
    }
}
