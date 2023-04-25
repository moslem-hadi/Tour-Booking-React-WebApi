using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class managemini : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void Check_User()
        {
            if (((AKUserClass)(Session["User"])).FullNameOfUser == "admin@webtina")
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
                        ((AKUserClass)(Session["User"])).EmailOfUser = temp.Mobile;
                        ((AKUserClass)(Session["User"])).PayReqIDs = "0";
                        ((AKUserClass)(Session["User"])).IsUserManager = (bool)temp.IsManager;
                        temp.LastLogin = DateTime.Now;
                        _data.SubmitChanges();
                    }
                    catch { }
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
                Response.Redirect("~/AdminPanel/login.aspx");
            }
            if ((bool)_data.usersdatas.Single(a => a.ID == ((AKUserClass)(Session["User"])).THisUserID).IsBanned) { Response.Redirect("~/AdminPanel/login.aspx"); }
        }
    }
}