using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace CMS.member
{
    public partial class addticket : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            DataAccessDataContext _data = new DataAccessDataContext();
            CMS.member.membermaster MasterPage = (CMS.member.membermaster)Page.Master;
            MasterPage.Check_User();

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            DataAccessDataContext _data = new DataAccessDataContext();
            if (string.IsNullOrWhiteSpace(txtOnvan.Text) || string.IsNullOrWhiteSpace(txtText.Text))
            {
                ltr_error.Text = "<div class='warning'>فیلدهای لازم را پر کنید.</div>";
                ltr_error.Visible = true;
                return;
            }
            string fileName = "";
            if (!fluFile.HasFile)
            { fileName = "none"; }
            else
            {
                fileName = fluFile.FileName;
                string fileExt = Path.GetExtension(fileName);
                if (!checkFileType(fileName))
                {
                    ltr_error.Text = "<div class='warning'>پسوند فایل ارسالی باید یکی از پسوندهای زیر باشد:<br> zip, rar, doc, docx, png, jpg, gif, jpeg</div>";

                    ltr_error.Visible = true;
                    return;
                }
                fileName = Guid.NewGuid().ToString();

                fileName += fileExt;
             
                try
                {
                    fluFile.SaveAs(Server.MapPath("~/content/temp/") + fileName);
                }
                catch
                {

                    ltr_error.Text = "<div class='warning'>پوشه content قابل نوشتن نیست. بررسی کنید.</div>";
                    ltr_error.Visible = true;
                    return;
                }
            }
            
            ticket tmp = new ticket();
            tmp.IsManageRead = false;
            tmp.IsRead = true;
            tmp.Part = "پشتیبانی کاربران";
            tmp.Priority = DropDownList2.SelectedValue;
            tmp.RegDate = DateTime.Now;
            tmp.Status = 1;
           // tmp.Text = txtText.Text.Replace("\r\n", "<br>");
            tmp.Title = CommonFunctions.GetPlainTextFromHtml(txtOnvan.Text);
            tmp.IsDeleted = false;
            tmp.LastUpdate = DateTime.Now;
            tmp.FileName = fileName;
            tmp.UserID = ((AKUserClass)(Session["User"])).THisUserID;
            _data.tickets.InsertOnSubmit(tmp);
            _data.SubmitChanges();





            ticketReply tmpreply = new ticketReply();

            tmpreply.IsManageReply = false;
            tmpreply.SendDate = DateTime.Now;
            tmpreply.Text = CommonFunctions.GetPlainTextFromHtml(txtText.Text).Replace("\r\n", "<br>");
            tmpreply.TicketID = tmp.ID;
            _data.ticketReplies.InsertOnSubmit(tmpreply);
            _data.SubmitChanges();



            newticket.Visible = false;
            try
            {
                CommonFunctions.SendMail(CommonFunctions.GetSettingShortValue("ManagerMail"), "تیکت جدید شماره " + tmp.ID, "تیکت جدید در سایت ارسال شده است. برای مشاهده آن روی لینک زیر کلیک کنید:<br> <p style='diretction:ltr;text-align:left'>" + CommonFunctions.GetSettingShortValue("WebsiteUrl") + "/adminpanel/ticketlist.aspx</p>", true, "","");
            }
            catch { }
            ltr_error.Text = string.Format("<div class='empty' style='font-size: 16px;color: #509c02;'>تیکت شماره {0} ایجاد شد. پس از بررسی در صورت لزوم پاسخ داده می شود. <br><a href='ticketlist.aspx'>لیست تیکت ها</a></div>", tmp.ID);

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