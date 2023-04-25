<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="advertising.aspx.cs" Inherits="CMS.Manage.advertising" %>

<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <title>تبلیغات</title>
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
        function ShowDestinationOthers(e ) {
            if (e == '5')
                document.getElementById('groupsddl').style.display = 'block'
            else
                document.getElementById('groupsddl').style.display = 'none'

            if (e == '1')
                document.getElementById('divSize').style.display = 'block'
            else
                document.getElementById('divSize').style.display = 'none'

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box righted">
        <div class="header">
            <h4>افزودن تبلیغ جدید</h4>
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
                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            مکان تبلیغ</label>
                    </div>
                    <div class="span2 right">

                        <asp:DropDownList ID="DropDownList1" Width="160px" runat="server" 
                            onchange="ShowDestinationOthers(this.value )">
                            <asp:ListItem Value="1" Text="صفحه اصلی"></asp:ListItem>
                            <asp:ListItem Value="2" Text="سمت راست"></asp:ListItem>
                            <asp:ListItem Value="3" Text="سمت چپ همه شهر ها"></asp:ListItem>
                            <asp:ListItem Value="4" Text="سمت چپ شهر خاص"></asp:ListItem>

                        </asp:DropDownList>

                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>
                
                <div id="divSize" style="display: block">
                    <div class="rowelement">
                        <div class="span1 right">
                            <label>
                                اندازه</label>
                        </div>
                        <div class="span2 right">
                            
                            <asp:DropDownList ID="ddlSize" runat="server" Width="160px">
                                <asp:ListItem Value="12">1 ستون</asp:ListItem>
                                <asp:ListItem Value="4">1/3 ستون</asp:ListItem>
                                <asp:ListItem Value="8">2/3 ستون</asp:ListItem>
                                <asp:ListItem Value="6">1/2 ستون</asp:ListItem>
                            </asp:DropDownList>

                        </div>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="separator">
                    </div>
                </div>

                <div id="groupsddl" style="display: none">
                    <div class="rowelement">
                        <div class="span1 right">
                            <label>
                                شهر</label>
                        </div>
                        <div class="span2 right">

                            <asp:DropDownList ID="ddlgroup" runat="server" Width="160px">
                            </asp:DropDownList>

                        </div>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="separator">
                    </div>
                </div>

                
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
                            اولویت نمایش
                        </label>
                    </div>
                    <div class="span2 right">
                        <asp:TextBox ID="txtPriority" runat="server" Width="160px" Text="0" TextMode="Number"   CssClass="req ltr"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>

                 


                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            تصویر</label>
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

                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            تاریخ نمایش
                        </label>
                    </div>
                    <div class="span2 right">
                        از &nbsp;&nbsp;&nbsp;<asp:TextBox CssClass="ltr" ID="txtStartDate" ClientIDMode="Static" runat="server" Width="70"></asp:TextBox>&nbsp;&nbsp;&nbsp;<br />
                        تا &nbsp;&nbsp;&nbsp;<asp:TextBox CssClass="ltr" ID="txtEndDate" ClientIDMode="Static" runat="server" Width="70"></asp:TextBox>
                        <p class="help" data-tip="برای نمایش بدون شرط، تاریخ انتشار و انقضا را مانند هم وارد کنید.">
                        </p>
                    </div>
                    <div class="clear">
                    </div>
                </div>



                <div class="separator">
                </div>
                <div class="rowelement">
                    <div class="span2 right">
                    </div>
                    <div class="span6 right">
                        <asp:Button ID="Button1" runat="server" Text="ذخـــیره" CssClass="btn btn-success"
                            OnClick="Button1_Click" />

                        <asp:Button ID="Button2" runat="server" Text="ذخـــیره" CssClass="btn btn-success"
                            OnClick="Button2_Click" Visible="false" />

                        <a href="advertising.aspx" class="btn btn-warning" runat="server" id="cancl" visible="false">انصراف</a>
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
            <h4>لیست تبلیغات</h4>
        </div>
        <div class="conten">
            <asp:GridView CssClass="table normal margin-reset" ID="GridView1" runat="server" AutoGenerateColumns="False"
                DataKeyNames="ID"
                Width="100%" AllowPaging="True" GridLines="None" PageSize="40"
                EnableTheming="False" EnableViewState="False" OnPageIndexChanging="GridView1_PageIndexChanging"
                ShowFooter="True" OnRowDataBound="GridView1_RowDataBound"
                OnRowCommand="GridView1_RowCommand" DataSourceID="sds_slider">

                <Columns>

                    <asp:TemplateField FooterText="عنوان" HeaderText="عنوان"
                        SortExpression="title">
                        <HeaderTemplate>
                            عنوان
                        </HeaderTemplate>
                        <ItemTemplate>
                            <b><a href='<%#Eval("pic","/content/ads/{0}") %>' class="imginside"><%# Eval("Title") %>
                                <img src='<%#Eval("pic","/content/ads/{0}") %>' alt="" />
                            </a></b><span style="font-size: 13px">(<asp:Literal ID="ltrPlace" runat="server"></asp:Literal>)</span>
                            <br />

                            از: <%# CMS.CommonFunctions.String2date(Eval("FromDate"), 3, "")%>
                               
                                تا: <%# CMS.CommonFunctions.String2date(Eval("ToDate"), 3, "")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle Width="250px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="اولویت" FooterText="اولویت">
                        <ItemTemplate>
                            <center>
                               <%# Eval("priority") %>
                          </center>
                        </ItemTemplate>

                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="20px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="وضعیت" FooterText="وضعیت">
                        <ItemTemplate>
                            <center>
                                <asp:Literal ID="ltrStat" runat="server"></asp:Literal>
                          </center>
                        </ItemTemplate>

                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="20px" />
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
                        <ItemStyle Width="40px" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle CssClass="footer" />
                <PagerStyle CssClass="pager" />
                <EmptyDataRowStyle CssClass="empty" />
                <EmptyDataTemplate>
                    <p class="empty">هیچ رکوردی وجود ندارد.</p>
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:SqlDataSource ID="sds_slider" runat="server"
                ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>"
                SelectCommand="SELECT * FROM [advertisment]"></asp:SqlDataSource>
        </div>
    </div>
</asp:Content>
