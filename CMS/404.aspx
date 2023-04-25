<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="CMS._404" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <title>آنچه میخواهید، پیدا نشد!</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

 <div class="container">
						
			<div class="row">
				<div class="col-md-12">
					<div class="row block404">
						<div class="col-md-4">
							<h1 class="error">404</h1>
						</div>
						<div class="col-md-8">
							<h1 class="title">صفحه مورد نظر پیدا نشد</h1>
							<p class="description">
								صفحه مورد نظر شما پیدا نشد. ممکن است آدرس را اشتباه وارد کرده باشید، یا صفحه حذف شده باشد. <br />
                               <br />  <br /> <a href="/">مراجعه به صفحه اصلی سایت</a>&nbsp;&nbsp;
                                |&nbsp;&nbsp;
                                <a href="/contactus">تماس با ما</a>
							</p>
						</div>
					</div>
				</div>
			</div> <!-- .rw-row -->

		</div> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScript" runat="server">
</asp:Content>
