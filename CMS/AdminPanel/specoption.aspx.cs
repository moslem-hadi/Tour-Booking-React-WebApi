using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class specoption : System.Web.UI.Page
    {

        class LocalDataSource
        {
            public string Title { get; set; }
            public string Priority { get; set; }
            public int ID { get; set; }
        }

        static List<LocalDataSource> groupgridlist = new List<LocalDataSource>();
        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();
            int specid = int.Parse(Request.QueryString["id"]);
            

            cancl.NavigateUrl = Request.RawUrl;

            if (!IsPostBack)
            {
                groupgridlist.Clear();
                getgroup(specid); 
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    SpecificationAttribute tmp = _data.SpecificationAttributes.Single(a => a.ID == specid);
                    ltrtitle.Text = tmp.Title;

                    hplback.NavigateUrl = "groupSpecification.aspx?id=" + tmp.GroupID + "#" + specid;
                }
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

        void getgroup(int id)
        {
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                foreach (SpecificationAttributeOption item in _data.SpecificationAttributeOptions.Where(a => a.SpecificationAttributeID == id).OrderBy(a=>a.Priority))
                {
                    LocalDataSource tmp = new LocalDataSource();
                    tmp.Title =  item.Title;
                    tmp.ID = item.ID;
                    tmp.Priority = item.Priority.ToString();
                    groupgridlist.Add(tmp);
                      
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

                SpecificationAttributeOption tmp = new SpecificationAttributeOption();
                tmp.Title = txtTitle.Text.Trim().ValidPersian();
                int specid = int.Parse(Request.QueryString["id"]);
                tmp.SpecificationAttributeID = specid;


                int priority = 0;
                int.TryParse(txtPriority.Text, out priority);
                if (priority == 0)
                    try
                    {
                        priority = (int)_data.SpecificationAttributeOptions.Where(a => a.SpecificationAttributeID == specid).Max(a => a.Priority) + 3;
                    }
                    catch { }
                tmp.Priority = priority;

                _data.SpecificationAttributeOptions.InsertOnSubmit(tmp);
                _data.SubmitChanges();
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                if (string.IsNullOrEmpty(txtTitle.Text))
                {
                    MessageBox1.Message = "فیلدهای لازم را پر کنید.";
                    MessageBox1.Visible = true;
                    return;
                }

                int id = int.Parse(ltrID.Text);
                SpecificationAttributeOption tmp = _data.SpecificationAttributeOptions.Single(a => a.ID == id);

                tmp.Title = txtTitle.Text.Trim();


                if (tmp.Title != txtTitle.Text.Trim().ValidPersian())
                {
                    foreach (ProductSpecification item in _data.ProductSpecifications.Where(a => a.SpecAttrOptionID == tmp.ID))
                    {
                        item.Value = tmp.Title;
                        _data.SubmitChanges();
                    }
                }
                tmp.Priority = int.Parse(txtPriority.Text);
                _data.SubmitChanges();
                Response.Redirect(Request.RawUrl);
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
                        SpecificationAttributeOption tmp = _data.SpecificationAttributeOptions.Single(a => a.ID == ID);


                        _data.ProductSpecifications.DeleteAllOnSubmit(_data.ProductSpecifications.Where(a => a.SpecAttrOptionID == tmp.ID));


                        _data.SpecificationAttributeOptions.DeleteOnSubmit(tmp);
                        _data.SubmitChanges();

                    }
                }
            }
            Response.Redirect(Request.RawUrl);



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
                    SpecificationAttributeOption tmp = _data.SpecificationAttributeOptions.Single(a => a.ID == id);
                    _data.SpecificationAttributeOptions.DeleteOnSubmit(tmp);
                 _data.ProductSpecifications.DeleteAllOnSubmit(_data.ProductSpecifications.Where(a => a.SpecAttrOptionID == tmp.ID));
 
                    _data.SubmitChanges();
                    Response.Redirect(Request.RawUrl);
                }
            }

            if (e.CommandName == "edt")
            {
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    int id = int.Parse(e.CommandArgument.ToString());
                    SpecificationAttributeOption tmp = _data.SpecificationAttributeOptions.Single(a => a.ID == id);
                    txtPriority.Text = tmp.Priority.ToString();
                    txtTitle.Text = tmp.Title;
                    ltrID.Text = tmp.ID.ToString();
                    Button1.Visible = false;
                    cancl.Visible = true;
                    Button2.Visible = true;
                }
            }

        } 
    }
}