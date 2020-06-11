<%@ Page Title="مدیریت کامنت ها" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Comment.aspx.cs" Inherits="SCMCore.Admin.Comment" %>

<%@ Import Namespace="SCMCore.ExtensionMethod" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <!-- Hidden Fields-->
            <asp:HiddenField ID="hfIdComment" runat="server" />
            <asp:HiddenField ID="hfIDContent" runat="server" />
            <asp:HiddenField ID="hfParentCommentID" runat="server" />

            <div class="col-lg-12" id="divInfo" runat="server">
                <section class="panel" dir="rtl">
                    <header class="panel-heading">
                        <span style="font-size: 20px;">پاسخ دهی  به کامنت های مرتبط با محتوا
                        </span>

                    </header>
                    <div class=" btn-group-justified" dir="rtl">

                        <asp:Button ID="btnNew" CssClass="btn btn-New" runat="server" Text="جدید" Style="float: right" UseSubmitBehavior="false" Width="80px" OnClick="btnNew_Click" />
                        <asp:Button ID="btnAdd" CssClass="btn btn-Add" ValidationGroup="submit" Style="float: right" runat="server" Text="ثبت" OnClick="btnAdd_Click" Width="80px" />
                        <asp:Button ID="btnGrid" CssClass="btn btn-Grid" runat="server" Style="float: right" Text="نمای جدولی" UseSubmitBehavior="false" Width="80px" OnClick="btnGrid_Click" />

                    </div>
                    <div class="panel-body">
                        <div class="position-center">

                            <div class="form-group">
                                <label for="FullName">نام  و نام خانوادگی کاربر :</label>
                                <asp:TextBox ID="txtFullName" CssClass=" form-control" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="form-group ">
                                <label for="content">عنوان محتوا:</label>

                                <asp:TextBox ID="txtContent" CssClass=" form-control" runat="server" Enabled="false"></asp:TextBox>


                            </div>
                            <div class="form-group ">
                                <label for="Email">ایمیل:</label>

                                <asp:TextBox ID="txtEmail" CssClass=" form-control" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="form-group ">
                                <label for="WebSite">وب سایت :</label>

                                <asp:TextBox ID="txtWebSite" CssClass=" form-control" runat="server" Enabled="false"></asp:TextBox>

                            </div>
                            <div class="form-group ">
                                <label for="UserComment">کامنت کاربر :</label>

                                <asp:TextBox ID="txtUserComment" CssClass=" form-control" runat="server" Enabled="false" TextMode="MultiLine"></asp:TextBox>

                            </div>
                            <div id="divAdminComment" class="form-group " runat="server">
                                <label for="AdminComment" >کامنت مدیر :</label>

                                <asp:TextBox ID="txtAdminComment" CssClass=" form-control" runat="server" MaxLength="500" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="کامنت خود را وارد کنید" Font-Size="Small" ForeColor="red" ControlToValidate="txtAdminComment" ValidationGroup="submit" Display="Dynamic"></asp:RequiredFieldValidator>

                            </div>

                        </div>
                    </div>
                </section>

            </div>

            <div class="col-lg-12" id="divTable" runat="server">
                <section class="panel" dir="rtl">
                    <header class="panel-heading">
                        <span style="font-size: 20px;">لیست کامنت های مرتبط با محتوا و تماس با ما
                        </span>
                    </header>
                    <div dir="rtl" style="margin-top: 5px">


                        <asp:Button ID="btnSearch" runat="server" CssClass="btnSearch" OnClick="btnSearch_Click" />
                        <div class="col-md-2 form-group" style="float: left">
                            <asp:TextBox ID="txtSearch" runat="server" Width="100%" CssClass="form-control" placeholder="  نام و نام خانوادگی- عنوان مطلب"></asp:TextBox>
                        </div>

                    </div>
                    <div class="panel-body">
                        <section id="unseen">
                            <asp:GridView ID="grdComment" runat="server" CssClass="display table table-bordered table-striped dataTable" AutoGenerateColumns="false"
                                DataKeyNames="IDComment" PageSize="10" AllowPaging="true" OnRowCommand="grdComment_RowCommand" OnRowEditing="grdComment_RowEditing" OnRowDeleting="grdComment_RowDeleting" OnPageIndexChanging="grdComment_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="عملیات برای نظر کاربر" HeaderStyle-Width="8%"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Button ID="btnDelCommentUser" CssClass="btn btn-Delete" ToolTip="حذف نظر کاربر" runat="server" CommandName="DeleteCommentUser" OnClientClick="return confirm('آیا مطمئن هستید ؟')" CommandArgument='<%#Eval("IDComment")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="نام و نام خانوادگی" HeaderStyle-Width="12%">
                                        <ItemTemplate>
                                            <asp:Label ID="FullName" runat="server" Text=' <%#Eval("FullName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="عنوان محتوا" HeaderStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:Label ID="ContentName" runat="server" Text='<%#ReturnContentName(Eval("ContentName").ToString()) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="تاریخ ارسال کاربر" HeaderStyle-Width="12%">
                                        <ItemTemplate>
                                            <asp:Label ID="CreateDate" runat="server" Text=' <%#DateTime.Parse(Eval("CreateDate").ToString()).ToShamsiDateString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="نظر کاربر" HeaderStyle-Width="20%"> 
                                        <ItemTemplate>
                                            <asp:Label ID="Comment" runat="server" Text=' <%#Eval("Comment").ToString().Substring(0,Eval("Comment").ToString().Length/3+1) + " ..." %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="عملیات برای نظر مدیر" HeaderStyle-Width="8%"   ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                                <asp:HiddenField ID="hfReplyID" runat="server" Value='<%#Eval("ReplyID")%>' />
                                            <asp:Button ID="btnDelCommentAdmin" CssClass="btn btn-Delete" ToolTip="حذف نظر مدیر" runat="server" CommandName="DeleteCommentAdmin" OnClientClick="return confirm('آیا مطمئن هستید ؟')" CommandArgument='<%#Eval("IDComment")%>'  Visible='<%#showDeleteButton(Eval("IDContent").ToString().StringToGuid()) %>'/>
                                            <asp:Button ID="btnEditCommentAdmin" CssClass="btn btn-edit"  runat="server" CommandName="EditCommentAdmin" CommandArgument='<%#Eval("IDComment")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="نظر مدیر">
                                        <ItemTemplate>
                                            <asp:Label ID="ReplyComment" runat="server" Text=' <%#Eval("ReplyComment") %>'></asp:Label>
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






            <div class="col-lg-12" id="divProductTable" runat="server">
                <section class="panel" dir="rtl">
                    <header class="panel-heading">
                        <span style="font-size: 20px;">لیست کامنت های مرتبط با محصولات
                        </span>
                    </header>
                    <div dir="rtl" style="margin-top: 5px">


                        <asp:Button  ID="btnSearchCommentProduct" runat="server" CssClass="btnSearch" OnClick="btnSearchCommentProduct_Click" />
                        <div class="col-md-2 form-group" style="float: left">
                            <asp:TextBox ID="txtSearchCommentProduct" runat="server" Width="100%" CssClass="form-control" placeholder="  نام و نام خانوادگی- عنوان محصول"></asp:TextBox>
                        </div>
                    </div>
                    <div class="panel-body">
                        <section id="unseen2">
                            <asp:GridView ID="grdProductComment" runat="server" CssClass="display table table-bordered table-striped dataTable" AutoGenerateColumns="false"
                                DataKeyNames="IDComment" PageSize="10" AllowPaging="true" OnRowCommand="grdProductComment_RowCommand" OnRowEditing="grdProductComment_RowEditing" OnRowDeleting="grdProductComment_RowDeleting" OnPageIndexChanging="grdProductComment_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="عملیات برای نظر کاربر" HeaderStyle-Width="8%"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Button ID="btnDelProductCommentUser" CssClass="btn btn-Delete" ToolTip="حذف نظر کاربر" runat="server" CommandName="DeleteProductCommentUser" OnClientClick="return confirm('آیا مطمئن هستید ؟')" CommandArgument='<%#Eval("IDComment")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="نام و نام خانوادگی"  HeaderStyle-Width="12%">
                                        <ItemTemplate>
                                            <asp:Label ID="FullName2" runat="server" Text=' <%#Eval("FullName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="عنوان محصول"  HeaderStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:Label ID="ContentName2" runat="server" Text='<%#ReturnContentName(Eval("ProductName").ToString()) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="تاریخ ارسال کاربر" HeaderStyle-Width="12%">
                                        <ItemTemplate>
                                            <asp:Label ID="CreateDate2" runat="server" Text=' <%#DateTime.Parse(Eval("CreateDate").ToString()).ToShamsiDateString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="نظر کاربر" HeaderStyle-Width="20%"> 
                                        <ItemTemplate>
                                            <asp:Label ID="Comment2" runat="server" Text=' <%#Eval("Comment").ToString().Substring(0,Eval("Comment").ToString().Length/3+1) + " ..." %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="عملیات برای نظر مدیر" HeaderStyle-Width="8%"   ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                                <asp:HiddenField ID="hfReplyID" runat="server" Value='<%#Eval("ReplyID")%>' />
                                            <asp:Button ID="btnDelProductCommentAdmin" CssClass="btn btn-Delete" ToolTip="حذف نظر مدیر" runat="server" CommandName="DeleteProductCommentAdmin" OnClientClick="return confirm('آیا مطمئن هستید ؟')" CommandArgument='<%#Eval("IDComment")%>'  />
                                            <asp:Button ID="btnEditProductCommentAdmin" CssClass="btn btn-edit"  runat="server" CommandName="EditProductCommentAdmin" CommandArgument='<%#Eval("IDComment")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="نظر مدیر">
                                        <ItemTemplate>
                                            <asp:Label ID="ReplyComment2" runat="server" Text=' <%#Eval("ReplyComment") %>'></asp:Label>
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
