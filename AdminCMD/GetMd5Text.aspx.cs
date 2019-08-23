using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminCMD
{
    public partial class GetMd5Text : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string Value = ValueText.Value;
            var ctx1 = new KeyValue();
            var query = from md5test in ctx1.Md5Test
                     where md5test.Value == Value
                             select md5test;
            if (query.Count() > 0)
            {
                KeyText.Value = query.First().Key;
                ValueText.Value = query.First().Value;
            }
            else {
                KeyText.Value = "暂未收录";
            }
            
        }
    }
}