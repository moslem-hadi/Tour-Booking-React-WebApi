using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class GroupSpecGroups : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();

             
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
               // int.TryParse(Request.QueryString["id"], out id);
                SpecificationAttributeGroup tmp = new SpecificationAttributeGroup();
                tmp.Title = txtTitle.Text.Trim().ValidPersian();
                tmp.Priority = int.Parse(txtPriority.Text);
                tmp.GroupID = id;
                _data.SpecificationAttributeGroups.InsertOnSubmit(tmp);
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
                SpecificationAttributeGroup tmp = _data.SpecificationAttributeGroups.Single(a => a.ID == id);


                tmp.Title = txtTitle.Text.Trim().ValidPersian();
                tmp.Priority = int.Parse(txtPriority.Text);
                _data.SubmitChanges();

                Response.Redirect(Request.RawUrl);

            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem == null) return;

            int id = int.Parse(DataBinder.Eval(e.Row.DataItem, "ID").ToString());
            Literal ltr_count = (Literal)e.Row.FindControl("ltr_count");

            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                ltr_count.Text = _data.SpecificationAttributes.Where(a => a.SpecGroupID == id).Count().ToString();
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
                        SpecificationAttributeGroup tmp = _data.SpecificationAttributeGroups.Single(a => a.ID == ID);
                        _data.SpecificationAttributeGroups.DeleteOnSubmit(tmp);

                        foreach (SpecificationAttribute item in _data.SpecificationAttributes.Where(a=>a.SpecGroupID==tmp.ID))
                        {
                            item.SpecGroupID = 0;
                            _data.SubmitChanges();
                        }
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
                    SpecificationAttributeGroup tmp = _data.SpecificationAttributeGroups.Single(a => a.ID == id);

                    foreach (SpecificationAttribute item in _data.SpecificationAttributes.Where(a => a.SpecGroupID == tmp.ID))
                    {
                        item.SpecGroupID = 0;
                        _data.SubmitChanges();
                    }


                    _data.SpecificationAttributeGroups.DeleteOnSubmit(tmp);

                    _data.SubmitChanges();
                    Response.Redirect(Request.RawUrl);
                }
            }

            if (e.CommandName == "edt")
            {
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    int id = int.Parse(e.CommandArgument.ToString());
                    SpecificationAttributeGroup tmp = _data.SpecificationAttributeGroups.Single(a => a.ID == id);
                 
                    txtTitle.Text = tmp.Title;
                    txtPriority.Text = tmp.Priority.ToString();

 
                    ltrID.Text = tmp.ID.ToString();
                    Button1.Visible = false;
                    cancl.Visible = true;
                    Button2.Visible = true;
                }
            }

        } 
    }
}