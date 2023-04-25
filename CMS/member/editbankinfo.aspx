<%@ Page  Language="C#" MasterPageFile="~/member/membermaster.Master" AutoEventWireup="true" CodeBehind="editbankinfo.aspx.cs" Inherits="CMS.member.editbankinfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>ویرایش مشخصات کاربری</title>
      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainFullContent" runat="server">



    <div class="content">
        <asp:Literal ID="ltr_error" runat="server"></asp:Literal>

        <div class="col-md-12">

            <div class="panel light-shadow white title-transparent rounded clearfix ">
                <div class="page-header">
                    <h3>
                        <span>ویرایش مشخصات بانکی</span></h3>
                </div>
                <p style="color: #FC0505;">- وارد کردن تمام اطلاعات الزامی است.<br /> </p>

                <div class="col-md-6">

                    <label class="title">نام بانک</label>

                    <asp:DropDownList ID="ddlBank" Width="231px" runat="server">
                        <asp:ListItem Value="غیره" />
                        <asp:ListItem Value="بانک ملی" />
                        <asp:ListItem Value="بانک ملت" />
                        <asp:ListItem Value="بانک سپه" />
                        <asp:ListItem Value="بانک صادرات" />
                        <asp:ListItem Value="بانک کشاورزی" />

                    </asp:DropDownList> *
                </div>

                <div class="col-md-6">

                    <label class="title">شماره حساب</label>
                    <asp:TextBox ID="txtbhesab"
                        onkeypress="return (event.charCode >= 48 && event.charCode <= 57) || (event.charCode==8 ||  event.charCode==0)"
                        Width="231px" runat="server" MaxLength="50" CssClass="ltr req ltrsp"></asp:TextBox> *<br />

                </div>

                <div class="col-md-6">

                    <label class="title">شماره کارت </label>
                    <asp:TextBox ID="txtbshomare"
                        onkeypress="return (event.charCode >= 48 && event.charCode <= 57) || (event.charCode==8 ||  event.charCode==0)"
                        Width="231px" MaxLength="16" runat="server" CssClass="ltr req ltrsp"></asp:TextBox> *<br />

                </div>

                <div class="col-md-6">

                    <label class="title">کد شبا (بدون IR) </label>
                    <asp:TextBox ID="txtbsheba"
                        onkeypress="return (event.charCode >= 48 && event.charCode <= 57) || (event.charCode==8 ||  event.charCode==0)"
                        Width="231px" MaxLength="24" runat="server" CssClass="ltr req sheba ltrsp"></asp:TextBox> *

                </div>

                <div class="col-md-6">
                    <label class="title">نام صاحب حساب </label>
                    <asp:TextBox ID="txtbsaheb" Width="231px" 
                        MaxLength="100" runat="server" CssClass="req"></asp:TextBox> *<br />
                </div> 
                <div class="clear"></div>
                <br />
                ما تعهد می دهیم اطلاعات شما نزد ما محفوظ خواهد بود.<br />
                <br />
                <div class="text-center">
                    <asp:Button ID="Button2" CssClass="btn btn-success"
                        OnClientClick="return checkfile();"
                        OnClick="Button2_Click" runat="server" Text="ذخیــره" />
                </div>

            </div>
        </div>
        <div class="clear"></div>
    </div>
    <script>
        $(".sheba").on("keyup", function () {
            var o, v = (o = $(this)).val();
            o.val(v.replace(/[^\d]/g, ""));
        });
    </script>
</asp:Content>
