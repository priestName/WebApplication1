using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SignalRChat1
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["user"] != "Priest@731097019")
            {
                Response.Redirect("index.html",true);
            }
        }
        public static void SetDefEnterControl(System.Web.UI.Control Ctrl)
        {
            Page mPage = Ctrl.Page;
            string mScript;
            mScript = @"<script language=""javascript""> ";
            mScript += "function document.onkeydown() {";
            mScript += "var e = event.srcElement; ";
            mScript += "var k = event.keyCode; ";
            mScript += @"if (k == 13 && e.type != ""textarea"") { ";
            mScript += "document.all." + Ctrl.ClientID + ".click(); ";
            mScript += "event.cancelBubble = true; ";
            mScript += "event.returnValue = false; ";
            mScript += "} }</script>";

            if (mPage.IsClientScriptBlockRegistered("SetEnterControl") == false)
                mPage.RegisterClientScriptBlock("SetEnterControl", mScript);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();

            string strInput = TextBox1.Text;
            p.StandardInput.WriteLine(strInput + "&exit");
            p.StandardInput.AutoFlush = true;
            string strOuput = p.StandardOutput.ReadToEnd();//10.0.17134.1 2018//6.3.9600 2013
            textarea1.InnerHtml += strOuput.Replace("Microsoft Windows [版本 6.3.9600]\r\n(c) 2013 Microsoft Corporation。保留所有权利。\r\n", "").Replace("&exit", "").Replace("\r\n", "<br />");
            TextBox1.Text ="";

            p.WaitForExit();
            p.Close();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(FileName.Text) || FileName.Text != "")
            {
                try
                {
                    if (FileName.Text == "五子棋")
                    {
                        Process.Start(@"C:\Users\Administrator\Desktop\新建文件夹\Debug\ConsoleApplication2.exe");
                    }
                    else if (FileName.Text == "MC")
                    {
                        Process.Start(@"C:\软件包\像素宝可梦世代\start.bat");
                    }
                    else
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = FileName.Text;
                        if (!string.IsNullOrEmpty(Arguments.Text) || Arguments.Text != "")
                            p.StartInfo.Arguments = Arguments.Text;
                        p.Start();
                    }
                }
                catch (Exception ex)
                {
                    HiddenField1.Value = ex.Message;
                }
            }
                
            //string ph = @"C:\Windows\system32\";
            //Process.Start("explorer", ph);
            //Process.Start(ph);
        }
    }
}