<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Accessory.aspx.cs" Inherits="SCMCore.Admin.Accessory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Admin/UserControl/TreeDropDown.ascx" TagPrefix="uc1" TagName="TreeDropDown" %>
<%@ Register Src="~/Admin/UserControl/DetailAssignProperty.ascx" TagPrefix="uc1" TagName="DetailAssignProperty" %>
<%@ Register Src="~/Admin/UserControl/DefineDetailProduct.ascx" TagPrefix="uc1" TagName="DefineDetailProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>مدیریت لوازم جانبی</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfModeAccessoryCategory" runat="server" />
            <asp:HiddenField ID="hfIDAccessoryCategory" runat="server" />
            <div class="col-lg-6" >
                <section class="panel" dir="rtl">
                    <header class="panel-heading">
                        <span>گروه بندی لوازم جانبی شرکت
                                <asp:Label ID="lblSupplierNameList" runat="server" ForeColor="Red"></asp:Label>
                        </span>
                    </header>
                    <div class="panel-body">
                        <section id="unseen2">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:TreeView ID="tvAccessoryCategory" runat="server" ExpandDepth="0" NodeIndent="15" CollapseImageUrl="images/details_close.png" ExpandImageUrl="images/details_open.png"
                                        SelectAction="None" OnSelectedNodeChanged="tvAccessoryCategory_SelectedNodeChanged">
                                        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                        <NodeStyle Font-Size="14px" ForeColor="Black" HorizontalPadding="2px"
                                            NodeSpacing="0px" VerticalPadding="2px" ImageUrl="images/arrow-left.png"></NodeStyle>
                                         <ParentNodeStyle Font-Bold="True" Font-Size="14px" ForeColor="Red" />
                                        <SelectedNodeStyle HorizontalPadding="0px" VerticalPadding="0px" BackColor="#B5B5B5" Font-Underline="False"></SelectedNodeStyle>
                                        <SelectedNodeStyle />
                                    </asp:TreeView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </section>
                    </div>
                </section>
            </div>

            <div class="col-lg-6" >
                <section class="panel" dir="rtl">
                    <header class="panel-heading">
                        <span>دسته جدید لوازم جانبی
                             <asp:Label ID="lblSupplierNameNew" runat="server" ForeColor="Red"></asp:Label>
                        </span>
                    </header>
                    <div class="btn-group-justified" dir="rtl">
                        <asp:Button ID="btnNewAccessoryCategory" class="btn btn-New" runat="server" Text="دسته بندی جدید" Style="float: right" UseSubmitBehavior="false" Width="120px" OnClick="btnNewAccessoryCategory_Click" />
                        <asp:Button ID="btnAddAccessoryCategory" class="btn btn-Add" ValidationGroup="submitAccessory" Style="float: right" runat="server" Text="ثبت دسته بندی" Width="120px" OnClick="btnAddAccessoryCategory_Click" />
                        <asp:Button ID="btnReturnSupplier" class="btn btn-Grid" runat="server" Text="لیست تامین کنندگان" UseSubmitBehavior="false" Width="180px" OnClick="btnReturnSupplier_Click" />
                    </div>
                    <div class="panel-body">
                        <div class="col-md-6 form-group " runat="server">
                            <label for="LName">عنوان گروه لوازم جانبی(En) :</label>
                            <asp:TextBox ID="txtAccessoryName_En" CssClass=" form-control" runat="server" MaxLength="300" Width="100%"></asp:TextBox>
                        </div>
                        <div class="col-md-6 form-group">
                            <label for="FName">عنوان گروه لوازم جانبی(Fa) :</label>
                            <asp:TextBox ID="txtAccessoryName_Fa" CssClass=" form-control" runat="server" MaxLength="300" Width="100%"></asp:TextBox>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <label for="TreeDropDownEdit">گروه بالاتر:</label><br />
                            <uc1:TreeDropDown runat="server" ID="TreeDropDownEdit" />
                        </div>

                        <div class="col-md-6 form-group ">
                            <label for="Sort">ترتیب برای نمایش :</label>
                            <asp:TextBox ID="txtSort" class=" form-control" runat="server" TextMode="Number" Width="100%"></asp:TextBox>
                        </div>

                    </div>
                </section>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <div class="modal fade" id="ModalNavigationOnAccessory" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="z-index: 1503">
        <div class="modal-dialog" style="width: 250px;">
            <div class="modal-content" dir="rtl">
                <asp:UpdatePanel ID="UpdatePanel115" runat="server">
                    <ContentTemplate>
                        <br />
                        <asp:Label ID="lblNodeName" runat="server" Style="text-align: center; margin-top: 5px; font-weight: bold"></asp:Label>
                        <div class="panel-body">
                            <asp:Button ID="btnDelAccessoryCategory" CssClass="btn btn-Delete tool" ToolTip="حذف" Style="margin-left: 20px;" data-placement="bottom"  runat="server" OnClientClick="return confirm(' آیا مطمئن هستید ؟ ')" OnClick="btnDelAccessoryCategory_Click" />
                            <asp:Button ID="btnEditAccessoryCategory" CssClass="btn btn-edit tool" ToolTip="ویرایش" Style="margin-left: 20px;" runat="server" OnClick="btnEditAccessoryCategory_Click" UseSubmitBehavior="false" />
                            <asp:Button ID="btnPropertyAssignAccessoryCategory" class="btn btn-Assign tool" runat="server" title="اختصاص ویژگی  به این لوازم جانبی" data-placement="bottom" OnClick="btnPropertyAssignAccessoryCategory_Click" />
                            <asp:Button ID="btnAddAccessoryInTree" class="btn btn-Accessory tool" ValidationGroup="submit" runat="server" OnClick="btnAddAccessoryInTree_Click" Width="80px" title="ثبت لوازم جانبی برای این دسته" data-placement="bottom" />

                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <uc1:DetailAssignProperty runat="server" ID="DetailAssignProperty" />
    <uc1:DefineDetailProduct runat="server" ID="DefineDetailProduct" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
</asp:Content>
