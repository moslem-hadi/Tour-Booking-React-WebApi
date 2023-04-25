using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using System.Threading;


namespace CMS.Manage
{
    public partial class emailsender : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    foreach (settingValue item in _data.settingValues.Where(a => a.Short == "websitename" ||
                    a.Short == "websitemail" || a.Short == "websitemailpass" || a.Short == "mailservice" 
                    || a.Short == "mailserviceport"))
                    {
                        switch (item.Short.ToLower())
                        {
                            case "websitemail":
                                txtEmail.Text = item.ShortValue;
                                break;

                            case "websitemailpass":
                                txtMailPass.Text = item.ShortValue;
                                break;

                            case "mailservice":
                                txtMailservice.Text = item.ShortValue;
                                break;

                            case "mailserviceport":
                                txtMailserviceport.Text = item.ShortValue;
                                break;
                            case "websitename":
                                txtName.Text = item.ShortValue;
                                break;
                        }
                    }


                    try
                    {
                        string newmail = Request.QueryString["to"];
                        if (!string.IsNullOrEmpty(newmail) && newmail!="-----")
                            TextBox2.Text = newmail;

                        if (Request.QueryString["touser"] != null)
                        {
                            TextBox2.Text = _data.usersdatas.Single(a => a.ID == int.Parse(Request.QueryString["touser"])).Email;
                        }
                    }
                    catch { }
                }
            }
        }
        bool IsEmail(string email)
        {
            const string MatchEmailPattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            if (email != null)
            {
                bool a = System.Text.RegularExpressions.Regex.IsMatch(email, MatchEmailPattern);
                return a;
            }
            else return false;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(TextBox1.Text.Trim()) || string.IsNullOrEmpty(TextBox2.Text.Trim()) ||
                string.IsNullOrEmpty(textarea1.Value.Replace("<br>", "").Trim()))
            {
                MessageBox1.Message = "عنوان و متن و گیرندگان را وارد کنید";
                MessageBox1.Visible = true;
                return;
            }

            MessageBox1.Visible = false;
            string[] mails = TextBox2.Text.Replace("-", ",").Replace("\r\n", ",").Replace("|", ",").Split(',');


            int delay = 0;
            int.TryParse(txtdelay.Text, out delay);
            int cnt = 0;
            int.TryParse(txtcnt.Text, out cnt);

            if (cnt == 0)
            {
                MessageBox1.Message = "تعداد ارسال در هربار را وارد کنید";
                MessageBox1.Visible = true;
                return;
            }
            string Body = "";
            string mailtitle = TextBox1.Text;
            string mailtext = textarea1.Value;


            int j = 0;







            MailMessage obMsg = new MailMessage();

            SmtpClient ob = new SmtpClient();

            ob.Host = txtMailservice.Text;
            ob.Port = int.Parse(txtMailserviceport.Text);
     
            string name = txtName.Text;
            string mailsender = txtEmail.Text;
            string mailpass = txtMailPass.Text;
            MailAddress sendermail = new MailAddress(mailsender, name, System.Text.Encoding.UTF8);
            System.Net.NetworkCredential objNC = new System.Net.NetworkCredential(mailsender, mailpass);

            obMsg.From = sendermail;
            obMsg.Sender = sendermail;
            obMsg.BodyEncoding = System.Text.Encoding.UTF8;
            obMsg.SubjectEncoding = System.Text.Encoding.UTF8;

            ob.Credentials = objNC;



            System.IO.StreamReader st = new System.IO.StreamReader(HttpContext.Current.Server.MapPath("~/commonmail.html"));

            if (CheckBox1.Checked)
                Body = st.ReadToEnd()
                    .Replace("[[WebsiteName]]", txtName.Text)
                    .Replace("[[WebsiteUrl]]", "http://nafisfile.com")
                    .Replace("[[title]]", mailtitle)
                    .Replace("[[text]]", mailtext);
            else
                Body = mailtext;
            obMsg.Subject = mailtitle;

            if (txtMailservice.Text.ToLower() == "smtp.gmail.com")
                ob.EnableSsl = true;
            obMsg.From = sendermail;
            obMsg.Sender = sendermail;
            obMsg.Body = Body;
            obMsg.IsBodyHtml = true;
            if (CheckBox2.Checked)
            {

                Thread threadSendMails;
                threadSendMails = new Thread(delegate()
                {
                    for (int i = 0; i < mails.Length; i++)
                    {
                        try
                        {
                            if (IsEmail(mails[i]))
                            {
                                obMsg.To.Clear();
                                obMsg.To.Add(new MailAddress(mails[i]));

                                ob.Send(obMsg);

                                
                                    j++;
                                    if (j >= cnt)
                                    {
                                        System.Threading.Thread.Sleep(delay * 60000);
                                        j = 0;
                                    }
                                
                            }

                        }
                        catch (Exception ex)
                        {
                        }

                    }

                });
                threadSendMails.IsBackground = true;
                threadSendMails.Start();


                MessageBox1.Message = "برای ارسال زمانبندی شد";
            }
            else
            {

                for (int i = 0; i < mails.Length; i++)
                {
                    try
                    {
                        if (IsEmail(mails[i]))
                        {
                            obMsg.To.Clear();
                            obMsg.To.Add(new MailAddress(mails[i]));

                            ob.Send(obMsg);
                            System.Threading.Thread.Sleep(100);
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox1.Message ="<div class='ltr'>"+ mails[i] + "<br>" + ex.Message + "<br>" + ex.ToString()+"</div>";
                        MessageBox1.Visible = true;
                        MessageBox1.MessageType = HRaz.MessageBox.MessageType.Error;
                        return;
                    }

                }


                MessageBox1.Message = "برای ارسال ذخیره شد";

            } 
            MessageBox1.Visible = true;
            MessageBox1.MessageType = HRaz.MessageBox.MessageType.Submit;
        }


      
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            //همه
            int i = 0;
            DataAccessDataContext _data = new DataAccessDataContext();
            string mails = "";
            foreach (usersdata item in _data.usersdatas)
            {
                mails += item.Email + ",";
                i++;
            }
            mails = mails.Remove(mails.Length - 1);
            TextBox2.Text = mails;
            Literal1.Text = "(" + i + " نفر)";
        }

    


    }
}