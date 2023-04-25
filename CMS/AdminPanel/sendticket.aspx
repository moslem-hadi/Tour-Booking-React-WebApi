<%@ Page  Language="C#" MasterPageFile="~/adminpanel/managemaster.Master" AutoEventWireup="true" CodeBehind="sendticket.aspx.cs" Inherits="CMS.Manage.sendticket" ValidateRequest="false" %>
<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<title>ارسال تیکت به کاربر</title>
    <script src="../component/Editor/nicEdit.js" type="text/javascript"></script>
    <script type="text/javascript">
        bkLib.onDomLoaded(function () {
            nicEditors.editors.push(
        new nicEditor({ maxHeight: 300 }).panelInstance(
            document.getElementById('<%=txtText.ClientID %>')
        )
    );
        }); 
    </script>
      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box">
        <div class="header">
            <h4>ارسال تیکت به کاربر</h4>
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
                            اولویت
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:DropDownList ID="DropDownList2" runat="server" Width="90">
                        
                    <asp:ListItem Value="High">خیلی زیاد</asp:ListItem>
                    <asp:ListItem Value="Medium" Selected="True">متوسط</asp:ListItem>
                    <asp:ListItem Value="Low">کم</asp:ListItem>
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
                            از بخش
                        </label>
                    </div>
                    <div class="span6 right">
                        
            <asp:DropDownList ID="DropDownList1"   runat="server">
            
                            <asp:ListItem>پشتیبانی کاربران</asp:ListItem>
                            <asp:ListItem>بخش مالی</asp:ListItem>
                            <asp:ListItem>تیم توسعه و طراحی</asp:ListItem>
                            <asp:ListItem>مدیریت سایت</asp:ListItem>
            </asp:DropDownList>
            
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>


                
                <div class="rowelement">
                    <div class="span2 right">
                        <label>به کاربر</label>
                    </div>
                    <div class="span6 right">
                        <asp:TextBox ID="txtusers" runat="server" TextMode="SingleLine" CssClass="ltr req"></asp:TextBox>
                       <p class="help" data-tip="کد کاربران. تفکیف با یکی از کاراکترهای: , ، - "></p>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div> 

                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            عنوان تیکت
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:TextBox ID="txtTitle" runat="server" MaxLength="100"  onkeyup="setshort(this.value)" CssClass="req"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div> 
                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            متن تیکت</label>
                    </div>
                    <div class="span6 right">
                        <div class="editor">
                            <textarea id="txtText" runat="server" class="niceditor"  cols="20" rows="2"></textarea>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div> 
                
                 
                 


                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            ضمیمه</label>
                    </div>
                    <div class="span6 right">
                          
                <asp:FileUpload ID="fluFile" runat="server" Width="150" />
                    
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>
                 
                 


                <div class="rowelement">
                    <div class="span2 right">
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
</asp:Content>
