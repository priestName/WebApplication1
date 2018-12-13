using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Baidu.Aip.Ocr;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net;

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        public Ocr client;
        public Form3()
        {
            InitializeComponent();

            string APP_ID = "15147577";
            string API_KEY = "XG2oDGFRG6VWCzLXta0qKxEI";
            string SECRET_KEY = "E48m5rAKTLlD4ZmE9mGBqIwXi7OiEbZQ ";

            client = new Ocr(API_KEY, SECRET_KEY);
            client.Timeout = 60000;  // 修改超时时间
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text += textBox1.Text+"/n";
            
        }
        public static string unit_utterance()
        {
            string token = "#####调用鉴权接口获取的token#####";
            string host = "https://aip.baidubce.com/rpc/2.0/unit/bot/chat?access_token=" + token;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
            request.Method = "post";
            request.ContentType = "application/json";
            request.KeepAlive = true;
            string str = "{\"log_id\":\"UNITTEST_10000\",\"version\":\"2.0\",\"service_id\":\"S10000\",\"session_id\":\"\",\"request\":{\"query\":\"你好\",\"user_id\":\"88888\"},\"dialog_state\":{\"contexts\":{\"SYS_REMEMBERED_SKILLS\":[\"1057\"]}}}"; // json格式
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string result = reader.ReadToEnd();
            Console.WriteLine("对话接口返回:");
            Console.WriteLine(result);
            return result;
        }
    }
}
