using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.AdminPanel
{
    public partial class prices : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();

            if(!IsPostBack)
            {
                txtEndDate.Text = txtStartDate.Text = DateTime.Now.Date.ToShortDateString();// = CommonFunctions.ToPersianDate(DateTime.Now);
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
            if (string.IsNullOrEmpty(txtStartDate.Text) || string.IsNullOrEmpty(txtEndDate.Text) || string.IsNullOrEmpty(txtPrice.Text))
            {

                MessageBox1.Message = "فیلدهای لازم را پر کنید.";
                MessageBox1.MessageType = HRaz.MessageBox.MessageType.Error;
                MessageBox1.Visible = true;
                return;
            }

            int id = 0;
            int.TryParse(Request.QueryString["id"], out id);
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {


                DateTime start, end;
                start = CommonFunctions.ToMiladi(txtStartDate.Text);
                end = CommonFunctions.ToMiladi(txtEndDate.Text);
                foreach (DateTime day in CommonFunctions.EachDay(start, end))
                {
                    var savedDay = _data.DayPrices.Where(a => a.Day == day.Date && a.ProductID == id).FirstOrDefault();
                    if (savedDay == null)
                    {
                        DayPrice tmp = new DayPrice();
                        tmp.Day = day.Date;
                        tmp.DayFa = CommonFunctions.ToPersianDate(day.Date);
                        tmp.IsAvailable = bool.Parse(dllAvability.SelectedValue);
                        tmp.Price = int.Parse(txtPrice.Text);
                        tmp.ProductID = id;
                        _data.DayPrices.InsertOnSubmit(tmp);
                        _data.SubmitChanges();

                    }
                    else
                    {
                        savedDay.IsAvailable = bool.Parse(dllAvability.SelectedValue);
                        savedDay.Price = int.Parse(txtPrice.Text);
                        _data.SubmitChanges();
                    }
                }

            }
            txtEndDate.Text = txtStartDate.Text = DateTime.Now.Date.ToShortDateString();// = CommonFunctions.ToPersianDate(DateTime.Now);
            

            GridView1.DataBind();

            MessageBox1.Message = "ذخیره شد.";
            MessageBox1.MessageType = HRaz.MessageBox.MessageType.Submit;
            MessageBox1.Visible = true;

        }
        //protected void btnEdit_Click(object sender, EventArgs e)
        //{
        //    MessageBox1.Visible = false;

        //    using (DataAccessDataContext _data = new DataAccessDataContext())
        //    {
        //        if (string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrEmpty(txtStartDate.Text) || string.IsNullOrEmpty(txtEndDate.Text))
        //        {
        //            MessageBox1.Message = "فیلدهای لازم را پر کنید.";
        //            MessageBox1.Visible = true;
        //            return;
        //        }

        //        int id = int.Parse(ltrID.Text);


        //        var savedDay = _data.DayPrices.FirstOrDefault(a=>a.ID==id);
        //        if (savedDay != null)
        //        {
        //            savedDay.IsAvailable = bool.Parse(dllAvability.SelectedValue);
        //            savedDay.Price = int.Parse(txtPrice.Text);
        //            _data.SubmitChanges();
        //        }

        //        Response.Redirect(Request.RawUrl);

        //    }
        //}



        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool isnew = (bool)DataBinder.Eval(e.Row.DataItem, "Passed");
                if (isnew)
                {
                    //e.Row.BackColor = System.Drawing.Color.FromName("#ffe7ad"); // is a "new" row
                    e.Row.Style.Add("opacity", "0.5");
                }
            }
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
            MessageBox1.Visible = false;
            if (e.CommandName == "Del")
            {
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    int id = int.Parse(e.CommandArgument.ToString());
                    var savedDay = _data.DayPrices.FirstOrDefault(a => a.ID == id);
                    if (savedDay != null)
                    {
                        _data.DayPrices.DeleteOnSubmit(savedDay);
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

                    var tmp = _data.DayPrices.Single(a => a.ID == id);


                    txtStartDate.Text = 
                    txtEndDate.Text = tmp.Day + ""; ;
                    txtPrice.Text = tmp.Price.ToString();
                    dllAvability.SelectedValue = tmp.IsAvailable.ToString();
                    ltrID.Text = tmp.ID.ToString();
                    
                }
            }

        }
    }
}