using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class adduser : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();

            if (!IsPostBack)
            {
 
            }
        }
         

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtMail.Text)
                || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox1.Message = "فیلدهای لازم را پر کنید.";
                MessageBox1.Visible = true;
                return;
            }


            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                usersdata tmp = new usersdata(); 

                tmp.BannedMsg="";
                string pass=System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "md5");
                tmp.Pass = pass;


                var email = CommonFunctions.ConvertNumFa2En(txtMail.Text.Trim().ToLower());
                var mobile = CommonFunctions.ConvertNumFa2En(txtMob.Text.Trim().ToLower());
                if (_data.usersdatas.Where(a => a.Email == email ).Any())
                {
                    MessageBox1.Message = "ایمیل تکراری است";
                    MessageBox1.Visible = true;
                    MessageBox1.MessageType = HRaz.MessageBox.MessageType.Error;
                    return;
                }
                if (_data.usersdatas.Where(a => a.Mobile == mobile  ).Any())
                {
                    MessageBox1.Message = "موبایل تکراری است";
                    MessageBox1.Visible = true;
                    MessageBox1.MessageType = HRaz.MessageBox.MessageType.Error;
                    return;
                }
                tmp.Email = email;
                tmp.Mobile = mobile;

                tmp.FullName = txtName.Text;
                tmp.IP = "0";
                tmp.IsBanned = false;
                tmp.IsManager = rdbadmin.Checked;
                tmp.LastLogin = DateTime.Now;
                tmp.RegisterDate = DateTime.Now; 
                tmp.Gender = true;
                tmp.UserType = int.Parse(ddlUserType.SelectedValue);
                tmp.IsActive = chbActivate.Checked;
                tmp.LastLoginIP = tmp.IP;
                _data.usersdatas.InsertOnSubmit(tmp);
                _data.SubmitChanges();
                Response.Redirect("userlist.aspx");

            }
        }
    }
}