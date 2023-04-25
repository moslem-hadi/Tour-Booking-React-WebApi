<%@ Page Title="" Language="C#" MasterPageFile="~/member/membermaster.Master" AutoEventWireup="true" CodeBehind="services.aspx.cs" Inherits="CMS.member.services" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>لیست سفارشات</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainFullContent" runat="server">
    <div class="content">
        <div class="page-header">
            <h3>
                <span>لیست سفارشات</span></h3>
        </div>

        <asp:Literal ID="ltr_error" runat="server"></asp:Literal>

        <div class="panel light-shadow white title-transparent rounded clearfix ">
            <div style="margin: -15px -15px -15px">
                <asp:GridView CssClass="table normal margin-reset" ID="grvOrders"
                    runat="server" AutoGenerateColumns="False"
                    DataKeyNames="ID" DataSourceID="sdsLatestOrders"
                    EnableModelValidation="True" Width="100%"
                    AllowPaging="False" GridLines="None"
                    EnableTheming="False" EnableViewState="False"
                    ShowFooter="False">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                        <asp:TemplateField FooterText="شناسه سفارش" HeaderText="شناسه سفارش">

                            <ItemTemplate>
                                <%# Eval("ID") %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <FooterStyle HorizontalAlign="Center" />
                            <ItemStyle Width="120px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField FooterText="نام مشتری" HeaderText="نام مشتری">

                            <ItemTemplate>
                                <%# Eval("FullName") %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField FooterText="تماس مشتری" HeaderText="تماس مشتری">

                            <ItemTemplate>
                                <%# Eval("tell") %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField FooterText="تاریخ ثبت" HeaderText="تاریخ ثبت">

                            <ItemTemplate>

                                <%# CMS.CommonFunctions.String2date(Eval("RegDate"), 2, "D") %>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="120px" HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField FooterText="تاریخ رزرو" HeaderText="تاریخ رزرو">

                            <ItemTemplate>
                                <%# Eval("ReservedDateFa") %>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="120px" HorizontalAlign="Center" />
                        </asp:TemplateField>



                    </Columns>
                    <FooterStyle CssClass="footer" />
                    <PagerSettings Position="TopAndBottom" />
                    <PagerStyle CssClass="pager" />
                    <EmptyDataRowStyle CssClass="empty" />
                    <EmptyDataTemplate>

                        <p class="empty">
                            سرویسی ثبت نشده است
                        </p>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>


        <asp:SqlDataSource ID="sdsGetDriverUserServices" runat="server"
            ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>"
            SelectCommand="GetDriverUserServices" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter Name="userId" Type="Int32" DefaultValue="0" />
                <asp:Parameter Name="count" Type="Int32" DefaultValue="7" />
            </SelectParameters>
        </asp:SqlDataSource>





    </div>
</asp:Content>
