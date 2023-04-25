<%@ Page  Language="C#" MasterPageFile="~/adminpanel/managemaster.Master" AutoEventWireup="true" CodeBehind="viewticket.aspx.cs" Inherits="CMS.Manage.viewticket" %>
<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server"> 
    
 <div class="box" style="margin-bottom:5px">
<div class="header">
<h4> <%= pmtitle %></h4>
</div> 
 
</div>    



    

            <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1">
        <ItemTemplate>
        
        <div class="ticket" style='<%# Eval("IsManageReply").ToString().ToLower() == "true" ? "background: #FAFAE4;" : "background: #fbfbfb" %>'>
        <div class="ticketdetails">
       
       <span style="float:right"><i class="icon-user"></i> فرستنده: <%# Eval("IsManageReply").ToString().ToLower() == "true" ? Eval("part") : "کاربر" %></span>
        <span style="float:left"><i class="icon-calendar"></i> <%#CMS.CommonFunctions.String2date(Eval("SendDate"), 3, "") + " ساعت: " + CMS.CommonFunctions.String2date(Eval("SendDate"), 2, "H") %></span>
       <div class="clear"></div>
        </div>
            <%# Eval("Text")%>
            <div class="clear">
            </div>
        </div>
        
        </ItemTemplate>
        </asp:Repeater>



        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>" 
            SelectCommand="ticketshow" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:QueryStringParameter Name="id" QueryStringField="id" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>



     

        <div class="ticket" style="background: #fbfbfb" runat="server" id="divattach">
     
            <asp:Literal ID="ltrattach" runat="server"></asp:Literal>
        </div>   
 

    <div class="clear"></div>
    <div class="box">
<div class="header">
<h4>پاسخ به تیکت</h4>
</div>
<div class="content">  
           <fieldset>
            
                <div class="rowelement withpadding">
              
      <cc1:messagebox id="MessageBox1" runat="server" messagetype="Submit" Message="تیکت شما ارسال شد."
                visible="false" />
                </div>
                <div class="rowelement" id="txt" runat="server">
                    <div class="span2 right">
                        <label>
                            متن تیکت
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:TextBox ID="txtText" TextMode="MultiLine" runat="server"></asp:TextBox>
                         
                    </div>
                     
              <%--      <div class="span2 right">
                        <label>
                            فایل ضمیمه
                        </label>
                    </div>
                    <div class="span6 right">
                         <asp:FileUpload ID="fluFile" runat="server" Width="210px" />
                        <p class="help" data-tip="پسوند فایل ارسالی باید یکی از پسوندهای زیر باشد:<br> zip, rar, doc, docx, png, jpg, gif, jpeg">
                        </p> 

                        <div style=" padding-right:261px"><asp:Button ID="Button1" runat="server" 
                    Text="ارسال" CssClass="btn btn-success" onclick="Button1_Click" /></div>
                    </div>--%>

                     

                    <div class="clear">
                    </div>
                </div>
               


</fieldset>
        </div>
        </div>
        

</asp:Content>