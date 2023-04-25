<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="inbox.aspx.cs" Inherits="CMS.Manage.inbox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<title>لیست پیام های دریافتی</title>
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
<h4>لیست پیام های دریافتی</h4>
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
                onrowdatabound="GridView1_RowDataBound">
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
                        <HeaderTemplate>
                            عنوان
                            <div class="sorting">
                                <a href='<%= string.Format(Request.QueryString["id"]==null ? "inbox.aspx?sort=title&dir=asc":"inbox.aspx?sort=title&dir=asc&id="+Request.QueryString["id"]) %>' class="tooltip" title="مرتب سازی صعودی">
                                    <img src="images/sort_up.png" alt="up" /></a> <a href='<%= string.Format(Request.QueryString["id"]==null ? "inbox.aspx?sort=title&dir=desc":"inbox.aspx?sort=title&dir=desc&id="+Request.QueryString["id"]) %>'
                                        class="tooltip" title="مرتب سازی نزولی">
                                        <img src="images/sort_down.png" alt="down" /></a>
                            </div>
                        </HeaderTemplate>
                        <FooterTemplate>
                         عنوان
                            <div class="sorting">
                                <a href='<%= string.Format(Request.QueryString["id"]==null ? "inbox.aspx?sort=title&dir=asc":"inbox.aspx?sort=title&dir=asc&id="+Request.QueryString["id"]) %>' class="tooltip" title="مرتب سازی صعودی">
                                    <img src="images/sort_up.png" alt="up" /></a> <a href='<%= string.Format(Request.QueryString["id"]==null ? "inbox.aspx?sort=title&dir=desc":"inbox.aspx?sort=title&dir=desc&id="+Request.QueryString["id"]) %>'
                                        class="tooltip" title="مرتب سازی نزولی">
                                        <img src="images/sort_down.png" alt="down" /></a>
                            </div>
                        </FooterTemplate>
                        <ItemTemplate>

                        
                         <img class="inboximg" src='<%# Eval("isread").ToString().ToLower()=="false" ? "images/newmail.png" : "images/readmail.png" %>' alt="" />
                         <%# Eval("reply").ToString() == string.Empty ? "" : "<img class='inboximg' src='images/mail-reply.png' alt='' style='float: left;' title='جواب داده شده' />"%>
                            <a href='<%#Eval("id","readmail.aspx?id={0}") %>'>
                                <%# Eval("Title") %></a>
                                <br /><span style="font-size:12px; color:#888;"><%# CMS.CommonFunctions.SubStringHtml(Eval("text"),0,200)+ " ..." %></span>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField FooterText="تاریخ" SortExpression="RegDate" HeaderText="تاریخ">
                        <HeaderTemplate>
                            تاریخ
                            <div class="sorting">
                                <a href='<%= string.Format(Request.QueryString["id"]==null ? "inbox.aspx?sort=RegDate&dir=asc":"inbox.aspx?sort=RegDate&dir=asc&id="+Request.QueryString["id"]) %>' class="tooltip" title="مرتب سازی صعودی">
                                    <img src="images/sort_up.png" alt="up" /></a> <a href='<%= string.Format(Request.QueryString["id"]==null ? "inbox.aspx?sort=RegDate&dir=desc":"inbox.aspx?sort=RegDate&dir=desc&id="+Request.QueryString["id"]) %>'
                                        class="tooltip" title="مرتب سازی نزولی">
                                        <img src="images/sort_down.png" alt="down" /></a>
                            </div>
                        </HeaderTemplate>
                        <FooterTemplate>
                         تاریخ
                            <div class="sorting">
                                <a href='<%= string.Format(Request.QueryString["id"]==null ? "inbox.aspx?sort=RegDate&dir=asc":"inbox.aspx?sort=RegDate&dir=asc&id="+Request.QueryString["id"]) %>' class="tooltip" title="مرتب سازی صعودی">
                                    <img src="images/sort_up.png" alt="up" /></a> <a href='<%= string.Format(Request.QueryString["id"]==null ? "inbox.aspx?sort=RegDate&dir=desc":"inbox.aspx?sort=RegDate&dir=desc&id="+Request.QueryString["id"]) %>'
                                        class="tooltip" title="مرتب سازی نزولی">
                                        <img src="images/sort_down.png" alt="down" /></a>
                            </div>
                        </FooterTemplate>
                        <ItemTemplate>
                            <span style="text-align:center; font-family:'b nazanin';display:block;">
                                <%# CMS.CommonFunctions.String2date(Eval("RegDate"), 3, "")+"<br>" + CMS.CommonFunctions.String2date(Eval("RegDate"), 2, "H")%></span>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center"/>
                        <FooterStyle HorizontalAlign="Center" />
                        <ItemStyle Width="80" />
                    </asp:TemplateField>
                    <asp:TemplateField FooterText="تنظیمات" HeaderText="تنظیمات">
                        <ItemTemplate>
                            <center>
                             
                                    <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("id") %>' ToolTip="حذف مطلب" CommandName="Delete" runat="server" OnClientClick="return confirm('از حذف این رکورد دارید؟');" ><i class="icon-remove"></i></asp:LinkButton>

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
                    <p class="empty">پیامی وجود ندارد</p>
                </EmptyDataTemplate>
            </asp:GridView>
     

    <asp:SqlDataSource ID="sds_content" runat="server" ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>"
                SelectCommand="SELECT     ID, FulName, Title, Text, IsRead,reply, RegDate FROM contactPm order by id desc" DeleteCommand="DELETE FROM [contactpm] WHERE (ID = @ID)"
                >
                <DeleteParameters>
                    <asp:ControlParameter ControlID="GridView1" Name="ID" PropertyName="SelectedValue" />
                </DeleteParameters>
                <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="-1" Name="GroupID" QueryStringField="id"
                        Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>


</div>
</div> 

</asp:Content>
