using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.AdminPanel
{
    public partial class reservs : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();

            DataAccessDataContext _data = new DataAccessDataContext();

            string query = "";
            string dir = "desc", sort = "id", user = "", stat = "-1", from = "", to = "", key = "";


            DateTime begin, end;
            begin = (DateTime)_data.Payments.OrderBy(a => a.ID).Take(1).Single().RegisterDate;
            end = DateTime.Now;

            query = " where (isdeleted is null or isdeleted <>1) ";

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


                query += $"and RegDate >= '{begin}' and RegDate<='{end}' ";

                if (!string.IsNullOrEmpty(key))
                    query += $" and (ID = {key} or code='{key}') ";

                if (!string.IsNullOrEmpty(user))
                    query += $" and (ReffererUserId= {user} OR UserId ={user})";
                 

                if (stat != "-1")
                    query += $" and IsPaid= {stat} ";


            }

            if (!IsPostBack)
            { 
                txtKey.Text = key;
                txtUser.Text = user;
                ddlDir.SelectedValue = dir;
                ddlSort.SelectedValue = sort;
                ddlStat.SelectedValue = stat; 

            }

            query += $" order by {sort} {dir} ";





            var data = _data.OrderList_Manage(query);
            CollectionPager1.DataSource = data.ToArray(); ;
            CollectionPager1.BindToControl = GridView1;
            GridView1.DataSource = CollectionPager1.DataSourcePaged;


        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            string key = txtKey.Text.Trim().Replace(" ", "+");
            string querystr = "reservs.aspx?key=" + key + "&user=" + txtUser.Text + 
                "&stat=" + ddlStat.SelectedValue + "&sort=" + ddlSort.SelectedValue + "&dir=" + ddlDir.SelectedValue;




            string url = Request.Url.GetLeftPart(UriPartial.Path);
            Response.Redirect(querystr);
        }
         
    }
}