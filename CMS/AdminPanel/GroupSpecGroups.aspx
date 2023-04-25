<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="GroupSpecGroups.aspx.cs" Inherits="CMS.Manage.GroupSpecGroups" %>

<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <title>لیست دسته بندی مشخصه های محصول</title>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box">
        <div class="header">
            <h4>ایجاد دسته بندی مشخصه جدید</h4>
        </div>
        <div class="content">

            <fieldset>

                <div class="rowelement withpadding">
                    <cc1:MessageBox ID="MessageBox1" runat="server" MessageType="Error" Visible="false">
                    </cc1:MessageBox>
                </div>
                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            عنوان دسته
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:TextBox ID="txtTitle" runat="server" Width="200px" MaxLength="100" CssClass="req"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="clear s">
                </div>

                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            اولویت (از کم به زیاد)
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:TextBox ID="txtPriority" runat="server" onkeypress="return validate(event)" Text="0" Width="60px" MaxLength="100" CssClass="ltr"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="clear s">
                </div>

                <div class="rowelement">
                    <div class="span6 right">
                    </div>
                    <div class="span6 right">
                        <asp:Button ID="Button1" runat="server" Text="ذخـــیره" CssClass="btn btn-success"
                            OnClick="Button1_Click" />

                        <asp:Button ID="Button2" runat="server" Text="ذخـــیره" CssClass="btn btn-success"
                            OnClick="Button2_Click" Visible="false" />

                        <a href="contentgroup.aspx" class="btn btn-warning" runat="server" id="cancl" visible="false">انصراف</a>
                        <asp:Literal ID="ltrID" Visible="false" runat="server"></asp:Literal>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </fieldset>
        </div>
    </div>

    <div class="box">
        <div class="header">
            <h4>لیست دسته بندی ها</h4>
        </div>
        <div class="conten">



            <asp:SqlDataSource ID="sdsSpecGroups" runat="server" ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>"
                SelectCommand="select * from SpecificationAttributeGroup order by priority" SelectCommandType="Text">
               
            </asp:SqlDataSource>

            <asp:GridView CssClass="table normal margin-reset" ID="GridView1" runat="server" AutoGenerateColumns="False"
                DataKeyNames="ID" EnableModelValidation="True"
                Width="100%" AllowPaging="True" GridLines="None" PageSize="40" DataSourceID="sdsSpecGroups"
                EnableTheming="False" EnableViewState="False" OnPageIndexChanging="GridView1_PageIndexChanging"
                ShowFooter="True" OnRowDataBound="GridView1_RowDataBound"
                OnRowCommand="GridView1_RowCommand">

                <Columns>

                    <asp:TemplateField FooterText="عنوان" HeaderText="عنوان"
                        SortExpression="title">
                        <HeaderTemplate>
                            عنوان
                        </HeaderTemplate>
                        <ItemTemplate>


                            <span style="font-size: 12px; color: #808080"><%# Eval("id") %> )</span>           <%# Eval("Title") %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle Width="250px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="اولویت" FooterText="اولویت">
                        <ItemTemplate>
                            <center>
                                <%# Eval("Priority") %>
                          </center>
                        </ItemTemplate>

                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="مشخصه ها" FooterText="مشخصه ها">
                        <ItemTemplate>
                            <center>
                                <asp:Literal ID="ltr_count" runat="server"></asp:Literal>
                          </center>
                        </ItemTemplate>

                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField FooterText="تنظیمات" HeaderText="تنظیمات">
                        <ItemTemplate>
                            <center>
                            
                            <asp:LinkButton ID="LinkButton2" CommandArgument='<%# Eval("id") %>' CommandName="edt" runat="server" ><i class="icon-edit"></i></asp:LinkButton>

<asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("id") %>' CommandName="Del" runat="server" OnClientClick="return confirm('از حذف این رکورد دارید؟ SpecID رکوردهای مرتبط 0 خواهد شد.');" ><i class="icon-remove"></i></asp:LinkButton>
                            
                            </center>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="50px" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle CssClass="footer" />
                <PagerStyle CssClass="pager" />
                <EmptyDataRowStyle CssClass="empty" />
                <EmptyDataTemplate>
                    <p class="empty">هیچ رکوردی وجود ندارد.</p>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
