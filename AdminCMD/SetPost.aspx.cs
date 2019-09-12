using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminCMD
{
    public partial class SetPost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.RequestType == "POST")
            {
                StreamReader reader = new StreamReader(Request.InputStream);
                String xmlData = reader.ReadToEnd();
                try
                {
                    JObject aa = JObject.Parse(xmlData);
                    if (xmlData.IndexOf("Value") >= 0)
                    {
                        //aa["Key"];
                        //aa["Value"];
                        Response.Write(DateTime.Now.ToString());
                    }
                    else {

                    }
                }
                catch (Exception)
                {

                }
                
                Response.End();
            }
        }
    }
}