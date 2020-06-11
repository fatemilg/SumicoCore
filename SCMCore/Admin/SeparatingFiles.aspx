<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="SeparatingFiles.aspx.cs" Inherits="SCMCore.Admin.SeparatingFiles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button runat="server" CssClass="btn btn-Add" ID="btnCreateAllImageSizes" OnClick="btnCreateAllImageSizes_Click" Text="Create All ImageSizes" />
    <asp:Button runat="server" CssClass="btn btn-Add" ID="btnSeparate" OnClick="btnSeparate_Click" Text="Separate Files" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
</asp:Content>
