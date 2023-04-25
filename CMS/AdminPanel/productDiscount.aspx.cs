using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;


namespace CMS.AdminPanel
{
    public partial class productDiscount : System.Web.UI.Page
    {

        public string title = "", price="";
        protected void Page_Load(object sender, EventArgs e)
        {

            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();
            int productId = 0 ;
            int.TryParse(Request.QueryString["id"], out productId);

            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                var product = _data.Products.FirstOrDefault(a => a.ID == productId);
                if (product == null)
                    Response.Redirect("/adminpanel",true);
                title = product.Title;
                price = CommonFunctions.SetCama(product.Price.ToString());
                txtPrice.Text = product.Price.ToString();
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
        protected void btnAdd_Click(object sender, EventArgs e)
        {

            MessageBox1.Visible = false;
            if (string.IsNullOrEmpty(txtUser.Text)  )
            {

                MessageBox1.Message = "فیلدهای لازم را پر کنید.";
                MessageBox1.MessageType = HRaz.MessageBox.MessageType.Error;
                MessageBox1.Visible = true;
                return;
            }

            int id = 0, userId = 0;
            int.TryParse(Request.QueryString["id"], out id);
            int.TryParse(txtUser.Text, out id);
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {


                var tmp = _data.ProductUserDiscounts.Where(a => a.UserID == userId && a.ProductID == id).FirstOrDefault();

                if (string.IsNullOrEmpty(txtDiscount.Text))
                {
                    if (tmp == null)
                    {
                        MessageBox1.Message = "برای این کاربر تخفیفی ثبت نشده است.";
                        MessageBox1.MessageType = HRaz.MessageBox.MessageType.Warning;
                        MessageBox1.Visible = true;
                    }
                    else
                    {

                        _data.ProductUserDiscounts.DeleteOnSubmit(tmp);
                        _data.SubmitChanges();
                        MessageBox1.Message = "تخفیف اختصاص داده شده به این کاربر حذف شد.";
                        MessageBox1.MessageType = HRaz.MessageBox.MessageType.Warning;
                        MessageBox1.Visible = true;

                    }
                }
                else
                {
                    if (tmp == null)
                    {
                        tmp = new ProductUserDiscount();
                        tmp.Discount = int.Parse(txtDiscount.Text);
                        tmp.ProductID = id;
                        tmp.UserID = userId;
                        _data.ProductUserDiscounts.InsertOnSubmit(tmp);
                        _data.SubmitChanges();


                        MessageBox1.Message = "تخفیف اضافه شد.";
                        MessageBox1.MessageType = HRaz.MessageBox.MessageType.Submit;
                        MessageBox1.Visible = true;

                    }
                    else
                    {


                        tmp.Discount = int.Parse(txtDiscount.Text);
                        _data.SubmitChanges();

                        MessageBox1.Message = "برای این کاربر ویرایش شد.";
                        MessageBox1.MessageType = HRaz.MessageBox.MessageType.Submit;
                        MessageBox1.Visible = true;
                    }
                }
            }
            txtUser.ReadOnly = false;
            txtUser.Text = string.Empty;
            txtDiscount.Text = string.Empty;
            ltrID.Text = string.Empty;
            GridView1.DataBind();

        }
        //protected void btnEdit_Click(object sender, EventArgs e)
        //{
        //    MessageBox1.Visible = false;

        //    using (DataAccessDataContext _data = new DataAccessDataContext())
        //    {

        //        if ( string.IsNullOrEmpty(txtDiscount.Text))
        //        {
        //            MessageBox1.Message = "فیلدهای لازم را پر کنید.";
        //            MessageBox1.Visible = true;
        //            return;
        //        }

        //        int id = int.Parse(ltrID.Text);

        //        var tmp = _data.ProductUserDiscounts.FirstOrDefault(a => a.ID == id);
        //        if (tmp != null)
        //        {
        //            tmp.Discount = int.Parse(txtDiscount.Text);
        //            _data.SubmitChanges();
        //        }

        //        Response.Redirect(Request.RawUrl);

        //    }
        //}


         


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int index = e.NewPageIndex + 1;
            string url = Request.Url.GetLeftPart(UriPartial.Path);

            e.Cancel = true; ;
            Response.Redirect(string.Format("{0}?page={1}", url, index));
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            MessageBox1.Visible = false;
            if (e.CommandName == "Del")
            {
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    int id = int.Parse(e.CommandArgument.ToString());
                    var tmp = _data.ProductUserDiscounts.FirstOrDefault(a => a.ID == id);
                    if (tmp != null)
                    {
                        _data.ProductUserDiscounts.DeleteOnSubmit(tmp);
                        _data.SubmitChanges();
                    }
                    Response.Redirect(Request.RawUrl);
                }
            }

            if (e.CommandName == "edt")
            {
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    int id = int.Parse(e.CommandArgument.ToString());

                    var tmp = _data.ProductUserDiscounts.Single(a => a.ID == id);
                    txtUser.ReadOnly = true;

                    txtUser.Text = tmp.UserID.ToString();
                    txtDiscount.Text = tmp.Discount.ToString() ;
                    ltrID.Text = tmp.ID.ToString();

                }
            }

        }
    }
}