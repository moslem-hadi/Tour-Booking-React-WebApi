<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/managemaster.Master" AutoEventWireup="true" CodeBehind="reservs.aspx.cs" Inherits="CMS.AdminPanel.reservs" %>
<%@ Register Assembly="CollectionPager" Namespace="SiteUtils" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<title>لیست رزروها</title>
  <script>
      function SelectAllCheckboxesGridView1(chk) {
          $('#<%=GridView1.ClientID %>').find("input:checkbox").each(function () {
              if (this != chk) {
                  this.checked = chk.checked;
              }
          });
      }
    </script>


  <script src="../component/datepicker/scripts/jquery.ui.datepicker-cc.all.min.js"></script>
    <link href="../component/datepicker/styles/jquery-ui-1.8.14.css" rel="stylesheet" />

    
    <script type="text/javascript">
        $(function () {
            // حالت پیشفرض
            $('.datepicker').datepicker();
            
        });
    </script>
    
    <script src="/js/openwindow.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
 
<div class="box">
<div class="header">
<h4>لیست رزرو ها</h4>
</div>
<div class="conten">
 
      
  <div class="srch1" style="padding:10px;">
                <asp:Panel ID="Panel1" CssClass="co1n" runat="server" DefaultButton="LinkButton2">
                    
                          <label>کد سفارش</label>&nbsp;&nbsp;
          <asp:TextBox ID="txtKey" runat="server" TextMode="Number" placeholder="کد سفارش" Width="200px" MaxLength="200"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <label>کد کاربر</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtUser" runat="server"  placeholder="" Width="120px" CssClass="textboxAuto" Font-Size="12px" />
               
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <label>وضعیت پرداخت</label>   &nbsp;
                 <asp:DropDownList ID="ddlStat" runat="server" Width="80"  CssClass="slc"> 
                    <asp:ListItem Value="-1">همه</asp:ListItem>
                    <asp:ListItem Value="1">پرداخت شده</asp:ListItem>
                    <asp:ListItem Value="0">پرداخت نشده</asp:ListItem>
              </asp:DropDownList>
                      
         


                    <div class="srch" style="display:inline-block" >
                     &nbsp;&nbsp;&nbsp;&nbsp;
                    <label>مرتب سازی</label>   &nbsp;&nbsp;&nbsp;&nbsp;

              <asp:DropDownList ID="ddlSort" runat="server" Width="70" CssClass="slc">
                  <asp:ListItem Value="ID">تاریخ درج</asp:ListItem>
                  <asp:ListItem Value="totalPrice">قیمت</asp:ListItem>
                  <asp:ListItem Value="UserID">کاربر</asp:ListItem>
              </asp:DropDownList>
                 <asp:DropDownList ID="ddlDir" runat="server" Width="50"  CssClass="slc"> 
                    <asp:ListItem Value="desc">نزولی</asp:ListItem>
                    <asp:ListItem Value="asc">صعودی</asp:ListItem>
              </asp:DropDownList>




                    <asp:LinkButton CssClass="srchbtn" ID="LinkButton2" runat="server" OnClick="LinkButton2_Click"><i class="icon-search"></i></asp:LinkButton>
                        </div>
                </asp:Panel> 
                <div class="clear"></div>
            </div>
            <asp:GridView CssClass="table normal margin-reset" ID="GridView1" runat="server"
                AutoGenerateColumns="False" DataKeyNames="ID" EnableModelValidation="True"
                Width="100%" AllowPaging="True" GridLines="None" 
                EnableTheming="False" EnableViewState="False" ShowFooter="false">
                <AlternatingRowStyle CssClass="alt" />
                <Columns>
                    <asp:TemplateField FooterText="کد سفارش" HeaderText="کد سفارش">
                        <ItemTemplate>
                            <a  href='<%# Eval("id","reservedetail.aspx?id={0}") %>' >
                                     سفارش شماره <%# Eval("id") %></a>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                        
                    </asp:TemplateField>
                     
                    <asp:TemplateField FooterText="تاریخ رزرو"  HeaderText="تاریخ رزرو">
                        <ItemTemplate>
                         
                             <%#Eval("ReservedDateFa") %>
                            
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField FooterText="نام مشتری"  HeaderText="نام مشتری">
                        <ItemTemplate>
                         
                             <%#Eval("FullName") %>
                            
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                    </asp:TemplateField>

                    <asp:TemplateField FooterText="راننده"  HeaderText="راننده">
                        <ItemTemplate>
                         
                             <%#Eval("DriverId")==null ? "---" :"<a href='userdetail.aspx?id="+Eval("DriverId")+"'>"+Eval("DriverName")+"</a>" %>
                            
                        </ItemTemplate>
                        <ItemStyle   HorizontalAlign="Center" />
                    </asp:TemplateField>

                      
                    <asp:TemplateField FooterText="وضعیت" HeaderText="وضعیت">
                        <ItemTemplate>
                            <%#CMS.CommonFunctions.OrderStatusName( Eval("Status"))%>    
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:TemplateField> 
                    <asp:TemplateField FooterText="پرداخت" HeaderText="پرداخت">
                        <ItemTemplate>
                           
                            <%# Eval("IsPaid").ToString().ToLower() == "true" ? "<span class='label label-success'><i class='icon-ok-sign icon-white' style='margin:2px'></i></span>" : "<span class='label label-important'><i class='icon-remove-sign icon-white' style='margin:2px'></i></span>"%>    
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField> 
                    <asp:TemplateField FooterText="مبلغ" HeaderText="مبلغ" SortExpression="Mablagh">
                        <ItemTemplate>
                            <%# CMS.CommonFunctions.SetCama(Eval("TotalPrice"))%>
                            <small class="small">تومان</small>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle CssClass="footer" />
                <PagerStyle CssClass="pager" />
                <EmptyDataRowStyle CssClass="empty" />
                <EmptyDataTemplate>
                    <p class="empty">
                        رکوردی وجود ندارد.</p>
                </EmptyDataTemplate>
            </asp:GridView>
     
     
            
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


    
    <link rel="stylesheet" href="css/jquery-ui.css">
    <script src="js/jquery-ui.js"></script>  

    <script language="javascript" type="text/javascript">
        $(function () {

            $('#<%=txtUser.ClientID%>').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "paylist.aspx/getUser",
                        data: "{'pre' :'" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {

                                return {
                                    FullName: item.FullName,
                                    ID: item.ID,
                                    json: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                },
                focus: function (event, ui) {
                    // this is for when focus of autocomplete item 
                    $('#<%=txtUser.ClientID%>').val(ui.item.ID);
                    return false;
                },
                select: function (event, ui) {
                    // this is for when select autocomplete item
                    $('#<%=txtUser.ClientID%>').val(ui.item.ID);
                    return false;
                }
            }).data("ui-autocomplete")._renderItem = function (ul, item) {
                // here return item for autocomplete text box, Here is the place 
                // where we can modify data as we want to show as autocomplete item
                return $("<li>")
                .append("<a  href='#' >" + item.FullName + "</a>").appendTo(ul);
            };



        });
    </script>

</asp:Content>
