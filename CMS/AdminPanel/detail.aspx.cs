using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace CMS.Manage
{
    public partial class detail : System.Web.UI.Page
    {
        public string FileID ="0",FileTitle,Sug;

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            int id = 0;
            int.TryParse(Request.QueryString["id"], out id);

            if (!((AKUserClass)(Session["User"])).IsMiniAdmin)
            {
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    _data.ProductComments.DeleteAllOnSubmit(_data.ProductComments.Where(a => a.ProductID == id));
                    _data.ProductGalleries.DeleteAllOnSubmit(_data.ProductGalleries.Where(a => a.ProductID == id));
                    //_data.ProductSpecifications.DeleteAllOnSubmit(_data.ProductSpecifications.Where(a => a.ProductID == id));
                    _data.Products.DeleteAllOnSubmit(_data.Products.Where(a => a.ID == id));
                    _data.SubmitChanges();
                    Response.Redirect("productlist.aspx");
                }
            }
            else Response.Redirect("noaccess.aspx", true);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();
            int id = 0;
                    int.TryParse(Request.QueryString["id"], out id);
                    FileID = id.ToString();
            if (!IsPostBack)
            { 
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    Product tmp = _data.Products.Single(a => a.ID == id);
                    
                     FileTitle = tmp.Title;
                     Sug = tmp.Slug;
                    ltrKeywords.Text = tmp.Keywords+"<br>"+tmp.SpecialKeywords;
                   
                    ltrtitle.Text = "<a href='/view/" + tmp.ID + "/" + tmp.Slug + "' target='_blank'>" + tmp.Title + "</a>";
                    //createMap((int)tmp.GroupID, (int)tmp.SubGroupID);

                    ltrRegDate.Text = CMS.CommonFunctions.String2date(tmp.RegDate, 2, "D");
                    ltrlastedit.Text = CMS.CommonFunctions.String2date(tmp.LastEdit, 2, "H") +" - "+ CMS.CommonFunctions.String2date(tmp.LastEdit, 2, "D");
                    selectedimg.ImageUrl = "~/content/productpic/" + tmp.Pic;
                    hplimg.NavigateUrl = "~/content/productpic/" + tmp.Pic;

                    //ltrHot.Text = (bool)tmp.IsHot ? "<i class='icon-ok'></i> پرفروش" : "<i class='icon-remove'></i> پرفروش";
                    //ltrNew.Text = (bool)tmp.IsNew ? "<i class='icon-ok'></i> جدید" : "<i class='icon-remove'></i> جدید"; ;
                    //ltrRecomended.Text = (bool)tmp.IsHot ? "<i class='icon-ok'></i> فروش ویژه" : "<i class='icon-remove'></i> فروش ویژه"; ;
                    ltrPrice.Text = CommonFunctions.SetCama(tmp.Price.ToString());

                    ltrinserUserID.Text = string.Format("<a href='userdetail.aspx?id={0}'>{0}</a>", tmp.InsertUserID);
                    ltrupdateUserID.Text = string.Format("<a href='userdetail.aspx?id={0}'>{0}</a>", tmp.LastEditUserID);
                    ltrStat.Text = (bool)tmp.IsActive ? "<i class='icon-ok'></i> در حال نمایش" : "<i class='icon-remove'></i> عدم نمایش";
                    


                  

                    ltrViewcount.Text = tmp.ViewCount.ToString();



                    ProductGroup tmpgroup = _data.ProductGroups.Single(a => a.ID == tmp.GroupID);
                    ltrGroup.Text  =   tmpgroup.Title  ;
              




                     

                }
            }
        }
         
          

    }
}