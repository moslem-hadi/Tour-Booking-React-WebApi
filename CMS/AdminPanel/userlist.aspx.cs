using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace CMS.Manage
{
    public partial class userlist : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();

            if (!IsPostBack)
            {

                if (Request.QueryString["key"] != null)
                {

                    string key = Request.QueryString["key"].Replace("+", " ");
                    int residID = 0;
                    int.TryParse(key, out residID);
                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CMSDataBaseConnectionString"].ConnectionString);
                    string comand = "SELECT ID, Email, FullName, IsActive, IsManager, RegisterDate, IsBanned,mobile, AffiliateMoney,BannedMsg FROM usersdata where fullname like '%' +@key +'%'  or email like '%' +@key +'%'  or ID =@residID";
                    conn.Open();
                    SqlCommand com = new SqlCommand(comand, conn);
                    com.Parameters.AddWithValue("@key", key); com.Parameters.AddWithValue("@residID", residID);

                    System.Data.DataSet ds = new System.Data.DataSet();

                    SqlDataAdapter da = new SqlDataAdapter(com);
                    da.Fill(ds);

                    GridView1.DataSourceID = null;


                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    conn.Close();
                    TextBox1.Text = key;

                }

            }

        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                CheckBox chkAdd;
                int rowCount = GridView1.Rows.Count;
                for (int i = 0; i <= (rowCount - 1); i++)
                {
                    chkAdd = (CheckBox)GridView1.Rows[i].FindControl("chkBxSelect");
                    int ID = int.Parse(GridView1.DataKeys[i].Value.ToString());
                    if (chkAdd.Checked == true)
                    {
                          

                    }
                }
                Response.Redirect("productlist.aspx");
            }
        }

         

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            string key = TextBox1.Text.Trim();
            if (key == string.Empty)
                Response.Redirect("Productlist.aspx");
            int id = 0;
            int.TryParse(key, out id);
            if (id != 0)
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    if (_data.Products.Where(a => a.ID == id).Count() == 1)
                        Response.Redirect("viewproduct.aspx?id=" + id);
                }
            string url = Request.Url.GetLeftPart(UriPartial.Path);
            Response.Redirect(url + "?key=" + key.Replace(" ", "+"));
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                int id = 0;
                id = int.Parse(e.CommandArgument.ToString());
                DataAccessDataContext _data= new DataAccessDataContext();

                usersdata tmp = _data.usersdatas.Single(a => a.ID == id);
             
                _data.usersdatas.DeleteOnSubmit(tmp);
                _data.SubmitChanges();
                GridView1.DataBind();
            }
        }
         
    }
}