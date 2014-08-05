<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="SearchWebVersion.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="mystyle.css">
</head>
<body>
    <a href="index.aspx"><img src="homebanner.png" style="opacity:0.3;width:15%;height:15%;" /></a>

    <center><h1> New Users Register Here </h1>
    
    <form id="form1" runat="server">
    <div>
    <div><div style="float:left;margin-left:550px;">Name: </div><div style="float:right;margin-right:550px;"><asp:TextBox ID="nameInputBox" runat="server" /></div><br /><br /><br /></div>
     <div><div style="float:left;margin-left:550px;">Email: </div><div style="float:right;margin-right:550px;"><asp:TextBox ID="mailInputBox" runat="server" TextMode="Email"/></div><br /><br /><br /></div>
    <div><div style="float:left;margin-left:500px;">Date of Birth: </div><div style="float:right;margin-right:550px;"><asp:TextBox ID="dobInputBox" runat="server" /></div><br /><br /><br /></div>
    <div><div style="float:left;margin-left:550px;">Country: </div><div style="float:right;margin-right:550px;"><asp:TextBox ID="countryInputBox" runat="server"/></div> <br /><br /><br /></div>
        <div><div style="float:left;margin-left:485px;">Desired Username: </div><div style="float:right;margin-right:550px;"><asp:TextBox ID="UsernameInputBox" runat="server" /></div><br /><br /><br /></div>
        <div><div style="float:left;margin-left:550px;">Password: </div><div style="float:right;margin-right:550px;"><asp:TextBox ID="pwdInputBox" runat="server" TextMode="Password"/></div><br /><br /><br /></div>
        <asp:ImageButton ID="registerSubmitButton" ImageUrl="images/submit.png" Height="50px" Width="100px" runat="server" OnClick="registerSubmitButton_Click" /> <br />
        <asp:Label ID="testlabel" runat="server" />
    </div>
    </form></center>

    
</body>
</html>
