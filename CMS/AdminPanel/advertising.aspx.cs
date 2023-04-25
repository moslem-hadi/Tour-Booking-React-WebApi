using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace CMS.Manage
{
    public partial class advertising : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();

            if (!IsPostBack)
            {
                getgroup(0, "");
                txtStartDate.Text = CommonFunctions.String2date(DateTime.Now, 3, "");
                txtEndDate.Text = CommonFunctions.String2date(DateTime.Now, 3, "");
            }
            if (Request.QueryString["page"] != null)
            {
                int index = 0;
                try
                {
                    index = int.Parse(Request.QueryString["page"]);
                }
                catch { }
                try
                {
                    GridView1.PageIndex = index - 1;

                }
                catch { GridView1.PageIndex = 0; }
            }
            GridView1.DataBind();
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
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                if (string.IsNullOrEmpty(txtTitle.Text))
                {
                    MessageBox1.Message = "فیلدهای لازم را پر کنید.";
                    MessageBox1.Visible = true;
                    return;
                }

                advertisment tmp = new advertisment();
                tmp.Title = txtTitle.Text.Trim();
                
                string imgName = "nopic.png";
                if (!FileUpload1.HasFile)
                {
                    MessageBox1.Message = "تصویر را انتخاب کنید.";
                    MessageBox1.Visible = true;
                    return;
                }
                else
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
                        FileUpload1.SaveAs(Server.MapPath("~/content/pictures/") + fileName);
                    }
                    catch
                    {

                        MessageBox1.Message = "پوشه content/images/pictures قابل نوشتن نیست. بررسی کنید.";
                        MessageBox1.MessageType = HRaz.MessageBox.MessageType.Error;
                        MessageBox1.Visible = true;
                        return;
                    }
                    imgName = fileName;
                }
                tmp.ClickCount = 0;


                string rldt, exdt;

                if (string.IsNullOrEmpty(txtStartDate.Text.Trim()))
                    rldt = CommonFunctions.String2date(DateTime.Now, 3, "");
                else
                    rldt = txtStartDate.Text;

                if (string.IsNullOrEmpty(txtEndDate.Text.Trim()))
                    exdt = CommonFunctions.String2date(DateTime.Now, 3, "");
                else
                    exdt = txtEndDate.Text;


                try
                {
                    string[] ReleaseDatearr = rldt.Split('/');
                    DateTime ReleaseDate = Persia.Calendar.ConvertToGregorian(int.Parse(ReleaseDatearr[0]), int.Parse(ReleaseDatearr[1]), int.Parse(ReleaseDatearr[2]), Persia.DateType.Gerigorian);
                    tmp.FromDate = ReleaseDate;
                }
                catch
                {
                    tmp.FromDate = DateTime.Now;
                }

                try
                {
                    string[] ExDatearr = exdt.Split('/');
                    DateTime ExDate = Persia.Calendar.ConvertToGregorian(int.Parse(ExDatearr[0]), int.Parse(ExDatearr[1]), int.Parse(ExDatearr[2]), Persia.DateType.Gerigorian);
                    tmp.ToDate = ExDate;
                }
                catch
                {
                    tmp.ToDate = tmp.FromDate;
                }
                tmp.Place = int.Parse(DropDownList1.SelectedItem.Value);
                if (DropDownList1.SelectedValue == "5")
                { 
                    tmp.GroupID = int.Parse( ddlgroup.SelectedValue ); 
                }
                else
                { 
                    tmp.GroupID = -1;
                }

                int.TryParse(txtPriority.Text, out int priority);
                tmp.Priority = priority;
                tmp.ColumnSize = int.Parse(ddlSize.SelectedValue);
                tmp.Pic = imgName;
                tmp.Link = txtLink.Text;
                _data.advertisments.InsertOnSubmit(tmp);
                _data.SubmitChanges();
                Response.Redirect("advertising.aspx");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                if (string.IsNullOrEmpty(txtTitle.Text))
                {
                    MessageBox1.Message = "فیلدهای لازم را پر کنید.";
                    MessageBox1.Visible = true;
                    return;
                }

                int id = int.Parse(ltrID.Text);
                advertisment tmp = _data.advertisments.Single(a => a.ID == id);





                string imgName = tmp.Pic;
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


                    fileName = Guid.NewGuid() + fileExt;
                    try
                    {
                        FileUpload1.SaveAs(Server.MapPath("~/content/pictures/") + fileName);
                    }
                    catch
                    {

                        MessageBox1.Message = "پوشه content/images/pictures قابل نوشتن نیست. بررسی کنید.";
                        MessageBox1.MessageType = HRaz.MessageBox.MessageType.Error;
                        MessageBox1.Visible = true;
                        return;
                    }
                    imgName = fileName;
                }

                string rldt, exdt;

                if (string.IsNullOrEmpty(txtStartDate.Text.Trim()))
                    rldt = CommonFunctions.String2date(DateTime.Now, 3, "");
                else
                    rldt = txtStartDate.Text;

                if (string.IsNullOrEmpty(txtEndDate.Text.Trim()))
                    exdt = CommonFunctions.String2date(DateTime.Now, 3, "");
                else
                    exdt = txtEndDate.Text;


                try
                {
                    string[] ReleaseDatearr = rldt.Split('/');
                    DateTime ReleaseDate = Persia.Calendar.ConvertToGregorian(int.Parse(ReleaseDatearr[0]), int.Parse(ReleaseDatearr[1]), int.Parse(ReleaseDatearr[2]), Persia.DateType.Gerigorian);
                    tmp.FromDate = ReleaseDate;
                }
                catch
                {
                    tmp.FromDate = DateTime.Now;
                }

                try
                {
                    string[] ExDatearr = exdt.Split('/');
                    DateTime ExDate = Persia.Calendar.ConvertToGregorian(int.Parse(ExDatearr[0]), int.Parse(ExDatearr[1]), int.Parse(ExDatearr[2]), Persia.DateType.Gerigorian);
                    tmp.ToDate = ExDate;
                }
                catch
                {
                    tmp.ToDate = tmp.FromDate;
                }

                int.TryParse(txtPriority.Text, out int priority);
                tmp.Priority = priority;
                tmp.ColumnSize = int.Parse(ddlSize.SelectedValue);
                tmp.Pic = imgName;
                tmp.Link = txtLink.Text;
                tmp.Place = int.Parse(DropDownList1.SelectedValue);
                if (DropDownList1.SelectedValue == "5")
                { 
                    tmp.GroupID = int.Parse(ddlgroup.SelectedValue ); 
                }
                else
                { 
                    tmp.GroupID = -1;
                }


                tmp.Title = txtTitle.Text.Trim(); 
                _data.SubmitChanges();
                Response.Redirect("advertising.aspx");
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem == null) return;

            int id = int.Parse(DataBinder.Eval(e.Row.DataItem, "ID").ToString());
            Literal ltrStat = (Literal)e.Row.FindControl("ltrStat");

            Literal ltrPlace = (Literal)e.Row.FindControl("ltrPlace");




                //         <asp:ListItem Value="1" Text="در هدر" ></asp:ListItem>
                //<asp:ListItem Value="2" Text="سمت راست" ></asp:ListItem>
                //<asp:ListItem Value="4" Text="سمت چپ صفحات" ></asp:ListItem>
                //<asp:ListItem Value="3" Text="سمت چپ همه گروه ها" ></asp:ListItem>
                //<asp:ListItem Value="5" Text="سمت چپ گروه خاص" ></asp:ListItem>



            DateTime Date = (DateTime)DataBinder.Eval(e.Row.DataItem, "ToDate");
            int Place = (int)DataBinder.Eval(e.Row.DataItem, "Place");
            DateTime FromDate = (DateTime)DataBinder.Eval(e.Row.DataItem, "FromDate");
            switch (Place)
            {
                case 1:
                    ltrPlace.Text = "صفحه اصلی";
                    break;
                case 2:
                    ltrPlace.Text = "راست";
                    break; 
                case 3:
                    ltrPlace.Text = "چپ گروه ها";
                    break;
                case 4:
                    ltrPlace.Text = "چپ گروه خاص";
                    break;

            }

            if (Date < DateTime.Now && FromDate != Date)
                ltrStat.Text = "<span class='label label-important' title='اتمام نمایش'><i class='icon-remove-sign icon-white' style='margin:2px'></i></span>";
            else
            {
                if (FromDate > DateTime.Now)
                    ltrStat.Text = "<span class='label' title='پیش انتشار'><i class='icon-minus-sign icon-white' style='margin:2px'></i></span>";
                else

                    ltrStat.Text = "<span class='label label-success' title='درحال نمایش'><i class='icon-ok-sign icon-white' style='margin:2px'></i></span>";

            }

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            CheckBox chkAdd;
            int rowCount = GridView1.Rows.Count;
            string idList = "";
            for (int i = 0; i <= (rowCount - 1); i++)
            {
                chkAdd = (CheckBox)GridView1.Rows[i].FindControl("chkBxSelect");
                int ID = int.Parse(GridView1.DataKeys[i].Value.ToString());
                if (chkAdd.Checked == true)
                {
                    using (DataAccessDataContext _data = new DataAccessDataContext())
                    {
                        advertisment tmp = _data.advertisments.Single(a => a.ID == ID);
                        _data.advertisments.DeleteOnSubmit(tmp);
                        _data.SubmitChanges();


                        try
                        {
                            if (File.Exists(Server.MapPath("~/content/pictures/") + tmp.Pic))
                                File.Delete(Server.MapPath("~/content/pictures/") + tmp.Pic);
                        }
                        catch
                        {
                        }
                    }
                }
            }
            Response.Redirect("advertising.aspx");



        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int index = e.NewPageIndex + 1;
            string url = Request.Url.GetLeftPart(UriPartial.Path);

            e.Cancel = true; ;
            Response.Redirect(string.Format("{0}?page={1}", url, index));
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    int id = int.Parse(e.CommandArgument.ToString());
                    advertisment tmp = _data.advertisments.Single(a => a.ID == id);
                    _data.advertisments.DeleteOnSubmit(tmp);
                    _data.SubmitChanges();

                    try
                    {
                        if (File.Exists(Server.MapPath("~/content/pictures/") + tmp.Pic))
                            File.Delete(Server.MapPath("~/content/pictures/") + tmp.Pic);
                    }
                    catch
                    {
                    }

                    Response.Redirect("advertising.aspx");
                }
            }

            if (e.CommandName == "edt")
            {
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    int id = int.Parse(e.CommandArgument.ToString());
                    advertisment tmp = _data.advertisments.Single(a => a.ID == id);
                    txtTitle.Text = tmp.Title;

                    txtStartDate.Text = CommonFunctions.String2date((DateTime)tmp.FromDate, 3, "");
                    txtEndDate.Text = CommonFunctions.String2date((DateTime)tmp.ToDate, 3, "");
                    DropDownList1.SelectedValue = tmp.Place.ToString();
                    if (tmp.Place.ToString() == "5")
                    {
                        ddlgroup.SelectedValue = tmp.GroupID.ToString();
                        Page.RegisterStartupScript("s", "<script>document.getElementById('groupsddl').style.display = 'inline'</script>");
                    }
                    txtLink.Text = tmp.Link;
                    ddlSize.SelectedValue = tmp.ColumnSize.ToString();
                    txtPriority.Text = tmp.Priority.ToString();
                    selectedimg.ImageUrl = "~/content/pictures/" + tmp.Pic;
                    selectedimg.Visible = true;
                    ltrID.Text = tmp.ID.ToString();
                    Button1.Visible = false;
                    cancl.Visible = true;
                    Button2.Visible = true;
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
    }
}