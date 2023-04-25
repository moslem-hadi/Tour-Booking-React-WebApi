<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="changepass.aspx.cs" Inherits="CMS.Manage.changepass" %>

<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<title>تغییر رمز ورود</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box">
        <div class="header">
            <h4>تغییر رمز ورود</h4>
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
                            رمز ورود کنونی
                        </label>
                    </div>
                    <div class="span2 right">
                        <asp:TextBox ID="txtLast" runat="server" Width="160px" MaxLength="100" TextMode="Password" CssClass="req"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div> 




                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            رمز ورود جدید
                        </label>
                    </div>
                    <div class="span2 right">
                        <asp:TextBox ID="txtNew" runat="server" Width="160px" MaxLength="100" TextMode="Password" CssClass="req"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div> 




                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            تکرار رمز ورود جدید
                        </label>
                    </div>
                    <div class="span2 right">
                        <asp:TextBox ID="txtRep" runat="server" Width="160px" MaxLength="100"  TextMode="Password" CssClass="req"></asp:TextBox>
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
                         
                        <a href="default.aspx" class="btn btn-warning" >انصراف</a>
                   
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
    
 
</asp:Content>
