using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.member
{
    public partial class bank : System.Web.UI.Page
    {
        public string currentmoney;
        protected void Page_Load(object sender, EventArgs e)
        {
            DataAccessDataContext _data = new DataAccessDataContext();
            CMS.member.membermaster MasterPage = (CMS.member.membermaster)Page.Master;
            MasterPage.Check_User();


            if (!IsPostBack)
            {
                usersdata tmp = _data.usersdatas.Single(a => a.ID == ((AKUserClass)(Session["User"])).THisUserID);

                var userMoney = CommonFunctions.GetUserCurrentBalance(tmp.ID);
                if (!IsPostBack)
                    currentmoney = CommonFunctions.SetCama(userMoney.ToString());

                if (userMoney < 50000)
                {
                    Panel1.Visible = false;
                    ltr_nomoney.Text = "<div class='alert alert-warning'>موجودی شما جهت برداشت  کافی نیست.</div>";

                }
                else
                {
                    if (string.IsNullOrEmpty(tmp.BankHesab) || string.IsNullOrEmpty(tmp.BankOwnername) || string.IsNullOrEmpty(tmp.BankKart))
                        ltr_nomoney.Text = "<div class='alert alert-warning'>قبل از درخواست برداشت اطلاعات حساب بانکی خود را وارد کنید. <a href='/member/editbankinfo.aspx'>اینجا کلیک کنید</a>.</div>";
                    else {
                        int money = (int)userMoney;
                        txtPrice.Text = ((money / 1000) * 1000).ToString();
                    }
                }
                if (string.IsNullOrEmpty(tmp.BankName) ||
                    string.IsNullOrEmpty(tmp.BankKart) ||
                    string.IsNullOrEmpty(tmp.BankOwnername))
                {
                    Panel1.Visible = false;

                }
            }


        }
        static readonly object _object = new object();

        protected void Button1_Click(object sender, EventArgs e)
        {
            lock (_object)
            {

                int reqmoney = 0;
                int.TryParse(txtPrice.Text, out reqmoney);
                if (reqmoney < 50000)
                {
                    ltr_error.Text = "<div class='warning'>مبلغ صحیح وارد کنید. میزان برداشتی باید از 50 هزار تومان بیشتر بوده و کمتر یا مساوی موجودیتان باشد.</div>";
                    ltr_error.Visible = true;
                    return;
                }
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    usersdata tmpuser = _data.usersdatas.Single(a => a.ID == ((AKUserClass)(Session["User"])).THisUserID);
                    var userMoney = CommonFunctions.GetUserCurrentBalance(tmpuser.ID);

                    if (reqmoney > userMoney)
                    {
                        ltr_error.Text = "<div class='warning'>موجودی شما کافی نیست.</div>";
                        ltr_error.Visible = true;
                        return;
                    }

                    PayRequest tmp = new PayRequest();
                    tmp.DoneTime = DateTime.Now;
                    tmp.IsDone = false;
                    tmp.IsRead = false;
                    tmp.Mablagh = reqmoney;
                    tmp.NotDoneMsg = "";
                    tmp.RegDate = DateTime.Now;
                    tmp.LeftOver = userMoney - reqmoney;
                    tmp.ResidNum = "";
                    tmp.ResitTime = "";
                    tmp.UserID = ((AKUserClass)(Session["User"])).THisUserID;
                    tmp.IsPaid = false;
                    _data.PayRequests.InsertOnSubmit(tmp);
                    _data.SubmitChanges();

                    
                   var transactionSucceed= CommonFunctions.AddTransaction($"درخواست برداشت وجه شماره {tmp.ID}", CreditTransactionType.Minus, tmpuser.ID, reqmoney, CommonFunctions.CreditEneity.PayRequest, tmp.ID);
                    if (transactionSucceed)
                    {

                        ltr_error.Text = "<div class='alert alert-success'>درخواست شما ارسال شده است. پس از واریز به شما اطلاع داده می شود. برای مشاهده وضعیت درخواست خود <a href='paystat.aspx'>به لیست برداشت ها بروید</a>.</div>";
                        ltr_error.Visible = true;
                        currentmoney = CommonFunctions.SetCama(userMoney - reqmoney).ToString();
                    }
                    else
                    {
                        ltr_error.Text = "<div class='alert alert-warning'>خطا در درخواست. لطفا بعدا امتحان کنید.</div>";
                        ltr_error.Visible = true;
                        _data.PayRequests.DeleteOnSubmit(tmp);
                        _data.SubmitChanges();
                    }
                }
            }
        }
    }
}