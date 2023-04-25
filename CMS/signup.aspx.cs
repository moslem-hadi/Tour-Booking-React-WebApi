using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS
{
    public partial class signup : System.Web.UI.Page
    {
        public string signupType;
        public UserTypes userType;
        protected void Page_Load(object sender, EventArgs e)
        {
            string type="";
            if (RouteData.Values["type"] != null)
            type = (RouteData.Values["type"] as string).ToLower();

            switch (type)
            {
                case "":
                    signupType = "در سایت"; userType = UserTypes.Normal; break;
                case "agancy":
                    signupType = "به عنوان آژانس"; userType = UserTypes.Agency; break;
                case "hotel":
                    signupType = "به عنوان هتل"; userType = UserTypes.Hotel; break;
                case "driver":
                    signupType = "به عنوان راننده"; userType = UserTypes.Driver; break;
                default:
                    break;
            }

        }

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            try
            {
                ltr_error.Visible = false;
                if (string.IsNullOrEmpty(txtUserName.Text.Trim()) ||
                   string.IsNullOrEmpty(txtPass.Text.Trim()) || string.IsNullOrEmpty(txtFullName.Text.Trim()))
                {
                    ltr_error.Text = "<div class='alert alert-warning'>وارد کردن همه اطلاعات الزامی است</div>";
                    ltr_error.Visible = true;
                    return;
                }
                ltr_error.Visible = false;
                var username = CommonFunctions.GetPlainTextFromHtml(CommonFunctions.ConvertNumFa2La(txtUserName.Text.Trim().ToLower()));

                ccJoin.ValidateCaptcha(txtValidate.Text);
                if (!ccJoin.UserValidated)
                {
                    txtValidate.Text = "";
                    ltr_error.Text = "<div class='alert alert-warning'>کد امنیتی وارد شده اشتباه است. لطفا مجددا وارد کنید</div>";
                    ltr_error.Visible = true;
                    return;
                }


                if (Session["LsendFormTime"] != null)
                {
                    try
                    {
                        DateTime dt = DateTime.Parse(Session["LsendFormTime"].ToString());
                        TimeSpan difference = DateTime.Now - dt;
                        if ((7 - (int)difference.TotalSeconds) > 0)
                        {
                            ltr_error.Text = "<div class='alert alert-warning fade in'>ارسال فرم هر 10 ثانیه یکبار مجاز است ." + (7 - (int)difference.TotalSeconds) + " ثانیه دیگر تلاش کنید.</div>";
                            ltr_error.Visible = true;
                            return;
                        }
                    }
                    catch { }
                }
                Session["LsendFormTime"] = DateTime.Now;


                DataAccessDataContext _data = new DataAccessDataContext();

                if (_data.usersdatas.Where(c => c.Mobile == username).Any())
                {
                    ltr_error.Text = "<div class='alert alert-warning fade in'>شماره موبایل وارد شده تکراری است. اگر قبلا ثبت نام کرده اید و کلمه عبور خود را فراموش کرده اید، با پشتیبانی تماس بگیرید.</div>";
                    ltr_error.Visible = true;
                    return;
                }

                usersdata tmp = new usersdata();

                tmp.Address = CommonFunctions.GetPlainTextFromHtml(txtAddress.Text);
                tmp.IsBanned = false;
                tmp.LastLogin = DateTime.Now;

                tmp.Pass = CommonFunctions.ConvertNumFa2La(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txtPass.Text, "md5"));


                tmp.FullName = CommonFunctions.GetPlainTextFromHtml(txtFullName.Text);
                tmp.Email = "";

                tmp.RegisterDate = DateTime.Now;
                tmp.Tell = "";
                tmp.IsActive = true;
                if (userType != UserTypes.Normal)
                    tmp.IsActive = false;
                tmp.IsManager = false;
                tmp.About = "";
                tmp.Mobile = username;
                tmp.BannedMsg = "";
                tmp.IP = CommonFunctions.GetUser_IP();
                tmp.IsBanned = false;
                tmp.LastLogin = DateTime.Now;
                tmp.MeliCode = "";
                tmp.ManagementName = CommonFunctions.GetPlainTextFromHtml(txtAgancyManagementName.Text);
                tmp.HotelStar = CommonFunctions.GetPlainTextFromHtml(txtHotelStar.Text);

                tmp.BankHesab = "";
                tmp.BankKart = "";
                tmp.BankName = "";
                tmp.BankOwnername = "";
                tmp.BankSheba = "";
                tmp.UserType = (int)userType;

                _data.usersdatas.InsertOnSubmit(tmp);
                _data.SubmitChanges();

                divAll.Visible = false;
                divDone.Visible = true;
                if (userType != UserTypes.Normal)
                    ltrMsg.Text = "<p class='text-center'>بعد از تایید عضویت توسط مدیریت، می‌توانید وارد حساب کاربری خود شود.</p>";
            }
            catch {

                ltr_error.Text = "<div class='alert alert-warning fade in'>خطایی در مراحل عضویت رخ داد. در صورت نیاز می توانید با پشتیبانی تماس بگیرید.</div>";
                ltr_error.Visible = true;
                return;
            }
        }
    }
}