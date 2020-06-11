<%@ Page Title="مدیریت محصولات" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="SCMCore.Admin.Product" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="SCMCore.ExtensionMethod" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Src="~/Admin/UserControl/AttachSite.ascx" TagPrefix="uc1" TagName="AttachSite" %>
<%@ Register Src="~/Admin/UserControl/TechnicalProperty.ascx" TagPrefix="uc1" TagName="TechnicalProperty" %>
<%@ Register Src="~/Admin/UserControl/DefineDetailProduct.ascx" TagPrefix="uc1" TagName="DefineDetailProduct" %>
<%@ Register Src="~/Admin/UserControl/DetailAssignProperty.ascx" TagPrefix="uc1" TagName="DetailAssignProperty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function validate() {
            var summary = "";
            summary += isValidProduct();
            if (summary != "") {
                alert(summary);
                return false;
            }
            else {
                return true;
            }
        }
        function isValidProduct() {
            var uid;
            var temp = document.getElementById("<%=txtProduct_En.ClientID %>");
            uid = temp.value;
            if (uid == "") {
                return ("نام محصول(En) را وارد کنید" + "\n");
            }
            else {
                return "";
            }
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divMyCtrl">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnAddProduct" />
                <asp:PostBackTrigger ControlID="btnAddProductCategory" />
            </Triggers>
            <ContentTemplate>
                <asp:HiddenField ID="hfModeProductCategory" runat="server" />

                <asp:HiddenField ID="hfModeProduct" runat="server" />
                <asp:HiddenField ID="hfModeVersionProduct" runat="server" />

                <asp:HiddenField ID="hfIDProductCategory" runat="server" />
                <asp:HiddenField ID="hfIDProduct" runat="server" />

                <asp:HiddenField ID="hfIDVersionProduct" runat="server" />

                <asp:HiddenField ID="hfProperty" runat="server" />
                <asp:HiddenField ID="hfIDParentGroup" runat="server" />
                <%-- baraye dastebandi goor ha dar hengame tarif grooh estefade mishavad--%>
                <asp:HiddenField ID="hfIDCategoryForProductInAutoComplete" runat="server" />
                <%--baraye enteghale iek mahsool beine grooh ha estefade mishavad--%>

                <div class="col-lg-12" id="divInfoProductCategory" runat="server">
                    <section class="panel" dir="rtl">
                        <header class="panel-heading">
                            <span>دسته بندی محصولات
                            </span>
                        </header>
                        <div class="btn-group-justified" dir="rtl">
                            <asp:Button ID="btnNewProductCategory" class="btn btn-New" runat="server" Text="دسته بندی جدید" Style="float: right" UseSubmitBehavior="false" Width="120px" OnClick="btnNewProductCategory_Click" />
                            <asp:Button ID="btnAddProductCategory" class="btn btn-Add" ValidationGroup="submitProductGroup" Style="float: right" runat="server" Text="ثبت دسته بندی" Width="120px" OnClick="btnAddProductCategory_Click" />
                            <asp:Button ID="btnGridProductCategory" class="btn btn-Grid" runat="server" Style="float: right" Text="لیست دسته بندی محصولات" UseSubmitBehavior="false" Width="150px" OnClick="btnGridProductCategory_Click" />
                        </div>
                        <div class="panel-body">
                            <div class="position-center">
                                <div class="col-md-6 form-group " runat="server">
                                    <label for="LName">عنوان گروه محصولات(En) :</label>

                                    <asp:TextBox ID="txtGroupName_En" CssClass=" form-control" runat="server" MaxLength="200" Width="100%"></asp:TextBox>
                                </div>
                                <div class="col-md-6 form-group">
                                    <label for="FName">عنوان گروه محصولات(Fa) :</label>
                                    <asp:TextBox ID="txtGroupName_Fa" CssClass=" form-control" runat="server" MaxLength="200" Width="100%"></asp:TextBox>
                                </div>

                                <div class="clearfix"></div>
                                <div class="col-md-6 form-group">
                                    <label for="FName">تامین کننده :</label>
                                    <asp:Label ID="lblSpplierNameForProductCategory" runat="server" CssClass="form-control" Width="100%"></asp:Label>
                                </div>

                                <div class="clearfix"></div>
                                <div class="col-md-5 form-group ">
                                    <label for="Name_En">سرگروه این دسته:</label>
                                    <asp:TextBox ID="txtParentGroup" CssClass=" form-control" runat="server" MaxLength="200" Style="width: 100%; display: inline; text-align: left"></asp:TextBox>
                                </div>
                                <div class="col-md-1 form-group">
                                    <label for="drpLegalUser">.</label><br />
                                    <input type="button" id="ClearProductCategory" class="btn btn-Clean ClearProductCategory " title="پاک کردن نام سرگروه این دسته" />
                                </div>
                                <div class="col-md-6 form-group ">
                                    <label for="Order">ترتیب برای نمایش :</label>
                                    <asp:TextBox ID="txtOrder" class=" form-control" runat="server" TextMode="Number" Width="100%"></asp:TextBox>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-12 form-group">
                                    <label for="MetaTags">MetaTags :</label>
                                    <asp:TextBox ID="txtMetaTagsCategory" class=" form-control" runat="server" MaxLength="1000" Width="100%"></asp:TextBox>
                                </div>
                                <div class="col-md-12 form-group ">
                                    <label for="MetaDescriptionsCategory">MetaDescriptions :</label>

                                    <asp:TextBox ID="txtMetaDescriptionsCategory" class=" form-control" runat="server" MaxLength="1000" Width="100%"></asp:TextBox>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-6 form-group ">
                                    <label for="LName">نمایش گروه محصول در سایت :</label>
                                    <asp:CheckBox ID="chkShowCategoryInSite" runat="server" Checked="True"></asp:CheckBox>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-12 form-group ">
                                    <label for="Pic">تصویر : </label>
                                    <asp:Button ID="delPicProductCategory" runat="server" CssClass="btn btn-Delete" ToolTip="حذف عکس" OnClick="delPicProductCategory_Click" />

                                    <div class="form-group last">

                                        <div class="col-md-11">
                                            <div class="fileupload fileupload-new" data-provides="fileupload">

                                                <div class="fileupload-preview fileupload-exists thumbnail" title="عکس جدید" style="max-width: 200px; max-height: 150px; line-height: 20px;"></div>
                                                <asp:Image ID="imgProductCategory" ToolTip="عکس ثبت شده" Style="max-width: 200px; max-height: 150px; line-height: 20px;" runat="server" Visible="false" />
                                                <div>
                                                    <span>

                                                        <asp:FileUpload ID="fuProductCategory" CssClass="default" runat="server" />

                                                    </span>

                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-12 form-group ">
                                    <label for="Order">توضیحات(Fa) :</label>
                                    <CKEditor:CKEditorControl ID="txtDescriptionProductcategory_Fa" runat="server"></CKEditor:CKEditorControl>
                                </div>
                                <div class="col-md-12 form-group " runat="server">
                                    <label for="Order">توضیحات (En):</label>
                                    <CKEditor:CKEditorControl ID="txtDescriptionProductcategory_En" runat="server"></CKEditor:CKEditorControl>
                                </div>
                            </div>
                        </div>
                    </section>

                </div>

                <div class="col-lg-12" id="divTableProductCategory" runat="server">
                    <section class="panel" dir="rtl">
                        <header class="panel-heading">
                            <span>لیست گروه بندی محصولات شرکت
                                <asp:Label ID="lblSupplierName" runat="server" ForeColor="Red"></asp:Label>
                            </span>
                        </header>
                        <div class="btn-group-justified" dir="rtl" style="margin-top: 5px">
                            <asp:Button ID="btnNewGridProductCategory" class="btn btn-New" runat="server" Text="دسته بندی جدید" UseSubmitBehavior="false" Width="120px" OnClick="btnNewGridProductCategory_Click" />
                            <asp:Button ID="btnReturnSupplier" class="btn btn-Grid" runat="server" Text="لیست تامین کنندگان" UseSubmitBehavior="false" Width="180px" OnClick="btnReturnSupplier_Click" />
                            <asp:Button id="btnShowModalDefinefromQueryStringMaster" runat="server" style="float:left"  CssClass="btn btn-Add" Text="ثبت محصول" Width="80px" UseSubmitBehavior="false" Visible="false" OnClick="btnShowModalDefinefromQueryStringMaster_Click"/>
                        
                        </div>
                        <div class="panel-body">
                            <section id="unseen2">

                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <asp:TreeView ID="tvShowProductCategory" runat="server" ExpandDepth="0" NodeIndent="15" CollapseImageUrl="images/details_close.png" ExpandImageUrl="images/details_open.png"
                                            SelectAction="None" OnSelectedNodeChanged="tvShowProductCategory_SelectedNodeChanged">
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

                <div class="col-lg-12" id="divInfoProduct" runat="server">
                    <section class="panel" dir="rtl">
                        <header class="panel-heading">
                            <span>ثبت محصول
                            </span>
                        </header>
                        <div class="btn-group-justified" dir="rtl">
                            <asp:Button ID="btnNewProduct" class="btn btn-New" runat="server" Text="محصول جدید" Style="float: right" UseSubmitBehavior="false" Width="120px" OnClick="btnNewProduct_Click" />
                            <asp:Button ID="btnAddProduct" class="btn btn-Add" ValidationGroup="submitProduct" Style="float: right" runat="server" Text="ثبت محصول" Width="80px" OnClick="btnAddProduct_Click" />
                            <asp:Button ID="btnGridProduct" class="btn btn-Grid" runat="server" Style="float: right" Text="لیست محصولات" UseSubmitBehavior="false" Width="120px" OnClick="btnGridProduct_Click" />
                        </div>
                        <div class="panel-body">
                            <div class="position-center">

                                <div class="col-md-10 form-group ">
                                    <label for="Name_En">گروه این محصول:</label>
                                    <asp:TextBox ID="txtCategoryForProduct" CssClass=" form-control" runat="server" MaxLength="200" Style="width: 100%; border-radius: 30px; display: inline; text-align: left"></asp:TextBox>
                                </div>
                                <div class="col-md-2 form-group">
                                    <label for="drpLegalUser">.</label><br />
                                    <input type="button" id="ClearCategoryForProduct" class="btn btn-Clean  ClearCategoryForProduct " title="پاک کردن نام سرگروه این دسته" />
                                </div>
                                <div class=" col-md-4 form-group " runat="server">
                                    <label for="LName">نام محصول(En) :</label>
                                    <asp:TextBox ID="txtProduct_En" CssClass=" form-control" runat="server" MaxLength="100" ToolTip="حداکثر 100 کاراکتر" Width="100%"></asp:TextBox>
                                </div>
                                <div class="col-md-4 form-group">
                                    <label for="FName">نام محصول(Fa) :</label>
                                    <asp:TextBox ID="txtProduct_Fa" CssClass=" form-control" runat="server" MaxLength="100" ToolTip="حداکثر 100 کاراکتر" Width="100%"></asp:TextBox>
                                </div>
                                <div class="col-md-4 form-group ">
                                    <label for="Name_En">خانواده محصولات :</label>

                                    <asp:DropDownList ID="drpProductFamily" class=" form-control m-bot15" runat="server" Width="100%">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4 form-group">
                                    <label for="IndexDescriptionText">Sort :</label>
                                    <asp:TextBox ID="txtSort" CssClass=" form-control" runat="server" TextMode="Number" Width="100%"></asp:TextBox>
                                </div>
                                <div class="col-md-4 form-group">
                                    <label for="IndexDescriptionText">IndexDescription Text :</label>
                                    <asp:TextBox ID="txtIndexDescriptionText" CssClass=" form-control" runat="server" MaxLength="100" ToolTip="حداکثر 30 کاراکتر" Width="100%"></asp:TextBox>
                                </div>
                                <div class="col-md-4 form-group">
                                    <label for="Pic">تصویر IndexDescription : </label>
                                    <asp:Button ID="btnDelIndexDescriptionPic" runat="server" CssClass="btn btn-Delete tool" data-placement="bottom" title=" حذف عکس قبلی" OnClick="btnDelIndexDescriptionPic_Click" />
                                    <div class="form-group last">
                                        <div class="col-md-11">
                                            <div class="fileupload fileupload-new" data-provides="fileupload">
                                                <div class="fileupload-preview fileupload-exists thumbnail" title="عکس جدید" style="max-width: 200px; max-height: 150px; line-height: 20px;"></div>
                                                <asp:Image ID="imgIndexDescription" ToolTip="عکس ثبت شده" Style="max-width: 200px; max-height: 150px; line-height: 20px;" runat="server" Visible="false" />

                                                <div>
                                                    <span>
                                                        <asp:FileUpload ID="fulIndexDescription" CssClass="default" runat="server" />

                                                    </span>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-12 form-group">
                                    <label for="MetaTags">MetaTags :</label>
                                    <asp:TextBox ID="txtMetaTags" class=" form-control" runat="server" MaxLength="1000" Width="100%"></asp:TextBox>

                                </div>
                                <div class="col-md-12 form-group ">
                                    <label for="MetaDescriptions">MetaDescriptions :</label>

                                    <asp:TextBox ID="txtMetaDescriptions" class=" form-control" runat="server" MaxLength="1000" Width="100%"></asp:TextBox>
                                </div>
                                <div class="col-md-12 form-group">
                                    <label for="Pic">تصویر : </label>
                                    <asp:Button ID="delPicProduct" runat="server" CssClass="btn btn-Delete" ToolTip="حذف عکس" OnClick="delPicProduct_Click" />
                                    <div class="form-group last">
                                        <div class="col-md-11">
                                            <div class="fileupload fileupload-new" data-provides="fileupload">
                                                <div class="fileupload-preview fileupload-exists thumbnail" title="عکس جدید" style="max-width: 200px; max-height: 150px; line-height: 20px;"></div>
                                                <asp:Image ID="imgProduct" ToolTip="عکس ثبت شده" Style="max-width: 200px; max-height: 150px; line-height: 20px;" runat="server" Visible="false" />
                                                <div>
                                                    <span>
                                                        <asp:FileUpload ID="fulPicUrl" CssClass="default" runat="server" />
                                                    </span>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="form-group " runat="server" visible="false">
                                    <label for="LName">توضیحات(Fa) :</label>
                                    <CKEditor:CKEditorControl ID="txtDescription_Fa" runat="server"></CKEditor:CKEditorControl>
                                </div>
                                <div class="form-group " runat="server" visible="false">
                                    <label for="LName">توضیحات(En) :</label>
                                    <CKEditor:CKEditorControl ID="txtDescription_En" runat="server"></CKEditor:CKEditorControl>
                                </div>
                            </div>
                        </div>
                    </section>
                </div>

                <div class="col-lg-12" id="divTableProduct" runat="server">
                    <section class="panel" dir="rtl">
                        <header class="panel-heading">
                            <span>لیست محصولات از گروه</span>

                            <asp:Label ID="lblProductCategoryNameInProduct" runat="server" ForeColor="red" style="font-size:14px"></asp:Label>

                            </span>

                        </header>
                        <div class="btn-group-justified" dir="rtl" style="margin-top: 5px">

                            <asp:Button ID="btnNewGridProduct" Style="float: right" class="btn btn-New" runat="server" Text="محصول جدید" UseSubmitBehavior="false" Width="120px" OnClick="btnNewGridProduct_Click" />
                            <asp:Button ID="btnListProductCategory" Style="float: right" class="btn btn-Grid" runat="server" Text="لیست دسته بندی محصولات" UseSubmitBehavior="false" Width="150px" OnClick="btnGridProductCategory_Click" />

                            <asp:Button ID="btnSearchProduct" runat="server" CssClass="btnSearch" OnClick="btnSearchProduct_Click" />
                            <div class="col-md-2 form-group" style="float: left">
                                <asp:TextBox ID="txtSearchProduct" runat="server" Width="100%" CssClass="form-control" placeholder=" عنوان محصول"></asp:TextBox>
                            </div>
                        </div>
                        <div class="panel-body">
                            <section id="unseen4">
                                <asp:GridView ID="grdProductDetails" runat="server" CssClass="table table-bordered table-striped table-condensed" AutoGenerateColumns="false" DataKeyNames="IDProduct" PageSize="10"
                                    AllowPaging="true" OnRowCommand="grdProductDetails_RowCommand" OnRowEditing="grdProductDetails_RowEditing"
                                    OnRowDeleting="grdProductDetails_RowDeleting" OnPageIndexChanging="grdProductDetails_PageIndexChanging" OnRowDataBound="grdProductDetails_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="عملیات" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Button ID="btnDel" CssClass="btn btn-Delete tool" ToolTip="حذف" runat="server" CommandName="Delete" OnClientClick="return confirm(' آیا مطمئن هستید ؟ ')" CommandArgument='<%#Eval("IDProduct")%>' />
                                                <asp:Button ID="btnEdit" CssClass="btn btn-edit tool" ToolTip="ویرایش" runat="server" CommandName="Edit" CommandArgument='<%#Eval("IDProduct")%>' />
                                                <asp:Button ID="btnPropertyAssignInTree" class="btn btn-Assign tool" runat="server" CommandName="DetailAssignProperty" title="اختصاص ویژگی  به این محصول" data-placement="bottom" CommandArgument='<%#Eval("IDProduct")%>' />
                                                <asp:Button ID="btnProperty" CssClass="btn btn-Property tool" ToolTip=" ایجاد ترکیب برای محصول " runat="server" CommandName="PropertyRule" CommandArgument='<%#Eval("IDProduct")%>' />
                                                <asp:Button ID="btnAttachSite" CssClass="btn btn-File tool" runat="server"
                                                    CommandName="AttachSite" CommandArgument='<%#Eval("IDProduct")%>' UseSubmitBehavior="false" data-placement="left" title="ثبت فایل ضمیمه" />

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="نام محصول(Fa)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="ProductName_Fa" runat="server" Text=' <%#Eval("Name_Fa") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="نام محصول(En)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="ProductName_En" runat="server" Text=' <%#Eval("Name_En") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="تاریخ ثبت" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="Date" runat="server" Text='<%#DateTime.Parse(Eval("CreateDate").ToString()).ToShamsiDateYMD() +" - "+DateTime.Parse(Eval("CreateDate").ToString()).ToString("HH:mm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="نویسنده" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="Author" runat="server" Text='<%#Eval("FullName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Picture" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Image Width="50px" Height="50px" ID="imgProduct" runat="server" ImageUrl=' <%#"../"+ Eval("ProductUrl")%>' Visible='<%#ShowImage(Eval("ProductUrl").ToString()) %>'></asp:Image>
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

        <!-- ModalShowSelectedNodInProductCategory -->
        <div class="modal fade" id="ModalShowSelectedNodInProductCategory" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class=" modal-dialog" style="width: 85%">
                <div class="modal-content" dir="rtl">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" style="font-size: 16px">گروه :  
                            <asp:Label ID="lblProductCategoryNameInHeader" runat="server" ForeColor="Red" Style="font-size: 16px"></asp:Label></h4>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="modal-body" dir="rtl">
                        <section class="panel">
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <div class="btn-group-justified" dir="rtl" style="text-align: center">
                                        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" TargetControlID="btnDelProductCategoryInTree" ConfirmText=" آیا مطمئن هستید؟" runat="server" />
                                        <asp:Button ID="btnDelProductCategoryInTree" class="btn btn-Delete tool" runat="server" UseSubmitBehavior="false" Width="80px" OnClick="btnDelProductCategoryInTree_Click" title="حذف دسته بندی" data-placement="bottom" />
                                        <asp:Button ID="btnEditProductCategoryInTree" class="btn btn-edit tool" runat="server" UseSubmitBehavior="false" Width="80px" OnClick="btnEditProductCategoryInTree_Click" title="ویرایش دسته بندی" data-placement="bottom" />
                                        <asp:Button ID="btnAddProductInTree" class="btn btn-Product tool" ValidationGroup="submit" runat="server" OnClick="btnAddProductInTree_Click" Width="80px" title="ایجاد محصول برای این دسته" data-placement="bottom" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </section>
                    </div>
                </div>
            </div>
        </div>
        <!-- ShowProductcategoryInTree -->



    </div>
    <uc1:AttachSite runat="server" ID="AttachSiteProduct" />
    <uc1:DefineDetailProduct runat="server" ID="DefineDetailProduct" />
    <uc1:DetailAssignProperty runat="server" ID="DetailAssignProperty" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script type="text/javascript" src="js/bootstrap-fileupload/bootstrap-fileupload.js"></script>
    <script type="text/javascript">
        function pageLoad(sender, args) {
            $(".tool").tooltip();
            $(".pop").popover({ offset: 1 });

            $("#<%=txtParentGroup.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../WebService/Auto.asmx/getProducCategory",
                        data: "{ 'prefix': '" + request.term + "','IDSupplier':'<%=IDSupplier%>'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('~')[0],
                                    val: item.split('~')[1]

                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {

                    $("#<%=hfIDParentGroup.ClientID %>").val(i.item.val);
                    $("#<%=txtParentGroup.ClientID %>").prop('disabled', true);
                },
                minLength: 1
            });

            $("#<%=txtCategoryForProduct.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../WebService/Auto.asmx/getProducCategory",
                        data: "{ 'prefix': '" + request.term + "','IDSupplier':'<%=IDSupplier%>'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('~')[0],
                                    val: item.split('~')[1]

                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {

                    $("#<%=hfIDCategoryForProductInAutoComplete.ClientID %>").val(i.item.val);
                    $("#<%=txtCategoryForProduct.ClientID %>").prop('disabled', true);
                },
                minLength: 1
            });

            $(".ClearProductCategory").click(function () {

                $("#<%=txtParentGroup.ClientID %>").val("");
                $("#<%=hfIDParentGroup.ClientID %>").val("");
                $("#<%=txtParentGroup.ClientID %>").prop('disabled', false);
            });

            $(".ClearCategoryForProduct").click(function () {

                $("#<%=txtCategoryForProduct.ClientID %>").val("");
                $("#<%=hfIDCategoryForProductInAutoComplete.ClientID %>").val("");
                $("#<%=txtCategoryForProduct.ClientID %>").prop('disabled', false);
            });
        };
    </script>

    <script type="text/javascript" src="js/bootstrap-fileupload/bootstrap-fileupload.js"></script>
</asp:Content>
