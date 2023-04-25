<%@ Page  Language="C#" MasterPageFile="~/adminpanel/managemaster.Master" AutoEventWireup="true" CodeBehind="ticketlist.aspx.cs" Inherits="CMS.Manage.ticketlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <title>لیست تیکت های دریافتی</title>
    <script>
        function SelectAllCheckboxesGridView1(chk) {
            $('#<%=GridView1.ClientID %>').find("input:checkbox").each(function () {
              if (this != chk) {
                  this.checked = chk.checked;
              }
          });
      }
    </script>
    <style>
        .id label {
            font: normal 10px tahoma !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="box">
        <div class="header">
            <h4>لیست تیکت های دریافتی</h4>
        </div>
        <div class="conten">

            <div class="srch">
                <asp:Panel ID="Panel1" CssClass="con" runat="server" DefaultButton="LinkButton2">

                    <asp:TextBox ID="TextBox1" runat="server" placeholder="جستجو بر اساس عنوان، کد تیکت و کد کاربری"></asp:TextBox>
                    <asp:LinkButton CssClass="srchbtn" ID="LinkButton2" runat="server"
                        OnClick="LinkButton2_Click"><i class="icon-search"></i></asp:LinkButton>
                </asp:Panel>
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" OnClientClick="return confirm('از حذف انتخاب شده ها اطمینان دارید؟');" class="btn btn-danger"><i class="icon-remove"></i> حذف انتخاب شده ها</asp:LinkButton>
                <a href="#" class="btn btn-warning" id="btnclose" onclick="return confirm('از بستن انتخاب شده ها اطمینان دارید؟');">بستن</a>
                <div class="clear"></div>
            </div>

            <asp:GridView CssClass="table normal margin-reset" ID="GridView1" runat="server" AutoGenerateColumns="False"
                DataKeyNames="ID" DataSourceID="sds_content" EnableModelValidation="True" Width="100%"  OnRowCommand="GridView1_RowCommand"
                AllowPaging="True" GridLines="None" PageSize="20" EnableTheming="False" EnableViewState="False"
                ShowFooter="True"
                OnRowDataBound="GridView1_RowDataBound">
                <AlternatingRowStyle CssClass="alt" />
                <Columns>
                    <asp:TemplateField FooterText="#" HeaderText="#" InsertVisible="False" SortExpression="ID">
                        <HeaderTemplate>

                            <input id="chkBxSelectselect" type="checkbox" name="none" onclick="javascript: SelectAllCheckboxesGridView1(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox Text="" id="chkBxSelect"  data-name='<%# Eval("id") %>' title='<%# Eval("id") %>' ClientIDMode="Static" runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>

                            <input id="chkBxSelectselect" type="checkbox" name="none" onclick="javascript: SelectAllCheckboxesGridView1(this);" />
                        </FooterTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Font-Names="b yekan" HorizontalAlign="Center" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField FooterText="عنوان" SortExpression="Title" HeaderText="عنوان">

                        <ItemTemplate>

                            <a href='<%#Eval("id","viewticket.aspx?id={0}") %>'>
                                <%# Eval("Title") %></a>
                            <br />
                            <small>ارسال: <%# CMS.CommonFunctions.String2date(Eval("RegDate"), 2, "d,H")%></small>

                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                    </asp:TemplateField>




                    <asp:TemplateField FooterText="کاربر" HeaderText="کاربر">

                        <ItemTemplate>
                            <center><a href='<%# Eval("userid","userdetail.aspx?id={0}") %>'><%# Eval("FullName")%></a></center>
                        </ItemTemplate>
                        <ItemStyle Width="90" />
                    </asp:TemplateField>


                    <asp:TemplateField FooterText="بروزرسانی" SortExpression="LastUpdate" HeaderText="بروزرسانی">

                        <ItemTemplate>
                            <p style="margin: 0; text-align: center; font-family: 'b yekan','byekan',tahoma; font-size: 12px" title='<%# CMS.CommonFunctions.String2date(Eval("LastUpdate"), 2, "H") %>'>
                                <%# CMS.CommonFunctions.String2date(Eval("LastUpdate"), 2, "d,H")%>
                            </p>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <FooterStyle HorizontalAlign="Center" />
                        <ItemStyle Width="80" />
                    </asp:TemplateField>


                    <asp:TemplateField FooterText="وضعیت" HeaderText="وضعیت">

                        <ItemTemplate>
                            <center><%# Eval("status").ToString() == "1" ? "<span style='color:#017ad8'>در انتظار پاسخ</span>" : Eval("status").ToString() == "2" ? "<span style='color:#35bd00'>پاسخ داده شده</span>" : Eval("status").ToString() == "3" ? "<span style='color:#7f7f7d'>بسته شده</span>" : "<span style='color:#e54600'>حذف شده</span>"%></center>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <FooterStyle HorizontalAlign="Center" />
                        <ItemStyle Width="90" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField FooterText="اولویت" HeaderText="اولویت">

                        <ItemTemplate>

                            <center><%# Eval("priority").ToString().ToLower()=="medium" ? "متوسط" : 
                                             Eval("priority").ToString().ToLower()=="low" ? "کم" : "خیلی زیاد"%></center>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <FooterStyle HorizontalAlign="Center" />
                        <ItemStyle Width="70" />
                    </asp:TemplateField>
                    <asp:TemplateField FooterText="بخش" HeaderText="بخش">

                        <ItemTemplate>

                            <center><%# Eval("part") %></center>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <FooterStyle HorizontalAlign="Center" />
                        <ItemStyle Width="90" />
                    </asp:TemplateField>

                    <asp:TemplateField FooterText="تنظیمات" HeaderText="تنظیمات">
                        <ItemTemplate>
                            <center>
                             
<asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("id") %>' ToolTip="حذف تیکت" CommandName="Del" runat="server" OnClientClick="return confirm('از حذف این تیکت اطمینان دارید؟');" ><i class="icon-remove"></i></asp:LinkButton>
                                    
                                    
<asp:LinkButton ID="LinkButton4" CommandArgument='<%# Eval("id") %>' ToolTip="بستن تیکت" CommandName="Update" runat="server" OnClientClick="return confirm('از بستن این تیکت اطمینان دارید؟');" ><i class="icon-screenshot"></i></asp:LinkButton>
                                    
                                    
                            </center>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="60px" />
                    </asp:TemplateField>


                </Columns>
                <FooterStyle CssClass="footer" />
                <PagerSettings Position="TopAndBottom" />
                <PagerStyle CssClass="pager" />
                <EmptyDataRowStyle CssClass="empty" />
                <EmptyDataTemplate>
                    <p class="empty">تیکتی وجود ندارد</p>
                </EmptyDataTemplate>
            </asp:GridView>


            <asp:SqlDataSource ID="sds_content" runat="server" ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>"
                SelectCommand="ManageTickets" UpdateCommand="update ticket set status=3 where id=@id" 
                SelectCommandType="StoredProcedure"  DeleteCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:QueryStringParameter Name="key" DefaultValue="none" QueryStringField="key" Type="String" />
                </SelectParameters>
             
                <UpdateParameters>
                    <asp:ControlParameter ControlID="GridView1" Name="ID" PropertyName="SelectedValue" />
                </UpdateParameters>
            </asp:SqlDataSource>


        </div>
    </div>
    <script>
        $(function () {
            $("#btnclose").click(function () {


                var checkedids = '';
                $('#<%=GridView1.ClientID %>').find("input:checkbox").each(function () {
                    if (this.checked == true) {
                        var chb = this;
                    var id = $(chb).parent().data('name');
                    if (id != "none")
                        checkedids = checkedids + id + ',';
                }
                });

            $.ajax({
                type: "Post",
                url: "ticketlist.aspx/close",
                data: "{ids: '" + checkedids + "'}",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (data) {
                    window.location.reload();
                }
            });
        });
    });
    </script>
</asp:Content>
