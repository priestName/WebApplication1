using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WindowsFormsApplication1
{
    class Class1
    {
        //private Socket listenSocket;
        //public ListenParam _listenParam { get; set; }
        //public event Action<ListenParam, AsyncSocketClient> OnAcceptSocket;

        //bool start;

        //NetServer _netServer;
        //public NetListener(NetServer netServer)
        //{
        //    _netServer = netServer;
        //}

        //public int _acceptAsyncCount = 0;
        //public bool StartListen()
        //{
        //    try
        //    {
        //        start = true;
        //        IPEndPoint listenPoint = new IPEndPoint(IPAddress.Parse("0.0.0.0"), _listenParam._port);
        //        listenSocket = new Socket(listenPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        //        listenSocket.Bind(listenPoint);
        //        listenSocket.Listen(200);

        //        Thread thread1 = new Thread(new ThreadStart(NetProcess));
        //        thread1.Start();

        //        StartAccept();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        NetLogger.Log(string.Format("**监听异常!{0}", ex.Message));
        //        return false;
        //    }
        //}

        //AutoResetEvent _acceptEvent = new AutoResetEvent(false);
        //private void NetProcess()
        //{
        //    while (start)
        //    {
        //        DealNewAccept();
        //        _acceptEvent.WaitOne(1000 * 10);
        //    }
        //}

        //private void DealNewAccept()
        //{
        //    try
        //    {
        //        if (_acceptAsyncCount <= 10)
        //        {
        //            StartAccept();
        //        }

        //        while (true)
        //        {
        //            AsyncSocketClient client = _newSocketClientList.GetObj();
        //            if (client == null)
        //                break;

        //            DealNewAccept(client);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        NetLogger.Log(string.Format("DealNewAccept 异常 {0}***{1}", ex.Message, ex.StackTrace));
        //    }
        //}

        //private void DealNewAccept(AsyncSocketClient client)
        //{
        //    client.SendBufferByteCount = _netServer.SendBufferBytePerClient;
        //    OnAcceptSocket?.Invoke(_listenParam, client);
        //}

        //private void AcceptEventArg_Completed(object sender, SocketAsyncEventArgs acceptEventArgs)
        //{
        //    try
        //    {
        //        Interlocked.Decrement(ref _acceptAsyncCount);
        //        _acceptEvent.Set();
        //        acceptEventArgs.Completed -= AcceptEventArg_Completed;
        //        ProcessAccept(acceptEventArgs);
        //    }
        //    catch (Exception ex)
        //    {
        //        NetLogger.Log(string.Format("AcceptEventArg_Completed {0}***{1}", ex.Message, ex.StackTrace));
        //    }
        //}

        //public bool StartAccept()
        //{
        //    SocketAsyncEventArgs acceptEventArgs = new SocketAsyncEventArgs();
        //    acceptEventArgs.Completed += AcceptEventArg_Completed;

        //    bool willRaiseEvent = listenSocket.AcceptAsync(acceptEventArgs);
        //    Interlocked.Increment(ref _acceptAsyncCount);

        //    if (!willRaiseEvent)
        //    {
        //        Interlocked.Decrement(ref _acceptAsyncCount);
        //        _acceptEvent.Set();
        //        acceptEventArgs.Completed -= AcceptEventArg_Completed;
        //        ProcessAccept(acceptEventArgs);
        //    }
        //    return true;
        //}

        //ObjectPool<AsyncSocketClient> _newSocketClientList = new ObjectPool<AsyncSocketClient>();
        //private void ProcessAccept(SocketAsyncEventArgs acceptEventArgs)
        //{
        //    try
        //    {
        //        using (acceptEventArgs)
        //        {
        //            if (acceptEventArgs.AcceptSocket != null)
        //            {
        //                AsyncSocketClient client = new AsyncSocketClient(acceptEventArgs.AcceptSocket);
        //                client.CreateClientInfo(this);

        //                _newSocketClientList.PutObj(client);
        //                _acceptEvent.Set();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        NetLogger.Log(string.Format("ProcessAccept {0}***{1}", ex.Message, ex.StackTrace));
        //    }
        //}
    }
}
