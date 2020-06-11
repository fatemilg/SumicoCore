<%@ Page Title="مدیریت کاربران" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="SCMCore.Admin.Register" EnableEventValidation="false" %>

<%@ Import Namespace="SCMCore.ExtensionMethod" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/table-responsive.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAdd" />
        </Triggers>
        <ContentTemplate>
            <!-- Hidden Fields-->
            <asp:HiddenField ID="hfMode" runat="server" />
            <asp:HiddenField ID="hfIdUser" runat="server" />
            <asp:HiddenField ID="hfPhoneMember" runat="server" />
            <asp:HiddenField ID="hfIDPhoneMember" runat="server" />
            <asp:HiddenField ID="hfModePhoneMember" runat="server" />
            <div class="col-lg-12" id="divInfo" runat="server">
                <section class="panel" dir="rtl">
                    <header class="panel-heading">
                        <span style="font-size: 20px;">ثبت کاربر
                        </span>
                    </header>
                    <div class=" btn-group-justified" dir="rtl">

                        <asp:Button ID="btnNew" CssClass="btn btn-New" runat="server" Text="جدید" Style="float: right" UseSubmitBehavior="false" Width="80px" OnClick="btnNew_Click" />
                        <asp:Button ID="btnAdd" CssClass="btn btn-Add" ValidationGroup="submit" Style="float: right" runat="server" Text="ثبت" OnClick="btnAdd_Click" Width="80px" />
                        <asp:Button ID="btnGrid" CssClass="btn btn-Grid" runat="server" Style="float: right" Text="لیست کاربران" UseSubmitBehavior="false" Width="100px" OnClick="btnGrid_Click" />

                    </div>
                    <div class="panel-body">
                        <div class="position-center">
                            <div class="form-group">
                                <label for="FName">نام :</label>
                                <asp:TextBox ID="txtFName" CssClass=" form-control" runat="server" MaxLength="200"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="نام را وارد کنید" Font-Size="Small" ForeColor="red" ControlToValidate="txtFName" ValidationGroup="submit" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group ">
                                <label for="LName">نام خانوادگی :</label>
                                <asp:TextBox ID="txtLName" CssClass=" form-control" runat="server" MaxLength="200"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="نام خانوادگی را وارد کنید" Font-Size="Small" ForeColor="red" ControlToValidate="txtLName" ValidationGroup="submit" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group ">
                                <label for="UserName">نام کاربری :</label>
                                <asp:TextBox ID="txtUserName" CssClass=" form-control" runat="server" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="نام کاربری را وارد کنید" Font-Size="small" ForeColor="red" ControlToValidate="txtUserName" ValidationGroup="submit" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group ">
                                <label for="Password">رمز عبور :</label>
                                <asp:TextBox ID="txtPassword" CssClass=" form-control" runat="server" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="رمز عبور را وارد کنید" Font-Size="Small" ForeColor="Red" ControlToValidate="txtPassword" ValidationGroup="submit" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group ">
                                <label for="Email">ایمیل اصلی:</label>
                                <asp:TextBox ID="txtEmail" CssClass=" form-control" runat="server" MaxLength="50"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="مقدار وارد شده در فرمت ایمیل نیست" Font-Size="small" ForeColor="red" ControlToValidate="txtEmail" ValidationGroup="submit" Display="Dynamic" ValidationExpression="\w+([-.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </div>
                            <div class="form-group ">
                                <label for="Email">شماره شناسنامه:</label>
                                <asp:TextBox ID="txtIdentifyNumber" CssClass=" form-control" runat="server" MaxLength="20"></asp:TextBox>
                            </div>
                            <div class="form-group ">
                                <label for="Email">کد ملی:</label>
                                <asp:TextBox ID="txtNationalCode" CssClass=" form-control" runat="server" MaxLength="20"></asp:TextBox>
                            </div>
                            <div class="form-group ">
                                <label for="Active">کاربر فعال </label>
                                <asp:CheckBox ID="chkActive" runat="server" Checked="true" />
                            </div>
                            <div class="form-group ">
                                <label for="PowerUser">مدیر سایت </label>
                                <asp:CheckBox ID="chkPowerUser" runat="server" />
                            </div>
                            <div class="form-group ">
                                <label for="Pic">تصویر : </label>
                                <div class="form-group last">
                                    <div class="col-md-11">
                                        <div class="fileupload fileupload-new" data-provides="fileupload">
                                            <div class="fileupload-preview fileupload-exists thumbnail" title="عکس جدید" style="max-width: 200px; max-height: 150px; line-height: 20px;"></div>
                                            <asp:Image ID="imgUser" ToolTip="عکس ثبت شده" Style="max-width: 200px; max-height: 150px; line-height: 20px;" runat="server" Visible="false" />
                                            <div>
                                                <span>
                                                    <asp:FileUpload ID="fulPicUrl" CssClass="default" runat="server" />
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
                        <span style="font-size: 20px;">لیست کاربران
                        </span>
                    </header>
                    <div dir="rtl">

                        <asp:Button ID="btnNew2" CssClass="btn btn-New" runat="server" Text="جدید" UseSubmitBehavior="false" Width="80px" OnClick="btnNew2_Click" />
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="txtSearch" placeholder="جستجو"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btnSearch" OnClick="btnSearch_Click" />

                    </div>
                    <div class="panel-body">
                        <section id="unseen">
                            <asp:GridView ID="grdUser" runat="server" CssClass="display table table-bordered table-striped dataTable" AutoGenerateColumns="false"
                                DataKeyNames="IDUser" PageSize="10" AllowPaging="true" OnRowCommand="grdUser_RowCommand" OnRowEditing="grdUser_RowEditing" OnRowDeleting="grdUser_RowDeleting" OnPageIndexChanging="grdUser_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="عملیات" HeaderStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:Button ID="btnEdit" CssClass="btn btn-edit" ToolTip="ویرایش" runat="server" CommandName="Edit" CommandArgument='<%#Eval("IDUser")%>' />
                                            <%--<asp:Button ID="btnTel" CssClass="btn btn-phone" ToolTip="اطلاعات تماس" runat="server" CommandName="Tel" CommandArgument='<%#Eval("IDUser")%>' />--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="نام">
                                        <ItemTemplate>
                                            <asp:Label ID="FName" runat="server" Text=' <%#Eval("FName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="نام خانوادگی">
                                        <ItemTemplate>
                                            <asp:Label ID="LName" runat="server" Text=' <%#Eval("LName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="نام کاربری">
                                        <ItemTemplate>
                                            <asp:Label ID="UserName" runat="server" Text=' <%#Eval("UserName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="شماره شناسنامه">
                                        <ItemTemplate>
                                            <asp:Label ID="IdentifyNumber" runat="server" Text=' <%#Eval("IdentifyNumber") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="کد ملی">
                                        <ItemTemplate>
                                            <asp:Label ID="NationalCode" runat="server" Text=' <%#Eval("NationalCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="کاربر فعال">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="Active" runat="server" Enabled="false" Checked='<%#bool.Parse(Eval("Active").ToString())%>'></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="مدیر سایت">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="PowerUser" runat="server" Enabled="false" Checked='<%#bool.Parse(Eval("PowerUser").ToString())%>'></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="مدیر اصلی">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="SupperUser" runat="server" Enabled="false" Checked='<%#bool.Parse(Eval("SupperUser").ToString())%>'></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Picture">
                                        <ItemTemplate>
                                            <asp:Image Width="50px" Height="50px" ID="FileType" runat="server" ImageUrl=' <%#"../"+ Eval("PicUrl")%>' Visible='<%#ShowImage(Eval("PicUrl").ToString()) %>'></asp:Image>
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
    <!-- Links-->
    <script src="js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="js/bootstrap-fileupload/bootstrap-fileupload.js"></script>

</asp:Content>
