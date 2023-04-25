using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Services;
using System.Collections;
using System.Drawing;

namespace CMS.Manage
{
    public partial class addproduct : System.Web.UI.Page
    {
        public ProductType productType  ;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (RouteData.Values["type"] == null)
                    Response.Redirect("/adminpanel/selecttype.aspx");
          var type= RouteData.Values["type"] as string;

            switch (type.ToLower())
            {
                case "gasht":
                    productType = ProductType.Gasht;
                    break;
                case "transfer":
                    productType = ProductType.Transfer;
                    break;
                case "tour":
                    productType = ProductType.Tour;
                    break;
                default:
                    Response.Redirect("productlist.aspx");
                    break;
            }

            if (!IsPostBack)
            {
                sdsHamrahiType.SelectParameters["type"].DefaultValue = ((int)GlobalValueTypes.Hamrahi).ToString();
                sdsVehicleType.SelectParameters["type"].DefaultValue = ((int)GlobalValueTypes.Vehicle).ToString();
                sdsPlaceType.SelectParameters["type"].DefaultValue = ((int)GlobalValueTypes.Place).ToString();
                sdsActivityType.SelectParameters["type"].DefaultValue = ((int)GlobalValueTypes.ActivityType).ToString();
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {

                    if (_data.ProductGroups.Count() == 0)
                    {
                        ltr_noGroup.Text = "<span style='color:#ff6666;padding: 5px 7px;background:#fff;border:1px solid #dcdcdc' class='rounded'>هیج گروهی وجود ندارد. <a href='ProductGroup.aspx'>افزودن گروه</a></span><br>";
                        ddlgroup.Visible = false;
                        Button1.Visible = false;
                        return;
                    }
                }
                getgroup(0, "");
            }
        }

        void getgroup(int id, string level)
        {
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                foreach (ProductGroup item in _data.ProductGroups.Where(a => a.ParentID == id))
                {
                    if (item.ParentID == 0)
                    {

                        ListItem lst = new ListItem(level + " " + item.Title, item.ID.ToString());
                        lst.Attributes.Add("style", "color: #fff; background: #68b800;");
                        ddlgroup.Items.Add(lst);
                    }
                    else
                    {
                        ListItem lst = new ListItem(level + " " + item.Title, item.ID.ToString());
                        ddlgroup.Items.Add(lst);
                    }
                    getgroup(item.ID, level + " —");
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;
            if (ddlgroup.SelectedValue == "-1")
            {

                MessageBox1.Message = "گروه را انتخاب کنید.";
                MessageBox1.Visible = true;
                return;
            }
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox1.Message = "فیلدهای لازم را پر کنید.";
                MessageBox1.Visible = true;
                return;
            }


            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                string foldername = DateTime.Now.ToString("yyyyMM") +"/";
                 
                string path= Path.Combine(Server.MapPath("~/content/productpic/"), foldername);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string imgName = "nopic.png";
                if (fulMainPic.HasFile)
                {
                    string fileName = imgName = fulMainPic.FileName;
                    string fileExtpic = Path.GetExtension(fileName);
                    if (!checkFileType(fileName))
                    {
                        MessageBox1.Message = "پسوند تصویر ارسالی باید یکی از پسوندهای زیر باشد:<br> PNG , GIF , JPG , JPEG";
                        MessageBox1.MessageType = HRaz.MessageBox.MessageType.Error;
                        MessageBox1.Visible = true;
                        return;
                    }

                    fileName =Guid.NewGuid().ToString()+ fileExtpic;
                   
                    try
                    {

                        System.Drawing.Image image_file = System.Drawing.Image.FromStream(fulMainPic.PostedFile.InputStream);

                        Bitmap bitmap_file = new Bitmap(CommonFunctions.FixedSize(image_file, 350, 180, true), 350, 180);
                        string p = Path.Combine(path, fileName);
                        bitmap_file.Save(p);
                    }
                    catch(Exception ex)
                    {

                        MessageBox1.Message = "پوشه content/productpic قابل نوشتن نیست. بررسی کنید. "+ex.Message;
                        MessageBox1.MessageType = HRaz.MessageBox.MessageType.Error;
                        MessageBox1.Visible = true;
                        return;
                    }
                    imgName = foldername + fileName;
                }
                if (string.IsNullOrWhiteSpace(imgName))
                    imgName = "nopic.png";

                int GroupID = int.Parse(ddlgroup.SelectedValue);

                Product tmp = new Product();
                tmp.Text = ceText.Text.ValidPersian();
                tmp.Title = txtTitle.Text.Trim().ValidPersian();
                tmp.GroupID = GroupID;
                tmp.RegDate = DateTime.Now;
                tmp.IsActive = chbActive.Checked;
                //tmp.PreviewPic = previewPic;

                int intnaum = 0;

                intnaum = 0;

                tmp.LastEditUserID = tmp.InsertUserID = ((AKUserClass)(Session["User"])).THisUserID;
                tmp.Pic = imgName;
                tmp.ViewCount = 0;
                tmp.IsNew = false;
                tmp.IsHot = false;

                tmp.TypeValue= (int)productType;

                 
                int.TryParse(txtPrice.Text, out intnaum);
                tmp.Price = intnaum;

                int.TryParse(txtHotelOff.Text, out intnaum);
                tmp.HotelDiscountPercent = intnaum;

                int.TryParse(txtAgancyOff.Text, out intnaum);
                tmp.AgancyDiscountPercent = intnaum;

                tmp.SeoDescription = CommonFunctions.SubStringText(txtTitle.Text, 0, 200).ValidPersian();
                tmp.SeoTitle = txtTitle.Text.ValidPersian();
                tmp.LastEdit = tmp.RegDate;


                tmp.Description = txtDesc.Text.Trim().ValidPersian();
                tmp.Slug = CommonFunctions.ReplaceSpace(txtTitle.Text.Trim().ToLower()).ValidPersian();
                tmp.ViewCount = 0;



                if (productType == CMS.ProductType.Transfer)
                {
                    tmp.HamrahiTypeValue = int.Parse(ddlHamrahiType.SelectedValue);
                    tmp.VehicleTypeValue = int.Parse(ddlVehicleType.SelectedValue);
                    tmp.PlaceTypeValue = int.Parse(ddlPlace.SelectedValue);
                }
                else if (productType == CMS.ProductType.Gasht)
                {
                    tmp.ActivityTypeValue = int.Parse(ddlActivityType.SelectedValue);
                }
                else if (productType == CMS.ProductType.Tour)//تور فقط گروهی داره
                    tmp.ActivityTypeValue =(int) ActivityTypeEnum.Group;

                    _data.Products.InsertOnSubmit(tmp);
                _data.SubmitChanges();

                //if (_data.SpecificationAttributes.Count() != 0)
                //    Response.Redirect("productSpecs.aspx?id=" + tmp.ID + "&t=new", true);
                //else
                    Response.Redirect("/adminpanel/productSeo.aspx?id=" + tmp.ID, true);
            }
        }




        int getMainGruopID(int id)
        {
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                ProductGroup tmp = _data.ProductGroups.Single(a => a.ID == id);

                int retVal = tmp.ID;
                if (tmp.ParentID != 0)
                    return getMainGruopID((int)tmp.ParentID);
                else return retVal;

            }
        }




    }
}