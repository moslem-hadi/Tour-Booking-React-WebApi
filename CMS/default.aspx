<%@ Page  Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CMS._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <title>ورود به حساب کاربری</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div>
        <img src="/assets/img/logo.png" alt="پرهان ترانسفر" style="max-width: 10rem;" class="img-fluid mb-4">
        <h3 class="mb-4">ورود به حساب کاربری</h3>
        <asp:Literal Text="" ID="ltr_error" runat="server" />
    </div>
    <div id="form" runat="server">
        <div class="form-group">
            <label for="loginUsername" class="form-label">
                شماره موبایل
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="val1" ControlToValidate="txtUserName" runat="server" ErrorMessage="<span class='text-danger' title='فیلد الزامی' data-toggle='tooltip'>×</span>" Display="Dynamic"></asp:RequiredFieldValidator>
            </label>
            <asp:TextBox ID="txtUserName" CssClass="form-control ltr" MaxLength="11" placeholder="" runat="server"></asp:TextBox>


        </div>
        <div class="form-group mb-4">
            <div class="row">
                <div class="col">
                    <label for="loginPassword" class="form-label">
                        رمز ورود
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="val1" ControlToValidate="txtPass" runat="server" ErrorMessage="<span class='text-danger' title='فیلد الزامی' data-toggle='tooltip'>×</span>" Display="Dynamic"></asp:RequiredFieldValidator>
                    </label>
                </div>
                <div class="col-auto"><a href="#" class="form-text small">رمز عبور را فراموش کرده اید؟</a></div>
            </div>
            <asp:TextBox ID="txtPass" runat="server" CssClass="ltr form-control" placeholder="رمز عبور" TextMode="Password"></asp:TextBox>


        </div>
        <div class="form-group mb-4">
            <div class="custom-control custom-checkbox">
                <input id="CheckBox1" runat="server" checked="checked" type="checkbox" class="custom-control-input">
                <label for="loginRemember" class="custom-control-label text-muted"><span class="text-sm">مرا به خاطر بسپار</span></label>
            </div>
        </div>
        <asp:Button ID="btnLogin" runat="server" ValidationGroup="val1" Text="ورود به سایت" CssClass="btn btn-lg btn-block btn-primary" OnClick="btnLogin_Click" />
        <hr data-content="...." class="my-3 hr-text letter-spacing-2">

        <p class="text-center">
            <small class="text-muted text-center">در صورتی که حساب کاربری ندارید، <a href="/signup">از اینجا ثبت نام کنید.</a></small>
        </p>
    </div>
</asp:Content> 