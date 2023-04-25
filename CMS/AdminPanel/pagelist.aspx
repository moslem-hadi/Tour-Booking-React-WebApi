<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="pagelist.aspx.cs" Inherits="CMS.Manage.pagelist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <title>لیست صفحات سایت</title>
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
            <h4>لیست صفحات سایت</h4>
        </div>
        <div class="conten">


            <div class="srch">
                <asp:Panel ID="Panel1" CssClass="con" runat="server" DefaultButton="LinkButton2">

                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    <asp:LinkButton CssClass="srchbtn" ID="LinkButton2" runat="server"
                        OnClick="LinkButton2_Click"><i class="icon-search"></i></asp:LinkButton>
                </asp:Panel>
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" OnClientClick="return confirm('از حذف انتخاب شده ها اطمینان دارید؟');" class="btn btn-danger"><i class="icon-remove"></i> حذف انتخاب شده ها</asp:LinkButton>
                <a href="addpage.aspx" class="btn btn-success"><i class="icon-plus"></i>افزودن صفحه</a>


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
                        <FooterStyle />
                        <HeaderStyle />
                        <ItemStyle Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField FooterText="عنوان" SortExpression="Title" HeaderText="عنوان">

                        <ItemTemplate>
                            <a href='<%# Eval("short","http://parhantransfer.ir/page/{0}") %>' target="_blank">
                                <%# Eval("Title") %></a>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                    </asp:TemplateField>



                    <%--<asp:TemplateField FooterText="بازدید" HeaderText="بازدید" 
                        SortExpression="ViewCount">
                        
                        <ItemTemplate>
                            <%# Eval("ViewCount") %>
                            
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                    </asp:TemplateField>--%>
                    <asp:TemplateField FooterText="تنظیمات" HeaderText="تنظیمات">
                        <ItemTemplate>
                            <center>
                             <a href='<%# Eval("ID","editpage.aspx?id={0}") %>'
                                            title="ویرایش صفحه " >
                                            <i class="icon-edit"></i></a>
                                    <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("id") %>' ToolTip="حذف صفحه" CommandName="Delete" runat="server" OnClientClick="return confirm('از حذف این رکورد اطمینان دارید؟');" ><i class="icon-remove"></i></asp:LinkButton>
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
                <EmptyDataTemplate>
                    <p class="empty">
                        صفحه ای وجود ندارد. برای افزودن صفحه جدید <a href="addpage.aspx">کلیک کنید</a>.
                    </p>
                </EmptyDataTemplate>
            </asp:GridView>




            <asp:SqlDataSource ID="sdsPageList" runat="server"
                ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>"
                DeleteCommand="DELETE FROM [pagecontent] WHERE (ID = @ID)"
                SelectCommand="PageList_manage" SelectCommandType="StoredProcedure">
                <DeleteParameters>
                    <asp:ControlParameter ControlID="GridView1" Name="ID" PropertyName="SelectedValue" />
                </DeleteParameters>
            </asp:SqlDataSource>


        </div>
    </div>

</asp:Content>
