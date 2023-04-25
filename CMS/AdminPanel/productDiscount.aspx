<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/managemaster.Master" AutoEventWireup="true" CodeBehind="productDiscount.aspx.cs" Inherits="CMS.AdminPanel.productDiscount" %>

<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <title>تخفیف محصول</title>

    <link href="/component/pwt-datetimepicker/css/persian-datepicker-0.5.10.css" rel="stylesheet">

    <script src="/component/pwt-datetimepicker/js/persian-datepicker-0.5.10.min.js"></script>
    <script src="/component/pwt-datetimepicker/js/persian-date.js"></script>

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
            <li class="ui-state-default ui-corner-top ui-tabs-selected ui-state-active"><a href="productdiscount.aspx?id=<%=Request.QueryString["id"] %>">تخفیف‌ها</a></li>
            <li class="ui-state-default ui-corner-top"><a href="prices.aspx?id=<%=Request.QueryString["id"] %>">قیمت روزها</a></li>
            <li class="ui-state-default ui-corner-top"><a href="detail.aspx?id=<%=Request.QueryString["id"] %>">جزئیات محصول</a></li>

        </ul>
        <div id="tabs-1" class="ui-tabs-panel ui-widget-content ui-corner-bottom">

            <div class="box">
                <div class="header">
                    <h4>تخفیف جدید برای کاربر</h4>
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
                            <div class="span4 right">
                                <b><%=title %></b>
                            </div>
                            <div class="clear">
                            </div>
                        </div>

                        <div class="rowelement">
                            <div class="span1 right">
                                <label>
                                    قیمت  
                                </label>
                            </div>
                            <div class="span4 right">
                                <b><%=price %> <small>تومان</small></b>
                                <asp:TextBox runat="server" ID="txtPrice" style="display:none" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="clear">
                            </div>
                        </div>

                        <div class="rowelement">
                            <div class="span1 right">
                                <label>
                                    کد کاربر  
                                </label>
                            </div>
                            <div class="span4 right">
                                <asp:TextBox ID="txtUser" ClientIDMode="Static" autocomplete="off" placeholder="جستجو با نام و موبایل"
                                    CssClass="req" runat="server" Width="160" MaxLength="50"></asp:TextBox>

                            </div>

                            <div class="clear">
                            </div>
                        </div>


                        <div class="rowelement">
                            <div class="span1 right">
                                <label>
                                    تخفیف  
                                </label>
                            </div>
                            <div class="span4 right">
                                <asp:TextBox ID="txtDiscount" ClientIDMode="Static" autocomplete="off"
                                    onkeypress="return validate(event)" Text="0"
                                    onkeyup="calcPrice()"
                                    CssClass="ltr req txtdarsad" runat="server" Width="50" MaxLength="2"></asp:TextBox>
                              &nbsp;&nbsp;  <span id="discountPrice"></span>
                            </div>

                            <div class="clear">
                            </div>
                        </div>

                        <div class="rowelement">
                            <div class="span2 right">
                            </div>
                            <div class="span6 right">
                                <asp:Button ID="btnAdd" runat="server" Text="ذخـــیره" CssClass="btn btn-success"
                                    OnClick="btnAdd_Click" />

                           <%--     <asp:Button ID="btnEdit" runat="server" Text="ذخـــیره" CssClass="btn btn-success"
                                    OnClick="btnEdit_Click" Visible="false" />--%>

                                <a href="prices.aspx" class="btn btn-warning" runat="server" id="cancl" visible="false">انصراف</a>
                                <asp:Literal ID="ltrID" Visible="false" runat="server"></asp:Literal>
                            </div>
                            <div class="clear">
                            </div>
                        </div>

                        <div class="clear">
                        </div>





                    </fieldset>
                </div>
            </div>
            <br />
            <div class="box">
                <div class="header">
                    <h4>لیست تخفیف‌ها <%=  title %></h4>
                </div>
                <div class="conten">


                    <asp:SqlDataSource ID="sdsgetProductUserDiscount" runat="server" ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>"
                        SelectCommand="getProductUserDiscount" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:QueryStringParameter DbType="Int32" Name="productId" QueryStringField="id" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                    <asp:GridView CssClass="table normal margin-reset" ID="GridView1" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="ID" EnableModelValidation="True"
                        Width="100%" AllowPaging="True" GridLines="None" PageSize="80" DataSourceID="sdsgetProductUserDiscount"
                        EnableTheming="False" EnableViewState="False" OnPageIndexChanging="GridView1_PageIndexChanging"
                        ShowFooter="True"
                        OnRowCommand="GridView1_RowCommand">

                        <Columns>

                            <asp:TemplateField FooterText="کاربر" HeaderText="کاربر"
                                SortExpression="title">
                                <ItemTemplate>
                                    <%# Eval("FullName") %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="نوع کاربر" FooterText="نوع کاربر">
                                <ItemTemplate>
                                    <center>
                                 <%# CMS.CommonFunctions.UserTypeName( Eval("UserType")) %>
                          </center>
                                </ItemTemplate>

                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="150px" />
                            </asp:TemplateField>


                            <asp:TemplateField FooterText="موبایل" HeaderText="موبایل"
                                SortExpression="title">
                                <ItemTemplate>
                                    <%# Eval("Mobile") %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="درصد تخفیف" FooterText="درصد تخفیف">
                                <ItemTemplate>
                                    <center>
                                 <%#  Eval("Discount") %>
                          </center>
                                </ItemTemplate>

                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="150px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="مبلغ پرداختی" FooterText="مبلغ پرداختی">
                                <ItemTemplate>
                                    <center>
                                <%# CMS.CommonFunctions.SetCama(Eval("FinalPrice")) %> تومان
                          </center>
                                </ItemTemplate>

                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="150px" />
                            </asp:TemplateField>



                            <asp:TemplateField FooterText="تنظیمات" HeaderText="تنظیمات">
                                <ItemTemplate>
                                    <center>
                            
                            <asp:LinkButton ID="LinkButton2" CommandArgument='<%# Eval("id") %>' CommandName="edt" runat="server" ><i class="icon-edit"></i></asp:LinkButton>

<asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("id") %>' CommandName="Del" runat="server" OnClientClick="return confirm('از حذف این رکورد دارید؟ همه رکوردهای مرتبط حذف خواهند شد.');" ><i class="icon-remove"></i></asp:LinkButton>
                            
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
                        <EmptyDataTemplate>
                            <p class="empty">هیچ رکوردی وجود ندارد.</p>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
              
            <div class="clear"></div>
            <p style="font-size:90%; color:#808080;margin-top:30px;">در صورتیکه کد کاربری که قبلا برای او قیمت ثبت شده است را وارد کنید، قیمت قبلی ویرایش می شود.
                <br />در صورتیکه درصد را 0 قرار دهید، تخفیفی برای کاربر لحاظ نخواهد شد. 
                <br />اگر درصد تخفیف را خالی بگذارید و ذخیره کنید، درصد اختصاص داده شده به آن کاربر حذف می شود.

            </p>
        </div>

    </div>


    <link rel="stylesheet" href="css/jquery-ui.css">

    <script src="js/jquery-ui.js"></script>

    <script language="javascript" type="text/javascript">
        function calcPrice() {
            var mainPrice = parseInt($("#txtPrice").val());
            var hotelOff = parseInt($("#txtDiscount").val());
            console.log(mainPrice)

            console.log(hotelOff)


            var price = mainPrice - (mainPrice * parseInt(hotelOff) / 100);
            console.log(price)

            if (!isNaN(price) && price != mainPrice)
                $("#discountPrice").html("پرداختی: " + addCommas(price) + ' <small>تومان</small>');
            else
                $("#discountPrice").html("");
        }
        $(function () {

            $('#<%=txtUser.ClientID%>').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "paylist.aspx/getUser",
                        data: "{'pre' :'" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {

                                return {
                                    FullName: item.FullName,
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
                    $('#<%=txtUser.ClientID%>').val(ui.item.ID);
                    return false;
                },
                select: function (event, ui) {
                    // this is for when select autocomplete item
                    $('#<%=txtUser.ClientID%>').val(ui.item.ID);
                    return false;
                }
            }).data("ui-autocomplete")._renderItem = function (ul, item) {
                // here return item for autocomplete text box, Here is the place 
                // where we can modify data as we want to show as autocomplete item
                return $("<li>")
                    .append("<a  href='#' >" + item.FullName + "</a>").appendTo(ul);
            };



        });
    </script>

</asp:Content>
