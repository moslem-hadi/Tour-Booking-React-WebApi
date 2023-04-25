using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] str = Request.Cookies.AllKeys;
            foreach (string co in str)
            {
                if (co == "timalog" || co==CommonFunctions.TokenCookieName)
                    Response.Cookies[co].Expires = DateTime.Now.AddDays(-1);
            }

            ((AKUserClass)(Session["User"])).THisUserID = 0;
            ((AKUserClass)(Session["User"])).FullNameOfUser = "0";
            ((AKUserClass)(Session["User"])).EmailOfUser = "";
            ((AKUserClass)(Session["User"])).IsUserManager  = false;
            ((AKUserClass)(Session["User"])).PayReqIDs = "";
            Session.Abandon();
            Session.Clear();
            Response.Redirect("~/");


        }
    }
}