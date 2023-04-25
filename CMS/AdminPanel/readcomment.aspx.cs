using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class readcomment : System.Web.UI.Page
    {

        public string pmtitle, pmText, pmName, pmMail, pmTell, pmTime, link, rating;
        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();
            try
            {
                DataAccessDataContext _data = new DataAccessDataContext();
                ProductComment tmp = _data.ProductComments.Single(a => a.ID == int.Parse(Request.QueryString["id"]));
                pmtitle = "مشاهده پیام: " + tmp.Title;
                Title = pmtitle;
                pmText = tmp.Text;
                usersdata tmuser = _data.usersdatas.Single(a => a.ID == tmp.UserID);

                pmName = "<a href='userdetail.aspx?id="+tmuser.ID+"'>" + tmuser.FullName +"</a>";
                

                rating = tmp.Rating.ToString();
                try
                {
                    Product tmpppro = _data.Products.Single(a => a.ID == tmp.ProductID);

                    link = string.Format("<a href='/product/{0}/{1}'>{2}</a>", tmpppro.ID, tmpppro.Slug,tmpppro.Title);
                }catch{
                    link = "محصول وجود ندارد ";
                }
                if (!IsPostBack)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(tmp.Reply.Trim()))
                            TextBox4.Text = tmp.Reply.Replace("<br>", "\r\n");
                    }
                    catch { }
                }

                pmTime = CMS.CommonFunctions.String2date(tmp.RegDate, 2, "D") + " ساعت: " + CMS.CommonFunctions.String2date(tmp.RegDate, 2, "H");
                tmp.IsRead = true;
                _data.SubmitChanges();
                 
            }
            catch
            {
                Response.Redirect("comment.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            DataAccessDataContext _data = new DataAccessDataContext();
            ProductComment tmp = _data.ProductComments.Single(a => a.ID == int.Parse(Request.QueryString["id"]));
            usersdata tmuser = _data.usersdatas.Single(a => a.ID == tmp.UserID);
            string pmtext = "", mailtext = ""; ;
            if (tmp.ProductID != 0)
            {
                var productTitle = _data.Products.Single(a => a.ID == tmp.ProductID).Title;
                pmtext = "برای نظر شما در مورد «" + productTitle + "» در محصولنارگل پیامی ارسال شده است\r\nvillanargol.ir";
                mailtext = "پاسخی به پرسش شما در محصولنارگل ارسال شد. برای مشاهده پرسش و پاسخ آن به صفحه کالا مراجعه کنید.<br><a href='http://villanargol.ir/product/" + tmp.ProductID + "/m'>اینجا کلیک کنید</a>";
            }
            else {
                pmtext = "برای پیام شما در محصولنارگل جوابی ارسال شده است\r\nvillanargol.ir";
                mailtext = "پاسخی به نظر شما در محصولنارگل ارسال شد. برای مشاهده پرسش و پاسخ آن به سایت مراجعه کنید.<br><a href='http://villanargol.ir/'>اینجا کلیک کنید</a>";
            }
            if (!string.IsNullOrEmpty(TextBox4.Text.Trim()))
                tmp.Reply = TextBox4.Text.Replace("\r\n", "<br>");
           
                tmp.IsOk = true;
            _data.SubmitChanges();

            if (CheckBox1.Checked)
            {
                CommonFunctions.SendSMS(tmuser.Mobile,pmtext);

                if (CommonFunctions.IsEmail(tmuser.Email))
                {
                    CommonFunctions.SendMail(tmuser.Email, pmtext, mailtext, true, "","");

                }
            }
            MessageBox1.Message = "پاسخ ارسال شد.";
            MessageBox1.MessageType = HRaz.MessageBox.MessageType.Warning;
            MessageBox1.Visible = true;

            //if (SendMail(tmp.Email, tmp.Text, TextBox4.Text.Replace("\r\n", "<br>")))
            //    MessageBox1.Visible = true;
            //else
            //{
            //    MessageBox1.Message = "ایمیل ارسال نشد. ممکن است تنظیمات ایمیل اشتباه باشد.";
            //    MessageBox1.MessageType = HRaz.MessageBox.MessageType.Warning;
            //    MessageBox1.Visible = true;
            //}
        }
    }
}