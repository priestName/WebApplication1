using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestPayhfPost
{
    public partial class index2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(LogDate.Text))
                LogDate.Text = DateTime.Now.ToString("yyyyMMddHH");
            Log.InnerHtml = "";
            FileInfo[] files = new DirectoryInfo(Directory.GetParent(Server.MapPath("~/")).FullName + @"\"+ PathText.Value + @"\").GetFiles(LogDate.Text + "*");
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
}