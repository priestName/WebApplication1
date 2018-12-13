﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Class6
    {
        public void saa()
        {
            unit_utterance();


        }
        public static string unit_utterance()
        {
            //24.14dee8b928c79aaa22c7dab96637b9f4.2592000.1547275788.282335-15147577
            //JObject job = JObject.Parse(getAccessToken());
            //string token = job["access_token"].ToString();
            string token = "24.14dee8b928c79aaa22c7dab96637b9f4.2592000.1547275788.282335-15147577";
            string host = "https://unit.gz.baidubce.com/rpc/2.0/unit/bot/chat?access_token=" + token;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
            request.Method = "post";
            request.ContentType = "application/json";
            request.KeepAlive = true;
            //string str = "{\"log_id\":\"UNITTEST_20181213\",\"version\":\"2.0\",\"service_id\":\"S10550\",\"session_id\":\"\",\"bot_session\":\"\",\"request\":{\"query\":\"你好\",\"user_id\":\"UNIT_WEB_d704bbbc51c74dd3a561e5529036f30f\",\"bernard_level\":\"0\",\"client_session\":{\"client_results\":\"\",\"candidate_options\":[]}},\"dialog_state\":{\"contexts\":{\"SYS_REMEMBERED_SKILLS\":[\"21441\",\"21443\"]}}}"; // json格式
            string str= "{\"bot_session\":\"\",\"log_id\":\"7758521\",\"request\":{\"bernard_level\":0,\"client_session\":\"{\\\"client_results\\\":\\\"\\\", \\\"candidate_options\\\":[]}\",\"query\":\"你好\",\"query_info\":{\"asr_candidates\":[],\"source\":\"KEYBOARD\",\"type\":\"TEXT\"},\"updates\":\"\",\"user_id\":\"88888\"},\"bot_id\":\"21441\",\"version\":\"2.0\"}"; // json格式 
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string result = reader.ReadToEnd();
            JObject JobResult = JObject.Parse(result);
            var Result_list= JobResult["result"]["response"]["action_list"].Select(s=>s["say"].ToString()).ToList();
            
            Console.WriteLine("对话接口返回:");
            Console.WriteLine(Result_list[1]);
            return result;
        }

        
        public static String getAccessToken()
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
            return result;
        }

    }
}
