<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" ValidateRequest="false"
    CodeBehind="setting.aspx.cs" Inherits="CMS.Manage.setting" %>

<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <title>تنظیمات</title>
    <script src="../component/Editor/nicEdit.js" type="text/javascript"></script>
    <script type="text/javascript">
        bkLib.onDomLoaded(function () { 
            nicEditors.editors.push(
            new nicEditor({ maxHeight: 310 }).panelInstance(document.getElementById('<%=txtBulletin.ClientID %>'))
            ); 
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box">
        <div class="header">
            <h4>تنظیمات سایت</h4>
        </div>
        <div class="content">
            <div class="cont tabs padding-reset settingtab">
                <ul>
                    <li><a href="#tabs-2">تنظیمات اصلی</a></li>
                    <li class="hidden"><a href="#tabs-1">درگاه</a></li>
                    <li>
                        <a href="#tabs-3">تنظیمات ایمیل</a></li>
                    <li>
                        <a href="#tabs-5">تنظیمات اسمس</a></li>

                    <%--<li><a href="#tabs-4">متون سایت</a></li>--%>
                </ul>
                <div id="tabs-1">
                    <div class="content">
                        <fieldset>
                            <div class="rowelement">
                                <div class="span2 right">
                                    <label>
                                        مرچنت زرین پال
                                    </label>
                                </div>
                                <div class="span6 right">
                                    <asp:TextBox ID="txtZarin" runat="server" CssClass=" ltr"></asp:TextBox>

                                </div>
                                <div class="clear">
                                </div>
                            </div>


                            <div class="rowelement">
                                <div class="span2 right">
                                    <label>
                                        اطلاعات درگاه ملت
                                    </label>
                                </div>
                                <div class="span6 right">
                                    <asp:TextBox ID="txtTerminalId" Width="100" runat="server" CssClass=" ltr" placeholder="TerminalId"></asp:TextBox>

                                    <asp:TextBox ID="txtUserName" Width="100" runat="server" CssClass=" ltr" placeholder="UserName"></asp:TextBox>

                                    <asp:TextBox ID="txtUserPassword" Width="100" runat="server" CssClass=" ltr" placeholder="UserPassword"></asp:TextBox>

                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="separator">
                            </div>



                            <div class="rowelement">
                                <div class="span2 right">
                                </div>
                                <div class="span6 right">
                                    <asp:Button ID="Button3" runat="server" Text="ذخـــیره" CssClass="btn btn-bravo"
                                        OnClick="Button3_Click" />
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
                <div id="tabs-2">
                    <div class="content">
                        <fieldset>
                            <div class="rowelement withpadding">
                                <cc1:MessageBox ID="MessageBox1" runat="server" MessageType="Error" Visible="false">
                                </cc1:MessageBox>
                            </div>
                            <div class="rowelement">
                                <div class="span2 right">
                                    <label>
                                        عنوان سایت
                                         
                                    </label>
                                </div>
                                <div class="span6 right">
                                    <asp:TextBox ID="txtTitle" runat="server" MaxLength="100" CssClass="req"></asp:TextBox>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="separator">
                            </div>

                            <div class="rowelement">
                                <div class="span2 right">
                                    <label>
                                        توضیحات گوگل</label>
                                </div>
                                <div class="span6 right">
                                    <asp:TextBox ID="txtDesc" runat="server" MaxLength="170"></asp:TextBox>
                                    <p class="help" data-tip="توضیحاتی که برای موتورهای جستجو از اهمیت بالایی برخوردار است. حداکثر 150 حرف.">
                                    </p>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="separator">
                            </div>





                            <div class="rowelement">
                                <div class="span2 right">
                                    <label>
                                        عنوان در اسلایدر
                                         
                                    </label>
                                </div>
                                <div class="span6 right">
                                    <asp:TextBox ID="txtslidertitle" runat="server" MaxLength="100"></asp:TextBox>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="separator">
                            </div>
                            
                                <div class="rowelement">
                                    <div class="span2 right">
                                        <label>
                                            توضیحات زیر سرچ</label>
                                    </div>
                                    <div class="span6 right">
                                        <asp:TextBox ID="txtslidertext" runat="server" MaxLength="500"></asp:TextBox>

                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                                <div class="separator">
                                </div>

                                <div class="rowelement">
                                    <div class="span2 right">
                                        <label>
                                            لینک عکس پسزمینه
                                        </label>
                                    </div>
                                    <div class="span6 right">
                                        <asp:TextBox ID="txtSliderPic" runat="server" MaxLength="400" CssClass="req ltr"></asp:TextBox>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                                <div class="separator">
                                </div>

                            <div runat="server" visible="false">
                                
                            <div class="rowelement">
                                <div class="span2 right">
                                    <label>
                                        توضیحات در اسلایدر</label>
                                </div>
                                <div class="span6 right">
                                    <asp:TextBox ID="txtslidersubtitle" runat="server" MaxLength="170"></asp:TextBox>

                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="separator">
                            </div>

                                 
                            </div>

                            <div class="rowelement">
                                <div class="span2 right">
                                    <label>
                                        ایمیل مدیر سایت
                                    </label>
                                </div>
                                <div class="span6 right">
                                    <asp:TextBox ID="txtManageMail" runat="server" MaxLength="300" CssClass="req ltr"></asp:TextBox>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="separator">
                            </div>

                            <div class="rowelement">
                                <div class="span2 right">
                                </div>
                                <div class="span6 right">
                                    <asp:Button ID="Button1" runat="server" Text="ذخـــیره" CssClass="btn btn-success"
                                        OnClick="Button1_Click" />
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
                <div id="tabs-3">
                    <div class="content">
                        <fieldset>
                            <div class="rowelement withpadding">
                                <cc1:MessageBox ID="MessageBox2" runat="server" MessageType="Error" Visible="false">
                                </cc1:MessageBox>
                            </div>
                            <div class="rowelement">
                                <div class="span2 right">
                                    <label>
                                        نام سایت در ایمیل
                                    </label>
                                </div>
                                <div class="span6 right">
                                    <asp:TextBox ID="txtName" runat="server" MaxLength="100" CssClass="req"></asp:TextBox>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="separator">
                            </div>
                            <div class="rowelement">
                                <div class="span2 right">
                                    <label>
                                        سرویس دهنده ایمیل
                                    </label>
                                </div>
                                <div class="span6 right">
                                    <asp:TextBox ID="txtMailservice" runat="server" MaxLength="100" CssClass="req ltr" Width="200" placeholder="mail.yoursite.com"></asp:TextBox>
                                    &nbsp;&nbsp;&nbsp;
                                    پورت&nbsp;&nbsp;
                                    <asp:TextBox ID="txtMailserviceport" runat="server" MaxLength="5" CssClass="req ltr" placeholder="110" Width="29px"></asp:TextBox>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="separator">
                            </div>
                            <div class="rowelement">
                                <div class="span2 right">
                                    <label>
                                        آدرس ایمیل
                                    </label>
                                </div>
                                <div class="span6 right">
                                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="100" CssClass="req ltr"></asp:TextBox>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="separator">
                            </div>
                            <div class="rowelement">
                                <div class="span2 right">
                                    <label>
                                        رمز ایمیل</label>
                                </div>
                                <div class="span6 right">
                                    <asp:TextBox ID="txtMailPass" CssClass="req ltr" runat="server" MaxLength="300"></asp:TextBox>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="separator">
                            </div>
                            <div class="rowelement">
                                <div class="span2 right">
                                </div>
                                <div class="span6 right">
                                    <asp:Button ID="Button2" runat="server" Text="ذخـــیره" CssClass="btn btn-danger"
                                        OnClick="Button2_Click" />
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>


                <div id="tabs-5">
                    <div class="content">
                        <fieldset>
                            <div class="rowelement withpadding">
                                <cc1:MessageBox ID="MessageBox4" runat="server" MessageType="Error" Visible="false">
                                </cc1:MessageBox>
                            </div>
                            <div class="rowelement">
                                <div class="span2 right">
                                    <label>
                                        امضای دیجیتال
                                    </label>
                                </div>
                                <div class="span6 right">
                                    <asp:TextBox ID="txtsign" runat="server" MaxLength="100" CssClass="req ltr"></asp:TextBox>
                                    <p class="help" data-tip="برای عدم ارسال اسمس، خالی بگذارید.">
                                    </p>

                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="separator">
                            </div>
                            <div class="rowelement">
                                <div class="span2 right">
                                    <label>
                                        شماره
                                    </label>
                                </div>
                                <div class="span6 right">
                                    <asp:TextBox ID="txtsmsnumber" runat="server" MaxLength="100" CssClass="req ltr"></asp:TextBox>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="separator">
                            </div>
                            <div class="rowelement">
                                <div class="span2 right">
                                    <label>
                                        پیام ارسالی برای خریدار</label>
                                </div>
                                <div class="span6 right">
                                    <asp:TextBox ID="txtsmstext" TextMode="MultiLine" CssClass="req" runat="server" MaxLength="300"></asp:TextBox>

                                    <p class="help" data-tip="[code] برای رسید خرید استفاده کنید.">
                                    </p>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="separator">
                            </div>
                            <div class="rowelement">
                                <div class="span2 right">
                                    <label>
                                        شماره مدیر
                                    </label>
                                </div>
                                <div class="span6 right">
                                    <asp:TextBox ID="txtsmsManagermob" runat="server" MaxLength="100" CssClass="req ltr"></asp:TextBox>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="separator">
                            </div>

                            <div class="rowelement">
                                <div class="span2 right">
                                </div>
                                <div class="span6 right">
                                    <asp:Button ID="Button5" runat="server" Text="ذخـــیره" CssClass="btn btn-danger"
                                        OnClick="Button5_Click" />
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
                <div id="tabs-4" class="hidden">
                    <div class="content">
                        <fieldset>
                            <div class="rowelement withpadding">
                                <cc1:MessageBox ID="MessageBox3" runat="server" MessageType="Error" Visible="false">
                                </cc1:MessageBox>
                            </div>



                            <div class="rowelement">
                                <div class="span2 right">
                                    <label>
                                        محتوای هدر صفحه اول
                                    </label>
                                </div>
                                <div class="span6 right">
                                    <asp:TextBox ID="txtFistPageHeader" runat="server" Width="450px" TextMode="MultiLine" CssClass=" ltr"></asp:TextBox>
                                    <p class="help" data-tip="کدهایی که میخواهید فقط در هدر صفحه اول باشند.">
                                    </p>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="separator">
                            </div>



                            <div class="rowelement">
                                <div class="span2 right">
                                    <label>
                                        محتوای هدر همه صفحات
                                    </label>
                                </div>
                                <div class="span6 right">
                                    <asp:TextBox ID="txtHeadContent" runat="server" Width="450px" TextMode="MultiLine" CssClass=" ltr"></asp:TextBox>
                                    <p class="help" data-tip="کدهایی که در Head سایت و قبل از تگ body قرار می گیرند. مانند کد آنالیز گوگل و...">
                                    </p>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="separator">
                            </div>



                            <div class="rowelement">
                                <div class="span2 right">
                                    <label>
                                        سایدبار چپ
                                    </label>
                                </div>
                                <div class="span6 right">
                                    <asp:TextBox ID="txtsideBar" runat="server" Width="450px" TextMode="MultiLine" CssClass=" ltr"></asp:TextBox>

                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="separator">
                            </div>


                            <div class="rowelement">
                                <div class="span2 right">
                                    <label>
                                        خوش آمد صفحه اول
                                    </label>
                                </div>
                                <div class="span6 right">
                                    <asp:TextBox ID="txtFirstPageWelcome" runat="server" Width="450px" TextMode="MultiLine" CssClass=" ltr"></asp:TextBox>
                                    <p class="help" data-tip="قسمتی که در صفحه اول، اگه کاربر لوگین نباشه نشون داده میشه.">
                                    </p>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="separator">
                            </div>

                            <div class="rowelement">
                                <div class="span2 right">
                                    <label>
                                        محتوای فوتر
                                    </label>
                                </div>
                                <div class="span6 right">
                                    <asp:TextBox ID="txtFooter" runat="server" Width="450px" TextMode="MultiLine" CssClass=" ltr"></asp:TextBox>
                                    <p class="help" data-tip="در پایین سایت نمایش داده می شود. کد html وارد کنید">
                                    </p>
                                    اطلاعات فوتر هر 1 ساعت 1 بار لود می شود.
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="separator">
                            </div>
                            <div class="rowelement">
                                <div class="span2 right">
                                    <label>
                                        اطلاعات تماس فوتر
                                    </label>
                                </div>
                                <div class="span6 right">
                                    <asp:TextBox ID="txtFooterContact" runat="server" Width="450px" TextMode="MultiLine" CssClass=" ltr"></asp:TextBox>

                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="separator">
                            </div>


                            <%-- <div class="rowelement">
                                <div class="span2 right">
                                    <label>
                                        تماس با ما</label>
                                </div>
                                <div class="span6 right">
                                    <asp:TextBox ID="txtContactUs" TextMode="MultiLine" runat="server"  Width="470px" Height="300"></asp:TextBox>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="separator">
                            </div>--%>
                            <div class="rowelement hidden">
                                <div class="span2 right">
                                    <label>
                                        اطلاعیه باشگاه مشتریان</label>
                                </div>
                                <div class="span6 right">
                                    <asp:TextBox ID="txtBulletin" runat="server" TextMode="MultiLine" Width="470px" Height="300"></asp:TextBox>

                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="rowelement">
                                <div class="span2 right">
                                </div>
                                <div class="span6 right">
                                    <asp:Button ID="Button4" runat="server" Text="ذخـــیره" CssClass="btn btn-golf  "
                                        OnClick="Button4_Click" />
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>




            </div>
        </div>
    </div>
    <script src="js/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script>

        $(".tabs").tabs();
		
    </script>
</asp:Content>
