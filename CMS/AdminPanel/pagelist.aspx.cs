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
    public partial class pagelist : System.Web.UI.Page
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
                        //var tmp = _data.usersdatas.Where(a => a.FirstName.Contains(key) || a.LastName.Contains(key) || a.Description.Contains(key) || a.Email1.Contains(key));

                        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CMSDataBaseConnectionString"].ConnectionString);
                        string comand = "SELECT    ID, Short, Title, ViewCount FROM pagecontent where title like '%' +@key +'%' or short like '%' +@key +'%' ";
                        conn.Open();
                        SqlCommand com = new SqlCommand(comand, conn);
                        com.Parameters.AddWithValue("@key", key);

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
                        PageContent tmp = _data.PageContents.Single(a => a.ID == ID);
                        _data.PageContents.DeleteOnSubmit(tmp);
                        _data.SubmitChanges();
                    }
                }
                Response.Redirect("pagelist.aspx");
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text.Trim() == string.Empty)
                Response.Redirect("pagelist.aspx");
            string url = Request.Url.GetLeftPart(UriPartial.Path);
            Response.Redirect(url + "?key=" + TextBox1.Text.Trim().Replace(" ", "+"));

        }


    }
}