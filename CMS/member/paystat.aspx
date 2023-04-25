<%@ Page  Language="C#" MasterPageFile="~/member/membermaster.Master" AutoEventWireup="true" CodeBehind="paystat.aspx.cs" Inherits="CMS.member.paystat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
<title>گزارش درخواست های دریافت از حساب</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainFullContent" runat="server">
    <div class="content">
            <div class="page-header">
                <h3>
                    <span>گزارش درخواست های دریافت از حساب</span></h3>
            </div>
      
            <div class="panel light-shadow white title-transparent rounded clearfix pad10">
 
      <div class="srch">
          <asp:Panel ID="Panel1" CssClass="con" runat="server" DefaultButton="LinkButton2">
          
          <asp:TextBox ID="TextBox1" runat="server" placeholder="جستجو در کد درخواست"></asp:TextBox>
          <asp:LinkButton CssClass="srchbtn" ID="LinkButton2" runat="server" 
              onclick="LinkButton2_Click"><i class="fa fa-search"></i></asp:LinkButton>
          </asp:Panel> 
          <div class="clear"></div>
      </div>
      <div style="margin:0 -15px -15px">
          <asp:GridView CssClass="table normal margin-reset" ID="GridView1" 
          runat="server" AutoGenerateColumns="False"
                DataKeyNames="ID" DataSourceID="sdspayreqlist" 
          EnableModelValidation="True" Width="100%" OnRowDataBound="GridView1_RowDataBound" 
                AllowPaging="True" GridLines="None" PageSize="10" PagerSettings-Position="Bottom" 
          EnableTheming="False" EnableViewState="False" ShowFooter="false">
                <AlternatingRowStyle CssClass="alt" />
                <Columns> 
                    <asp:TemplateField FooterText="کد درخواست"  HeaderText="کد درخواست">
                        
                        <ItemTemplate>
                                <%# Eval("ID") %>
                            
                        </ItemTemplate>
                        
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                     <asp:TemplateField FooterText="تارخ درخواست" HeaderText="تارخ درخواست">
                        <ItemTemplate>
                            <%# CMS.CommonFunctions.String2date(Eval("regdate"), 2, "D") + " ساعت " + CMS.CommonFunctions.String2date(Eval("regdate"), 2, "H")%>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="160px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                     
                    <asp:TemplateField FooterText="رسید" HeaderText="رسید" 
                       >
                        
                        <ItemTemplate>
                           <%# string.IsNullOrEmpty(Eval("ResidNum").ToString()) ? "هنوز پرداخت نشده است" : Eval("ResidNum")%>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="140px" HorizontalAlign="Center" />
                    </asp:TemplateField> 
                     
                    <asp:TemplateField FooterText="مبلغ" HeaderText="مبلغ" 
                        SortExpression="ViewCount">
                        
                        <ItemTemplate>
                            <%# CMS.CommonFunctions.SetCama(Eval("mablagh"))%> <span class="small">تومان</span>
                            
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField FooterText="وضعیت" HeaderText="وضعیت" 
                        SortExpression="Mablagh">
                        
                        <ItemTemplate>
                            <asp:Literal ID="ltrstat" runat="server"></asp:Literal>
                            <%# string.IsNullOrEmpty(Eval("NotDoneMsg").ToString()) ? "" : Eval("NotDoneMsg", "<p style='text-align:right;font-size:11px;margin:3px 0;'>علت: <span style='color:#f01'>{0}</span></p>")%>
                            
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                     
                </Columns>
                <FooterStyle CssClass="footer" />
                <PagerStyle CssClass="pager" />
                <EmptyDataRowStyle CssClass="empty" />				
                <EmptyDataTemplate>
				<img src="images/under_construction.jpg" alt="" class="mg-bottom-30" /><br />
				<p class="empty">درخواستی وجود ندارد. برای درخواست واریز وجه، <a href="bank.aspx">به صفحه بانک خود مراجعه کنید</a>.</p>
                </EmptyDataTemplate>
            </asp:GridView>




      </div>
    <asp:SqlDataSource ID="sdspayreqlist" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>" 
        SelectCommand="payreqlist" SelectCommandType="StoredProcedure">

                <SelectParameters> 
                            <asp:Parameter Name="UserID" Type="Int32" />
                </SelectParameters>
    </asp:SqlDataSource>


</div>
 
    </div>
</asp:Content>
