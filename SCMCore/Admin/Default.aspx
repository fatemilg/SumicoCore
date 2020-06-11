<%@ Page Title="صفحه اصلی" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SCMCore.Admin.Default" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Admin/UserControl/DatePicker.ascx" TagPrefix="uc1" TagName="DatePicker" %>
<%@ Register Src="~/Admin/UserControl/TreeProduct.ascx" TagPrefix="uc1" TagName="TreeProduct" %>
<%@ Register Src="~/Admin/UserControl/TreeAccessory.ascx" TagPrefix="uc1" TagName="TreeAccessory" %>




<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="SCMCore.ExtensionMethod" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12">
        <div class="profile-nav alt">
            <section class="panel" style="border-radius: 30px; height: auto;">
                <div class="user-heading alt clock-row terques-bg" style="height: auto; border-radius: 30px; min-height: 0">
                    <h1 class="text-center"><%=DateTime.Now.ToShamsiDateString() %></h1>
                    <h1 class="text-center">
                        <asp:Label ID="lblClock" runat="server"></asp:Label></h1>
                </div>
            </section>
        </div>
    </div>
    <div class="col-md-12">

        <asp:UpdatePanel runat="server" ID="up1">
            <ContentTemplate>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script>
        setInterval(function () {
            $.post(
                   "getServerDate.ashx",
                   function (result) {
                       document.getElementById("<%=lblClock.ClientID %>").textContent = result;
                   }
               );
        }, 1000);
    </script>
</asp:Content>
