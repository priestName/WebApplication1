using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Class1
    {
        static byte[] buffer = new byte[1024];
        private static int count = 0;
        public void Main()
        {
            WriteLine("server:ready", ConsoleColor.Green); //绿色

            #region 启动程序
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7788));//IPAddress.Any  192.168.3.251  172.18.250.7
            socket.Listen(10000);
            socket.BeginAccept(new AsyncCallback(ClientAccepted), socket);
            Console.ReadLine();
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
            count++;
            var socket = ar.AsyncState as Socket;
            var client = socket.EndAccept(ar);
            IPEndPoint clientipe = (IPEndPoint)client.RemoteEndPoint;
            WriteLine(clientipe + " 已上线   在线人数" + count, ConsoleColor.Yellow);
            //item.Send(Encoding.UTF8.GetBytes("clients:" + string.Join("|", clientNames))); //默认Unicode
            client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), client);
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
                //输出接收信息
                WriteLine(clientipe + " ：" + message, ConsoleColor.White);
                //服务器发送消息
                socket.Send(Encoding.UTF8.GetBytes(message)); //默认Unicode
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), socket);
                #endregion
            }
            catch (Exception ex)
            {
                //设置计数器
                count--;
                //断开连接
                WriteLine(clientipe + " 已下线  剩余人数：" + (count), ConsoleColor.Red);
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