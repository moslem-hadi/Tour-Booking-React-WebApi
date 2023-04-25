<%@ Page Language="C#" MasterPageFile="~/AdminPanel/managemaster.Master" AutoEventWireup="true" CodeBehind="Explore.aspx.cs" Inherits="CMS.AdminPanel.Explore" %>


<%@ Register TagPrefix="GleamTech" Namespace="GleamTech.FileUltimate" Assembly="GleamTech.FileUltimate" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <title>اکسپلورر</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ltr">
        <GleamTech:FileManager ID="fileManager" runat="server"
            Width="100%"
            Height="800" ArchiveBrowsingEnabled="true"   ShowFileExtensions="true"
            Resizable="True">

            <GleamTech:FileManagerRootFolder Name="Content" Location="~/content"  >
                <GleamTech:FileManagerAccessControl Path="\" AllowedPermissions="Full"  />
            </GleamTech:FileManagerRootFolder>


        </GleamTech:FileManager>
    </div>
</asp:Content>
