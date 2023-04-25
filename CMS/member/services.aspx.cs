using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.member
{
    public partial class services : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.member.membermaster MasterPage = (CMS.member.membermaster)Page.Master;
            MasterPage.Check_User();
            DataAccessDataContext _data = new DataAccessDataContext();
            usersdata tmp = _data.usersdatas.Single(a => a.ID == ((AKUserClass)(Session["User"])).THisUserID);
            if (tmp.UserType != (int)UserTypes.Driver)
                Response.Redirect("/member");

            sdsGetDriverUserServices.SelectParameters["UserID"].DefaultValue = ((AKUserClass)(Session["User"])).THisUserID.ToString();

        }
    }
}