using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using WindowsFormsApplication2;
using System.Windows.Forms;

namespace ConsoleApplication2
{
    class Class1
    {
        static byte[] buffer = new byte[1024];
        private static int count = 0;
        static List<Socket> soc = new List<Socket> { };
        //private static Form1 form1 = new Form1();
        public void Main()
        {
            WriteLine("server:ready", ConsoleColor.Green); //绿色

            #region 启动程序
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //②将该socket绑定到主机上面的某个端口
            socket.Bind(new IPEndPoint(IPAddress.Any, 7788));

            //③启动监听，并且设置一个最大的队列长度
            socket.Listen(10000);

            //④开始接受客户端连接请求
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
            //设置计数器
            count++;
            var socket = ar.AsyncState as Socket;
            //这就是客户端的Socket实例，我们后续可以将其保存起来
            var client = socket.EndAccept(ar);
            soc.Add(client);
            //客户端IP地址和端口信息
            IPEndPoint clientipe = (IPEndPoint)client.RemoteEndPoint;
            WriteLine(clientipe + " is connected，total connects " + count, ConsoleColor.Yellow);

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
                string[] amsg = message.Split('：');
                string msg = amsg[0].Substring(1, amsg[0].Length + 1);
                //输出接收信息
                if (!string.IsNullOrEmpty(message))
                {
                    WriteLine(clientipe + " ：" + message, ConsoleColor.White);
                }
                //服务器发送消息
                //socket.Send(Encoding.UTF8.GetBytes(message)); //默认Unicode
                foreach (var item in soc)
                {
                    //if (item.RemoteEndPoint == soc[1].RemoteEndPoint|| item.RemoteEndPoint== clientipe)
                    //{
                    item.Send(Encoding.UTF8.GetBytes(clientipe + " ：" + message));
                    //socket.Send(Encoding.UTF8.GetBytes(message));
                    //}
                }
                //接收下一个消息(因为这是一个递归的调用，所以这样就可以一直接收消息）异步
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), socket);
                #endregion
            }
            catch (Exception ex)
            {
                //设置计数器
                count--;
                //断开连接
                WriteLine(clientipe + " is disconnected，total connects " + (count), ConsoleColor.Red);
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
