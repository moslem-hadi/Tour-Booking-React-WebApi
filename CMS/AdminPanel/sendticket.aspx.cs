using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace CMS.Manage
{
    public partial class sendticket : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            DataAccessDataContext _data = new DataAccessDataContext();
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();
            if (Request.QueryString["to"] != null)
                txtusers.Text = Request.QueryString["to"];

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            DataAccessDataContext _data = new DataAccessDataContext();
            if (string.IsNullOrWhiteSpace(txtTitle.Text) || string.IsNullOrWhiteSpace(txtText.Value))
            {
                MessageBox1.Message = "فیلدهای لازم را پر کنید.";
                MessageBox1.Visible = true;
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
                    MessageBox1.Message = "پسوند فایل ارسالی باید یکی از پسوندهای زیر باشد:<br> zip, rar, doc, docx, png, jpg, gif, jpeg";

                    MessageBox1.Visible = true;
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

                    MessageBox1.Message = "پوشه content قابل نوشتن نیست. بررسی کنید.";
                    MessageBox1.Visible = true;
                    return;
                }
            }

            string[] users = (txtusers.Text.Replace("-", ",").Replace("،", ",").Replace("\r\n", ",")).Split(',');
            foreach (string item in users)
            {
                int userid = 0;
                int.TryParse(item, out userid);
                if (userid != 0)
                {
                    if (_data.usersdatas.Where(a => a.ID == userid).Count() != 0)
                    {

                        ticket tmp = new ticket();
                        tmp.IsManageRead = false;
                        tmp.IsRead = false;
                        tmp.Part = DropDownList1.SelectedItem.Value;
                        tmp.Priority = DropDownList2.SelectedValue;
                        tmp.RegDate = DateTime.Now;
                        tmp.Status = 2;
                        // tmp.Text = txtText.Text.Replace("\r\n", "<br>");
                        tmp.Title = txtTitle.Text;
                        tmp.IsDeleted = false;
                        tmp.LastUpdate = DateTime.Now;
                        tmp.UserID = userid;

                        tmp.FileName = fileName;
                        _data.tickets.InsertOnSubmit(tmp);
                        _data.SubmitChanges();





                        ticketReply tmpreply = new ticketReply();

                        tmpreply.IsManageReply = true;
                        tmpreply.SendDate = DateTime.Now;
                        tmpreply.Text = txtText.Value;
                        tmpreply.TicketID = tmp.ID;
                        _data.ticketReplies.InsertOnSubmit(tmpreply);
                        _data.SubmitChanges();




                        usersdata tmpuser = _data.usersdatas.Single(a => a.ID == userid);

                        CommonFunctions.SendMail(tmpuser.Email, "تیکت جدید شماره" + tmp.ID,
                            string.Format("تیکت جدید برای شما در سایت ارسال شده است. برای مشاهده آن روی لینک زیر کلیک کنید:<br> <p style='diretction:ltr;text-align:left'><a href='{0}'>{0}</a></p>", CommonFunctions.GetSettingVal("WebsiteUrl") + "/member/viewticket.aspx?id=" + tmp.ID), true, "", "");

                            string smstext = string.Format("دریافت تیکت از سوی مدیر:\r\nیک تیکت به شماره {0} برای شما ارسال شد\r\nپرهان ترانسفر  parhantransfer.ir",tmp.ID);
                            CommonFunctions.SendSMS(tmpuser.Mobile, smstext);

                    }
                }
            }


            MessageBox1.Message = "تیکت جدید ایجاد شد. <a href='ticketlist.aspx'>لیست تیکت ها</a>";
            MessageBox1.Visible = true;
            MessageBox1.MessageType = HRaz.MessageBox.MessageType.Submit;

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