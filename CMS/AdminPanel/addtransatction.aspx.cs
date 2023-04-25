using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class addtransatction : System.Web.UI.Page
    {
        static readonly object _object = new object();
        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();

            if (!IsPostBack)
            {
                if(Request.QueryString["id"] != null)
                {
                DataAccessDataContext _data = new DataAccessDataContext();
                    var userid = int.Parse(Request.QueryString["id"]);
                    var user = _data.usersdatas.FirstOrDefault(a => a.ID == userid);
                    if (user != null)
                    {
                        txtuserID.Text = userid.ToString();
                        txtuserID.Visible = false;
                        ltrUserName.Text ="کد "+userid+" - "+ user.FullName + " <span style='color:#3fad1c; display:inline-block; margin-right:10px'>" + CommonFunctions.UserTypeName(user.UserType)+"</span>";
                    }

                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            lock (_object)
            {

                if (!string.IsNullOrEmpty(txtMablagh.Text) && !string.IsNullOrEmpty(txtTitle.Text)
                 && !string.IsNullOrEmpty(txtuserID.Text))
                {
                    DateTime datettime = DateTime.Now;
                    DataAccessDataContext _data = new DataAccessDataContext();
                    var user = _data.usersdatas.FirstOrDefault(a => a.ID == int.Parse(txtuserID.Text));
                    if (user == null)
                    {
                        MessageBox1.Message = "کد کاربر اشتباه است.";
                        MessageBox1.MessageType = HRaz.MessageBox.MessageType.Error;
                        MessageBox1.Visible = true;
                        return;
                    }
                    int money = int.Parse(txtMablagh.Text);
                    bool add = bool.Parse(ddlType.SelectedItem.Value);

                    CreditTransaction tmp = new CreditTransaction();
                    var type = bool.Parse(ddlType.SelectedItem.Value);


                    var done = CommonFunctions.AddTransaction(txtTitle.Text, type ? CreditTransactionType.Plus: CreditTransactionType.Minus,
                        user.ID, money, CommonFunctions.CreditEneity.AdminAdd);

                    if (!done)
                    {
                        MessageBox1.Message = "خطا در ذخیره تراکنش.. لطفا تراکنش ها را بررسی کنید و مجددا امتحان نمایید.";
                        MessageBox1.MessageType = HRaz.MessageBox.MessageType.Error;
                        MessageBox1.Visible = true;
                        return;

                    }

                    _data.SubmitChanges();
                    Response.Redirect("translist.aspx");
                }
            }
        }
    }
}