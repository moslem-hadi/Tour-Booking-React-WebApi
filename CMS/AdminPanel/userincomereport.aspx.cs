using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class userincomereport : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();

            DataAccessDataContext _data = new DataAccessDataContext();
            if (!IsPostBack)
            {
                txtfrom.Text = CommonFunctions.ConvertNumFa2En(CommonFunctions.String2date(DateTime.Now.AddYears(-1), 2, "D"));
                txtto.Text = CommonFunctions.ConvertNumFa2En(CommonFunctions.String2date(DateTime.Now, 2, "D"));

            }
            if (Request.QueryString["id"] != null)
            {

                string key = Request.QueryString["id"].Trim();
                int userid = 0;
                int.TryParse(key, out userid);
              
                var tmp = _data.usersdatas.FirstOrDefault(a => a.ID == userid);
                if (tmp == null)
                    Response.Redirect("userincomereport.aspx");

                var creditSum = _data.CreditTransactions.Where(a => a.UserID == tmp.ID).Sum(a => (int?)a.Mablagh) ??0;



                ltraffnow.Text = CommonFunctions.SetCama(creditSum) ;
                ltrname.Text = tmp.FullName;



                string from = "", to1 = "";
                from = Request.QueryString["from"].Trim();
                to1 = Request.QueryString["to"].Trim();
                var todate = DateTime.Parse(to1);
                todate = new DateTime(todate.Year, todate.Month, todate.Day, 23, 59, 59);
                 

                var data2 = _data.UserTransactionReport(userid, DateTime.Parse(from), todate);

                CollectionPager2.DataSource = data2.ToArray(); ;
                CollectionPager2.BindToControl = GridView2;
                GridView2.DataSource = CollectionPager2.DataSourcePaged;
                 
                 
                //conn.Close();
                if (!IsPostBack)
                {
                    if (string.IsNullOrEmpty(from))
                    {
                        txtfrom.Text = CommonFunctions.ConvertNumFa2En(CommonFunctions.String2date(DateTime.Now.AddYears(1), 2, "D"));
                        txtto.Text = CommonFunctions.ConvertNumFa2En(CommonFunctions.String2date(DateTime.Now, 2, "D"));

                    }
                    else {
                        txtfrom.Text = CommonFunctions.String2date(from, 3, "");
                        txtto.Text = CommonFunctions.String2date(to1, 3, "");
                    }
                    TextBox1.Text = key;
                }

            }

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            GridViewRow row = e.Row;
            if (row.DataItem == null) return;


            Literal ltrStat = (Literal)e.Row.FindControl("ltrstat");

            bool IsDone = (bool)(DataBinder.Eval(e.Row.DataItem, "IsDone"));
            bool IsRead = (bool)(DataBinder.Eval(e.Row.DataItem, "IsRead"));
            bool IsPaid = (bool)(DataBinder.Eval(e.Row.DataItem, "IsPaid"));

            if (!IsDone)
            {
                ltrStat.Text = "<span style='color:blue;'>در انتظار بررسی</span>";
            }
            else
            {
                if (!IsPaid)
                {
                    ltrStat.Text = "<span style='color:Red;'>واریز نشده</span>";
                }
                else
                {
                    ltrStat.Text = "<span style='color:#2b7da3; '>واریز شده</span>";
                }
            }





        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string key = TextBox1.Text.Trim();
            if (key == string.Empty)
                Response.Redirect("userincomereport.aspx");


            string[] from = txtfrom.Text.Split('/');
            DateTime fromDate = Persia.Calendar.ConvertToGregorian(int.Parse(from[0]), int.Parse(from[1]), int.Parse(from[2]),0,0,0, Persia.DateType.Gerigorian);
            string[] end = txtto.Text.Split('/');
            DateTime endDate = Persia.Calendar.ConvertToGregorian(int.Parse(end[0]), int.Parse(end[1]), int.Parse(end[2]),23,59,59, Persia.DateType.Gerigorian);


            string url = Request.Url.GetLeftPart(UriPartial.Path);
            Response.Redirect(url + "?id=" + key.Trim() + "&from=" + fromDate + "&to=" + endDate);
        }
    }
}