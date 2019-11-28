using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;

namespace ConsoleApp1
{
    class Class5
    {
        public void main()
        {
            string c = "http://localhost:7150/api/get_projectpayment?token=70b55debb208a9f0af416b1bbdab0f73&schoolname=&projectname=&startdate=&enddate=";
            string a = "get_students?token=d66a19bc4b26cd68299116f07517ae0c&page=&pagesize=&hours=";
            string u = "http://localhost:500/api/";
            var b = Post(c);
            //var b = Post(u,a);
            Console.WriteLine(b);
        }
        public static string Post(string url)
        {
            string result = "";
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "GET";
                req.ContentLength = 0;
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();
                //获取内容
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {

                result = e.Message;
            }
            
            return result;
        }
        public static string Post(string url, string content)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            #region 添加Post 参数
            byte[] data = Encoding.UTF8.GetBytes(content);
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
    }
}
