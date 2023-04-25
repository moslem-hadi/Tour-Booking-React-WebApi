using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class groupSpecification : System.Web.UI.Page
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
                SpecificationAttribute tmp = new SpecificationAttribute();
                tmp.Title = txtTitle.Text.Trim().ValidPersian();

                int priority = 0;
                int.TryParse(txtPriority.Text, out priority);
                if(priority==0)
                    try
                    {
                        priority = (int)_data.SpecificationAttributes.Where(a=>a.GroupID==id).Max(a => a.Priority) + 5;
                    }
                    catch { }

                tmp.Priority = priority;
                tmp.UseInSearch = chbFilte.Checked;
                tmp.Status = chbStat.Checked;
                tmp.FiledType = byte.Parse(ddlFieldType.SelectedValue);
                tmp.ShowInProductPage = chbshow.Checked;


                tmp.SpecGroupID = int.Parse(ddlSpecGroup.SelectedValue);
                tmp.GroupID = id;
                _data.SpecificationAttributes.InsertOnSubmit(tmp);
                 
                _data.SubmitChanges();


                if (tmp.FiledType == 3)
                {
                    SpecificationAttributeOption tmpattoption = new SpecificationAttributeOption();
                    tmpattoption.Priority = 1;
                    tmpattoption.SpecificationAttributeID = tmp.ID;
                    tmpattoption.Title = tmp.Title;
                    _data.SpecificationAttributeOptions.InsertOnSubmit(tmpattoption);
                    _data.SubmitChanges();
                }


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
                SpecificationAttribute tmp = _data.SpecificationAttributes.Single(a => a.ID == id);



                if (tmp.Title != txtTitle.Text.Trim().ValidPersian())
                {
                    foreach (ProductSpecification item in _data.ProductSpecifications.Where(a => a.SpecAttrOptionID == tmp.ID))
                    {
                        item.Title = tmp.Title;
                        _data.SubmitChanges();
                    }
                }


                int lastGroupID = (int)tmp.SpecGroupID;
                bool lastshow = (bool)tmp.ShowInProductPage;

                tmp.Title = txtTitle.Text.Trim().ValidPersian();
                tmp.Priority = int.Parse(txtPriority.Text);
                tmp.UseInSearch = chbFilte.Checked;
                tmp.ShowInProductPage = chbshow.Checked;

                if (lastshow != tmp.ShowInProductPage)
                {
                    foreach (ProductSpecification item in _data.ProductSpecifications.Where(a=>a.SpecAttrID==tmp.ID))
                    {
                        item.ShowInProductPage = tmp.ShowInProductPage;
                        _data.SubmitChanges();
                    }
                }

                tmp.Status = chbStat.Checked;
                tmp.SpecGroupID = int.Parse(ddlSpecGroup.SelectedValue);
                tmp.FiledType = byte.Parse(ddlFieldType.SelectedValue);
                _data.SubmitChanges();

                if (lastGroupID != int.Parse(ddlSpecGroup.SelectedValue))
                {
                    foreach (ProductSpecification item in _data.ProductSpecifications.Where(a => a.SpecAttrID == id))
                    {
                        item.SpecGroupID = int.Parse(ddlSpecGroup.SelectedValue);
                        _data.SubmitChanges();
                    }
                }

                Response.Redirect(Request.RawUrl);

            }
        }


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem == null) return;

            int id = int.Parse(DataBinder.Eval(e.Row.DataItem, "ID").ToString());
            Literal ltr_count = (Literal)e.Row.FindControl("ltr_count");
            Literal ltr_SpecGroup = (Literal)e.Row.FindControl("ltr_SpecGroup");
            int SpecGroupID = int.Parse(ltr_SpecGroup.Text);

            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                ltr_count.Text = _data.SpecificationAttributeOptions.Where(a => a.SpecificationAttributeID == id).Count().ToString();

                if (SpecGroupID != 0)
                    ltr_SpecGroup.Text = _data.SpecificationAttributeGroups.Single(a => a.ID == SpecGroupID).Title;
                else
                    ltr_SpecGroup.Text = "-";
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
                        SpecificationAttribute tmp = _data.SpecificationAttributes.Single(a => a.ID == ID);
                        _data.SpecificationAttributes.DeleteOnSubmit(tmp);


                        _data.SpecificationAttributeOptions.DeleteAllOnSubmit(_data.SpecificationAttributeOptions.Where(a => a.SpecificationAttributeID == tmp.ID));


                        
                        _data.ProductSpecifications.DeleteAllOnSubmit(_data.ProductSpecifications.Where(s => s.SpecAttrID == tmp.ID)); 
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
                    SpecificationAttribute tmp = _data.SpecificationAttributes.Single(a => a.ID == id);
                    _data.SpecificationAttributes.DeleteOnSubmit(tmp);
                    _data.SpecificationAttributeOptions.DeleteAllOnSubmit(_data.SpecificationAttributeOptions.Where(a => a.SpecificationAttributeID == tmp.ID));
                    _data.ProductSpecifications.DeleteAllOnSubmit(_data.ProductSpecifications.Where(s => s.SpecAttrID == tmp.ID)); 


                    _data.SpecificationAttributes.DeleteOnSubmit(tmp);


                    _data.SubmitChanges();
                    Response.Redirect(Request.RawUrl);
                }
            }

            if (e.CommandName == "edt")
            {
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    int id = int.Parse(e.CommandArgument.ToString());
                    SpecificationAttribute tmp = _data.SpecificationAttributes.Single(a => a.ID == id);
                    txtTitle.Text = tmp.Title;
                    txtPriority.Text = tmp.Priority.ToString();

                    ddlSpecGroup.SelectedValue = tmp.SpecGroupID.ToString();


                    if ((byte)tmp.FiledType == 3)
                    {

                        Page.RegisterStartupScript("s0", "<script>document.getElementById('noshow').innerText = \"عدم جستجو\";document.getElementById('noshow').style.visibility = \"visible\";document.getElementById('chbFilte').checked = false;document.getElementById('chbFilte').disabled = true;document.getElementById('chbshow').disabled = false;</script>");

                    }
                    chbFilte.Checked = (bool)tmp.UseInSearch;
                    ddlFieldType.SelectedValue = tmp.FiledType.ToString();
                    chbStat.Checked = bool.Parse(tmp.Status.ToString());
                    chbshow.Checked = (bool)tmp.ShowInProductPage;
                    ltrID.Text = tmp.ID.ToString();
                    Button1.Visible = false;
                    cancl.Visible = true;
                    Button2.Visible = true;
                }
            }

        } 
    }
}