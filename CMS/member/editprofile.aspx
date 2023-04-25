<%@ Page  Language="C#" MasterPageFile="~/member/membermaster.Master" AutoEventWireup="true" CodeBehind="editprofile.aspx.cs" Inherits="CMS.member.editprofile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>ویرایش مشخصات فردی</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainFullContent" runat="server">

    <div class="page-header">
        <h3>
            <span>ویرایش مشخصات شخصی</span>
        </h3>
    </div>

    <div class="panel light-shadow white title-transparent rounded clearfix">
        
            <asp:Literal ID="ltr_error" runat="server"></asp:Literal>

        <div class="col-md-6 col-sm-12">
            
            <div class="page-headers">
                <h4>
                    <span>ویرایش اطلاعات فردی</span></h4>
            </div>
			<p  >- الزامی است کلیه اطلاعات وارد شده بصورت حقیقی باشد، تمام مسئولیت ورود اطلاعات ناصحیح بر عهده شما میباشد.</p>
            <br />
            
            <label class="title">موبایل *</label>
            <asp:TextBox ID="txtMob" Width="230px"  ReadOnly="true" style="background: #FFF4F4;" ToolTip="غیرقابل تغییر" MaxLength="11" placeholder="09________" runat="server" CssClass="ltr"></asp:TextBox>
			 	
            <br />
            <label class="title">نام ونام خانوادگی*</label>
            <asp:TextBox ID="txtName" Width="230px" runat="server" MaxLength="50" 
                CssClass="farsiinput req"></asp:TextBox><br />
            
            <label class="title">ایمیل</label> 
            <asp:TextBox ID="txtMail" Width="230px" runat="server" MaxLength="100"  CssClass="ltr req"></asp:TextBox><br />
             

            
            <label class="title">جنسیت *</label>
            <asp:DropDownList ID="ddlGender" Width="230px" runat="server">
                <asp:ListItem Value="0" Text="انتخاب کنید"></asp:ListItem>
                <asp:ListItem Value="True" Text="آقــا"></asp:ListItem>
                <asp:ListItem Value="False" Text="خــانم"></asp:ListItem>
            </asp:DropDownList>
            <br /> 
            <label class="title">کد ملی *</label>
            <asp:TextBox ID="txtMeliCode" Width="230px" MaxLength="10" runat="server" CssClass="ltr"></asp:TextBox>
            <br />

         </div> 
            <div class="clear">
            </div>
            <br />
            <div class="center">
                <asp:Button ID="Button1" OnClick="Button1_Click" CssClass="btn-success btn" runat="server" Text="ذخیــره اطلاعات" />
            </div>
            <br />

        <div class="clear"></div>
    </div>
    <script src="/js/farsiInput.js"></script>
    <script>

        if ($(".farsiinput").length > 0)
            $(".farsiinput").farsiInput();
    </script>
</asp:Content>
