<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="DefineGroup.aspx.cs" Inherits="SCMCore.Admin.DefineGroup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Admin/UserControl/PersonelAutoComplete.ascx" TagPrefix="uc1" TagName="PersonelAutoComplete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfIdGroup" runat="server" />

            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <section class="panel" dir="rtl">
                    <header class="panel-heading">
                        <span style="font-size: 20px;">لیست گروه ها
                        </span>
                    </header>

                    <div class="panel-body">
                        <section id="unseen">
                            <asp:TreeView ID="tvGroup" runat="server" ExpandDepth="0" NodeIndent="15" CollapseImageUrl="images/details_close.png" ExpandImageUrl="images/details_open.png"
                                OnSelectedNodeChanged="tvGroup_SelectedNodeChanged" SelectAction="None">
                                <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                <NodeStyle Font-Size="12pt" ForeColor="Black" HorizontalPadding="2px"
                                    NodeSpacing="0px" VerticalPadding="2px" ImageUrl="images/arrow-left.png"></NodeStyle>
                                <ParentNodeStyle Font-Bold="False" />
                                <SelectedNodeStyle HorizontalPadding="0px" VerticalPadding="0px" BackColor="#B5B5B5" Font-Underline="False"></SelectedNodeStyle>
                                <SelectedNodeStyle />
                            </asp:TreeView>
                        </section>
                    </div>
                </section>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="modal fade" id="ModalGroupEvents" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content" dir="rtl">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">گروه 
                            <asp:Label ID="lblModalGroupName" runat="server" ForeColor="Red"></asp:Label></h4>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="modal-body row" dir="rtl">
                    <section class="panel">
                        <header class="panel-heading tab-bg-dark-navy-blue tab-right ">
                            <ul class="nav nav-tabs pull-right" style="margin: 0">
                                <li class="">
                                    <a data-toggle="tab" href="#EditGroup" dir="ltr">ویرایش گروه
                                                <i class="fa fa-bars"></i>
                                    </a>
                                </li>
                                <li class="">
                                    <a data-toggle="tab" href="#SubGroup" dir="ltr">ایجاد زیر گروه 
                                                 <i class="fa fa-bars"></i>
                                    </a>
                                </li>
                                <li class="">
                                    <a data-toggle="tab" href="#UserGroup" dir="ltr">کاربران گروه  
                                                <i class="fa fa-bars"></i>
                                    </a>
                                </li>
                            </ul>
                        </header>
                        <div class="panel-body">
                            <div class="tab-content">
                                <div id="UserGroup" class="tab-pane">
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                            <section class="panel" dir="rtl">
                                                <div class="btn-group-justified" dir="rtl">
                                                    <asp:Button ID="btnNewUSerGroup" CssClass="btn btn-New" runat="server" Text="جدید" Style="float: right" UseSubmitBehavior="false" Width="80px" OnClick="btnNewUSerGroup_Click" />
                                                    <asp:Button ID="btnAddUserGroup" CssClass="btn btn-Add" ValidationGroup="submitUserGroup" Style="float: right" runat="server" Text="ثبت" OnClick="btnAddUserGroup_Click" Width="80px" UseSubmitBehavior="false" />
                                                </div>
                                                <div class="panel-body">
                                                    <div class="form-group col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                        <label for="txtUserFullName">نام کاربر :</label>
                                                        <uc1:PersonelAutoComplete runat="server" ID="PersonelAutoComplete" />
                                                    </div>
                                                </div>
                                            </section>
                                            <section class="panel" dir="rtl">
                                                <asp:GridView ID="grdUserGroup" runat="server" CssClass="display table table-bordered table-striped dataTable" AutoGenerateColumns="false"
                                                    DataKeyNames="IDUserGroup" AllowPaging="true" OnRowCommand="grdUserGroup_RowCommand" OnRowEditing="grdUserGroup_RowEditing"
                                                    OnRowDeleting="grdUserGroup_RowDeleting" OnPageIndexChanging="grdUserGroup_PageIndexChanging">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="عملیات" HeaderStyle-Width="4%">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnDelete" CssClass="btn btn-Delete tool" data-placement="top" title="حذف" runat="server" CommandName="Del" CommandArgument='<%#Eval("IDUserGroup")%>' UseSubmitBehavior="false" />
                                                                <ajaxToolkit:ConfirmButtonExtender ID="cbeDelete" TargetControlID="btnDelete" ConfirmText="آیا مطمئن هستید؟" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="نام کاربر">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblFullName" Text='<%#Eval("FullName")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="اولویت" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtSort" Text='<%#Eval("Sort")%>' AutoPostBack="true" OnTextChanged="txtSort_TextChanged" style="text-align:center" MaxLength="2" />
                                                                <asp:HiddenField runat="server" ID="hfIDUserGroup" value='<%#Eval("IDUserGroup")%>' />

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
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div id="SubGroup" class="tab-pane">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="form-group col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <br />
                                                <asp:Button ID="btnAddSubGroup" CssClass="btn btn-Add" ValidationGroup="submitSubGroup" Style="float: right" runat="server" Text="ثبت" OnClick="btnAddSubGroup_Click" Width="80px" />
                                            </div>
                                            <div class="form-group col-lg-6 col-md-12 col-sm-12 col-xs-12" style="float: right">
                                                <label for="txtGroupTitle">عنوان :</label>
                                                <asp:TextBox ID="txtGroupTitle" CssClass="form-control" runat="server" MaxLength="200" Width="100%"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="عنوان را وارد کنید" Font-Size="Small" ForeColor="red" ControlToValidate="txtGroupTitle" ValidationGroup="submitSubGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                            <div id="divGroupType" runat="server" class="form-group col-lg-6 col-md-12 col-sm-12 col-xs-12" style="float: right">
                                                <label>نوع گروه :</label>
                                                <asp:DropDownList ID="drpGroupType" runat="server" class=" form-control m-bot15"></asp:DropDownList>
                                            </div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div id="EditGroup" class="tab-pane">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <div class="form-group col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <br />
                                                <asp:Button ID="btnEditGroup" CssClass="btn btn-Add" ValidationGroup="submitEditGroup" Style="float: right" runat="server" Text="ویرایش" OnClick="btnEditGroup_Click" Width="80px" />
                                                <asp:Button ID="btnDeleteGroup" CssClass="btn btn-danger" Style="float: right" runat="server" Text="حذف" OnClick="btnDeleteGroup_Click" Width="80px" />
                                            </div>
                                            <div class="form-group col-lg-6 col-md-12 col-sm-12 col-xs-12" style="float: right">
                                                <label for="txtGroupTitle">عنوان :</label>
                                                <asp:TextBox ID="txtGroupEditTitle" CssClass="form-control" runat="server" MaxLength="200" Width="100%"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="عنوان را وارد کنید" Font-Size="Small" ForeColor="red" ControlToValidate="txtGroupEditTitle" ValidationGroup="submitEditGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                            <div id="divGroupTypeEdit" runat="server" class="form-group col-lg-6 col-md-12 col-sm-12 col-xs-12" style="float: right">
                                                <label>نوع گروه :</label>
                                                <asp:DropDownList ID="drpGroupTypeEdit" runat="server" class=" form-control m-bot15"></asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <label for="tvEditGroup">گروه بالادست: </label>
                                                <asp:TreeView ID="tvEditGroup" runat="server" ImageSet="BulletedList" ExpandDepth="0" NodeIndent="15"
                                                    SelectAction="None">
                                                    <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                                    <NodeStyle Font-Size="12pt" ForeColor="Black" HorizontalPadding="2px"
                                                        NodeSpacing="0px" VerticalPadding="2px"></NodeStyle>
                                                    <ParentNodeStyle Font-Bold="False" />
                                                    <SelectedNodeStyle HorizontalPadding="0px" VerticalPadding="0px" BackColor="#B5B5B5" Font-Underline="False"></SelectedNodeStyle>
                                                    <SelectedNodeStyle />
                                                </asp:TreeView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </section>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
</asp:Content>
