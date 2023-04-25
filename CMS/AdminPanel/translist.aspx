<%@ Page Language="C#" MasterPageFile="~/adminpanel/managemaster.Master" AutoEventWireup="true" CodeBehind="translist.aspx.cs" Inherits="CMS.Manage.translist" %>

<%@ Register Assembly="CollectionPager" Namespace="SiteUtils" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <title>لیست تراکنش ها</title>
    <script>
        function SelectAllCheckboxesGridView1(chk) {
            $('#<%=GridView1.ClientID %>').find("input:checkbox").each(function () {
                if (this != chk) {
                    this.checked = chk.checked;
                }
            });
        }
    </script>

    <script src="/js/openwindow.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="box">
        <div class="header">
            <h4>لیست تراکنش ها</h4>
        </div>
        <div class="conten">

            <div class="srch">
                <asp:Panel ID="Panel1" CssClass="con" runat="server" DefaultButton="LinkButton2">
                    <asp:TextBox ID="TextBox1" runat="server" placeholder="جستجو کد کاربر:"></asp:TextBox>
                    <asp:LinkButton CssClass="srchbtn" ID="LinkButton2" runat="server" OnClick="LinkButton2_Click"><i class="icon-search"></i></asp:LinkButton>

                </asp:Panel>

                <a href="addtransatction.aspx" class="btn btn-success"><i class="icon-plus"></i>افزودن تراکنش</a>

                <div class="clear"></div>
            </div>

            <asp:GridView CssClass="table normal margin-reset" ID="GridView1" runat="server"
                AutoGenerateColumns="False" DataKeyNames="ID" EnableModelValidation="True"
                Width="100%" AllowPaging="True" GridLines="None"
                EnableTheming="False" EnableViewState="False" ShowFooter="false">
                <AlternatingRowStyle CssClass="alt" />
                <Columns>
                    <asp:TemplateField FooterText="کد" HeaderText="کد">
                        <ItemTemplate>
                            <%# Eval("ID")%>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField FooterText="عنوان" SortExpression="Title" HeaderText="عنوان">
                        <ItemTemplate>
                            <span style="color: #777; font-size: 11px;">
                                <a
                                    href='<%# Eval("id","edittrnasaction.aspx?id={0}") %>'>
                                    <%# Eval("Description") %></a>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField FooterText="کاربر" HeaderText="کاربر">
                        <ItemTemplate>
                            
                            <a href="userdetail.aspx?id=<%# Eval("userid")%>">
                                <%# Eval("userid")%>
                            </a>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField FooterText="نوع" HeaderText="نوع">
                        <ItemTemplate>

                            <%# Eval("Type").ToString().ToLower() == "true" ? "<span class='label label-success'><i class='icon-arrow-up icon-white' style='margin:2px'></i></span>" : "<span class='label label-important'><i class='icon-arrow-down icon-white' style='margin:2px'></i></span>"%>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField FooterText="تاریخ" HeaderText="تاریخ" SortExpression="ViewCount">
                        <ItemTemplate>
                            <%# CMS.CommonFunctions.String2date(Eval("RegDate"), 2, "H")+ " - " + CMS.CommonFunctions.String2date(Eval("RegDate"), 2, "D").Remove(0,2) %>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="110px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField FooterText="مبلغ" HeaderText="مبلغ" SortExpression="Mablagh">
                        <ItemTemplate>
                            <div style='color:<%# Eval("Type").ToString().ToLower()=="true" ? "#468847" :"#b94a48"%>'>
                            <b style="display:inline-block; direction:ltr"><%# CMS.CommonFunctions.SetCama(Eval("Mablagh"))%></b>
                            <span class="small">تومان</span></div>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle CssClass="footer" />
                <PagerStyle CssClass="pager" />
                <EmptyDataRowStyle CssClass="empty" />
                <EmptyDataTemplate>
                    <p class="empty">
                        رکوردی وجود ندارد.
                    </p>
                </EmptyDataTemplate>
            </asp:GridView>



            <div class="clear"></div>

            <cc1:CollectionPager ID="CollectionPager1" runat="server" BackText="قبلی" ControlCssClass="pagering"
                FirstText="ابتدا" LabelText="صفحه" LastText="آخر" MaxPages="20000" NextText="بعدی"
                PageNumbersDisplay="Numbers" QueryStringKey="page"
                ResultsLocation="None" EnableViewState="False" LabelStyle="" PageSize="10" ResultsStyle=""
                ShowLabel="False" PageNumbersSeparator=" " ShowFirstLast="False">
            </cc1:CollectionPager>
            <div class="clear"></div>


        </div>
    </div>

</asp:Content>
