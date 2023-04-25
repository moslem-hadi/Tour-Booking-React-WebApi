<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true"
    CodeBehind="userlist.aspx.cs" Inherits="CMS.Manage.userlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <title>لیست کاربران سایت</title>
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
            <h4>
                لیست کاربران سایت </h4>
        </div>
        <div class="conten">
            <div class="srch">
                <asp:Panel ID="Panel1" CssClass="con" runat="server" DefaultButton="LinkButton2">
                    <asp:TextBox ID="TextBox1" runat="server" placeholder="جستجو در ایمیل، نام و کدکاربری"></asp:TextBox>
                    <asp:LinkButton CssClass="srchbtn" ID="LinkButton2" runat="server" OnClick="LinkButton2_Click"><i class="icon-search"></i></asp:LinkButton>
                    <br />
                </asp:Panel>
                <a href="adduser.aspx" class="btn btn-success"><i class="icon-plus"></i>افزودن کاربر</a>
            <div class="clear"></div>
            </div>
            <asp:GridView CssClass="table normal margin-reset" ID="GridView1" runat="server"
                AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="sdsUserList_manage"
                EnableModelValidation="True" Width="100%" 
                AllowPaging="True" GridLines="None" PageSize="20" EnableTheming="False" EnableViewState="False"
                ShowFooter="True" OnRowCommand="GridView1_RowCommand">
                <AlternatingRowStyle CssClass="alt" />
                <Columns>
                    <asp:TemplateField FooterText="نام و نام خانوادگی" HeaderText="نام و نام خانوادگی">
                        <HeaderTemplate>
نام و نام خانوادگی
                        </HeaderTemplate>
                        <ItemTemplate>
                            <a href='<%# Eval("id","userdetail.aspx?id={0}") %>'
                             >
                                <%# Eval("fullname") %></a>
                                <%# Eval("Ismanager").ToString().ToLower()=="true" ? "<img src='images/icons/dashboard/briefcase.png' style='float:left' title='مدیر' />" : "" %>


                            <%# Eval("isbanned").ToString().ToLower()=="true" ?  "<span class='label label-danger' style='float:left'>مسدود</span>": "" %>


                        </ItemTemplate>
                        <FooterTemplate>
                            نام و نام خانوادگی
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField FooterText="کد" HeaderText="کد">
                        <ItemTemplate>
                            <span style="font:normal 11px tahoma"><%# Eval("ID") %></span>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField FooterText="نوع کاربری" HeaderText="نوع کاربری">
                        <ItemTemplate>
                            <%# CMS.CommonFunctions.UserTypeName(Eval("UserType") )%>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>


                    <asp:TemplateField FooterText="موبایل" HeaderText="موبایل">
                        <ItemTemplate>
                           <div class="center"><%# Eval("mobile") %></div>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField> 
                    <asp:TemplateField FooterText="وضعیت" HeaderText="وضعیت">
                        <ItemTemplate>
                            <%# Eval("isactive").ToString().ToLower()=="true" ?  "<span style='color:#2b7da3; font-size:13px'>فعال</span>": "<span style='color:#f01;font-size:13px'>غیرفعال</span>" %>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="45px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField FooterText="تنظیمات" HeaderText="تنظیمات">
                        <ItemTemplate>
                            <center>
                                <a href='<%# Eval("ID","userdetail.aspx?id={0}") %>' title="مشاهده اطلاعات کاربر "><i
                                    class="icon-eye-open"></i></a>

                                <a href='<%# Eval("ID","edituser.aspx?id={0}") %>' title="ویرایش کاربر ">
                                    <i class="icon-edit"></i></a>

                            </center>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="65px" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle CssClass="footer" />
                <PagerSettings Position="TopAndBottom" />
                <PagerStyle CssClass="pager" />
                <EmptyDataRowStyle CssClass="empty" />
                <EmptyDataTemplate>
                    <p class="empty">
                        رکوردی وجود ندارد.
                    </p>
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsUserList_manage" runat="server" ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>"
                SelectCommand="UserList_manage" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="typ" QueryStringField="e" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
    </div>
</asp:Content>
