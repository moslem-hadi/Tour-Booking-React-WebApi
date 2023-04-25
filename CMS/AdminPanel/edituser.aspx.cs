using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class edituser : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();

            if (!IsPostBack)
            {
                int userid = 0;
                int.TryParse(Request.QueryString["id"], out userid);
                if (userid == 0)
                    Response.Redirect("userlist.aspx");
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    usersdata tmp = _data.usersdatas.Single(a => a.ID == userid);
                     
                    txtMail.Text = tmp.Email;
                    txtMob.Text = tmp.Mobile; 
                    txtName.Text = tmp.FullName; 
                    rdbadmin.Checked = (bool)tmp.IsManager;
                    rdbuser.Checked = !rdbadmin.Checked;
                    chbBanned.Checked = (bool)tmp.IsBanned;
                    txtBannedMassage.Text = tmp.BannedMsg;
                    ddlUserType.SelectedValue = tmp.UserType.ToString();
                    chbActivate.Checked = tmp.IsActive;



                    txtbsheba.Text = tmp.BankSheba;
                    txtbhesab.Text = tmp.BankHesab;
                    txtbshomare.Text = tmp.BankKart;
                    txtbname.Text = tmp.BankName;
                    txtbsaheb.Text = tmp.BankOwnername;
                    txtMeli.Text = tmp.MeliCode;
                }
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            MessageBox1.Visible = false;
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtMail.Text))
            {
                MessageBox1.Message = "فیلدهای لازم را پر کنید.";
                MessageBox1.Visible = true;
                return;
            }


            int userid = 0;
            int.TryParse(Request.QueryString["id"], out userid);
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                usersdata tmp = _data.usersdatas.Single(a => a.ID == userid);
               

                if (!string.IsNullOrEmpty(txtPassword.Text))
                {
                    string pass = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "md5");
                    tmp.Pass = pass;
                }
                var email = CommonFunctions.ConvertNumFa2En(txtMail.Text.Trim().ToLower());
                var mobile = CommonFunctions.ConvertNumFa2En(txtMob.Text.Trim().ToLower());
                if (_data.usersdatas.Where(a => a.Email == email && a.ID != tmp.ID).Any())
                {
                    MessageBox1.Message = "ایمیل تکراری است";
                    MessageBox1.Visible = true;
                    MessageBox1.MessageType = HRaz.MessageBox.MessageType.Error;
                    return;
                }
                if (_data.usersdatas.Where(a => a.Mobile == mobile && a.ID != tmp.ID).Any())
                {
                    MessageBox1.Message = "موبایل تکراری است";
                    MessageBox1.Visible = true;
                    MessageBox1.MessageType = HRaz.MessageBox.MessageType.Error;
                    return;
                }
                tmp.Email = email;
                tmp.FullName = txtName.Text;
 
                tmp.IsManager = rdbadmin.Checked;
                tmp.Mobile = mobile;

                tmp.UserType = int.Parse(ddlUserType.SelectedValue);
                tmp.IsActive = chbActivate.Checked;

                tmp.IsBanned = chbBanned.Checked;




                tmp.BankHesab = txtbhesab.Text;
                tmp.BankKart = txtbshomare.Text;
                tmp.BankName = txtbname.Text;
                tmp.BankOwnername = txtbsaheb.Text;
                tmp.BankSheba = txtbsheba.Text.ToLower().Replace("ir", "");

                tmp.MeliCode = txtMeli.Text;



                if (chbBanned.Checked)
                    tmp.BannedMsg = txtBannedMassage.Text;

                _data.SubmitChanges();
                MessageBox1.Message = "با موفقیت ذخیره شد";
                MessageBox1.Visible = true;
                MessageBox1.MessageType = HRaz.MessageBox.MessageType.Submit;
                return;

            }
        }
    }
}