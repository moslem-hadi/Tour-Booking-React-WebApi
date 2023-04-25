<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="CMS.signup" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <title>عضویت در سایت</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div runat="server" id="divAll">
        <div>
            <img src="/assets/img/logo.png" alt="پرهان ترانسفر" style="max-width: 10rem;" class="img-fluid mb-4">
            <h3 class="mb-4">عضویت  
            <%=signupType %>
            </h3>
            <asp:Literal Text="" ID="ltr_error" runat="server" />
        </div>
        <div id="form" runat="server">
            <div class="form-group">
                <label for="loginUsername" class="form-label">
                    نام و نام خانوادگی
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="val1" ControlToValidate="txtFullName" runat="server" ErrorMessage="<span class='text-danger' title='فیلد الزامی' data-toggle='tooltip'>×</span>" Display="Dynamic"></asp:RequiredFieldValidator>
                </label>
                <asp:TextBox ID="txtFullName" CssClass="form-control" MaxLength="50" placeholder="" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="loginUsername" class="form-label">
                    شماره موبایل
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="val1" ControlToValidate="txtUserName" runat="server" ErrorMessage="<span class='text-danger' title='فیلد الزامی' data-toggle='tooltip'>×</span>" Display="Dynamic"></asp:RequiredFieldValidator>
                </label>
                <asp:TextBox ID="txtUserName" CssClass="form-control ltr" MaxLength="11" placeholder="" runat="server"></asp:TextBox>
            </div>
            <div class="form-group mb-4">
                <label for="loginPassword" class="form-label">
                    رمز ورود
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="val1" ControlToValidate="txtPass" runat="server" ErrorMessage="<span class='text-danger' title='فیلد الزامی' data-toggle='tooltip'>×</span>" Display="Dynamic"></asp:RequiredFieldValidator>
                </label>
                <asp:TextBox ID="txtPass" runat="server" CssClass="ltr form-control" MaxLength="50" placeholder="رمز عبور" TextMode="Password"></asp:TextBox>
            </div>

            <%if (userType == CMS.UserTypes.Agency || userType == CMS.UserTypes.Hotel)
                { %>
            <div class="form-group">
                <label for="loginUsername" class="form-label">
                    آدرس  
                </label>
                <asp:TextBox ID="txtAddress" CssClass="form-control ltr" placeholder="" runat="server"></asp:TextBox>
            </div>
            <%} %>
            <%if (userType == CMS.UserTypes.Agency)
                { %>
            <div class="form-group">
                <label for="loginUsername" class="form-label">
                    نام مدیریت آژانس
                </label>
                <asp:TextBox ID="txtAgancyManagementName" CssClass="form-control ltr" MaxLength="50" placeholder="" runat="server"></asp:TextBox>
            </div>
            <%} %>

            <%if (userType == CMS.UserTypes.Hotel)
                { %>
            <div class="form-group">
                <label for="loginUsername" class="form-label">
                    تعداد ستاره یا درجه
                </label>
                <asp:TextBox ID="txtHotelStar" CssClass="form-control ltr" MaxLength="50" placeholder="" runat="server"></asp:TextBox>
            </div>
            <%} %>




            <div class="form-group">
                <label for="loginUsername" class="form-label">
                    کد امنیتی  
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="val1" ControlToValidate="txtValidate" runat="server" ErrorMessage="<span class='text-danger' title='فیلد الزامی' data-toggle='tooltip'>×</span>" Display="Dynamic"></asp:RequiredFieldValidator>
                </label>
                <div class="captcha-container">
                    <asp:TextBox ID="txtValidate" MaxLength="5" CssClass="ltr form-control" placeholder="کد امنیتی را وارد کنید"
                        runat="server"></asp:TextBox><cc1:CaptchaControl ID="ccJoin" CssClass="captchalogin" runat="server" CaptchaBackgroundNoise="low"
                            CaptchaLength="5" CaptchaHeight="32" CaptchaWidth="110" CaptchaLineNoise="None" CaptchaChars="123456789"
                            CaptchaMinTimeout="5" CaptchaMaxTimeout="240" FontColor="#1d4a9c" BackColor="#ffffff" />
                </div>
                
            </div>


            <asp:Button ID="btnSignup" runat="server" ValidationGroup="val1" Text="عضویت در سایت" CssClass="btn btn-lg btn-block btn-primary"
                OnClick="btnSignup_Click" />
            <p class="text-center mt-3">
                <small class="text-muted text-center">در صورتی که عضو هستید، 
               
                <a href="/">برای ورود اینجا کلیک کنید.</a>
                </small>
            </p>
            <hr data-content="...." class="my-3 hr-text letter-spacing-2">

            <div class="text-center">
                <ul class="signup-type">
                    <%if (userType != CMS.UserTypes.Normal)
                        { %>
                    <li><a class="btn btn-light" href="/signup">عضویت عادی</a></li>
                    <%} %>
                    <%if (userType != CMS.UserTypes.Agency)
                        { %>
                    <li><a class="btn btn-light" href="/signup/agancy">عضویت به عنوان آژانس</a></li>
                    <%} %>
                    <%if (userType != CMS.UserTypes.Hotel)
                        { %>
                    <li><a class="btn btn-light" href="/signup/hotel">عضویت به عنوان هتل</a></li>
                    <%} %>
                    <%if (userType != CMS.UserTypes.Driver)
                        { %>
                    <li><a class="btn btn-light" href="/signup/driver">عضویت پرسنل</a></li>
                    <%} %>
                </ul>
            </div>

        </div>


    </div>
    <div runat="server" id="divDone" visible="false">


        <div class="f-modal-alert">

            <div class="f-modal-icon f-modal-success animate">
                <span class="f-modal-line f-modal-tip animateSuccessTip"></span>
                <span class="f-modal-line f-modal-long animateSuccessLong"></span>
                <div class="f-modal-placeholder"></div>
                <div class="f-modal-fix"></div>
            </div>
        </div>
        <h5 class="htitlesmall text-success text-center m-t-20">عضویت شما با موفقیت انجام شد.</h5>
        <asp:Literal Text="<p class='text-center'>می توانید از طریق <a href='/'>این لینک</a> وارد حساب کاربری خود شوید.</p>" ID="ltrMsg" runat="server" />


    </div>

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="cphScript" runat="server">
    <%if (userType != CMS.UserTypes.Normal)
        { %>
    <script src="/assets/libs/jquery/jquery.min.js"></script>
    <script>
        $(document).ready(function () {
            $('.captchalogin').find('img').attr('src', '/' + $('.captchalogin').find('img').attr('src'));
        });
    </script>
    <%} %>
</asp:Content>
