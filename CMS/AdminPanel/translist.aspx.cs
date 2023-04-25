using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class translist : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();

            DataAccessDataContext _data = new DataAccessDataContext();


            if (Request.QueryString["key"] != null)
            {

                string key = Request.QueryString["key"].Replace("+", " ");
                int residID = 0;
                int.TryParse(key, out residID);


                if (residID != 0)
                {
                    CollectionPager1.DataSource = _data.Managetransactionslist(residID).ToArray(); ;
                    CollectionPager1.BindToControl = GridView1;
                    GridView1.DataSource = CollectionPager1.DataSourcePaged;

                }
                else
                {

                    CollectionPager1.DataSource = _data.Managetransactionslist(0).ToArray(); ;
                    CollectionPager1.BindToControl = GridView1;
                    GridView1.DataSource = CollectionPager1.DataSourcePaged;

                }
                if (!IsPostBack)
                    TextBox1.Text = key;
            }
            else
            {

                CollectionPager1.DataSource = _data.Managetransactionslist(0).ToArray(); ;
                CollectionPager1.BindToControl = GridView1;
                GridView1.DataSource = CollectionPager1.DataSourcePaged;

            }
            //conn.Close();

        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            string key = TextBox1.Text.Trim();
            if (key == string.Empty)
                Response.Redirect("translist.aspx");

            string url = Request.Url.GetLeftPart(UriPartial.Path);
            Response.Redirect(url + "?key=" + key.Replace(" ", "+"));
        }
         
    }
}