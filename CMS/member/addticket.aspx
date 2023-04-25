<%@ Page  Language="C#" MasterPageFile="~/member/membermaster.Master" AutoEventWireup="true" CodeBehind="addticket.aspx.cs" Inherits="CMS.member.addticket" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>ارسال تیکت پشتیبانی</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainFullContent" runat="server">
    <div class="content">
            <div class="page-header">
                <h3>
                    <span>ارسال تیکت پشتیبانی</span></h3>
            </div>
        <div class="panel light-shadow white title-transparent rounded clearfix">
            <fieldset>
                <asp:Literal ID="ltr_error" runat="server"></asp:Literal>
                <div runat="server" id="newticket">
               
                <div class="row">
                    <div class="col-md-2 right">
                        <label>
                            اولویت
                        </label>
                    </div>
                    <div class="col-md-8 right">
                        <asp:DropDownList ID="DropDownList2" runat="server" Width="90">
                        
                    <asp:ListItem Value="High">خیلی زیاد</asp:ListItem>
                    <asp:ListItem Value="Medium" Selected="True">متوسط</asp:ListItem>
                    <asp:ListItem Value="Low">کم</asp:ListItem>
                        </asp:DropDownList>  
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>
                <%--<div class="row">
                    <div class="col-md-2 right">
                        <label>
                            بخش مربوطه
                        </label>
                    </div>
                    <div class="col-md-8 right">
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem>پشتیبانی کاربران</asp:ListItem>
                            <asp:ListItem>بخش مالی</asp:ListItem>
                            <asp:ListItem>تیم توسعه و طراحی</asp:ListItem>
                            <asp:ListItem>مدیریت سایت</asp:ListItem>
                        </asp:DropDownList>  
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>--%>
                <div class="row">
                    <div class="col-md-2 right">
                        <label>
                            عنوان تیکت
                        </label>
                    </div>
                    <div class="col-md-8 right">
                        <asp:TextBox ID="txtOnvan" runat="server" MaxLength="100" CssClass="req"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>
                <div class="row">
                    <div class="col-md-2 right">
                        <label>
                            توضیحات</label>
                    </div>
                    <div class="col-md-8 right"> 
                           
                        <asp:TextBox ID="txtText" Height="160" runat="server" TextMode="MultiLine" CssClass="req"></asp:TextBox>
                            
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div> 
                <div class="row">
                    <div class="col-md-2 right">
                        <label>
                            فایل ضمیمه</label>
                    </div>
                    <div class="col-md-8 right">
                        <asp:FileUpload ID="fluFile" runat="server" Width="210px" />
                        <p class="help" data-tip="پسوند فایل ارسالی باید یکی از پسوندهای زیر باشد:<br> zip, rar, doc, docx, png, jpg, gif, jpeg">
                        </p> 
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div> 
                <div class="row">
                    <div class="col-md-2 right">
                        &nbsp;&nbsp;
                    </div>
                    <div class="col-md-8 right">
                        <asp:Button ID="Button1" runat="server" Text="ارســـال" OnClientClick="return checkfile();"
                            CssClass="btn btn-success" OnClick="Button1_Click" />
                        <a href="ticketlist.aspx" class="btn btn-danger">انصراف</a>
                    </div>
                    <div class="clear">
                    </div>
                </div></div>
            </fieldset>
        </div>
    </div>
</asp:Content>
