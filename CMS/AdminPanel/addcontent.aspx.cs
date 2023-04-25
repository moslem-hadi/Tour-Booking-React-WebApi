using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;

namespace CMS.Manage
{
    public partial class addcontent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();
        } 

        private bool checkFileType(string fileName)
        {
            string fileExt = Path.GetExtension(fileName);
            switch (fileExt.ToLower())
            {
                case ".gif":
                    return true;
                case ".png":
                    return true;
                case ".jpg":
                    return true;
                case ".jpeg":
                    return true;
                default:
                    return false;

            }

        }
         
        protected void Button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtTitle.Text) || string.IsNullOrWhiteSpace(txtShort.Text) || string.IsNullOrWhiteSpace(txtText.Value))
            {
                MessageBox1.Message = "فیلدهای لازم را پر کنید.";
                MessageBox1.Visible = true;
                return;
            }
           
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {

                string shortvalue = CMS.CommonFunctions.ReplaceSpace(txtShort.Text.Trim());
                if (string.IsNullOrWhiteSpace(shortvalue))
                {
                    shortvalue = CMS.CommonFunctions.ReplaceSpace(txtTitle.Text);
                }
                string imgName = "nopic.png";
                if (FileUpload1.HasFile)
                {
                    string fileName = imgName = FileUpload1.FileName;
                    string fileExt = Path.GetExtension(fileName);
                    if (!checkFileType(fileName))
                    {
                        MessageBox1.Message = "پسوند تصویر ارسالی باید یکی از پسوندهای زیر باشد:<br> PNG , GIF , JPG , JPEG";
                        MessageBox1.MessageType = HRaz.MessageBox.MessageType.Error;
                        MessageBox1.Visible = true;
                        return;
                    }


                    fileName = Guid.NewGuid()+ fileExt;
                        
                    
                    try
                    { 
                        System.Drawing.Image image_file = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);

                        Bitmap bitmap_file = new Bitmap(CommonFunctions.FixedSize(image_file, 400, 300, true), 400, 300);
                        bitmap_file.Save(Server.MapPath("~/content/images/") + fileName);
                    }
                    catch
                    {

                        MessageBox1.Message = "پوشه content/images قابل نوشتن نیست. بررسی کنید.";
                        MessageBox1.MessageType = HRaz.MessageBox.MessageType.Error;
                        MessageBox1.Visible = true;
                        return;
                    }
                    imgName = fileName;
                }
                if (string.IsNullOrWhiteSpace(imgName))
                    imgName = "nopic.png";






                 

                string KeyWords = "";


                Content tmp = new Content();
                tmp.Text = txtText.Value;
                tmp.Title = txtTitle.Text.Trim();
                 
                tmp.GroupID = 0;
                tmp.SubGroupID = 0;

                tmp.RegDate = CommonFunctions.String2date(DateTime.Now,2,"n");
                tmp.Pic = imgName; 
                if (string.IsNullOrEmpty(txtDesc.Text.Trim()))
                    tmp.Description = CMS.CommonFunctions.SubStringHtml(txtText.Value, 0, 150);
                else
                    tmp.Description = txtDesc.Text;
                  

                tmp.Short = shortvalue;
              
                tmp.ViewCount = 0;
                tmp.Short = CMS.CommonFunctions.ReplaceSpace(txtShort.Text);
                _data.Contents.InsertOnSubmit(tmp);
                _data.SubmitChanges();


                Response.Redirect("contentlist.aspx");

            }
        }
    }
}