using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.member
{
    public partial class membermini : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Check_User();
        }
        public void Check_User()
        {
            
            DataAccessDataContext _data = new DataAccessDataContext();
            string pic = "";
            try
            {
                if (((AKUserClass)(Session["User"])).THisUserID == 0)
                {
                    #region cookie
                    if (Request.Cookies["timalog"] != null)
                    {

                        usersdata temp = _data.usersdatas.Where(c => c.Mobile == Request.Cookies["timalog"].Values["UINF"].ToString() && c.Pass == Request.Cookies["timalog"].Values["PINF"].ToString()).Single();

                        if ((bool)temp.IsBanned)
                        {
                            Response.Redirect("/login-1.aspx", false);
                            return;
                        }
                        ((AKUserClass)(Session["User"])).THisUserID = temp.ID;
                        ((AKUserClass)(Session["User"])).FullNameOfUser = temp.FullName;
                        ((AKUserClass)(Session["User"])).EmailOfUser = temp.Mobile;
                        ((AKUserClass)(Session["User"])).PayReqIDs = "0";
                        ((AKUserClass)(Session["User"])).IsUserManager = (bool)temp.IsManager;
                        temp.LastLogin = DateTime.Now;
                        _data.SubmitChanges();


                    }
                    #endregion
                }
                else
                {

                    usersdata temp = _data.usersdatas.Where(c => c.ID == ((AKUserClass)(Session["User"])).THisUserID).Single();
                    

                    if ((bool)temp.IsBanned)
                    {
                        Response.Redirect("/login-1.aspx", true);
                        return;
                    }

                }

            }
            catch
            {
                Response.Redirect("/login-1.aspx", true);
            }

            if (((AKUserClass)(Session["User"])).THisUserID == 0)
            {
                Session.Add("redirectto", Request.RawUrl);
                Response.Redirect("~/login.aspx", true);
                return;
            }
             

        }
    }
}