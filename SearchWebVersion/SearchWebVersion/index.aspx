<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="SearchWebVersion.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="mystyle.css"/>
</head>
<body style="font-family:Verdana;">
    <img src="homebanner.png" style="opacity:0.5;" />
    <center>

        <br /><br /><br /><br />
        <asp:Label ID="userLink" runat="server"></asp:Label>
    
        <form id="form1" runat="server">
    <div>
        <asp:Label ID="HomePageHeader" runat="server" Text="Enter your search term below" Font-Size="Large"></asp:Label><br /><br />
        <div>
            <div style="float:left;margin-left:400px;">
                    <asp:TextBox ID="KeywordInputBox" runat="server" Width="500px" Height="40px"></asp:TextBox>
            </div>
            <div style="float:right;margin-right:370px;">
                <asp:ImageButton ID="search" ImageUrl="images/search.png" Height="50px" Width="50px" runat="server" OnClick="SearchButton_Click" />
            </div>
        </div>
    </div>
            
            

    

    <br /><br /><br /><br /><br /><br /> 
    <asp:Label ID="testLabel" runat="server" />
        <asp:Label ID="loginLabel" runat="server"><a href="login.aspx">Log in </a> | <a href="register.aspx">New User? Register</a></asp:Label>
                    <br /><asp:Button ID="logoutButton" runat="server" onClick="logoutButton_Click" Text="Logout" Visible="false"></asp:Button>

        <br /> 
        <asp:Label ID="add_Link" runat="server" /> 

        <br /><br /><br /><br />
        <asp:Label ID="DateTimeLabelTwo" runat="server" /></form>
    </center>
</body>
</html>
