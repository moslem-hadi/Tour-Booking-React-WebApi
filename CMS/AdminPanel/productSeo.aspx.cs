using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class productSeo : System.Web.UI.Page
    {

        public static string keys = "" ;
        protected void Page_Load(object sender, EventArgs e)
        { 

            keys = Request.Form[lsbKeywords.UniqueID];
            if (!IsPostBack)
            {
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    int id = 0;
                    int.TryParse(Request.QueryString["id"], out id);
                    Product tmp = _data.Products.Single(a => a.ID == id);

                    BingKeywords();


                    if(!string.IsNullOrEmpty(tmp.Keywords))
                        foreach (string item in tmp.Keywords.Split(','))
                        {
                            ListItem lst = new ListItem(item, item);
                            int a = lsbKeywords.Items.Count;
                            if (lsbKeywords.Items.Contains(lst))
                                lsbKeywords.Items.FindByValue(item).Selected = true;
                        }

              
                   

                    Page.RegisterStartupScript("s0", "<script>convert("+tmp.Price+", 'persian-price');</script>");
                    //


                    txtShort.Text = tmp.Slug;
                    txtgoogledesc.Text = tmp.SeoDescription;
                    txtgoogledesc.Text = tmp.SeoDescription;
                    txtSeoTitle.Text = tmp.SeoTitle;
                }
            }
        }

        private void BingKeywords()
        {

            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                foreach (ProductKeyword item in _data.ProductKeywords.Distinct().OrderByDescending(a => a.Count))
                {

                    ListItem lst = new ListItem(item.KeyName, item.KeyName);
                    lsbKeywords.Items.Add(lst);

                }
            }
        }

          
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            using (DataAccessDataContext _data = new DataAccessDataContext())
            {

                int id = 0;
                int.TryParse(Request.QueryString["id"], out id);
                Product tmp = _data.Products.Single(a => a.ID == id);
                 
                tmp.SeoDescription = CommonFunctions.SubStringText(txtgoogledesc.Text, 0, 200).ValidPersian();

                tmp.SeoTitle = txtSeoTitle.Text.ValidPersian();

                tmp.LastEdit = DateTime.Now;
                 
                tmp.Slug = CommonFunctions.ReplaceSpace(txtShort.Text.Trim().ToLower()).ValidPersian();
                tmp.LastEditUserID = ((AKUserClass)(Session["User"])).THisUserID;

                #region keywords

                string KeyWords = keys;

                tmp.Keywords = KeyWords.ValidPersian(); if (!string.IsNullOrEmpty(KeyWords))
                {
                    string[] key = KeyWords.Split(',');

                    foreach (string keyitem in key)
                    {
                        if (!_data.ProductKeywords.Any(a => a.KeyName == keyitem))
                        {
                            ProductKeyword tmpkey = new ProductKeyword();
                            tmpkey.KeyName = keyitem.ValidPersian();

                            tmpkey.Count = 1;
                            _data.ProductKeywords.InsertOnSubmit(tmpkey);
                            _data.SubmitChanges();
                        }
                    }
                }

                #endregion
                _data.SubmitChanges();


                string PageNum = "";
                if (!string.IsNullOrEmpty(Request.QueryString["retp"]))
                    PageNum = Request.QueryString["retp"];
                if (!string.IsNullOrEmpty(PageNum))
                    Response.Redirect("Productlist.aspx" + Server.UrlDecode(PageNum) + "#" + tmp.ID, true);
                else

                    Response.Redirect("Productlist.aspx", true);
            }
        }
         

     
    }
}