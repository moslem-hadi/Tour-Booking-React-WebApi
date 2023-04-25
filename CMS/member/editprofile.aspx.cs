using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace CMS.member
{
    public partial class editprofile : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            DataAccessDataContext _data = new DataAccessDataContext();
            CMS.member.membermaster MasterPage = (CMS.member.membermaster)Page.Master;
            MasterPage.Check_User();

            string userID;
            if (!IsPostBack)
            {
                var tmp = _data.usersdatas.Single(a => a.ID == ((AKUserClass)(Session["User"])).THisUserID);

                 
                txtMob.Text = tmp.Mobile; 

                txtName.Text = tmp.FullName; 
                 
                txtMail.Text = tmp.Email;
                txtMeliCode.Text = tmp.MeliCode;
                userID = tmp.ID.ToString();

                //ltrprofile.Text = string.Format("<a href='/profile.aspx?id={0}' target='_blank'>صفحه پروفایل شما</a>", userID);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DataAccessDataContext _data = new DataAccessDataContext();
            if (  ddlGender.SelectedIndex == 0 
                || string.IsNullOrEmpty(txtMeliCode.Text.Trim())
                || string.IsNullOrEmpty(txtMeliCode.Text.Trim())

                )
            {
                ltr_error.Text = "<div class='warning'>وارد کردن فیلد های ستاره دار اجباری   است</div>";
                ltr_error.Visible = true;
                return;
            }
            if (!string.IsNullOrEmpty(txtMail.Text) &&CommonFunctions.IsEmail(txtMail.Text))
            {
                ltr_error.Text = "<div class='warning'>ایمیل وارد شده صحیح نیست</div>";
                ltr_error.Visible = true;
                return;
            }
            if (!CommonFunctions.CheckMeliCode(txtMeliCode.Text.Trim()))
            {
                ltr_error.Text = "<div class='warning'>کد ملی وارد شده معتبر نیست</div>";
                ltr_error.Visible = true;
                return;
            }
            if (!string.IsNullOrEmpty(txtMeliCode.Text.Trim()))
            {
                var exist = _data.usersdatas.Any(a => a.MeliCode == txtMeliCode.Text.Trim() &&
                  a.ID != ((AKUserClass)(Session["User"])).THisUserID);
                 
                if (exist)
                {

                    ltr_error.Text = "<div class='warning'>کد ملی وارد شده تکراری است.</div>";
                    ltr_error.Visible = true;
                    return;
                }
            }
           
            var tmp = _data.usersdatas.Single(a => a.ID == ((AKUserClass)(Session["User"])).THisUserID);

            tmp.FullName = CommonFunctions.GetPlainTextFromHtml(txtName.Text.Trim());
           
            tmp.Mobile = CommonFunctions.GetPlainTextFromHtml(txtMob.Text);
             
            
            tmp.Gender = bool.Parse(ddlGender.SelectedValue);
            tmp.MeliCode = CommonFunctions.GetPlainTextFromHtml(txtMeliCode.Text);

             
            _data.SubmitChanges();

            ((AKUserClass)(Session["User"])).FullNameOfUser = CommonFunctions.GetPlainTextFromHtml(txtName.Text);

            ltr_error.Text = "<div class='success'>تغییرات ذخیره شد</div>";
            ltr_error.Visible = true;
            return;
        }

        private bool checkFileType(string fileName)
        {
            string fileExt = Path.GetExtension(fileName);
            switch (fileExt.ToLower())
            {
                case ".gif":
                    return true;
                case ".png":
                    return true;
                case ".jpg":
                    return true;
                case ".jpeg":
                    return true;
                default:
                    return false;

            }

        }

    }
}