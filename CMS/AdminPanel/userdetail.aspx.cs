using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class userdetail : System.Web.UI.Page
    {

        public string fullname, email, regdate, stat, UserID, mobile,   Pic, 
             tell, BankName, BankHesab, BankKart, BankOwnername, BankSheba, BankChangeDate,
             MeliCode, IP, lastip, lastlogin, 
            State, BirthYear, Gender;

     

        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();

            int userid = 0;
            int.TryParse(Request.QueryString["id"], out userid);
            if (userid == 0)
                Response.Redirect("userlist.aspx");
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                usersdata tmp = _data.usersdatas.Single(a => a.ID == userid);

               
                Title = "اطلاعات " + tmp.FullName;

                fullname = tmp.FullName;
                email = tmp.Email;
                regdate = CommonFunctions.String2date(tmp.RegisterDate, 2, "D,H");
                UserID = userid.ToString();
                mobile = string.IsNullOrEmpty(tmp.Mobile) ? "-----" : tmp.Mobile;

                lastip = tmp.LastLoginIP;

                BankName = string.IsNullOrEmpty(tmp.BankName) ? "-----" : tmp.BankName;
                BankHesab = string.IsNullOrEmpty(tmp.BankHesab) ? "-----" : tmp.BankHesab;
                BankKart = string.IsNullOrEmpty(tmp.BankKart) ? "-----" : tmp.BankKart;
                BankOwnername = string.IsNullOrEmpty(tmp.BankOwnername) ? "-----" : tmp.BankOwnername;
                BankSheba = string.IsNullOrEmpty(tmp.BankSheba) ? "-----" : "IR" + tmp.BankSheba;

                IP = tmp.IP;

                lastlogin = CommonFunctions.String2date(tmp.LastLogin, 2, "D") + " - " + CommonFunctions.String2date(tmp.LastLogin, 2, "H");
                Title = "اطلاعات " + tmp.FullName;

                MeliCode = tmp.MeliCode;



            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            int userid = 0;
            int.TryParse(Request.QueryString["id"], out userid);
            if (userid == 0)
                Response.Redirect("userlist.aspx");
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                usersdata tmp = _data.usersdatas.Single(a => a.ID == userid);
                _data.usersdatas.DeleteOnSubmit(tmp);
                 

                Response.Redirect("userlist.aspx");

            }
        }

    }
}