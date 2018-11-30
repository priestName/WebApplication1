using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        static byte[] buffer = new byte[1024];
        //①创建一个Socket
        public Socket sockets= new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public static string userids = string.Empty;
        public Form1(string userid)
        {
            userids = userid;
            InitializeComponent();
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
                var message =Encoding.UTF8.GetString(buffer, 0, length);

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
        public void WriteLine(string str)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            //Action<string> action = (date) => 
            //{
                richTextBox_accept.Text += DateTime.Now.ToString("MM-dd HH:mm:ss") + ":" + str + System.Environment.NewLine;
            //};
            //Invoke(action, str); 
            richTextBox_accept.SelectionStart = richTextBox_accept.Text.Length;
            richTextBox_accept.ScrollToCaret();
        }
        #endregion
        //发送
        private void button1_Click(object sender, EventArgs e)
        {
            var messages = richTextBox1.Text;
            var outputBuffer = Encoding.UTF8.GetBytes(userids+":"+messages);
            sockets.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, null, null);
        }
        //连接
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //②连接到指定服务器的指定端口
                sockets.Connect("127.0.0.1", 7788); //localhost代表本机

                WriteLine("已连接");

                //③实现异步接受消息的方法 客户端不断监听消息
                sockets.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), sockets);
            }
            catch (Exception ex)
            {
                WriteLine("client:error " + ex.Message);
            }
            finally
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var messages = richTextBox1.Text;
            if (messages.Substring(0,1)=="@")
            {
                var outputBuffer = Encoding.UTF8.GetBytes(messages);
                sockets.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, null, null);
            }
        }
    }
}
