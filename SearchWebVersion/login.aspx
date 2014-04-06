<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="SearchWebVersion.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body {
            color: white;
            font-family: Roboto;
        }
        a {
            color: skyblue;
            font-style:italic;
        }
    </style>
</head>
<body background="images/wall1.jpg">
    <a href="index.aspx">Home</a>
    <center><h1>Existing Users login here </h1><br />
    <h3>Enter your details below: </h3><br />
    <form id="form1" runat="server">
    <div>
        Enter username: 
        <asp:TextBox ID="userloginTextBox" runat="server"></asp:TextBox>
        <br /><br />
        Enter password: 
        <asp:TextBox ID="pwdloginTextBox" runat="server"></asp:TextBox><br /> <br />
        <asp:Button ID="loginButton" runat="server" onClick="loginAction" Text="Login"/>
    </div>
    </form></center>
</body>
</html>
