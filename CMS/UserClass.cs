using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace CMS
{
    public class AKUserClass
    {
        public int THisUserID
        {
            get;
            set;
        }
        public string FullNameOfUser
        {
            get;
            set;
        } 
        public string EmailOfUser
        {
            get;
            set;
        }
        public string Pic
        {
            get;
            set;
        }
        public string PayReqIDs
        {
            get;
            set;
        }
        public string UserMobileNum
        {
            get;
            set;
        }
        public bool IsUserManager
        {
            get;
            set;
        }

        public bool IsMiniAdmin
        {
            get;
            set;
        }
       

        public AKUserClass()
        {

        }
    }
}
