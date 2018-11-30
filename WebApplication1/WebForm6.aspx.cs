using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        string msges = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["ale"]))
            {
                page();
            }
        }
        void page()
        {
            string res = string.Empty;
            switch (Request.Form["ale"].ToString())
            {
                case "login":
                    SqlParameter[] parameter = {
                        new SqlParameter("@userid",Request.Form["user"].ToString()),
                        new SqlParameter("@psd", Request.Form["password"].ToString())
                    };
                    string sql = @"select * from Users where uid=@userid and psd=@psd";
                    DataTable dt = SqlHelper.ExcuteDataTable(CommandType.Text, sql, parameter);
                    res = dt.Rows.Count.ToString();
                    SetCookie("Uid", Request.Form["user"].ToString(), 1);
                    SetCookie("Psd", Request.Form["password"].ToString(), 1);
                    break;
            }
            Context.Response.Write(res);
        }
        public static void SetCookie(string key, string value, int days)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
            if (cookie == null)
            {
                cookie = new HttpCookie(key);
            }
            cookie.Value = HttpUtility.UrlEncode(value);

            HttpContext.Current.Response.AppendCookie(cookie);
        }
    }
}