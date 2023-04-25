using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using System.IO;
using System.Threading;

namespace CMS
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
             string url = Request.Url.ToString().ToLower();
            if (!url.Contains("parhantransfer") && !url.Contains("localhost"))
            {
                Response.Redirect("http://webtina.ir");
                return;
            }

            if (url.Contains("parhantransfer.com"))
            {
                url = url.Replace("parhantransfer.com", "parhantransfer.ir");

                //Response.Status = "301 Moved Permanently";
                //Response.AddHeader("Location", url);

                Response.RedirectPermanent(url, true);
            }
            else if (url.Contains("www.webtina"))
            {
                url = url.Replace("www.webtina", "webtina");

                //Response.Status = "301 Moved Permanently";
                //Response.AddHeader("Location", url);

                Response.RedirectPermanent(url, true);
            }

        }
        private static string GetSubDomain(Uri url)
        {
            string host = url.Host;
            if (host.Split('.').Length > 1)
            {
                int index = host.IndexOf(".");
                return host.Substring(0, index);
            }

            return null;
        }
        void RegisterRoutes(RouteCollection routes)
        { 
            routes.MapPageRoute(
                "addproduct-Rout",
                "adminpanel/addproduct/{type}",
                "~/adminpanel/addproduct.aspx"
                );
            routes.MapPageRoute(
                "login-Rout",
                "login",
                "~/login.aspx"
                );
            routes.MapPageRoute(
                "signup-Rout",
                "signup",
                "~/signup.aspx"
                );
            routes.MapPageRoute(
                "signup-param-Rout",
                "signup/{type}",
                "~/signup.aspx"
                );
            routes.MapPageRoute(
                "forgotpass-Rout",
                "forgotpass",
                "~/forgotpass.aspx"
                );



            routes.MapPageRoute(
                "findlaptop1-Rout",
                "findlaptop",
                "~/findlaptop.aspx"
                );
            routes.MapPageRoute(
                "bulletin-Rout",
                "member/bulletin/{id}",
                "~/member/bulletin.aspx"
                );
            routes.MapPageRoute(
                "upgrade-Rout",
                "member/upgrade",
                "~/member/upgrade.aspx"
                );
            routes.MapPageRoute(
                "downloads-Rout",
                "member/downloads",
                "~/member/downloads.aspx"
                );
            routes.MapPageRoute(
                "upgradepaydone-Rout",
                "member/upgrade/paydone/{reqid}",
                "~/member/paydone.aspx"
                );
            routes.MapPageRoute(
                "vieworder-Rout",
                "member/vieworder/{id}",
                "~/member/vieworder.aspx"
                );
            routes.MapPageRoute(
                "changepic-Rout",
                "member/changepic",
                "~/member/changepic.aspx"
                );
            routes.MapPageRoute(
                "editprofile-Rout",
                "member/editprofile",
                "~/member/editprofile.aspx"
                );
            routes.MapPageRoute(
                "orders-Rout",
                "member/orders",
                "~/member/orders.aspx"
                );
            routes.MapPageRoute(
                "changepass-Rout",
                "member/changepass",
                "~/member/changepass.aspx"
                );
            routes.MapPageRoute(
                "memberlogout-Rout",
                "member/logout",
                "~/member/logout.aspx"
                );
            routes.MapPageRoute(
                "article-Rout",
                "article/{id}/{title}",
                "~/article.aspx"
                );
            routes.MapPageRoute(
                "articlelist-Rout",
                "articlelist/{id}/{title}",
                "~/articlelist.aspx"
                );
            routes.MapPageRoute(
                "file-Rout",
                "shop/file/{id}/{title}",
                "~/shop/file.aspx"
                ); 
            routes.MapPageRoute(
                "paydone-Rout",
                "paydone/{reqid}",
                "~/paydone.aspx"
                );
            routes.MapPageRoute(
                "view1-Rout",
                "view/{id}/{title}",    
                "~/view.aspx"
                );

            routes.MapPageRoute(
                "product2-Rout",
                "product/{id}/{title}/{ref}",
                "~/product.aspx"
                );
            routes.MapPageRoute(
                "cat-Rout",
                "cat/{id}/{title}",
                "~/cat.aspx"
                ); 
        }


        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);

        }


        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            try
            {
                HttpContext currentContext = HttpContext.Current;
                string userId = "0";
                string url = currentContext.Request.Url.PathAndQuery.ToString();
                string ipAddress = currentContext.Request.UserHostAddress;
                string browser = currentContext.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version;

                string msg = "none", reff = "none";
                userId = ((AKUserClass)(HttpContext.Current.Session["User"])).THisUserID.ToString();
                if (currentContext.Request.QueryString["er"] != string.Empty)
                {
                    try
                    {
                        msg = currentContext.Request.QueryString["er"].Replace("%20", " ");
                    }
                    catch { }
                }

                try
                {
                    reff = currentContext.Request.UrlReferrer.ToString();
                }
                catch { reff = "none"; }



                StreamWriter tmp = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/errorLog.txt"), true);


                tmp.WriteLine("Url: http://villanargol.ir" + url);
                tmp.WriteLine("Refferer: " + reff);
                tmp.WriteLine("Time: " + DateTime.Now);
                tmp.WriteLine("UserID: " + userId);
                tmp.WriteLine("UserIP: " + CommonFunctions.GetUser_IP());


                tmp.WriteLine("LogText: " + Server.GetLastError().InnerException + "\r\n\r\n" + Server.GetLastError().Message);
                tmp.WriteLine("Browser: " + browser);
                tmp.WriteLine("-----------------------------------------------\r\n");
                tmp.WriteLine();
                tmp.Close();


            }
            catch { }


        }


        void Session_Start(object sender, EventArgs e)
        {
            Session.Add("User", "NULL");

            AKUserClass UserClass = new AKUserClass();
            Session["User"] = UserClass;
            ((AKUserClass)(Session["User"])).THisUserID = 0;
            ((AKUserClass)(Session["User"])).FullNameOfUser = "0";
            ((AKUserClass)(Session["User"])).EmailOfUser = "0";
            ((AKUserClass)(Session["User"])).UserMobileNum = "";
            ((AKUserClass)(Session["User"])).PayReqIDs = "0";
            ((AKUserClass)(Session["User"])).Pic = "avatar-m1.jpg";



            Session.Add("Basket", "NULL");
            System.Collections.ArrayList Array = new System.Collections.ArrayList();
            Session["Basket"] = Array;

            Session.Timeout = 40;
             
            if (!Request.Browser.Crawler)
            {
                Thread view = new Thread(delegate ()
                {
                    CommonFunctions.AddViewCount();
                });

                view.IsBackground = true;
                view.Start();

            }
        }

        void Session_End(object sender, EventArgs e)
        { 
            Session.Contents.RemoveAll();
            Session.Abandon();
        }

    }
}
