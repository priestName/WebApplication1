using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form6 : Form
    {
        DateTime jsTime = new DateTime();
        public Form6()
        {
            this.Location.Offset(0,0);
            InitializeComponent();
            if (string.IsNullOrEmpty(ReadText()))
            {
                jsTimes.Visible = false;
                label2.Visible = false;
            }
            else
            {
                shicha(ReadText());
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            WriteJson(dateTimePicker1.Text);
            shicha(dateTimePicker1.Text);
        }
        public void shicha(string Time)
        {
            label1.Visible = false;
            button1.Visible = false;
            dateTimePicker1.Visible = false;
            BackColor = Color.White;
            TransparencyKey = Color.White;
            DateTime EndTime = Convert.ToDateTime(Time);
            DateTime NowTime = DateTime.Now;
            var a= EndTime - NowTime;
            jsTime = new DateTime(0001,01,a.Days,a.Hours,a.Minutes,a.Seconds);
            jsTimes.Text = jsTime.ToString("dd天 hh时 mm分 ss秒");
            timer1.Interval = 1000;
            timer1.Enabled = true;
            jsTimes.Visible = true;
            label2.Visible = true;
            ShowInTaskbar = false;
        }
        public string ReadText()
        {
            string myStr = string.Empty;
            //c#文件流读文件 
            if (!File.Exists("UserTime.txt"))
            {
                File.Create("UserTime.txt").Dispose();
            }
            
            using (FileStream fsRead = new FileStream(@"UserTime.txt", FileMode.Open))
            {
                int fsLen = (int)fsRead.Length;
                byte[] heByte = new byte[fsLen];
                int r = fsRead.Read(heByte, 0, heByte.Length);
                myStr = System.Text.Encoding.UTF8.GetString(heByte);
            }
            return myStr;
        }
        public void WriteJson(string Text)
        {
            byte[] myByte = System.Text.Encoding.UTF8.GetBytes(Text);
            using (FileStream fsWrite = new FileStream(@"UserTime.txt", FileMode.Create, FileAccess.Write))
            {
                fsWrite.Write(myByte, 0, myByte.Length);
            };
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            jsTime = new DateTime(jsTime.Year, jsTime.Month, jsTime.Day, jsTime.Hour, jsTime.Minute, jsTime.Second-1);
            jsTimes.Text = jsTime.ToString("dd天 hh时 mm分 ss秒");

        }
    }
}
