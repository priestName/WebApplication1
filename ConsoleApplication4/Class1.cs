using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Web;
using System.Linq;

namespace ConsoleApplication4
{
    class Class1
    {
        private static System.Web.Caching.Cache cache = HttpRuntime.Cache;
        public void saa()
        {
            if (cache["accesstoken"] == null)
                getAccessToken();
            Console.WriteLine(unit_utterance());
        }
        // unit对话接口
        public string unit_utterance()
        {
            string token = cache["accesstoken"].ToString();
            string host = "https://aip.baidubce.com/rpc/2.0/unit/service/chat?access_token=" + token;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
            request.Method = "post";
            request.ContentType = "application/json";
            request.KeepAlive = true;
            string str = "{\"log_id\":\"UNITTEST_10000\",\"version\":\"2.0\",\"service_id\":\"S10550\",\"session_id\":\"\",\"request\":{\"query\":\"北京天气\",\"bernard_level\":0,\"user_id\":\"88888\",\"client_session\":\"{\\\"client_results\\\":\\\"\\\", \\\"candidate_options\\\":[]}\"},\"query_info\":{\"asr_candidates\":[],\"source\":\"KEYBOARD\",\"type\":\"TEXT\"},\"updates\":\"\",\"dialog_state\":{\"contexts\":{\"SYS_REMEMBERED_SKILLS\":[\"21441\"]}}}"; // json格式
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string result = reader.ReadToEnd();
            JObject JobResult = JObject.Parse(result);
            try
            {
                var Result_list = JobResult["result"]["response_list"]["action_list"].Select(s => s["say"].ToString()).ToList();
                return Result_list[1];
            }
            catch
            {
                return result.ToString();
            }
        }

        public String getAccessToken()
        {
            // 调用getAccessToken()获取的 access_token建议根据expires_in 时间 设置缓存
            // 百度云中开通对应服务应用的 API Key 建议开通应用的时候多选服务
            String clientId = "XG2oDGFRG6VWCzLXta0qKxEI";
            // 百度云中开通对应服务应用的 Secret Key
            String clientSecret = "E48m5rAKTLlD4ZmE9mGBqIwXi7OiEbZQ";
            String authHost = "https://aip.baidubce.com/oauth/2.0/token";
            HttpClient client = new HttpClient();
            List<KeyValuePair<String, String>> paraList = new List<KeyValuePair<string, string>>();
            paraList.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            paraList.Add(new KeyValuePair<string, string>("client_id", clientId));
            paraList.Add(new KeyValuePair<string, string>("client_secret", clientSecret));

            HttpResponseMessage response = client.PostAsync(authHost, new FormUrlEncodedContent(paraList)).Result;
            String result = response.Content.ReadAsStringAsync().Result;
            JObject job = JObject.Parse(result);
            if (cache["accesstoken"] != null)
                cache.Remove("accesstoken");
            cache.Insert("accesstoken", job["access_token"].ToString());
            return result;
        }
    }
}
