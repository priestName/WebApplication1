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
            IsSys.Value = "";
            var ismobile = System.Web.HttpContext.Current.Request.Browser.IsMobileDevice;
            var device = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"].ToString().ToLower();
            //mozilla/5.0 (iphone; cpu iphone os 9_1 like mac os x) applewebkit/601.1.46 (khtml, like gecko) version/9.0 mobile/13b143 safari/601.1
        }
    }
}