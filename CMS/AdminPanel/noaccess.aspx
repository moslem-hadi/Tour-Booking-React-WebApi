<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="noaccess.aspx.cs" Inherits="CMS.Manage132901951.noaccess" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>دسترسی محدود</title>
    <link href="css/style.css" rel="stylesheet" />
    <style>
        .cont {
            background: #fff;
            width: 661px;
            margin: 0 auto;
    border-radius: 5px;
    border: 1px solid #DADADA;

        }
        
        .cont  img{border-radius:5px;}
        .htitle{
    font: bold 33px "sans";
    text-align: center;
    margin: 30px 0 20px 0;
    color: #EE452E;}
        .goto{
    display: block;
    text-align: center;
    font: bold 21px "sans";
    color: #3F98EA;
    text-decoration: none;
    margin-bottom: 10px;}
        .goto img{vertical-align:middle;margin-left:5px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        
    <div class="cont"> 
        <h4 class="htitle">[دسترسی ممکن نیست]</h4>
        <a href="default.aspx" class="goto">
            <img src="images/icons/dashboard/home.png" /> صفحه اصلی مدیریت</a>
        
    <img src="images/noaccess.png" />
    </div>
    </form>
</body>
</html>
