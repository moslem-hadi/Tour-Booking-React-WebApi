<%@ Page Language="C#" MasterPageFile="~/member/membermaster.Master" AutoEventWireup="true"
    CodeBehind="default.aspx.cs" Inherits="CMS.member._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>پنل کاربری - داشبورد</title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainFullContent" runat="server">
     
    <div class="page-header">
        <div class="title">
            میز کار
                        <span>آمار و اطلاعات اولیه</span>
        </div>

    </div>

    <div class="content">

        <div class="row">
            <div class="col-md-12">
                <asp:Literal ID="ltrshoperr" runat="server"></asp:Literal>
                <asp:Literal ID="ltrmsg" runat="server"></asp:Literal>

                   <div class="col-md-4">
                    <a href="http://parhantransfer.ir/transfer" class="dash-stat light-shadow blue rounded">
                        <div class="dash-stat-cont">
                            <span class="dash-stat-sub bg">
                                رزرو ترانسفر </span>
                                <img src="images/icons/Transfer.svg"  class="icon pull-right">
                       
                        </div>
                    </a>
                </div>

                <div class="col-md-4">
                    <a href="http://parhantransfer.ir/gasht" class="dash-stat light-shadow teal rounded">
                        <div class="dash-stat-cont">
                            <span class="dash-stat-sub bg">رزرو گشت </span>
                                <img src="images/icons/Gasht.svg"  class="icon pull-right">
                               
                        </div>
                    </a>
                </div>

                <div class="col-md-4">
                    <a href="http://parhantransfer.ir/tour" class="dash-stat light-shadow red rounded">
                        <div class="dash-stat-cont">
                            <span class="dash-stat-sub bg">رزرو تور </span>
                                <img src="images/icons/Tour.svg"  class="icon pull-right">
                                
                        </div>
                    </a>
                </div>

            </div>



            <div class="mg-top-20">

                <div class="col-md-5">

                    <a href="bank.aspx" class="light-shadow dash-stat light-shadow white currmoney rounded">

                        <div class="dash-stat-cont">
                            <span class="dash-stat-sub">موجودی فعلی
                                
                            </span>

                            <span class="dash-stat-main"><%= currentmoney =="0" ?"صفر" : currentmoney+" &nbsp;<small>تومان</small>"  %> </span>
                            <span class="dash-stat-more">درخواست برداشت <i class="fa fa-arrow-left"></i></span>
                        </div>
                    </a> 
                </div>



                <div class="col-md-7">
                     
                    <%if (userType != CMS.UserTypes.Driver)
                        { %>
                    <div id="divOrders" class="panel light-shadow white title-transparent rounded" 
                        data-toggle="false" data-expand="false">
                          <div class="panel-title">
                        آخرین سفارشات 
                        <small class="text-muted btn btn-link" style="color: #055bad;  font-size: 13px; font-weight: normal;float:left">
                            <a href="orders.aspx">مشاهده همه سفارشات</a></small>
                    </div>


                        <asp:GridView CssClass="table" ID="grvOrders"
                            runat="server" AutoGenerateColumns="False"
                            DataKeyNames="ID" DataSourceID="sdsLatestOrders"
                            EnableModelValidation="True" Width="100%"
                            AllowPaging="False" GridLines="None" 
                            EnableTheming="False" EnableViewState="False"
                            ShowFooter="False">
                            <AlternatingRowStyle CssClass="alt" />
                            <Columns>
                                <asp:TemplateField FooterText="شناسه سفارش"   HeaderText="شناسه سفارش">

                                    <ItemTemplate> 
                                            <%# Eval("ID") %> 
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <FooterStyle HorizontalAlign="Center" />
                                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterText="نام مشتری"   HeaderText="نام مشتری">

                                    <ItemTemplate> 
                                            <%# Eval("FullName") %> 
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField FooterText="تاریخ ثبت" HeaderText="تاریخ ثبت">

                                    <ItemTemplate>

                                        <%# CMS.CommonFunctions.String2date(Eval("RegDate"), 2, "D") %>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField FooterText="تاریخ رزرو" HeaderText="تاریخ رزرو">

                                    <ItemTemplate>
                                            <%# Eval("ReservedDateFa") %> 
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                                </asp:TemplateField>



                            </Columns>
                            <FooterStyle CssClass="footer" />
                            <PagerSettings Position="TopAndBottom" />
                            <PagerStyle CssClass="pager" />
                            <EmptyDataRowStyle CssClass="empty" />
                            <EmptyDataTemplate>

                               <p class="empty">
                                   سفارشی ثبت نشده است
                               </p>
                            </EmptyDataTemplate>
                        </asp:GridView>

                        <asp:SqlDataSource ID="sdsLatestOrders" runat="server"
                            ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>"
                            SelectCommand="GetUserOrders" SelectCommandType="StoredProcedure">
                            <SelectParameters >
                                <asp:Parameter  Name="userId" Type="Int32" DefaultValue="0" />
                                <asp:Parameter  Name="count" Type="Int32" DefaultValue="7" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                         
                    </div>

                    <%} %>

                    
                    <%if (userType == CMS.UserTypes.Driver)
                        { %>
                       <div id="divServices" class="panel light-shadow white title-transparent rounded" 
                        data-toggle="false" data-expand="false">
                          <div class="panel-title">
                        آخرین سرویس‌ها 
                        <small class="text-muted btn btn-link" style="color: #055bad;  font-size: 13px; font-weight: normal;float:left">
                            مشاهده همه سرویس‌ها</small>
                    </div>


                        <asp:GridView CssClass="table" ID="GridView1"
                            runat="server" AutoGenerateColumns="False"
                            DataKeyNames="ID" DataSourceID="sdsGetDriverUserServices"
                            EnableModelValidation="True" Width="100%"
                            AllowPaging="False" GridLines="None" 
                            EnableTheming="False" EnableViewState="False"
                            ShowFooter="False">
                            <AlternatingRowStyle CssClass="alt" />
                            <Columns>
                                <asp:TemplateField FooterText="شناسه سفارش"   HeaderText="شناسه سفارش">

                                    <ItemTemplate> 
                                            <%# Eval("ID") %> 
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <FooterStyle HorizontalAlign="Center" />
                                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterText="نام مشتری"   HeaderText="نام مشتری">

                                    <ItemTemplate> 
                                            <%# Eval("FullName") %> 
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterText="تماس مشتری"   HeaderText="تماس مشتری">

                                    <ItemTemplate> 
                                            <%# Eval("tell") %> 
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField FooterText="تاریخ ثبت" HeaderText="تاریخ ثبت">

                                    <ItemTemplate>

                                        <%# CMS.CommonFunctions.String2date(Eval("RegDate"), 2, "D") %>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField FooterText="تاریخ رزرو" HeaderText="تاریخ رزرو">

                                    <ItemTemplate>
                                            <%# Eval("ReservedDateFa") %> 
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                                </asp:TemplateField>



                            </Columns>
                            <FooterStyle CssClass="footer" />
                            <PagerSettings Position="TopAndBottom" />
                            <PagerStyle CssClass="pager" />
                            <EmptyDataRowStyle CssClass="empty" />
                            <EmptyDataTemplate>

                               <p class="empty">
                                   سرویسی ثبت نشده است
                               </p>
                            </EmptyDataTemplate>
                        </asp:GridView>

                        <asp:SqlDataSource ID="sdsGetDriverUserServices" runat="server"
                            ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>"
                            SelectCommand="GetDriverUserServices" SelectCommandType="StoredProcedure">
                            <SelectParameters >
                                <asp:Parameter  Name="userId" Type="Int32" DefaultValue="0" />
                                <asp:Parameter  Name="count" Type="Int32" DefaultValue="7" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                         
                    </div>

                    
                    <%} %>

                    <div id="bullist" runat="server" class="panel light-shadow white title-transparent rounded" data-title="اطلاعیه ها" data-toggle="false" data-expand="false">
                        <table class="table">
                            <asp:SqlDataSource ID="ReminderListMain_manage" runat="server" ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>"
                                SelectCommand="ReminderListMain_manage" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                            <asp:Repeater ID="rptReminders" DataSourceID="ReminderListMain_manage" runat="server">
                                <ItemTemplate>
                                    <tr class='<%# Eval("priority") %>'>
                                        <td>
                                            <a href='<%# Eval("id","viewbulletin/{0}") %>' data-toggle="modal" data-target="#news"><%# Eval("title") %> </a>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>


                        </table>
                    </div>


                </div>

            </div>







        </div>

    </div>


    <div class="modal fade" id="news" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="text-center">
                        <img src="/images/loading.gif" /></div>
                </div>
            </div>
            <script>
                $('#news').on('hidden.bs.modal', function () {
                    $(this).removeData('bs.modal');
                });
            </script>
        </div>
    </div>

</asp:Content>
