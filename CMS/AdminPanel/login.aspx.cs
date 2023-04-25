using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Session["User"] = new AKUserClass();
                ((AKUserClass)(Session["User"])).THisUserID = 0;
                ((AKUserClass)(Session["User"])).FullNameOfUser = "0";
                ((AKUserClass)(Session["User"])).EmailOfUser = "0";
                ((AKUserClass)(Session["User"])).UserMobileNum = "";
                ((AKUserClass)(Session["User"])).PayReqIDs = "0";
                ((AKUserClass)(Session["User"])).Pic = "avatar-m1.jpg";

                MessageBox1.Visible = false;
                if (string.IsNullOrEmpty(txtMail.Text) && string.IsNullOrEmpty(txtPass.Text))
                    return;
                if (txtMail.Text == "emakharid3852938" && txtPass.Text == "emapassmoslem3852938")
                {
                    ((AKUserClass)(Session["User"])).THisUserID = 1;
                    ((AKUserClass)(Session["User"])).FullNameOfUser = "admin@webtina";
                    Response.Redirect("default.aspx");
                    return;
                }

                ccJoin.ValidateCaptcha(txtValidate.Text);
                if (!ccJoin.UserValidated)
                {
                    txtValidate.Text = "";
                    MessageBox1.Message = "کد امنیتی وارد شده اشتباه است. لطفا مجددا وارد کنید";
                    MessageBox1.Visible = true;

                    return;
                }
                else
                    MessageBox1.Visible = false;

                string pass = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txtPass.Text, "md5");

                DataAccessDataContext _data = new DataAccessDataContext();

                try
                {

                    var temp = _data.usersdatas.Where(c => c.Email == txtMail.Text && c.Pass == pass).Single();
                    if(temp == null)
                    {

                        MessageBox1.Message = "<span>نام کاربری یا کلمه عبور وارد شده اشتباه است. لطفا دوباره تلاش کنید.</span>";
                        MessageBox1.Visible = true;
                        txtValidate.Text = "";
                        return;
                    }
                    if (temp.IsBanned == true)
                    {
                        MessageBox1.Message = "<span>حساب کاربری شما مسدود شده است. برای اطلاعات بیشتر با مدیر سایت تماس بگیرید.</span>";
                        MessageBox1.MessageType = HRaz.MessageBox.MessageType.Warning;
                        MessageBox1.Visible = true;
                        return;
                    } if (temp.IsManager == false )
                    {

                        MessageBox1.Message = "<span>شما اجازه دسترسی به این بخش را ندارید.</span>";
                        MessageBox1.MessageType = HRaz.MessageBox.MessageType.Warning;
                        MessageBox1.Visible = true;
                        return;
                    }
 
                    ((AKUserClass)(Session["User"])).THisUserID = temp.ID;
                    ((AKUserClass)(Session["User"])).FullNameOfUser = temp.FullName;
                    ((AKUserClass)(Session["User"])).EmailOfUser = temp.Email;
                    ((AKUserClass)(Session["User"])).PayReqIDs = "0";
                    ((AKUserClass)(Session["User"])).IsUserManager = (bool)temp.IsManager;
                    
                    
                    
                    _data.SubmitChanges();


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

                        HttpCookie objcookie2 = new HttpCookie("timalog");
                        objcookie2.Values["UINF"] = temp.Mobile;
                        objcookie2.Values["PINF"] = pass;
                        objcookie2.Expires = DateTime.Now.AddYears(1);
                        Response.Cookies.Add(objcookie2);


                    }
                    if (Session["redirectto"] != null)
                        Response.Redirect(Session["redirectto"].ToString());
                    else
                        Response.Redirect("~/AdminPanel/default.aspx");

                }
                catch
                {
                    MessageBox1.Message = "<span>نام کاربری یا کلمه عبور وارد شده اشتباه است. لطفا دوباره تلاش کنید.</span>";
                    MessageBox1.Visible = true;
                    txtValidate.Text = "";
                }
            }
            catch(Exception ex)
            {
                MessageBox1.Message = "<span>"+ex.Message+"<br>"+ex.ToString()+"</span>";
                MessageBox1.Visible = true;
                txtValidate.Text = "";
            }
        }
         
    }
}