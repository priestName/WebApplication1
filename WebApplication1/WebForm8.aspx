﻿<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <title></title>
  <style type="text/css">
    html, body {
      margin: 0px;
      padding: 0px;
      width: 100%;
      height: 100%;
      background-color: #f8f7f7;
      font-family: arial,sans-serif;
    }
  
    #layouttable {
      margin:0px;
      padding:0px;
      width:100%;
      height:100%;
      border:2px solid green;
      border-collapse:collapse;
      min-width:800px;
    }
  
      #layouttable td {
        border: 1px solid green;
      }
  
    .h100p {
      height:100%;
    }
  
    .midtr{height:auto;}
      .midtr tr td {
        height: 100%;
      }
  
    #chatmsgbox, #chatonlinebox {
      background-color:white;
      overflow-x: hidden;
      overflow-y: auto;
      overflow-wrap: break-word;
      height: 100%;
    }
  
    #chatonlinebox {
      background-color:#f5d0a8;
    }
  
    .rc, .sd {
      overflow:hidden;
    }
  
     .rc p {
      float: left;
      color: green;
    }
      .sd p {
        float: right;
        color: orange;
      }
  </style>
  
</head>
<body>
  <table id="layouttable">
    <colgroup>
      <col style="width:auto" />
      <col style="width: 200px;" />
    </colgroup>
    <tr style="height:30px; background-color:lightblue;color:yellow;">
      <td>
        欢迎进入梦在旅途的网页即时在线大众聊天室 - www.zuowenjun.cn：
      </td>
      <td>
        当前在线人员
      </td>
    </tr>
    <tr style="height:auto;" id="midtr">
      <td>
        <div id="chatmsgbox">
        </div>
      </td>
      <td>
        <div id="chatonlinebox">
          <ul id="chatnames"></ul>
        </div>
      </td>
    </tr>
    <tr style="height:50px;">
      <td colspan="2">
        <label for="name">聊天妮称：</label>
        <input type="text" id="name" style="width:80px;" />
        <input type="button" id="btnsavename" value="确认进入" />
        <label for="msg">输入内容：</label>
        <input type="text" id="msg" style="width:400px;" />
        <input type="button" id="btnSend" value="发送消息" disabled="disabled" />
      </td>
    </tr>
  </table>
  <script type="text/javascript">
    var chatName = null;
    var oChatmsgbox, oMsg, oChatnames;
    var ajaxforSend, ajaxforRecv;
  
    //页面加载初始化
    window.onload = function () {
      document.getElementById("btnsavename").onclick = function () {
        this.disabled = true;
        var oName = document.getElementById("name");
        oName.readOnly = true;
        document.getElementById("btnSend").disabled = false;
        //receiveMsg();
        setChatStatus(oName.value,"on");
      }
  
      document.getElementById("btnSend").onclick = function () {
        sendMsg(oMsg.value);
      };
  
      //init
      oChatmsgbox = document.getElementById("chatmsgbox");
      oMsg = document.getElementById("msg");
      oChatnames = document.getElementById("chatnames");
      ajaxforSend = getAjaxObject();
      ajaxforRecv = getAjaxObject();
    }
  
    //离开时提醒
    window.onbeforeunload = function () {
      event.returnValue = "您确定要退出聊天室吗？";
    }
  
    //关闭时离线
    window.onunload = function () {
      setChatStatus(chatName, "off");
    }
  
    //设置聊天状态：在线 OR 离线
    function setChatStatus(name, status) {
      callAjax(getAjaxObject(), "action=" + status + "&name=" + name, function (rs) {
        if (!rs.success) {
          alert(rs.info);
          return;
        }
        if (status == "on") {
          chatName = document.getElementById("name").value;
          setTimeout("receiveMsg()",500);
        }
        loadOnlineChatNames();
      });
    }
  
    //加载在线人员名称列表
    function loadOnlineChatNames(){
      callAjax(getAjaxObject(), "action=onlines", function (rs) {
        var lis = "";
        for(var i=0;i<rs.length;i++)
        {
          lis += "<li>"+ rs[i] +"</li>";
        }
        oChatnames.innerHTML = lis;
      });
    }
  
    //接收消息列表
    function receiveMsg() {
      callAjax(ajaxforRecv, "action=receive&name=" + chatName, function (rs) {
        if (rs.success) {
          showChatMsgs(rs.msgs, "rc");
        }
        setTimeout("receiveMsg()", 500);
      });
    }
    //发送消息
    function sendMsg(msg) {
      callAjax(ajaxforSend, "action=send&name=" + chatName + "&msg=" + escape(msg), function (rs) {
        if (rs.success) {
          showChatMsgs(rs.msgs, "sd");
          oMsg.value = null;
          //alert("发送成功！");
        }
      });
    }
  
    //显示消息
    function showChatMsgs(msgs, cssClass) {
      var loadonline = false;
      for (var i = 0; i < msgs.length; i++) {
        var msg = msgs[i];
        oChatmsgbox.innerHTML += "<div class='" + cssClass + "'><p>[" + msg.name + "] - " + msg.sendtime + " 说:<br/>" + msg.content + "</p></div>";
        if (msg.type == "on" || msg.type == "off")
        {
          loadonline = true;
        }
      }
      if (loadonline)
      {
        loadOnlineChatNames();
      }
    }
  
    //调用AJAX
    function callAjax(ajax, param, callback) {
  
      ajax.open("post", "ChatHandler.ashx", true);
      ajax.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
      ajax.onreadystatechange = function () {
        if (ajax.readyState == 4 && ajax.status == 200) {
          var json = eval("(" + ajax.responseText + ")");
          callback(json);
        }
      };
      ajax.send(param);
    }
  
    //获取AJAX对象（XMLHttpRequest）
    function getAjaxObject() {
      var xmlhttp;
      if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp = new XMLHttpRequest();
      }
      else {// code for IE6, IE5
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
      }
      return xmlhttp;
    }
  
  </script>
</body>
</html>

