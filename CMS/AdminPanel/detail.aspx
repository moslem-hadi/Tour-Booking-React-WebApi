<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="detail.aspx.cs" Inherits="CMS.Manage.detail" %>

<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <title>نمایش اطلاعات  محصول</title>
    <style>
        p {
            font: normal 13px tahoma;
            margin: 0;
            padding: 0;
        }
    </style>
    <script src="/js/openwindow.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="cont tabs padding-reset ui-tabs ui-widget ui-widget-content ui-corner-all">
        <ul class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
            <li class="ui-state-default ui-corner-top"><a href="editproduct.aspx?id=<%=Request.QueryString["id"] %>">ویرایش محصول</a></li>
            <li class="ui-state-default ui-corner-top"><a href="productSeo.aspx?id=<%=Request.QueryString["id"] %>">تنظیمات سئو</a></li>
            <li class="ui-state-default ui-corner-top"><a href="productimages.aspx?id=<%=Request.QueryString["id"] %>">گالری تصاویر</a></li>
            
            <%--<li class="ui-state-default ui-corner-top"><a href="relatedproduct.aspx?id=<%=Request.QueryString["id"] %>">محصولات مرتبط</a></li> --%>
            <li class="ui-state-default ui-corner-top"><a href="productdiscount.aspx?id=<%=Request.QueryString["id"] %>">تخفیف‌ها</a></li>
            <li class="ui-state-default ui-corner-top"><a href="prices.aspx?id=<%=Request.QueryString["id"] %>">قیمت روزها</a></li> 
            <li class="ui-state-default ui-corner-top ui-tabs-selected ui-state-active"><a href="detail.aspx?id=<%=Request.QueryString["id"] %>">جزئیات محصول</a></li> 

        </ul>
        <div id="tabs-1" class="ui-tabs-panel ui-widget-content ui-corner-bottom" style="padding:10px 0">
          <fieldset>
                <div class="rowelement withpadding">
                    <cc1:MessageBox ID="MessageBox1" runat="server" MessageType="Error" Visible="false">
                    </cc1:MessageBox>
                </div>

                

                 


                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            عنوان  محصول
                        </label>
                    </div>
                    <div class="span6 right">
                        <div  style="float:left">
                         
                        <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Red" OnClientClick="return confirm('از حذف این کالا اطمینان دارید؟');" OnClick="LinkButton1_Click"> حذف</asp:LinkButton>
                     
                        </div>
                        <p>
                            <asp:Literal ID="ltrtitle" runat="server"></asp:Literal><br />
                            <asp:Literal ID="ltrsubtitle" runat="server"></asp:Literal>

                        </p>

                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>





                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            شهر
                        </label>
                    </div>
                    <div class="span6 right">
                        <p>
                            <asp:Literal ID="ltrGroup" Text="" runat="server"></asp:Literal>
                        </p>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>
                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            تاریخ درج
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:Literal ID="ltrRegDate" runat="server"></asp:Literal>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                        آخرین ویرایش:
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Literal ID="ltrlastedit" runat="server"></asp:Literal>

                    </div>
                    <div class="clear">
                    </div>


                       <div class="span2 right">
                         
                    </div>
                    <div class="span6 right">
                       کاربر:  <asp:Literal ID="ltrinserUserID" runat="server"></asp:Literal>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        کاربر:   <asp:Literal ID="ltrupdateUserID" runat="server"></asp:Literal>

                    </div>
                    <div class="clear">
                    </div>



                </div>
                <div class="separator">
                </div>




                <div class="rowelement">
                    <div class="span8 right">
                        <label>
                            تعداد بازدید
                        </label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Literal ID="ltrViewcount" runat="server"></asp:Literal>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                      
                         
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>





                <div class="rowelement">
                    <div class="span8 right">
                        <label>
                            هزینه
                        </label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Literal ID="ltrPrice" runat="server"></asp:Literal>
                        
                         

                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>  
                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            تگ ها</label>
                    </div>
                    <div class="span6 right">
                        <p>
                            <asp:Literal ID="ltrKeywords" runat="server"></asp:Literal></p>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>
                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            تصاویر</label>
                    </div>
                    <div class="span6 right">
                        <div class='imagelist slider'>
                            <asp:HyperLink ID="hplimg" runat="server">
                                <asp:Image ID="selectedimg" runat="server" ClientIDMode="Static" ImageUrl="images/nopic.png"
                                    AlternateText="" />
                            </asp:HyperLink>
                        </div>


                        <asp:ListView ID="ListView1" runat="server" DataKeyNames="ID" DataSourceID="sds_images">
                            <ItemTemplate>
                                <div class='imagelist slider'>

                                    <a href='<%# Eval("filename","/content/productpic/gallery/{0}") %>'>
                                        <img src='<%# Eval("filename","/HPicturer.ashx?img=~/content/productpic/gallery/{0}&w=105&h=110") %>' alt='<%# Eval("Title") %>' />
                                    </a>

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
                                    این  محصول تصویر گالری ندارد
                                </p>
                            </EmptyDataTemplate>
                        </asp:ListView>
                        <asp:SqlDataSource ID="sds_images" runat="server" ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>"
                            SelectCommand="SELECT * FROM [ProductGallery] WHERE ([productid] = @AdvID)">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="AdvID" QueryStringField="id" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>





                    </div>
                    <div class="clear">
                    </div>
                </div>



                <div class="separator">
                </div>


                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            وضعیت
                        </label>
                    </div>
                    <div class="span6 right">

                        <asp:Literal ID="ltrStat" runat="server"></asp:Literal>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                    <div class="clear">
                    </div>

            </fieldset>

           <div class="clear"></div>

        </div>
         
    </div>

</asp:Content>
