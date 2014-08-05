<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="scraper.aspx.cs" Inherits="SearchWebVersion.scraper" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="mystyle.css">
</head>
<body style="background-color:white;font-family:Roboto;">
    <a href="index.aspx"><img src="homebanner.png" style="opacity:0.3;width:15%;height:15%;" /></a>
    <center> <h1> Add URL to the search index </h1><br /><h2>Enter URL below</h2><br />
        Note: Please enter the URL without the prefix 'http://www.'
    </center>
    <form id="form1" runat="server">
    <div>
        <center>
           <br />URL: 
        <asp:TextBox ID="InputTextBox" runat="server" /> <br /><br />
        
           <asp:Button ID="AddUrlButton" runat="server" Text="Add" Onclick="AddUrlButton_Click" /> <br /> <br />
        <asp:Label ID="OutputLabel" runat="server" />
            </center>
    </div>
    </form>
    <center><asp:Label ID="site_count_label" runat="server"></asp:Label></center>
</body>
</html>
