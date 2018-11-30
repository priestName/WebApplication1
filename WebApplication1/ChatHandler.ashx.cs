using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Threading;
using System.Collections.Concurrent;

public class ChatHandler : IHttpHandler
{
    private class Msg
    {
        public string name { get; set; }
        public string sendtime { get; set; }
        public string content { get; set; }
        public string readednams { get; set; }
        public int readedCount { get; set; }
        public string type { get; set; }
    }

    private static List<Msg> msgs = new List<Msg>();
    private static ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();
    private static object syncObject = new object(), syncObject1 = new object();
    private static List<string> onLineNames = new List<string>();
    public void ProcessRequest(HttpContext context)
    {
        string chatName = context.Request.Form["name"];
        string msg = context.Request.Form["msg"];
        string actionName = context.Request.Form["action"];
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

        object responseObject = null;

        switch (actionName)
        {
            case "receive":
                {
                    responseObject = GetNewMessages(chatName);
                    break;
                }
            case "send":
                {
                    responseObject = SendMessage(chatName, msg, "normal");
                    break;
                }
            case "on":
            case "off":
                {
                    responseObject = SetChatStatus(chatName, actionName);
                    break;
                }
            case "onlines":
                {
                    responseObject = onLineNames;
                    break;
                }
        }

        context.Response.ContentType = "text/json";
        context.Response.Write(jsSerializer.Serialize(responseObject));

    }

    private object SetChatStatus(string chatName, string status)
    {
        if (status == "on")
        {
            if (onLineNames.Exists(s => s == chatName))
            {
                return new { success = false, info = "该聊天妮称已经存在，请更换一个名称吧！" };
            }
            lock (syncObject1)
            {
                onLineNames.Add(chatName);
            }
            SendMessage(chatName, "大家好，我进入聊天室了！", status);
            return new { success = true, info = string.Empty };
        }
        else
        {
            lock (syncObject1)
            {
                onLineNames.Remove(chatName);
            }
            SendMessage(chatName, "再见，我离开聊天室了！", status);
            return new { success = true, info = string.Empty };
        }
    }

    /// <summary>
    /// 获取未读的新消息
    /// </summary>
    /// <param name="chatName"></param>
    /// <returns></returns>
    private object GetNewMessages(string chatName)
    {
        //第一种：循环处理
        while (true)
        {

            var newMsgs = msgs.Where(m => m.name != chatName && !(m.readednams ?? "").Contains(chatName)).OrderBy(m => m.sendtime).ToList();
            if (newMsgs != null && newMsgs.Count() > 0)
            {
                lock (syncObject)
                {
                    newMsgs.ForEach((m) =>
                    {
                        m.readednams += chatName + ",";
                        m.readedCount++;
                    });
                    int chatNameCount = onLineNames.Count();
                    msgs.RemoveAll(m => m.readedCount >= chatNameCount);
                }

                return new { success = true, msgs = newMsgs };
            }

            Thread.Sleep(1000);
        }


        //第二种方法，采用自旋锁
        //List<Msg> newMsgs = null;
        //SpinWait.SpinUntil(() =>
        //{
        //  newMsgs = msgs.Where(m => m.name != chatName && !(m.readednams ?? "").Contains(chatName)).OrderBy(m => m.sendtime).ToList();
        //  return newMsgs.Count() > 0;
        //}, -1);

        //rwLock.EnterWriteLock();
        //newMsgs.ForEach(m =>
        //{
        //  m.readednams += chatName + ",";
        //  m.readedCount++;
        //});
        //rwLock.ExitWriteLock();
        //return new { success = true, msgs = newMsgs };
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="chatName"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    private object SendMessage(string chatName, string msg, string type)
    {
        var newMsg = new Msg() { name = chatName, sendtime = DateTime.Now.ToString("yyyy/MM/dd HH:mm"), content = HttpContext.Current.Server.HtmlEncode(msg), readednams = null, type = type };
        //rwLock.EnterWriteLock();
        lock (syncObject)
        {
            msgs.Add(newMsg);
        }
        //rwLock.ExitWriteLock();
        return new { success = true, msgs = new[] { newMsg } };
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
