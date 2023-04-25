using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.member
{
    public partial class membermaster : System.Web.UI.MasterPage
    {
        public string unreadticket;
        protected void Page_Load(object sender, EventArgs e)
        {
            Check_User();
             
            ltrdatebtn.Text =  CommonFunctions.String2date(DateTime.Now, 2, "n");

            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                int _unreadticket = _data.tickets.Where(a => a.UserID == ((AKUserClass)(Session["User"])).THisUserID && a.IsRead == false && a.IsDeleted==false).Count();
                if (_unreadticket != 0)
                    unreadticket = string.Format("<span class='notify'>{0}</span>", _unreadticket.ToString());
                //var unreadNot = _data.Notifications.Where(a => a.UserID == ((AKUserClass)(Session["User"])).THisUserID && a.IsRead == false).Count();
                //if (unreadNot != 0)
                //    ltrnotifcnt.Text = "<span class='badge badge-xs badge-danger'>" + unreadNot + "</span>";

                  
            }
        }
        public UserTypes usertype;

        public void Check_User()
        {
            if (Session["User"] == null)
            {
                Session["User"] = new AKUserClass();

                Response.Redirect("/", true);
            }
            if (((AKUserClass)(Session["User"])).FullNameOfUser == "admin@webtina.ir")
            {
                return;
            }
            DataAccessDataContext _data = new DataAccessDataContext();
            try
            {
                if (((AKUserClass)(Session["User"])).THisUserID == 0)
                {
                    #region cookie
                    if (Request.Cookies["timalog"] != null)
                    {

                        usersdata temp = _data.usersdatas.Where(c => c.Mobile == Request.Cookies["timalog"].Values["UINF"].ToString() && c.Pass == Request.Cookies["timalog"].Values["PINF"].ToString()).FirstOrDefault();
                        if(temp==null)
                            Response.Redirect("/", true);

                        usertype = (UserTypes)temp.UserType;
                        if ((bool)temp.IsBanned)
                        {
                            Session["User"] = new AKUserClass();
                            Session["errorCode"] = "حساب کاربری شما مسدود شده است." +(string.IsNullOrEmpty(temp.BannedMsg) ? "" :"علت: "+ temp.BannedMsg);
                            Response.Redirect("/",true);
                            return;
                        }
                        ((AKUserClass)(Session["User"])).THisUserID = temp.ID;
                        ((AKUserClass)(Session["User"])).FullNameOfUser = temp.FullName;
                        ((AKUserClass)(Session["User"])).EmailOfUser = temp.Mobile;
                        ((AKUserClass)(Session["User"])).PayReqIDs = "0";
                        ((AKUserClass)(Session["User"])).IsUserManager = (bool)temp.IsManager;
                        temp.LastLogin = DateTime.Now ;
                        _data.SubmitChanges();
                    
                        if (!temp.IsActive)
                        { 
                            Session["User"] = new AKUserClass();
                            Session["errorCode"] = "حساب کاربری شما فعال نشده است. بر روی لینک فعالسازی که برایتان ایمیل شده است کلیک کنید.";
                            Response.Redirect("/", true);
                        }
                    }
                    #endregion
                }
                else
                {

                    usersdata temp = _data.usersdatas.Where(c => c.ID == ((AKUserClass)(Session["User"])).THisUserID).FirstOrDefault();
                    if (temp == null)
                    {
                        AKUserClass UserClass = new AKUserClass();
                        Session["User"] = UserClass;
                        Response.Redirect("/", true);
                    }
                    usertype = (UserTypes)temp.UserType;
                    if ((bool)temp.IsBanned)
                    {
                        Session["User"] = new AKUserClass();
                        Session["errorCode"] = "حساب کاربری شما مسدود شده است." + (string.IsNullOrEmpty(temp.BannedMsg) ? "" : "علت: " + temp.BannedMsg);
                        Response.Redirect("/", true);
                        return;
                    }
                    if (!temp.IsActive)
                    {
                        Session["User"] = new AKUserClass();
                        Session["errorCode"] = "حساب کاربری شما فعال نشده است. بر روی لینک فعالسازی که برایتان ایمیل شده است کلیک کنید.";
                        Response.Redirect("/", true);
                    }
                     

                }

            }
            catch
            {
                Session["User"] = new AKUserClass();
                Response.Redirect("/", true);
            }

            if (((AKUserClass)(Session["User"])).THisUserID == 0)
            {
                Session["User"] = new AKUserClass();
                Session.Add("redirectto", Request.RawUrl);
                Response.Redirect("~/", true);
                return;
            }
            
            if ((bool)_data.usersdatas.Single(a => a.ID == ((AKUserClass)(Session["User"])).THisUserID).IsBanned)
            {
                Session["User"] = new AKUserClass(); Response.Redirect("~/"); }

            ltr_username.Text = ((AKUserClass)(Session["User"])).FullNameOfUser;
           
            ltrID.Text = ((AKUserClass)(Session["User"])).THisUserID.ToString();


        }

 
       
    }
}