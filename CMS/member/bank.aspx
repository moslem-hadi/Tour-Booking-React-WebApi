<%@ Page Language="C#" MasterPageFile="~/member/membermaster.Master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="bank.aspx.cs" Inherits="CMS.member.bank" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>حساب شما</title>
    <script type="text/javascript" src="/js/tabs.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainFullContent" runat="server">
    <div class="content">
        <div class="page-header">
            <h3>
                <span>حساب شما</span></h3>
        </div>

        <asp:Literal ID="ltr_error" runat="server"></asp:Literal>
      <div class="col-md-7">

                  <div class="panel light-shadow white title-transparent rounded clearfix rounded">
           

                <h3 style="font-size:18px; font-weight:bold; margin-bottom:25px;" >درخواست برداشت از سایت</h3>

                <p>
                    - پس از رسیدن موجودی شما به 10.000 تومان میتوانید درخواست پرداخت نمایید.
                </p>
                <hr />
                <asp:Literal Text="" ID="ltr_nomoney" runat="server" />
                <asp:Panel ID="Panel1" runat="server" DefaultButton="Button1">
                    <p style="color: #FC0505;">
                        - تسویه حساب و واریز وجه بحساب شما بدون کسر هیچگونه هزینه ای میباشد.<br />
                        - واریز وجه بصورت پایا میباشد که ممکن است تا 24ساعت به طول بیانجامد!
                    </p>
                    <label class="title">میزان درخواستی</label>
                    <asp:TextBox ID="txtPrice"
                        onkeypress="return (event.charCode >= 65 && event.charCode <= 90) || (event.charCode >= 97 && event.charCode <= 122) || (event.charCode >= 48 && event.charCode <= 57) || (event.charCode==8 ||  event.charCode==0)"
                        Text="0" runat="server" Width="140"
                        onkeyup="convert(this.value,'persian-price1');this.blur();this.focus()"
                        MaxLength="8" CssClass="ltr req txtprice"></asp:TextBox>

                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text="ارسال درخواست "
                        CssClass="btn btn-success" OnClick="Button1_Click" />
                </asp:Panel>
                <div class="col-md-10 col-md-offset-2">
                    <span id="persian-price1"></span>
                </div>

            </div>
            </div>
                <div class="col-md-5">

                    <div  class="light-shadow dash-stat light-shadow white currmoney rounded">

                        <div class="dash-stat-cont">
                            <span class="dash-stat-sub">موجودی فعلی
                            </span>
                            <span class="dash-stat-main"><%= currentmoney =="0" ?"صفر" : currentmoney+" &nbsp;<small>تومان</small>"  %> </span>
                        </div>
                    </div> 
                </div>
            
            <div class="clear"></div>
        
    </div>

</asp:Content>
