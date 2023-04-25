<%@ Page  Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="userdetail.aspx.cs" Inherits="CMS.Manage.userdetail" %>

<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box">
        <div class="header">
            <h4>اطلاعات و آمار کاربر</h4>
        </div>
        <div class="content pad">


            <div class="m-b-20 prfileInfo">
                <div class="img"> 
                    <h4>عملیات</h4>

                    <div class="content list">
                        <a href='<%="edituser.aspx?id="+UserID%>'><i class="icon-edit"></i>ویرایش</a>
                        <%--<a href='<%="paylist.aspx?key=&type=-1&from=&to=&stat=-1&sort=ID&dir=desc&user=314"+UserID%>'><i class="icon-cog"></i>لیست پرداخت ها</a>--%>
                        <a href='<%="smssender.aspx?to="+mobile%>'><i class="icon-envelope"></i>ارسال پیامک</a>
                        <a href='<%="addtransatction.aspx?id="+UserID%>'><i class="icon-qrcode"></i>افزودن تراکنش</a>
                          <a href='<%="userincomereport.aspx?id="+UserID+"&from=12/01/2019%2012:00:00%20AM&to="+DateTime.Now%>'><i class="icon-magnet"></i> مشاهده آمار مالی</a>   

                        <asp:LinkButton ID="lnkDel" ForeColor="Red" OnClick="lnkDel_Click" runat="server" OnClientClick="return confirm('حذف شود؟ غیرقابل بازگشت است');"><i class="icon-remove"></i> حذف کاربر</asp:LinkButton>
                    </div>
                </div>


                <ul class="userdetail">
                    <li><i class="icon-user"></i>نام اصلی: <b><%=fullname%></b></li>
                    <li><i class="icon-barcode"></i>کد: <b><%=UserID%></b></li>
                    <li><i class="icon-envelope"></i>ایمیل: <b><%=email%></b></li>
                    <li><i class="icon-calendar"></i>عضویت: <b><%=regdate%></b></li>
                    <li><i class="icon-asterisk"></i>موبایل: <b><%=mobile%></b></li>
                    <li class="sp"></li> 
                    <li class="h"><i class="icon-chevron-left"></i>IP عضویت: <b class="ltrsp"><%=IP%></b></li>
                    <li class="h"><i class="icon-chevron-left"></i>آخرین IP ورود: <b><%=lastip%></b></li>
                    <li class="h"><i class="icon-chevron-left"></i>آخرین ورود: <b><%=lastlogin%></b></li> 
                    <li class="h"><i class="icon-chevron-left"></i>نام بانک: <b><%=BankName%></b></li>
                    <li class="h"><i class="icon-chevron-left"></i>شماره حساب: <b class="ltrsp"><%=BankHesab%></b></li>
                    <li class="h"><i class="icon-chevron-left"></i>صاحب حساب: <b><%=BankOwnername%></b></li>
                    <li class="h"><i class="icon-chevron-left"></i>شبا: <b class="ltrsp"><%=BankSheba%></b></li>
                    <li class="h"><i class="icon-chevron-left"></i>شماره کارت: <b class="ltrsp"><%=BankKart%></b></li>
                        

                    <li class="h"><i class="icon-chevron-left"></i>کد ملی: <b class="ltrsp"><%=MeliCode%></b></li>


                </ul>
            </div>

            <div class="clear"></div>


            <hr />
            <div class="half" style="width: 39.5%">
                 

<%--                <div class="box">
                    <div class="header">
                        <h4>سفارش های کاربر</h4>
                    </div>

                    <div class="content list">
                        <asp:ListView ID="ListView1" runat="server" DataKeyField="ID"
                            DataSourceID="sdsorder">
                            <ItemTemplate>
                                <i class="icon-chevron-left"></i><a href='<%# Eval("ID","order.aspx?id={0}") %>'>سفارش شماره <%# Eval("id") %></a> | <%# CMS.CommonFunctions.SetCama(Eval("TotalPrice")) %> تومان
                 <br />
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <p class="empty">بدون سفارش</p>
                            </EmptyDataTemplate>
                        </asp:ListView>


                        <asp:SqlDataSource ID="sdsorder" runat="server"
                            ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>"
                            SelectCommand="User_OrderList" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="userID" QueryStringField="id" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>

                </div>--%>


            </div>
            <div class="half" style="width: 59.5%">


                <%--<div class="box">
                    <div class="header">
                        <h4>سابقه خرید اشتراک</h4>
                    </div>

                    <div class="content list">
                        <asp:ListView ID="lstvUphistory" runat="server" DataKeyField="ID"
                            DataSourceID="sdsGetUserUpgradeHistory">
                            <ItemTemplate>
                                <b><%# Eval("title") %></b>
                                &nbsp;&nbsp;/&nbsp;&nbsp;  <%# Eval("selecteddays") %>  روز 
                     &nbsp;&nbsp;/&nbsp;&nbsp; پرداخت: <a href='<%# Eval("paymentid","paydetail.aspx?id={0}") %>'><%# Eval("paymentid") %></a>
                                &nbsp;&nbsp;/&nbsp;&nbsp; <%# CMS.CommonFunctions.SetCama(Eval("price")) %> تومان
                     &nbsp;&nbsp;/&nbsp;&nbsp;
                     تاریخ : <%# Eval("regdate")%>
                                <hr style="margin: 10px 0;">
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <p class="empty">بدون خرید</p>
                            </EmptyDataTemplate>
                        </asp:ListView>


                        <asp:SqlDataSource ID="sdsGetUserUpgradeHistory" runat="server"
                            ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>"
                            SelectCommand="GetUserUpgradeHistory" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="userID" QueryStringField="id" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>

                </div>--%>


                 


                 

                 
            </div>
            <div class="clear"></div>
        </div>
    </div>
</asp:Content>
