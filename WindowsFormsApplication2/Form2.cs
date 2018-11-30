using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text;
            string psd = textBox2.Text;
            UserIP users;
            using (DataClasses1DataContext ddc = new DataClasses1DataContext())
            {
                ddc.Log = Console.Out;
                //取出student
                users = ddc.UserIP.SingleOrDefault<UserIP>(s => s.UserId == user && s.PassWord == psd);
            }
            if (users != null)
            {
                this.Visible=false;
                Form1 f1=new Form1(user);
                f1.ShowDialog();
                this.Visible = true;
            }
        }
    }
}
