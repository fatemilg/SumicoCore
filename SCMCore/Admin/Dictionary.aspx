<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Dictionary.aspx.cs" Inherits="SCMCore.Admin.Dictionary" %>

<%@ Register Src="~/Admin/UserControl/FileUpload.ascx" TagPrefix="uc1" TagName="FileUpload" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <!-- Hidden Fields-->
            <asp:HiddenField ID="hfMode" runat="server" />
            <asp:HiddenField ID="hfIDDictionary" runat="server" />
            <asp:HiddenField ID="hfDictionaryPicUrl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="col-lg-12">
        <section class="panel" dir="rtl">
            <header class="panel-heading">
                <span style="font-size: 20px;">فربین پدیا
                </span>
            </header>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class=" btn-group-justified" dir="rtl">
                        <asp:Button ID="btnNew" CssClass="btn btn-New" runat="server" Text="جدید" Style="float: right" UseSubmitBehavior="false" Width="80px" OnClick="btnNew_Click" />
                        <asp:Button ID="btnAdd" CssClass="btn btn-Add"  Style="float: right" runat="server" Text="ثبت" OnClick="btnAdd_Click" Width="80px" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="panel-body">
                <div class="position-center">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="form-group">
                                <label for="KeyWord">اصطلاح :</label>
                                <asp:TextBox ID="txtKeyWord" CssClass=" form-control" runat="server" MaxLength="500"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="Title">عبارت :</label>
                                <asp:TextBox ID="txtTitle" CssClass=" form-control" runat="server" MaxLength="500"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="SourceText">منبع :</label>
                                <asp:TextBox ID="txtSourceText" CssClass=" form-control" runat="server" MaxLength="500"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="Abstract">خلاصه توضیح :</label>
                                <asp:TextBox ID="txtAbstract" CssClass=" form-control" runat="server" MaxLength="200" ></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="MetaTag">Meta Tags :</label>
                                <asp:TextBox ID="txtMetaTag" CssClass=" form-control" runat="server" MaxLength="5000" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="MetaDescription">Meta Descriptions :</label>
                                <asp:TextBox ID="txtMetaDescription" CssClass=" form-control" runat="server" MaxLength="5000" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            <div class="form-group ">
                                <label for="LName">توضیحات:</label>
                                <CKEditor:CKEditorControl ID="txtValue" runat="server"></CKEditor:CKEditorControl>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="form-group">
                        <label for="Pic">تصویر : </label>
                        <uc1:FileUpload runat="server" ID="fulDictionary" />
                    </div>
                </div>
            </div>
        </section>
    </div>
    <div class="col-lg-12">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <section class="panel" dir="rtl">
                    <header class="panel-heading">
                        <span style="font-size: 20px;">لیست عبارات
                        </span>
                    </header>
                    <div dir="rtl">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="txtSearch" placeholder="جستجو"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btnSearch" OnClick="btnSearch_Click" />
                    </div>
                    <div class="panel-body">
                        <section id="unseen">
                            <asp:GridView ID="grdDictionary" runat="server" CssClass="display table table-bordered table-striped dataTable" AutoGenerateColumns="false"
                                DataKeyNames="IDDictionary" PageSize="10" AllowPaging="true" OnRowCommand="grdDictionary_RowCommand" OnRowEditing="grdDictionary_RowEditing" OnRowDeleting="grdDictionary_RowDeleting" OnPageIndexChanging="grdDictionary_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="عملیات" HeaderStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:Button runat="server" CommandName="Delete" ID="btDelete" CssClass="btn btn-Delete" OnClientClick="return confirm(' آیا مطمئن هستید ؟ ')" CommandArgument='<%#Eval("IDDictionary")%>' />
                                            <asp:Button ID="btnEdit" CssClass="btn btn-edit" ToolTip="ویرایش" runat="server" CommandName="Edit" CommandArgument='<%#Eval("IDDictionary")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="اصطلاح">
                                        <ItemTemplate>
                                            <asp:Label ID="KeyWord" runat="server" Text=' <%#Eval("KeyWord") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="عبارت">
                                        <ItemTemplate>
                                            <asp:Label ID="Title" runat="server" Text=' <%#Eval("Title") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Picture">
                                        <ItemTemplate>
                                            <asp:Image Width="50px" ID="FileType" runat="server" ImageUrl=' <%#"../"+ Eval("PicUrl")%>' Visible='<%#ShowImage(Eval("PicUrl").ToString()) %>'></asp:Image>
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
</asp:Content>
