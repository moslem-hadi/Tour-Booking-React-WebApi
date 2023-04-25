    <%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="productgroup.aspx.cs" Inherits="CMS.Manage.productgroup" ValidateRequest="false" %>

<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<title>لیست شهرها</title>
    <script src="js/urlcheck.js" type="text/javascript"></script>
        <link href="/component/select2/select2-4.0.1-rc.1/dist/css/select2.min.css" rel="stylesheet" />
    <script src="/component/select2/select2-4.0.1-rc.1/dist/js/select2.min.js"></script>

    <script>
        $(document).ready(function () {
            $("#" + "<%=DropDownList1.ClientID%>").select2({
                placeholder: "شهر را انتخاب کنید",
                allowClear: true,
                language: "fa",
                dir: "rtl"
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">




    
<div class="box">
<div class="header">
<h4>لیست شهرها</h4>
</div>
<div class="conten">
<asp:GridView CssClass="table normal margin-reset" ID="GridView1" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="ID" EnableModelValidation="True" 
                Width="100%" AllowPaging="True" GridLines="None" PageSize="100" 
                 EnableTheming="False" EnableViewState="False"  OnPageIndexChanging="GridView1_PageIndexChanging" 
                ShowFooter="True" onrowdatabound="GridView1_RowDataBound" 
        onrowcommand="GridView1_RowCommand" >
                
                <Columns>
               
                    <asp:TemplateField FooterText="عنوان" HeaderText="عنوان" 
                        SortExpression="title">
                        <HeaderTemplate>
                          عنوان
                        </HeaderTemplate>
                        <ItemTemplate>
                            <b><%# Eval("Title") %></b>
                        </ItemTemplate> 
                        <HeaderStyle HorizontalAlign="Right" />
                        
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="کد" FooterText="کد">
                        <ItemTemplate>
                            <center>
                                <%# Eval("id") %>
                          </center>
                        </ItemTemplate>
                        
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="40px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="اولویت" FooterText="اولویت">
                        <ItemTemplate>
                            <center>
                                <%# Eval("PRIORITY") %>
                          </center>
                        </ItemTemplate>
                        
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="40px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="تعداد محصول" FooterText="تعداد محصول">
                        <ItemTemplate>
                            <center>
                                <asp:Literal ID="ltr_count" runat="server"></asp:Literal>
                          </center>
                        </ItemTemplate>
                        
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="70px" />
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
</div></div>





    <div class="box">
        <div class="header">
            <h4>
                ایجاد شهر  جدید</h4>
        </div>
        <div class="content">
            
            <fieldset>
            
                <div class="rowelement withpadding">
                <cc1:MessageBox ID="MessageBox1" runat="server" MessageType="Error" Visible="false">
            </cc1:MessageBox>
                </div>
                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            شهر یا استان والد
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:DropDownList ID="DropDownList1" Width="410px" runat="server">
                </asp:DropDownList>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div> 
                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            عنوان شهر/استان
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:TextBox ID="txtTitle" runat="server" Width="400px" MaxLength="100" CssClass="req"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div> 
                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            آدرس در URL
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:TextBox ID="txtSlug" runat="server" Width="400px" MaxLength="100" CssClass="req ltr"   onkeyup="nospaces(this)"></asp:TextBox>

                        <small id="getUrl" style="font-size:12px;background: #eee;padding: 3px 5px;border-radius: 3px;vertical-align: middle;cursor: pointer;"><i class="icon-hand-right" style="vertical-align:middle"></i> دریافت آدرس استان</small>

                        <script>
                            $(document).ready(function () {
                                $("#getUrl").click(function () {
                                    var groupID = $("#<%=DropDownList1.ClientID%>").val();
                                    if (groupID == 0)
                                        return;
                                    $("#getUrl").append('<span id="liload" class="text-center"><img src="/images/loading.gif" width="15" /></span>');
                                    
                                    $.ajax({
                                        type: "Post",
                                        url: "productgroup.aspx/GetUrl",
                                        data: "{groupID: "+groupID+"}",
                                        contentType: "application/json;charset=utf-8",
                                        dataType: "json",
                                        success: function (data) {
                                            var index = 0;

                                            $("#<%=txtSlug.ClientID%>").val(data.d + '/').focus();
                                            

                                            $("#liload").remove();

                                        }
                                    });
                                });
                            });
                        </script>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                
                <div class="separator">
                </div> 
                
                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            عنوان صفحه برای سئو
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:TextBox ID="txtSeoTitle" runat="server" Width="400px" MaxLength="100" CssClass="req "></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div> 
              



                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            توضیحات سئو</label>
                    </div>
                    <div class="span6 right">
                        <asp:TextBox ID="txtSeoDescription" runat="server" Width="400px" MaxLength="300" TextMode="MultiLine"></asp:TextBox>
                        <p class="help" data-tip="تگ meta description برای توضیحات سئو.">
                        </p>
                    </div>
                    <div class="clear">
                    </div>
                </div> 
                
                <div class="separator">
                </div> 

                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            اولویت (از کم به زیاد)
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:TextBox ID="txtPriority" runat="server" Width="80px" MaxLength="10" onkeypress="return validate(event)" Text="0"  CssClass="req ltr"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                 
                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            وضعیت
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:CheckBox ID="chbshow" Text=" نمایش داده شود" Checked="true" runat="server" />
                    </div>
                    <div class="clear">
                    </div>
                </div>
                 
                <div class="rowelement center"> 
                        <asp:Button ID="Button1" runat="server" Text="ذخـــیره" CssClass="btn btn-success"
                            OnClick="Button1_Click" />
                            
                        <asp:Button ID="Button2" runat="server" Text="ذخـــیره" CssClass="btn btn-success"
                            OnClick="Button2_Click" Visible="false" />
                            
                        <a href="ProductGroup.aspx" class="btn btn-warning" runat="server" id="cancl" visible="false">انصراف</a>
                        <asp:Literal ID="ltrID" Visible="false" runat="server"></asp:Literal>
                     
                    <div class="clear">
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
    
</asp:Content>
