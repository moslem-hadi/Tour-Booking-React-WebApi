using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.AdminPanel
{
    public partial class settingVal : System.Web.UI.Page
    {

        public List<settingValue> list;
        protected void Page_Load(object sender, EventArgs e)
        {
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                list = _data.settingValues.ToList();
            }
        }
    }
}