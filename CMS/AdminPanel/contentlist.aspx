<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="contentlist.aspx.cs" Inherits="CMS.Manage.contentlist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<title>لیست اخبار سایت</title>
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
<h4>لیست اخبار سایت</h4>
</div>
<div class="content">
 
      <div class="srch">
          <asp:Panel ID="Panel1" CssClass="con" runat="server" DefaultButton="LinkButton2">
          
          <asp:TextBox ID="TextBox1" runat="server" placeholder="جستجو در عنوان و آدرس یکتا"></asp:TextBox>
          <asp:LinkButton CssClass="srchbtn" ID="LinkButton2" runat="server" 
              onclick="LinkButton2_Click"><i class="icon-search"></i></asp:LinkButton>
          </asp:Panel>
          <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click" OnClientClick="return confirm('از حذف انتخاب شده ها اطمینان دارید؟');"  class="btn btn-danger" ><i class="icon-remove"></i> حذف انتخاب شده ها</asp:LinkButton>
          <a href="addcontent.aspx" class="btn btn-success"><i class="icon-plus"></i> افزودن خبر</a>
            
            <div class="clear"></div>
      </div>
      <asp:GridView CssClass="table normal margin-reset" ID="GridView1" runat="server" AutoGenerateColumns="False"
                DataKeyNames="ID" DataSourceID="sdsPageList" EnableModelValidation="True" Width="100%" 
                AllowPaging="True" GridLines="None" PageSize="20" EnableTheming="False" EnableViewState="False"
                ShowFooter="True">
                <AlternatingRowStyle CssClass="alt" />
                <Columns>
                    <asp:TemplateField FooterText="#" HeaderText="#" InsertVisible="False" SortExpression="ID">
                    <HeaderTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" onclick="javascript:SelectAllCheckboxesGridView1(this);"
                                Text="<i class='icon-th-list'></i>" />
                    </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkBxSelect" runat="server" Text='<%# Container.DataItemIndex+1 %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" onclick="javascript:SelectAllCheckboxesGridView1(this);"
                                Text="<i class='icon-th-list'></i>" />
                        </FooterTemplate>
                        <FooterStyle/>
                        <HeaderStyle/>
                        <ItemStyle Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField FooterText="عنوان" SortExpression="Title" HeaderText="عنوان">
                        
                        <ItemTemplate>
                            <a href='<%# Eval("id","../news/{0}/")+Eval("short") %>' target="_blank">
                                <%# Eval("Title") %></a>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                    </asp:TemplateField> 

                    <asp:TemplateField FooterText="بازدید" HeaderText="بازدید" 
                        SortExpression="ViewCount">
                        
                        <ItemTemplate>
                            <%# Eval("ViewCount") %>
                            
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField FooterText="تنظیمات" HeaderText="تنظیمات">
                        <ItemTemplate>
                            <center>
                             <a href='<%# Eval("ID","editcontent.aspx?id={0}") %>'
                                            title="ویرایش خبر " >
                                            <i class="icon-edit"></i></a>
                                    <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("id") %>' ToolTip="حذف خبر" CommandName="Delete" runat="server" OnClientClick="return confirm('از حذف این رکورد دارید؟');" ><i class="icon-remove"></i></asp:LinkButton>
                            </center>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="60px" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle CssClass="footer" />
                <PagerSettings Position="TopAndBottom" />
                <PagerStyle CssClass="pager" />
                <EmptyDataRowStyle CssClass="empty" />
                <EmptyDataTemplate>   <p class="empty">
                    خبری وجود ندارد. برای افزودن خبر جدید <a href="addcontent.aspx">
                            کلیک کنید</a>.</p>
                </EmptyDataTemplate>
            </asp:GridView>




    <asp:SqlDataSource ID="sdsPageList" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>" 
        DeleteCommand="DELETE FROM [content] WHERE (ID = @ID)"
        SelectCommand="ContentList_manage" SelectCommandType="StoredProcedure">
         <DeleteParameters>
                    <asp:ControlParameter ControlID="GridView1" Name="ID" PropertyName="SelectedValue" />
                </DeleteParameters>
                <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="n" Name="key" QueryStringField="key"
                        Type="String" />
                </SelectParameters>
    </asp:SqlDataSource>


</div>
</div> 

</asp:Content>
