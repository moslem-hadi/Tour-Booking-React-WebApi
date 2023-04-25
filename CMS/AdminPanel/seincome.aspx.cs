using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class seincome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();

        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                CheckBox chkAdd;
                Label lblKeyWord;
                int rowCount = GridView1.Rows.Count;
                for (int i = 0; i <= (rowCount - 1); i++)
                {
                    chkAdd = (CheckBox)GridView1.Rows[i].FindControl("chkBxSelect");
                    lblKeyWord = (Label)GridView1.Rows[i].FindControl("lblKeyWord");
//                    int ID = int.Parse(GridView1.DataKeys[i].Value.ToString());
                    if (chkAdd.Checked == true)
                    {
                        _data.referrerKeywords.DeleteAllOnSubmit(_data.referrerKeywords.Where(a=>a.KeyWord==lblKeyWord.Text));
                        _data.SubmitChanges();

                    }
                }
                Response.Redirect(Request.RawUrl);
            }
        }
    }
}