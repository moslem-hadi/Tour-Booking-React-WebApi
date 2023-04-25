<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="smssender.aspx.cs" Inherits="CMS.Manage.smssender" %>
<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<title>ارسال اسمس </title> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box">
        <div class="header">
            <h4>ارسال اسمس</h4>
        </div>
        <div class="content">
            
            <fieldset>
            
                <div class="rowelement withpadding">
                <cc1:MessageBox ID="MessageBox1" runat="server" MessageType="Error" Visible="false">
            </cc1:MessageBox>
                </div>
             
                <div class="rowelement">
                    <div class="span2 right">
                        <label>به شماره</label>
                    </div>
                    <div class="span6 right">
                        <asp:TextBox ID="txtNums" runat="server"  CssClass="ltr req"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div> 
                 
                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            متن پیامک</label>
                    </div>
                    <div class="span6 right">  
                        <asp:TextBox ID="txtText" runat="server" TextMode="MultiLine" CssClass="req"></asp:TextBox>
                       
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
</asp:Content>
