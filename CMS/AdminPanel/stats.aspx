<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="stats.aspx.cs" Inherits="CMS.Manage.stats" %>

<%@ Register Assembly="CollectionPager" Namespace="SiteUtils" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <title>آمار بازدید، درآمد، فروش و کاربران</title>
    <script src="../component/datepicker/scripts/jquery.ui.datepicker-cc.all.min.js"></script>
    <link href="../component/datepicker/styles/jquery-ui-1.8.14.css" rel="stylesheet" />

    
    <script type="text/javascript">
        $(function () {
            // حالت پیشفرض
            $('.datepicker').datepicker();
            
        });
    </script>
    
    <script type="text/javascript">

        function OpenPopup() {
            window.open("filestat.aspx", "List", "scrollbars=no,resizable=no,width=600,height=600");
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box">
        <div class="header">
            <h4>آمار سایت</h4>
        </div>
        <div class="content">


            <div class="rowelement" style="padding: 10px 20px 10px 4px; ">
      <div class="srch2">
                <label class="tlt">مرتب سازی بر اساس</label>
                <asp:DropDownList ID="DropDownList1" runat="server" Width="70">
                    <asp:ListItem Value="ID">تاریخ</asp:ListItem>
                    <asp:ListItem Value="ViewCount">تعداد بازدید</asp:ListItem> 
                    <asp:ListItem Value="SalesPrice">میزان تراکنش بانکی</asp:ListItem> 
                </asp:DropDownList>
                <asp:DropDownList ID="DropDownList2" runat="server" Width="40">
                    <asp:ListItem Value="desc">نزولی</asp:ListItem>
                    <asp:ListItem Value="asc">صعودی</asp:ListItem>
                </asp:DropDownList>
           از 
          <asp:TextBox ID="txtfrom" runat="server" Width="65px" CssClass="ltr datepicker"></asp:TextBox>
          تا
          <asp:TextBox ID="txtto" runat="server" Width="65px" CssClass="ltr datepicker"></asp:TextBox>
         
                <asp:Button ID="Button1" runat="server" Text="برو"   CssClass="btn btn-info" OnClick="Button1_Click"  />
                   
                </div>
            </div>

            
            <div class="rowelement" style="padding: 10px 20px 0 10px">
                <div class="half" style="min-height:inherit">
                    
                <label class="tlt">مجموع بازدید: </label> <asp:Literal ID="ltrviewcount" runat="server"></asp:Literal> بازدید<br />
                <label class="tlt">تعداد فروش: </label> <asp:Literal ID="ltrsalecount" runat="server"></asp:Literal> 
                </div>
                 <div class="half" style="min-height:inherit">
                <label class="tlt">تعداد  محصولات: </label> <asp:Literal ID="ltrupcount" runat="server"></asp:Literal> فایل<br /> 
                <label class="tlt">تراکنش بانکی: </label> <asp:Literal ID="ltrallsale" runat="server"></asp:Literal> تومان 
                </div>  </div>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <div class="stat">
                        <div class="day">
                            <%#CMS.CommonFunctions.String2date(Eval("Day"), 2, "D")%><br />
                            <%# ((DateTime)Eval("Day")).ToShortDateString()%>
                        </div>
                        <p class="sp1">تعداد بازدید<br />
                            <%#CMS.CommonFunctions.SetCama(Eval("ViewCount"))%></p> 
                        <div class="sp2">تراکنش بانکی<br />
                            <%#CMS.CommonFunctions.SetCama(Eval("SalesPrice"))%></div>  
                        <p class="sp1">تعداد فروش<br />
                            <%#CMS.CommonFunctions.SetCama(Eval("SaleCount"))%></p> 
                        <p class="sp1">تعداد عضویت<br />
                            <%#CMS.CommonFunctions.SetCama(Eval("RegisterCount"))%></p>
                        <div class="clear"></div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>



            <div class="clear"></div>

            <cc1:CollectionPager ID="CollectionPager1" runat="server" BackText="قبلی" ControlCssClass="pagering"
                FirstText="ابتدا" LabelText="صفحه" LastText="آخر" MaxPages="200" NextText="بعدی"
                PageNumbersDisplay="Numbers" QueryStringKey="page"
                ResultsLocation="None" EnableViewState="False" LabelStyle="" PageSize="10" ResultsStyle=""
                ShowLabel="False" PageNumbersSeparator=" " ShowFirstLast="False">
            </cc1:CollectionPager>
            <div class="clear"></div>



        </div>
    </div>
</asp:Content>
