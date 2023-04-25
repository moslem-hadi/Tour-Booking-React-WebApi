<%@ Page  Language="C#" MasterPageFile="~/member/membermaster.Master" AutoEventWireup="true" CodeBehind="ticketlist.aspx.cs" Inherits="CMS.member.ticketlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
<title>لیست تیکت های پشتیبانی</title>
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainFullContent" runat="server">
    <div class="content">
      
            <div class="page-header">
                <h3>
                    <span>لیست تیکت های پشتیبانی</span></h3>
            </div>
            
            <div class="panel light-shadow white title-transparent rounded clearfix ">
                <div style="margin:-15px -15px -15px">
      <asp:GridView CssClass="table normal margin-reset" ID="GridView1" 
          runat="server" AutoGenerateColumns="False"
                DataKeyNames="ID" DataSourceID="sdsticketList" 
          EnableModelValidation="True" Width="100%" OnRowDataBound="GridView1_RowDataBound"
                AllowPaging="True" GridLines="None" PageSize="10" PagerSettings-Position="Bottom" 
          EnableTheming="False" EnableViewState="False"
                ShowFooter="false" >
                <AlternatingRowStyle CssClass="alt" />
                <Columns> 
                    <asp:TemplateField FooterText="عنوان" SortExpression="Title" HeaderText="عنوان">
                        
                        <ItemTemplate>
                            
                            <a href='<%# Eval("id","viewticket.aspx?id={0}")%>' >
                                <%# Eval("Title") %></a>
                            
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                    </asp:TemplateField>

                    <asp:TemplateField FooterText="بخش" SortExpression="Title" HeaderText="بخش">
                        
                        <ItemTemplate>
                            
                                <center><%# Eval("Part") %></center>
                            
                        </ItemTemplate>
                        <ItemStyle Width="130" />
                        <HeaderStyle HorizontalAlign="Right" />
                    </asp:TemplateField>




                      <asp:TemplateField FooterText="بروزرسانی" SortExpression="LastUpdate" HeaderText="بروزرسانی">
                   
                        <ItemTemplate>
                            <p style="text-align:center; font-family:'b yekan','byekan',tahoma; margin:0;"   title='<%# CMS.CommonFunctions.String2date(Eval("LastUpdate"), 2, "H") %>'>
                                <%# CMS.CommonFunctions.String2date(Eval("LastUpdate"), 3, "")%></p>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center"/>
                        <FooterStyle HorizontalAlign="Center" />
                        <ItemStyle Width="90" />
                    </asp:TemplateField>

                    
                    <asp:TemplateField FooterText="وضعیت"  HeaderText="وضعیت">
                        
                        <ItemTemplate>
                           <center><%# Eval("status").ToString() == "1" ? "<span style='color:#017ad8'>در انتظار پاسخ</span>" : Eval("status").ToString() == "2" ? "<span style='color:#35bd00'>پاسخ داده شده</span>" : "<span style='color:#7f7f7d'>بسته شده</span>"%></center>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center"/>
                        <FooterStyle HorizontalAlign="Center" />
                        <ItemStyle Width="90" />
                    </asp:TemplateField>

                    <asp:TemplateField FooterText="حذف" HeaderText="حذف">
                        <ItemTemplate>
                            <center>
                            <asp:LinkButton ID="lnkDel" CommandArgument='<%# Eval("id") %>' ToolTip="حذف تیکت" CommandName="Delete"  runat="server" OnClientClick="return confirm('از حذف این رکورد دارید؟');" ><i class="fa fa-times text-danger"></i></asp:LinkButton>
                            </center>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="30px" />
                    </asp:TemplateField> 
                </Columns>
                <FooterStyle CssClass="footer" />
                <PagerStyle CssClass="pager" />
                <EmptyDataRowStyle CssClass="empty" />
                <EmptyDataTemplate>   <p class="empty">
				<img src="images/under_construction.jpg" alt="" class="mg-bottom-30" /><br />
                    تیکتی وجود ندارد. برای افزودن تیکت جدید <a href="addticket.aspx">
                            کلیک کنید</a>.</p>
                </EmptyDataTemplate>
            </asp:GridView>

</div>



    <asp:SqlDataSource ID="sdsticketList" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>" 
        DeleteCommand="update ticket set IsDeleted='true',status=4 where ID=@ID" 
        SelectCommand="Usertickets" SelectCommandType="StoredProcedure">
         <DeleteParameters>
                    <asp:ControlParameter ControlID="GridView1" Name="ID" PropertyName="SelectedValue" />
                </DeleteParameters>
                <SelectParameters>
                            <asp:Parameter Name="UserID" Type="Int32" />
                </SelectParameters>
    </asp:SqlDataSource>


</div>
 
    </div>
</asp:Content>
