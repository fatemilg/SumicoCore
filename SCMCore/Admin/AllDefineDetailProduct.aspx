<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AllDefineDetailProduct.aspx.cs" Inherits="SCMCore.Admin.AllDefineDetailProduct" %>

<%@ Register Src="~/Admin/UserControl/DefineDetailProduct.ascx" TagPrefix="uc1" TagName="DefineDetailProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:DefineDetailProduct runat="server" ID="DefineDetailProduct" />
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="btn-group-justified" dir="rtl">
                <asp:Button runat="server" ID="btnGetAll" Text="همه ترکیب های ثبت شده" CssClass="btn btn-success" OnClick="btnGetAll_Click" style="width:200px;" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
</asp:Content>
