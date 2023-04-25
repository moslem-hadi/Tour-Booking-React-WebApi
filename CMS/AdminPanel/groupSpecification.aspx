<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="groupSpecification.aspx.cs" Inherits="CMS.Manage.groupSpecification" %>

<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<title>لیست مشخصه های محصول</title>
    
       
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box">
        <div class="header">
            <h4>
                ایجاد مشخصه جدید</h4>
        </div>
        <div class="content">
            
            <fieldset>
            
                <div class="rowelement withpadding">
                <cc1:MessageBox ID="MessageBox1" runat="server" MessageType="Error" Visible="false">
            </cc1:MessageBox>
                </div>
                 
                <div class="rowelement hidden">
                    <div class="span2 right">
                        <label>
                            گروه بندی مشخصه
                        </label>
                    </div>
                    <div class="span6 right">
                        
      
            <asp:SqlDataSource ID="sdsSpecGroups" runat="server" ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>"
                 SelectCommand="select * from SpecificationAttributeGroup order by priority" SelectCommandType="Text">
                
            </asp:SqlDataSource>
                        <asp:DropDownList ID="ddlSpecGroup" runat="server" Width="200px"  DataSourceID="sdsSpecGroups" DataTextField="Title" DataValueField="ID"> 
                        </asp:DropDownList>
                    


                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="clear s">
                </div> 


                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            عنوان مشخصه
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:TextBox ID="txtTitle" runat="server" Width="200px" MaxLength="100" CssClass="req"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="clear s">
                </div> 

                 



                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            نوع مشخصه
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:DropDownList ID="ddlFieldType" runat="server" Width="200px"  onchange="ShowDestinationOthers(this.value,'chbFilte','chbshow')">
                            <asp:ListItem Value="1">تک انتخابی - DropDownList</asp:ListItem>
                            <asp:ListItem Value="3">متن - TextBox</asp:ListItem>
                            <asp:ListItem Value="2">چند انتخابی - CheckBox</asp:ListItem> 
                            
                            
                        </asp:DropDownList>
                     
                    &nbsp;&nbsp;   <span id="noshow" style="visibility:hidden">عدم نمایش | فقط لحاظ در جستجو</span>
                         
    <script>
        function ShowDestinationOthers(e, filter, show) {

            document.getElementById('noshow').style.visibility = "hidden";



            if (e == '1') {
                document.getElementById(filter).disabled = false;
                document.getElementById(show).disabled = false;

            }
            else if (e == '2') {

                document.getElementById('noshow').style.visibility = "visible";
                document.getElementById('noshow').innerText = "عدم نمایش | فقط لحاظ در جستجو";
                document.getElementById(show).checked = false;
                document.getElementById(show).disabled = true;
                document.getElementById(filter).disabled = false;

            }
            else if (e == '3') {

                document.getElementById('noshow').innerText = "عدم جستجو";
                document.getElementById('noshow').style.visibility = "visible";
                document.getElementById(filter).checked = false;
                document.getElementById(filter).disabled = true;
                document.getElementById(show).disabled = false;

            } 

        }
    </script>


                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="clear s">
                </div>  
                
                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            اولویت (از کم به زیاد)
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:TextBox ID="txtPriority" runat="server" onkeypress="return validate(event)" Text="0" Width="60px" MaxLength="100" CssClass="ltr"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="clear s">
                </div> 
                
                
                
                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            وضعیت
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:CheckBox ID="chbStat" ClientIDMode="Static" Text=" فعال باشد" Checked="true" runat="server" /><br />
                        <asp:CheckBox ID="chbFilte" ClientIDMode="Static" Text=" در جستجو ها لحاظ شود" runat="server" /><br />
                        <asp:CheckBox ID="chbshow" ClientIDMode="Static" Text=" در مشخصات محصول نمایش داده شود" Checked="true"  runat="server" />
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="clear s">
                </div> 
                <div class="rowelement">
                    <div class="span2 right">
                    </div>
                    <div class="span6 right">
                        <asp:Button ID="Button1" runat="server" Text="ذخـــیره" CssClass="btn btn-success"
                            OnClick="Button1_Click" />
                            
                        <asp:Button ID="Button2" runat="server" Text="ذخـــیره" CssClass="btn btn-success"
                            OnClick="Button2_Click" Visible="false" />
                            
                        <a href="groupSpecification.aspx" class="btn btn-warning" runat="server" id="cancl" visible="false">انصراف</a>
                        <asp:Literal ID="ltrID" Visible="false" runat="server"></asp:Literal>
                    </div>
                    <div class="clear">
                    </div>
                </div>

                    <div class="clear">
                    </div>



                 

            </fieldset>
        </div>
    </div>
    
