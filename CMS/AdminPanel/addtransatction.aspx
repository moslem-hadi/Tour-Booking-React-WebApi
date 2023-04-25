<%@ Page Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="addtransatction.aspx.cs" Inherits="CMS.Manage.addtransatction" %>

<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <title>افزودن تراکنش به حساب کاربری</title>

    <link href="../component/datepicker/styles/jquery-ui-1.8.14.css" rel="stylesheet"
        type="text/css" />


    <script src="../component/datepicker/scripts/jquery.ui.datepicker-cc.all.min.js"
        type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            // حالت پیشفرض
            $('#txtRegDate').datepicker();

        });
    </script>
    <script src="js/urlcheck.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box">
        <div class="header">
            <h4>افزودن تراکنش</h4>
        </div>
        <div class="content">

            <fieldset>

                <div class="rowelement withpadding">
                    <cc1:MessageBox ID="MessageBox1" runat="server" MessageType="Error" Visible="false">
                    </cc1:MessageBox>
                </div>
                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            توضیح تراکنش
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:TextBox ID="txtTitle" runat="server" TextMode="SingleLine" MaxLength="100" CssClass="req"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>



                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            کاربر
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:TextBox ID="txtuserID" placeholder="کد کاربری" runat="server" MaxLength="10" Width="70" CssClass="req ltr"></asp:TextBox>
                        <b>
                            <asp:Literal Text="" ID="ltrUserName" runat="server" /></b>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>

                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            مبلغ
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:TextBox ID="txtMablagh" runat="server" MaxLength="10" Width="70" CssClass="req ltr txtprice"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>



                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            نوع تراکنش
                        </label>
                    </div>
                    <div class="span6 right">

                        <asp:DropDownList ID="ddlType" Width="93px" runat="server">
                            <asp:ListItem Value="True">افزایش</asp:ListItem>
                            <asp:ListItem Value="False">کاهش</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>





<%--                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            تاریخ تراکنش
                        </label>
                    </div>
                    <div class="span6 right">

                        <asp:TextBox CssClass="ltr" ID="txtRegDate" ClientIDMode="Static" runat="server" Width="70"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        ساعت
                        &nbsp;&nbsp;&nbsp;
                         <asp:TextBox CssClass="ltr" ID="txtTime" runat="server" Width="70" plceholder="HH:MM"></asp:TextBox>


                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>--%>




                <div class="rowelement">
                    <div class="span2 right">
                    </div>
                    <div class="span6 right">
                        <asp:Button ID="Button1" runat="server" Text="ذخـــیره" CssClass="btn btn-success"
                            OnClick="Button1_Click" />
                        <a href="reminders.aspx" class="btn btn-warning">انصراف</a>
                        <br />
                        <br />
                        <div class="alert alert-warning">
                            توجه کنید که افزودن تراکنش متفرقه، بر موجودی کاربر تاثیر میگذارد و موجودی را افزایش/کاهش می دهد.
                            اما امکان حذف تراکنش وجود ندارد و در صورت نیاز باید تراکنش جدید ثبت شود. 
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
