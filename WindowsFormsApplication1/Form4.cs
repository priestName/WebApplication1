using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form4 : Form
    {
        string sockId;
        bool start=true;
        static Socket socket;
        static byte[] buffer = new byte[1024];
        public Form4()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            textBox1.Clear();
            richTextBox1.Clear();
            richTextBox2.Clear();
            label1.Text = "我的ID:Null";

            try
            {
                //①创建一个Socket
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //②连接到指定服务器的指定端口
                socket.Connect("192.168.3.251", 7788); //localhost代表本机    47.106.232.163   192.168.3.251

                WriteLine("client:connect to server success!");


                //③实现异步接受消息的方法 客户端不断监听消息
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), socket);
            }
            catch (Exception ex)
            {
                WriteLine("client:error " + ex.Message);
            } 
            finally
            {
               
            }
        }
        private void Begin()
        {
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), socket);
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            //④接受用户输入，将消息发送给服务器端
            var message = "msg:"+richTextBox2.Text;
            var outputBuffer = Encoding.UTF8.GetBytes(message);
            socket.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, null, null);
            richTextBox2.Clear();
            //socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), socket);
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
                if (message.Split(':')[0]=="msg")
                {
                    WriteLine(message.Split(':')[1]);
                }
                start = false;
                //接收下一个消息(因为这是一个递归的调用，所以这样就可以一直接收消息了）
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), socket);
            }
            catch (Exception ex)
            {
                WriteLine("client:error "+ex.Message);
            }
        }
        #endregion

        #region 扩展方法
        public void WriteLine(string str)
        {
            if (start && str.Split(':')[0]!= "client")
            {
                label1.Text = "我的ID:" + str;
                return;
            }
            string datatime = DateTime.Now.ToString("MM-dd HH:mm:ss");
            richTextBox1.Text += String.Format("[{0}] {1}\r\n", datatime, str);
            //richTextBox1.Select(0, datatime.Length+str.Length);
            //richTextBox1.SelectionColor = color;
            richTextBox1.Focus();
            richTextBox1.Select(richTextBox1.Text.Length,0);
            richTextBox1.ScrollToCaret();
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            sockId = textBox1.Text;
            textBox1.ReadOnly = true;
        }
    }
}