<div class="box">
<div class="header">
<h4>لیست مشخصه ها</h4>
</div>
<div class="conten">

      
            <asp:SqlDataSource ID="sdsSpecificationAttribute" runat="server" ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>"
                 SelectCommand="GroupSpecifications" SelectCommandType="StoredProcedure">
                
            </asp:SqlDataSource>

<asp:GridView CssClass="table normal margin-reset" ID="GridView1" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="ID" EnableModelValidation="True" 
                Width="100%" AllowPaging="True" GridLines="None" PageSize="80" DataSourceID="sdsSpecificationAttribute"
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
                            
                            <%--<%# Eval("FiledType").ToString() == "3" ? Eval("Title") : string.Format("<a href='specoption.aspx?id={0}'>{1}</a>", Eval("ID"), Eval("Title")) %>--%>
                                                   <a  id='<%# Eval("id") %>' href='<%# Eval("id","specoption.aspx?id={0}") %>' ><%# Eval("Title") %>  <i style="font-style:normal;color:#808080;font-size: 13px;font-weight:normal"> (<%# Eval("FiledType").ToString() == "1" ? "تک انتخابی" : Eval("FiledType").ToString() == "2" ? "چند انتخابی" : "متن"%>)</i></a>

                            <%# Eval("UseInSearch").ToString()=="False" ? "" : "<span style='color:#f01;float:left' title='لحاظ در جستجوها'><i class='icon icon-search'></i></span>" %>
                        </ItemTemplate> 
                        <HeaderStyle HorizontalAlign="Right" />
                        
                    </asp:TemplateField>
                    
                    
                    <asp:TemplateField HeaderText="دسته" FooterText="دسته">
                        <ItemTemplate>
                            <center style="font-style:normal;color:#808080;font-size: 13px;font-weight:normal">
                                <asp:Literal ID="ltr_SpecGroup" Text='<%# Eval("SpecGroupID") %>' runat="server"></asp:Literal>
                          </center>
                        </ItemTemplate>
                        
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="150px" />
                    </asp:TemplateField>

                     

                    <asp:TemplateField HeaderText="اولویت" FooterText="اولویت">
                        <ItemTemplate>
                            <center>
                                <%# Eval("Priority") %>
                          </center>
                        </ItemTemplate>
                        
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="مقادیر" FooterText="مقادیر">
                        <ItemTemplate>
                            <center>
                                <asp:Literal ID="ltr_count" runat="server"></asp:Literal>
                          </center>
                        </ItemTemplate>
                        
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="50px" />
                    </asp:TemplateField>
                      <asp:TemplateField FooterText="تنظیمات" HeaderText="تنظیمات">
                        <ItemTemplate>
                            <center>
                            
                            <asp:LinkButton ID="LinkButton2" CommandArgument='<%# Eval("id") %>' CommandName="edt" runat="server" ><i class="icon-edit"></i></asp:LinkButton>

<asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("id") %>' CommandName="Del" runat="server" OnClientClick="return confirm('از حذف این رکورد دارید؟ همه رکوردهای مرتبط حذف خواهند شد.');" ><i class="icon-remove"></i></asp:LinkButton>
                            
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
                <EmptyDataTemplate >
                <p class="empty">هیچ رکوردی وجود ندارد.</p>
                </EmptyDataTemplate>
            </asp:GridView>
</div></div>
</asp:Content>
