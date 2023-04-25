<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="seincome.aspx.cs" Inherits="CMS.Manage.seincome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<title>لیست ورودی های موتورهای جستجو</title>
  <script>
      function SelectAllCheckboxesGridView1(chk) {
          $('#<%=GridView1.ClientID %>').find("input:checkbox").each(function () {
              if (this != chk) {
                  this.checked = chk.checked;
              }
          });
      }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
 
<div class="box">
<div class="header">
<h4>ورودی های موتورهای جستجو</h4>
</div>
<div class="conten">
 
      <div class="srch">
         
          <asp:LinkButton ID="LinkButton4" runat="server" onclick="LinkButton3_Click" class="btn btn-danger" OnClientClick="return confirm('مطمئنید؟');" ><i class="icon-ok"></i> حذف انتخاب شده ها </asp:LinkButton>
            <div class="clear"></div>
            
      </div>
      
            <asp:GridView CssClass="table normal margin-reset" ID="GridView1" runat="server" AutoGenerateColumns="False"
                DataSourceID="sds_content" EnableModelValidation="True" Width="100%"
                AllowPaging="True" GridLines="None" PageSize="20" EnableTheming="False" EnableViewState="False"
                ShowFooter="True" DataKeyNames="KeyWord"  >
                        <AlternatingRowStyle CssClass="alt" />
                <Columns>
                
                    <asp:TemplateField FooterText="#" HeaderText="#" InsertVisible="False" 
                        SortExpression="ID">
                        <ItemTemplate>
                            <asp:Label ID="lblKeyWord" Visible="false" runat="server" Text='<%# Eval("KeyWord") %>'></asp:Label> 
                            <asp:CheckBox ID="chkBxSelect" runat="server" Text='<%# Container.DataItemIndex+1 %>'/>
                        </ItemTemplate>
                        <HeaderTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" onclick="javascript:SelectAllCheckboxesGridView1(this);"
                                Text="#" />
                        </HeaderTemplate>
                        <FooterTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" onclick="javascript:SelectAllCheckboxesGridView1(this);"
                                Text="#" />
                        </FooterTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:TemplateField> 
                  
                   <asp:TemplateField FooterText="کلمه کلیدی" SortExpression="cnt" 
                        HeaderText="کلمه کلیدی">
                    
                        <ItemTemplate> 
                        <a target="_blank" href='<%# string.Format("https://www.google.com/search?q={0}",CMS.CommonFunctions.ReplaceSpaceForSearch2(Server.UrlDecode(Eval("KeyWord").ToString())))  %>' title='<%# Server.UrlDecode(Eval("KeyWord","مشاهده نتایج جستجوی گوگل برای {0}")) %>'><%# Server.UrlDecode(Eval("KeyWord").ToString()) %></a>    
                  
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        
                    </asp:TemplateField>



                   <asp:TemplateField FooterText="تعداد تکرار" SortExpression="cnt" 
                        HeaderText="تعداد تکرار">
                    
                        <ItemTemplate> 
                        <%# Eval("cnt")%>
              
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle Width="100px" />
                    </asp:TemplateField> 
                </Columns>
                <FooterStyle CssClass="footer" />
                <PagerSettings Position="Bottom" />
                <PagerStyle CssClass="pager" />
                <EmptyDataRowStyle CssClass="empty" />
                <EmptyDataTemplate>
                    <p class="empty">
                        رکوردی وجود ندارد.</p>
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:SqlDataSource ID="sds_content" runat="server" ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>"
                SelectCommand="VewStats" SelectCommandType="StoredProcedure"
                DeleteCommand="delete from referrerKeyword where KeyWord=@KeyWord"
                > 
            </asp:SqlDataSource>

             

             


</div>
</div> 

</asp:Content>
