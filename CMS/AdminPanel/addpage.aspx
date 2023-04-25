<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Managemaster.Master" AutoEventWireup="true" ValidateRequest="false"
    CodeBehind="addpage.aspx.cs" Inherits="CMS.Manage.addpage" %>

<%@ Register Assembly="HRaz.MessageBox" Namespace="HRaz.MessageBox" TagPrefix="cc1" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<title>افزودن صفحه</title>
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
    <script src="js/urlcheck.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box">
        <div class="header">
            <h4>
                ایجاد صفحه جدید</h4>
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
                            عنوان صفحه
                        </label>
                    </div>
                    <div class="span6 right">
                        <asp:TextBox ID="txtTitle" runat="server" onkeyup="setshort(this.value)" MaxLength="100" CssClass="req"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>
                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            آدرس یکتای صفحه</label>
                    </div>
                    <div class="span6 right">
                        <asp:TextBox ID="txtShort" runat="server" MaxLength="200" Width="200" CssClass="ltr req" onkeyup="nospaces(this)"></asp:TextBox>
                        <p class="help" data-tip="شاخص یکتایی که برای آدرس دهی صفحه به کار می رود. به جای فاصله از - استفاده کنید.">
                        </p>
                         
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>
                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            متن صفحه</label>
                    </div> 
                    <div class="clear">
                    </div>
                </div>


              <div class="editor big">
                            <textarea id="txtText" runat="server" class="niceditor" cols="20" rows="2"></textarea>
                        </div>


                <div class="separator">
                </div> 
                <div class="rowelement">
                    <div class="span2 right">
                        <label>
                            توضیحات گوگل</label>
                    </div>
                    <div class="span6 right">
                        <asp:TextBox ID="txtDesc" runat="server" MaxLength="300"></asp:TextBox>
                        <p class="help" data-tip="توضیحاتی که برای موتورهای جستجو از اهمیت بالایی برخوردار است. حداکثر 150 حرف.">
                        </p>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="separator">
                </div>  
                <div class="separator">
                </div>
                <div class="rowelement">
                    <div class="span2 right">
                    </div>
                    <div class="span6 right">
                        <asp:Button ID="Button1" runat="server" Text="ذخـــیره" CssClass="btn btn-success"
                            OnClick="Button1_Click" />
                        <a href="/AdminPanel/pages" class="btn btn-warning">انصراف</a>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
