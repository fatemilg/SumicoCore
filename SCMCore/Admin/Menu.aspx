<%@ Page Title="مدیریت منو" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="SCMCore.Admin.Menu" EnableEventValidation="false" %>

<%@ Import Namespace="SCMCore.ExtensionMethod" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfIdMenu" runat="server" />

            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <section class="panel" dir="rtl">
                    <header class="panel-heading">
                        <span style="font-size: 20px;">لیست منوها
                        </span>
                    </header>

                    <div class="panel-body">
                        <section id="unseen">
                            <asp:TreeView ID="tvMenu" runat="server" ExpandDepth="0" NodeIndent="15" CollapseImageUrl="images/details_close.png" ExpandImageUrl="images/details_open.png"
                                OnSelectedNodeChanged="tvMenu_SelectedNodeChanged" SelectAction="None">
                                <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                <NodeStyle Font-Size="12pt" ForeColor="Black" HorizontalPadding="2px"
                                    NodeSpacing="0px" VerticalPadding="2px" ImageUrl="images/arrow-left.png"></NodeStyle>
                                <ParentNodeStyle Font-Bold="False" ForeColor="Red" />
                                <SelectedNodeStyle HorizontalPadding="0px" VerticalPadding="0px" BackColor="#B5B5B5" Font-Underline="False"></SelectedNodeStyle>
                                <SelectedNodeStyle />
                            </asp:TreeView>
                        </section>
                    </div>
                </section>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="modal fade" id="ModalMenuEvents" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content" dir="rtl">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">منوی 
                            <asp:Label ID="lblModalMenuName" runat="server" ForeColor="Red"></asp:Label></h4>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="modal-body row" dir="rtl">
                    <section class="panel">
                        <header class="panel-heading tab-bg-dark-navy-blue tab-right ">
                            <ul class="nav nav-tabs pull-right" style="margin: 0">
                                <li class="">
                                    <a data-toggle="tab" href="#EditMenu" dir="ltr">ویرایش منو
                                                <i class="fa fa-bars"></i>
                                    </a>
                                </li>
                                <li class="">
                                    <a data-toggle="tab" href="#AddMenu" dir="ltr">زیر منو جدید  
                                                <i class="fa fa-bars"></i>
                                    </a>
                                </li>
                            </ul>
                        </header>
                        <div class="panel-body">
                            <div class="tab-content">
                                <div id="AddMenu" class="tab-pane">
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                            <section class="panel" dir="rtl">
                                                <div class="btn-group-justified" dir="rtl">
                                                    <asp:Button ID="btnNew" class="btn btn-New" runat="server" Text="جدید" Style="float: right" UseSubmitBehavior="false" Width="80px" OnClick="btnNew_Click" />
                                                    <asp:Button ID="btnAdd" class="btn btn-Add" ValidationGroup="submit" Style="float: right" runat="server" Text="ثبت" OnClick="btnAdd_Click" Width="80px" />
                                                </div>
                                                <div class="panel-body">

                                                    <div class="col-lg-6 col-md-6">
                                                        <label>نام منو(انگلیسی) :</label>
                                                        <asp:TextBox ID="txtName_En" class=" form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6">
                                                        <label>نام منو(فارسی) :</label>
                                                        <asp:TextBox ID="txtName_Fa" class=" form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="نام منو را وارد کنید" Font-Size="Small" ForeColor="red" ControlToValidate="txtName_Fa" ValidationGroup="submit" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6">
                                                        <label>آدرس دهی :</label>
                                                        <asp:TextBox ID="txtMenuUrl" class=" form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6">
                                                        <label>ترتیب برای نمایش :</label>
                                                        <asp:TextBox ID="txtOrder" class=" form-control" runat="server" TextMode="Number"></asp:TextBox>
                                                    </div>
                                                    <div class="clearfix"></div>
                                                    <div class="col-lg-6 col-md-6" style="float:right">
                                                        <label>نمایش منو : </label>
                                                        <br />
                                                        <asp:CheckBox ID="chkActive" runat="server" Checked="true" Text="نمایش داده شود؟" />
                                                    </div>
                                                    <div class="clearfix"></div>
                                                </div>
                                            </section>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div id="EditMenu" class="tab-pane">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <section class="panel" dir="rtl">
                                                <div class="btn-group-justified" dir="rtl">
                                                    <asp:Button ID="btnEditMenu" CssClass="btn btn-Add" ValidationGroup="submitEditMenu" Style="float: right" runat="server" Text="ویرایش" OnClick="btnEditMenu_Click" Width="80px" />
                                                    <asp:Button ID="btnDeleteMenu" CssClass="btn btn-danger" Style="float: right" runat="server" Text="حذف" OnClick="btnDeleteMenu_Click" Width="80px" />
                                                </div>
                                                <div class="panel-body">
                                                    <div class="col-lg-6 col-md-6" style="float: right">
                                                        <label>نمایش منو : </label>
                                                        <br />
                                                        <asp:CheckBox ID="chkEditActive" runat="server" Checked="true" Text="نمایش داده شود؟" />
                                                    </div>
                                                    <div class="clearfix"></div>
                                                    <div class="col-lg-6 col-md-6">
                                                        <label>نام منو(انگلیسی) :</label>
                                                        <asp:TextBox ID="txtEditName_En" class=" form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6">
                                                        <label>نام منو(فارسی) :</label>
                                                        <asp:TextBox ID="txtEditName_Fa" class=" form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="نام منو را وارد کنید" Font-Size="Small" ForeColor="red" ControlToValidate="txtEditName_Fa" ValidationGroup="submitEditMenu" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6">
                                                        <label>آدرس دهی :</label>
                                                        <asp:TextBox ID="txtEditMenuUrl" class=" form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6">
                                                        <label>ترتیب برای نمایش :</label>
                                                        <asp:TextBox ID="txtEditOrder" class=" form-control" runat="server" TextMode="Number"></asp:TextBox>
                                                    </div>
                                                    <br />

                                                    <div class="form-group col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                        <label for="tvEditMenu">گروه بالادست: </label>
                                                        <asp:TreeView ID="tvEditMenu" runat="server" ImageSet="BulletedList" ExpandDepth="0" NodeIndent="15"
                                                            SelectAction="None">
                                                            <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                                            <NodeStyle Font-Size="12pt" ForeColor="Black" HorizontalPadding="2px"
                                                                NodeSpacing="0px" VerticalPadding="2px"></NodeStyle>
                                                            <ParentNodeStyle Font-Bold="False" ForeColor="Blue" />
                                                            <SelectedNodeStyle HorizontalPadding="0px" VerticalPadding="0px" BackColor="#B5B5B5" Font-Underline="False"></SelectedNodeStyle>
                                                            <SelectedNodeStyle />
                                                        </asp:TreeView>
                                                    </div>
                                                </div>
                                            </section>
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
