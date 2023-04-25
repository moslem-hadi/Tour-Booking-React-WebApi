using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CMS
{
    public partial class Main : System.Web.UI.MasterPage
    { 
        protected void Page_Load(object sender, EventArgs e)
        { 
            UserFunctions();
        } 
        public void UserFunctions()
        {
            DataAccessDataContext _data = new DataAccessDataContext();
            if (Session["User"] == null)
            {
                Session.Add("User", "NULL");

                AKUserClass UserClass = new AKUserClass();
                Session["User"] = UserClass;
                ((AKUserClass)(Session["User"])).THisUserID = 0;
                ((AKUserClass)(Session["User"])).FullNameOfUser = "0";
                ((AKUserClass)(Session["User"])).EmailOfUser = "0";
                ((AKUserClass)(Session["User"])).PayReqIDs = "0";
                ((AKUserClass)(Session["User"])).IsUserManager = false;
                ((AKUserClass)(Session["User"])).IsMiniAdmin = false;
                ((AKUserClass)(Session["User"])).Pic = "";
                  return;
            }

            if (((AKUserClass)(Session["User"])).THisUserID == 0)
            {
                #region cookie
                try
                {
                    if (Request.Cookies["timalog"] != null)
                    {
                        var temp = _data.usersdatas.Where(c => c.Mobile == Request.Cookies["timalog"].Values["UINF"].ToString() && c.Pass == Request.Cookies["timalog"].Values["PINF"].ToString()).FirstOrDefault();
                        if(temp!=null)
                        if (!(bool)temp.IsBanned)
                        {
                            ((AKUserClass)(Session["User"])).THisUserID = temp.ID;
                            ((AKUserClass)(Session["User"])).FullNameOfUser = temp.FullName;
                            ((AKUserClass)(Session["User"])).EmailOfUser = temp.Mobile;
                            ((AKUserClass)(Session["User"])).PayReqIDs = "0";
                                ((AKUserClass)(Session["User"])).UserMobileNum = temp.Mobile;
                                

                                ((AKUserClass)(Session["User"])).IsUserManager = (bool)temp.IsManager;
                            
                            temp.LastLogin = DateTime.Now;
                            _data.SubmitChanges();
                        }
                    }
                }
                catch { }
                #endregion
            }

            
            
        }



        #region compress

        private static readonly Regex REGEX_BETWEEN_TAGS = new Regex(@">\s+<", RegexOptions.Compiled);
        private static readonly Regex REGEX_LINE_BREAKS = new Regex(@"\n\s+", RegexOptions.Compiled);
        protected override void Render(HtmlTextWriter writer)
        {
            using (HtmlTextWriter htmlwriter = new HtmlTextWriter(new System.IO.StringWriter()))
            {
                base.Render(htmlwriter);
                string html = htmlwriter.InnerWriter.ToString();
                html = REGEX_BETWEEN_TAGS.Replace(html, "> <"); html = REGEX_LINE_BREAKS.Replace(html, string.Empty); writer.Write(html.Trim());
            }
        }

        #endregion
    }
}