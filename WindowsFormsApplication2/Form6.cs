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
using System.Runtime.InteropServices; 

namespace WindowsFormsApplication2
{
    
    public partial class Form6 : Form
    {
        #region 隐藏tab
        [DllImport("user32.dll")]
        public static extern
        Int32 GetWindowLong(IntPtr hwnd, Int32 index);
        [DllImport("user32.dll")]
        public static extern
        Int32 SetWindowLong(IntPtr hwnd, Int32 index, Int32 newValue);
        public const int GWL_EXSTYLE = (-20);
        public static int WS_EX_TOOLWINDOW = 0x00000080;
        #endregion
        TimeSpan jsTime = new TimeSpan();
        public Form6()
        {
            TopLevel = true;
            TopMost = true;
            InitializeComponent();
            string logtime = ReadText();
            dateTimePicker1.Text = DateTime.Now.ToString();
            if (string.IsNullOrEmpty(logtime))
            {
                jsTimes.Visible = false;
                FormBorderStyle = FormBorderStyle.FixedSingle;
            }
            else
            {
                if (Convert.ToDateTime(logtime) < DateTime.Now)
                {
                    jsTimes.Visible = false;
                    FormBorderStyle = FormBorderStyle.FixedSingle;
                }
                else {
                    shicha(logtime);
                }
                
            }
            AddWindowExStyle(Handle, WS_EX_TOOLWINDOW);

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(dateTimePicker1.Text) >DateTime.Now)
            {
                WriteJson(dateTimePicker1.Text);
                shicha(dateTimePicker1.Text);
                int WidthLocation = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width - Width + 15;
                Location = new Point(WidthLocation, 15);
            }
            else
            {
                return;
            }
        }
        public void shicha(string Time)
        {
            label1.Visible = false;
            button1.Visible = false;
            dateTimePicker1.Visible = false;
            BackColor = Color.White;
            TransparencyKey = Color.White;
            FormBorderStyle= FormBorderStyle.None;
            ControlBox = false;
            DateTime EndTime = Convert.ToDateTime(Time);
            DateTime NowTime = DateTime.Now;
            jsTime = EndTime - NowTime;
            jsTimes.Text = string.Format("{0}天 {1}时 {2}分 {3}秒", jsTime.Days, jsTime.Hours, jsTime.Minutes, jsTime.Seconds);
            timer1.Interval = 1000;
            timer1.Enabled = true;
            jsTimes.Visible = true;
            ShowInTaskbar = false;
            Height = 66;
            Width = 190;
            
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
            return myStr == "" ? null:myStr;
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
            TimeSpan tmsp = new TimeSpan(0, 0, 0, -1);
            jsTime = jsTime.Add(tmsp);
            if (jsTime<=tmsp)
            {
                return;
            }
            jsTimes.Text = string.Format("{0}天 {1}时 {2}分 {3}秒", jsTime.Days, jsTime.Hours, jsTime.Minutes, jsTime.Seconds);
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            string logtime = ReadText();
            if (!string.IsNullOrEmpty(logtime))
            {
                if (Convert.ToDateTime(logtime) > DateTime.Now)
                {
                    int WidthLocation = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width - Width + 15;
                    Location = new Point(WidthLocation, 15);
                }
            }
        }
        /// <summary>
        /// 隐藏tab
        /// </summary>
        /// <param name="hwnd">Handle</param>
        /// <param name="val">WS_EX_TOOLWINDOW</param>
        public static void AddWindowExStyle(IntPtr hwnd, Int32 val)
        {
            int oldValue = GetWindowLong(hwnd, GWL_EXSTYLE);
            if (oldValue == 0)
            {
                throw new System.ComponentModel.Win32Exception();
            }
            if (0 == SetWindowLong(hwnd, GWL_EXSTYLE, oldValue | val))
            {
                throw new System.ComponentModel.Win32Exception();
            }
        }
    }
}
