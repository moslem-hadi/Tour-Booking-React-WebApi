<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="readcomment.aspx.cs" Inherits="CMS.Manage.readcomment" %>

<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server"> 
    <style>.tlt{width:90px}</style>

<div class="box half  content"  style="width:590px">
<div class="header">
<h4> <%= pmtitle %></h4>
</div> 
 
    
                <div class="rowelement withpadding">
        <%= pmText %>
</div> 
</div> 
    <div class="box half content" style="width:271px"> 
        
<div class="header">
<h4>اطلاعات پیام</h4>
</div> 
                <div class="rowelement withpadding">
زمان: <b><%= pmTime %></b>
        <br />
        نام:  <b><%= pmName%></b>
           
        <br />

کالا:  <b> <%= link %></b>
       </div>
        
    </div>
    <div class="clear"></div>
    <div class="box">
<div class="header">
<h4>پاسخ به پیام</h4>
</div>
<div class="content">  
           <fieldset>
            
                <div class="rowelement withpadding">
              
      <cc1:messagebox id="MessageBox1" runat="server" messagetype="Submit" Message="پیام شما ارسال شد."
                visible="false" />
                </div>
                <div class="rowelement" id="txt" runat="server">
                    <div class="span2 right">
                        <label>
                            متن پیام
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:TextBox ID="TextBox4" TextMode="MultiLine" runat="server"></asp:TextBox>

                        <div style=" padding-right:261px">
                            <asp:CheckBox ID="CheckBox1" runat="server" Text="ارسال پیام" Checked="true" />
                            <br />
                            <asp:Button ID="Button2" runat="server" 
                    Text="ارسال" CssClass="btn btn-success" onclick="Button1_Click" /></div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
               


</fieldset>
        </div>
        </div>

</asp:Content>