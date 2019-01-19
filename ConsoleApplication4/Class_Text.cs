using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Class_Text
    {
        int a = 0;
        string ips = string.Empty;
        public void saa()
        {
            string myStr = string.Empty;
            using (FileStream fsRead = new FileStream(@"Log\User_Ip_Name.txt", FileMode.Open))
            {
                int fsLen = (int)fsRead.Length;
                byte[] heByte = new byte[fsLen];
                int r = fsRead.Read(heByte, 0, heByte.Length);
                myStr = System.Text.Encoding.UTF8.GetString(heByte).Replace("\r\n", "");
            }
            JObject job = JObject.Parse(myStr.Substring(0, myStr.Length - 1) + "}");
            int a= job.Count;
            //while (true)
            //{
            //    var msg = Console.ReadLine();
            //    if (!string.IsNullOrEmpty(msg))
            //    {
            //        a++;
            //        InsertIpNameText("User" + a + ":{ip:'" + msg + "',name:'User" + a + "'},");
            //    }
            //}
            //ips= Console.ReadLine();
            //jsonText(Console.ReadLine());
        }

        public void jsonText(string Name)
        {
            string myStr = string.Empty;
            //c#文件流读文件 
            using (FileStream fsRead = new FileStream(@"C:\Users\admin\Desktop\Log\User_Ip_Name.txt", FileMode.Open))
            {
                int fsLen = (int)fsRead.Length;
                byte[] heByte = new byte[fsLen];
                int r = fsRead.Read(heByte, 0, heByte.Length);
                myStr = System.Text.Encoding.UTF8.GetString(heByte);
            }
            myStr = myStr.Replace("\r\n", "");
            myStr = myStr.Substring(0, myStr.Length - 1) + "}";
            JObject job = JObject.Parse(myStr);
            foreach (var item in job)
            {
                if (item.Value["ip"].ToString() == ips)
                {
                    item.Value["name"] = Name;
                }
            }
            Console.WriteLine(job["User1"]["name"]);
        }

        public void InsertIpNameText(string msg)
        {
            byte[] myByte = System.Text.Encoding.UTF8.GetBytes("\r\n"+msg);
            Directory.CreateDirectory(@"Log\");
            using (FileStream fsWrite = new FileStream(@"Log\User_Ip_Name.txt", FileMode.Create,FileAccess.Write))
            {
                if (fsWrite.Length==0)
                {
                    fsWrite.Write(Encoding.UTF8.GetBytes("{"), 0, Encoding.UTF8.GetBytes("{").Length);
                }
                fsWrite.Write(myByte, 0, myByte.Length);
            };
        }

        public void InsertLogText(string msg) {
            byte[] myByte = System.Text.Encoding.UTF8.GetBytes(msg);
            //string TextName = DateTime.Now.Date.ToString("yyyy_MM_dd") + "_log.txt";
            string TextName = "User_Ip_Name.txt";
            using (FileStream fsWrite = new FileStream(@"C:\Users\admin\Desktop\Log\"+ TextName, FileMode.Append))
            {
                fsWrite.Position = fsWrite.Length;
                fsWrite.Write(myByte, 0, myByte.Length);
            };
            //c#文件流读文件 
            using (FileStream fsRead = new FileStream(@"C:\Users\admin\Desktop\Log\" + TextName, FileMode.Open))
            {
                int fsLen = (int)fsRead.Length;
                byte[] heByte = new byte[fsLen];
                int r = fsRead.Read(heByte, 0, heByte.Length);
                string myStr = System.Text.Encoding.UTF8.GetString(heByte);
                Console.WriteLine(myStr);
                Console.ReadKey();
            }
        }
    }
}
