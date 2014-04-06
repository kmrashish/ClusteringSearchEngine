<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Results.aspx.cs" Inherits="SearchWebVersion.Results" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Search Results :)</title>
    <style>
        p {
            font-family: Roboto;
            font-size: 40px;
        }
        a {
            color: skyblue;
        }
    </style>
</head>
<body style="background-color:azure;font-family :Roboto;font-size:20px; color:white;" background="images/wall1.jpg">
    <a href="index.aspx">Home</a>
    <div><div style="float:left;"><h1><p>Welcome to the results page!!</p></h1></div><div style="float:right"><asp:Label ID="DateTimeLabel" runat="server" /></div></div><br /> <br /> <br /><br />
    
    <div style="float:left">
    <form id="form1" runat="server">
    <div>
       <asp:Label ID="ConnectionStatusLabel" runat="server" Font-Size="15px"></asp:Label><br /><br />
       Search Results:
       <asp:Label ID="ActualResults" runat="server" />
    </div>
    </form>
        </div>
</body>
</html>
