using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class smssender : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();
            if (!IsPostBack)
            {
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    

                    try
                    {
                        string newmail = Request.QueryString["to"];
                        if (!string.IsNullOrEmpty(newmail) && newmail != "-----")
                            txtNums.Text = newmail;

                        if (Request.QueryString["touser"] != null)
                        {
                            txtNums.Text = _data.usersdatas.Single(a => a.ID == int.Parse(Request.QueryString["touser"])).Mobile;
                        }
                    }
                    catch { }
                }
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

               string txt = txtText.Text;
                string  number = txtNums.Text;
                if (string.IsNullOrEmpty(txt)  )
                {
                    MessageBox1.Message = "متن و شماره را وارد کنید..";
                    MessageBox1.MessageType = HRaz.MessageBox.MessageType.Error;
                    MessageBox1.Visible = true;
                    return;
                }
                bool stat = CommonFunctions.SendSMS(number, txt);
                if (stat)
                {
                    MessageBox1.Message = "ارسال شد.";
                    MessageBox1.MessageType = HRaz.MessageBox.MessageType.Submit;
                    MessageBox1.Visible = true;
                }
                else
                {
                    MessageBox1.Message = "ارسال نشد.";
                    MessageBox1.MessageType = HRaz.MessageBox.MessageType.Error;
                    MessageBox1.Visible = true;
                }
           
        }
    }
}