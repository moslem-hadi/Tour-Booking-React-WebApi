<%@ Page  Language="C#" MasterPageFile="~/member/membermaster.Master" AutoEventWireup="true" CodeBehind="changepass.aspx.cs" Inherits="CMS.member.changepass" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
<title>تغییر رمزر ورود</title>
        <script src="/component/bootstrap-sweetalert/sweetalert2.min.js"></script>
    <link href="../component/bootstrap-sweetalert/sweetalert2.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainFullContent" runat="server">



<div class="content">
      <div class="page-header">
                <h3>
                    <span>تغییر رمزر ورود</span></h3>
            </div>
 <div class="panel light-shadow white title-transparent rounded clearfix">
            
                <asp:Literal ID="ltr_error" runat="server"></asp:Literal>
             
            <label class="title"  >رمز فعلی</label>
            <asp:TextBox ID="txtCurr" Width="230px" runat="server" MaxLength="100" CssClass="req ltr" TextMode="Password"></asp:TextBox><br />
            
            <label class="title" >رمز جدید </label>
            <asp:TextBox ID="txtNew" Width="230px" blur="check();" runat="server"  MaxLength="100" CssClass="req ltr" TextMode="Password"></asp:TextBox><br />
<label class="title" >تکرار رمز جدید </label>
            <asp:TextBox ID="txtRep" Width="230px" runat="server" MaxLength="100" CssClass="req ltr" TextMode="Password"></asp:TextBox><br />
     <script>
         $(function () {
             $("#<%=txtNew.ClientID%>").on("blur", function () {
                 if ($("#<%=txtNew.ClientID%>").val().length < 7) {
                     swal({
                         type: 'error',
                         title: 'توجه',
                         text: 'پسورد جدید باید حداقل 7 حرف باشد.',
                         confirmButtonText: 'متوجه شدم!',
                         allowOutsideClick: false
                     })
                 }
             })
         })
     </script>
             <p class="text-muted">
                 رمز عبور بایستی حداقل 7 کاراکتر باشد و بهتر است بصورت ترکیبی از اعداد ، حروف (کوچک و بزرگ) و کاراکتر های خاص انتخاب شود.
                 
                 مثال:  L6s%U#jB9
             </p>
            <label class="title" ></label>
            <asp:Button ID="Button1"  CssClass="btn btn-success" OnClick="Button1_Click" runat="server" Text="ذخیــره" />
            <div class="clear">
            </div>

     </div>

       
     </div>
</asp:Content>
