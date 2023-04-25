<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="menu.aspx.cs" Inherits="CMS.Manage.menu" %>

<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<title>لیست منوها</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box righted">
        <div class="header">
            <h4>
                ایجاد منو جدید</h4>
        </div>
        <div class="content">
            
            <fieldset>
            
                <div class="rowelement withpadding">
                <cc1:MessageBox ID="MessageBox1" runat="server" MessageType="Error" Visible="false">
            </cc1:MessageBox>
                </div>
                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            عنوان منو
                        </label>
                    </div>
                    <div class="span2 right">
                        <asp:TextBox ID="txtTitle" runat="server" Width="160px" MaxLength="100" CssClass="req"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>
                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            لینک منو
                        </label>
                    </div>
                    <div class="span2 right">
                        <asp:TextBox ID="txtLink" runat="server" Width="160px" MaxLength="100" CssClass="req ltr"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div> 
                
                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            موقعیت</label>
                    </div>
                    <div class="span2 right">
                        <asp:DropDownList ID="ddlPosition" ClientIDMode="Static" Width="100px" runat="server">
                            <asp:ListItem Value="1" Selected="True">منوی اصلی بالا</asp:ListItem>
                            <asp:ListItem Value="2">منوی فوتر</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <script>
                    $(function () {
                        $("#ddlPosition").change(function () {
                            if ($(this).val() == "2") {
                                $("#divparent").hide();
                            }
                            else
                                $("#divparent").show();
                        });
                        $("#ddlPosition").change();
                    });
                </script>
                
                <div class="separator">
                
                </div> 
                <div id="divparent">

                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            منوی والد
                        </label>
                    </div>
                    <div class="span2 right">
                        <asp:DropDownList ID="DropDownList1" Width="160px" runat="server"> 
                </asp:DropDownList>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div> 
                </div> 
                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            اولویت</label>
                    </div>
                    <div class="span2 right">
                        <asp:TextBox ID="txtPriority" runat="server" Width="40px" MaxLength="2" CssClass="req ltr"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                
                <div class="separator">
                
                </div> 



                
                <div class="rowelement">
                    <div class="span1 right">
                    &nbsp;
                    </div>
                    <div class="span2 right">
                        <asp:Button ID="Button1" runat="server" Text="ذخـــیره" CssClass="btn btn-success"
                            OnClick="Button1_Click" />
                            
                        <asp:Button ID="Button2" runat="server" Text="ذخـــیره" CssClass="btn btn-success"
                            OnClick="Button2_Click" Visible="false" />
                            
                        <a href="menu.aspx" class="btn btn-warning" runat="server" id="cancl" visible="false">انصراف</a>
                        <asp:Literal ID="ltrID" Visible="false" runat="server"></asp:Literal>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
    
<div class="box lefted">
<div class="header">
<h4>لیست منو ها</h4>
</div>
<div class="conten">
<asp:GridView CssClass="table normal margin-reset" ID="GridView1" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="ID" EnableModelValidation="True" 
                Width="100%" AllowPaging="True" GridLines="None" PageSize="40" 
                 EnableTheming="False" EnableViewState="False"  OnPageIndexChanging="GridView1_PageIndexChanging" 
                ShowFooter="True" 
        onrowcommand="GridView1_RowCommand" >
                
                <Columns>
               
                    <asp:TemplateField FooterText="عنوان" HeaderText="عنوان" 
                        SortExpression="title">
                        <HeaderTemplate>
                          عنوان
                        </HeaderTemplate>
                        <ItemTemplate>
                            <a  href='<%# Eval("link") %>' target="_blank" ><%# Eval("Title") %></a>
                        </ItemTemplate> 
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle Width="250px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="موقعیت" FooterText="موقعیت">
                        <ItemTemplate>
                            <center>
                                <%#Eval("Position")%>
                          </center>
                        </ItemTemplate>
                        
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="70px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="اولویت" FooterText="اولویت">
                        <ItemTemplate>
                            <center>
                                <%#Eval("Priority")%>
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

<asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("id") %>' CommandName="Del" runat="server" OnClientClick="return confirm('از حذف این رکورد دارید؟');" ><i class="icon-remove"></i></asp:LinkButton>
                            
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
                <EmptyDataTemplate >
                <p class="empty">هیچ رکوردی وجود ندارد.</p>
                </EmptyDataTemplate>
            </asp:GridView>
</div></div>
</asp:Content>
