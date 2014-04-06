<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="SearchWebVersion.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        a {
            color: skyblue;
        }
    </style>
</head>
<body background="images/wall1.jpg" style="background-color:azure;font-family:Verdana;color:white;">
    <center><h1>Search Engine Homepage</h1>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="HomePageHeader" runat="server" Text="Enter the keyword below"></asp:Label><br /><br />
        <asp:TextBox ID="KeywordInputBox" runat="server"></asp:TextBox><br /><br />
        <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="SearchButton_Click" />
    </div>
    </form></center>

    <br /><br /><br /><br /><br /><br /> <center><a href="login.aspx">Log in </a> | <a href="register.aspx">New User? Register</a>
        <br /> <b><a href="scraper.aspx" target="_new"> Add data to our index </a></b>
                                         </center>
</body>
</html>
