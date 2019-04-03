using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApplication2
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            JObject JsonText = JObject.Parse(@"{'19-03': '19-11:43'}");
            if (JsonText.Property(DateTime.Now.ToString("yy-MM")) != null)
            {
                JsonText[DateTime.Now.ToString("yy-MM")]+=("," + DateTime.Now.ToString("dd-hh:mm"));
                //.Add(DateTime.Now.ToString("yy-MM"), DateTime.Now.ToString("dd-hh:mm"));
            }
            
            JToken js = JsonText;
            string a = js.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            JObject JsonText = JObject.Parse("{}");
            if (JsonText.Property(DateTime.Now.ToString("yy-MM")).ToString() == "")
            {
                JsonText.Add(DateTime.Now.ToString("yy-MM"),DateTime.Now.ToString("dd-hh:mm"));
            }
            string a = JsonText.ToString();
            //WriteJson(JsonText.ToString());
        }
        public void WriteJson(string Text)
        {
            byte[] myByte = System.Text.Encoding.UTF8.GetBytes(Text);
            using (FileStream fsWrite = new FileStream(@"E:\priestName\EditCount.txt", FileMode.Create, FileAccess.Write))
            {
                fsWrite.Write(myByte, 0, myByte.Length);

            };
        }
    }
}
