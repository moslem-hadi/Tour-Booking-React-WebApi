<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/managemaster.Master" AutoEventWireup="true" CodeBehind="settingVal.aspx.cs" Inherits="CMS.AdminPanel.settingVal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBarContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">



    
<div class="card-box  ">

    <table class="table table-striped table-bordered ltr">
        <tr>
            <th class="text-center" width="60">
                <label>ID</label>
            </th>
            <th class="text-center" width="50">
                <label>Load</label>
            </th>
            <th class="text-center" width="150">
                <label>Name Fa</label>
            </th>
            <th class="text-center" width="250">
                <label>Name</label>

            </th>
            <th class="text-center">
                <label>Value</label>
            </th>
        </tr>
        <%
        foreach (var item in list)
        {
             %>
            <tr>
                <td class="text-center">
                    <%= item.ID %>
                </td>
                <td style="font-size:18px;text-align:center;">
                    <%= bool.Parse(item.Hidden.ToString())? "<i class='fa fa-check'></i>" : "<i class='fa fa-times'></i>"%>
                    
                </td>
                <td>
                    <%= item.Short %>
                </td>
                <td>
                    <%= item.ShortValue %>
                </td>
                <td>
                    <span class="edit" data-id="<%=item.ID %>" data-name="<%=item.Short%>" data-namefa="<%=item.ShortValue %>" data-load="<%=item.Hidden %>"><i class="fa fa-edit"></i></span>
                    <%=  CMS.CommonFunctions.SubStringHtml(item.TextValue, 0, 200) %>
                </td>
            </tr>
       <% } %>

    </table>
</div>


<div class="card-box  " id="divToBeScrolledTo">

      <div class="form-group">
          
          <asp:Literal ID="ltrerror" runat="server"></asp:Literal>
            <label class="control-label col-md-2">اضافه / ویرایش</label>
            <div class="col-md-10">
                <input id="id" name="id" placeholder="ID" type="text" class="form-control inline ltr" style="width:80px" />
                <img src="~/Areas/Admin/Content/images/sp-loading.gif" id="imgload" style="display:none" />
                <span style="font-size:11px; color:#808080">0 یا خالی برای موقع افزودن</span>

                <br />
                <input id="title" name="title" placeholder="Name" type="text" class="form-control inline ltr m-t-10" style="width:250px" /><br />

                <input id="titleFa" name="titleFa" placeholder="NameFa - نام فارسی" type="text" class="form-control inline  m-t-10" style="width:250px" /><br />
                <textarea id="value" placeholder="Value" cols="5" name="value" class="form-control m-t-10 ltr"></textarea>
                <input type="checkbox" name="loadatfirst" id="loadatfirst" value="Yes"> <label for="coded">لود شدن در سینگلتون (LoadAtFirst)</label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input type="checkbox" name="coded" value="EnCode"> <label for="coded">کد شده (فقط موقع افزودن)</label>
                <br />
                <input value="ذخیره" type="submit" class="btn btn-success m-t-10" />
            </div>

        </div>
        <div class="clearfix"></div>
    
</div>

<style>
    .edit{
        cursor:pointer;font-size:16px;
    vertical-align: middle;
    margin-right:10px;
    }
</style>
@section scripts{
    <script>
        $(function () {
            
            $(".edit").click(function () {
                $("#id").val($(this).data("id"));
                $("#titleFa").val($(this).data("namefa"));
                $("#title").val($(this).data("name"));
                if ($(this).data("load") == "True")
                    $("#loadatfirst").prop("checked", true);
                $('html,body').animate({
                    scrollTop: $("#divToBeScrolledTo").offset().top
                });
                $("#imgload").show(); 

                $.ajax({
                    url: 'Action("getSettingValue","sysadmin")',
                    data: { "id": $(this).data("id") },
                    dataType: "json",
                    type: "POST",
                    success: function (result) {
                        $("#value").val(result);

                        $("#imgload").hide();
                    },
                    error: function (request, status, error) {
                        alert("Ajax Failure. " + error);
                    }


                })

            });

        });

    </script>
    
    }
</asp:Content>
