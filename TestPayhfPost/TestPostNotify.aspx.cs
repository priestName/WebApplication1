using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestPayhfPost
{
    public partial class TestPostNotify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.IO.Stream stream = Request.InputStream;
            if (stream != null && stream.Length > 0)
            {
                System.IO.StreamReader streamReader = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8);
                string JsonDate = streamReader.ReadToEnd();
                Dictionary<string, string> PostJsonDate = Lantel.Pay.ChinapnrPay.NsposPay.PostJson(JsonDate);
                if (PostJsonDate != null && PostJsonDate["respCode"] == "000000")
                {
                    string orderId = PostJsonDate["termOrdId"];//本地订单号
                    string outTransId = PostJsonDate["outTransId"];//支付宝订单号

                    DateTime payTime = DateTime.Now;
                    payTime = DateTime.ParseExact(PostJsonDate["transDate"] + PostJsonDate["transTime"], "yyyyMMddHHmmss", CultureInfo.CurrentCulture, DateTimeStyles.None); ; //yyyy-MM-dd HH:mm:ss
                    if (payTime == DateTime.MinValue) { payTime = DateTime.Now; }

                    insertIpName(Directory.GetParent(Server.MapPath("~/")).FullName + @"\Log\", payTime.ToString("yyyyMMddHHmmss"), JsonConvert.SerializeObject(JsonDate, Formatting.None));
                    Response.Write("RECV_ORD_ID_" + orderId);
                    Response.End();
                }
            }
        }
        public void insertIpName(string Path,string name, string text)
        {
            byte[] myByte = System.Text.Encoding.UTF8.GetBytes(text);
            if (!string.IsNullOrEmpty(Path))
            {
                Directory.CreateDirectory(Path);
            }
            using (FileStream fsWrite = new FileStream(Path + name + ".txt", FileMode.Append))
            {
                fsWrite.Write(myByte, 0, myByte.Length);
            };
        }
    }
}