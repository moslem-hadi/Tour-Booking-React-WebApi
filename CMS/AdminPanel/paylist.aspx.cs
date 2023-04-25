using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;
using System.Web.Script.Services;

namespace CMS.Manage
{
    public partial class paylist : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();

            DataAccessDataContext _data = new DataAccessDataContext();
            
            string query = "";
            string dir = "desc", sort = "id", user = "", stat="-1" , from ="" , to="", key = "";


            DateTime begin, end;
            begin = (DateTime)_data.Payments.OrderBy(a => a.ID).Take(1).Single().RegisterDate;
            end = DateTime.Now;


            if (Request.QueryString["key"] != null)
            {
                key = Request.QueryString["key"].Replace("+", " ");

                user = Request.QueryString["user"];


                from = Request.QueryString["from"];

                to = Request.QueryString["to"];
                if (string.IsNullOrEmpty(from))
                    from = CommonFunctions.String2date(begin, 3, "");
                if (string.IsNullOrEmpty(to))
                    to = CommonFunctions.String2date(end, 3, "");

                string[] fromarr = from.Split('/');
                begin = Persia.Calendar.ConvertToGregorian(int.Parse(fromarr[0]), int.Parse(fromarr[1]), int.Parse(fromarr[2]), 0, 0, 0, Persia.DateType.Gerigorian);
                string[] endarr = to.Split('/');
                end = Persia.Calendar.ConvertToGregorian(int.Parse(endarr[0]), int.Parse(endarr[1]), int.Parse(endarr[2]), 23, 59, 59, Persia.DateType.Gerigorian);

                dir = Request.QueryString["dir"];
                sort = Request.QueryString["sort"];
                stat = Request.QueryString["stat"]; 

                query = $" where RegisterDate >= '{begin}' and RegisterDate<='{end}' ";

                if (!string.IsNullOrEmpty(key))
                    query += $" and (ID = {key} or TrackingCode='{key}' or SaleReferenceId='{key}' or SaleOrderId='{key}' or EntityID={key}) ";

                if (!string.IsNullOrEmpty(user))
                    query += $" and userid= {user} ";

 

                if (stat != "-1")
                    query += $" and IsSuccess= {stat} ";


            }

            if (!IsPostBack)
            {

                txtfrom.Text = CommonFunctions.String2date(begin, 3, "");
                txtto.Text = CommonFunctions.String2date(end, 3, "");
                txtKey.Text = key;
                txtUser.Text = user;
                ddlDir.SelectedValue = dir;
                ddlSort.SelectedValue = sort;
                ddlStat.SelectedValue = stat; 

            }

            query += $" order by {sort} {dir} ";





            var data = _data.paymentList_Manage(query);
            CollectionPager1.DataSource = data.ToArray(); ;
            CollectionPager1.BindToControl = GridView1;
            GridView1.DataSource = CollectionPager1.DataSourcePaged;


        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            string key = txtKey.Text.Trim().Replace(" ", "+");
            string querystr = "paylist.aspx?key=" + key + "&user=" + txtUser.Text + "&from=" + txtfrom.Text + "&to=" + txtto.Text +
                "&stat=" + ddlStat.SelectedValue + "&sort=" + ddlSort.SelectedValue + "&dir=" + ddlDir.SelectedValue;




            string url = Request.Url.GetLeftPart(UriPartial.Path);
            Response.Redirect(querystr);
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<usersdata> getUser(string pre)
        {
            List<usersdata> allproduct = new List<usersdata>();
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                allproduct = _data.usersdatas.Where(a => a.FullName.Contains(pre) || a.Email.Contains(pre) || a.Mobile.Contains(pre) || a.ID.ToString()== pre).OrderBy(a => a.FullName).ToList();
            }
            return allproduct;
        }

    }
}