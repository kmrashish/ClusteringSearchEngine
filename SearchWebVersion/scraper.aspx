<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="scraper.aspx.cs" Inherits="SearchWebVersion.scraper" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        a {
            color: skyblue;
            font-style:italic;
        }
    </style>
</head>
<body style="background-color:aliceblue;font-family:Roboto;color:white;" background="images/wall1.jpg">
    <a href="index.aspx">Home</a>
    <center> <h1> Add URL to the search index </h1><br /><h2>Enter URL below</h2><br />
        Note: Please enter the URL without the prefix 'http://www.'
    </center>
    <form id="form1" runat="server">
    <div>
        <center>
           <br />URL: 
        <asp:TextBox ID="InputTextBox" runat="server" /> <br /><br />
        
            Please select a category for the URL: 
            <asp:DropDownList ID="clusterTypeDDL" runat="server">
                <asp:ListItem>Academia</asp:ListItem>
                <asp:ListItem>News/Sports</asp:ListItem>
                <asp:ListItem>Information/Blogs</asp:ListItem>
                <asp:ListItem>Technology</asp:ListItem>
                <asp:ListItem>Social Network</asp:ListItem>
                <asp:ListItem>Entertainment</asp:ListItem>
                <asp:ListItem>Other</asp:ListItem>
            </asp:DropDownList>

            <asp:Button ID="AddUrlButton" runat="server" Text="Add" Onclick="AddUrlButton_Click" /> <br /> <br />
        <asp:Label ID="OutputLabel" runat="server" />
            </center>
    </div>
    </form>
    <center><asp:Label ID="site_count_label" runat="server"></asp:Label></center>
</body>
</html>
