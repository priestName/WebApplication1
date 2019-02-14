using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlTypes;

namespace WebApplication1
{
    public partial class WebForm21 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //IsSys.Value = "";
            //var ismobile = System.Web.HttpContext.Current.Request.Browser.IsMobileDevice;
            //var device = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"].ToString().ToLower();
            //mozilla/5.0 (iphone; cpu iphone os 9_1 like mac os x) applewebkit/601.1.46 (khtml, like gecko) version/9.0 mobile/13b143 safari/601.1
            NProcessRequest(Context);
        }
        public void NProcessRequest(HttpContext context)
        {
            string elxStr = @"<html xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:x='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'>
                                <head>
                                    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>    
                                    <style type='text/css'>td{text-align:center;border:solid 1px #000} br{mso-data-placement:same-cell} td.str-format{mso-number-format:\@;}.title td{background-color:#ff0000}</style>
                                </head>
                                <body>
                                    <table align='center' class='title'>
                                          <tbody>
                                            <tr>
                                                <td width='70px'>a1</td>
                                                <td width='70px'>a11</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <table align='center'>
                                          <tbody>
                                            <tr>
                                                <td width='70px'>b1</td>
                                                <td width='70px'>b11</td>
                                            </tr>
                                            <tr>
                                                <td width='70px'>b2</td>
                                                <td width='70px'>b22</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </body>";
            context.Response.Clear();
            context.Response.Buffer = true;
            context.Response.AppendHeader("Content-Disposition", "attachment;filename=" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.Response.ContentType = "application/vnd.ms-excel";
            context.Response.Write(elxStr);
            context.Response.End();
        }
    }
}