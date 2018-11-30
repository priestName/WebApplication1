using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Sockets;
using System.Text;

namespace WebApplication2.Controllers
{
    public class DefaultController : Controller
    {
        static byte[] buffer = new byte[1024];
        //①创建一个Socket
        public Socket sockets = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // GET: Default
        public ActionResult Index()
        {
            //②连接到指定服务器的指定端口
            sockets.Connect("127.0.0.1", 7788); //localhost代表本机

            WriteLine("已连接");
            return View();
        }
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

        #region 显示内容
        public string WriteLine(string str)
        {
            string msgstr = "";
            Action<string> action = (date) =>
            {
                msgstr = DateTime.Now.ToString("MM-dd HH:mm:ss") + ":" + str + System.Environment.NewLine;
            };
            //Invoke(action, str);
            return msgstr;
        }
        #endregion

        private void button1_Click(string messages)
        {
            var outputBuffer = Encoding.UTF8.GetBytes(messages);
            sockets.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, null, null);
        }

    }
}