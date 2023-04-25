using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class stats : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();

            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                string order = "id", dir = "desc";
                DateTime begin, end;
                begin = (DateTime)_data.siteStats.OrderBy(a => a.ID).Take(1).Single().Day;
                end = DateTime.Now;

                if (Request.QueryString["order"] != null)
                {
                    order = Request.QueryString["order"];
                    dir = Request.QueryString["dir"];
                    begin = DateTime.Parse(Request.QueryString["begin"]);
                    end = DateTime.Parse(Request.QueryString["end"]);
                   
                }
                if (!IsPostBack)
                {
                    DropDownList1.SelectedValue = order;
                    DropDownList2.SelectedValue = dir;
                    txtfrom.Text = CommonFunctions.String2date(begin, 3, "");
                    txtto.Text = CommonFunctions.String2date(end, 3, "");

              
                }
                var datas = _data.siteStats.Where(a => a.ID == -1).ToArray();
                switch (order)
                {
                    case "ID":
                        if (dir == "desc")
                            datas = _data.siteStats.Where(a => a.Day >= begin && a.Day <= end).OrderByDescending(a => a.ID).ToArray();
                        else
                            datas = _data.siteStats.Where(a => a.Day >= begin && a.Day <= end).OrderBy(a => a.ID).ToArray();
                        break;

                    case "ViewCount":
                        if (dir == "desc")
                            datas = _data.siteStats.Where(a => a.Day >= begin && a.Day <= end).OrderByDescending(a => a.ViewCount).ToArray();
                        else
                            datas = _data.siteStats.Where(a => a.Day >= begin && a.Day <= end).OrderBy(a => a.ViewCount).ToArray();
                        break;



                    case "FileCount":
                        if (dir == "desc")
                            datas = _data.siteStats.Where(a => a.Day >= begin && a.Day <= end).OrderByDescending(a => a.FileCount).ToArray();
                        else
                            datas = _data.siteStats.Where(a => a.Day >= begin && a.Day <= end).OrderBy(a => a.FileCount).ToArray();
                        break;



                    case "MarketingCount":
                        if (dir == "desc")
                            datas = _data.siteStats.Where(a => a.Day >= begin && a.Day <= end).OrderByDescending(a => a.MarketingCount).ToArray();
                        else
                            datas = _data.siteStats.Where(a => a.Day >= begin && a.Day <= end).OrderBy(a => a.MarketingCount).ToArray();
                        break;



                    case "RegisterCount":
                        if (dir == "desc")
                            datas = _data.siteStats.Where(a => a.Day >= begin && a.Day <= end).OrderByDescending(a => a.RegisterCount).ToArray();
                        else
                            datas = _data.siteStats.Where(a => a.Day >= begin && a.Day <= end).OrderBy(a => a.RegisterCount).ToArray();
                        break;


                    case "SalesPrice":
                        if (dir == "desc")
                            datas = _data.siteStats.Where(a => a.Day >= begin && a.Day <= end).OrderByDescending(a => a.SalesPrice).ToArray();
                        else
                            datas = _data.siteStats.Where(a => a.Day >= begin && a.Day <= end).OrderBy(a => a.SalesPrice).ToArray();
                        break;


                    case "MarketingEarning":
                        if (dir == "desc")
                            datas = _data.siteStats.Where(a => a.Day >= begin && a.Day <= end).OrderByDescending(a => a.MarketingEarning).ToArray();
                        else
                            datas = _data.siteStats.Where(a => a.Day >= begin && a.Day <= end).OrderBy(a => a.MarketingEarning).ToArray();
                        break;


                    case "SiteEarning":
                        if (dir == "desc")
                            datas = _data.siteStats.Where(a => a.Day >= begin && a.Day <= end).OrderByDescending(a => a.SiteEarning).ToArray();
                        else
                            datas = _data.siteStats.Where(a => a.Day >= begin && a.Day <= end).OrderBy(a => a.SiteEarning).ToArray();
                        break;

                    default:
                            datas = _data.siteStats.Where(a => a.Day >= begin && a.Day <= end).OrderByDescending(a => a.ID).ToArray();
                            break;
                        
                }
                CollectionPager1.DataSource = datas;
                CollectionPager1.BindToControl = Repeater1;
                Repeater1.DataSource = CollectionPager1.DataSourcePaged;


                //مجموع فروش
                ltrallsale.Text = CMS.CommonFunctions.SetCama(datas.Sum(a => a.SalesPrice).ToString());
                
                ltrviewcount.Text = datas.Sum(a => a.ViewCount).ToString();
                ltrupcount.Text = datas.Sum(a => a.FileCount).ToString();
                ltrsalecount.Text = datas.Sum(a => a.SaleCount).ToString();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string[] from = txtfrom.Text.Split('/');
            DateTime fromDate = Persia.Calendar.ConvertToGregorian(int.Parse(from[0]), int.Parse(from[1]), int.Parse(from[2]), Persia.DateType.Gerigorian);
            string[] end = txtto.Text.Split('/');
            DateTime endDate = Persia.Calendar.ConvertToGregorian(int.Parse(end[0]), int.Parse(end[1]), int.Parse(end[2]), Persia.DateType.Gerigorian);

            Response.Redirect(string.Format("stats.aspx?order={0}&dir={1}&begin={2}&end={3}", DropDownList1.SelectedItem.Value, DropDownList2.SelectedItem.Value,fromDate.ToShortDateString(),endDate.ToShortDateString()),true);
        }
    }
}