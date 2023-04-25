using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web.Services;
using System.Web.Script.Services;
using CMS.Classes;
using Newtonsoft.Json;

namespace CMS.Manage
{
    public partial class productlist : System.Web.UI.Page
    {
        public string PageNum = "%3fpage%3d1";//?page=1
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetTypes();
                string dir = "desc", sort = "id", gid = "-1", key = "",  Type="-1";
                if (!string.IsNullOrEmpty(Request.QueryString["key"]))
                {
                    key = Request.QueryString["key"].Replace("+", " ");
                    TextBox1.Text = key;
                }

                if (!string.IsNullOrEmpty(Request.QueryString["dir"]))
                    dir = Request.QueryString["dir"];


                if (!string.IsNullOrEmpty(Request.QueryString["sort"]))
                    sort = Request.QueryString["sort"];

                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                    gid = Request.QueryString["id"];

                if (!string.IsNullOrEmpty(Request.QueryString["Type"]))
                    Type = Request.QueryString["Type"];
                 
                if (!string.IsNullOrEmpty(dir))
                {
                    ddlDir.SelectedValue = dir;
                    ddlSort.SelectedValue = sort;
                }
                if (gid != "-1")
                    txtgroup.Text = gid;
                if (Type != "-1")
                    ddlType.SelectedValue = Type;
                 
            }
            if (!string.IsNullOrEmpty(Request.Url.Query))
                PageNum = Server.UrlEncode(Request.Url.Query); 
        }

        private void GetTypes()
        {
            var productTypes = new List<ProductTypesJson>();
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                var jsons = _data.GlobalValues.Where(a => a.Type == (int)GlobalValueTypes.ProductType)
                    .Select(a => a.Value).ToList();
                foreach (var item in jsons)
                {
                    productTypes.Add(JsonConvert.DeserializeObject<ProductTypesJson>(item));
                }
                ddlType.DataSource = productTypes;
                ddlType.DataBind();
            }
        }

        //protected void LinkButton1_Click(object sender, EventArgs e)
        //{
        //    using (DataAccessDataContext _data = new DataAccessDataContext())
        //    {
        //        CheckBox chkAdd;
        //        int rowCount = GridView1.Rows.Count;
        //        for (int i = 0; i <= (rowCount - 1); i++)
        //        {
        //            chkAdd = (CheckBox)GridView1.Rows[i].FindControl("chkBxSelect");
        //            int ID = int.Parse(GridView1.DataKeys[i].Value.ToString());
        //            if (chkAdd.Checked == true)
        //            {
        //                Product tmp = _data.Products.Single(a => a.ID == ID);
        //                if (File.Exists(Server.MapPath("~/content/productpic/" + tmp.Pic)) && tmp.Pic != "nopic.png")
        //                {
        //                    try
        //                    {
        //                        File.Delete("~/content/productpic/" + tmp.Pic);
        //                    }
        //                    catch { }
        //                }

        //                foreach (ProductGallery item in _data.ProductGalleries.Where(a => a.ProductID == ID))
        //                {

        //                    if (File.Exists(Server.MapPath("~/content/productpic/gallery/" + item.FileName)))
        //                    {
        //                        try
        //                        {
        //                            File.Delete("~/content/productpic/gallery//" + item.FileName);
        //                        }
        //                        catch { }
        //                    }
        //                }

        //                _data.Products.DeleteOnSubmit(tmp);
        //                _data.SubmitChanges();

        //            }
        //        }
        //        Response.Redirect("productlist.aspx");
        //    }
        //}


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<ProductGroup> getGroups(string pre)
        {
            List<ProductGroup> allproduct = new List<ProductGroup>();
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                allproduct = _data.ProductGroups.Where(a => a.Title.Contains(pre)).OrderBy(a => a.Title).ToList();
            }
            return allproduct;
        }

     

        protected void ListView1_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            dpItems2.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            dpItems.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            string key=TextBox1.Text.Trim();
           
            int id=0;
            int.TryParse(key, out id);
            if(id!=0)
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    if (_data.Products.Where(a => a.ID == id).Count() == 1)
                        Response.Redirect("detail.aspx?id=" + id);
                }

            string dir, sort, gid,stat, Type;
            int intval = 0;

            int.TryParse(txtgroup.Text, out intval);
            if (intval == 0)
                gid = "-1";
            else gid = intval.ToString();

            int.TryParse(ddlType.SelectedValue, out intval);
            if (intval == 0)
                Type = "-1";
            else Type = intval.ToString();


            sort = ddlSort.SelectedItem.Value;
            dir = ddlDir.SelectedItem.Value;
             



            string url = Request.Url.GetLeftPart(UriPartial.Path);
            Response.Redirect(url + "?key=" +CommonFunctions.SafeSqlLiteral(key.Replace(" ", "+")) + "&dir=" + dir + "&sort=" + sort + "&id=" + gid+"&type="+ Type);
        }
         
    }
}