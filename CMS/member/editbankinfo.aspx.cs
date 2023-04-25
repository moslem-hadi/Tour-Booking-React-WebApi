using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace CMS.member
{
    public partial class editbankinfo : System.Web.UI.Page
    {

        public string meliKart = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            DataAccessDataContext _data = new DataAccessDataContext();
            CMS.member.membermaster MasterPage = (CMS.member.membermaster)Page.Master;
            MasterPage.Check_User();

            string userID;
            if (!IsPostBack)
            {
                var tmp = _data.usersdatas.Single(a => a.ID == ((AKUserClass)(Session["User"])).THisUserID);

                if (!string.IsNullOrEmpty(tmp.BankHesab))
                {
                    ltr_error.Text = "<div class='warning'>در صورت نیاز به تغییر اطلاعات بانکی خود، <a href='/member/addticket.aspx'>تیکت ارسال کنید</a>.</div>";
                    ltr_error.Visible = true;
                    Button2.Visible = false;
                }

                // txtMeli.Text = tmp.MeliCode;
                userID = tmp.ID.ToString();
                txtbsheba.Text = tmp.BankSheba;
                txtbhesab.Text = tmp.BankHesab;
                txtbshomare.Text = tmp.BankKart;
                try
                { ddlBank.SelectedValue = tmp.BankName; }
                catch { }
                txtbsaheb.Text = tmp.BankOwnername;
                 
            }
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            DataAccessDataContext _data = new DataAccessDataContext();
          
            if (string.IsNullOrEmpty(txtbhesab.Text) ||
                string.IsNullOrEmpty(txtbshomare.Text)||
                string.IsNullOrEmpty(txtbsaheb.Text)||
                string.IsNullOrEmpty(txtbhesab.Text)
                )
            {
                ltr_error.Text = "<div class='warning'>وارد کردن همه اطلاعات حساب بانکی  و کد ملی اجباری است</div>";
                ltr_error.Visible = true;
                return;
            }
            if (txtbsheba.Text.Length < 24)
            {

                ltr_error.Text = "<div class='warning'>شماره شبا، 24 رقمی می باشد. لطفا اصلاح نمایید</div>";
                ltr_error.Visible = true;
                return;
            }

            if (txtbshomare.Text.Length < 16)
            {

                ltr_error.Text = "<div class='warning'>شماره کارت بانکی، 16 رقمی می باشد. لطفا اصلاح نمایید</div>";
                ltr_error.Visible = true;
                return;
            }

            if (!string.IsNullOrEmpty(txtbsheba.Text.Trim()))
            {
                var exist = _data.usersdatas.Any(a => a.BankSheba == txtbsheba.Text.Trim() &&
                  a.ID != ((AKUserClass)(Session["User"])).THisUserID);

                if (exist)
                {

                    ltr_error.Text = "<div class='warning'>شماره شبا وارد شده تکراری است.</div>";
                    ltr_error.Visible = true;
                    return;
                }
            }



            if (!string.IsNullOrEmpty(txtbshomare.Text.Trim()))
            {
                var exist = _data.usersdatas.Any(a => a.BankKart == txtbshomare.Text.Trim() &&
                  a.ID != ((AKUserClass)(Session["User"])).THisUserID);

                if (exist)
                {

                    ltr_error.Text = "<div class='warning'>شماره کارت بانکی وارد شده تکراری است.</div>";
                    ltr_error.Visible = true;
                    return;
                }
            }
            if (!string.IsNullOrEmpty(txtbhesab.Text.Trim()))
            {
                var exist = _data.usersdatas.Any(a => a.BankHesab == txtbhesab.Text.Trim() &&
                  a.ID != ((AKUserClass)(Session["User"])).THisUserID);

                if (exist)
                {

                    ltr_error.Text = "<div class='warning'>شماره حساب وارد شده تکراری است.</div>";
                    ltr_error.Visible = true;
                    return;
                }
            }


            var tmp = _data.usersdatas.Single(a => a.ID == ((AKUserClass)(Session["User"])).THisUserID);


            // tmp.MeliCode = txtMeli.Text;
            tmp.BankHesab = txtbhesab.Text;
            tmp.BankKart = txtbshomare.Text;
            tmp.BankName = ddlBank.SelectedValue;
            tmp.BankOwnername = txtbsaheb.Text;
            tmp.BankSheba = txtbsheba.Text.ToLower().Replace("ir", "");
              

            _data.SubmitChanges(); 
            ltr_error.Text = "<div class='success'>تغییرات ذخیره شد</div>";
            ltr_error.Visible = true;
            return;
        }

    }
}