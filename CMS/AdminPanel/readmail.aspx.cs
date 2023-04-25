using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class readmail : System.Web.UI.Page
    {

        public string pmtitle, pmText, pmName, pmMail, pmTell, pmTime, IP;
        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();
            try
            {
                DataAccessDataContext _data = new DataAccessDataContext();
                contactPm tmp = _data.contactPms.Single(a => a.ID == int.Parse(Request.QueryString["id"]));
                pmtitle ="مشاهده پیام: "+ tmp.Title;
                Title = pmtitle;
                pmText = tmp.Text;
                pmTell = tmp.Tell;
                IP = tmp.IP;
                pmMail = tmp.Email;
                pmName = tmp.FulName;
                if (!IsPostBack)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(tmp.Reply.Trim()))
                            TextBox4.Text = tmp.Reply.Replace("<br>", "\r\n");
                    }
                    catch { }
                }

                pmTime = CMS.CommonFunctions.String2date(tmp.RegDate, 3, "") + " ساعت: " + CMS.CommonFunctions.String2date(tmp.RegDate, 2, "H");
                tmp.IsRead = true;
                _data.SubmitChanges();
                if (tmp.Email == string.Empty)
                {
                    txt.Visible = false;
                    MessageBox1.Message = "اطلاعات ایمیل وارد نشده است. نمی توانید به این پیام پاسخ دهید";
                    MessageBox1.MessageType = HRaz.MessageBox.MessageType.Warning;
                    MessageBox1.Visible = true;
                }
            }
            catch
            {
                Response.Redirect("inbox.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            DataAccessDataContext _data = new DataAccessDataContext();
            contactPm tmp = _data.contactPms.Single(a => a.ID == int.Parse(Request.QueryString["id"]));
            tmp.Reply = TextBox4.Text.Replace("\r\n", "<br>");

            _data.SubmitChanges();


            if (SendMail(tmp.Email, tmp.Text, TextBox4.Text.Replace("\r\n", "<br>")))
                MessageBox1.Visible = true;
            else
            { 
                MessageBox1.Message = "ایمیل ارسال نشد. ممکن است تنظیمات ایمیل اشتباه باشد.";
                MessageBox1.MessageType = HRaz.MessageBox.MessageType.Warning;
                MessageBox1.Visible = true;
            }
        }
        bool SendMail(string Mail, string yourpmtext, string reply)
        {
            bool isSeant = false;
            DataAccessDataContext _data = new DataAccessDataContext();
            try
            { 
                string Body = "";
                MailMessage obMsg = new MailMessage();

                SmtpClient ob = new SmtpClient(CommonFunctions.GetSettingVal("mailservice"), int.Parse(CommonFunctions.GetSettingVal("mailserviceport")));

                MailAddress sendermail = new MailAddress(CommonFunctions.GetSettingVal("WebsiteMail"), CommonFunctions.GetSettingVal("WebsiteName"), System.Text.Encoding.UTF8);
                System.Net.NetworkCredential objNC = new System.Net.NetworkCredential(CommonFunctions.GetSettingVal("WebsiteMail"), CommonFunctions.GetSettingVal("WebsiteMailPass"));

                obMsg.From = sendermail;
                obMsg.Sender = sendermail;
                obMsg.BodyEncoding = System.Text.Encoding.UTF8;
                obMsg.SubjectEncoding = System.Text.Encoding.UTF8;
                if (CommonFunctions.GetSettingVal("mailservice").ToLower() == "smtp.gmail.com")
                    ob.EnableSsl = true;
                ob.Credentials = objNC;
                obMsg.ReplyTo= new MailAddress(CommonFunctions.GetSettingVal("ManagerMail"));
               

                System.IO.StreamReader st = new System.IO.StreamReader(Server.MapPath("~/AdminPanel/replymsg.html"));
                string title = "پاسخ به پیام شما در " + CommonFunctions.GetSettingVal("WebsiteName");

                Body = st.ReadToEnd()
                    .Replace("[[WebsiteUrl]]",CommonFunctions.GetSettingVal("WebsiteUrl"))
                    .Replace("[[WebsiteName]]",CommonFunctions.GetSettingVal("WebsiteName"))
                    .Replace("[[title]]", title)
                    .Replace("[[reply]]", reply)
                    .Replace("[[yourpmtext]]", yourpmtext);
                 
               
                obMsg.Subject = title;

                obMsg.From = sendermail;
                obMsg.Sender = sendermail;
                obMsg.To.Add(new MailAddress(Mail));
                obMsg.Body = Body;
                obMsg.IsBodyHtml = true;
                ob.Send(obMsg);





                isSeant = true;
            }
            catch {
                isSeant = false;
            }
            return isSeant;
        }
    }
}