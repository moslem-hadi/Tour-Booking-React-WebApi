<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="slider.aspx.cs" Inherits="CMS.Manage.slider" %>

<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<title>اسلایدر تصاویر</title>
<link href="../component/datepicker/styles/jquery-ui-1.8.14.css" rel="stylesheet"
                            type="text/css" />
                            
    
    <script src="../component/datepicker/scripts/jquery.ui.datepicker-cc.all.min.js"
        type="text/javascript"></script>

      <script type="text/javascript">
          $(function () {
              // حالت پیشفرض
              $('#txtStartDate').datepicker();
              $('#txtEndDate').datepicker();

          });
    </script>
    <script>
        function ShowDestinationOthers(e, k) {
            if (e == '2')
                document.getElementById(k).style.display = 'inline'
            else
                document.getElementById(k).style.display = 'none'
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box righted">
        <div class="header">
            <h4>
                افزودن تصویر جدید</h4>
        </div>
        <div class="content">
            
            <fieldset>
            
                <div class="rowelement withpadding">
                <cc1:MessageBox ID="MessageBox1" runat="server" MessageType="Error" Visible="false">
            </cc1:MessageBox>
                </div>
                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            عنوان
                        </label>
                    </div>
                    <div class="span2 right">
                        <asp:TextBox ID="txtTitle" runat="server" Width="160px" MaxLength="100" CssClass="req"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>  
             <%--   <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            توضیحات</label>
                    </div>
                    <div class="span2 right">
                        <asp:TextBox ID="txtDesc" runat="server" Width="160px" MaxLength="300" TextMode="MultiLine"></asp:TextBox>
                        <p class="help" data-tip="به صورت اختیاری است و در صفحه اصلی تصویر نمایش داده می شود.">
                        </p>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>  



                
                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            مکان تبلیغ</label>
                    </div>
                    <div class="span2 right">
                        
                <asp:DropDownList ID="DropDownList1" Width="160px" runat="server"  onchange="ShowDestinationOthers(this.value,'groupsddl')">
                <asp:ListItem Value="1" Text="صفحه اصلی" ></asp:ListItem>
                <asp:ListItem Value="2" Text="در صفحه گروه" ></asp:ListItem>
                
                </asp:DropDownList>

                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>  
                    
                <div ID="groupsddl" style="display:none">
                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            گروه</label>
                    </div>
                    <div class="span2 right">
                        
                <asp:DropDownList ID="ddlgroup" runat="server" Width="160px">
                        <asp:ListItem Value="-1">انتخاب کنید</asp:ListItem>
                        </asp:DropDownList>

                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>  </div>

                      --%>


                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            لینک
                        </label>
                    </div>
                    <div class="span2 right">
                        <asp:TextBox ID="txtLink" runat="server" Width="160px" Text="http://" MaxLength="500" CssClass="req ltr"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>  



                 
                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            فایل تصویر</label>
                    </div>
                    <div class="span2 right">
                        <asp:FileUpload ID="FileUpload1" Width="165px" runat="server" CssClass="req" />
                           <asp:Image ID="selectedimg" runat="server" Visible="false" ClientIDMode="Static" ImageUrl="images/nopic.png" BorderWidth="1"
                    AlternateText="" CssClass="newimg nomarg" />
                        
                    </div>
                    <div class="clear">
                    </div>
                </div>

                
                <div class="separator">
                </div>  
              <%--             
                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            تاریخ نمایش </label>
                    </div>
                    <div class="span2 right">

                         از &nbsp;&nbsp;&nbsp;<asp:TextBox CssClass="ltr" ID="txtStartDate" ClientIDMode="Static" runat="server" Width="70" ></asp:TextBox>&nbsp;&nbsp;&nbsp;<br />
                         تا &nbsp;&nbsp;&nbsp;<asp:TextBox CssClass="ltr" ID="txtEndDate" ClientIDMode="Static" runat="server" Width="70" ></asp:TextBox>
                                <p class="help" data-tip="برای نمایش بدون شرط، تاریخ انتشار و انقضا را مانند هم وارد کنید.">
                        </p>
                    </div>
                    <div class="clear">
                    </div>
                </div> 

                
                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            اولویت
                        </label>
                    </div>
                    <div class="span2 right">
                        <asp:TextBox ID="txtPriority" runat="server" Width="160px" MaxLength="10" onkeypress="return validate(event)" Text="0"  CssClass="req ltr"></asp:TextBox>

                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div> 
                
                <div class="separator">
                </div>  --%>

                <div class="rowelement">
                    <div class="span2 right">
                    </div>
                    <div class="span6 right">
                        <asp:Button ID="Button1" runat="server" Text="ذخـــیره" CssClass="btn btn-success"
                            OnClick="Button1_Click" />
                            
                        <asp:Button ID="Button2" runat="server" Text="ذخـــیره" CssClass="btn btn-success"
                            OnClick="Button2_Click" Visible="false" />
                            
                        <a href="slider.aspx" class="btn btn-warning" runat="server" id="cancl" visible="false">انصراف</a>
                        <asp:Literal ID="ltrID" Visible="false" runat="server"></asp:Literal>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
    
<div class="box lefted">
<div class="header">
<h4>لیست تصویر ها</h4>
</div>
<div class="conten">
<asp:GridView CssClass="table normal margin-reset" ID="GridView1" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="ID" 
                Width="100%" AllowPaging="True" GridLines="None" PageSize="40" 
                 EnableTheming="False" EnableViewState="False"  OnPageIndexChanging="GridView1_PageIndexChanging" 
                ShowFooter="True" onrowdatabound="GridView1_RowDataBound" 
        onrowcommand="GridView1_RowCommand" DataSourceID="sds_slider" >
                
                <Columns>
               
                    <asp:TemplateField FooterText="عنوان" HeaderText="عنوان" 
                        SortExpression="title">
                        <HeaderTemplate>
                          عنوان
                        </HeaderTemplate>
                        <ItemTemplate>
                            <b><a href='<%#Eval("pic","/content/slider/{0}") %>' class="imginside"><%# Eval("Title") %>
                            <img src='<%#Eval("pic","/content/slider/{0}") %>' alt="" />
                            </a></b>
                          
                        </ItemTemplate> 
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle Width="250px" />
                    </asp:TemplateField>
                     
                      <asp:TemplateField FooterText="تنظیمات" HeaderText="تنظیمات">
                        <ItemTemplate>
                            <center>
                            
                            <asp:LinkButton ID="LinkButton2" CommandArgument='<%# Eval("id") %>' CommandName="edt" runat="server" ><i class="icon-edit"></i></asp:LinkButton>

<asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("id") %>' CommandName="Del" runat="server" OnClientClick="return confirm('از حذف این رکورد دارید؟');" ><i class="icon-remove"></i></asp:LinkButton>
                            
                            </center>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="50px" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle CssClass="footer" />
                <PagerStyle CssClass="pager" />
                <EmptyDataRowStyle CssClass="empty" />
                <EmptyDataTemplate >
                <p class="empty">هیچ رکوردی وجود ندارد.</p>
                </EmptyDataTemplate>
            </asp:GridView>
    <asp:SqlDataSource ID="sds_slider" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>" 
        SelectCommand="SELECT * FROM [slidercontent]"></asp:SqlDataSource>
</div></div>
</asp:Content>
