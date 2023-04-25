<%@ Page   Language="C#" MasterPageFile="~/AdminPanel/Managemini.master" AutoEventWireup="true" CodeBehind="paydetail.aspx.cs" Inherits="CMS.Manage.paydetail" %>
 
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box">
        <div class="header">
            <h4>جزئیات پرداخت</h4>
        </div>
        <div class="content pad20">
 <label class="tlt">وضعیت پرداخت: </label> <b>
            <asp:Literal ID="ltrStat" runat="server"></asp:Literal></b><br />
          
    <label class="tlt">کد وضعیت: </label> <b>
            <asp:Literal ID="ltrstatCode" runat="server"></asp:Literal></b><br />
    <label class="tlt">کد رسید پرداخت: </label> <b>
            <asp:Literal ID="ltrReqID" runat="server"></asp:Literal></b><br />

            <label class="tlt">بانک: </label> <b>
            <asp:Literal ID="ltrBankName" runat="server"></asp:Literal></b><br />
    

    <label class="tlt">رسید بانکی: </label> <b>
            <asp:Literal ID="ltrBank" runat="server"></asp:Literal></b><br />
    <label class="tlt">تاریخ: </label> <b>
            <asp:Literal ID="ltrDate" runat="server"></asp:Literal></b><br />
 
        <label class="tlt">
            کد موجودیت:</label>
        <b>
            <asp:Literal ID="ltrcode" runat="server"></asp:Literal></b><br />
        <label class="tlt">
            مبلغ:</label><b><asp:Literal ID="ltrprice" runat="server"></asp:Literal></b> تومان
        <br /> 
    <label class="tlt">نام خریدار: </label> <b>
            <asp:Literal ID="ltrBuyerName" runat="server"></asp:Literal></b><br />
  <label class="tlt">موبایل خریدار: </label> <b>
            <asp:Literal ID="ltrmob" runat="server"></asp:Literal></b><br /> 
 
   


            </div>

            </div>
</asp:Content>
