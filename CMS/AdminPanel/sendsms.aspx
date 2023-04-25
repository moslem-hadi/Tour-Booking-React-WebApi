<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="sendsms.aspx.cs" Inherits="CMS.Manage.sendsms" ValidateRequest="false" %>

<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<title>ارسال پیامک</title> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box">
        <div class="header">
            <h4>ارسال پیامک</h4>
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
                            شماره فرستنده
                        </label>
                    </div>
                    <div class="span6 right">

                        <asp:DropDownList ID="DropDownList1" runat="server" Width="200">
                            <asp:ListItem>10000086676429</asp:ListItem>
                            <asp:ListItem>5000282313</asp:ListItem>
                            <asp:ListItem>5000290</asp:ListItem>
                            <asp:ListItem>10000139500000</asp:ListItem>
                            <asp:ListItem>02100021000</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>
                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            شماره گیرنده
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:TextBox ID="TextBox1" runat="server" Width="200" CssClass="req"></asp:TextBox>
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
                        <asp:TextBox ID="TextBox3" runat="server" Width="400" Height="180" TextMode="MultiLine" CssClass="ltr req" ></asp:TextBox>
                        
                         
                    </div>
                    <div class="clear">
                    </div>
                </div>
                
                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            دریافت وضعیت تحویل</label>
                    </div>
                    <div class="span6 right">
                        <asp:CheckBox ID="CheckBox1" Text="دریافت وضعیت تحویل" runat="server" />
                        
                         
                    </div>
                    <div class="clear">
                    </div>
                </div>

                
                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            ارسال فلش</label>
                    </div>
                    <div class="span6 right">
                        <asp:CheckBox ID="CheckBox2" Text="بصورت فلش" runat="server" />
                        
                         
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
                        <asp:Button ID="Button1" runat="server" Text="ارسال کن" CssClass="btn btn-success"
                            OnClick="Button1_Click" /> 
                    </div>
                    <div class="clear">
                    </div>
                </div>
                 
                <div class="rowelement">
                
    <asp:GridView ID="GridView1" runat="server" Width="100%"  CssClass="table normal margin-reset"
                    AutoGenerateColumns="true" GridLines="None" AllowPaging="True" PageSize="150"
                    AlternatingRowStyle-CssClass="alt" >
                    <EmptyDataTemplate>
                        <p class="emptygrid">
                            دلیوری ای وجود ندارد.</p>
                    </EmptyDataTemplate>
                    <AlternatingRowStyle CssClass="alt" />
                </asp:GridView>


                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
