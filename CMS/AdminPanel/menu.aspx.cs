using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class menu : System.Web.UI.Page
    {

        class LocalDataSource
        {
            public string Title { get; set; }
            public string Link { get; set; }
            public int Priority { get; set; }
            public string Position { get; set; }
            public int ID { get; set; }
        }

        List<LocalDataSource> groupgridlist = new List<LocalDataSource>();
        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();
            if (!IsPostBack)
            {
                ListItem lst = new ListItem("منوی اصلی", "0");
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
                foreach (menulink item in _data.menulinks.Where(a => a.ParentID == id).OrderBy(a => a.Position).ThenBy(a=>a.Priority).ThenBy(a=>a.ID))
                {
                    LocalDataSource tmp = new LocalDataSource();
                    tmp.Title = level.Remove(0, 2) + " " + item.Title;
                    tmp.ID = item.ID;
                    tmp.Priority = (int)item.Priority;
                    tmp.Link = item.Link;
                    tmp.Position = (int)item.Position==1? "اصلی":"فوتر";
                    groupgridlist.Add(tmp);
                    if (item.ParentID == 0)
                    {

                        if (!IsPostBack)
                        {
                            ListItem lst = new ListItem(level + " " + item.Title +$"({tmp.Position})", item.ID.ToString());
                            DropDownList1.Items.Add(lst);
                        }
                        getgroup(item.ID, level + " —");

                    }
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

                menulink tmp = new menulink();
                tmp.Title = txtTitle.Text.Trim();
                tmp.Link = txtLink.Text.Trim();
                int Priority = 0;
                int.TryParse(txtPriority.Text, out Priority);
                tmp.Priority = Priority;
                tmp.ParentID = int.Parse(DropDownList1.SelectedItem.Value);
                tmp.Position=short.Parse(ddlPosition.SelectedItem.Value);
                _data.menulinks.InsertOnSubmit(tmp);
                _data.SubmitChanges();
                txtTitle.Text = string.Empty;
                txtPriority.Text = string.Empty;
                txtLink.Text = string.Empty;
                BindGrid();
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
                menulink tmp = _data.menulinks.Single(a => a.ID == id);


                tmp.Title = txtTitle.Text.Trim();
                tmp.Link = txtLink.Text.Trim();
                int Priority = 0;
                int.TryParse(txtPriority.Text, out Priority);
                tmp.Priority = Priority;
                tmp.ParentID = int.Parse(DropDownList1.SelectedItem.Value);
                tmp.Position=short.Parse(ddlPosition.SelectedItem.Value);
                _data.SubmitChanges();
                Response.Redirect("menu.aspx");
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
                        menulink tmp = _data.menulinks.Single(a => a.ID == ID);
                        _data.menulinks.DeleteOnSubmit(tmp);
                        _data.SubmitChanges();

                    }
                }
            }
            Response.Redirect("menu.aspx");



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
                    menulink tmp = _data.menulinks.Single(a => a.ID == id);
                    _data.menulinks.DeleteOnSubmit(tmp);
                    _data.SubmitChanges();
                    Response.Redirect("menu.aspx");
                }
            }

            if (e.CommandName == "edt")
            {
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    int id = int.Parse(e.CommandArgument.ToString());
                    menulink tmp = _data.menulinks.Single(a => a.ID == id);
                    txtLink.Text = tmp.Link;
                    txtTitle.Text = tmp.Title;
                    txtPriority.Text = tmp.Priority.ToString();
                    ltrID.Text = tmp.ID.ToString();
                    ddlPosition.SelectedValue = tmp.Position.ToString();
                    DropDownList1.SelectedValue = tmp.ParentID.ToString();
                    Button1.Visible = false;
                    cancl.Visible = true;
                    Button2.Visible = true;
                }
            }

        } 
    }
}