using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.member
{
    public partial class viewticket : System.Web.UI.Page
    {

        public string pmtitle, pmText, pmTime;
        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.member.membermaster MasterPage = (CMS.member.membermaster)Page.Master;
            MasterPage.Check_User();

            DataAccessDataContext _data = new DataAccessDataContext();
            try
            {
                ticket tmp = _data.tickets.Single(a => a.ID == int.Parse(Request.QueryString["id"]) && (bool)a.IsDeleted == false);

                if (tmp.UserID != ((AKUserClass)(Session["User"])).THisUserID)
                    Response.Redirect("ticketlist.aspx");

                pmText = tmp.Text;
                pmtitle = tmp.Title;
                pmTime = CMS.CommonFunctions.String2date(tmp.RegDate, 3, "") + " ساعت: " + CMS.CommonFunctions.String2date(tmp.RegDate, 2, "H");
                tmp.IsRead = true;
                _data.SubmitChanges();

                if (tmp.FileName != "none")
                    ltrattach.Text = "<div class='ticket' ><a href='/content/temp/" + tmp.FileName + "' style='display:block;'> <i class='icon-download-alt'></i> فایل ضمیمه</a></div>";

                if (tmp.Status.ToString() == "3")
                {
                    ltrnorespone.Visible = true;
                    response.Visible = false;
                }

            }
            catch { Response.Redirect("ticketlist.aspx"); }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DataAccessDataContext _data = new DataAccessDataContext();
            ticketReply tmp = new ticketReply();

             

            ticket tmpticket = _data.tickets.Single(a => a.ID == int.Parse(Request.QueryString["id"]) && (bool)a.IsDeleted == false);
            tmpticket.LastUpdate = DateTime.Now;
            tmpticket.Status = 1;
            tmpticket.IsManageRead = false;
            _data.SubmitChanges();
             
            tmp.IsManageReply = false;
            tmp.SendDate = DateTime.Now;
            tmp.Text = CommonFunctions.GetPlainTextFromHtml(TextBox1.Text).Replace("\r\n", "<br>");
            tmp.TicketID = int.Parse(Request.QueryString["id"]);
            _data.ticketReplies.InsertOnSubmit(tmp);
            _data.SubmitChanges();

            try
            {
                CommonFunctions.SendMail(CommonFunctions.GetSettingShortValue("ManagerMail"), "پاسخ جدید به تیکت شماره " + tmpticket.ID, "پاسخ جدید به تیکت در سایت ارسال شده است. برای مشاهده آن روی لینک زیر کلیک کنید:<br> <p style='diretction:ltr;text-align:left'>" + CommonFunctions.GetSettingShortValue("WebsiteUrl") + "/adminpanel/viewticket.aspx?id=" + tmpticket.ID + "</p>", true, "","");

            }
            catch { }


            Response.Redirect("viewticket.aspx?id="+ tmpticket.ID);

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