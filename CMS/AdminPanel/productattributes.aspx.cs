using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class productattributes : System.Web.UI.Page
    {

        class LocalDataSource
        {
            public string Title { get; set; }
            public long ID { get; set; }
            public int Priority { get; set; }
        }

        static List<LocalDataSource> groupgridlist = new List<LocalDataSource>();
        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();
            if (!IsPostBack)
            {
                int id = 0;
                int.TryParse(Request.QueryString["id"], out id);
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {

                    Product tmp = _data.Products.Single(a => a.ID == id);
                    ltrTitle.Text = string.Format("<a href='detail.aspx?id={0}'>{1}</a>", tmp.ID, tmp.Title);
                }
                groupgridlist.Clear();
                getgroup(id);
            }

            GridView1.DataSource = groupgridlist;

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
            GridView1.DataBind();
        }


        void getgroup(long id)
        {
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                foreach (ProductAttribute item in _data.ProductAttributes.Where(a => a.ProductID == id).OrderBy(a=> a.Priority))
                {
                    LocalDataSource tmp = new LocalDataSource();
                    try
                    {
                        
                        tmp.Title = item.Title;
                        tmp.ID = item.ID;
                        tmp.Priority = (int)item.Priority;
                        groupgridlist.Add(tmp);

                    }
                    catch { }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                if (string.IsNullOrEmpty(txtTitle.Text))
                {
                    MessageBox1.Message = "فیلدهای لازم را پر کنید.";
                    MessageBox1.Visible = true;
                    return;
                }



                int id = 0;
                int.TryParse(Request.QueryString["id"], out id);



                ProductAttribute tmp = new ProductAttribute();
                 
                tmp.Description = txtDesc.Text;
                tmp.IsMultiSelect = !(rdbSingle.Checked); 
                tmp.Priority = int.Parse(txtPriority.Text);
                tmp.ProductID = id;
                tmp.Title = txtTitle.Text;

                _data.ProductAttributes.InsertOnSubmit(tmp);
                _data.SubmitChanges();


                string url = Request.Url.GetLeftPart(UriPartial.Path);
                Response.Redirect(string.Format("{0}", url + "?id=" + id));
            }
        }





        protected void Button2_Click(object sender, EventArgs e)
        {

            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                int id = int.Parse(ltrID.Text);
                if (string.IsNullOrEmpty(txtTitle.Text))
                {
                    MessageBox1.Message = "فیلدهای لازم را پر کنید.";
                    MessageBox1.Visible = true;
                    return;
                }

                 

                ProductAttribute tmp = _data.ProductAttributes.Single(a=>a.ID==id);
                 
                tmp.Description = txtDesc.Text;
                tmp.IsMultiSelect = !(rdbSingle.Checked); 
                tmp.Priority = int.Parse(txtPriority.Text);
             
                tmp.Title = txtTitle.Text;
                 
                _data.SubmitChanges();


                string url = Request.Url.GetLeftPart(UriPartial.Path);
                Response.Redirect(string.Format("{0}", url + "?id=" + tmp.ProductID));
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int index = e.NewPageIndex + 1;
            string url = Request.Url.GetLeftPart(UriPartial.Path);

            e.Cancel = true; ;
            Response.Redirect(string.Format("{0}", url, index));
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            int id = 0;
            int.TryParse(Request.QueryString["id"], out id);
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

                        ProductAttribute tmp = _data.ProductAttributes.Single(a => a.ID == ID);
                        _data.ProductAttributes.DeleteOnSubmit(tmp);
                        _data.SubmitChanges();

                    }
                }
            }

            string url = Request.Url.GetLeftPart(UriPartial.Path);
            Response.Redirect(string.Format("{0}", url + "?id=" + id));

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "Del")
            {
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    ProductAttribute tmp = _data.ProductAttributes.Single(a => a.ID == id);
                    _data.ProductAttributes.DeleteOnSubmit(tmp);
                    id = (int)tmp.ProductID;
                    _data.SubmitChanges();

                }
                string url = Request.Url.GetLeftPart(UriPartial.Path);
                Response.Redirect(string.Format("{0}", url + "?id=" + id));
            }

            if (e.CommandName == "edt")
            {
                using (DataAccessDataContext _data = new DataAccessDataContext())
                { 
                    ProductAttribute tmp = _data.ProductAttributes.Single(a => a.ID == id);
                    ltrID.Text = id.ToString();
                    txtDesc.Text = tmp.Description; 
                    txtPriority.Text = tmp.Priority.ToString();
                    txtTitle.Text = tmp.Title;
                    rdbSingle.Checked = !(bool)tmp.IsMultiSelect;
                    RadioButton2.Checked = !rdbSingle.Checked;


                    cancl.NavigateUrl = "productattributes.aspx?id=" + Request.QueryString["id"];




                    Button1.Visible = false;
                    cancl.Visible = true;
                    Button2.Visible = true;
                }
            }


        }
    }
}