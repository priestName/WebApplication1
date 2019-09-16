using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SignalRChat1
{
    /// <summary>
    /// Handler 的摘要说明
    /// </summary>
    public class Handler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            UserDLL userDLL = new UserDLL();
            string type = context.Request.Form["type"].ToLower();
            string name = context.Request.Form["name"].ToLower();
            string pwd = context.Request.Form["password"].ToLower();
            string text = "true";
            switch (type)
            {
                case "login":
                    text = (userDLL.SetUser(MD5(pwd), name) != null).ToString();
                    break;
                case "loginto":
                    if (userDLL.SetUser(string.Empty, name) == null)
                    {
                        text = userDLL.AddUser(new Models.SockedUser { Name = name, Password = MD5(pwd) }).ToString();
                    }
                    else {
                        text = "昵称已存在";
                    }
                    break;
                default:
                    break;
            }
            SetCookie("UserName",name);
            context.Response.Write(text.ToLower());
        }
        public static void SetCookie(string key, string value)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
            if (cookie == null)
            {
                cookie = new HttpCookie(key);
            }
            cookie.Value = HttpUtility.UrlEncode(value);
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        string MD5(string input)
        {
            MD5 md5Hasher = System.Security.Cryptography.MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            foreach (byte d in data)
            {
                sBuilder.Append(d.ToString("x2"));
            }
            return sBuilder.ToString();
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}