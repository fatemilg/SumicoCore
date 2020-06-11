<%@ Page Title=" ویژگی فنی محصولات" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ProductTechnicalProperty.aspx.cs" Inherits="SCMCore.Admin.ProductTechnicalProperty" %>

<%@ Import Namespace="SCMCore.ExtensionMethod" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfMode" runat="server" />
            <asp:HiddenField ID="hfIDTechnicalProperty" runat="server" />


            <div class="col-lg-12" id="divInfo" runat="server">
                <section class="panel" dir="rtl">
                    <header class="panel-heading">
                        <span style="font-size: 20px;">تعریف ویژگی فنی محصولات
                        </span>

                    </header>
                    <div class="btn-group-justified" dir="rtl">

                        <asp:Button ID="btnNew" class="btn btn-New" runat="server" Text="جدید" Style="float: right" UseSubmitBehavior="false" Width="80px" OnClick="btnNew_Click" />
                        <asp:Button ID="btnAdd" class="btn btn-Add" ValidationGroup="submit" Style="float: right" runat="server" Text="ثبت" Width="80px" OnClick="btnAdd_Click" />
                        <asp:Button ID="btnGrid" CssClass="btn btn-Grid" runat="server" Style="float: right" Text="لیست ویژگی ها" UseSubmitBehavior="false" Width="100px" OnClick="btnGrid_Click" />
                    </div>
                    <div class="panel-body">
                        <div class="position-center">
                            <div class="form-group">
                                <label for="Name">نام ویژگی(En) :</label>
                                <asp:TextBox ID="txtName_En" class=" form-control" runat="server" MaxLength="200"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="نام ویژگی(En) را وارد کنید" Font-Size="Small" ForeColor="red" ControlToValidate="txtName_En" ValidationGroup="submit" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label for="Name">نام ویژگی(Fa) :</label>
                                <asp:TextBox ID="txtName" class=" form-control" runat="server" MaxLength="200"></asp:TextBox>
                            </div>

                        </div>
                    </div>
                </section>
            </div>
            <div class="col-lg-12" id="divTable" runat="server">
                <section class="panel" dir="rtl">
                    <header class="panel-heading">
                        <span style="font-size: 20px;">لیست ویژگی های فنی محصولات
                        </span>
                    </header>
                    <div dir="rtl" style="margin-top: 5px">

                        <asp:Button ID="btnNew2" CssClass="btn btn-New" runat="server" Text="جدید" UseSubmitBehavior="false" Width="80px" OnClick="btnNew2_Click" />

                        <asp:Button ID="btnSearch" runat="server" CssClass="btnSearch" OnClick="btnSearch_Click" />
                        <div class="col-md-2 form-group" style="float: left">
                            <asp:TextBox ID="txtSearch" runat="server" Width="100%" CssClass="form-control" placeholder="نام ویژگی"></asp:TextBox>
                        </div>
                    </div>
                    <div class="panel-body">
                        <section id="unseen" class="col-md-4">
                            <asp:GridView ID="grdTechnicalProperty" runat="server" CssClass="table table-bordered table-striped table-condensed" AutoGenerateColumns="false"
                                DataKeyNames="IDTechnicalProperty" PageSize="10" AllowPaging="true" OnRowCommand="grdTechnicalProperty_RowCommand" OnRowEditing="grdTechnicalProperty_RowEditing" OnRowDeleting="grdTechnicalProperty_RowDeleting" OnPageIndexChanging="grdTechnicalProperty_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="عملیات">
                                        <ItemTemplate>
                                            <asp:Button ID="btnDel" CssClass="btn btn-Delete" ToolTip="حذف" runat="server" CommandName="Delete" OnClientClick="return confirm(' آیا مطمئن هستید ؟ ')" CommandArgument='<%#Eval("IDTechnicalProperty")%>' />
                                            <asp:Button ID="btnEdit" CssClass="btn btn-edit" ToolTip="ویرایش" runat="server" CommandName="Edit" CommandArgument='<%#Eval("IDTechnicalProperty")%>' />
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="کد">
                                        <ItemTemplate>
                                            <asp:Label ID="Code" runat="server" Text=' <%#Eval("Code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="نام ویژگی(Fa)">
                                        <ItemTemplate>
                                            <asp:Label ID="Name" runat="server" Text=' <%#Eval("Name_Fa") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="نام ویژگی(En)">
                                        <ItemTemplate>
                                            <asp:Label ID="Description_Fa" runat="server" Text=' <%#Eval("Name_En") %>'></asp:Label>
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
