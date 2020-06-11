<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="DefineRole.aspx.cs" Inherits="SCMCore.Admin.DefineRole" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Admin/UserControl/PersonelAutoComplete.ascx" TagPrefix="uc1" TagName="PersonelAutoComplete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfMode" runat="server" />
            <asp:HiddenField ID="hfIdRole" runat="server" />
            <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12" dir="rtl" style="float: right;">
                <section class="panel" dir="rtl">
                    <header class="panel-heading">
                        <span style="font-size: 20px;">تعریف نقش کاربران
                        </span>
                    </header>
                    <div class="btn-group-justified" dir="rtl">
                        <asp:Button ID="btnNew" CssClass="btn btn-New" runat="server" Text="جدید" Style="float: right" UseSubmitBehavior="false" Width="80px" OnClick="btnNew_Click" />
                        <asp:Button ID="btnAdd" CssClass="btn btn-Add" ValidationGroup="submit" Style="float: right" runat="server" Text="ثبت" OnClick="btnAdd_Click" Width="80px" />
                    </div>
                    <div class="panel-body">
                        <div class="form-group  col-md-6 col-sm-12 col-xs-12" dir="rtl">
                            <label for="txtTitle">عنوان :</label>
                            <asp:TextBox ID="txtTitle" CssClass="form-control" runat="server" MaxLength="200" Width="100%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="عنوان را وارد کنید" Font-Size="Small" ForeColor="red" ControlToValidate="txtTitle" ValidationGroup="submit" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </section>
            </div>
            <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12" dir="rtl">
                <section class="panel" dir="rtl">
                    <header class="panel-heading">
                        <span style="font-size: 20px;">لیست نقش ها
                        </span>
                    </header>
                    <div dir="rtl">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="txtSearch" placeholder="جستجو"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btnSearch" OnClick="btnSearch_Click" />
                    </div>
                    <div class="panel-body">
                        <section id="unseen">
                            <asp:GridView ID="grdRole" runat="server" CssClass="display table table-bordered table-striped dataTable" AutoGenerateColumns="false"
                                DataKeyNames="IDRole" PageSize="10" AllowPaging="true" OnRowCommand="grdRole_RowCommand" OnRowEditing="grdRole_RowEditing" OnRowDeleting="grdRole_RowDeleting" OnPageIndexChanging="grdRole_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="عملیات" HeaderStyle-Width="25%">
                                        <ItemTemplate>
                                            <ajaxToolkit:ConfirmButtonExtender ID="cbeDelete" TargetControlID="btnDelete" ConfirmText="آیا مطمئن هستید؟" runat="server" />
                                            <asp:Button ID="btnDelete" CssClass="btn btn-Delete tool" data-placement="top" title="حذف " runat="server" CommandName="Del" CommandArgument='<%#Eval("IDRole")%>' UseSubmitBehavior="false" />
                                            <asp:Button ID="btnEdit" CssClass="btn btn-edit tool" data-placement="top" title="ویرایش" runat="server" CommandName="Edit" CommandArgument='<%#Eval("IDRole")%>' UseSubmitBehavior="false" />
                                            <asp:Button ID="btnUSers" CssClass="btn btn-Member tool" data-placement="top" title="کاربران این نقش" runat="server" CommandName="Users" CommandArgument='<%#Eval("IDRole")%>' UseSubmitBehavior="false" />
                                            <asp:Button ID="btnMenu" CssClass="btn btn-Menu tool" data-placement="top" title="منو های این نقش" runat="server" CommandName="Menu" CommandArgument='<%#Eval("IDRole")%>' UseSubmitBehavior="false" Visible='<%#ShowMenuRole() %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="عنوان" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Title" runat="server" Text=' <%#Eval("Title") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div>
                                        اطلاعاتی برای نمایش موجود نیست
                                    </div>
                                </EmptyDataTemplate>
                                <PagerStyle CssClass="gridview" />
                            </asp:GridView>
                        </section>
                    </div>
                </section>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="modal fade" id="ModalUsers" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content" dir="rtl">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" dir="rtl">کاربران نقش 
                            <asp:Label ID="lblRoleForUser" runat="server" ForeColor="Red"></asp:Label></h4>
                        </div>
                        <div class="modal-body row">
                            <section class="panel" dir="rtl">
                                <div class="btn-group-justified" dir="rtl">
                                    <asp:Button ID="btnNewUSerRole" CssClass="btn btn-New" runat="server" Text="جدید" Style="float: right" UseSubmitBehavior="false" Width="80px" OnClick="btnNewUSerRole_Click" />
                                    <asp:Button ID="btnAddUserRole" CssClass="btn btn-Add" ValidationGroup="submitUserRole" Style="float: right" runat="server" Text="ثبت" OnClick="btnAddUserRole_Click" Width="80px" UseSubmitBehavior="false" />
                                </div>
                                <div class="panel-body">
                                    <div class="form-group col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <label for="txtUserFullName">نام کاربر :</label>
                                        <uc1:PersonelAutoComplete runat="server" ID="PersonelAutoComplete" />
                                    </div>
                                </div>
                            </section>
                            <section class="panel" dir="rtl">
                                <asp:GridView ID="grdUserRole" runat="server" CssClass="display table table-bordered table-striped dataTable" AutoGenerateColumns="false"
                                    DataKeyNames="IDUserRole" AllowPaging="true" OnRowCommand="grdUserRole_RowCommand" OnRowEditing="grdUserRole_RowEditing"
                                    OnRowDeleting="grdUserRole_RowDeleting" OnPageIndexChanging="grdUserRole_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="عملیات" HeaderStyle-Width="4%">
                                            <ItemTemplate>
                                                <asp:Button ID="btnDelete" CssClass="btn btn-Delete tool" data-placement="top" title="حذف" runat="server" CommandName="Del" CommandArgument='<%#Eval("IDUserRole")%>' UseSubmitBehavior="false" />
                                                <ajaxToolkit:ConfirmButtonExtender ID="cbeDelete" TargetControlID="btnDelete" ConfirmText="آیا مطمئن هستید؟" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="نام کاربر">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblFullName" Text='<%#Eval("FullName")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div>
                                            اطلاعاتی برای نمایش موجود نیست
                                        </div>
                                    </EmptyDataTemplate>
                                    <PagerStyle CssClass="gridview" />
                                </asp:GridView>
                            </section>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="modal fade" id="ModalMenu" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content" dir="rtl">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" dir="rtl">انتساب منو  به نقش 
                            <asp:Label ID="lblRoleFormenu" runat="server" ForeColor="Red"></asp:Label></h4>
                        </div>
                        <div class="modal-body row">
                            <section class="panel" dir="rtl">
                                <div style="overflow-y: scroll; height: 650px">
                                    <asp:TreeView ID="tvMenu" runat="server" ImageSet="BulletedList" ExpandDepth="0" NodeIndent="15"
                                        OnSelectedNodeChanged="tvMenu_SelectedNodeChanged" SelectAction="None">
                                        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                        <NodeStyle Font-Size="10pt" ForeColor="Black" HorizontalPadding="2px"
                                            NodeSpacing="0px" VerticalPadding="2px"></NodeStyle>
                                        <ParentNodeStyle Font-Bold="False" ForeColor="Red" />
                                        <SelectedNodeStyle HorizontalPadding="0px" VerticalPadding="0px" BackColor="#B5B5B5" Font-Underline="False"></SelectedNodeStyle>
                                        <SelectedNodeStyle />
                                    </asp:TreeView>
                                </div>
                            </section>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
</asp:Content>
