<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="specoption.aspx.cs" Inherits="CMS.Manage.specoption" %>

<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<title>لیست گزینه های مشخصه</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box righted">
        <div class="header">
            <h4>
                ایجاد گزینه جدید <asp:HyperLink ID="hplback" style="font-size:14px;padding-left:10px;font-weight:normal;float:left" runat="server">برگشت به لیست مشخصه ها</asp:HyperLink></h4>
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
                            مشخصه
                        </label>
                    </div>
                    <div class="span2 right">
                        <asp:Literal ID="ltrtitle" runat="server"></asp:Literal>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="clear s">
                </div> 
                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            عنوان گزینه
                        </label>
                    </div>
                    <div class="span2 right">
                        <asp:TextBox ID="txtTitle" runat="server" Width="160px" MaxLength="100" CssClass="req"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="clear s">
                </div> 
                
                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            اولویت                        </label>
                    </div>
                    <div class="span2 right">
                        <asp:TextBox ID="txtPriority" runat="server" onkeypress="return validate(event)" Text="0" Width="160px" MaxLength="100" CssClass="ltr"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div> 
                <div class="rowelement">
                    <div class="span1 right">
                    </div>
                    <div class="span2 right">
                        <asp:Button ID="Button1" runat="server" Text="ذخـــیره" CssClass="btn btn-success"
                            OnClick="Button1_Click" />
                            
                        <asp:Button ID="Button2" runat="server" Text="ذخـــیره" CssClass="btn btn-success"
                            OnClick="Button2_Click" Visible="false" />
                             
                        <asp:HyperLink ID="cancl" CssClass="btn btn-warning" runat="server">انصراف</asp:HyperLink>
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
<h4>لیست گزینه ها</h4>
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
                            <a  href='<%# Eval("id","/contents/{0}") %>' title='<%# Eval("Title") %>'><%# Eval("Title") %></a>
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
