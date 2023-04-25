<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="relatedproduct.aspx.cs" Inherits="CMS.Manage.relatedproduct" %>

<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<title>کالاهای مرتبط با محصول</title>
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
<asp:Content ID="Content2" ContentPlaceHolderID="SideBarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
   
    
    
    <div class="cont tabs padding-reset ui-tabs ui-widget ui-widget-content ui-corner-all">
        <ul class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
            <li class="ui-state-default ui-corner-top"><a href="editproduct.aspx?id=<%=Request.QueryString["id"] %>">ویرایش محصول</a></li>
            <li class="ui-state-default ui-corner-top"><a href="productSeo.aspx?id=<%=Request.QueryString["id"] %>">تنظیمات سئو</a></li>
            <li class="ui-state-default ui-corner-top"><a href="productimages.aspx?id=<%=Request.QueryString["id"] %>">گالری تصاویر</a></li>
            
            <%--<li class="ui-state-default ui-corner-top ui-tabs-selected ui-state-active"><a href="relatedproduct.aspx?id=<%=Request.QueryString["id"] %>">محصولات مرتبط</a></li> --%>
            <li class="ui-state-default ui-corner-top"><a href="detail.aspx?id=<%=Request.QueryString["id"] %>">جزئیات محصول</a></li> 

        </ul>
        <div id="tabs-1" class="ui-tabs-panel ui-widget-content ui-corner-bottom">
            
    
     <div class="box righted">
        <div class="header">
            <h4>
                افزودن کالا </h4>
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
                            محصول
                        </label>
                    </div>
                    <div class="span2 right">
                        <asp:Literal ID="ltrTitle" runat="server"></asp:Literal>
                    </div>
                    <div class="clear">
                    </div>
                </div>

                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            کد کالاها
                        </label>
                    </div>
                    <div class="span2 right">
                        <asp:TextBox ID="txtIds" runat="server" Width="80px" TextMode="MultiLine" CssClass="ltr"></asp:TextBox>
                        <br />کد هرکالا در یک خط
                
                    </div>
                    <div class="clear">
                    </div>
                </div>
                    
                
                <div class="rowelement"> 
                    
                    
                    <div class="span3 right">
                        <asp:CheckBox ID="CheckBox2" runat="server" Text="دوطرفه" />
                       <br />

                        (این کالا جزو کالاهای مرتبط کدهای بالا شود)
                       <br />
                       <br />
                    </div>
                    <div class="clear">
                    </div>

                    
                    <div class="span1 right"></div>
                    <div class="span2 right">
                         <asp:Button ID="Button1" runat="server" Text="ذخـــیره" CssClass="btn btn-success"
                            OnClick="Button1_Click" /> 
                         
                        </div>
                    <div class="clear">
                    </div>

                    
                </div>

                  
            </fieldset>
        </div>
    </div>
    
<div class="box lefted">
<div class="header">
<h4>لیست کالاها</h4>
</div>
<div class="conten">
<asp:GridView CssClass="table normal margin-reset" ID="GridView1" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="ID" EnableModelValidation="True" 
                Width="100%" AllowPaging="True" GridLines="None" PageSize="100" 
                 EnableTheming="False" EnableViewState="False"  OnPageIndexChanging="GridView1_PageIndexChanging" 
                ShowFooter="True"  
        onrowcommand="GridView1_RowCommand" >
                
                <Columns>
               
                   
                    <asp:TemplateField FooterText="عنوان" HeaderText="عنوان" 
                        SortExpression="title">
                        <HeaderTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" onclick="javascript:SelectAllCheckboxesGridView1(this);"
                                Text="عنوان" />
                    </HeaderTemplate>
                        <ItemTemplate>
                           
                             <asp:CheckBox ID="chkBxSelect" runat="server" Text='' />  
                            
                                    <a style="font:normal 12px tahoma" href='<%# Eval("RelatedProductID","detail.aspx?id={0}") %>'><%# Eval("title") %></a>
                                   

                        </ItemTemplate> 
                        <FooterTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" onclick="javascript:SelectAllCheckboxesGridView1(this);"
                                Text="عنوان" />
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle Width="250px" />
                    </asp:TemplateField> 
                      <asp:TemplateField FooterText="کد" HeaderText="کد">
                        <ItemTemplate>
                            <center>
                             
                                <span style="font:normal 12px tahoma"><%# Eval("RelatedProductID") %></span>
                            
                            </center>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="40px" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle CssClass="footer" />
                <PagerStyle CssClass="pager" />
                <EmptyDataRowStyle CssClass="empty" />
                <EmptyDataTemplate >
                <p class="empty">هیچ رکوردی وجود ندارد.</p>
                </EmptyDataTemplate>
            </asp:GridView>

    
                <div class="rowelement">  <div class="span2 right">
    <div class="btnbottom"><asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click" OnClientClick="return confirm('از حذف انتخاب شده ها اطمینان دارید؟');"  class="btn btn-danger" ><i class="icon-remove"></i> حذف انتخاب شده ها</asp:LinkButton></div>
                    
</div>
                    <div class="clear"></div>
</div>
</div>

</div>



                   <div class="clear"></div>

        </div>
         
    </div>

</asp:Content>
