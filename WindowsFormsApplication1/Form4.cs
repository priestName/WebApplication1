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
        string sockName=string.Empty;
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
            label2.Text = string.Empty;
            richTextBox2.Focus();

            try
            {
                //①创建一个Socket
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //②连接到指定服务器的指定端口
                socket.Connect("127.0.0.1", 7788); //localhost代表本机    47.106.232.163   192.168.3.251
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
            if (textBox1.Visible == true)
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    this.errorProvider1.SetError(textBox1, "请输入内容");
                    return;
                }
                this.errorProvider1.SetError(textBox1, "");
                if (label2.Text == label1.Text.Split(':')[1])
                {
                    label2.Text = textBox1.Text;
                }
                var message = "EditName:" + textBox1.Text;
                label1.Text = "我的ID:"+ textBox1.Text;
                var outputBuffer = Encoding.UTF8.GetBytes(message);
                socket.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, null, null);
                textBox1.Clear();
                textBox1.Visible = false;
            }
            else
            {
                if (string.IsNullOrEmpty(richTextBox2.Text))
                {
                    this.errorProvider1.SetError(richTextBox2, "请输入内容");
                    return;
                }
                this.errorProvider1.SetError(richTextBox2, "");
                //④接受用户输入，将消息发送给服务器端
                var message = "msg:" + richTextBox2.Text; 
                if (!string.IsNullOrEmpty(label2.Text))
                {
                    message = "msg:" + label2.Text + ":" + richTextBox2.Text;
                    WriteLine(label1.Text.Split(':')[1] + ":" + richTextBox2.Text);
                }
                var outputBuffer = Encoding.UTF8.GetBytes(message);
                socket.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, null, null);
                richTextBox2.Clear();
            }
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
                string message = Encoding.UTF8.GetString(buffer, 0, length);
                string msgName = message.Split(':')[0];
                //显示消息
                if (msgName == "msg")
                {
                    WriteLine(message.Split(':')[1]+":"+ message.Split(':')[2]);
                } else if (msgName == "name")
                {
                    label1.Text = "我的ID:"+ message.Split(':')[1];
                } else if (msgName == "clients")
                {
                    treeView1.Nodes.Clear();
                    groupBox1.Text = "在线人数"+ message.Split(':')[1].Split('|').Length;
                    foreach (var item in message.Split(':')[1].Split('|'))
                    {
                        this.treeView1.Invoke(new Action(() => { treeView1.Nodes.Add(item); }));
                    }
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

        private void 刷新列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var message = "EditName:shuaxliebiao111";
            var outputBuffer = Encoding.UTF8.GetBytes(message);
            socket.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, null, null);
            
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeView1.SelectedNode = treeView1.GetNodeAt(e.X, e.Y);//选中该节点
                Point p = this.treeView1.PointToClient(Cursor.Position);
                this.contextMenuStrip1.Show(this.treeView1, p);
            }
        }

        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            label2.Text= treeView1.SelectedNode.Text;
            //sockName = treeView1.SelectedNode.Text;
        }

        private void 选择聊天对象ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label2.Text = treeView1.SelectedNode.Text;
            //sockName = treeView1.SelectedNode.Text;
        }

        private void label1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Visible == true)
            {
                textBox1.Visible = false;
                richTextBox2.Focus();
            }
            else {
                textBox1.Visible = true;
                textBox1.Focus();
            }

        }

        private void label2_DoubleClick(object sender, EventArgs e)
        {
            label2.Text = string.Empty;
            //sockName = string.Empty;
        }
    }
}
