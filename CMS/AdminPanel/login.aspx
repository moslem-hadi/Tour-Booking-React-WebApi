<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="CMS.Manage.login" %>
<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<!DOCTYPE HTML>
<html lang="en">
<head>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8"> 
    <title>ورود به مدیریت سایت</title>
    
    <link href="css/bootstrap.css" rel="stylesheet" media="screen">
    <link href="css/style.css" rel="stylesheet" media="screen">
    <!--[if lt IE 9]><link href="css/ie-hacks.css" rel="stylesheet" media="screen"><![endif]-->
    <link rel='shortcut icon' type='image/x-icon' href='images/favicon.ico' />
    <script src="js/jquery-1.7.1.min.js" type="text/javascript"></script> 
    <script >
        $(document).ready(function () {
            $('p.forgotpassp').click(function () {
                //$('div#Tbody11').slideUp('slow'); 
                $(this).nextAll('div.forgotpassdiv').slideToggle('fast');
            })
        });

    </script>
</head>
<body>
    <form runat="server" id="form1">
    <div id="main">
        <div class="container-fluid">
            <div class="row-fluid">
                <div id="main-content" class="login-content">
                    <div id="login_logo"></div>
                    <div class="form-signin" >
                          <cc1:MessageBox ID="MessageBox1" runat="server" MessageType="Error" Visible="false">
                          </cc1:MessageBox>
                        <div class="input-prepend"> 
                            <asp:TextBox ID="txtMail" class="ltr"  placeholder="ایمیل" runat="server"></asp:TextBox><span class="add-on"><span class="icon-user"></span></span></div>
                      
                        <div class="input-prepend"> 
                            <asp:TextBox ID="txtPass" class="ltr" TextMode="Password" placeholder="رمز ورود" runat="server"></asp:TextBox><span class="add-on"><span class="icon-lock"></span></span>

                        </div>
                        
                        
                        <div class="input-prepend"> 
                            
     <asp:TextBox ID="txtValidate" MaxLength="5"  CssClass="CapchaInput"
                            runat="server"></asp:TextBox><cc1:CaptchaControl ID="ccJoin" CssClass="captchalogin" runat="server" CaptchaBackgroundNoise="low"
                            CaptchaLength="5" CaptchaHeight="32" CaptchaWidth="110" CaptchaLineNoise="None"
                            CaptchaMinTimeout="5" CaptchaMaxTimeout="240" FontColor="#000000" BackColor="#eeeeee" />
                            </div>

                        <asp:CheckBox ID="CheckBox1" runat="server" Checked="true"  CssClass="checkbox inline pull-right" Text="مرا بخاطر بسپار" />

                         
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-info pull-left" Text="ورود" OnClick="Button1_Click"/>
                        
                        <div class="clear"></div>
                         
                        <div class="clear"></div>
                    </div>
                </div>
            </div>
        </div>
    </div> 
        </form>
</body>
</html>
