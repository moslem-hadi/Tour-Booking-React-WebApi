using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class inbox : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();
            if (!Page.IsPostBack)
            {

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

                if (Request.QueryString["key"] != null)
                {
                    try
                    {
                        string key = Request.QueryString["key"].Replace("+", " ");
                        //var tmp = _data.usersdatas.Where(a => a.FirstName.Contains(key) || a.LastName.Contains(key) || a.Description.Contains(key) || a.Email1.Contains(key));

                        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CMSDataBaseConnectionString"].ConnectionString);
                        string comand = "SELECT     ID, FulName, Title, Text,reply, IsRead, RegDate FROM contactPm where [contactPM].title like '%' +@key +'%' or [contactPM].text like '%' +@key +'%'";
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
                    catch { }
                }


            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            DataAccessDataContext _data = new DataAccessDataContext();
            CheckBox chkAdd;
            int rowCount = GridView1.Rows.Count;
            for (int i = 0; i <= (rowCount - 1); i++)
            {
                chkAdd = (CheckBox)GridView1.Rows[i].FindControl("chkBxSelect");
                int ID = int.Parse(GridView1.DataKeys[i].Value.ToString());
                if (chkAdd.Checked == true)
                {
                    contactPm tmp = _data.contactPms.Single(a => a.ID == ID);
                    _data.contactPms.DeleteOnSubmit(tmp);
                    _data.SubmitChanges();
                }
            }
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int index = e.NewPageIndex + 1;
            string url = Request.Url.GetLeftPart(UriPartial.Path);
            string sorting = "";
            if (Request.QueryString["key"] != null)
            {
                sorting += "&key=" + Request.QueryString["key"];
            }
            e.Cancel = true; ;
            Response.Redirect(string.Format("{0}?page={1}{2}", url, index, sorting));
        }



        protected void LinkButton2_Click(object sender, EventArgs e)
        {

            if (TextBox1.Text.Trim() == string.Empty)
                Response.Redirect("inbox.aspx");
            string url = Request.Url.GetLeftPart(UriPartial.Path);
            Response.Redirect(url + "?key=" + TextBox1.Text.Trim().Replace(" ", "+"));
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool isnew = (bool)DataBinder.Eval(e.Row.DataItem, "IsRead");
                if (!isnew)
                {
                    e.Row.BackColor = System.Drawing.Color.FromName("#ffe7ad"); // is a "new" row
                    e.Row.Style.Add("background-image", "none");
                }
            }
        }
    }
}