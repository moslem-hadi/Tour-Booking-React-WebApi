using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class changepass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();


        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            DataAccessDataContext _data = new DataAccessDataContext();

            if (string.IsNullOrEmpty(txtLast.Text) || string.IsNullOrEmpty(txtNew.Text))
                return;

            if (txtNew.Text != txtRep.Text)
            {
                MessageBox1.Message = "<div class='message Error'>تکرار کلمه عبور با آن یکسان نیست.</div>";
                MessageBox1.Visible = true;
                return;
            }
            usersdata tmp = _data.usersdatas.Single(a => a.ID == ((AKUserClass)(Session["User"])).THisUserID);


            string pass = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txtLast.Text, "md5");

            if (pass != tmp.Pass)
            {
                MessageBox1.Message = "<div class='message Error'>کلمه عبور فعلی اشتباه است.</div>";
                MessageBox1.Visible = true;
                return;

            }

            string newpass = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txtNew.Text, "md5");

            tmp.Pass = newpass;
            _data.SubmitChanges();

            MessageBox1.Message = "<div class='message submit'>رمز عبور جدید شما ذخیره شد.</div>";
            MessageBox1.Visible = true;
        }
    }
}