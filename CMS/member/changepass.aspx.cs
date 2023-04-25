using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.member
{
    public partial class changepass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            DataAccessDataContext _data = new DataAccessDataContext();
            if (string.IsNullOrEmpty(txtCurr.Text) || string.IsNullOrEmpty(txtNew.Text) || string.IsNullOrEmpty(txtRep.Text))
            {
                ltr_error.Text = "<div class='alert alert-warning'>وارد کردن همه فیلدها اجباری است</div>";
                ltr_error.Visible = true;
                return;
            }

            if (txtNew.Text.Length<7)
            {
                ltr_error.Text = "<div class='alert alert-warning'>پسورد جدید باید حداقل 7 حرف باشد.</div>";
                ltr_error.Visible = true;
                return;
            }
            if (txtNew.Text != txtRep.Text)
            {
                ltr_error.Text = "<div class='alert alert-warning'>تکرار کلمه عبور با آن یکسان نیست.</div>";
                ltr_error.Visible = true;
                return;
            }
            usersdata tmp = _data.usersdatas.Single(a => a.ID == ((AKUserClass)(Session["User"])).THisUserID);


            string pass =   System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txtCurr.Text, "md5");

            if (pass != tmp.Pass)
            {
                ltr_error.Text = "<div class='warning'>کلمه عبور فعلی اشتباه است.</div>";
                ltr_error.Visible = true;
                return;

            }

            string newpass =   System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txtNew.Text, "md5");

            tmp.Pass = newpass;
            _data.SubmitChanges();

            ltr_error.Text = "<div class='alert alert-success'>رمز عبور جدید شما ذخیره شد.</div>";
            ltr_error.Visible = true;


        }
    }
}