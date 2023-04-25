using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var returnUrl = Request.QueryString["ReturnUrl"];

            if (Request.Cookies["aspnetusertoken"] != null && !string.IsNullOrEmpty(returnUrl))
            {
                //    int userId = ((AKUserClass)(Session["User"])).THisUserID;
                //if (!int.TryParse(Request.Cookies["aspnetusertoken"].Value, out userId))
                //    return;
                string token = Request.Cookies[CommonFunctions.TokenCookieName].Value;
                if (string.IsNullOrEmpty(token))
                    Response.Redirect("~/logout.aspx", true);
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    if (!CommonFunctions.IsLoginTokenValid(token))
                        return;


                    var user = CommonFunctions.GetUserByToken(token);
                    if (user == null)
                        return;



                    if (!string.IsNullOrEmpty(returnUrl ?? ""))
                    {
                        Response.Redirect(returnUrl +
                            (returnUrl.Contains($"token={token}") ? "" :
                            (returnUrl.Contains("?") ? "&" : "?") + $"token={token}"), true);
                    }
                    else
                    {
                        Response.Redirect("/member", true);
                    }

                }
                //Response.Redirect("//" + Request.Url.Authority, true);
            }



            if (Session["errorCode"] != null)
            {
                Session["errorCode"] = null;
                ltr_error.Text = "<div class='alert alert-warning fade in'>" + Session["errorCode"]+"</div>";
                ltr_error.Visible = true;
            }

            if (Session["User"] != null && ((AKUserClass)(Session["User"])).THisUserID != 0)
                Response.Redirect("/member",true);
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            ltr_error.Visible = false;

            Session["User"] = new AKUserClass();
            ((AKUserClass)(Session["User"])).THisUserID = 0;
            ((AKUserClass)(Session["User"])).FullNameOfUser = "0";
            ((AKUserClass)(Session["User"])).EmailOfUser = "0";
            ((AKUserClass)(Session["User"])).UserMobileNum = "";
            ((AKUserClass)(Session["User"])).PayReqIDs = "0";
            ((AKUserClass)(Session["User"])).Pic = "avatar-m1.jpg";



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





            string pass = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txtPass.Text, "md5");

            DataAccessDataContext _data = new DataAccessDataContext();

            try
            {

                var temp = _data.usersdatas.Where(c => c.Mobile == CommonFunctions.ConvertNumFa2La(txtUserName.Text.Trim().ToLower()) && c.Pass == CommonFunctions.ConvertNumFa2La(pass)).FirstOrDefault();

                if (temp == null)
                {
                    ltr_error.Text = "<div class='alert alert-warning'>ایمیل یا کلمه عبور وارد شده اشتباه است." +
                        " لطفا دوباره تلاش کنید. اگر رمز ورود خود را فراموش کرده اید، با پشتیبانی تماس بگیرید.</div>";
                    ltr_error.Visible = true;
                    return;
                }

                if ((bool)temp.IsBanned)
                {
                    ltr_error.Text = "<div class='alert alert-info'>حساب کاربری شما مسدود شده است. علت مسدود شدن حساب کاربری:<br>" + temp.BannedMsg + "</div>";
                    ltr_error.Visible = true;
                    form.Visible = false;
                    return;
                }



                if (!(bool)temp.IsActive)
                {
                    ltr_error.Text = "<div class='alert alert-info'>حساب کاربری شما فعال نشده است.</div>";
                    ltr_error.Visible = true;
                    form.Visible = false;
                    return;
                }
                ((AKUserClass)(Session["User"])).THisUserID = temp.ID;
                ((AKUserClass)(Session["User"])).FullNameOfUser = temp.FullName;
                ((AKUserClass)(Session["User"])).EmailOfUser = temp.Email;
                ((AKUserClass)(Session["User"])).PayReqIDs = "0";
                ((AKUserClass)(Session["User"])).IsUserManager = (bool)temp.IsManager;
                string token = Guid.NewGuid().ToString();
                var expire = CommonFunctions.TokenExpireDateTime;


                _data.LoginTokens.DeleteAllOnSubmit(_data.LoginTokens.Where(a => a.UserId == temp.ID));
                _data.LoginTokens.InsertOnSubmit(new LoginToken
                {
                    Token = token,
                    ExpireDate = expire,
                    RegDate = DateTime.Now,
                    UserId = temp.ID
                });


                temp.IP = CommonFunctions.GetUser_IP();
                temp.LastLogin = DateTime.Now;
                _data.SubmitChanges();

                HttpCookie tokenCookie = new HttpCookie(CommonFunctions.TokenCookieName, token) { Expires = expire };
                Response.Cookies.Add(tokenCookie);

                if (CheckBox1.Checked)
                {
                    #region cookie
                    string[] str = Request.Cookies.AllKeys;
                    foreach (string co in str)
                    {
                        if (co == "timalog")
                            Response.Cookies[co].Expires = DateTime.Now.AddDays(-1);
                    }
                    #endregion

                    HttpCookie objcookie = new HttpCookie("timalog");
                    objcookie.Values["UINF"] = temp.Mobile;
                    objcookie.Values["PINF"] = pass;
                    objcookie.Expires = DateTime.Now.AddYears(1);
                    Response.Cookies.Add(objcookie);

                }
                var returnUrl = Request.QueryString["ReturnUrl"];

                if (returnUrl != null)
                    Response.Redirect(returnUrl +
                        (returnUrl.Contains($"token={token}") ? "" :
                        (returnUrl.Contains("?") ? "&" : "?") + $"token={token}"), true);
                else if (Session["ReturnUrl"] != null)
                    Response.Redirect(Session["ReturnUrl"].ToString());
                else
                    Response.Redirect("~/member");

            }
            catch
            {
                ltr_error.Text = "<div class='alert alert-warning'>ایمیل یا کلمه عبور وارد شده اشتباه است. لطفا دوباره تلاش کنید. اگر رمز ورود خود را فراموش کرده اید، <a href='forgotpass.aspx'>برای درخواست رمز جدید کلیک کنید</a>.</div>";
                ltr_error.Visible = true;
                //txtValidate.Text = "";
            }
        }

        [System.Web.Services.WebMethod()]
        [HttpPost]
        public static bool IsLoginTokenValid(string token)
        {
            return CommonFunctions.IsLoginTokenValid(token);
        }
    }
}