<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="productlist.aspx.cs" Inherits="CMS.Manage.productlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <title>لیست محصولات</title>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="box">
        <div class="header">
            <h4>لیست محصولات
    
            </h4>
        </div>
        <div class="conten">

            <div class="srch">
                <asp:Panel ID="Panel1" CssClass="con" runat="server" DefaultButton="LinkButton2" Style="width: 560px">

                    <asp:TextBox ID="TextBox1" runat="server" placeholder="جستجو در عنوان و کد محصول" Width="170px" MaxLength="200"></asp:TextBox>
                    <div class="ui-widget" style="text-align: left">
                        <asp:TextBox ID="txtgroup" runat="server" placeholder="شهر" Width="60px" CssClass="textboxAuto" Font-Size="12px" />
                    </div>
                    <div class="ui-widget" style="text-align: left">
                        <asp:DropDownList ID="ddlType" ClientIDMode="Static"  Width="60px" CssClass="slc" 
                            DataTextField="Title" DataValueField="Value"  
                            runat="server"  ValidationGroup="val">
                        </asp:DropDownList> 
                    </div>
                    <asp:DropDownList ID="ddlSort" runat="server" Width="70" CssClass="slc">
                        <asp:ListItem Value="ID">تاریخ درج</asp:ListItem>
                        <asp:ListItem Value="Price">قیمت</asp:ListItem>
                        <asp:ListItem Value="ViewCount">بازدید</asp:ListItem>
                        <asp:ListItem Value="Title">عنوان</asp:ListItem>
                        <asp:ListItem Value="Priority">اولویت</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlDir" runat="server" Width="50" CssClass="slc">
                        <asp:ListItem Value="desc">نزولی</asp:ListItem>
                        <asp:ListItem Value="asc">صعودی</asp:ListItem>
                    </asp:DropDownList>

                    <asp:LinkButton CssClass="srchbtn" ID="LinkButton2" runat="server"
                        OnClick="LinkButton2_Click"><i class="icon-search"></i></asp:LinkButton>
                    <br />

                </asp:Panel>



                <a href="addproduct.aspx" class="btn btn-success"><i class="icon-plus"></i>افزودن محصول</a>


            </div>
            <div class="clear"></div>
            <hr class="mb10 mt10" />


            <div class="recordCount <%=dpItems.TotalRowCount==0 ? "hidden" : "" %>">تعداد: <%=dpItems.TotalRowCount %> محصول</div>

            <nav class="<%=dpItems2.TotalRowCount > dpItems2.PageSize ? "pager ltr" : "hidden"%>">
                <asp:DataPager ID="dpItems2" runat="server" PagedControlID="lvwItems" PageSize="30" QueryStringField="page">
                    <Fields>

                        <asp:NextPreviousPagerField ShowFirstPageButton="false" ShowNextPageButton="false" PreviousPageText="" FirstPageText="" ShowPreviousPageButton="true"
                            ButtonCssClass="pnext" />

                        <asp:NumericPagerField NumericButtonCssClass="" CurrentPageLabelCssClass="pcur" />

                        <asp:NextPreviousPagerField ButtonType="Link" ShowLastPageButton="false" ShowNextPageButton="true" NextPageText="" ShowPreviousPageButton="false" LastPageText=""
                            ButtonCssClass="pback" />


                    </Fields>

                </asp:DataPager>
            </nav>




                <asp:ListView ID="lvwItems" runat="server" OnPagePropertiesChanging="ListView1_PagePropertiesChanging" DataSourceID="sdsPageList"
                    ItemPlaceholderID="PlaceHolder">
                    <LayoutTemplate>
                        
            <table class="lst table normal margin-reset">
                <tr>
                    <td class="span5">عنوان محصول</td>
                    <td class="span2">شهر</td>
                    <td class="span1">قیمت</td>
                    <td class="span1">اختیارات</td>
                </tr>
                        <asp:PlaceHolder ID="PlaceHolder" runat="server"></asp:PlaceHolder>
                

            </table>
                    </LayoutTemplate>
                    <EmptyDataTemplate>
                                <p class="empty"><br /><br />جستجوی شما نتیجه ای نداشت!<br /><br /></p>
                    </EmptyDataTemplate>

                    <ItemTemplate>


                        <tr id="<%# Eval("id") %>">
                            <td class="span5 " style="text-align: right">
                                <img src='<%# Eval("Pic","/HPicturer.ashx?img=~/content/productpic/{0}&w=50&h=50") %>'
                                    alt='<%# Eval("Title") %>' title='<%# Eval("Title") %>' class="mngpic" />
                                <a href='<%#"http://parhantransfer.ir/view/"+CMS.CommonFunctions.ReplaceSpace(Eval("slug")) %>' title='<%# Eval("PrivateDesc") %>'>
                                    <%# Eval("Title") %></a>
                                <span class="code">کد: <%# Eval("id") %></span>


                            </td>
                            <td class="span2">
                                <a style="font-size: 11px" href='<%# Eval("GroupID","productlist.aspx?id={0}")%>'>
                                    <%# Eval("GroupTitle")%></a>

                            </td>
                            <td class="span1">
                                <span><%# CMS.CommonFunctions.SetCamaHezar(Eval("price")) %></span>

                                <small style="font-size: 11px; display: block; color: #808080">هزار تومان</small>

                            </td>
                            <td class="span1">
                                <div class="center">




                                    <a href='<%# Eval("ID","editproduct.aspx?id={0}&retp=")+PageNum %>'
                                        title="ویرایش محصول ">
                                        <i class="icon-edit"></i></a>



                                    <a href='<%# Eval("ID","productseo.aspx?id={0}") %>'
                                        title="تنظیمات سئو ">
                                        <i class="icon-cog"></i></a>

                                    <a href='<%# Eval("ID","productimages.aspx?id={0}") %>'
                                        title="گالری محصول ">

                                        <i class="icon-picture"></i></a>


                                    <a href='<%# Eval("ID","prices.aspx?id={0}") %>'
                                        title="قیمت ها ">
                                        <i class="icon-list"></i></a>

                                    <a href='<%# Eval("ID","detail.aspx?id={0}") %>'
                                        title="مشاهده اطلاعات ">
                                        <i class="icon-eye-open"></i></a>


                                </div>
                            </td>
                        </tr>




                    </ItemTemplate>
                </asp:ListView>



            <div class="clear"></div>

            <div class="recordCount <%=dpItems.TotalRowCount==0 ? "hidden" : "" %>">تعداد: <%=dpItems.TotalRowCount %> محصول</div>

            <nav class="<%=dpItems.TotalRowCount > dpItems.PageSize ? "pager ltr" : "hidden"%>">
                <asp:DataPager ID="dpItems" runat="server" PagedControlID="lvwItems" PageSize="30" QueryStringField="page">
                    <Fields>


                        <asp:NextPreviousPagerField ShowFirstPageButton="false" ShowNextPageButton="false" PreviousPageText="" FirstPageText="" ShowPreviousPageButton="true"
                            ButtonCssClass="pnext" />

                        <asp:NumericPagerField NumericButtonCssClass="" CurrentPageLabelCssClass="pcur" />

                        <asp:NextPreviousPagerField ButtonType="Link" ShowLastPageButton="false" ShowNextPageButton="true" NextPageText="" ShowPreviousPageButton="false" LastPageText=""
                            ButtonCssClass="pback" />

                    </Fields>

                </asp:DataPager>
            </nav>




            <div class="clear"></div>



            <asp:SqlDataSource ID="sdsPageList" runat="server"
                ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>"
                SelectCommand="productList_manage" SelectCommandType="StoredProcedure">

                <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="n" Name="key" QueryStringField="key" Type="String" />
                    <asp:QueryStringParameter DefaultValue="-1" Name="GroupID" QueryStringField="id" Type="Int32" />
                    <asp:QueryStringParameter DefaultValue="-1" Name="type" QueryStringField="type" Type="Int32" />
                    <asp:QueryStringParameter DefaultValue="id" Name="sort" QueryStringField="sort" Type="String" />
                    <asp:QueryStringParameter DefaultValue="desc" Name="dir" QueryStringField="dir" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>


        </div>
    </div>



    <link rel="stylesheet" href="css/jquery-ui.css">
    <script src="/js/jquery-ui.js"></script>

    <script language="javascript" type="text/javascript">
        $(function () {
            $('#<%=txtgroup.ClientID%>').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "productlist.aspx/getGroups",
                        data: "{'pre' :'" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {

                                return {
                                    Title: item.Title,
                                    ID: item.ID,
                                    json: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                },
                focus: function (event, ui) {
                    // this is for when focus of autocomplete item 
                    $('#<%=txtgroup.ClientID%>').val(ui.item.ID);
                    return false;
                },
                select: function (event, ui) {
                    // this is for when select autocomplete item
                    $('#<%=txtgroup.ClientID%>').val(ui.item.ID);
                    return false;
                }
            }).data("ui-autocomplete")._renderItem = function (ul, item) {
                // here return item for autocomplete text box, Here is the place 
                // where we can modify data as we want to show as autocomplete item
                return $("<li>")
                    .append("<a  href='#' >" + item.Title + "</a>").appendTo(ul);
            };


        });
    </script>

</asp:Content>
