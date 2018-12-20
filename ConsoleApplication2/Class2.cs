using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Class2
    {
        Socket _socket = null;
        EndPoint _endPoint = null;
        bool _isListening = false;
        int BACKLOG = 10;
        public void SocketManager(string ip,int port)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream,ProtocolType.Tcp);
            IPAddress _ip = IPAddress.Parse(ip);
            _endPoint = new IPEndPoint(_ip, port);
        }
        public void stock()
        {
            _socket.Bind(_endPoint);//绑定端口
            _socket.Listen(BACKLOG);//开启监听
            Thread acceptServer = new Thread(AcceptWork); //开启新线程处理监听
            acceptServer.IsBackground = true;
            _isListening = true;
            acceptServer.Start();
        }

        private void AcceptWork()
        {
            while (_isListening)
            {
            Socket acceptSocket = _socket.Accept();
            if (acceptSocket != null)
            {
                Thread socketConnectedThread = new Thread(newSocketReceive);
                socketConnectedThread.IsBackground = true;
                socketConnectedThread.Start(acceptSocket);
            }
            Thread.Sleep(200);
            }
        }

        public void newSocketReceive(object obj)
        {
            Socket socket = obj as Socket;
            while (true)
            {
                try
                {
                    if (socket == null) return;
                    //这里向系统投递一个接收信息的请求，并为其指定ReceiveCallBack做为回调函数 
                    socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveCallBack, buffer);
                }
                catch (Exception ex)
                {
                    return;
                }
                Thread.Sleep(100);
            }
        }
        private void ReceiveCallBack(IAsyncResult ar)
        {
        }
    }
}
