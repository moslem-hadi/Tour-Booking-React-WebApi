<%@ Page  Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="userincomereport.aspx.cs" Inherits="CMS.Manage.userincomereport" %>

<%@ Register Assembly="CollectionPager" Namespace="SiteUtils" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <title>آمار مالی کاربر</title>

    <script src="/js/openwindow.js" type="text/javascript"></script>
    <script src="../component/datepicker/scripts/jquery.ui.datepicker-cc.all.min.js"></script>
    <link href="../component/datepicker/styles/jquery-ui-1.8.14.css" rel="stylesheet" />


    <script type="text/javascript">
        $(function () {
            // حالت پیشفرض
            $('.datepicker').datepicker();

        });
    </script>
    <style>
        .half {
            min-height: 75px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="box">
        <div class="header">
            <h4>آمار مالی کاربر</h4>
        </div>
        <div class="conten">

            <div class="rowelement" style="padding: 10px 20px 10px 20px;">
                <div class="srch2">
                    <asp:Panel ID="Panel1" CssClass="con" runat="server" DefaultButton="Button1">
                        کد کاربر  &nbsp; 
                     <asp:TextBox ID="TextBox1" runat="server" Width="65"></asp:TextBox>



                        &nbsp;  &nbsp;  &nbsp;  &nbsp; از   &nbsp;  
          <asp:TextBox ID="txtfrom" runat="server" Width="65px" CssClass="ltr datepicker"></asp:TextBox>
                        &nbsp;  &nbsp;    &nbsp;  &nbsp; تا  &nbsp; 
          <asp:TextBox ID="txtto" runat="server" Width="65px" CssClass="ltr datepicker"></asp:TextBox>
                        &nbsp;  &nbsp;  &nbsp;  
                <asp:Button ID="Button1" runat="server" Text="برو" CssClass="btn btn-info" OnClick="Button1_Click" />


                    </asp:Panel>



                    <div class="clear"></div>
                </div>
            </div>


            <hr />
            <div class="rowelement" style="padding: 0px 20px 0 20px">


                <div class="">
                    <span>نام کاربر</span>
                    
                        <h4><asp:Literal ID="ltrname" runat="server"></asp:Literal></h4>
                     
                </div> 

                <div class="clear"></div>
                 

                <label class="tlt">موجودی فعلی: </label>
                <asp:Literal ID="ltraffnow" runat="server"></asp:Literal> تومان
                 
            </div>

             


             



            <h3 style="padding: 20px 10px 10px 0">تراکنش ها</h3>

            <asp:GridView CssClass="table normal margin-reset" ID="GridView2" runat="server"
                AutoGenerateColumns="False" DataKeyNames="ID" EnableModelValidation="True"
                Width="100%" AllowPaging="True" GridLines="None"
                EnableTheming="False" EnableViewState="False" ShowFooter="false">
                <AlternatingRowStyle CssClass="alt" />
                <Columns>
                    <asp:TemplateField FooterText="کد" HeaderText="کد">
                        <ItemTemplate>
                            <%# Eval("ID")%>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField FooterText="عنوان" SortExpression="Title" HeaderText="عنوان">
                        <ItemTemplate>
                            <span style="color: #777; font-size: 11px;">
                                <a
                                    href='<%# Eval("id","edittrnasaction.aspx?id={0}") %>'>
                                    <%# Eval("Description") %></a>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField FooterText="کاربر" HeaderText="کاربر">
                        <ItemTemplate>

                            <a href="userdetail.aspx?id=<%# Eval("userid")%>">
                                <%# Eval("userid")%>
                            </a>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField FooterText="نوع" HeaderText="نوع">
                        <ItemTemplate>

                            <%# Eval("Type").ToString().ToLower() == "true" ? "<span class='label label-success' title='افزایش'><i class='icon-arrow-up icon-white' style='margin:2px'></i></span>" : "<span class='label label-important' title='کاهش'><i class='icon-arrow-down icon-white' style='margin:2px'></i></span>"%>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField FooterText="تاریخ" HeaderText="تاریخ" SortExpression="ViewCount">
                        <ItemTemplate>
                            <%# CMS.CommonFunctions.String2date(Eval("RegDate"), 2, "H")+ " - " + CMS.CommonFunctions.String2date(Eval("RegDate"), 2, "D").Remove(0,2) %>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="110px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField FooterText="مبلغ" HeaderText="مبلغ" SortExpression="Mablagh">
                        <ItemTemplate>
                            <div style='color:<%# Eval("Type").ToString().ToLower()=="true" ? "#468847" :"#b94a48"%>'>
                            <b style="display:inline-block; direction:ltr"><%# CMS.CommonFunctions.SetCama(Eval("Mablagh"))%></b>
                            <span class="small">تومان</span></div>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="140px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle CssClass="footer" />
                <PagerStyle CssClass="pager" />
                <EmptyDataRowStyle CssClass="empty" />
                <EmptyDataTemplate>
                    <p class="empty">
                        بدون رکورد
                    </p>
                </EmptyDataTemplate>
            </asp:GridView>



            <div class="clear"></div>

            <cc1:CollectionPager ID="CollectionPager2" runat="server" BackText="قبلی" ControlCssClass="pagering"
                FirstText="ابتدا" LabelText="صفحه" LastText="آخر" MaxPages="20000" NextText="بعدی"
                PageNumbersDisplay="Numbers" QueryStringKey="page"
                ResultsLocation="None" EnableViewState="False" LabelStyle="" PageSize="10" ResultsStyle=""
                ShowLabel="False" PageNumbersSeparator=" " ShowFirstLast="False">
            </cc1:CollectionPager>
            <div class="clear"></div>




            <h3 style="padding: 20px 10px 10px 0">درخواست های برداشت</h3>
            <asp:GridView CssClass="table normal margin-reset" ID="GridView3"
                runat="server" AutoGenerateColumns="False"
                DataKeyNames="ID" DataSourceID="sdspayreqlist"
                EnableModelValidation="True" Width="100%" OnRowDataBound="GridView1_RowDataBound"
                AllowPaging="True" GridLines="None" PageSize="10" PagerSettings-Position="Bottom"
                EnableTheming="False" EnableViewState="False" ShowFooter="false">
                <AlternatingRowStyle CssClass="alt" />
                <Columns>
                    <asp:TemplateField FooterText="کد درخواست" HeaderText="کد درخواست">

                        <ItemTemplate>
                            <span style="color: #777; font-size: 11px;">
                                <%# Container.DataItemIndex+1 %>)</span> <a onclick="return popup(this,'مشاهده',1,520,460)"
                                    href='<%# Eval("id","requestdetail.aspx?id={0}") %>' target="_blank">
                                    <%# Eval("ID")%></a>
                        </ItemTemplate>

                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Right" />
                    </asp:TemplateField>



                    <asp:TemplateField FooterText="کاربر" HeaderText="کاربر">

                        <ItemTemplate>
                            <span style="color: #777; font-size: 11px;">
                                <a href='<%# Eval("userid","userdetail.aspx?id={0}") %>'>
                                    <%# Eval("FullName")%></a>
                        </ItemTemplate>

                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Right" />
                    </asp:TemplateField>





                    <asp:TemplateField FooterText="تارخ درخواست" HeaderText="تارخ درخواست">
                        <ItemTemplate>
                            <%# CMS.CommonFunctions.String2date(Eval("regdate"), 2, "D") + " ساعت " + CMS.CommonFunctions.String2date(Eval("regdate"), 2, "H")%>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle Width="140px" HorizontalAlign="Right" />
                    </asp:TemplateField>

                    <asp:TemplateField FooterText="رسید" HeaderText="رسید">

                        <ItemTemplate>
                            <%# string.IsNullOrEmpty(Eval("ResidNum").ToString()) ? "هنوز پرداخت نشده است" : Eval("ResidNum")%>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="140px" HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField FooterText="مبلغ" HeaderText="مبلغ"
                        SortExpression="ViewCount">

                        <ItemTemplate>
                            <%# CMS.CommonFunctions.SetCama(Eval("mablagh"))%> <span class="small">تومان</span>
                            <br />
                            <span style="color: #777; font-size: 11px">مانده <%# CMS.CommonFunctions.SetCama(Eval("leftover"))%></span>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField FooterText="وضعیت" HeaderText="وضعیت"
                        SortExpression="Mablagh">

                        <ItemTemplate>
                            <asp:Literal ID="ltrstat" runat="server"></asp:Literal>
                            <%# string.IsNullOrEmpty(Eval("NotDoneMsg").ToString()) ? "" : Eval("NotDoneMsg", "<p style='text-align:right;font-size:11px;margin:3px 0;'>علت: <span style='color:#f01'>{0}</span></p>")%>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                </Columns>
                <FooterStyle CssClass="footer" />
                <PagerStyle CssClass="pager" />
                <EmptyDataRowStyle CssClass="empty" />
                <EmptyDataTemplate>
                    <p class="empty">بدون رکورد</p>
                </EmptyDataTemplate>
            </asp:GridView>




            <asp:SqlDataSource ID="sdspayreqlist" runat="server"
                ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>"
                SelectCommand="payreqlist_report" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="id" QueryStringField="id" Type="Int32" />
                    <asp:QueryStringParameter DefaultValue="" Name="from" QueryStringField="from" Type="DateTime" />
                    <asp:QueryStringParameter Name="to" QueryStringField="to" Type="DateTime" />
                </SelectParameters>

            </asp:SqlDataSource>
        </div>
    </div>

</asp:Content>
