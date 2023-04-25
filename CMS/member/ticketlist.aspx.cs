using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.member
{
    public partial class ticketlist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.member.membermaster MasterPage = (CMS.member.membermaster)Page.Master;
            MasterPage.Check_User();

            DataAccessDataContext _data = new DataAccessDataContext(); 
            sdsticketList.SelectParameters["UserID"].DefaultValue = ((AKUserClass)(Session["User"])).THisUserID.ToString();

        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool isnew = (bool)DataBinder.Eval(e.Row.DataItem, "IsRead");
                if (!isnew)
                {
                    e.Row.BackColor = System.Drawing.Color.FromName("#F5FFAD"); // is a "new" row
                    e.Row.Style.Add("background-image", "none");
                }
            }
        }
    }
}