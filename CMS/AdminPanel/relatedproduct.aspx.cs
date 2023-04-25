using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class relatedproduct : System.Web.UI.Page
    {
        class LocalDataSource
        {
            public string Title { get; set; } 
            public long ID { get; set; }
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
                    ltrTitle.Text = string.Format("<a href='detail.aspx?id={0}'>{1}</a>",tmp.ID,tmp.Title);
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
                foreach (RelatedProduct item in _data.RelatedProducts.Where(a => a.ProductID == id))
                {
                    LocalDataSource tmp = new LocalDataSource();
                    try
                    {
                        Product pppp = _data.Products.Single(x => x.ID == item.RelatedProductID);

                        tmp.Title = pppp.Title;
                        tmp.ID = item.ID;

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
                if (string.IsNullOrEmpty(txtIds.Text))
                {
                    MessageBox1.Message = "فیلدهای لازم را پر کنید.";
                    MessageBox1.Visible = true;
                    return;
                }

                string a = txtIds.Text;

                string[] delimiter = { Environment.NewLine };

                string[] ids = a.Split(delimiter, StringSplitOptions.None); 


                 

                int id = 0;
                int.TryParse(Request.QueryString["id"], out id);

                foreach (string item in ids)
                { 
                    int productid = 0;
                    int.TryParse(item.Trim(), out productid);
                    if (productid == 0)
                        continue;
                    if (productid == id)
                        continue;
                    try
                    {
                        Product pppp = _data.Products.Single(x => x.ID == productid);

                        if (_data.RelatedProducts.Where(x => x.ProductID == id && x.RelatedProductID == productid).Count() == 0)
                        {
                            RelatedProduct tmp = new RelatedProduct();// _data.RelatedProducts.Single(a => a.ID == id);

                            tmp.ProductID = id;
                            tmp.RelatedProductID = productid;
                            _data.RelatedProducts.InsertOnSubmit(tmp);
                            _data.SubmitChanges();
                        }

                        if (CheckBox2.Checked)
                        {

                            if (_data.RelatedProducts.Where(x => x.RelatedProductID == id && x.ProductID == productid).Count() == 0)
                            {
                                RelatedProduct tmp = new RelatedProduct();// _data.RelatedProducts.Single(a => a.ID == id);

                                tmp.ProductID = productid;
                                tmp.RelatedProductID = id;
                                _data.RelatedProducts.InsertOnSubmit(tmp);
                                _data.SubmitChanges();
                            }
                        }
                    }
                    catch { }

                }

                string url = Request.Url.GetLeftPart(UriPartial.Path);
                Response.Redirect(string.Format("{0}", url + "?id=" + id));
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

                        RelatedProduct tmp = _data.RelatedProducts.Single(a => a.ID == ID);
                        _data.RelatedProducts.DeleteOnSubmit(tmp);
                        _data.SubmitChanges();

                    }
                }
            }

            string url = Request.Url.GetLeftPart(UriPartial.Path);
            Response.Redirect(string.Format("{0}", url + "?id=" + id));

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            long id = long.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "Del")
            {
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    RelatedProduct tmp = _data.RelatedProducts.Single(a => a.ID == id);
                    _data.RelatedProducts.DeleteOnSubmit(tmp);
                    id = (long)tmp.ProductID;
                    _data.SubmitChanges();

                }
                string url = Request.Url.GetLeftPart(UriPartial.Path);
                Response.Redirect(string.Format("{0}", url + "?id=" + id));
            }
             

        } 
    }
}