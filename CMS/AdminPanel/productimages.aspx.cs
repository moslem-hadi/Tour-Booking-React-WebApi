using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

namespace CMS.Manage
{
    public partial class productimages : System.Web.UI.Page
    {


        
        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();

            if (!IsPostBack)
            { 
                int id = 0;
                int.TryParse(Request.QueryString["id"], out id); 
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    string tlt = _data.Products.Single(a => a.ID == id).Title;
                    ltrtitle.Text =  tlt;
                }
            }
        }


         
        protected void Button1_Click(object sender, EventArgs e)
        {
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                MessageBox1.Visible = false;

                int id = 0;
                int.TryParse(Request.QueryString["id"], out id);
                int uploadcnt = 0; 
                if (!(FileUpload1.HasFile || FileUpload1.FileBytes.Count() < 0))
                {
                    MessageBox1.Message = "تصویری برای ارسال انتخاب کنید.";
                    MessageBox1.Visible = true;
                    return;
                }
                else
                {

                    if (string.IsNullOrEmpty(txtTitle.Text))
                        txtTitle.Text = ltrtitle.Text;
                    string foldername = DateTime.Now.ToString("yyyyMM") + "/";
                    string path = Server.MapPath("~/content/productpic/gallery/") + foldername;

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    string productslug = _data.Products.Single(a => a.ID == id).Slug;
                    foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
                    {
                        string fileName = postedFile.FileName;
                        if (!checkFileType(fileName))
                        { 
                            MessageBox1.Message = "پسوند تصویر ارسالی باید یکی از پسوندهای زیر باشد:<br> PNG , GIF , JPG , JPEG.";
                            MessageBox1.Visible = true;
                            continue;
                        }
                        string fileExt = Path.GetExtension(fileName);
                        if (chbChangeName.Checked)
                        {
                            fileName = productslug + "_" + Guid.NewGuid().ToString("N").Remove(8) + fileExt;

                        }

                        else {
                            if (File.Exists(path + fileName))
                            {
                                fileName = fileName.Remove(fileName.LastIndexOf('.'));
                                fileName = CMS.CommonFunctions.SubStringText(fileName, 0, 160);
                                fileName = fileName + "_" + Guid.NewGuid().ToString("N").Remove(6);
                                fileName = fileName.Replace('%', '-').Substring(fileName.LastIndexOf("\\") + 1);
                                fileName = fileName.Replace('#', '-').Substring(fileName.LastIndexOf("\\") + 1);
                                fileName = fileName.Replace('@', '-').Substring(fileName.LastIndexOf("\\") + 1);
                                fileName = fileName.Replace(',', '-').Substring(fileName.LastIndexOf("\\") + 1);
                                fileName = fileName.Replace('&', '-').Substring(fileName.LastIndexOf("\\") + 1);
                                fileName = fileName.Replace('=', '-').Substring(fileName.LastIndexOf("\\") + 1);
                                fileName = fileName.Replace(' ', '-').Substring(fileName.LastIndexOf("\\") + 1);

                                fileName += fileExt;
                            }
                        }
                        try
                        {
                            postedFile.SaveAs(path+ fileName);
                            uploadcnt++;
                        }
                        catch
                        {
                            continue;
                        }


                        ProductGallery tmp = new ProductGallery();
                        tmp.Title = txtTitle.Text.Trim();
                      //  tmp.Description = txtDesc.Text.Trim();
                        tmp.FileName = foldername+ fileName;
                        tmp.ProductID = id;
                        _data.ProductGalleries.InsertOnSubmit(tmp);
                        _data.SubmitChanges();



                    }





                    MessageBox1.Message = uploadcnt + " عکس آپلود شد.";
                    MessageBox1.MessageType = HRaz.MessageBox.MessageType.Submit;
                    MessageBox1.Visible = true;
                    ListView1.DataBind();
                }
            }
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

        protected void Button2_Click(object sender, EventArgs e)
        {


            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
              
                int id = 0;
                int.TryParse(ltrID.Text, out id);

                ProductGallery tmp = _data.ProductGalleries.Single(a => a.ID == id);
                string AksName = tmp.FileName;
                string productslug = _data.Products.Single(a => a.ID == id).Slug;
                if (FileUpload1.HasFile)
                {

                    string fileName = FileUpload1.FileName;
                    if (!checkFileType(fileName))
                    {

                        MessageBox1.Message = "پسوند تصویر ارسالی باید یکی از پسوندهای زیر باشد:<br> PNG , GIF , JPG , JPEG.";
                        MessageBox1.Visible = true;
                        return;
                    }
                    string fileExt = Path.GetExtension(fileName);


                    string foldername = DateTime.Now.ToString("yyyyMM") + "/";
                    string path = Server.MapPath("~/content/productpic/gallery/") + foldername;

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    AksName = fileName;
                    if (chbChangeName.Checked)
                    {
                        AksName = productslug + "_" + Guid.NewGuid().ToString("N").Remove(8)+fileExt;

                    }
                    else {

                        if (File.Exists(path + AksName))
                        {
                            AksName = AksName.Remove(AksName.LastIndexOf('.'));
                            AksName = CMS.CommonFunctions.SubStringText(AksName, 0, 160);

                            AksName = AksName + "_" + (Guid.NewGuid()).ToString("N").Remove(6);
                            AksName = AksName.Replace('%', '-').Substring(AksName.LastIndexOf("\\") + 1);
                            AksName = AksName.Replace('#', '-').Substring(AksName.LastIndexOf("\\") + 1);
                            AksName = AksName.Replace('@', '-').Substring(AksName.LastIndexOf("\\") + 1);
                            AksName = AksName.Replace(',', '-').Substring(AksName.LastIndexOf("\\") + 1);
                            AksName = AksName.Replace('&', '-').Substring(AksName.LastIndexOf("\\") + 1);
                            AksName = AksName.Replace('=', '-').Substring(AksName.LastIndexOf("\\") + 1);
                            AksName = AksName.Replace(' ', '-').Substring(AksName.LastIndexOf("\\") + 1);

                            AksName += fileExt;
                        }
                    }

                    try
                    {
                        FileUpload1.SaveAs(path + AksName);
                    }
                    catch
                    {
                        MessageBox1.Message = "پوشه content/productpic/gallery قابل نوشتن نیست. بررسی کنید.";
                        MessageBox1.MessageType = HRaz.MessageBox.MessageType.Error;
                        MessageBox1.Visible = true;
                        return;
                    }
                    if (System.IO.File.Exists(Server.MapPath("~/content/productpic/gallery/") + tmp.FileName))
                        {
                            try
                            {
                                System.IO.File.Delete(Server.MapPath("~/content/productpic/gallery/") + tmp.FileName);
                            }
                            catch (Exception ex) { }
                        }

                    tmp.FileName = foldername+ AksName;
                }
                tmp.Title = txtTitle.Text.Trim();
              //  tmp.Description = txtDesc.Text.Trim();
                _data.SubmitChanges();
                Response.Redirect(Request.RawUrl);
            }
        }
         
        

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int index = e.NewPageIndex + 1;
            string url = Request.Url.GetLeftPart(UriPartial.Path);

            e.Cancel = true; ;
            Response.Redirect(string.Format("{0}?page={1}", url, index));
        } 


        protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "del")
            {

                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    int id = int.Parse(e.CommandArgument.ToString());
                    ProductGallery tmp = _data.ProductGalleries.Single(a => a.ID == id);
                    
                    if (System.IO.File.Exists(Server.MapPath("~/content/productpic/gallery/") + tmp.FileName))
                    {
                        try
                        {
                            string path = Server.MapPath("~/content/productpic/gallery/") + tmp.FileName;
                            FileInfo file = new FileInfo(path);
                            System.Drawing.Image img = System.Drawing.Image.FromFile(path);

                            img.Dispose(); 
                            file.Delete();
                             
                        }
                        catch { }
                    }
                    _data.ProductGalleries.DeleteOnSubmit(tmp);
                    _data.SubmitChanges();
                    Response.Redirect(Request.RawUrl);
                }
            }
            if (e.CommandName == "edt")
            {
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    int id = int.Parse(e.CommandArgument.ToString());
                    ProductGallery tmp = _data.ProductGalleries.Single(a => a.ID == id);
                    //txtDesc.Text = tmp.Description;
                    txtTitle.Text = tmp.Title;
                    ltrID.Text = tmp.ID.ToString();
                    Button1.Visible = false;
                    edt_btns.Visible = true;
                    ltrinserttitle.Text = "ویرایش تصویر";
                    oldimg.ImageUrl = "/HPicturer.ashx?img=~/content/productpic/gallery/" + tmp.FileName + "&w=105&h=110";
                    oldpic.Visible = true;
                }

            }
        }
    }
}