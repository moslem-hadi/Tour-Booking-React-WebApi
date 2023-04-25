using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class editpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();

            if(!IsPostBack)
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    int id = 0;
                    int.TryParse(Request.QueryString["id"], out id);
                    PageContent tmp = _data.PageContents.Single(a => a.ID == id);

                    txtDesc.Text = tmp.Description;
                    txtShort.Text = tmp.Short;
                    txtText.Value = tmp.Text;
                    txtTitle.Text = tmp.Title;
                }
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

                string shortvalue = txtShort.Text;
                int id = 0;
                int.TryParse(Request.QueryString["id"], out id);
                if (_data.PageContents.Where(a => a.Short == shortvalue && a.ID!=id).Count() != 0)
                {
                    MessageBox1.Message = "این آدرس یکتا از قبل وجود دارد.";
                    MessageBox1.Visible = true;
                    return;
                }

                string KeyWords = "";
                PageContent tmp = _data.PageContents.Single(a => a.ID == id);
                tmp.Text = txtText.Value;
                tmp.Title = txtTitle.Text.Trim();
                  
                if (string.IsNullOrEmpty(txtDesc.Text.Trim()))
                    tmp.Description = CMS.CommonFunctions.SubStringHtml(txtText.Value, 0, 250);
                else
                    tmp.Description = txtDesc.Text;

                 
                tmp.Short = CMS.CommonFunctions.ReplaceSpace(txtShort.Text);
                _data.SubmitChanges();


                Response.Redirect("pagelist.aspx");

            }
        }
    }
}