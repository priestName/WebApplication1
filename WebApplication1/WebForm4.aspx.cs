using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text;
using System.IO;
using System.Collections;

namespace WebApplication1
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        string msges = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["ale"]))
            {
                page();
            }
        }
        void page()
        {
            string res = string.Empty;
            switch (Request.Form["ale"].ToString())
            {
                case "login":
                    SqlParameter[] parameter = {
                        new SqlParameter("@userid",Request.Form["user"].ToString()),
                        new SqlParameter("@psd", Request.Form["password"].ToString())
                    };
                    string sql = @"select * from Users where uid=@userid and psd=@psd";
                    DataTable dt=SqlHelper.ExcuteDataTable(CommandType.Text, sql, parameter);
                    res = dt.Rows.Count.ToString();
                    SetCookie("Uid", Request.Form["user"].ToString(), 1);
                    SetCookie("Psd", Request.Form["password"].ToString(), 1);
                    break;
                case "msgs":
                    string ms = Request.Form["msgss"].ToString();
                    bb(ms);
                    res = "1";
                    break;
                case "msglog":
                    //Connect(IPAddress.Parse("192.168.3.251"), 6000);
                    //clientSocket.Listen(0);
                    //Socket serverSocket = clientSocket.Accept();
                    //int bytes = serverSocket.Receive(MsgBuffer, MsgBuffer.Length, 0);
                    //res = Encoding.ASCII.GetString(MsgBuffer, 0, bytes);
                    //ReceiveData();
                    //res = msges;
                    break;
            }
            Context.Response.Write(res);
        }

        #region 
        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        byte[] MsgBuffer = new byte[4096];
        public Hashtable DataTable = new Hashtable();//因为要接收到多个服务器（ip）发送的数据，此处按照ip地址分开存储发送数据
        //连接
        public void Connect(IPAddress ip, int port)
        {
            this.clientSocket.BeginConnect(ip, port, new AsyncCallback(ConnectCallback), this.clientSocket);
        }
        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;
                handler.EndConnect(ar);
            }
            catch (SocketException ex)
            { }
        }
        //发送
        public void Send(string data)
        {
            Send(System.Text.Encoding.UTF8.GetBytes(data));
        }
        private void Send(byte[] byteData)
        {
            try
            {
                int length = byteData.Length;
                byte[] head = BitConverter.GetBytes(length);
                byte[] data = new byte[head.Length + byteData.Length];
                Array.Copy(head, data, head.Length);
                Array.Copy(byteData, 0, data, head.Length, byteData.Length);
                this.clientSocket.BeginSend(data, 0, data.Length, 0, new AsyncCallback(SendCallback), this.clientSocket);
            }
            catch (SocketException ex)
            { }
        }
        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;
                handler.EndSend(ar);
            }
            catch (SocketException ex)
            { }
        }
        //接收
        public void ReceiveData()
        {
            clientSocket.BeginReceive(MsgBuffer, 0, MsgBuffer.Length, 0, new AsyncCallback(ReceiveCallback), null);
        }
        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                int REnd = clientSocket.EndReceive(ar);
                if (REnd > 0)
                {
                    byte[] data = new byte[REnd];
                    Array.Copy(MsgBuffer, 0, data, 0, REnd);
                    msges=Encoding.ASCII.GetString(data);

                    clientSocket.BeginReceive(MsgBuffer, 0, MsgBuffer.Length, 0, new AsyncCallback(ReceiveCallback), null);
                }
                else
                {
                    dispose();
                }
            }
            catch (SocketException ex)
            { }
        }
        private void dispose()
        {
            try
            {
                this.clientSocket.Shutdown(SocketShutdown.Both);
                this.clientSocket.Close();
            }
            catch (Exception ex)
            { }
        }

        #endregion

        public void bb(string sendStr)
        {
            int port = 6000;
            string host = "192.168.3.251";//服务器端ip地址
            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint ipe = new IPEndPoint(ip, port);
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(ipe);

            //send message
            byte[] sendBytes = Encoding.ASCII.GetBytes(sendStr);
            clientSocket.Send(sendBytes);

            //receive message
            //string recStr = "";
            //byte[] recBytes = new byte[4096];
            //int bytes = clientSocket.Receive(recBytes, recBytes.Length, 0);
            //recStr += Encoding.ASCII.GetString(recBytes, 0, bytes);
            //Console.WriteLine(recStr);
            //Console.Read();
            //clientSocket.Close();
        }
        public static void SetCookie(string key, string value, int days)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
            if (cookie == null)
            {
                cookie = new HttpCookie(key);
            }
            cookie.Value = HttpUtility.UrlEncode(value);

            HttpContext.Current.Response.AppendCookie(cookie);
        }
    }
}