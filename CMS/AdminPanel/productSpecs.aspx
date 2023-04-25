<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="productSpecs.aspx.cs" Inherits="CMS.Manage.productSpecs" ValidateRequest="false" %>

<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <title>مشخصه های محصول</title>
    <style>
         textarea {
                min-height: 60px !important; 
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    
    
    <div class="cont tabs padding-reset ui-tabs ui-widget ui-widget-content ui-corner-all">
        <ul class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
            <li class="ui-state-default ui-corner-top"><a href="editproduct.aspx?id=<%=Request.QueryString["id"] %>">ویرایش محصول</a></li>
            <li class="ui-state-default ui-corner-top ui-tabs-selected ui-state-active"><a href="productspecs.aspx?id=<%=Request.QueryString["id"] %>">مشخصه ها</a></li>
            <li class="ui-state-default ui-corner-top"><a href="productimages.aspx?id=<%=Request.QueryString["id"] %>">گالری تصاویر</a></li>
            
            <%--<li class="ui-state-default ui-corner-top"><a href="relatedproduct.aspx?id=<%=Request.QueryString["id"] %>">محصولات مرتبط</a></li> --%>
            <li class="ui-state-default ui-corner-top"><a href="prices.aspx?id=<%=Request.QueryString["id"] %>">قیمت ها</a></li> 
            <li class="ui-state-default ui-corner-top"><a href="detail.aspx?id=<%=Request.QueryString["id"] %>">جزئیات محصول</a></li> 

        </ul>
        <div id="tabs-1" class="ui-tabs-panel ui-widget-content ui-corner-bottom">
            
    <div class="box">
        <div class="header">
            <h4>مشخصه های محصول</h4>
        </div>
        <div class="content">

            <fieldset>

                <div class="rowelement withpadding">
                    <cc1:MessageBox ID="MessageBox1" runat="server" MessageType="Error" Visible="false">
                    </cc1:MessageBox>
                </div>

                <div class="rowelement">
                    <div class="span2 right">
                        <b>عنوان محصول</b>
                    </div>
                    <div class="span6 right">
                        <asp:Literal ID="ltrtitle" runat="server"></asp:Literal>
                    </div>
                    <div class="clear"></div>
                    </div>
                <div class="rowelement">

                    <div class="span2 right">
                        <b>شهر</b>
                    </div>
                    <div class="span6 right">
                        <asp:Literal ID="ltrgrop" runat="server"></asp:Literal>
                    </div>
                    <div class="clear"></div>
                </div>
                <div class="separator">
                </div>

                <asp:Literal ID="ltrer" runat="server"></asp:Literal>
             
                        <%--------specs ----------%>
                         

                        <asp:SqlDataSource ID="sdsSpecificationAttribute" runat="server" ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>"
                            SelectCommand="GroupSpecificationsActive" SelectCommandType="StoredProcedure">
                           
                        </asp:SqlDataSource>

                        <asp:Panel ID="pnl_View" runat="server">

                            <asp:ListView ID="lvwItems" runat="server" DataKeyNames="ID" DataSourceID="sdsSpecificationAttribute"
                                ItemPlaceholderID="PlaceHolder" OnItemDataBound="ItemDB">
                                <LayoutTemplate>
                                  
                                        <asp:PlaceHolder ID="PlaceHolder" runat="server"></asp:PlaceHolder>
                                 
                                </LayoutTemplate>
                                <ItemTemplate>
                                   
                <div class="rowelement">
                    <div class="span2 right">
                                        <h4>

                                            <%# Eval("Title")%>
                                        </h4>
                        </div>
                    <div class="span6 right">
                                        <asp:HiddenField ID="hf_Type" runat="server" Value='<%# Eval("FiledType")%>' />
                                        <asp:HiddenField ID="hd_ID" runat="server" Value='<%# Eval("ID")%>' />

                                        <div class="answer">




                                            <asp:ListBox ID="lsbOptions" runat="server" OnDataBound="lsbOptions_DataBound" SelectionMode="Multiple" DataSourceID="sdsGetSpecOptions" DataTextField="title" DataValueField="id" Visible="false"></asp:ListBox>

                                            <asp:TextBox ID="txtText" runat="server" Visible="false" TextMode="MultiLine" Height="60px"></asp:TextBox>

                                            <asp:DropDownList ID="ddlOptions" runat="server" OnDataBound="ddlOptions_DataBound" DataSourceID="sdsGetSpecOptions"
                                                 DataTextField="title"   DataValueField="id" Visible="false"></asp:DropDownList>
                                             
                                            <asp:Literal ID="divider" visible="false" runat="server" Text="<hr />"></asp:Literal>

                                   



                                            <asp:SqlDataSource ID="sdsGetSpecOptions" runat="server" ConnectionString="<%$ ConnectionStrings:CMSDataBaseConnectionString %>"
                                                SelectCommand="GetSpecOptions" SelectCommandType="StoredProcedure">
                                                <SelectParameters>
                                                    <asp:Parameter Name="specID" DefaultValue="" Type="Int32" />

                                                </SelectParameters>
                                            </asp:SqlDataSource>

                                            
                                    </div>





                                        </div>
                    <div class="clear">
                    </div>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>



                        </asp:Panel>



                        <%--------specs ----------%>
              
                    <div class="clear">
                    </div>
                 

                <div class="rowelement">
                    <div class="span6 right">
                    </div>
                    <div class="span6 right">
                        <asp:Button ID="Button1" runat="server" Text="ذخـــیره" CssClass="btn btn-success"
                            OnClick="Button1_Click" />
                          
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </fieldset>
        </div>
    </div>

        <div class="clear"></div>

        </div>
         
    </div>

</asp:Content>
