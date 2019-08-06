using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;


namespace TestPayhfPost
{
    public partial class TestHttpPost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = Directory.GetParent(Server.MapPath("~/")).FullName +"\\";
            string xmlData = string.Empty;
            
            if (Request.RequestType == "POST")
            {
                try
                {
                    //接收并读取POST过来的文件流
                    StreamReader reader = new StreamReader(Request.InputStream,Encoding.UTF8);
                    xmlData = reader.ReadToEnd();
                    reader.Close();
                    if (!string.IsNullOrEmpty(xmlData))
                    {
                        insertIpName(Directory.GetParent(Server.MapPath("~/")).FullName + @"\OldLog\", JsonConvert.SerializeObject(xmlData, Formatting.None));
                        Dictionary<string, string> DataStr = TestPost(xmlData);
                        insertIpName(Directory.GetParent(Server.MapPath("~/")).FullName + @"\Log\", JsonConvert.SerializeObject(DataStr, Formatting.None));
                        Response.Write("RECV_ORD_ID_{" + DataStr["ordId"].ToString() + "}");
                        Response.End();
                    }
                }
                catch (Exception ex)
                {
                    insertIpName(Directory.GetParent(Server.MapPath("~/")).FullName + @"\ErroLog\", ex.Message);
                    throw;
                }
                xmlData = string.Empty;
                Response.End();
            }
            else
            {
                FileInfo[] files = new DirectoryInfo(Directory.GetParent(Server.MapPath("~/")).FullName + @"\Log\").GetFiles(DateTime.Now.ToString("yyyyMMddHH") + "*");
                foreach (var item in files)
                {
                    string myStr = string.Empty;
                    using (FileStream fsRead = item.Open(FileMode.Open))
                    {
                        int fsLen = (int)fsRead.Length;
                        byte[] heByte = new byte[fsLen];
                        int r = fsRead.Read(heByte, 0, heByte.Length);
                        myStr = Encoding.UTF8.GetString(heByte);
                        Log.InnerHtml += "<a>TextName：" + item.Name + "</a><p>" + myStr + "</p><br/>";
                    }
                }
            }
        }
        public static Dictionary<string, string> TestPost(string TestUrlPost)
        {
            string TopItem = TestUrlPost.Substring(0, TestUrlPost.IndexOf('='));
            if (TestUrlPost.LastIndexOf(TopItem) != TestUrlPost.IndexOf(TopItem))
            {
                TestUrlPost = TestUrlPost.Substring(0, TestUrlPost.LastIndexOf(TopItem));
            }
            string[] UrlPost = TestUrlPost.Split('&');
            Dictionary<string, string> UrlPostList = new Dictionary<string, string>();
            foreach (var item in UrlPost)
            {
                UrlPostList.Add(item.Substring(0, item.IndexOf('=')), item.Substring(item.IndexOf('=') + 1));
            }
            return UrlPostList;
        }
        public void insertIpName(string name,string text)
        {
            byte[] myByte = System.Text.Encoding.UTF8.GetBytes(text);
            if (!string.IsNullOrEmpty(name))
            {
                Directory.CreateDirectory(name);
            }
            using (FileStream fsWrite = new FileStream(name + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt", FileMode.Append))
            {
                fsWrite.Write(myByte, 0, myByte.Length);
            };
        }
    }
}