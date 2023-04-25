using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class editproduct : System.Web.UI.Page
    {
        public ProductType productType;

        protected void Page_Load(object sender, EventArgs e)
        { 
           
            if (!IsPostBack)
            {
                sdsHamrahiType.SelectParameters["type"].DefaultValue = ((int)GlobalValueTypes.Hamrahi).ToString();
                sdsVehicleType.SelectParameters["type"].DefaultValue = ((int)GlobalValueTypes.Vehicle).ToString();
                sdsPlaceType.SelectParameters["type"].DefaultValue = ((int)GlobalValueTypes.Place).ToString();
                sdsActivityType.SelectParameters["type"].DefaultValue = ((int)GlobalValueTypes.ActivityType).ToString();

                getgroup(0, "");
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    int id = 0;
                    int.TryParse(Request.QueryString["id"], out id);
                    Product tmp = _data.Products.Single(a => a.ID == id);
                    productType = (ProductType)tmp.TypeValue;
                    Page.RegisterStartupScript("s0", "<script>convert("+tmp.Price+", 'persian-price');</script>");

                    chbActive.Checked = (bool)tmp.IsActive;
                    ceText.Text = tmp.Text;
                    txtTitle.Text = tmp.Title;
                    ddlgroup.SelectedValue = tmp.GroupID.ToString() ;
                    selectedimg.ImageUrl = "~/content/productpic/" + tmp.Pic;
                
                    txtPrice.Text = tmp.Price.ToString();

                    txtDesc.Text = tmp.Description;
                    txtHotelOff.Text = (tmp.HotelDiscountPercent ?? 0).ToString();
                    txtAgancyOff.Text = (tmp.AgancyDiscountPercent ?? 0).ToString();
                    if (productType == CMS.ProductType.Transfer)
                    {
                        ddlHamrahiType.SelectedValue = tmp.HamrahiTypeValue.ToString();
                        ddlVehicleType.SelectedValue = tmp.VehicleTypeValue.ToString();
                        ddlPlace.SelectedValue = tmp.PlaceTypeValue.ToString();

                    }
                    else
                        ddlActivityType.SelectedValue = tmp.ActivityTypeValue.ToString();
                   


                }
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
            if (ddlgroup.SelectedValue=="-1")
            {

                MessageBox1.Message = "گروه را انتخاب کنید.";
                MessageBox1.Visible = true;
                return;
            }
            if (string.IsNullOrWhiteSpace(txtTitle.Text) )
            {
                MessageBox1.Message = "فیلدهای لازم را پر کنید.";
                MessageBox1.Visible = true;
                return;
            }


            using (DataAccessDataContext _data = new DataAccessDataContext())
            {

                int id = 0;
                int.TryParse(Request.QueryString["id"], out id);
                Product tmp = _data.Products.Single(a => a.ID == id);

                string foldername = DateTime.Now.ToString("yyyyMM") + "/";
                string path = Server.MapPath("~/content/productpic/") + foldername;

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string imgName = tmp.Pic;
                 
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

                    fileName = Guid.NewGuid().ToString() + fileExtpic;
                  
                     
                    try
                    {

                        System.Drawing.Image image_file = System.Drawing.Image.FromStream(fulMainPic.PostedFile.InputStream);

                        Bitmap bitmap_file = new Bitmap(CommonFunctions.FixedSize(image_file, 350, 180, true), 350, 180);
                        bitmap_file.Save(path + fileName);
                    }
                    catch
                    {

                        MessageBox1.Message = "پوشه content/productpic قابل نوشتن نیست. بررسی کنید.";
                        MessageBox1.MessageType = HRaz.MessageBox.MessageType.Error;
                        MessageBox1.Visible = true;
                        return;
                    }
                    imgName = foldername + fileName;
                }
                if (string.IsNullOrWhiteSpace(imgName))
                    imgName = "nopic.png";


 




                int GroupID = int.Parse(ddlgroup.SelectedValue);



                tmp.Text = ceText.Text.ValidPersian(); ;
                tmp.Title = txtTitle.Text.Trim().ValidPersian() ;
                tmp.GroupID = GroupID; 
                tmp.IsActive = chbActive.Checked;
                 
                int intnaum = 0;

                tmp.Description = txtDesc.Text.Trim().ValidPersian();

                tmp.Pic = imgName;

                 
                int pric = 0;
                int.TryParse(txtPrice.Text, out pric);
                 
                tmp.Price = pric;


                int.TryParse(txtHotelOff.Text, out intnaum);
                tmp.HotelDiscountPercent = intnaum;

                int.TryParse(txtAgancyOff.Text, out intnaum);
                tmp.AgancyDiscountPercent = intnaum;

                tmp.LastEdit = DateTime.Now;

                tmp.LastEditUserID = ((AKUserClass)(Session["User"])).THisUserID;

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
                    tmp.ActivityTypeValue = (int)ActivityTypeEnum.Group;


                _data.SubmitChanges();


                string PageNum = "";
                if (!string.IsNullOrEmpty(Request.QueryString["retp"]))
                    PageNum = Request.QueryString["retp"];
                if (!string.IsNullOrEmpty(PageNum))
                    Response.Redirect("Productlist.aspx" + Server.UrlDecode(PageNum) + "#" + tmp.ID, true);
                else

                    Response.Redirect("Productlist.aspx", true);
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