﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="editcontent.aspx.cs" Inherits="CMS.Manage.editcontent" ValidateRequest="false" %>

<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<title>ویرایش خبر</title>
    <script src="../component/Editor/nicEdit.js" type="text/javascript"></script>
    <script type="text/javascript">
        bkLib.onDomLoaded(function () {
            nicEditors.editors.push(
        new nicEditor({ maxHeight: 300 }).panelInstance(
            document.getElementById('<%=txtText.ClientID %>')
        )
    );
        }); 
    </script>
    
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box">
        <div class="header">
            <h4>
               ویرایش خبر</h4>
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
                            عنوان خبر
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:TextBox ID="txtTitle" runat="server" MaxLength="100"  onkeyup="setshort(this.value)" CssClass="req"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>
                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            آدرس یکتای خبر</label>
                            
                    </div>
                    <div class="span6 right">
                        <asp:TextBox ID="txtShort" runat="server" MaxLength="200" Width="200" CssClass="ltr req" ClientIDMode="Static" onkeyup="nospaces(this)"></asp:TextBox>
                        <p class="help" data-tip="متنی که برای نمایش در آدرس خبر به کار می رود. یکتا نیست. به جای فاصله از - استفاده کنید.">
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
                            متن خبر</label>
                    </div>
                    <div class="span6 right">
                        <div class="editor">
                            <textarea id="txtText" runat="server" class="niceditor"  cols="20" rows="2"></textarea>
                        </div>
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
                        <asp:TextBox ID="txtDesc" runat="server" MaxLength="300"></asp:TextBox>
                        <p class="help" data-tip="توضیحاتی که برای موتورهای جستجو از اهمیت بالایی برخوردار است. حداکثر 150 حرف.">
                        </p>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>

                
                <div class="rowelement" runat="server" visible="false">
                    <div class="span2 right">
                        <label>
                            تصویر</label>
                    </div>
                    <div class="span6 right">
                            
                           <asp:Image ID="selectedimg" runat="server" ClientIDMode="Static" ImageUrl="images/nopic.png" BorderWidth="1"
                    AlternateText="" CssClass="newimg" />
                <asp:FileUpload ID="FileUpload1" runat="server" Width="150" />
                        <p class="help" data-tip="تصویر با پسوند رایج. حداکثر حجم 1 مگابایت.">
                        </p>
               


                    </div>
                    <div class="clear">
                    </div>
                </div> 
                
                 

                <div class="rowelement">
                    <div class="span2 right">
                    </div>
                    <div class="span6 right">
                        <asp:Button ID="Button1" runat="server" Text="ذخـــیره" CssClass="btn btn-success"
                            OnClick="Button1_Click" />
                        <a href="contentlist.aspx" class="btn btn-warning">انصراف</a>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
