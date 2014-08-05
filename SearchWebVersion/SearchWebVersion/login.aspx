<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="SearchWebVersion.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="mystyle.css">
    </style>
</head>
<body>
    <a href="index.aspx"><img src="homebanner.png" style="opacity:0.3;width:15%;height:15%;" /></a>
    <center><h1>Existing Users login here </h1><br />
    <h3>Enter your details below: </h3><br />
    <form id="form1" runat="server">
    <div>
        Username: 
        <asp:TextBox ID="userloginTextBox" runat="server"></asp:TextBox>
        <br /><br />
        Password: 
        <asp:TextBox ID="pwdloginTextBox" runat="server" TextMode="Password"></asp:TextBox><br /> <br />
        <asp:Button ID="loginButton" runat="server" onClick="loginAction" Text="Login"/>
    </div>
    </form><asp:Label ID="error_status" runat="server" /></center>
</body>
</html>
