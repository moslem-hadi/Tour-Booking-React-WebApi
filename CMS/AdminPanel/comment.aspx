<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="comment.aspx.cs" Inherits="CMS.Manage.comment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<title>لیست نظر های دریافتی</title>
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
<h4>لیست نظر های دریافتی</h4>
</div>
<div class="conten">
 
      <div class="srch">
          <asp:Panel ID="Panel1" CssClass="con" runat="server" DefaultButton="LinkButton2">
          
          <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
          <asp:LinkButton CssClass="srchbtn" ID="LinkButton2" runat="server" 
              onclick="LinkButton2_Click"><i class="icon-search"></i></asp:LinkButton>
          </asp:Panel>
          <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click" OnClientClick="return confirm('از حذف انتخاب شده ها اطمینان دارید؟');"  class="btn btn-danger" ><i class="icon-remove"></i> حذف انتخاب شده ها</asp:LinkButton>
       <div class="clear"></div>
      </div>

    <asp:GridView CssClass="table normal margin-reset"  ID="GridView1" runat="server" AutoGenerateColumns="False"
                DataKeyNames="ID" DataSourceID="sds_content" EnableModelValidation="True" Width="100%"
                AllowPaging="True" GridLines="None" PageSize="20" EnableTheming="False" EnableViewState="False"
                ShowFooter="True" OnPageIndexChanging="GridView1_PageIndexChanging" 
                onrowdatabound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                <AlternatingRowStyle CssClass="alt" />
                <Columns>
                    <asp:TemplateField FooterText="#" HeaderText="#" InsertVisible="False" SortExpression="ID">
                    <HeaderTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" onclick="javascript:SelectAllCheckboxesGridView1(this);"
                                Text="#" />
                    </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkBxSelect" runat="server" Text='<%# Container.DataItemIndex+1 %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" onclick="javascript:SelectAllCheckboxesGridView1(this);"
                                Text="#" />
                        </FooterTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Font-Names="b yekan" HorizontalAlign="Center" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField FooterText="عنوان" SortExpression="Title" HeaderText="عنوان">
                   
                        <ItemTemplate>

                        
                         <img class="inboximg" src='<%# Eval("isread").ToString().ToLower()=="false" ? "images/newmail.png" : "images/readmail.png" %>' alt="" />
                         <%# Eval("reply").ToString() == string.Empty ? "" : "<img class='inboximg' src='images/mail-reply.png' alt='' style='float: left;' title='جواب داده شده' />"%>
                            <a href='<%#Eval("id","readcomment.aspx?id={0}") %>'>
                                <%# Eval("Title") %></a>
                                <br /><span style="font-size:12px; color:#888;"><%# CMS.CommonFunctions.SubStringHtml(Eval("text"),0,200)+ " ..." %></span>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField FooterText="محصول" SortExpression="Title" HeaderText="محصول">
                   
                        <ItemTemplate>
                            <%# Eval("ProductID").ToString() == "0" ? "<center>نظـــر عمومی</center>" : 
                                    "<a style='font:normal 11px tahoma;display:block;text-align:center;' href='/product/" 
                                    + Eval("ProductID")+"/" + Eval("Slug") + "'>" + Eval("ProductTitle") + "</a>" %>
                            
                        </ItemTemplate>
                        <ItemStyle Width="200" />
                    </asp:TemplateField>
                    <asp:TemplateField FooterText="تاریخ" SortExpression="RegDate" HeaderText="تاریخ">
                        
                        <ItemTemplate>
                            <span style="text-align:center; display:block;">
                                <%# CMS.CommonFunctions.String2date(Eval("RegDate"), 2, "D")+"<br>" + CMS.CommonFunctions.String2date(Eval("RegDate"), 2, "H")%></span>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center"/>
                        <FooterStyle HorizontalAlign="Center" />
                        <ItemStyle Width="80" />
                    </asp:TemplateField>
                    <asp:TemplateField FooterText="تنظیمات" HeaderText="تنظیمات">
                        <ItemTemplate>
                            <center>
                                   <asp:LinkButton ID="LinkButton3" CommandArgument='<%# Eval("id") %>' ToolTip="تایید نظر" CommandName="ok" runat="server" OnClientClick="return confirm('از تایید این رکورد دارید؟');" ><i class="icon-ok"></i></asp:LinkButton>

                             
                                    <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("id") %>' ToolTip="حذف نظر" CommandName="Delete" runat="server" OnClientClick="return confirm('از حذف این رکورد دارید؟');" ><i class="icon-remove"></i></asp:LinkButton>

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
                    <p class="empty">نظری وجود ندارد</p>
                </EmptyDataTemplate>
            </asp:GridView>
     

    <asp:SqlDataSource ID="sds_content" runat="server" ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>"
                SelectCommand="comments_list" SelectCommandType="StoredProcedure" DeleteCommand="DELETE FROM [ProductComment] WHERE (ID = @ID)"
                >
                <DeleteParameters>
                    <asp:ControlParameter ControlID="GridView1" Name="ID" PropertyName="SelectedValue" />
                </DeleteParameters>
                <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="-1" Name="ID" QueryStringField="id"
                        Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>


</div>
</div> 

</asp:Content>
