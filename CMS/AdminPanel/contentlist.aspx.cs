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
    public partial class contentlist : System.Web.UI.Page
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
                        Content tmp = _data.Contents.Single(a => a.ID == ID);
                        _data.Contents.DeleteOnSubmit(tmp);
                        _data.SubmitChanges();
                    }
                }
                Response.Redirect("contentlist.aspx");
            }
        }
         
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text.Trim() == string.Empty)
                Response.Redirect("contentlist.aspx");
            string url = Request.Url.GetLeftPart(UriPartial.Path);
            Response.Redirect(url + "?key=" + TextBox1.Text.Trim().Replace(" ", "+"));
        }
    }
}