<%@ Page  Language="C#" MasterPageFile="~/member/membermaster.Master" AutoEventWireup="true" CodeBehind="viewticket.aspx.cs" Inherits="CMS.member.viewticket" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>مشاهده تیکت</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainFullContent" runat="server">
    <div class="content">
            <div class="page-header">
                <h3>
                    <span>  <%=pmtitle %></span></h3>
            </div>
            <div class="panel light-shadow white title-transparent rounded clearfix ">
            

            
                <asp:Literal ID="ltr_error" runat="server"></asp:Literal>
        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1">
        <ItemTemplate>
        
        <div class="ticket" style='<%# Eval("IsManageReply").ToString().ToLower() == "true" ? "background: #E9FAEF;border-color:#B9F5A4" : "background: #f9f9f9" %>'>
        <div class="ticketdetails">
       <span class="right"><i class="icon-user"></i> فرستنده: <%# Eval("IsManageReply").ToString().ToLower() == "true" ? Eval("part") : "کاربر" %></span>
        <span class="left"><i class="icon-calendar"></i> <%#CMS.CommonFunctions.String2date(Eval("SendDate"), 3, "") + " ساعت: " + CMS.CommonFunctions.String2date(Eval("SendDate"), 2, "H") %></span>
       <div class="clear"></div>
        </div>
            <%# Eval("Text")%>
            <div class="clear">
            </div>

            <%--<%# Eval("Filename").ToString() == "none" ? "" : Eval("filename", "<a href='/content/temp/{0}' style='display:block;  border-top: 1px dashed #D3D3D3;margin-top: 10px;padding-top: 10px;'> <i class='icon-download-alt'></i> فایل ضمیمه</a>")%>--%>

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



        
        
            <asp:Literal ID="ltrattach" runat="server"></asp:Literal>
        <br /><br />
        <div class="contenttext">
        
                                <h6 class="tlt">
                    ارسال پاسخ</h6>
       
                <asp:Literal ID="ltrnorespone" Visible="false" runat="server" Text="<i class='icon-exclamation-sign'></i> تیکت بسته شده است، نمی توانید به این تیکت پاسخ دهید."></asp:Literal>
                <div runat="server" id="response">
                        <div class="center">  <asp:TextBox ID="TextBox1" Width="330px" runat="server" TextMode="MultiLine" placeholder="متن پاسخ شما"></asp:TextBox>
                    <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator1" ValidationGroup="required"
                        ControlToValidate="TextBox1" runat="server" ErrorMessage="<b>×</b>" Display="Dynamic"
                        ToolTip="فیلد الزامی"></asp:RequiredFieldValidator>
              <br />
                    </div>
                
                 
                     <div class="center">    <asp:Button ID="Button1" runat="server" Text="ارسال پاسخ"
                          OnClick="Button1_Click" ValidationGroup="required"  CssClass="btn btn-success" />
                </div></div>
            
        
            <div class="clear">
            </div>
        </div>
        </div>
    </div>
</asp:Content>
