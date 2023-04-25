using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class productSpecs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    int id = 0;
                    int.TryParse(Request.QueryString["id"], out id);
                    Product tmp = _data.Products.Single(a => a.ID == id);
                    ltrtitle.Text = string.Format("<a href='detail.aspx?id={0}'>{1}</a>",tmp.ID,tmp.Title);
                    ltrgrop.Text = _data.ProductGroups.Single(a => a.ID == tmp.GroupID).Title;

                    if (_data.SpecificationAttributes .Count() == 0)
                    {
                        Button1.Visible = false;
                        ltrer.Text = "<p class='center empty'> مشخصه ای تعریف نشده است</p>";
                    }
                    

                }
            }
        }




        protected void ItemDB(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.DataItemIndex.ToString() != "-1")
            {
                int id = 0;
                int.TryParse(Request.QueryString["id"], out id);

                SqlDataSource sdsGetSpecOptions = e.Item.FindControl("sdsGetSpecOptions") as SqlDataSource;
                sdsGetSpecOptions.SelectParameters["specID"].DefaultValue = DataBinder.Eval(e.Item.DataItem, "ID").ToString();
                int specID= int.Parse(DataBinder.Eval(e.Item.DataItem, "ID").ToString());

                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    HiddenField hiddenField = e.Item.FindControl("hf_Type") as HiddenField;

                    switch (Convert.ToByte(hiddenField.Value))
                    {
                        case 3:
                            {
                                TextBox TXT = e.Item.FindControl("txtText") as TextBox;
                                TXT.Visible = true;

                                if (_data.ProductSpecifications.Where(a => a.ProductID == id && a.SpecAttrID == specID).Count() == 1)
                                {
                                    TXT.Text = _data.ProductSpecifications.Single(a => a.ProductID == id && a.SpecAttrID == specID).Value.Replace("<br>", "\r\n");
                                }

                                break;
                            }
                        case 1:
                            DropDownList ddlOptions = e.Item.FindControl("ddlOptions") as DropDownList;
                            ddlOptions.Visible = true;
                            break;
                        case 2:
                            ListBox lsbOptions = e.Item.FindControl("lsbOptions") as ListBox;
                            lsbOptions.Visible = true;
                            break;

                        case 4:
                            Literal divider = e.Item.FindControl("divider") as Literal;
                            divider.Visible = true;
                            break;
                        default:
                            break;
                    }






                }
            }
        }
        //protected void SubItemDB(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemIndex.ToString() != "-1")
        //    {
               
        //        var repeater = (Repeater)sender;
        //        var parentItem = (ListViewItem)repeater.NamingContainer;
                

        //        HiddenField hiddenField = parentItem.FindControl("hf_Type") as HiddenField;

        //        switch (Convert.ToByte(hiddenField.Value))
        //        {
        //            case 3:
        //                TextBox TXT = e.Item.FindControl("TextBox1") as TextBox;
        //                TXT.Visible = true;
        //                break;
        //            case 1:
        //                RadioButton RDB = e.Item.FindControl("RadioButton1") as RadioButton;
        //                RDB.Visible = true;
        //                break;
        //            case 2:
        //                CheckBox CHB = e.Item.FindControl("CheckBox1") as CheckBox;
        //                CHB.Visible = true;
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}




        public string getID(object val)
        {
            return val.ToString();
        }

        protected void ddlOptions_DataBound(object sender, EventArgs e)
        {
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                int id = 0;
                int.TryParse(Request.QueryString["id"], out id);



                DropDownList list = sender as DropDownList;
                int selectedvalue = 0;
                int specID = 0;


                ListViewItem item = (ListViewItem)list.NamingContainer;
                HiddenField hd_ID = (HiddenField)item.FindControl("hd_ID");



                specID = int.Parse(hd_ID.Value);
                if (_data.ProductSpecifications.Where(a => a.ProductID == id && a.SpecAttrID == specID).Count() == 1)
                {
                    selectedvalue = (int)_data.ProductSpecifications.Single(a => a.ProductID == id && a.SpecAttrID == specID).SpecAttrOptionID;

                    try
                    {
                        list.SelectedValue = selectedvalue.ToString();
                    }
                    catch
                    { }
                }
            }
        }

        protected void lsbOptions_DataBound(object sender, EventArgs e)
        {
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                int id = 0;
                int.TryParse(Request.QueryString["id"], out id);



                ListBox list = sender as ListBox;
       
                int specID = 0;


                ListViewItem item = (ListViewItem)list.NamingContainer;
                HiddenField hd_ID = (HiddenField)item.FindControl("hd_ID");



                specID = int.Parse(hd_ID.Value);

                foreach (ListItem li in list.Items)
                {
                    if (_data.ProductSpecifications.Where(a => a.ProductID == id && a.SpecAttrID == specID && a.SpecAttrOptionID == int.Parse(li.Value)).Count() == 1)
                    {


                        li.Selected = true;
                    }
                }
            }
        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            int id = 0;
            int.TryParse(Request.QueryString["id"], out id);

            
            foreach (ListViewItem lvitem in lvwItems.Items)
            {
                int spcID = Convert.ToInt32(lvwItems.DataKeys[lvitem.DataItemIndex][0]);
                
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    bool ShowInProductPage = (bool)_data.SpecificationAttributes.Single(a => a.ID == spcID).ShowInProductPage;
                    SpecificationAttribute tmpattr = _data.SpecificationAttributes.Single(a => a.ID == spcID);
                     

                    if (lvitem.FindControl("txtText") != null)
                        if (lvitem.FindControl("txtText").Visible != false)
                        {
                            TextBox txtValue = (TextBox)lvitem.FindControl("txtText");
                            if (!string.IsNullOrEmpty(txtValue.Text.Trim()))
                            {

                                if (_data.ProductSpecifications.Where(a => a.ProductID == id && a.SpecAttrID == tmpattr.ID).Count() == 1)
                                {
                                    ProductSpecification tmp = _data.ProductSpecifications.Single(a => a.ProductID == id && a.SpecAttrID == tmpattr.ID);
                                  
                                    int specoptionID = _data.SpecificationAttributeOptions.SingleOrDefault(a => a.SpecificationAttributeID == tmpattr.ID).ID;
                                    tmp.SpecAttrOptionID = specoptionID;
                                    tmp.Title = tmpattr.Title;
                                    tmp.ShowInProductPage = ShowInProductPage;
                                    if (txtValue.Text == "-")
                                        tmp.ShowInProductPage = false;
                                    tmp.SpecGroupID = tmpattr.SpecGroupID;
                                    tmp.Value = txtValue.Text.ValidPersian().Replace("\r\n", "<br>");
                                    _data.SubmitChanges();
                                }
                                else
                                {
                                    ProductSpecification tmp = new ProductSpecification();
                                    tmp.Priority = tmpattr.Priority;
                                    int specoptionID = _data.SpecificationAttributeOptions.SingleOrDefault(a => a.SpecificationAttributeID == tmpattr.ID).ID;
                                    tmp.ProductID = id;
                                    tmp.SpecAttrOptionID = specoptionID;
                                    tmp.SpecAttrID = tmpattr.ID;
                                    tmp.Title = tmpattr.Title;
                                    tmp.ShowInProductPage = ShowInProductPage;
                                    if (txtValue.Text == "-")
                                        tmp.ShowInProductPage = false;
                                    tmp.SpecGroupID = tmpattr.SpecGroupID;
                                    tmp.Value = txtValue.Text.ValidPersian().Replace("\r\n", "<br>");
                                    _data.ProductSpecifications.InsertOnSubmit(tmp);
                                    _data.SubmitChanges();
                                }
                            }
                        }

                     


                    if (lvitem.FindControl("ddlOptions") != null)
                        if (lvitem.FindControl("ddlOptions").Visible != false)
                        {
                            DropDownList ddlOptions = (DropDownList)lvitem.FindControl("ddlOptions");


                            int specoptionID = int.Parse(ddlOptions.SelectedValue);
                            if (_data.ProductSpecifications.Where(a => a.ProductID == id && a.SpecAttrID == tmpattr.ID).Count() == 1)
                            {
                                int beforeSavedSpecOptionID = (int)_data.ProductSpecifications.Single(a => a.ProductID == id && a.SpecAttrID == tmpattr.ID).SpecAttrOptionID;

                                if (specoptionID != beforeSavedSpecOptionID)
                                {

                                    ProductSpecification tmp = _data.ProductSpecifications.Single(a => a.ProductID == id && a.SpecAttrID == tmpattr.ID);
                                    tmp.Priority = tmpattr.Priority;
                                    tmp.SpecAttrOptionID = specoptionID;
                                    tmp.Title = tmpattr.Title;
                                    tmp.ShowInProductPage = ShowInProductPage;
                                    tmp.Value = ddlOptions.SelectedItem.Text;
                                    if (ddlOptions.SelectedItem.Text == "-")
                                        tmp.ShowInProductPage = false;
                                    tmp.SpecGroupID = tmpattr.SpecGroupID;
                                    _data.SubmitChanges();
                                }
                            }
                            else
                            {

                                ProductSpecification tmp = new ProductSpecification();
                                tmp.Priority = tmpattr.Priority;
                                tmp.ProductID = id;
                                tmp.SpecAttrOptionID = specoptionID;
                                tmp.SpecAttrID = tmpattr.ID; 
                                tmp.ShowInProductPage = ShowInProductPage;
                                tmp.Title = tmpattr.Title; tmp.SpecGroupID = tmpattr.SpecGroupID;
                                tmp.Value = ddlOptions.SelectedItem.Text;

                                if (ddlOptions.SelectedItem.Text == "-")
                                    tmp.ShowInProductPage = false;
                                _data.ProductSpecifications.InsertOnSubmit(tmp);
                                _data.SubmitChanges();
                            }



                        }



                    if (lvitem.FindControl("lsbOptions") != null)
                        if (lvitem.FindControl("lsbOptions").Visible != false)
                        {
                            ListBox lsbOptions = (ListBox)lvitem.FindControl("lsbOptions");


                            foreach (ListItem li in lsbOptions.Items)
                            {
                                if (li.Selected)
                                {
                                    if (_data.ProductSpecifications.Where(a => a.ProductID == id && a.SpecAttrID == tmpattr.ID && a.SpecAttrOptionID == int.Parse(li.Value)).Count() == 0)
                                    {

                                        ProductSpecification tmp = new ProductSpecification();
                                        tmp.Priority = tmpattr.Priority;
                                        int specoptionID = int.Parse(li.Value);
                                        tmp.ProductID = id;
                                        tmp.SpecAttrOptionID = specoptionID;
                                        tmp.SpecAttrID = tmpattr.ID;
                                        tmp.Title = tmpattr.Title;
                                        tmp.ShowInProductPage = ShowInProductPage;
                                        tmp.Value = li.Text; tmp.SpecGroupID = tmpattr.SpecGroupID;
                                        _data.ProductSpecifications.InsertOnSubmit(tmp);
                                        _data.SubmitChanges();
                                    }

                                }
                                else
                                {
                                    if (_data.ProductSpecifications.Where(a => a.ProductID == id && a.SpecAttrID == tmpattr.ID && a.SpecAttrOptionID == int.Parse(li.Value)).Count() != 0)
                                    {

                                        ProductSpecification tmp = _data.ProductSpecifications.Single(a => a.ProductID == id && a.SpecAttrID == tmpattr.ID && a.SpecAttrOptionID == int.Parse(li.Value));
                                        
                                        _data.ProductSpecifications.DeleteOnSubmit(tmp);
                                        _data.SubmitChanges();
                                    }
                                }


                            }


                        }





                }
            }


            if (Request.QueryString["t"] != null)
                if (Request.QueryString["t"] == "new")
                    Response.Redirect("productimages.aspx?id=" + id,true);
            
            

                Response.Redirect("detail.aspx?id=" + id);

        }
         
    }
}