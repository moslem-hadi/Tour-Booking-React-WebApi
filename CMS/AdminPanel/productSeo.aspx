<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="productSeo.aspx.cs" Inherits="CMS.Manage.productSeo" ValidateRequest="false" EnableEventValidation="false" %>

<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>
<%@ Register Assembly="CuteEditor" Namespace="CuteEditor" TagPrefix="CE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <title>تنظیمات سئو محصول</title>
    <script src="../component/select2/select2-4.0.4/dist/js/select2.min.js"></script>
    <link href="../component/select2/select2-4.0.4/dist/css/select2.min.css" rel="stylesheet" />
    <script>
        $(document).ready(function () {

            $("#" + "<%=lsbKeywords.ClientID%>").select2({
                placeholder: "تگ را انتخاب یا تایپ کنید",
                allowClear: true,
                language: "fa",
                tags: true,
                tokenSeparators: [',', '،', '-'],
                dir: "rtl"
            });

        });
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
            <li class="ui-state-default ui-corner-top"><a href="editproduct.aspx?id=<%=Request.QueryString["id"] %>">ویرایش محصول</a></li>
            <li class="ui-state-default ui-corner-top ui-tabs-selected ui-state-active"><a href="productSeo.aspx?id=<%=Request.QueryString["id"] %>">تنظیمات سئو</a></li>
            <li class="ui-state-default ui-corner-top"><a href="productimages.aspx?id=<%=Request.QueryString["id"] %>">گالری تصاویر</a></li>
            <li class="ui-state-default ui-corner-top"><a href="productdiscount.aspx?id=<%=Request.QueryString["id"] %>">تخفیف‌ها</a></li>
            <li class="ui-state-default ui-corner-top"><a href="prices.aspx?id=<%=Request.QueryString["id"] %>">قیمت روزها</a></li>
            <li class="ui-state-default ui-corner-top"><a href="detail.aspx?id=<%=Request.QueryString["id"] %>">جزئیات محصول</a></li>

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
                                    درآدرس</label>
                            </div>
                            <div class="span6 right">
                                <asp:TextBox ID="txtShort" ClientIDMode="Static" runat="server" Width="410px" MaxLength="300" CssClass="ltr" onkeyup="nospaces(this)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="val" ControlToValidate="txtShort"
                                    runat="server" ErrorMessage="×" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>

                            </div>
                            <div class="clear"></div>
                              </div>
                            <div class="separator">
                            </div>
                            
                        <div class="rowelement">
                            <div class="span2 right">
                                <label>
                                    عنوان سئو
                                </label>
                            </div>
                            <div class="span6 right">
                                <asp:TextBox ID="txtSeoTitle" ClientIDMode="Static" runat="server" Width="410px" MaxLength="100" CssClass="req"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="val" ControlToValidate="txtSeoTitle"
                                    runat="server" ErrorMessage="×" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                <span id="ltrseotltchar" style="background: #FF5C8A; border-radius: 7px; opacity: 0.4; padding: 0 4px; color: #fff; text-shadow: 1px 1px #444;">0</span>

                                <script>
                                    $('#txtSeoTitle').keyup(function () {
                                        var cs = $(this).val().length;
                                        $('#ltrseotltchar').text(cs);
                                        if (cs >= 0 && cs < 10)
                                            $('#ltrseotltchar').css("background-color", "#FD72AA");
                                        if (cs > 10 && cs < 25)
                                            $('#ltrseotltchar').css("background-color", "#BA72FD");
                                        if (cs > 25 && cs < 45)
                                            $('#ltrseotltchar').css("background-color", "#7772FD");
                                        if (cs > 45 && cs < 55)
                                            $('#ltrseotltchar').css("background-color", "#BA72FD");
                                        if (cs > 55 && cs < 70)
                                            $('#ltrseotltchar').css("background-color", "#FD72AA");
                                        if (cs > 70)
                                            $('#ltrseotltchar').css("background-color", "red");

                                    });
                                </script>

                            </div>
                            <div class="clear"></div>
                              </div>
                            <div class="separator">
                            </div>
                            
                        <div class="rowelement">



                            <div class="span2 right">
                                <label>
                                    توضیحات سئو
                                </label>
                            </div>
                            <div class="span6 right">
                                <asp:TextBox ID="txtgoogledesc" ClientIDMode="Static" runat="server" Width="410px" MaxLength="200"></asp:TextBox>
                                <span id="characters" style="background: #EC72FD; border-radius: 7px; opacity: 0.4; padding: 0 4px; color: #fff; text-shadow: 1px 1px #444;">0</span>

                            </div>
                            <div class="clear"></div>
                            <script>
                                $('#txtgoogledesc').keyup(function () {
                                    var cs = $(this).val().length;
                                    $('#characters').text(cs);


                                    if (cs >= 0 && cs < 50)
                                        $('#characters').css("background-color", "#EC72FD");

                                    if (cs > 50 && cs < 80)
                                        $('#characters').css("background-color", "#BA72FD");

                                    if (cs > 80 && cs < 120)
                                        $('#characters').css("background-color", "#7772FD");
                                    if (cs > 120 && cs < 140)
                                        $('#characters').css("background-color", "#EC72FD");
                                    if (cs > 140 && cs < 160)
                                        $('#characters').css("background-color", "#FD72AA");
                                    if (cs > 160)
                                        $('#characters').css("background-color", "red");
                                });
                            </script>
                               </div>
                            <div class="separator">
                            </div>
                            
                        <div class="rowelement">
                             
                            <div class="span2 right">
                                <label>
                                    تگ ها</label>
                            </div>
                            <div class="span6 right">


                                <asp:ListBox ID="lsbKeywords" runat="server" ClientIDMode="Static" Width="420px" SelectionMode="Multiple"></asp:ListBox>


                                <p class="help" data-tip="کلمات کلیدی مهمی که فکر میکنید کاربر برای جستجوی محصول آن را در موتور جستجو وارد می کند.">
                                </p>
                            </div>
                            <div class="clear"></div>
                        

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
</asp:Content>
