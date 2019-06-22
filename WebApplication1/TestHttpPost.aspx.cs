using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class TestHttpPost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.RequestType == "POST")
            {
                //接收并读取POST过来的XML文件流
                StreamReader reader = new StreamReader(Request.InputStream);
                String xmlData = reader.ReadToEnd();
                //Label1.Text = xmlData;
                insertIpName(xmlData);
                //把数据重新返回给客户端
                try
                {
                    JObject aa = JObject.Parse(xmlData);
                    JObject aa1 = JObject.Parse(aa["jsonData"].ToString());
                    Response.Write("RECV_ORD_ID_{" + aa1["ordId"] + "}");
                }
                catch (Exception )
                {
                    Response.Write("RECV_ORD_ID_{ordId}");
                }
                
                //Response.Write(DateTime.Now.ToString() + "测试成功！");
                Response.End();
            }
        }
        public void insertIpName(string text)
        {
            byte[] myByte = System.Text.Encoding.UTF8.GetBytes("\r\n" + text);
            Directory.CreateDirectory(@"Log\");
            using (FileStream fsWrite = new FileStream(@"Log\"+DateTime.Now.ToString("yyyyMMddHHmmss") +".txt", FileMode.Append))
            {
                if (fsWrite.Length == 0)
                {
                    fsWrite.Write(Encoding.UTF8.GetBytes("{"), 0, Encoding.UTF8.GetBytes("{").Length);
                }
                fsWrite.Write(myByte, 0, myByte.Length);
            };
        }
    }
}