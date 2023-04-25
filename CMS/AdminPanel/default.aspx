<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CMS.Manage._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<title>مدیریت محتوا وبتینا</title>
</asp:Content> 
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div class="quick-buttons">
                            <div class="row-fluid">
                                <div class="span3">
                                    <a href="productlist.aspx" class="btn btn-block"><i class="dashboard-icons archives_dashboard"></i>
                                        <span class="dasboard-icons-title">محصولات</span></a></div>
                                <div class="span3">
                                    <a href="addproduct.aspx" class="btn btn-block"><i class="dashboard-icons limited_edition_dashboard">
                                    </i><span class="dasboard-icons-title">افزودن محصول</span></a></div>
                                
                                <div class="span3">
                                    <a href="reservs.aspx" class="btn btn-block"><i class="dashboard-icons attibutes_dashboard"></i>
                                        <span class="dasboard-icons-title">سفارش ها</span></a></div>
                                <div class="span3">
                                    <a href="pagelist.aspx" class="btn btn-block"><i class="dashboard-icons administrative_docs_dashboard"></i>
                                        <span class="dasboard-icons-title">صفحات</span></a></div>
                                
                            </div>
                            
                            <div class="row-fluid">
                                <div class="span3">
                                    <a href="setting.aspx" class="btn btn-block"><i class="dashboard-icons settings_dashboard"></i>
                                        <span class="dasboard-icons-title">تنظیمات</span></a></div>
                                <div class="span3">
                                    <a href="paylist.aspx" class="btn btn-block"><i class="dashboard-icons bank_dashboard"></i>
                                        <span class="dasboard-icons-title">پرداخت ها</span></a></div>
                                <div class="span3">
                                    <a href="userlist.aspx" class="btn btn-block"><i class="dashboard-icons user_dashboard">
                                    </i><span class="dasboard-icons-title">کاربران</span></a></div>
                                <div class="span3">
                                    <a href="inbox.aspx" class="btn btn-block"><i class="dashboard-icons contact_dashboard"></i>
                                        <span class="dasboard-icons-title">پیام ها</span></a></div>
                            </div>
                        </div>
  

</asp:Content>
