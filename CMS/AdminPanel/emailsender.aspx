<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" CodeBehind="emailsender.aspx.cs" Inherits="CMS.Manage.emailsender" ValidateRequest="false" %>

<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
        <title>ارسال ایمیل</title>
  

     <script src="../component/Editor/nicEdit.js" type="text/javascript"></script>
    <script type="text/javascript">
        bkLib.onDomLoaded(function () {
            nicEditors.editors.push(
        new nicEditor({ maxHeight: 300 }).panelInstance(
            document.getElementById('<%=textarea1.ClientID %>')
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
            <h4>ارسال ایمیل</h4>
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
عنوان
                        </label>
                    </div>
                    <div class="span6 right">
                        
            <asp:TextBox ID="TextBox1" runat="server" MaxLength="100" Width="410"></asp:TextBox>
            <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator1" ValidationGroup="required"
                ControlToValidate="TextBox1" runat="server" ErrorMessage="×" Display="Dynamic"
                ToolTip="فیلد الزامی"></asp:RequiredFieldValidator>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div> 





  <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            ارسال به
                        </label>
                    </div>
                    <div class="span6 right">
                        
            <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" MaxLength="100" Height="100" Width="410" CssClass="ltr"></asp:TextBox>
            
            <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator2" ValidationGroup="required"
                ControlToValidate="TextBox2" runat="server" ErrorMessage="×" Display="Dynamic"
                ToolTip="فیلد الزامی"></asp:RequiredFieldValidator>


                        <br />
                
            <span>انتخاب</span>
            <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">همه</asp:LinkButton> &nbsp; |&nbsp; 
            <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click">خبرنامه ای</asp:LinkButton>
            &nbsp;&nbsp;
            <asp:Literal ID="Literal1" runat="server" ></asp:Literal>
      <br />
                        
          آدرس های ایمیل را با , یا | یا - یا اینتر از هم جدا کنید.
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div> 
                 




                
                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            ارسال با
                        </label>
                    </div>
                    <div class="span6 right">
                        نام ارسال کننده:
                        
                                    <asp:TextBox ID="txtName" Text="نفیس فایل" runat="server" MaxLength="100" CssClass="req" Width="130"></asp:TextBox>
                        <br />
           سرویس دهنده:  &nbsp;      <asp:TextBox ID="txtMailservice" Text="smtp.gmail.com" runat="server" MaxLength="100" CssClass="req ltr" Width="130" placeholder="mail.yoursite.com"></asp:TextBox>
                                    &nbsp;&nbsp;&nbsp;
                                    پورت:&nbsp;&nbsp;
                                    <asp:TextBox ID="txtMailserviceport" Text="587" runat="server" MaxLength="5"  CssClass="req ltr" placeholder="25" Width="29px"></asp:TextBox>
<br />
                         آدرس ایمیل:
                         &nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtEmail" Text="" runat="server" MaxLength="100" Width="130" CssClass="req ltr"></asp:TextBox>
                       &nbsp;&nbsp;&nbsp;&nbsp;  رمز:
                     &nbsp;&nbsp;&nbsp;    
                                    <asp:TextBox ID="txtMailPass" Text="" CssClass="req ltr" runat="server" Width="130" MaxLength="300"></asp:TextBox>
                              
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div> 





                
                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            زمانبندی
                        </label>
                    </div>
                    <div class="span6 right">
                            <asp:CheckBox ID="CheckBox2" runat="server" Text=" زمانبندی"  Checked="false"/>:     &nbsp;&nbsp;&nbsp;&nbsp; ارسال به      &nbsp;&nbsp;&nbsp;&nbsp;     &nbsp;&nbsp;
            <asp:TextBox ID="txtcnt" runat="server" Text="100" MaxLength="7" Width="70" CssClass="ltr"></asp:TextBox>&nbsp;&nbsp; نفر  &nbsp;در هر 
            &nbsp;
            <asp:TextBox ID="txtdelay" runat="server" MaxLength="7" Text="60" Width="70" CssClass="ltr"></asp:TextBox>&nbsp;&nbsp; دقیقه


                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div> 






                
                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            متن
                        </label>
                    </div>
                    <div class="span6 right">
                          <div class="editor">
                <textarea name="area1" class="myTextEditor" cols="35" runat="server" id="textarea1" style="width: 100%; height: 350px;"></textarea>

            </div>
            <asp:CheckBox ID="CheckBox1" runat="server" Text=" استفاده از قالب پیشفرض ایمیل"  Checked="true"/>
            
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
                        <asp:Button ID="Button1" runat="server" Text="ارسال" CssClass="btn btn-success"
                            OnClick="Button1_Click" />
                         
                        <a href="default.aspx" class="btn btn-warning" >انصراف</a>
                   
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <fieldset>
        </div>   

    </div>
</asp:Content>
