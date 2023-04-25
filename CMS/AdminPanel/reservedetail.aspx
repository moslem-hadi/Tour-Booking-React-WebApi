<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/managemaster.master" AutoEventWireup="true" CodeBehind="reservedetail.aspx.cs" Inherits="CMS.AdminPanel.reservedetail" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box">
        <div class="header">
            <h4>جزئیات سفارش</h4>
        </div>
        <div class="content ">
            <div class="pad20">
                <div class="row">
                    <div class="span4">
                        <label class="tlt">کد سفارش: </label>
                        <b><%=order.ID %></b><br />

                        <label class="tlt">تاریخ ثبت : </label>
                        <b><%=CMS.CommonFunctions.ToPersianDateTime(order.RegDate)%></b><br />

                        <label class="tlt">تاریخ رزرو : </label>
                        <b><%=order.ReservedDateFa%></b><br />

                        <label class="tlt">نام مشتری: </label>
                        <b><%=order.FullName %></b><br />

                        <label class="tlt">موبایل مشتری: </label>
                        <b><%=order.Tell %></b><br />

                        <label class="tlt">موبایل سفارش دهنده: </label>
                        <b><%=order.RegistererTell %></b><br />

                        <label class="tlt">تعداد نفرات: </label>
                        <b><%=order.AdultCount %> بزرگسال + <%=order.ChildCount%> کودک</b><br />


                        <label class="tlt">وضعیت پرداخت: </label>
                        <b><%=order.IsPaid? "<span style='color:#0f0'>پرداخت موفق</span>": "<span style='color:#f01'>عدم پرداخت</span>" %></b><br />

                    </div>
                    <div class="span3">


                        <label class="tlt">تاریخ ورود: </label>
                        <b><%=order.InDate %></b><br />

                        <label class="tlt">تاریخ خروج: </label>
                        <b><%=order.OutDate %></b><br />

                        <label class="tlt">نام هتل محل اقامت: </label>
                        <b><%=order.Hotel %></b><br />

                        <label class="tlt">آدرس هتل: </label>
                        <b><%=order.HotelAddress %></b><br />

                        <label class="tlt">وسیله نقلیه: </label>
                        <b><%=order.Vehicle== null ? " - " :
                           order.Vehicle==1 ? "اتوبوس": 
                           order.Vehicle==2? "قطار":"هواپیما"
                        %></b><br />

                        <label class="tlt">شماره: </label>
                        <b><%=order.VehicleNumber %></b><br />

                        <label class="tlt">ساعت حرکت: </label>
                        <b><%=order.DepartureTime %></b><br />

                        <label class="tlt">ساعت رسیدن: </label>
                        <b><%=order.ArriveTime %></b><br />
                    </div>
                </div>




            </div>


            <div class="pad20">
                <h3>محصولات این سفارش:</h3>
            </div>
            <table class="table normal margin-reset" style="width: 100%; border-collapse: collapse; border-bottom: 1px solid #ddd">

                <tbody>

                    <tr>
                        <th style="text-align: center" width="80">شناسه</th>
                        <th>عنوان</th>
                        <th style="text-align: center" width="110">مبلغ</th>
                        <th style="text-align: center" width="80">تعداد</th>
                        <th style="text-align: center" width="130">مبلغ کل</th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptProducts" DataSourceID="sdsProducts">
                        <ItemTemplate>
                            <tr>
                                <td style="text-align: center"><%# Eval("ID") %></td>
                                <td>
                                    <span class="label label-info"><%#CMS.CommonFunctions.ProductTypeName(Eval("TypeValue")) %></span>
                                    <a href="detail.aspx?id=<%# Eval("id") %>">
                                        <%# Eval("Title") %>
                                    </a>

                                </td>
                                <td style="text-align: center"><%# CMS.CommonFunctions.SetCama(Eval("ProductPrice")) %> <small>تومان</small></td>

                                <td style="text-align: center"><%# Eval("count") %></td>
                                <td style="text-align: center"><%# CMS.CommonFunctions.SetCama(int.Parse(Eval("ProductPrice").ToString())*int.Parse(Eval("count").ToString())) %><small>تومان</small></td>
                            </tr>
                        </ItemTemplate>

                    </asp:Repeater>
                    <asp:SqlDataSource runat="server" ID="sdsProducts"
                        SelectCommand="select p.id, p.Title, od.ProductPrice, od.Count,p.TypeValue  from OrderDetail od join Product p on p.id=od.ProductId where od.OrderId=@id" SelectCommandType="Text"
                        ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>">
                        <SelectParameters>
                            <asp:Parameter Name="id" DefaultValue="0" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </tbody>
            </table>

            <div id="divpay" style='<%= rptPayments.Items.Count == 0 ? "display:none": "" %>'>
                <div class="pad20">
                    <h3>پرداخت ها:</h3>
                </div>
                <table class="table normal margin-reset" style="width: 100%; border-collapse: collapse; border-bottom: 1px solid #ddd">

                    <tbody>

                        <tr>
                            <th style="text-align: center" width="80">شناسه</th>
                            <th style="text-align: center" width="80">تاریخ ثبت</th>
                            <th style="text-align: center" width="110">مبلغ</th>
                            <th style="text-align: center" width="80">بانک</th>
                            <th style="text-align: center" width="60">وضعیت</th>
                            <th style="text-align: center">کد وضعیت بانک</th>
                            <th style="text-align: center">رهگیری</th>
                            <th style="text-align: center" width="80">IP</th>
                        </tr>
                        <asp:Repeater runat="server" ID="rptPayments" DataSourceID="sdsPayments">
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align: center"><%# Eval("ID") %></td>
                                    <td style="text-align: center"><%# CMS.CommonFunctions.ToPersianDate(Eval("RegisterDate")) %></td>
                                    <td style="text-align: center"><%# CMS.CommonFunctions.SetCama(Eval("Mablagh")) %> <small>تومان</small></td>
                                    <td style="text-align: center"><%# Eval("Bank") %></td>
                                    <td style="text-align: center">
                                        <%# Eval("IsSuccess").ToString().ToLower() == "true" ? "<span class='label label-success'>موفق</span>" : "<span class='label label-important'>ناموفق</span>"%>    
                                    </td>
                                    <td style="text-align: center"><%# Eval("BankReturnStat") %></td>
                                    <td style="text-align: center"><%# Eval("TrackingCode") %></td>
                                    <td style="text-align: left; direction: ltr"><%# Eval("PayerIP") %></td>
                                </tr>
                            </ItemTemplate>

                        </asp:Repeater>
                        <asp:SqlDataSource runat="server" ID="sdsPayments"
                            SelectCommand="select * from payment where EntityType='Reserve' and EntityID=@id" SelectCommandType="Text"
                            ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>">
                            <SelectParameters>
                                <asp:Parameter Name="id" DefaultValue="0" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </tbody>
                </table>
            </div>



            <div class="pad20">
                <h3 style="margin-bottom: 20px;">عملیات:</h3>

                <label class="tlt">راننده : </label>
                <b>
                    <asp:DropDownList runat="server" ID="ddlDrivers" DataSourceID="sdsDrivers" DataValueField="ID" DataTextField="FullName" AppendDataBoundItems="true" Width="150">
                        <asp:ListItem Value="" Text="بدون راننده"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource runat="server" ID="sdsDrivers"
                        SelectCommand="select ID, FullName from usersdata where UserType=@type and isactive=1 and isbanned=0" SelectCommandType="Text"
                        ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>">
                        <SelectParameters>
                            <asp:Parameter Name="type" DefaultValue="0" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </b>
                <br />
                <label class="tlt">وضعیت سفارش : </label>
                <b>
                    <asp:DropDownList runat="server" ID="ddlOrderStatus" Width="150">
                        <asp:ListItem Value="0" Text="جدید"></asp:ListItem>
                        <asp:ListItem Value="1" Text="در حال بررسی"></asp:ListItem>
                        <asp:ListItem Value="2" Text="انجام شد"></asp:ListItem>
                        <asp:ListItem Value="3" Text="کنسل شده"></asp:ListItem>
                    </asp:DropDownList>

                </b>
                <br />

                <asp:Button Text="ذخیره" ID="btnSave" OnClick="btnSave_Click" CssClass="btn btn-success" runat="server" />
            </div>
        </div>

    </div>
</asp:Content>
