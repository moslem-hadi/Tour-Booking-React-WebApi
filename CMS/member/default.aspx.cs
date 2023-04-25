using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.member
{
    public partial class _default : System.Web.UI.Page
    {
        public UserTypes userType;
        public string  currentmoney;
        protected void Page_Load(object sender, EventArgs e)
        {
            DataAccessDataContext _data = new DataAccessDataContext();
            CMS.member.membermaster MasterPage = (CMS.member.membermaster)Page.Master;
            MasterPage.Check_User();

            usersdata tmp = _data.usersdatas.Single(a => a.ID == ((AKUserClass)(Session["User"])).THisUserID);
            userType = (UserTypes)tmp.UserType;



            sdsLatestOrders.SelectParameters["userId"].DefaultValue = tmp.ID.ToString();
            sdsGetDriverUserServices.SelectParameters["userId"].DefaultValue = tmp.ID.ToString();


            currentmoney = CommonFunctions.SetCama(CommonFunctions.GetUserCurrentBalance(tmp.ID).ToString());

            if (_data.Reminders.Where(a => a.IsDone == false && (((DateTime)a.ToDate >= DateTime.Now && (DateTime)a.FromDate <= DateTime.Now) || a.FromDate == a.ToDate)).Count() == 0)
            { bullist.Visible = false; }

            //            if (string.IsNullOrEmpty(tmp.SIteName))

            //ltrshoperr.Text = "<div class='warning'><a href='shopinfo.aspx' style='color:#f01' >فروشگاه خود را بسازید. برای ایجاد فروشگاه، اینجا کلیک کنید.</a></div>";


        }

         

    }
}