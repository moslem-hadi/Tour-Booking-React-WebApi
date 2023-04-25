using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class addpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtShort.Text) || string.IsNullOrEmpty(txtTitle.Text))
            {
                MessageBox1.Message = "فیلدهای لازم را پر کنید.";
                MessageBox1.Visible = true;
                return;
            }

            using (DataAccessDataContext _data = new DataAccessDataContext())
            {

                string shortvalue=txtShort.Text;
                if (_data.PageContents.Where(a => a.Short == shortvalue).Any())
                {
                    MessageBox1.Message = "این آدرس یکتا از قبل وجود دارد.";
                    MessageBox1.Visible = true;
                    return;
                }
                string KeyWords = "";
                PageContent tmp = new PageContent();
                tmp.Text = txtText.Value;
                tmp.Title = txtTitle.Text.Trim();
                 
                if (string.IsNullOrEmpty(txtDesc.Text.Trim()))
                    tmp.Description = CMS.CommonFunctions.SubStringHtml(txtText.Value, 0, 250);
                else
                    tmp.Description = txtDesc.Text;


                tmp.ViewCount = 0;
                tmp.Short = CMS.CommonFunctions.ReplaceSpace(txtShort.Text);
                _data.PageContents.InsertOnSubmit(tmp);
                _data.SubmitChanges();


                Response.Redirect("pagelist.aspx");

            }
        }
    }
}