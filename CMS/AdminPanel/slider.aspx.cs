using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace CMS.Manage
{
    public partial class slider : System.Web.UI.Page
    {
         
        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();

            if (!IsPostBack)
            {
             //   getgroup(0, "");
                //txtStartDate.Text = CommonFunctions.String2date(DateTime.Now, 3, "");
                //txtEndDate.Text = CommonFunctions.String2date(DateTime.Now, 3, "");
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

                sliderContent tmp = new sliderContent();
                tmp.Title = txtTitle.Text.Trim();
                tmp.Description = "";// txtDesc.Text.Trim();
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

                    fileName = Guid.NewGuid()+fileExt;
                    
                    try
                    {
                        FileUpload1.SaveAs(Server.MapPath("~/content/slider/") + fileName);
                    }
                    catch
                    {

                        MessageBox1.Message = "پوشه content/images/slider قابل نوشتن نیست. بررسی کنید.";
                        MessageBox1.MessageType = HRaz.MessageBox.MessageType.Error;
                        MessageBox1.Visible = true;
                        return;
                    }
                    imgName =  fileName;
                }
                tmp.ClickCount = 0;

                tmp.ToDate = tmp.FromDate = DateTime.Now;


                tmp.Place = 0;// int.Parse(DropDownList1.SelectedItem.Value);
                    tmp.GroupID = -1;


                tmp.Priority = 0;//int.Parse(txtPriority.Text);

                tmp.Pic = imgName;
                tmp.Url = txtLink.Text;
                _data.sliderContents.InsertOnSubmit(tmp);
                _data.SubmitChanges();
                Response.Redirect("slider.aspx");
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
                sliderContent tmp = _data.sliderContents.Single(a => a.ID == id);





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
                    try
                    {
                        File.Delete(Server.MapPath("~/content/slider/") + tmp.Pic);

                    }
                    catch { }

                    fileName = Guid.NewGuid() + fileExt;

                    try
                    {
                        FileUpload1.SaveAs(Server.MapPath("~/content/slider/") + fileName);
                    }
                    catch
                    {

                        MessageBox1.Message = "پوشه content/slider قابل نوشتن نیست. بررسی کنید.";
                        MessageBox1.MessageType = HRaz.MessageBox.MessageType.Error;
                        MessageBox1.Visible = true;
                        return;
                    }
                    imgName = fileName;
                } 
                 
                tmp.Pic = imgName;
                tmp.Url = txtLink.Text;

                 
                tmp.Title = txtTitle.Text.Trim();
                _data.SubmitChanges();
                Response.Redirect("slider.aspx");
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.DataItem == null) return;

            //int id = int.Parse(DataBinder.Eval(e.Row.DataItem, "ID").ToString());
            //Literal ltrStat = (Literal)e.Row.FindControl("ltrStat");


            //DateTime Date = (DateTime)DataBinder.Eval(e.Row.DataItem, "ToDate");
            //DateTime FromDate = (DateTime)DataBinder.Eval(e.Row.DataItem, "FromDate");


            //if (Date < DateTime.Now && FromDate != Date)
            //    ltrStat.Text = "<span class='label label-important' title='اتمام نمایش'><i class='icon-remove-sign icon-white' style='margin:2px'></i></span>";
            //else
            //{
            //    if (FromDate > DateTime.Now)
            //        ltrStat.Text = "<span class='label' title='پیش انتشار'><i class='icon-minus-sign icon-white' style='margin:2px'></i></span>";
            //    else

            //        ltrStat.Text = "<span class='label label-success' title='درحال نمایش'><i class='icon-ok-sign icon-white' style='margin:2px'></i></span>";

            //}
            
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
                        sliderContent tmp = _data.sliderContents.Single(a => a.ID == ID);
                        _data.sliderContents.DeleteOnSubmit(tmp);
                        _data.SubmitChanges();


                        try
                        {
                            if (File.Exists(Server.MapPath("~/content/slider/") + tmp.Pic))
                                File.Delete(Server.MapPath("~/content/slider/") + tmp.Pic);
                        }
                        catch { 
                        }
                    }
                }
            }
            Response.Redirect("slider.aspx");



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
                    sliderContent tmp = _data.sliderContents.Single(a => a.ID == id);
                    _data.sliderContents.DeleteOnSubmit(tmp);
                    _data.SubmitChanges();

                    try
                    {
                        if (File.Exists(Server.MapPath("~/content/slider/") + tmp.Pic))
                            File.Delete(Server.MapPath("~/content/slider/") + tmp.Pic);
                    }
                    catch
                    {
                    }

                    Response.Redirect("slider.aspx");
                }
            }

            if (e.CommandName == "edt")
            {
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    int id = int.Parse(e.CommandArgument.ToString());
                    sliderContent tmp = _data.sliderContents.Single(a => a.ID == id);
                   // txtDesc.Text = tmp.Description;
                    txtTitle.Text = tmp.Title;

                     
                    if (tmp.Place.ToString() == "2")
                    {
                        //ddlgroup.SelectedValue = tmp.GroupID.ToString();
                        Page.RegisterStartupScript("s", "<script>document.getElementById('groupsddl').style.display = 'inline'</script>");
                    }
                    txtLink.Text = tmp.Url;
                    selectedimg.ImageUrl = "~/content/slider/" + tmp.Pic;
                    selectedimg.Visible = true;
                    ltrID.Text = tmp.ID.ToString();
                    Button1.Visible = false;
                    cancl.Visible = true;
                    Button2.Visible = true;
                }
            }

        }


         
    }
}