<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Results.aspx.cs" Inherits="SearchWebVersion.Results" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Search Results :)</title>
   <link rel="stylesheet" type="text/css" href="mystyle.css">
</head>
<body style="background-color:white;font-family :Roboto;font-size:20px;;" >
    <a href="index.aspx"><img src="homebanner.png" style="opacity:0.3;width:15%;height:15%;" /></a>
    
    <center><br /><br />
    <div><div><h1><p>Search Results for "<u><asp:Label ID="keywordHeaderLabel" runat="server" /></u>"</p></h1></div><div >
        <asp:Label ID="DateTimeLabel" runat="server" /></div></div><br /> <br /> 
    
    <div>
    <form id="form1" runat="server">
    <div>
       
       Search Results:
       <asp:Label ID="ActualResults" runat="server"/>
    </div>
    </form>
        </div>
        <asp:Label ID="refined_results" runat="server" />

    </center>
</body>
</html>
