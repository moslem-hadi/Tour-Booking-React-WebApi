<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/managemaster.Master" AutoEventWireup="true" CodeBehind="prices.aspx.cs" Inherits="CMS.AdminPanel.prices" %>

<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <title>قیمت روزهای محصول</title>

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
            <li class="ui-state-default ui-corner-top"><a href="productdiscount.aspx?id=<%=Request.QueryString["id"] %>">تخفیف‌ها</a></li>
            <li class="ui-state-default ui-corner-top ui-tabs-selected ui-state-active"><a href="prices.aspx?id=<%=Request.QueryString["id"] %>">قیمت روزها</a></li>
            <li class="ui-state-default ui-corner-top"><a href="detail.aspx?id=<%=Request.QueryString["id"] %>">جزئیات محصول</a></li>

        </ul>
        <div id="tabs-1" class="ui-tabs-panel ui-widget-content ui-corner-bottom">

            <div class="box">
                <div class="header">
                    <h4>قیمت جدید</h4>
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
                                    تاریخ
                                </label>
                            </div>
                            <div class="span6 right">
                                از &nbsp;&nbsp;&nbsp;<asp:TextBox CssClass="ltr" ID="txtStartDate" ClientIDMode="Static" runat="server" Width="80"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                         تا &nbsp;&nbsp;&nbsp;<asp:TextBox CssClass="ltr" ID="txtEndDate" ClientIDMode="Static" runat="server" Width="80"></asp:TextBox>
                            </div>
                            <div class="clear">
                            </div>
                        </div>
                        <script>

                            s1 = new Array("", "يک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه")
                            s2 = new Array("ده", "يازده", "دوازده", "سيزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده")
                            s3 = new Array("", "", "بيست", "سي", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود")
                            s4 = new Array("", "صد", "دويست", "سيصد", "چهارصد", "پانصد", "ششصد", "هفتصد", "هشتصد", "نهصد")
                            function convert(z, elname) {
                                z = parseInt(z);
                                if (z == 0) { result = "صفر" } else {
                                    result = ""
                                    convert2(z)
                                }
                                //alert("."+result) 

                                if (result == "Error") {
                                    document.getElementById(elname).innerHTML = "";
                                }
                                else {
                                    document.getElementById(elname).innerHTML = result + " تومان";

                                }
                            }

                            function convert2(y) {
                                if (y > 999999999 && y < 1000000000000)
                                { bghb = (y % 1000000000); temp = y - bghb; bil = temp / 1000000000; convert3r(bil); result = result + " ميليارد"; if (bghb != 0) { result = result + " و "; convert2(bghb); } }
                                else if (y > 999999 && y <= 999999999)
                                { bghm = (y % 1000000); temp = y - bghm; mil = temp / 1000000; convert3r(mil); result = result + " ميليون"; if (bghm != 0) { result = result + " و "; convert2(bghm); } }
                                else if (y > 999 && y <= 999999) { bghh = (y % 1000); temp = y - bghh; hez = temp / 1000; convert3r(hez); result = result + " هزار"; if (bghh != 0) { result = result + " و "; convert2(bghh); } }
                                else if (y <= 999) convert3r(y); else result = "Error";
                            }

                            function convert3r(x) {
                                bgh = (x % 100); temp = x - bgh; sad = temp / 100;
                                if (bgh == 0) { result = result + s4[sad] }
                                else
                                {
                                    if (x > 100) result = result + s4[sad] + " و ";
                                    if (bgh < 10) { result = result + s1[bgh] }
                                    else if (bgh < 20) { bgh2 = (bgh % 10); result = result + s2[bgh2] }
                                    else {
                                        bgh2 = (bgh % 10); temp = bgh - bgh2; dah = temp / 10;
                                        if (bgh2 == 0) { result = result + s3[dah] }
                                        else { result = result + s3[dah] + " و " + s1[bgh2] }
                                    }
                                }
                            }


                        </script>



                        <div class="rowelement">
                            <div class="span1 right">
                                <label>
                                    قیمت  
                                </label>
                            </div>
                            <div class="span3 right">
                                <asp:TextBox ID="txtPrice" ClientIDMode="Static" autocomplete="off"
                                    onkeyup="convert(this.value,'persian-price')"
                                    onkeypress="return validate(event)" Text="0" CssClass="ltr req txtprice" runat="server" Width="100" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="val" ControlToValidate="txtPrice"
                                    runat="server" ErrorMessage="×" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <span id="persian-price" style="display: inline-block"></span>
                            </div>

                            <div class="clear">
                            </div>
                        </div>






                        <div class="rowelement">
                            <div class="span1 right">
                                <label>
                                    وضعیت  
                                </label>
                            </div>
                            <div class="span3 right">
                                <asp:DropDownList ID="dllAvability" runat="server" Width="133px">
                                    <asp:ListItem Value="True">موجود</asp:ListItem>
                                    <asp:ListItem Value="False">موجود نیست</asp:ListItem>

                                </asp:DropDownList>
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
                                <asp:Button ID="btnAdd" runat="server" Text="ذخـــیره" CssClass="btn btn-success"
                                    OnClick="btnAdd_Click" />

                                <asp:Button ID="btnEdit" runat="server" Text="ذخـــیره" CssClass="btn btn-success"
                                    OnClick="btnAdd_Click" Visible="false" />

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
                    <h4>لیست قیمت ها</h4>
                </div>
                <div class="conten">


                    <asp:SqlDataSource ID="sdsSpecificationAttribute" runat="server" ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>"
                        SelectCommand="GetDayPrices_Manage" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:QueryStringParameter DbType="Int32" Name="id" QueryStringField="id" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                    <asp:GridView CssClass="table normal margin-reset" ID="GridView1" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="ID" EnableModelValidation="True"
                        Width="100%" AllowPaging="True" GridLines="None" PageSize="80" DataSourceID="sdsSpecificationAttribute"
                        EnableTheming="False" EnableViewState="False" OnPageIndexChanging="GridView1_PageIndexChanging"
                        ShowFooter="True" onrowdatabound="GridView1_RowDataBound"
                        OnRowCommand="GridView1_RowCommand">

                        <Columns>

                            <asp:TemplateField FooterText="روز" HeaderText="روز"
                                SortExpression="title">

                                <ItemTemplate>

                                    <%# Eval("DayFa") %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />

                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="قیمت" FooterText="قیمت">
                                <ItemTemplate>
                                    <center>
                                <%# CMS.CommonFunctions.SetCama(Eval("price")) %> تومان
                          </center>
                                </ItemTemplate>

                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="150px" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="قابل خرید" FooterText="قابل خرید">
                                <ItemTemplate>
                                    <center>
                                <img src='<%# Eval("IsAvailable","images/{0}.png") %>' />
                          </center>
                                </ItemTemplate>

                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="90px" />
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

        </div>

    </div>
    <script>

        /*
         observer
         */
        var d = new Date();
        var today = '<%= CMS.CommonFunctions.ToPersianDate(DateTime.Now)%>';
         
                d.setDate(d.getDate() - 1);

                var startDay = parseInt(d.getTime());
                d.setDate(d.getDate() + 60);

                var endDay = parseInt(d.getTime());

                $("#txtStartDate").pDatepicker({
                    format: 'YYYY/MM/DD',

                    autoClose: true,
                    inputDelay: 800,
                    navigator: {
                        enabled: true,
                        text: {
                            btnNextText: "",
                            btnPrevText: ""
                        }
                    },
                    formatter: function (unixDate) {
                        var self = this;
                        var pdate = new persianDate(unixDate);
                        pdate.formatPersian = false;
                        return pdate.format(self.format);
                    },

                    maxDate: endDay,
                    minDate: startDay

                });


                $("#txtEndDate").pDatepicker({
                    format: 'YYYY/MM/DD',

                    autoClose: true,
                    inputDelay: 800,
                    navigator: {
                        enabled: true,
                        text: {
                            btnNextText: "",
                            btnPrevText: ""
                        }
                    },
                    formatter: function (unixDate) {
                        var self = this;
                        var pdate = new persianDate(unixDate);
                        pdate.formatPersian = false;
                        return pdate.format(self.format);
                    },

                    maxDate: endDay,
                    minDate: startDay

                });

                //$("#txtStartDate").pDatepicker("setDate", [parseInt(today.split('/')[0]), parseInt(today.split('/')[1]), parseInt(today.split('/')[2]), 0, 0]);
                //$("#txtEndDate").pDatepicker("setDate", [parseInt(today.split('/')[0]), parseInt(today.split('/')[1]), parseInt(today.split('/')[2]), 0, 0]);











    </script>
</asp:Content>
