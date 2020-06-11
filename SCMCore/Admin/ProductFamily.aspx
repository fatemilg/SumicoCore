<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ProductFamily.aspx.cs" Inherits="SCMCore.Admin.ProductFamily" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Import Namespace="SCMCore.ExtensionMethod" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>خانواده محصولات</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfModeProductFamily" runat="server" />
            <asp:HiddenField ID="hfIDProductFamily" runat="server" />
            <div class="col-lg-12" id="divInfo" runat="server">
                <section class="panel" dir="rtl">
                    <header class="panel-heading">
                        <span style="font-size: 20px;">ثبت خانواده محصول
                        </span>
                    </header>
                    <div class="btn-group-justified" dir="rtl">
                        <asp:Button ID="btnNewProductFamily" class="btn btn-New" runat="server" Text="جدید" Style="float: right" UseSubmitBehavior="false" Width="80px" OnClick="btnNewProductFamily_Click" />
                        <asp:Button ID="btnAddProductFamily" class="btn btn-Add" ValidationGroup="submitProductFamily" Style="float: right" runat="server" Text="ثبت" Width="80px" OnClick="btnAddProductFamily_Click" />
                    </div>
                    <div class="panel-body">
                        <div class="position-center">
                            <div class="form-group col-md-6">
                                <label for="Name">عنوان خانواده محصول(Fa) :</label>
                                <asp:TextBox ID="txtName_Fa" class=" form-control" runat="server" MaxLength="100" Style="width: 100%"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-6">
                                <label for="Name">عنوان  خانواده محصول (En) :</label>
                                <asp:TextBox ID="txtName_En" class=" form-control" runat="server" MaxLength="100" Style="width: 100%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="نام  خانواده محصول(En) را وارد کنید" Font-Size="Small" ForeColor="red" ControlToValidate="txtName_En" ValidationGroup="submitProductFamily" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-12">
                                <label for="Name">توضیحات (Fa) :</label>
                                <asp:TextBox ID="txtDescription_Fa" class=" form-control" runat="server" TextMode="MultiLine" Style="width: 100%"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-12">
                                <label for="Name">توضیحات (En) :</label>
                                <asp:TextBox ID="txtDescription_En" class=" form-control" runat="server" TextMode="MultiLine" Style="width: 100%"></asp:TextBox>
                            </div>

                        </div>
                    </div>
                </section>
            </div>
            <div class="col-lg-12" id="divTable" runat="server">
                <section class="panel" dir="rtl">
                    <header class="panel-heading">
                        <span style="font-size: 20px;">لیست  خانواده محصولات
                        </span>
                    </header>
                    <div class="panel-body">
                        <section id="unseen">
                            <asp:GridView ID="grdProductFamily" runat="server" CssClass="table table-bordered table-striped table-condensed" AutoGenerateColumns="false"
                                DataKeyNames="IDProductFamily" PageSize="10" AllowPaging="true" OnRowCommand="grdProductFamily_RowCommand" OnRowEditing="grdProductFamily_RowEditing" OnRowDeleting="grdProductFamily_RowDeleting" OnPageIndexChanging="grdProductFamily_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="عملیات" HeaderStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Button ID="btnDel" CssClass="btn btn-Delete" ToolTip="حذف" runat="server" CommandName="Delete" CommandArgument='<%#Eval("IDProductFamily")%>' UseSubmitBehavior="false" />
                                            <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender33" TargetControlID="btnDel" ConfirmText=" آیا مطمئن هستید؟" runat="server" />
                                            <asp:Button ID="btnEdit" CssClass="btn btn-edit" ToolTip="ویرایش" runat="server" CommandName="Edit" CommandArgument='<%#Eval("IDProductFamily")%>' UseSubmitBehavior="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="عنوان خانواده محصول">
                                        <ItemTemplate>
                                            <asp:Label ID="Name_Fa" runat="server" Text=' <%#Eval("Name_Fa") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="عنوان انگلیسی خانواده محصول">
                                        <ItemTemplate>
                                            <asp:Label ID="Name_En" runat="server" Text=' <%#Eval("Name_En") %>'></asp:Label>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
</asp:Content>
