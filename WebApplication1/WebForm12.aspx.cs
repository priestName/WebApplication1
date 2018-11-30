using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Baidu.Aip.Ocr;
using System.IO;

namespace WebApplication1
{
    public partial class WebForm12 : System.Web.UI.Page
    {
        static public Ocr client;
        protected void Page_Load(object sender, EventArgs e)
        {
            string APP_ID = "14701082";
            string API_KEY = "4Eedc93qsNT084m5Twh3TxPk";
            string SECRET_KEY = "P8Mnu7LVwvzG1YG6PwpWNGCYQHjLYjRb";
            client = new Ocr(API_KEY, SECRET_KEY);
            client.Timeout = 60000;  // 修改超时时间
        }

        protected void Button1_ServerClick(object sender, EventArgs e)
        {
            var image = File.ReadAllBytes("C:\\Users\\admin\\Desktop\\捕获.JPG");
            // 如果有可选参数
            var options = new Dictionary<string, object>{
                    {"language_type", "CHN_ENG"},
                    { "recognize_granularity", "big"}
                };
            // 带参数调用通用文字识别, 图片参数为本地图片//
            var result = client.General(image, options);
            var res = result["words_result"];
            foreach (var item in res)
            {
                var a = item["location"];
                //divs.InnerHtml+= @"<p style='width: "+ a["width"] + "px; height: " + a["height"] + "px; padding - top:" + a["top"] + "px; padding - left:" + a["left"] + "px'>"+ item["words"] + "</p>";
                divs.InnerHtml += item["words"] + "<br />";

            }
        }
    }
}