<%@ Page Language="C#" MasterPageFile="~/AdminPanel/managemaster.Master" AutoEventWireup="true" CodeBehind="SelectType.aspx.cs" Inherits="CMS.AdminPanel.SelectType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <title>افزودن محصول جدید
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="brow">
        <div class="row-fluid text-center center">
            <div>
                <br />
                <h4 class="">نوع محصول مورد نظر را انتخاب کنید</h4>
            </div>
            <br />
            <br />

            <%foreach (var item in ProductTypes)
                {%>
            <div class="selecttype">
                <a href='/adminpanel/addproduct/<%=item.Name %>'>
                    <img src='<%=item.Pic %>' />
                    <br />
                    <span class="dasboard-icons-title"><%=item.Title %></span>


                </a>
            </div>

            <%  } %>
        </div>
    </div>

</asp:Content>
