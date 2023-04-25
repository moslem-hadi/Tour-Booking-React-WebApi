<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="productattributeoptions.aspx.cs" Inherits="CMS.Manage.productattributeoptions" %>

<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <title>
       گزینه های ویژگی قابل انتخاب مرتبط</title>
    <script>
        function SelectAllCheckboxesGridView1(chk) {
            $('#<%=GridView1.ClientID %>').find("input:checkbox").each(function () {
             if (this != chk) {
                 this.checked = chk.checked;
             }
         });
     }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box righted">
        <div class="header">
            <h4>افزودن گزینه
                <asp:HyperLink ID="hplback" style="font-size:14px;padding-left:10px;font-weight:normal;float:left" runat="server">برگشت به لیست ویژگی ها</asp:HyperLink>
            </h4>
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
                            محصول
                        </label>
                    </div>
                    <div class="span2 right">
                        <asp:Literal ID="ltrTitle" runat="server"></asp:Literal>
                    </div>
                    <div class="clear">
                    </div>
                </div>

                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            ویژگی
                        </label>
                    </div>
                    <div class="span2 right">
                        <asp:Literal ID="ltrAttrTitle" runat="server"></asp:Literal>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                
                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            عنوان گزینه
                        </label>
                    </div>
                    <div class="span2 right">
                        <asp:TextBox ID="txtTitle" runat="server" Width="210px" CssClass=" "></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>

                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            توضیحات 
                        </label>
                    </div>
                    <div class="span2 right">
                        <asp:TextBox ID="txtDesc" runat="server" Width="210px" CssClass=" "></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>


                
                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            تغییر قیمت  
                        </label>
                    </div>
                    <div class="span2 right">
                        <asp:TextBox ID="txtPriceAdjust" runat="server" Width="175px" onkeypress="return validate(event)" Text="0"  CssClass="ltr req txtprice"></asp:TextBox>
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
                        <asp:TextBox ID="txtPriority" runat="server" onkeypress="return validate(event)" Text="0" Width="50px" CssClass="ltr"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                
                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                            وضعیت
                        </label>
                    </div>
                    <div class="span2 right">
                        <asp:CheckBox ID="chbStat"  runat="server" Checked="true" Text=" موجود" />
                    </div>
                    <div class="clear">
                    </div>
                </div>

                <div class="rowelement">
                    <div class="span1 right">
                        <label>
                              پیشفرض
                        </label>
                    </div>
                    <div class="span2 right">
                        <asp:CheckBox ID="chbPreselect" runat="server" Checked="false" Text=" انتخاب شده باشد" />
                    </div>
                    <div class="clear">
                    </div>
                </div>

                 


                <div class="rowelement">
                    <div class="span1 right"></div>
                    <div class="span2 right">
                        <asp:Button ID="Button1" runat="server" Text="ذخـــیره" CssClass="btn btn-success"
                            OnClick="Button1_Click" />
                        
                        <asp:Button ID="Button2" runat="server" Text="ذخـــیره" CssClass="btn btn-success"
                            OnClick="Button2_Click" Visible="false" />
                <asp:HyperLink ID="cancl" visible="false" class="btn btn-warning" runat="server">انصراف</asp:HyperLink>
                             
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
            <h4>لیست کالاها</h4>
        </div>
        <div class="conten">
            <asp:GridView CssClass="table normal margin-reset" ID="GridView1" runat="server" AutoGenerateColumns="False"
                DataKeyNames="ID" EnableModelValidation="True"
                Width="100%" AllowPaging="True" GridLines="None" PageSize="100"
                EnableTheming="False" EnableViewState="False" OnPageIndexChanging="GridView1_PageIndexChanging"
                ShowFooter="True"
                OnRowCommand="GridView1_RowCommand">

                <Columns>


                    <asp:TemplateField FooterText="عنوان" HeaderText="عنوان"
                        SortExpression="title">
                        <HeaderTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" onclick="javascript:SelectAllCheckboxesGridView1(this);"
                                Text="عنوان" />
                        </HeaderTemplate>
                        <ItemTemplate>

                            <asp:CheckBox ID="chkBxSelect" runat="server" Text='<%# Eval("title") %>' />


                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" onclick="javascript:SelectAllCheckboxesGridView1(this);"
                                Text="عنوان" />
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                      
                    </asp:TemplateField>





                    

                    <asp:TemplateField FooterText="تغییرقیمت" HeaderText="تغییرقیمت"
                        SortExpression="PriceAdjust">
                       
                        <ItemTemplate>

                           <%# CMS.CommonFunctions.SetCama(Eval("PriceAdjust") )%>  تومان


                        </ItemTemplate>
                      
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
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
                <EmptyDataTemplate>
                    <p class="empty">هیچ رکوردی وجود ندارد.</p>
                </EmptyDataTemplate>
            </asp:GridView>


            <div class="btnbottom">
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" OnClientClick="return confirm('از حذف انتخاب شده ها اطمینان دارید؟');" class="btn btn-danger"><i class="icon-remove"></i> حذف انتخاب شده ها</asp:LinkButton></div>
        </div>

    </div>
</asp:Content>
