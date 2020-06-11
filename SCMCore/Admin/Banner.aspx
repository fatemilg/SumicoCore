<%@ Page Title="مدیریت بنر" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Banner.aspx.cs" Inherits="SCMCore.Admin.Banner" %>

<%@ Import Namespace="SCMCore.ExtensionMethod" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAddFile" />
        </Triggers>
        <ContentTemplate>
            <asp:HiddenField ID="hfModeFile" runat="server" />
            <asp:HiddenField ID="hfIDBanner" runat="server" />
            <asp:HiddenField ID="hfIDProductForUrl" runat="server" />

            <div class="col-lg-12" id="divInfo" runat="server">
                <section class="panel" dir="rtl">
                    <header class="panel-heading">
                        <span style="font-size: 20px;">اضافه کردن عکس برای بنر
                        </span>
                    </header>
                    <div class="btn-group-justified" dir="rtl">
                        <asp:Button ID="btnNewFile" class="btn btn-New" runat="server" Text="جدید" Style="float: right" UseSubmitBehavior="false" Width="80px" OnClick="btnNewFile_Click" />
                        <asp:Button ID="btnAddFile" class="btn btn-Add" ValidationGroup="submitFile" Style="float: right" runat="server" Text="ثبت" Width="80px" OnClick="btnAddFile_Click" />
                        <asp:Button ID="btnGrid" CssClass="btn btn-Grid" runat="server" Style="float: right" Text="لیست بنرها" UseSubmitBehavior="false" Width="100px" OnClick="btnGrid_Click" />
                    </div>
                    <div class="panel-body">
                        <div class="position-center">
                            <div class="form-group">
                                <label for="Name">عنوان عکس :</label>
                                <asp:TextBox ID="txtFileName" class=" form-control" runat="server" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="نام عکس را وارد کنید" Font-Size="Small" ForeColor="red" ControlToValidate="txtFileName" ValidationGroup="submitFile" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label for="Name">عنوان عکس(انگلیسی) :</label>
                                <asp:TextBox ID="txtFileName_En" class=" form-control" runat="server" MaxLength="100"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="Name">توضیحات :</label>
                                <asp:TextBox ID="txtDescription" class=" form-control" runat="server" MaxLength="400"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="Name">توضیحات(انگلیسی) :</label>
                                <asp:TextBox ID="txtDescription_En" class=" form-control" runat="server" MaxLength="400"></asp:TextBox>
                            </div>

                            <div class="form-group" id="divForeinUrl" runat="server">
                                <label for="Content">لینک آدرسی که بنر به آن اشاره می کند: (Example: http://yoursite.com)</label>
                                <asp:TextBox ID="RetForeignUrl" class=" form-control" runat="server" dir="ltr">
                                </asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label for="txtSort">ترتیب نمایش :</label>
                                <asp:TextBox ID="txtSort" TextMode="Number" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="Active">فعال :</label>
                                <asp:CheckBox ID="chkActive" runat="server" Checked="true"></asp:CheckBox>
                            </div>
                            <div class="form-group ">
                                <label for="Pic">تصویر : </label>
                                <asp:Button ID="delPicBanner" runat="server" CssClass="btn btn-Delete" ToolTip="حذف عکس" OnClick="delPicBanner_Click" />
                                <div class="form-group last">

                                    <div class="col-md-11">
                                        <div class="fileupload fileupload-new" data-provides="fileupload">

                                            <div class="fileupload-preview fileupload-exists thumbnail" title="عکس جدید" style="max-width: 200px; max-height: 150px; line-height: 20px;"></div>
                                            <asp:Image ID="imgBanner" ToolTip="عکس ثبت شده" Style="max-width: 200px; max-height: 150px; line-height: 20px;" runat="server" Visible="false" />
                                            <div>
                                                <span>

                                                    <asp:FileUpload ID="fulPicUrl" CssClass="default" runat="server" />

                                                </span>

                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                                 <div class="form-group ">
                                <label for="Pic">تصویر برای موبایل : </label>
                                <asp:Button ID="btnDeletePicMobile" runat="server" CssClass="btn btn-Delete" ToolTip="حذف عکس" OnClick="btnDeletePicMobile_Click" />
                                <div class="form-group last">

                                    <div class="col-md-11">
                                        <div class="fileupload fileupload-new" data-provides="fileupload">

                                            <div class="fileupload-preview fileupload-exists thumbnail" title="عکس جدید" style="max-width: 200px; max-height: 150px; line-height: 20px;"></div>
                                            <asp:Image ID="imgBannerMobile" ToolTip="عکس ثبت شده" Style="max-width: 200px; max-height: 150px; line-height: 20px;" runat="server" Visible="false" />
                                            <div>
                                                <span>

                                                    <asp:FileUpload ID="fulPicUrlForMobile" CssClass="default" runat="server" />

                                                </span>

                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </div>
            <div class="col-lg-12" id="divTable" runat="server">
                <section class="panel" dir="rtl">
                    <header class="panel-heading">
                        <span style="font-size: 20px;">لیست بنرها
                        </span>
                    </header>
                    <div dir="rtl">

                        <asp:Button ID="btnNew2" CssClass="btn btn-New" runat="server" Text="جدید" UseSubmitBehavior="false" Width="80px" OnClick="btnNew2_Click" />

                    </div>
                    <div class="panel-body">
                        <section id="unseen">
                            <asp:GridView ID="grdBanner" runat="server" CssClass="table table-bordered table-striped table-condensed" AutoGenerateColumns="false"
                                DataKeyNames="IDBanner" PageSize="10" AllowPaging="true" OnRowCommand="grdBanner_RowCommand" OnRowEditing="grdBanner_RowEditing" OnRowDeleting="grdBanner_RowDeleting" OnPageIndexChanging="grdBanner_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="عملیات" HeaderStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Button ID="btnDel" CssClass="btn btn-Delete" ToolTip="حذف" runat="server" CommandName="Delete" OnClientClick="return confirm(' آیا مطمئن هستید ؟ ')" CommandArgument='<%#Eval("IDBanner")%>' />
                                            <asp:Button ID="btnEdit" CssClass="btn btn-edit" ToolTip="ویرایش" runat="server" CommandName="Edit" CommandArgument='<%#Eval("IDBanner")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="عنوان فایل">
                                        <ItemTemplate>
                                            <asp:Label ID="Name" runat="server" Text=' <%#Eval("Name_Fa") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="توضیحات">
                                        <ItemTemplate>
                                            <asp:Label ID="Description_Fa" runat="server" Text=' <%#Eval("Description_Fa") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="لینک خارجی">
                                        <ItemTemplate>
                                            <asp:Label ID="ForeignLink" runat="server" Text=' <%#Eval("ForeignLink") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="نمایش تصویر">
                                        <ItemTemplate>
                                            <asp:Image Width="50px" Height="50px" ID="FileType" runat="server" ImageUrl=' <%#"../"+ Eval("PicUrl")%>' Visible='<%#ShowImage(Eval("PicUrl").ToString()) %>'></asp:Image>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="نمایش در صفحه اصلی">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="Active" runat="server" Enabled="false" Checked='<%#bool.Parse(Eval("Active").ToString())%>'></asp:CheckBox>
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
    <script src="js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="js/bootstrap-fileupload/bootstrap-fileupload.js"></script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
</asp:Content>
