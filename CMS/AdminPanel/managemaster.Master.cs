using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class managemaster : System.Web.UI.MasterPage
    {
        private static readonly Regex REGEX_BETWEEN_TAGS = new Regex(@">\s+<", RegexOptions.Compiled);
        private static readonly Regex REGEX_LINE_BREAKS = new Regex(@"\n\s+", RegexOptions.Compiled);
        protected override void Render(HtmlTextWriter writer)
        {

            using (HtmlTextWriter htmlwriter = new HtmlTextWriter(new System.IO.StringWriter()))
            {

                base.Render(htmlwriter);

                string html = htmlwriter.InnerWriter.ToString();



                html = REGEX_BETWEEN_TAGS.Replace(html, "> <");

                html = REGEX_LINE_BREAKS.Replace(html, string.Empty);



                writer.Write(html.Trim());

            }

        }
        public string month, year, day, unread, userName, prodcnt, ordercnt, commentcnt, unreadticket;
        protected void Page_Load(object sender, EventArgs e)
        {
            Check_User();
            CreateMap();

            string[] days = CMS.CommonFunctions.String2date(DateTime.Now, 2, "M").Split(' ');
            year = days[2];
            month = days[1];
            day = days[0];
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                int unreadmsg = _data.contactPms.Where(a => a.IsRead == false).Count();
                if (unreadmsg != 0)
                    unread = string.Format("<span class='notify'>{0}</span>", unreadmsg);
                 
                int comnt=_data.ProductComments.Where(a => a.IsRead == false ).Count();
                if(comnt!=0)
                    commentcnt = string.Format("<span class='notify'>{0}</span>", comnt.ToString());

                int _unreadticket = _data.tickets.Where(a => !(bool)a.IsManageRead).Count();
                if (_unreadticket != 0)
                    unreadticket = string.Format("<span class='notify'>{0}</span>", _unreadticket.ToString());

            }
            userName = ((AKUserClass)(Session["User"])).FullNameOfUser;

            if (!IsPostBack)
                ltrDate.Text = CommonFunctions.String2date(DateTime.Now, 2, "n");

            if (Request.RawUrl.ToLower().Contains("explore.aspx"))
            {
                divSideBar.Visible = false;
                divMainContent.Attributes.Remove("class");
            }
        }

        private void CreateMap()
        {
            string url = System.IO.Path.GetFileName(Request.Url.AbsolutePath).ToLower();
            switch (url)
            {
                case "default.aspx":
                    ltrmap.Text = "<li class='active'>داشــبورد</li>";
                    break;
                case "changepass.aspx":
                    ltrmap.Text = "<li class='active'>تغییر رمز ورود</li>";
                    break;
                case "pagelist.aspx":
                    ltrmap.Text = "<li class='active'>لیست صفحات</li>";
                    break;
                case "contentgroup.aspx":
                    ltrmap.Text = "<li class='active'>لیست گروه مطالب</li>";
                    break;
                case "ticketlist.aspx":
                    ltrmap.Text = "<li class='active'>لیست تیکت ها</li>";
                    break;
                case "stats.aspx":
                    ltrmap.Text = "<li class='active'>آمار سایت</li>";
                    break;
                case "productlist.aspx":
                    ltrmap.Text = "<li class='active'>لیست محصولات</li>";
                    break;
                case "specifications.aspx":
                    ltrmap.Text = "<li class='active'>مشخصه های محصول</li>";
                    break;
                case "contentlist.aspx":
                    ltrmap.Text = "<li class='active'>لیست مطالب</li>";
                    break;
                case "homepagelist.aspx":
                    ltrmap.Text = "<li class='active'>لیست بخش ها</li>";
                    break;
                case "smssender.aspx":
                    ltrmap.Text = "<li class='active'>ارسال اسمس</li>";
                    break;
                case "banners.aspx":
                    ltrmap.Text = "<li class='active'>لیست بنرهای بازاریابی</li>";
                    break;
                case "defaultpic.aspx":
                    ltrmap.Text = "<li class='active'>لیست تصاویر پیشفرض</li>";
                    break;
                case "afflist.aspx":
                    ltrmap.Text = "<li class='active'>لیست  پورسانت ها</li>";
                    break;
                case "menu.aspx":
                    ltrmap.Text = "<li class='active'>لیست  منو ها</li>";
                    break;
                case "paylist.aspx":
                    ltrmap.Text = "<li class='active'>لیست  پرداختی ها</li>";
                    break;
                case "payrequest.aspx":
                    ltrmap.Text = "<li class='active'>لیست درخواست های برداشت</li>";
                    break;
                case "siteearn.aspx":
                    ltrmap.Text = "<li class='active'>دریافتی های سایت</li>";
                    break;
                case "contestlist.aspx":
                    ltrmap.Text = "<li class='active'>لیست مسابقات</li>";
                    break;
                case "colorlist.aspx":
                    ltrmap.Text = "<li class='active'>لیست رنگ ها</li>";
                    break;
                case "advertising.aspx":
                    ltrmap.Text = "<li class='active'>لیست تبلیغات</li>";
                    break;
                case "slider.aspx":
                    ltrmap.Text = "<li class='active'>لیست تصاویر اسلایدر</li>";
                    break;
                case "productgroup.aspx":
                    ltrmap.Text = "<li class='active'>لیست شهر</li>";
                    break;
                case "userlist.aspx":
                    ltrmap.Text = "<li class='active'>لیست کاربران</li>";
                    break;
                case "inbox.aspx":
                    ltrmap.Text = "<li class='active'>صندوق دریافت پیام</li>";
                    break;
                case "setting.aspx":
                    ltrmap.Text = "<li class='active'>تنظیمات</li>";
                    break;
                case "reminders.aspx":
                    ltrmap.Text = "<li class='active'>اطلاعیه ها</li>";
                    break;
                case "seincome.aspx":
                    ltrmap.Text = "<li class='active'>ورودی های موتورهای جستجو</li>";
                    break;
                case "orderlist.aspx":
                    ltrmap.Text = "<li class='active'>لیست سفارشات</li>";
                    break;
                case "comment.aspx":
                    ltrmap.Text = "<li class='active'>لیست سفارشات</li>";
                    break;
                case "couponlist.aspx":
                    ltrmap.Text = "<li class='active'>لیست کوپن های تخفیف</li>";
                    break;
                case "userincomereport.aspx":
                    ltrmap.Text = "<li class='active'>آمار مالی کاربر</li>";
                    break;
                case "upgradelist.aspx":
                    ltrmap.Text = "<li class='active'>لیست اشتراک ها</li>";
                    break;
                case "addcoupon.aspx":
                    ltrmap.Text = "<li><a href='couponlist.aspx'>لیست کوپن های تخفیف</a><span class='divider'>&raquo;</span></li><li class='active'>افزودن کوپن تخفیف</li>";
                    break;
                case "addupgrade.aspx":
                    ltrmap.Text = "<li><a href='upgradelist.aspx'>لیست اشتراک ها</a><span class='divider'>&raquo;</span></li><li class='active'>افزودن اشتراک</li>";
                    break;
                case "editupgrade.aspx":
                    ltrmap.Text = "<li><a href='upgradelist.aspx'>لیست اشتراک ها</a><span class='divider'>&raquo;</span></li><li class='active'>ویرایش اشتراک</li>";
                    break;
                case "productfiles.aspx":
                    ltrmap.Text = "<li><a href='productlist.aspx'>لیست محصولات</a><span class='divider'>&raquo;</span></li><li class='active'>لیست فایل های محصول</li>";
                    break;
                case "productspecs.aspx":
                    ltrmap.Text = "<li><a href='productlist.aspx'>لیست محصولات</a><span class='divider'>&raquo;</span></li><li class='active'>مشخصه های محصول</li>";
                    break;
                case "editcoupon.aspx":
                    ltrmap.Text = "<li><a href='couponlist.aspx'>لیست کوپن های تخفیف</a><span class='divider'>&raquo;</span></li><li class='active'>ویرایش کوپن تخفیف</li>";
                    break;
                case "specoption.aspx":
                    ltrmap.Text = "<li>مشخصه های محصول <span class='divider'>&raquo;</span></li><li class='active'>گزینه های مشخصه</li>";
                    break;
                case "groupspecgroups.aspx":
                    ltrmap.Text = "<li class='active'>دسته بندی مشخصه ها</li>";
                    break;
                case "addorder.aspx":
                    ltrmap.Text = "<li><a href='orderlist.aspx'>لیست سفارشات</a><span class='divider'>&raquo;</span></li><li class='active'>افزودن سفارش</li>";
                    break;
                case "addtransatction.aspx":
                    ltrmap.Text = "<li class='active'>افزودن تراکنش</li>";
                    break;
                case "reservs.aspx":
                    ltrmap.Text = "<li class='active'>لیست سفارشات</li>";
                    break;
                case "explore.aspx":
                    ltrmap.Text = "<li class='active'>مدیریت فایل‌ها</li>";
                    break;
                case "reservedetail.aspx":
                    ltrmap.Text = "<li><a href='reservs.aspx'>لیست سفارشات</a><span class='divider'>&raquo;</span></li><li class='active'>جزئیات سفارش</li>";
                    break;
                case "groupspecification.aspx":
                    ltrmap.Text = "<li class='active'>مشخصه های محصولات</li>";
                    break;
                case "adduser.aspx":
                    ltrmap.Text = "<li><a href='userlist.aspx'>لیست کاربران</a><span class='divider'>&raquo;</span></li><li class='active'>افزودن کاربر</li>";
                    break;
                case "readcomment.aspx":
                    ltrmap.Text = "<li><a href='comment.aspx'>لیست نظرات</a><span class='divider'>&raquo;</span></li><li class='active'>مشاهده نظر</li>";
                    break;
                case "order.aspx":
                    ltrmap.Text = "<li><a href='orderlist.aspx'>لیست سفارشات</a><span class='divider'>&raquo;</span></li><li class='active'>مشاهده سفارش</li>";
                    break;
                case "editorder.aspx":
                    ltrmap.Text = "<li><a href='orderlist.aspx'>لیست سفارشات</a><span class='divider'>&raquo;</span></li><li class='active'>ویرایش سفارش</li>";
                    break;
                case "sendticket.aspx":
                    ltrmap.Text = "<li><a href='ticketlist.aspx'>لیست تیکت ها</a><span class='divider'>&raquo;</span></li><li class='active'>ارسال تیکت</li>";
                    break;
                case "viewcontest.aspx":
                    ltrmap.Text = "<li><a href='contestlist.aspx'>لیست مسابقات</a><span class='divider'>&raquo;</span></li><li class='active'>مشاهده مسابقه</li>";
                    break;

                case "userdetail.aspx":
                    ltrmap.Text = "<li><a href='userlist.aspx'>لیست کاربران</a><span class='divider'>&raquo;</span></li><li class='active'>اطلاعات کاربر</li>";
                    break;

                case "edituser.aspx":
                    ltrmap.Text = "<li><a href='userlist.aspx'>لیست کاربران</a><span class='divider'>&raquo;</span></li><li class='active'>ویرایش کاربر</li>";
                    break;

                case "editreminder.aspx":
                    ltrmap.Text = "<li><a href='reminders.aspx'>اطلاعیه ها</a><span class='divider'>&raquo;</span></li><li class='active'>ویرایش اطلاعیه</li>";
                    break;
                case "addreminder.aspx":
                    ltrmap.Text = "<li><a href='reminders.aspx'>اطلاعیه ها</a><span class='divider'>&raquo;</span></li><li class='active'>افزودن اطلاعیه</li>";
                    break;
                case "readmail.aspx":
                    ltrmap.Text = "<li><a href='inbox.aspx'>صندوق دریافت</a><span class='divider'>&raquo;</span></li><li class='active'>مشاهده پیام</li>";
                    break;
                case "gasht":
                case "tour":
                case "transfer":
                case "selecttype.aspx":
                    ltrmap.Text = "<li><a href='productlist.aspx'>لیست محصولات</a><span class='divider'>&raquo;</span></li><li class='active'>افزودن محصول</li>";
                    break;
                case "relatedproduct.aspx":
                    ltrmap.Text = "<li><a href='productlist.aspx'>لیست محصولات</a><span class='divider'>&raquo;</span></li><li class='active'>محصولات مرتبط</li>";
                    break;
                case "productdetails.aspx":
                    ltrmap.Text = "<li><a href='productlist.aspx'>لیست محصولات</a><span class='divider'>&raquo;</span></li><li class='active'>مشخصه های محصول</li>";
                    break;
                case "productattributes.aspx":
                    ltrmap.Text = "<li><a href='productlist.aspx'>لیست محصولات</a><span class='divider'>&raquo;</span></li><li class='active'>ویژگی  های محصول</li>";
                    break;

                case "productattributeoptions.aspx":
                    ltrmap.Text = "<li><a href='productlist.aspx'>لیست محصولات</a><span class='divider'>&raquo;</span></li><li class='active'>ویژگی  های محصول</li><span class='divider'>&raquo;</span></li><li class='active'>گزینه ویژگی  های محصول</li>";
                    break;
                case "detail.aspx":
                    ltrmap.Text = "<li><a href='productlist.aspx'>لیست محصولات</a><span class='divider'>&raquo;</span></li><li class='active'>مشاهده محصول</li>";
                    break;
                case "editproduct.aspx":
                    ltrmap.Text = "<li><a href='productlist.aspx'>لیست محصولات</a><span class='divider'>&raquo;</span></li><li class='active'>ویرایش محصول</li>";
                    break;
                case "pricelist.aspx":
                    ltrmap.Text = "<li><a href='productlist.aspx'>لیست محصولات</a><span class='divider'>&raquo;</span></li><li class='active'>ویرایش قمیت ها</li>";
                    break;
                case "productimages.aspx":
                    ltrmap.Text = "<li><a href='productlist.aspx'>لیست محصولات</a><span class='divider'>&raquo;</span></li><li class='active'>لیست تصاویر</li>";
                    break;
                case "addpage.aspx":
                    ltrmap.Text = "<li><a href='pagelist.aspx'>لیست صفحات</a><span class='divider'>&raquo;</span></li><li class='active'>افزودن صفحه</li>";
                    break;
                case "addcontent.aspx":
                    ltrmap.Text = "<li><a href='contentlist.aspx'>لیست مطالب</a><span class='divider'>&raquo;</span></li><li class='active'>افزودن مطلب</li>";
                    break;
                case "editcontent.aspx":
                    ltrmap.Text = "<li><a href='contentlist.aspx'>لیست مطالب</a><span class='divider'>&raquo;</span></li><li class='active'>ویرایش مطلب</li>";
                    break;


                case "addcontest.aspx":
                    ltrmap.Text = "<li><a href='contestlist.aspx'>لیست مسابقات</a><span class='divider'>&raquo;</span></li><li class='active'>افزودن مسابقه</li>";
                    break;
                case "editcontest.aspx":
                    ltrmap.Text = "<li><a href='contestlist.aspx'>لیست مسابقات</a><span class='divider'>&raquo;</span></li><li class='active'>ویرایش مسابقه</li>";
                    break;

                case "editpage.aspx":
                    ltrmap.Text = "<li><a href='pagelist.aspx'>لیست صفحات</a><span class='divider'>&raquo;</span></li><li class='active'>ویرایش صفحه</li>";
                    break;

            }
        }
        public void Check_User()
        {
            if (Session["User"] == null)
            {

                Response.Redirect("/AdminPanel/login.aspx", true);
            }





            if (((AKUserClass)(Session["User"])).FullNameOfUser == "admin@webtina" && ((AKUserClass)(Session["User"])).THisUserID==1)
            {
                return;
            }
            DataAccessDataContext _data = new DataAccessDataContext();

            if (((AKUserClass)(Session["User"])).THisUserID == 0)
            {
                #region cookie
                if (Request.Cookies["timalog"] != null)
                {
                    try
                    {
                        usersdata temp = _data.usersdatas.Where(c => c.Mobile == Request.Cookies["timalog"].Values["UINF"].ToString() && c.Pass == Request.Cookies["timalog"].Values["PINF"].ToString()).Single();

                        ((AKUserClass)(Session["User"])).THisUserID = temp.ID;
                        ((AKUserClass)(Session["User"])).FullNameOfUser = temp.FullName;
                        ((AKUserClass)(Session["User"])).EmailOfUser = temp.Email;
                        ((AKUserClass)(Session["User"])).PayReqIDs = "0";
                        
                        
                        ((AKUserClass)(Session["User"])).IsUserManager = (bool)temp.IsManager;
                        
                        temp.LastLogin = DateTime.Now;
                        _data.SubmitChanges();
                    }
                    catch
                    {
                    }
                }
                #endregion
            }

            

            if (((AKUserClass)(Session["User"])).THisUserID == 0)
            {
                Session.Add("redirectto", Request.RawUrl);
                Response.Redirect("~/AdminPanel/login.aspx");
            }

            if (!((AKUserClass)(Session["User"])).IsUserManager)
            {
                //if (!((AKUserClass)(Session["User"])).IsMiniAdmin)
                    Response.Redirect("~/AdminPanel/login.aspx");
            }


            //if (((AKUserClass)(Session["User"])).IsMiniAdmin)
            //{

            //    if (Request.RawUrl.ToLower().Contains("/productspecs.aspx") || Request.RawUrl.ToLower().Contains("/productSpecs.aspx") || Request.RawUrl.ToLower().Contains("/productlist.aspx") ||
            //        Request.RawUrl.ToLower() == "/AdminPanel" || Request.RawUrl.ToLower() == "/AdminPanel/" || Request.RawUrl.ToLower().Contains("/default.aspx") || Request.RawUrl.ToLower().Contains("/login.aspx") || Request.RawUrl.ToLower().Contains("/addproduct.aspx") || Request.RawUrl.ToLower().Contains("/detail.aspx?id=") ||
            //       Request.RawUrl.ToLower().Contains("/changepass.aspx") || Request.RawUrl.ToLower().Contains("/relatedproduct.aspx?id=") ||
            //    Request.RawUrl.ToLower().Contains("/editproduct.aspx?id=") || Request.RawUrl.ToLower().Contains("/copy.aspx") || Request.RawUrl.ToLower().Contains("/productimages.aspx?id="))
            //    {

            //    }
            //    else
            //    {

            //        Response.Redirect("~/AdminPanel/noaccess.aspx");
            //    }


            //}


            if ((bool)_data.usersdatas.Single(a => a.ID == ((AKUserClass)(Session["User"])).THisUserID).IsBanned) { Response.Redirect("~/AdminPanel/login.aspx"); }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            string key = TextBox1.Text.Trim();
            if (key == string.Empty)
                Response.Redirect("Productlist.aspx");
            int id = 0;
            int.TryParse(key, out id);
            if (id != 0)
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    if (_data.Products.Where(a => a.ID == id).Count() == 1)
                        Response.Redirect("detail.aspx?id=" + id,true);
                } 
            Response.Redirect(  "Productlist.aspx?key=" + key.Replace(" ", "+"));
        }
    }
}