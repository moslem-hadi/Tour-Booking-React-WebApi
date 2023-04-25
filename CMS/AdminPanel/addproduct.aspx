<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="addproduct.aspx.cs" Inherits="CMS.Manage.addproduct" ValidateRequest="false" %>

<%@ Register Assembly="CuteEditor" Namespace="CuteEditor" TagPrefix="CE" %>
 

<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <title>افزودن محصول</title>
    <script src="/component/select2/select2-4.0.4/dist/js/select2.min.js"></script>
    <link href="/component/select2/select2-4.0.4/dist/css/select2.min.css" rel="stylesheet" />
    <script>
        $(document).ready(function () {
            $("#" + "<%=ddlgroup.ClientID%>").select2({
                placeholder: "شهر را انتخاب کنید",
                allowClear: true,
                language: "fa",
                dir: "rtl"
            });

        });


    </script>

    <script>


        function checkfile() {

            if (document.getElementById('<%= ddlgroup.ClientID %>').value == "-1") {
                document.getElementById("grpSpan").className = "filereq";
                window.scroll(0, 0);
                return false;
            }
            else {
                document.getElementById("grpSpan").className = "reqhidden";
                return true;
            }



        }
    </script>
    <script src="js/urlcheck.js" type="text/javascript"></script>
    <style>
        .span1 {
            width: 120px;
        }

            .span1 + .span3 {
                width: 190px;
                margin-right: 0;
            }

            .span1 + .span6 {
                margin-right: 0;
                width: 530px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="cont tabs padding-reset ui-tabs ui-widget ui-widget-content ui-corner-all">
        <ul class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
            <li class="ui-state-default ui-corner-top ui-tabs-selected ui-state-active"><a href="addproduct.aspx">افزودن محصول</a></li>

            <li class="ui-state-default ui-corner-top disabled"><a href="#">تنظیمات سئو</a></li>
            <li class="ui-state-default ui-corner-top disabled"><a href="#">گالری تصاویر</a></li>
            <li class="ui-state-default ui-corner-top"><a href="#">تخفیف‌ها</a></li>
            <li class="ui-state-default ui-corner-top"><a href="#">قیمت روزها</a></li>
            <li class="ui-state-default ui-corner-top disabled"><a href="#">جزئیات محصول</a></li>

        </ul>
        <div id="tabs-1" class="ui-tabs-panel ui-widget-content ui-corner-bottom" style="padding: 10px 0">

            <fieldset>
                <div class="rowelement withpadding">
                    <cc1:MessageBox ID="MessageBox1" runat="server" MessageType="Error" Visible="false">
                    </cc1:MessageBox>
                </div>



                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            شهر
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:DropDownList ID="ddlgroup" ClientIDMode="Static" runat="server" Width="427px" ValidationGroup="val">
                            <asp:ListItem Value="-1">شهر را انتخاب کنید</asp:ListItem>
                        </asp:DropDownList>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="-1" ValidationGroup="val" ControlToValidate="ddlgroup"
                            runat="server" ErrorMessage="×" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>

                        <span id="grpSpan" class='reqhidden' title='فیلد الزامی'><i class='icon-exclamation-sign icon-white'></i></span>
                        <asp:Literal ID="ltr_noGroup" Text="<div class='spacer'></div>" runat="server"></asp:Literal>


                    </div>
                    <div class="clear">
                    </div>
                </div>



                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            عنوان  
                        </label>
                    </div>
                    <div class="span6 right">

                        <asp:TextBox ID="txtTitle" ClientIDMode="Static" required="required" onchange="setshort(this.value)" runat="server" MaxLength="100" CssClass="req" Width="410px"></asp:TextBox>
                        <asp:RequiredFieldValidator EnableClientScript="true" ID="RequiredFieldValidator1" ValidationGroup="val" ControlToValidate="txtTitle"
                            runat="server" ErrorMessage="×" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>


                        <span id="tltcharacters" style="background: #FF5C8A; border-radius: 7px; opacity: 0.4; padding: 0 4px; color: #fff; text-shadow: 1px 1px #444;">0</span>

                        <script>

                            function setshort(val) {
                                document.getElementById('txtSeoTitle').value = val;
                            }
                            $('#txtTitle').keyup(function () {
                                var cs = $(this).val().length;
                                $('#tltcharacters').text(cs);
                                if (cs >= 0 && cs < 10)
                                    $('#tltcharacters').css("background-color", "#FD72AA");
                                if (cs > 10 && cs < 25)
                                    $('#tltcharacters').css("background-color", "#BA72FD");
                                if (cs > 25 && cs < 45)
                                    $('#tltcharacters').css("background-color", "#7772FD");
                                if (cs > 45 && cs < 55)
                                    $('#tltcharacters').css("background-color", "#BA72FD");
                                if (cs > 55 && cs < 70)
                                    $('#tltcharacters').css("background-color", "#FD72AA");
                                if (cs > 70)
                                    $('#tltcharacters').css("background-color", "red");

                            });
                        </script>

                    </div>
                    <div class="clear">
                    </div>
                </div>

                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            توضیحات کوتاه  
                        </label>
                    </div>
                    <div class="span6 right">

                        <asp:TextBox ID="txtDesc" ClientIDMode="Static" required="required" runat="server" TextMode="MultiLine"
                            CssClass="req" Width="410px"></asp:TextBox>

                    </div>
                    <div class="clear">
                    </div>
                </div>


                <div class="separator">
                </div>


                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            متن  توضیحات</label>
                    </div>

                    <div class="clear">
                    </div>
                </div>



                <div class="cute">
                   
                <CE:Editor   runat="server"
                    id="ceText" themetype="Office2003" 
                        editorwysiwygmodecss="~/adminpanel/css/WysiwygCss.css" height="400px" width="845px"
                        configurationpath="~/CuteSoft_Client/CuteEditor/Configuration/AutoConfigure/miniFull.config"
                    ></CE:Editor>
                  </div>

                <div class="separator">
                </div>
                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            قیمت پایه  
                        </label>
                    </div>
                    <div class="span3 right">
                        <asp:TextBox ID="txtPrice" ClientIDMode="Static" autocomplete="off"
                            onkeyup="convert(this.value,'persian-price')"
                            onkeypress="return validate(event)" Text="0" CssClass="ltr req txtprice" runat="server" Width="100" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="val" ControlToValidate="txtPrice"
                            runat="server" ErrorMessage="×" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>

                    </div>

                    <div class="span1 right">
                        <label>
                            وضعیت
                        </label>
                    </div>
                    <div class="span3 right">

                        <asp:CheckBox ID="chbActive" Checked="true" runat="server" ClientIDMode="Static" Text="فعال باشد " />


                    </div>
                    <div class="clear"></div>

                    <div class="span1 right">
                    </div>
                    <div class="span6 right">
                        <span id="persian-price"></span>
                    </div>

                    <div class="clear">
                    </div>
                </div>

                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            تخفیف هتل  
                        </label>
                    </div>
                    <div class="span3 right">
                        <asp:TextBox ID="txtHotelOff" ClientIDMode="Static" autocomplete="off"
                            onkeypress="return validate(event)" Text="0"
                            onkeyup="calcHotelPrice()"
                            CssClass="ltr req txtdarsad" runat="server" Width="50" MaxLength="2"></asp:TextBox>
                        <span id="hotelrice"></span>

                    </div>
                    <div class="span1 right">
                        <label>
                            تخفیف آژانس  
                        </label>
                    </div>
                    <div class="span3 right">
                        <asp:TextBox ID="txtAgancyOff" ClientIDMode="Static" autocomplete="off"
                            onkeypress="return validate(event)" Text="0"
                            onkeyup="calcAgancyPrice()"
                            CssClass="ltr req txtdarsad" runat="server" Width="50" MaxLength="2"></asp:TextBox>
                        <span id="AgancyPrice"></span>

                    </div>
                    <div class="clear">
                    </div>
                </div>

                <div class="separator">
                </div>

                <% if (productType == CMS.ProductType.Transfer)
                    { %>
                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            نوع همراهی  
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:DropDownList ID="ddlHamrahiType" ClientIDMode="Static"
                            DataTextField="Title" DataValueField="Value" DataSourceID="sdsHamrahiType"
                            runat="server" Width="150px" ValidationGroup="val">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsHamrahiType" runat="server"
                            ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>"
                            SelectCommand="GetGlobalValuesByType" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:Parameter Name="type" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>


                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            نوع خودرو  
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:DropDownList ID="ddlVehicleType" ClientIDMode="Static"
                            DataTextField="Title" DataValueField="Value" DataSourceID="sdsVehicleType"
                            runat="server" Width="150px" ValidationGroup="val">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsVehicleType" runat="server"
                            ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>"
                            SelectCommand="GetGlobalValuesByType" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:Parameter Name="type" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>


                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            محل  
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:DropDownList ID="ddlPlace" ClientIDMode="Static"
                            DataTextField="Title" DataValueField="Value" DataSourceID="sdsPlaceType"
                            runat="server" Width="150px" ValidationGroup="val">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsPlaceType" runat="server"
                            ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>"
                            SelectCommand="GetGlobalValuesByType" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:Parameter Name="type" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>


                    </div>
                    <div class="clear">
                    </div>
                </div>


                <div class="separator">
                </div>

                <%} %>

                <% if (productType == CMS.ProductType.Gasht)
                    { %>
                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            نوع  
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:DropDownList ID="ddlActivityType" ClientIDMode="Static"
                            DataTextField="Title" DataValueField="Value" DataSourceID="sdsActivityType"
                            runat="server" Width="150px" ValidationGroup="val">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsActivityType" runat="server"
                            ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>"
                            SelectCommand="GetGlobalValuesByType" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:Parameter Name="type" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>


                    </div>
                    <div class="clear">
                    </div>
                </div>


                <div class="separator">
                </div>

                <%} %>


                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            تصویر کوچک</label>
                    </div>
                    <div class="span6 right">
                        <asp:FileUpload ID="fulMainPic" runat="server" Width="210px" />
                        <div class="text-red">
                            اندازه <b>180×300 پیکسل</b> می شود.<br />
                            نام عکس همان آدرس محصول می شود
                        </div>
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
                        <asp:Button ID="Button1" runat="server" Text="ذخـــیره" OnClientClick="return checkfile();"
                            CssClass="btn btn-success" OnClick="Button1_Click" ValidationGroup="val" />
                        <a href="productlist.aspx" class="btn btn-warning">انصراف</a>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </fieldset>



            <div class="clear"></div>

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
                calcHotelPrice();
                calcAgancyPrice();
            }
        }

        function convert2(y) {
            if (y > 999999999 && y < 1000000000000) { bghb = (y % 1000000000); temp = y - bghb; bil = temp / 1000000000; convert3r(bil); result = result + " ميليارد"; if (bghb != 0) { result = result + " و "; convert2(bghb); } }
            else if (y > 999999 && y <= 999999999) { bghm = (y % 1000000); temp = y - bghm; mil = temp / 1000000; convert3r(mil); result = result + " ميليون"; if (bghm != 0) { result = result + " و "; convert2(bghm); } }
            else if (y > 999 && y <= 999999) { bghh = (y % 1000); temp = y - bghh; hez = temp / 1000; convert3r(hez); result = result + " هزار"; if (bghh != 0) { result = result + " و "; convert2(bghh); } }
            else if (y <= 999) convert3r(y); else result = "Error";
        }

        function convert3r(x) {
            bgh = (x % 100); temp = x - bgh; sad = temp / 100;
            if (bgh == 0) { result = result + s4[sad] }
            else {
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
        function calcHotelPrice() {
            var mainPrice = parseInt($("#txtPrice").val());
            var hotelOff = parseInt($("#txtHotelOff").val());
            var price = mainPrice - (mainPrice * parseInt(hotelOff) / 100);
            if (!isNaN(price) && price != mainPrice)
                $("#hotelrice").html("<br>پرداختی: " + addCommas(price) + ' <small>تومان</small>');
            else
                $("#hotelrice").html("");
        }
        function calcAgancyPrice() {
            var mainPrice = parseInt($("#txtPrice").val());
            var agancyOff = parseInt($("#txtAgancyOff").val());
            var price = mainPrice - (mainPrice * parseInt(agancyOff) / 100);
            if (!isNaN(price) && price != mainPrice)
                $("#AgancyPrice").html("<br>پرداختی: " + addCommas(price) + ' <small>تومان</small>');
            else
                $("#AgancyPrice").html("");
        }
    </script>
</asp:Content>
