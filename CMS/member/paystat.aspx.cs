using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace CMS.member
{
    public partial class paystat : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.member.membermaster MasterPage = (CMS.member.membermaster)Page.Master;
            MasterPage.Check_User();

            sdspayreqlist.SelectParameters["UserID"].DefaultValue = ((AKUserClass)(Session["User"])).THisUserID.ToString();
            if (!IsPostBack)
            {

                if (Request.QueryString["key"] != null)
                {

                    string key = Request.QueryString["key"].Replace("+", " ");
                    int residID = 0;
                    int.TryParse(key, out residID);
                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CMSDataBaseConnectionString"].ConnectionString);
                    string comand = "SELECT *  FROM PayRequest where UserID= @userID and ID =@key";
                    conn.Open();
                    SqlCommand com = new SqlCommand(comand, conn);
                    com.Parameters.AddWithValue("@key", residID); com.Parameters.AddWithValue("@userID", ((AKUserClass)(Session["User"])).THisUserID.ToString());

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
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            string key = TextBox1.Text.Trim();
            if (key == string.Empty)
                Response.Redirect("paystat.aspx");

            string url = Request.Url.GetLeftPart(UriPartial.Path);
            Response.Redirect(url + "?key=" + key.Replace(" ", "+"));
        }
    }
}