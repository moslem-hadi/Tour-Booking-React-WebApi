<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="adduser.aspx.cs" Inherits="CMS.Manage.adduser" %>

<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<title>افزودن کاربر</title>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box">
        <div class="header">
            <h4>
                ایجاد کاربر جدید</h4>
        </div>
        <div class="content">
            
            <fieldset>
             
                <div class="rowelement withpadding">
                <cc1:MessageBox ID="MessageBox1" runat="server" MessageType="Error" Visible="false">
            </cc1:MessageBox>
                </div>
                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            نام و نام خانوادگی
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:TextBox ID="txtName" runat="server" MaxLength="100" CssClass="req" Width="200"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div> 


                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            ایمیل
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:TextBox ID="txtMail" runat="server" MaxLength="100" CssClass="req ltr" Width="200"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div> 


                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                           رمز عبور
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:TextBox ID="txtPassword" runat="server" MaxLength="20" CssClass="req ltr" Width="200"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div> 
                 




                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            موبایل
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:TextBox ID="txtMob" runat="server" MaxLength="40" CssClass="ltr" Width="200"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div> 
                 
                 

              
                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            نوع حساب
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:DropDownList runat="server" ID="ddlUserType"  Width="200">
                            <asp:ListItem Value="0" Text="عادی" />
                            <asp:ListItem Value="1" Text="هتل" />
                            <asp:ListItem Value="2" Text="آژانس" />
                            <asp:ListItem Value="3" Text="راننده" />
                        </asp:DropDownList>
                    </div>
                    <div class="clear">
                    </div>
                </div>

                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            نوع کاربری
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:RadioButton ID="rdbuser" runat="server" Text="&nbsp; عادی" GroupName="rdb" Checked="True" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rdbminiadmin" runat="server" Text="&nbsp; مدیر محدود" GroupName="rdb"   />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rdbadmin" runat="server" Text="&nbsp; مدیر" GroupName="rdb" />
                    </div>
                    <div class="clear">
                    </div>
                </div>



                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            وضعیت
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:CheckBox ID="chbActivate" runat="server" Text=" فعال باشد" />
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
                        <a href="userlist.aspx" class="btn btn-warning">انصراف</a>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
