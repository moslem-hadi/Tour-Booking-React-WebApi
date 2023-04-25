using System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class viewticket : System.Web.UI.Page
    {

        public string pmtitle, pmText, pmName, pmMail, pmTell, pmTime, UserID, IsManageReply;
        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();

            DataAccessDataContext _data = new DataAccessDataContext();
            try
            {
                ticket tmp = _data.tickets.Single(a => a.ID == int.Parse(Request.QueryString["id"]));

                pmText = tmp.Text;
                Title = tmp.Title;
                pmtitle = tmp.Title;
               //IsManageReply=tmp.
                UserID = ((int)tmp.UserID).ToString();
                pmTime = CMS.CommonFunctions.String2date(tmp.RegDate, 3, "") + " ساعت: " + CMS.CommonFunctions.String2date(tmp.RegDate, 2, "H");
                tmp.IsManageRead = true;
                _data.SubmitChanges();
                if (tmp.FileName != "none")
                {
                    ltrattach.Text = "<a href='/content/temp/" + tmp.FileName + "'> <i class='icon-download-alt'></i> فایل ضمیمه</a>";

                }
                else divattach.Visible = false;

                if ((bool)tmp.IsDeleted)
                {
                    txt.Visible = false;
                    MessageBox1.Visible = true;
                    MessageBox1.Message = "تیکت توسط کاربر حذف شده، نمی  توانید پاسخ دهید.";
                    MessageBox1.MessageType = HRaz.MessageBox.MessageType.Error;
                }

            }
            catch { Response.Redirect("ticketlist.aspx"); }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DataAccessDataContext _data = new DataAccessDataContext();
            ticketReply tmp = new ticketReply();

            //string fileName = "none";
            //if (!fluFile.HasFile)
            //{ fileName = "none"; }
            //else
            //{
            //    fileName = fluFile.FileName;
            //    string fileExt = Path.GetExtension(fileName);
            //    if (!checkFileType(fileName))
            //    {
            //        MessageBox1.Message = "<div class='warning'>پسوند فایل ارسالی باید یکی از پسوندهای زیر باشد:<br> zip, rar, doc, docx, png, jpg, gif, jpeg</div>";

            //        MessageBox1.Visible = true;
            //        return;
            //    }

            //    fileName = Guid.NewGuid().ToString();
            //    fileName += fileExt;
            //    try
            //    {
            //        fluFile.SaveAs(Server.MapPath("~/content/temp/") + fileName);
            //    }
            //    catch
            //    {

            //        MessageBox1.Message= "پوشه content قابل نوشتن نیست. بررسی کنید";
            //        MessageBox1.Visible = true;
            //        return;
            //    }
            //}



            ticket tmpticket = _data.tickets.Single(a => a.ID == int.Parse(Request.QueryString["id"]) && (bool)a.IsDeleted == false);
            tmpticket.LastUpdate = DateTime.Now;
            tmpticket.Status = 2;
            tmpticket.IsRead = false;
            _data.SubmitChanges();

             

            tmp.IsManageReply = true;
            tmp.SendDate = DateTime.Now;
            tmp.Text = txtText.Text.Replace("\r\n", "<br>");
            tmp.TicketID = int.Parse(Request.QueryString["id"]);
            _data.ticketReplies.InsertOnSubmit(tmp);
            _data.SubmitChanges();
            txtText.Text = "";
            usersdata tmpuser = _data.usersdatas.FirstOrDefault(a => a.ID == tmpticket.UserID);

            string txt = $"پاسخ جدید به تیکت شماره {tmpticket.ID} در سایت ارسال شده است.<br>متن پاسخ: {tmp.Text}<br><br> برای مشاهده و پاسخ به تیتک روی لینک زیر کلیک کنید:<br> <p style='diretction:ltr;text-align:left'><a href='https://parhantransfer.ir/member/viewticket.aspx?id={tmpticket.ID}'>https://parhantransfer.ir/member/viewticket.aspx?id={tmpticket.ID}</a></p>";
            CommonFunctions.SendMail(tmpuser.Email, "پاسخ جدید به تیکت شماره " + tmpticket.ID, txt, true, "","");

           
                string smstext = string.Format("تیکت به شماره {0} پاسخ داده شد\r\nپرهان ترانسفر  parhantransfer.ir", tmp.ID);
                CommonFunctions.SendSMS(tmpuser.Mobile, smstext);
                _data.SubmitChanges();
         

            Response.Redirect("viewticket.aspx?id=" + tmpticket.ID);
        }


        private bool checkFileType(string fileName)
        {
            string fileExt = Path.GetExtension(fileName).ToLower();

            List<string> lst = new List<string>() {
                ".rar",
                ".zip",
                ".7z",
                ".psd",
                ".doc",
                ".docx",
                ".xls",
                ".xlsx",
                ".ppt",
                ".pptx",
                ".zip",
                ".rar",
                ".7z",
                ".doc",
                ".docx",
                ".png",
                ".jpg",
                ".gif",
                ".jpeg",
                ".pdf",
                ".ppt",
                ".pptx",
                ".xls",
                ".xlsx",
                ".psd",
                ".mp3",
                ".mp4",
                ".mpeg",
                ".mkv",
                ".avi"


            };

            if (lst.Contains(fileExt.ToLower()))
                return true;
            else
                return false;

        }
    }
}