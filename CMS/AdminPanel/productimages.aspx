<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="productimages.aspx.cs" Inherits="CMS.Manage.productimages" %>

<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<title>لیست تصاویر محصول</title>
 <style type="text/css">
        
        table
        {
            border: 1px solid #ccc;
        }

        table th
        {
            background-color: #F7F7F7;
            color: #333;
            font-weight: bold;
        }

        table th, table td
        {
            padding: 5px;
            border-color: #ccc;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    
    
    <div class="cont tabs padding-reset ui-tabs ui-widget ui-widget-content ui-corner-all">
        <ul class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
            <li class="ui-state-default ui-corner-top"><a href="editproduct.aspx?id=<%=Request.QueryString["id"] %>">ویرایش محصول</a></li>
            <li class="ui-state-default ui-corner-top"><a href="productSeo.aspx?id=<%=Request.QueryString["id"] %>">تنظیمات سئو</a></li>
            <li class="ui-state-default ui-corner-top ui-tabs-selected ui-state-active"><a href="productimages.aspx?id=<%=Request.QueryString["id"] %>">گالری تصاویر</a></li>
            
            <%--<li class="ui-state-default ui-corner-top"><a href="relatedproduct.aspx?id=<%=Request.QueryString["id"] %>">محصولات مرتبط</a></li> --%>
           <li class="ui-state-default ui-corner-top"><a href="productdiscount.aspx?id=<%=Request.QueryString["id"] %>">تخفیف‌ها</a></li>
            <li class="ui-state-default ui-corner-top"><a href="prices.aspx?id=<%=Request.QueryString["id"] %>">قیمت روزها</a></li> 
             <li class="ui-state-default ui-corner-top"><a href="detail.aspx?id=<%=Request.QueryString["id"] %>">جزئیات محصول</a></li> 

        </ul>
        <div id="tabs-1" class="ui-tabs-panel ui-widget-content ui-corner-bottom">
            
    <div class="box righted">
        <div class="header">
            <h4>
                <asp:Literal ID="ltrinserttitle" Text="ارسال تصویر جدید" runat="server"></asp:Literal></h4>
        </div>
        <div class="content">
            
            <fieldset>
            
                <div class="rowelement withpadding">
                <cc1:MessageBox ID="MessageBox1" runat="server" MessageType="Error" Visible="false">
            </cc1:MessageBox>
                </div>

                 
                 <div runat="server" id="oldpic"  visible="false">
                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            تصویر </label>
                    </div>
                    <div class="span2 right">
                        <asp:Image ID="oldimg" runat="server" CssClass="edtimg"  />
                    </div>
                    <div class="clear">
                    </div>
                </div>
                
                <div class="separator">
                </div> </div>


                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            عنوان
                        </label>
                    </div>
                    <div class="span2 right">
                        <asp:TextBox ID="txtTitle" runat="server" Width="160px" MaxLength="100"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>  
                 
                 
                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            تصویر </label>
                    </div>
                    <div class="span2 right">
                        <asp:FileUpload ID="FileUpload1" runat="server"  Width="160px" AllowMultiple="true" />
                        
                    </div>
                    <div class="clear">
                    </div>
                </div>
                
                <div class="separator">
                </div> 


                <div class="rowelement">
                    <div class="span1 right">
                        <label>تغییر نام فایل
                             </label>
                    </div>
                    <div class="span2 right">
                        <asp:CheckBox ID="chbChangeName" Checked="true" runat="server" Text=" فایل عکس رو تغییر نام بده" />
                        
                        <p class="help" data-tip="فایل عکس را همنام با آدرس محصول میکند + 8 کاراکتر رندم در انتهای آن">
                        </p>
                        
                    </div>
                    <div class="clear">
                    </div>
                </div>
                
                <div class="separator">
                </div> 


                <div class="rowelement">
                    <div class="span2 right">
                    </div>
                    <div class="span6 right">
                        <asp:Button ID="Button1" runat="server" Text="ذخـــیره" CssClass="btn btn-success"
                            OnClick="Button1_Click" />
                            <div runat="server" id="edt_btns" Visible="false">
                        <asp:Button ID="Button2" runat="server" Text="ذخـــیره" CssClass="btn btn-success"
                            OnClick="Button2_Click"  />
                        <a href='<%=Request.RawUrl %>' class="btn btn-warning">انصراف</a>    
                        <asp:Literal ID="ltrID" Visible="false" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </fieldset>


 



        </div>
    </div>
    
<div class="box lefted">
<div class="header">
<h4> لیست تصاویر
    <asp:Literal ID="ltrtitle" runat="server"></asp:Literal></h4>
</div>
<div class="conten">
 <asp:ListView ID="ListView1" runat="server" DataKeyNames="ID" DataSourceID="sds_images"
                    OnItemCommand="ListView1_ItemCommand"  >
                    <ItemTemplate>
                        <div class='imagelist slider'>
                            <a href='<%# Eval("filename","/content/productpic/gallery/{0}") %>' target="_blank">
                                    <img src='<%# Eval("filename","/HPicturer.ashx?img=~/content/productpic/gallery/{0}&w=105&h=110") %>' alt='<%# Eval("Title") %>' />
                                    </a>
                            <div>
                                <asp:LinkButton ID="LinkButton1" 
                                    runat="server" ToolTip="ویرایش تصویر" CommandArgument='<%# Eval("id") %>' CommandName="edt"
                                    ><i class="icon-edit"></i></asp:LinkButton>

                                <asp:LinkButton ID="LinkButton2" OnClientClick="return confirm('از حذف تصویر اطمینان دارید؟');"
                                    runat="server" ToolTip="حذف تصویر" CommandArgument='<%# Eval("id") %>' CommandName="del"
                                    ><i class="icon-remove"></i></asp:LinkButton>



                            </div>
                        </div>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <div class="clr">
                        </div>
                        <div id="itemPlaceholderContainer" runat="server" style="">
                            <span id="itemPlaceholder" runat="server" />
                        </div>
                        
                    </LayoutTemplate>
                    <EmptyDataTemplate>
                        <p class="empty">
                            هیچ تصویری برای این محصول وجود ندارد</p>
                    </EmptyDataTemplate>
                </asp:ListView>
                <asp:SqlDataSource ID="sds_images" runat="server" ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>"
                    SelectCommand="SELECT * FROM [ProductGallery] WHERE ([productid] = @AdvID)">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="AdvID" QueryStringField="id" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                
    <div class="clear"></div>
</div></div>

            
        <div class="clear"></div>

        </div>
         
    </div>

</asp:Content>
