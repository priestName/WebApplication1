<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm10.aspx.cs" Inherits="WebApplication1.WebForm10" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server"> 
    <div>  
        <asp:TextBox ID="TextBox1" runat="server" Height="206px" TextMode="MultiLine" Width="700px" ></asp:TextBox>     
        <hr />  
        <asp:TextBox ID="TextBox2" runat="server" Height="197px" TextMode="MultiLine" Width="398px"></asp:TextBox>  
        <asp:TextBox ID="TextBox3" runat="server" Height="197px" TextMode="MultiLine"></asp:TextBox>  
        <asp:Button ID="Button1" runat="server" Height="197px" OnClick="Button1_Click" Text="SEND" Width="138px" />
    </div>
    </form>
</body>
</html>
