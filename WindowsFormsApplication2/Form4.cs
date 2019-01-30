using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication2
{
    public partial class Form4 : Form
    {
        #region 全局参数
        private int times = 0;
        /// <summary>
        /// 频率
        /// </summary>
        private int jiange = 0;
        /// <summary>
        /// 1左键锁定;2右键锁定;3左键锁定+数字切换;4左键连点;5右键连点
        /// </summary>
        private int i = 0;
        /// <summary>
        /// 点击次数
        /// </summary>
        private int cishu = 0;
        /// <summary>
        /// 输入数据监测
        /// </summary>
        private bool checktime = false;
        /// <summary>
        /// 等待时间
        /// </summary>
        private int dengdai = 0;

        private bool zt = false;
        /// <summary>
        /// 单位时间
        /// </summary>
        private int timeInterval = 0;
        #endregion
        public Form4()
        {
            TopLevel = true;
            InitializeComponent();
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            //安装勾子
            //KeyBordHook _keyboardHook = new KeyBordHook();
            //_keyboardHook.InstallHook(this.OnKeyPress);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            label1.Text = "时间：" + times.ToString();
            label5.Text = "当前数字：" + cishu.ToString();
            textBox1.Text = "1";
            textBox2.Text = "1";
            textBox3.Text = "1000";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "重置(Enter/F12)")
            {
                NewFrom();
                return;
            }
             textBox1_TextChanged(null,null);
            textBox2_TextChanged(null, null);
            textBox3_TextChanged(null, null);
            EditI();
            if (!checktime || i==0)
            {
                return;
            }
            //单位时间
            timeInterval = string.IsNullOrEmpty(textBox3.Text.ToString()) ? 1000 : Convert.ToInt32(textBox3.Text.ToString());
            //频率
            jiange = string.IsNullOrEmpty(textBox1.Text.ToString()) ? 0 : Convert.ToInt32(textBox1.Text.ToString());
            //等待时间
            dengdai = string.IsNullOrEmpty(textBox2.Text.ToString())?0:Convert.ToInt32(textBox2.Text.ToString());
            timer1.Enabled = true;
            timer1.Interval = timeInterval;


            button1.Text = "重置(Enter/F12)";
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            radioButton3.Enabled = false;
            radioButton4.Enabled = false;
            radioButton5.Enabled = false;
        }
        #region 调用user32.dll文件
        [DllImport("user32",EntryPoint = "mouse_event",SetLastError =true)]
        static extern void mouse_event(int flags, int dX, int dY, int buttons, int extraInfo);
        /// <summary>
        /// 模拟鼠标移动
        /// </summary>
        const int MOUSEEVENTF_MOVE = 0x1;
        /// <summary>
        /// 左键按下
        /// </summary>
        const int MOUSEEVENTF_LEFTDOWN = 0x2;
        /// <summary>
        /// 左键松开
        /// </summary>
        const int MOUSEEVENTF_LEFTUP = 0x4;
        /// <summary>
        /// 右键按下
        /// </summary>
        const int MOUSEEVENTF_RIGHTDOWN = 0x8;
        /// <summary>
        /// 右键松开
        /// </summary>
        const int MOUSEEVENTF_RIGHTUP = 0x10;
        /// <summary>
        /// 中键按下
        /// </summary>
        const int MOUSEEVENTF_MIDDLEDOWN = 0x20;
        /// <summary>
        /// 中键松开
        /// </summary>
        const int MOUSEEVENTF_MIDDLEUP = 0x40;
        /// <summary>
        /// 这表示鼠标轮被滚动，如果鼠标有一个滚轮。滚动的数量由dwData给出。
        /// </summary>
        const int MOUSEEVENTF_WHEEL = 0x800;
        /// <summary>
        /// 鼠标移动位置;dX和dY参数含有规范化的绝对坐标。
        /// </summary>
        const int MOUSEEVENTF_ABSOLUTE = 0x8000;


        //[DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
        //public static extern void keybd_event(Keys bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        //keybd_event(Keys.D1, 0, 0, 0);
        #endregion
        private void timer1_Tick(object sender, EventArgs e)
        {
            times = timeInterval;
            int time = times/1000;
            label1.Text = "时间：" +(time).ToString();

            if (dengdai != 0)//计算等待时间
            {
                if (dengdai == time) { dengdai = 0; time = 0; }
                return;
            }
            if (!zt)
            {
                if (i == 3 || i == 1)
                {
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);//执行左键按下操作
                    zt = true;
                }
                else if (i == 2)
                {
                    mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);//执行右键按下操作
                    zt = true;
                }
            }
            if (i == 1 || i == 2)
            { }
            else {
                if (time == jiange)
                {
                    time = 0;
                    if (i==3)
                    {

                        if (cishu != 9)
                        {
                            cishu++;
                            label5.Text = cishu.ToString();
                            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);//左键弹起
                            SendKeys.Send("{" + cishu + "}");//数字键
                            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);//左键按下
                        }
                        else {
                            NewFrom();
                        }
                    }
                    if (i == 4)
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN|MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    }
                    if (i == 5)
                    {
                        mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                    }
                }
            }
        }
        /// <summary>
        /// 重置界面
        /// </summary>
        private void NewFrom()
        {
            timer1.Enabled = false;
            times = 0;
            jiange = 0;
            cishu = 0;
            i = 0;
            zt = false;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            radioButton3.Enabled = true;
            radioButton4.Enabled = true;
            radioButton5.Enabled = true;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
            textBox1.Text = "1";
            textBox2.Text = "1";
            textBox3.Text = "1000";
            button1.Text = "开始(Enter/F12)";
            label1.Text = "时间：" + times.ToString();
            label5.Text = "当前数字：" + cishu.ToString();
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);//左键弹起
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }
        private void EditI()
        {
            if (radioButton1.Checked)
            {
                i = 1;//左键锁定
            }
            else if (radioButton2.Checked)
            {
                i = 2;//右键锁定
            }
            else if (radioButton3.Checked)
            {
                i = 3;//左键锁定+数字切换
            }
            else if (radioButton4.Checked)
            {
                i = 4;//左键连点
            }
            else if (radioButton5.Checked)
            {
                i = 5;//右键连点
            }
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(textBox3.Text.ToString()) != 0)
                {
                    textBox3.BackColor = Color.White;
                    textBox3.ForeColor = Color.Black;
                    this.errorProvider1.SetError(label6, null);
                    checktime = true;
                }
                else {
                    textBox3.BackColor = Color.Red;
                    textBox3.ForeColor = Color.White;
                    this.errorProvider1.SetError(label6, "请输入数字0~999999999");
                    checktime = false;
                }
            }
            catch (Exception s)
            {
                textBox3.BackColor = Color.Red;
                textBox3.ForeColor = Color.White;
                this.errorProvider1.SetError(label6, "请输入数字0~999999999");
                checktime = false;
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked || radioButton2.Checked)
            {
                textBox1.BackColor = Color.White;
                textBox1.ForeColor = Color.Black;
                checktime = true;
            }
            else
            {
                try
                {
                    if (Convert.ToInt32(textBox1.Text.ToString()) != 0)
                    {
                        textBox1.BackColor = Color.White;
                        textBox1.ForeColor = Color.Black;
                        this.errorProvider1.SetError(label7, null);
                        checktime = true;
                    }
                    else {
                        textBox1.BackColor = Color.Red;
                        textBox1.ForeColor = Color.White;
                        this.errorProvider1.SetError(label7, "请输入数字0~999999999");
                        checktime = false;
                    }
                }
                catch (Exception)
                {
                    textBox1.BackColor = Color.Red;
                    textBox1.ForeColor = Color.White;
                    this.errorProvider1.SetError(label7, "请输入数字0~999999999");
                    checktime = false;
                }
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(textBox2.Text.ToString()) != 0)
                {
                    textBox2.BackColor = Color.White;
                    textBox2.ForeColor = Color.Black;
                    this.errorProvider1.SetError(label8, null);
                    checktime = true;
                }
                else {
                    textBox2.BackColor = Color.Red;
                    textBox2.ForeColor = Color.White;
                    this.errorProvider1.SetError(label8, "请输入数字0~999999999");
                    checktime = false;
                }
            }
            catch (Exception)
            {
                textBox2.BackColor = Color.Red;
                textBox2.ForeColor = Color.White;
                this.errorProvider1.SetError(label8, "请输入数字0~999999999");
                checktime = false;
            }
        }

        private void Form4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12 || e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            NewFrom();
        }
        //public void OnKeyPress(KeyBordHook.HookStruct hookStruct, out bool handle)
        //{
        //    handle = false;
        //    Keys key = (Keys)hookStruct.vkCode;
        //    if (key == Keys.F12 || key == Keys.Enter)
        //    {
        //        button1_Click(null, null);
        //    }
        //    return;
        //}
    }
}
