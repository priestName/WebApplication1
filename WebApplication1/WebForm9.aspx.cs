using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Sockets;
using System.Text;

namespace WebApplication1
{
    public partial class WebForm9 : System.Web.UI.Page
    {
        static byte[] buffer = new byte[1024];
        //①创建一个Socket
        public static Socket sockets;
        static Class2 c2 = new Class2();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                sockets = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
        }
        //发送
        protected void Button1_Click(object sender, EventArgs e)
        {
            var outputBuffer = Encoding.UTF8.GetBytes(msgshw.InnerText);
            sockets.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, null, null);
            sockets.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), sockets);
            //int asoc = sockets.Receive(buffer);
            //WriteLine(Encoding.ASCII.GetString(buffer, 0, asoc));
        }
        //连接
        protected void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //②连接到指定服务器的指定端口
                sockets.Connect("127.0.0.1", 7788); //localhost代表本机
                //③实现异步接受消息的方法 客户端不断监听消息
                sockets.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), sockets);
                WriteLine("已连接");
                Button2.Enabled = false;
            }
            catch (Exception ex)
            {
                WriteLine(ex.ToString());
            }
        }
        #region 显示内容
        public void WriteLine(string str)
        {
            WebForm9 w9 = new WebForm9();
            //Control.CheckForIllegalCrossThreadCalls = false;
            //Action<string> action = (date) =>
            //{
            msgss.InnerText+=DateTime.Now.ToString("MM-dd HH:mm:ss") + ":" + str + System.Environment.NewLine;
            //};
            //Invoke(action, str);
            //msgss.SelectionStart = msgss.InnerText.Length;
            //msgss.ScrollToCaret();
            this.msgss.InnerText += str;
        }
        #endregion

        #region 接收信息
        /// <summary>
        /// 接收信息
        /// </summary>
        /// <param name="ar"></param>
        public void ReceiveMessage(IAsyncResult ar)
        {
            try
            {
                var socket = ar.AsyncState as Socket;

                var length = socket.EndReceive(ar);
                //读取出来消息内容
                var message = Encoding.ASCII.GetString(buffer, 0, length);

                //显示消息
                if (!string.IsNullOrEmpty(message))
                {
                    WriteLine(message);
                }
                //接收下一个消息(因为这是一个递归的调用，所以这样就可以一直接收消息了）
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), socket);
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
        }
        #endregion
    }
}