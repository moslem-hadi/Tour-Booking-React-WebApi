using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class paydetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemini MasterPage = (CMS.Manage.managemini)Page.Master;
            MasterPage.Check_User();

            int payid = 0;
            int.TryParse(Request.QueryString["id"], out payid);
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                int cnt = _data.Payments.Where(a => a.ID == payid ).Count();
                if (cnt != 1)
                    Response.Redirect("/", true);
                else
                {
                    Payment tmp = _data.Payments.Single(a => a.ID == payid);
                    //ltrBuyerMail.Text = string.IsNullOrEmpty(tmp.Email) ? "بدون ایمیل" : tmp.Email;
                    ltrBuyerName.Text = tmp.Name;
                    ltrBank.Text = tmp.SaleReferenceId;
                    ltrstatCode.Text = tmp.Status.ToString() + (!string.IsNullOrEmpty(tmp.BankReturnStat) ? "  -  کد بانک : " +tmp.BankReturnStat: "");
                    ltrcode.Text = tmp.EntityID.ToString();
                    ltrDate.Text = CommonFunctions.String2date(tmp.RegisterDate, 2, "D") + " ساعت " + CommonFunctions.String2date(tmp.RegisterDate, 2, "H");

                   // ltrtype.Text = tmp.EntityType.ToString()== "Basket" ? "سبد خرید" : "اشتراک";
                    ltrprice.Text = CommonFunctions.SetCama(tmp.Mablagh.ToString());
                    ltrBankName.Text = tmp.Bank;
                    ltrReqID.Text = tmp.ID.ToString(); 
                    ltrStat.Text = (bool)tmp.IsSuccess ? "<span style='color:#0f0'>پرداخت موفق</span>" : "<span style='color:#f01'>پرداخت ناموفق</span>";
                
                    tmp.IsRead = true;
                    _data.SubmitChanges();
                    Title = tmp.Title;
                }

            }
        }
    }
}