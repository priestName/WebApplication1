using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        //private static Program program = new Program();//因为要接收到多个服务器（ip）发送的数据，此处按照ip地址分开存储发送数据
        //string msgess = string.Empty;
        //static int port = 6000;
        //static string host = "192.168.3.251";
        //static IPAddress ip = IPAddress.Parse(host);
        //static IPEndPoint ipe = new IPEndPoint(ip, port);
        //static Socket sSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public static void Main(string[] args)
        {
            while (true)
            {
                string s = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(s))
                {
                    return;
                }
                string a=string.Empty;
                foreach (var item in Encoding.ASCII.GetBytes(s)){a += item.ToString() + ",";}
                Console.WriteLine("ASCII：" + a); a = string.Empty;
                foreach (var item in Encoding.UTF7.GetBytes(s)) { a += item.ToString() + ","; }
                Console.WriteLine("UTF7：" + a); a = string.Empty;
                foreach (var item in Encoding.UTF8.GetBytes(s)) { a += item.ToString() + ","; }
                Console.WriteLine("UTF8：" +a); a = string.Empty;
                foreach (var item in Encoding.UTF32.GetBytes(s)) { a += item.ToString() + ","; }
                Console.WriteLine("UTF32：" + a); a = string.Empty;
                foreach (var item in Encoding.Unicode.GetBytes(s)) { a += item.ToString() + ","; }
                Console.WriteLine("Unicode/UTF16：" + a);
            }
            //Console.WriteLine("server:ready", ConsoleColor.Green); //绿色
        }
        //public static void cc()
        //{
        //    sSocket.Bind(ipe);
        //    sSocket.Listen(0);
        //    Socket serverSocket = sSocket.Accept();

        //    string recStr = "";
        //    byte[] recByte = new byte[4096];
        //    int bytes = serverSocket.Receive(recByte, recByte.Length, 0);
        //    recStr += Encoding.ASCII.GetString(recByte, 0, bytes);
        //    Console.WriteLine("服务器端获得信息:{0}", recStr);
        //    cc();
        //}
        //public static void dd()
        //{
        //    string sendStr = Console.ReadLine();
        //    byte[] sendByte = Encoding.ASCII.GetBytes(sendStr);
        //    sSocket.Send(sendByte, sendByte.Length, 0);
        //    sSocket.Close();
        //}

        #region 异步
        //Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //byte[] MsgBuffer = new byte[4096];
        ////连接
        //public void Connect(IPAddress ip, int port)
        //{
        //    this.clientSocket.BeginConnect(ip, port, new AsyncCallback(ConnectCallback), this.clientSocket);
        //}
        //private void ConnectCallback(IAsyncResult ar)
        //{
        //    try
        //    {
        //        Socket handler = (Socket)ar.AsyncState;
        //        handler.EndConnect(ar);
        //    }
        //    catch (SocketException ex)
        //    { }
        //}
        
        ////发送
        //public void Send(string data)
        //{
        //    Send(System.Text.Encoding.UTF8.GetBytes(data));
        //}

        //private void Send(byte[] byteData)
        //{
        //    try
        //    {
        //        int length = byteData.Length;
        //        byte[] head = BitConverter.GetBytes(length);
        //        byte[] data = new byte[head.Length + byteData.Length];
        //        Array.Copy(head, data, head.Length);
        //        Array.Copy(byteData, 0, data, head.Length, byteData.Length);
        //        this.clientSocket.BeginSend(data, 0, data.Length, 0, new AsyncCallback(SendCallback), this.clientSocket);
        //    }
        //    catch (SocketException ex)
        //    { }
        //}

        //private void SendCallback(IAsyncResult ar)
        //{
        //    try
        //    {
        //        Socket handler = (Socket)ar.AsyncState;
        //        handler.EndSend(ar);
        //    }
        //    catch (SocketException ex)
        //    { }
        //}
        
        ////接收
        //public void ReceiveData()
        //{
        //    clientSocket.BeginReceive(MsgBuffer, 0, MsgBuffer.Length, 0, new AsyncCallback(ReceiveCallback), null);
        //}

        //private void ReceiveCallback(IAsyncResult ar)
        //{
        //    try
        //    {
        //        int REnd = clientSocket.EndReceive(ar);
        //        if (REnd > 0)
        //        {
        //            byte[] data = new byte[REnd];
        //            Array.Copy(MsgBuffer, 0, data, 0, REnd);
        //            msgess = Encoding.ASCII.GetString(data);
        //            //在此次可以对data进行按需处理

        //            Console.WriteLine(clientSocket.BeginReceive(MsgBuffer, 0, MsgBuffer.Length, 0, new AsyncCallback(ReceiveCallback), null));
        //        }
        //        else
        //        {
        //            dispose();
        //        }
        //    }
        //    catch (SocketException ex)
        //    { Console.WriteLine(ex); }
        //}

        //private void dispose()
        //{
        //    try
        //    {
        //        this.clientSocket.Shutdown(SocketShutdown.Both);
        //        this.clientSocket.Close();
        //    }
        //    catch (Exception ex)
        //    { Console.WriteLine(ex); }
        //}
        #endregion

        #region 同步
        ////public static void aa()
        ////{
        ////    int port = 6000;
        ////    string host = "192.168.3.251";
        ////    IPAddress ip = IPAddress.Parse(host);
        ////    IPEndPoint ipe = new IPEndPoint(ip, port);
        ////    Socket sSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        ////    sSocket.Bind(ipe);
        ////    sSocket.Listen(0);
        ////    Console.WriteLine("监听已经打开，请等待");

        ////    //receive message
        ////    Socket serverSocket = sSocket.Accept();
        ////    Console.WriteLine("连接已经建立");
        ////    string recStr = "";
        ////    byte[] recByte = new byte[4096];
        ////    int bytes = serverSocket.Receive(recByte, recByte.Length, 0);
        ////    recStr += Encoding.ASCII.GetString(recByte, 0, bytes);
        ////    Console.WriteLine("服务器端获得信息:{0}", recStr);

        ////    //send message
        ////    string sendStr = Console.ReadLine();
        ////    byte[] sendByte = Encoding.ASCII.GetBytes(sendStr);
        ////    serverSocket.Send(sendByte, sendByte.Length, 0);
        ////    serverSocket.Close();
        ////    sSocket.Close();
        ////}
        ////public static void bb()
        ////{
        ////    int port = 6000;
        ////    string host = "192.168.3.251";//服务器端ip地址
        ////    IPAddress ip = IPAddress.Parse(host);
        ////    IPEndPoint ipe = new IPEndPoint(ip, port);
        ////    Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        ////    clientSocket.Connect(ipe);

        ////    //send message
        ////    string sendStr = Console.ReadLine();
        ////    byte[] sendBytes = Encoding.ASCII.GetBytes(sendStr);
        ////    clientSocket.Send(sendBytes);

        ////    //receive message
        ////    string recStr = "";
        ////    byte[] recBytes = new byte[4096];
        ////    int bytes = clientSocket.Receive(recBytes, recBytes.Length, 0);
        ////    recStr += Encoding.ASCII.GetString(recBytes, 0, bytes);
        ////    Console.WriteLine(recStr);
        ////    Console.Read();
        ////    clientSocket.Close();
        ////}
        #endregion
    }
}
