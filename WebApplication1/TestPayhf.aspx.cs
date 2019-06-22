using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lantel.Pay;
using Newtonsoft.Json.Linq;

namespace WebApplication1
{
    public partial class TestPayhf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            JObject a = Pay.BrandPay("310000016000594425", TextBox1.Text, "0.50", "测试", "W1", "wx7d7a394a192ab065", "oZG_U05SBo2zok7plQyA_fiQk67o", "", "http://test.priest.ink:8050/TestHttpPost.aspx", DateTime.Now.AddSeconds(120).ToString("yyyyMMddHHmmss"));
            Label1.Text = a.ToString();
        }
    }
}