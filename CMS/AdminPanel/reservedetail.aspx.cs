using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.AdminPanel
{
    public partial class reservedetail : System.Web.UI.Page
    {
        public Order order;
        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();

            int reserveID = 0;
            int.TryParse(Request.QueryString["id"], out reserveID);
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                order = _data.Orders.FirstOrDefault(a => a.ID == reserveID);
                sdsPayments.SelectParameters["id"].DefaultValue = order.ID.ToString();
                sdsProducts.SelectParameters["id"].DefaultValue = order.ID.ToString();
                sdsDrivers.SelectParameters["type"].DefaultValue = ((int)UserTypes.Driver).ToString();

                if (order == null)
                    Response.Redirect("/", true);


                Title = "سفارش شماره " + order.ID;

                if (!IsPostBack)
                {
                    if (order.DriverId != null )
                        ddlDrivers.SelectedValue = order.DriverId.ToString();
                    ddlOrderStatus.SelectedValue = order.Status.ToString();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int reserveID = 0;
            int.TryParse(Request.QueryString["id"], out reserveID);
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                var editorder = _data.Orders.FirstOrDefault(a => a.ID == reserveID);
                editorder.DriverId = int.Parse(ddlDrivers.SelectedValue);
                editorder.Status = int.Parse(ddlOrderStatus.SelectedValue);
                _data.SubmitChanges();
            }
        }
    }
}