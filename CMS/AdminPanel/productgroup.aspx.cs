using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class productgroup : System.Web.UI.Page
    {

        class LocalDataSource
        {
            public string Title { get; set; }
            public string Text { get; set; }
            public string slug { get; set; }
            public string Priority { get; set; }
            public int ID { get; set; }
        }

        List<LocalDataSource> groupgridlist = new List<LocalDataSource>();
        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();

            if (!IsPostBack)
            {
                ListItem lst = new ListItem("استان", "0");
                DropDownList1.Items.Add(lst);
            }
            BindGrid();
        }

        void BindGrid()
        {

            groupgridlist.Clear();
            getgroup(0, " —");

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

        void getgroup(int id, string level)
        {
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                foreach (ProductGroup item in _data.ProductGroups.Where(a => a.ParentID == id).OrderBy(a => a.Priority))
                {
                    LocalDataSource tmp = new LocalDataSource();
                    tmp.Title = level.Remove(0, 2) + " " + item.Title;
                    tmp.ID = item.ID;
                    tmp.Text = item.Text;
                    tmp.slug = item.Slug;
                    tmp.Priority = item.Priority.ToString();
                    groupgridlist.Add(tmp);


                    if (!IsPostBack)
                    {
                        ListItem lst = new ListItem(level + " " + item.Title, item.ID.ToString());
                        DropDownList1.Items.Add(lst);
                    }
                    getgroup(item.ID, level + " —");

                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            MessageBox1.Visible = false;
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                if (string.IsNullOrEmpty(txtTitle.Text) || string.IsNullOrEmpty(txtSlug.Text) || string.IsNullOrEmpty(txtSeoTitle.Text))
                {
                    MessageBox1.Message = "فیلدهای لازم را پر کنید.";
                    MessageBox1.Visible = true;
                    return;
                }

                string slug = CommonFunctions.ReplaceSpace(txtSlug.Text.ValidPersian().ToLower());
                if (_data.ProductGroups.Where(a => a.Slug == slug).Count() != 0)
                {
                    MessageBox1.Message = "url تکراری است.";
                    MessageBox1.Visible = true;
                    return;
                }

                ProductGroup tmp = new ProductGroup();
                tmp.Title = txtTitle.Text.Trim().ValidPersian();
                tmp.Text = "";
                tmp.ParentID = int.Parse(DropDownList1.SelectedValue);
                

                tmp.Priority = int.Parse(txtPriority.Text);

                tmp.Show = chbshow.Checked; 

                tmp.SeoDescription = txtSeoDescription.Text.ValidPersian();
                tmp.SeoTitle = txtSeoTitle.Text.ValidPersian();
                tmp.Slug = slug;



                _data.ProductGroups.InsertOnSubmit(tmp);
                _data.SubmitChanges();
                txtTitle.Text =   txtSeoTitle.Text = txtSlug.Text = string.Empty;

                //setGroupMapping((int)tmp.ID, (int)tmp.ParentID);






                if (tmp.ParentID == 0)
                {
                    ListItem lst = new ListItem("— " + tmp.Title, tmp.ID.ToString());
                    DropDownList1.Items.Add(lst);
                }
                BindGrid();
            }
        }

        //protected void setGroupMapping(int groupID, int ParentID)
        //{
        //    using (DataAccessDataContext _data = new DataAccessDataContext())
        //    {

        //        if (ParentID == 0)
        //        {
        //            if (_data.ProductGroupMappings.Where(a => a.AncestorID == groupID && a.ChildID == groupID).Count() == 0)
        //            {
        //                ProductGroupMapping tmp = new ProductGroupMapping();
        //                tmp.AncestorID = groupID;
        //                tmp.ChildID = groupID;
        //                tmp.PathLength = 0;
        //                _data.ProductGroupMappings.InsertOnSubmit(tmp);
        //                _data.SubmitChanges();
        //            }
        //        }
        //        else
        //        {

        //            // باید لیست والدها گرفته بشه و با کد گروه جدید اضافه شده تک تک اد بشه


        //            parentids.Clear();
        //            level = 0;
        //            getAllParentIDs(groupID);

        //            StringBuilder listString = new StringBuilder();

        //            foreach (string s in parentids)
        //            {
        //                string[] item = s.Split('#');
        //                int JadID = int.Parse(item[0]);
        //                int PathLenght = int.Parse(item[1]);


        //                if (_data.ProductGroupMappings.Where(a => a.AncestorID == JadID && a.ChildID == groupID).Count() == 0)
        //                {
        //                    ProductGroupMapping tmp = new ProductGroupMapping();
        //                    tmp.AncestorID = JadID;
        //                    tmp.ChildID = groupID;
        //                    tmp.PathLength = PathLenght;
        //                    _data.ProductGroupMappings.InsertOnSubmit(tmp);
        //                    _data.SubmitChanges();
        //                }
        //            }
        //        }
        //    }
        //}



        //protected void deleteMapping()
        //{
        //    using (DataAccessDataContext _data = new DataAccessDataContext())
        //    {
        //        _data.ProductGroupMappings.DeleteAllOnSubmit(_data.ProductGroupMappings);
        //        _data.SubmitChanges();

        //        foreach (ProductGroup tmp in _data.ProductGroups)
        //        {
        //            setGroupMapping((int)tmp.ID, (int)tmp.ParentID);
        //        }
        //    }

        //}

        ArrayList parentids = new ArrayList();
        int level = 0;
        protected void getAllParentIDs(int groupID)
        {
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                ProductGroup tmp = _data.ProductGroups.Single(a => a.ID == groupID);

                parentids.Add(tmp.ID + "#" + level++);

                if (tmp.ParentID != 0)
                {
                    getAllParentIDs((int)tmp.ParentID);
                }
            }
        }

        [WebMethod]
        public static string GetUrl(int groupID)
        {
            
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                ProductGroup tmp = _data.ProductGroups.Single(a => a.ID == groupID);
                
                    return tmp.Slug;
                
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            MessageBox1.Visible = false;
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                if (string.IsNullOrEmpty(txtTitle.Text) || string.IsNullOrEmpty(txtSlug.Text) || string.IsNullOrEmpty(txtSeoTitle.Text))
                {
                    MessageBox1.Message = "فیلدهای لازم را پر کنید.";
                    MessageBox1.Visible = true;
                    return;
                }

                int id = int.Parse(ltrID.Text);

                if (id == int.Parse(DropDownList1.SelectedValue))
                {

                    MessageBox1.Message = "استان را بدرستی انتخاب کنید.";
                    MessageBox1.Visible = true;
                    return;
                }
                string slug = CommonFunctions.ReplaceSpace(txtSlug.Text.ValidPersian().ToLower());
                if (_data.ProductGroups.Where(a => a.Slug == slug && a.ID != id).Count() != 0)
                {
                    MessageBox1.Message = "url تکراری است.";
                    MessageBox1.Visible = true;
                    return;
                }

                ProductGroup tmp = _data.ProductGroups.Single(a => a.ID == id);
                
                tmp.Priority = int.Parse(txtPriority.Text);

                tmp.Show = chbshow.Checked;
                tmp.Title = txtTitle.Text.Trim();
                tmp.Text = "";
                bool ischanged = false;
                if (tmp.ParentID != int.Parse(DropDownList1.SelectedValue))
                {
                    ischanged = true;
                }
                tmp.SeoDescription = txtSeoDescription.Text.ValidPersian();
                tmp.SeoTitle = txtSeoTitle.Text.ValidPersian();
                tmp.Slug = slug;



                tmp.ParentID = int.Parse(DropDownList1.SelectedValue);


                _data.SubmitChanges();

               // if (ischanged) { deleteMapping(); }
                Response.Redirect("ProductGroup.aspx");
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem == null) return;

            int id = int.Parse(DataBinder.Eval(e.Row.DataItem, "ID").ToString());
            Literal ltr_count = (Literal)e.Row.FindControl("ltr_count");

            using (DataAccessDataContext _data = new DataAccessDataContext())
            {

                ltr_count.Text = _data.Products.Where(a => a.GroupID == id && a.IsActive==true ).Count().ToString();
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            CheckBox chkAdd;
            int rowCount = GridView1.Rows.Count;
            string idList = "";
            for (int i = 0; i <= (rowCount - 1); i++)
            {
                chkAdd = (CheckBox)GridView1.Rows[i].FindControl("chkBxSelect");
                int ID = int.Parse(GridView1.DataKeys[i].Value.ToString());
                if (chkAdd.Checked == true)
                {
                    using (DataAccessDataContext _data = new DataAccessDataContext())
                    {
                        ProductGroup tmp = _data.ProductGroups.Single(a => a.ID == ID);
                        _data.ProductGroups.DeleteOnSubmit(tmp);
                        _data.SubmitChanges();

                    }
                }
            }
            Response.Redirect("ProductGroup.aspx");



        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int index = e.NewPageIndex + 1;
            string url = Request.Url.GetLeftPart(UriPartial.Path);

            e.Cancel = true; ;
            Response.Redirect(string.Format("{0}?page={1}", url, index));
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    int id = int.Parse(e.CommandArgument.ToString());
                    ProductGroup tmp = _data.ProductGroups.Single(a => a.ID == id);
                    _data.ProductGroups.DeleteOnSubmit(tmp);

                    deleteChilds(tmp.ID);
                    _data.Products.DeleteAllOnSubmit(_data.Products.Where(a => a.GroupID == tmp.ID));
                    _data.SubmitChanges();
                    //deleteMapping();

                    Response.Redirect("ProductGroup.aspx");

                }
            }

            if (e.CommandName == "edt")
            {
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    int id = int.Parse(e.CommandArgument.ToString());
                    ProductGroup tmp = _data.ProductGroups.Single(a => a.ID == id);
                    
                    txtTitle.Text = tmp.Title;
                    DropDownList1.SelectedValue = tmp.ParentID.ToString();
                    ltrID.Text = tmp.ID.ToString();
                    txtSeoDescription.Text = tmp.SeoDescription;
                    txtPriority.Text = tmp.Priority.ToString();
                    txtSeoTitle.Text = tmp.SeoTitle;
                    txtSlug.Text = tmp.Slug;
                    Button1.Visible = false;
                    chbshow.Checked = (bool)tmp.Show;
                    cancl.Visible = true;
                    Button2.Visible = true;
                }
            }

        }

        private void deleteChilds(int groupid)
        {
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                foreach (ProductGroup item in _data.ProductGroups.Where(a => a.ParentID == groupid))
                {
                    _data.Products.DeleteAllOnSubmit(_data.Products.Where(a => a.GroupID == item.ID));
                    deleteChilds(item.ID);

                    _data.ProductGroups.DeleteOnSubmit(item);
                    _data.SubmitChanges();
                }
            }
        }
    }
}