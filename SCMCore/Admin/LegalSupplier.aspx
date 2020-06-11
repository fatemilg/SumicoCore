<%@ Page Title="مدیریت تامین کنندگان " Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="LegalSupplier.aspx.cs" Inherits="SCMCore.Admin.LegalSupplier" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Admin/UserControl/TreeView.ascx" TagPrefix="uc1" TagName="TreeView" %>
<%@ Register Src="~/Admin/UserControl/CasscadDropDownCountry.ascx" TagPrefix="uc1" TagName="CasscadDropDownCountry" %>
<%@ Register Src="~/Admin/UserControl/FileUpload.ascx" TagPrefix="uc1" TagName="FileUpload" %>
<%@ Register Src="~/Admin/UserControl/AttachSite.ascx" TagPrefix="uc1" TagName="AttachSite" %>



<%@ Import Namespace="SCMCore.ExtensionMethod" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function validateCompany() {
            var summary = "";
            summary += isvalidCompanyName();
            if (summary != "") {
                alert(summary);
                return false;
            }
            else {
                return true;
            }
        }
        function isvalidCompanyName() {
            if (document.getElementById("<%=txtName.ClientID %>").value == "") {
                return ("لطفا نام شرکت را وارد کنید" + "\n");
            }
            else {
                return "";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAdd" />

        </Triggers>
        <ContentTemplate>
            <asp:HiddenField ID="hfMode" runat="server" />
            <asp:HiddenField ID="hfIDLegalUser" runat="server" />
            <asp:HiddenField ID="hfParentCompany" runat="server" />
            <asp:HiddenField ID="hfModeRealUser" runat="server" />
            <asp:HiddenField ID="hfIDRealUser" runat="server" />
            <asp:HiddenField ID="hfIDSupplierForProduct" runat="server" />
            <asp:HiddenField ID="hfUploadTo" runat="server" />
            <div class="col-lg-12" id="divInfo" runat="server">
                <section class="panel" dir="rtl">
                    <header class="panel-heading">
                        <span style="font-size: 20px;">تامین کنندگان
                        </span>

                    </header>
                    <div class="btn-group-justified" dir="rtl">
                        <asp:Button ID="btnNew" class="btn btn-New" runat="server" Text="جدید" Style="float: right" UseSubmitBehavior="false" Width="80px" OnClick="btnNew_Click" />
                        <asp:Button ID="btnAdd" class="btn btn-Add" Style="float: right" runat="server" Text="ثبت" Width="80px" OnClick="btnAdd_Click" />
                        <asp:Button ID="btnGrid" CssClass="btn btn-Grid" runat="server" Style="float: right" Text="لیست تامین کنندگان" UseSubmitBehavior="false" Width="130px" OnClick="btnGrid_Click" />
                    </div>
                    <div class="panel-body">
                        <div class="position-center">
                            <div class="col-md-6 form-group ">
                                <label for="Name">نام شرکت :</label>
                                <asp:TextBox ID="txtName" class=" form-control" runat="server" MaxLength="200" Width="100%"></asp:TextBox>
                            </div>
                            <div class="col-md-6 form-group">
                                <label for="Name">نام شرکت(انگلیسی) :</label>
                                <asp:TextBox ID="txtName_En" class=" form-control" runat="server" MaxLength="200" Width="100%"></asp:TextBox>
                            </div>
                            <div class="col-md-6 form-group">
                                <label for="Name">شرکت اصلی :</label>
                                <asp:TextBox ID="txtBaseCompany" Style="width: 92%; border-radius: 30px; display: inline" CssClass="form-control" runat="server"></asp:TextBox>
                                <input type="button" id="clearBaseCompany" class="btn btn-Clean tool" data-placement="left" title="پاک کردن شرکت اصلی" />
                            </div>

                            <div class="col-md-6 form-group">
                                <label for="Name">شرکت فعال است :</label>
                                <asp:CheckBox ID="chkActive" class=" form-control" runat="server" Checked="true" Width="100%"></asp:CheckBox>
                            </div>
                            <div class="clearfix"></div>

                            <uc1:CasscadDropDownCountry runat="server" ID="CasscadDropDownCountryLegalSupplier" />
                            <div class="clearfix"></div>
                            <div class="col-md-4 form-group">
                                <label for="Name">شناسه ملی:</label>
                                <asp:TextBox ID="txtNationalCode" class=" form-control" runat="server" MaxLength="20" Width="100%"></asp:TextBox>
                            </div>
                            <div class="col-md-4 form-group">
                                <label for="Name">کد اقتصادی :</label>
                                <asp:TextBox ID="txtRegistrationCode" class=" form-control" runat="server" MaxLength="20" Width="100%"></asp:TextBox>
                            </div>
                            <div class="col-md-4 form-group">
                                <label for="Name">کد پستی:</label>
                                <asp:TextBox ID="txtPostalCode" class=" form-control" runat="server" MaxLength="20" Width="100%"></asp:TextBox>
                            </div>
                            <div class="col-md-4 form-group">
                                <label for="Name">وب سایت :</label>
                                <asp:TextBox ID="txtWebSite" class=" form-control" runat="server" MaxLength="100" Width="100%"></asp:TextBox>
                            </div>
                            <div class="col-md-4 form-group">
                                <label for="Name">شماره ثبت :</label>
                                <asp:TextBox ID="txtRegistrationNumber" class=" form-control" runat="server" TextMode="Number" Width="100%"></asp:TextBox>
                            </div>
                            <div class="col-md-12 form-group">
                                <label for="MetaTags">MetaTags :</label>
                                <asp:TextBox ID="txtMetaTag" class=" form-control" runat="server" MaxLength="1000" Width="100%" TextMode="MultiLine" Style="overflow: hidden"></asp:TextBox>
                            </div>
                            
                            <div class="col-md-4 form-group ">
                                <label for="Pic">لگو تامین کننده(نمایش در سایت) : </label>
                                <div class="form-group last">
                                    <div class="col-md-11">
                                        <div class="fileupload fileupload-new" data-provides="fileupload">
                                            <div class="fileupload-preview fileupload-exists thumbnail" title="عکس جدید" style="max-width: 200px; max-height: 150px; line-height: 20px;"></div>
                                            <asp:Image ID="imgLegalUser" ToolTip="عکس ثبت شده" Style="max-width: 200px; max-height: 150px; line-height: 20px;" runat="server" Visible="false" />
                                            <div>
                                                <span>
                                                    <asp:FileUpload ID="fulPicUrl" CssClass="default" runat="server" />
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="col-md-12 form-group">
                                <label for="Name">توضیحات (نمایش در سایت) :</label>
                                <CKEditor:CKEditorControl ID="txtDescription" runat="server"></CKEditor:CKEditorControl>
                            </div>
                            <br />
                            <br />
                            <div class="col-md-12 form-group ">
                                <label for="Name">توضیحات انگلیسی (نمایش در سایت) :</label>
                                <CKEditor:CKEditorControl ID="txtDescription_En" runat="server"></CKEditor:CKEditorControl>
                            </div>
                        </div>
                    </div>
                </section>
            </div>
            <div class="col-lg-6" id="divTable" runat="server" style="float: right;">
                <section class="panel" dir="rtl">
                    <header class="panel-heading">
                        <span style="font-size: 20px;">لیست تامین کنندگان 
                        </span>
                    </header>
                    <div dir="rtl" style="margin-top: 5px">
                        <asp:Button ID="btnNew2" CssClass="btn btn-New" runat="server" Text="جدید" UseSubmitBehavior="false" Width="80px" OnClick="btnNew2_Click" />
                        <asp:Button ID="btnSearchCompany" runat="server" CssClass="btnSearch" OnClick="btnSearchCompany_Click" />
                        <div class="col-md-3 form-group" style="float: left">
                            <asp:TextBox ID="txtSearchCompany" runat="server" Width="100%" CssClass="form-control" placeholder=" عنوان شرکت "></asp:TextBox>
                        </div>
                    </div>
                    <div class="panel-body">
                        <section id="unseen">

                         
                            <asp:GridView ID="grdLegalUser" runat="server" CssClass="table table-bordered table-striped table-condensed" AutoGenerateColumns="false"
                                DataKeyNames="IDUser" PageSize="10" AllowPaging="true" OnRowCommand="grdLegalUser_RowCommand" OnRowEditing="grdLegalUser_RowEditing" OnRowDeleting="grdLegalUser_RowDeleting"
                                OnPageIndexChanging="grdLegalUser_PageIndexChanging" OnRowDataBound="grdLegalUser_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="عملیات" HeaderStyle-Width="40%">
                                        <ItemTemplate>
                                            <asp:Button ID="btnEdit" CssClass="btn btn-edit tool" data-placement="top" title="ویرایش" runat="server" CommandName="Edit" CommandArgument='<%#Eval("IDUser")%>' />
                                            <asp:Button ID="btnMember" CssClass="btn btn-Member tool" data-placement="top" title="پرسنل" runat="server" CommandName="Member" CommandArgument='<%#Eval("IDUser")%>' />
                                            <asp:Button ID="btnProduct" CssClass="btn btn-Product tool" data-placement="top" title="محصولات" runat="server" CommandName="Product" CommandArgument='<%#Eval("IDUser")%>' />
                                            <asp:Button ID="btnAccessory" CssClass="btn btn-Accessory tool" data-placement="top" title="لوازم جانبی" runat="server" CommandName="Accessory" CommandArgument='<%#Eval("IDUser")%>' />
                                            <asp:Button ID="btnDependency" CssClass="btn btn-Link tool" data-placement="top" title="وابسته ها" runat="server" CommandName="Dependency" CommandArgument='<%#Eval("IDUser")%>' />

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="نام شرکت">
                                        <ItemTemplate>
                                            <asp:Label ID="Name" runat="server" Text=' <%#Eval("Name_Fa") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="لوگو">
                                        <ItemTemplate>
                                            <asp:Image ID="imgLogo" runat="server" ImageUrl='<%#"../"+Eval("PicUrl")%>' Style="width: 150px;"></asp:Image>
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
            <div class="col-lg-6" id="divMembers" runat="server" style="float: right;">
                <section class="panel" dir="rtl">
                    <header class="panel-heading">
                        <span style="font-size: 20px;">تعریف پرسنل در
                            <asp:Label ID="lblCompany" runat="server" Text="" />
                    </header>
                    <div class="btn-group-justified" dir="rtl">
                        <asp:Button ID="btnNewMember" class="btn btn-New" runat="server" Text="جدید" Style="float: right" UseSubmitBehavior="false" Width="80px" OnClick="btnNewMember_Click" />
                        <asp:Button ID="btnAddMember" class="btn btn-Add" Style="float: right" runat="server" Text="ثبت" Width="80px" OnClick="btnAddMember_Click" UseSubmitBehavior="false" ValidationGroup="AddMember" /><%-- OnClientClick="return BtnClick()"--%>
                        <asp:Button ID="btnGridMember" CssClass="btn btn-Grid" runat="server" Style="float: right" Text="لیست پرسنل شرکت..." UseSubmitBehavior="false" Width="150px" OnClick="btnGridMember_Click" />
                    </div>
                    <div class="panel-body">
                        <div class="position-center" style="width: 100%">
                            <div class="col-md-12 form-group ">
                                <label for="Name">جنسیت :</label>
                                <asp:RadioButtonList ID="rbSex" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="مرد" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="زن" Value="1"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="col-md-4 form-group">
                                <label for="FName">نام :</label>
                                <asp:TextBox ID="txtFName" CssClass=" form-control" runat="server" MaxLength="200" Width="100%"></asp:TextBox>
                            </div>
                            <div class="col-md-4 form-group ">
                                <label for="LName">نام خانوادگی :</label>
                                <asp:TextBox ID="txtLName" CssClass=" form-control" runat="server" MaxLength="200" Width="90%" Style="display: inline-block"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Font-Size="Large" ForeColor="red" ErrorMessage="*" ControlToValidate="txtLName" ValidationGroup="AddMember" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-4 form-group">
                                <label for="Name">شماره شناسنامه:</label>
                                <asp:TextBox ID="txtMemberIdentifyNumber" class=" form-control" runat="server" MaxLength="20" Width="100%"></asp:TextBox>
                            </div>
                            <uc1:CasscadDropDownCountry runat="server" ID="CasscadDropDownCountryPersonelSupplier" />
                            <div class="clearfix"></div>

                            <div class="col-md-4 form-group">
                                <label for="Name">کد ملی :</label>
                                <asp:TextBox ID="txtMemberMeliCode" class=" form-control" runat="server" MaxLength="20" Width="100%"></asp:TextBox>
                            </div>

                            <div class="col-md-4 form-group">
                                <label for="Name">وب سایت :</label>
                                <asp:TextBox ID="txtMemberWebsite" class=" form-control" runat="server" MaxLength="100" Width="100%"></asp:TextBox>
                            </div>
                            <div class="col-md-4 form-group">
                                <label for="Name">فرد فعال است :</label>
                                <asp:CheckBox ID="chkMemberActive" class=" form-control" runat="server" Checked="true" Width="100%"></asp:CheckBox>
                            </div>
                            <div class="col-md-12 form-group " runat="server" visible="false">
                                <asp:Button ID="RemovePostOrganization" runat="server" CssClass="btn btn-Remove" OnClick="RemovePostOrganization_Click" />
                                <label for="Name">پست سازمانی:</label>
                                <asp:Label ID="lblOrganizationPost" runat="server" Text="" Style="direction: rtl"></asp:Label>
                                <asp:TreeView ID="tvOrganizationPost" runat="server" ImageSet="Arrows" ExpandDepth="0" NodeIndent="15"
                                    ShowLines="true" OnSelectedNodeChanged="tvOrganizationPost_SelectedNodeChanged" SelectAction="None">
                                    <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                    <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                                        NodeSpacing="0px" VerticalPadding="2px"></NodeStyle>
                                    <ParentNodeStyle Font-Bold="False" />
                                    <SelectedNodeStyle />
                                </asp:TreeView>
                            </div>
                            <div class="col-md-12 form-group" id="divDescriptionRealUser" runat="server" visible="false">
                                <label for="Name">توضیحات :</label>
                                <asp:TextBox ID="txtDescriptionRealUser" runat="server" class=" form-control" TextMode="MultiLine" Width="100%" MaxLength="1000" />
                            </div>
                            <div class="col-md-12 form-group" id="divDescription_EnRealUser" runat="server" visible="false">
                                <label for="Name">توضیحات(انگلیسی) :</label>
                                <asp:TextBox ID="txtDescription_EnRealUser" runat="server" class=" form-control" TextMode="MultiLine" Width="100%" MaxLength="1000" />
                            </div>
                        </div>
                    </div>
                </section>
            </div>
            <div class="col-lg-6" id="divTableMembers" runat="server" style="float: right;">
                <section class="panel" dir="rtl">
                    <header class="panel-heading">
                        <span style="font-size: 20px;">لیست افراد در 
                            <asp:Label ID="lblCompanyForMember" runat="server" Text="" />
                        </span>
                    </header>
                    <div dir="rtl" style="margin-top: 5px">
                        <asp:Button ID="btnNewMemberForCompany" CssClass="btn btn-New" runat="server" Text="جدید" UseSubmitBehavior="false" Width="80px" OnClick="btnNewMemberForCompany_Click" />
                        <asp:Button ID="btnSearchMember" runat="server" CssClass="btnSearch" OnClick="btnSearchMember_Click" />
                        <div class="col-md-3 form-group" style="float: left">
                            <asp:TextBox ID="txtSearchMember" runat="server" Width="100%" CssClass="form-control" placeholder=" نام - نام خانوادگی"></asp:TextBox>
                        </div>
                    </div>
                    <div class="panel-body">
                        <section id="unseen">
                            <asp:HiddenField ID="hfPhoneMember" runat="server" />
                            <asp:HiddenField ID="hfIDPhoneMember" runat="server" />
                            <asp:HiddenField ID="hfModePhoneMember" runat="server" />
 
                            <asp:GridView ID="grdMember" runat="server" CssClass="table table-bordered table-striped table-condensed" AutoGenerateColumns="false"
                                DataKeyNames="IDUser" PageSize="10" AllowPaging="true" OnRowCommand="grdMember_RowCommand" OnRowEditing="grdMember_RowEditing"
                                OnRowDeleting="grdMember_RowDeleting" OnPageIndexChanging="grdMember_PageIndexChanging" OnRowDataBound="grdMember_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="عملیات">
                                        <ItemTemplate>
                                            <asp:Button ID="btnEdit" CssClass="btn btn-edit" ToolTip="ویرایش" runat="server" CommandName="Edit" CommandArgument='<%#Eval("IDUser")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="نام و نام خانوادگی ">
                                        <ItemTemplate>
                                            <asp:Label ID="FullName" runat="server" Text=' <%#Eval("FullName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="پست سازمانی">
                                        <ItemTemplate>
                                            <asp:Label ID="OrganizationPosition" runat="server" Text=' <%#Eval("OrganizationPositionName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="جنسیت">
                                        <ItemTemplate>
                                            <asp:Label ID="Sex" runat="server" Text=' <%#ShowSex(Eval("Sex").ToString().StringToBool()) %>'></asp:Label>
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

    <div class="modal fade" id="ModalSupplierDependency" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content" dir="rtl">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">وابسته های 
                                    <asp:Label ID="lblSupplierName" runat="server" ForeColor="Red"></asp:Label></h4>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="modal-body row" dir="rtl">
                    <section class="panel">

                        <header class="panel-heading tab-bg-dark-navy-blue tab-right ">
                            <ul class="nav nav-tabs pull-right" style="margin: 0">
                                <li class="active">
                                    <a data-toggle="tab" href="#LogoInMenu" dir="ltr"><span dir="rtl" style="margin-right: 5px">لوگو </span>
                                        <i class="fa fa-bars"></i>
                                    </a>
                                </li>
                                <li class="">
                                    <a data-toggle="tab" href="#Catalog" dir="ltr"><span dir="rtl" style="margin-right: 5px">کاتالوگ </span>
                                        <i class="fa fa-bars"></i>
                                    </a>
                                </li>
                            </ul>
                        </header>
                        <div class="panel-body">
                            <div class="tab-content">
                                <div id="LogoInMenu" class="tab-pane active">
                                    <div class="col-md-4">
                                        <uc1:FileUpload runat="server" ID="FileUploadMenuPicUrl" />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btnDelOldSupplierMenuPic" class="btn btn-Delete tool" data-placement="top" title="پاک کردن تصویر قبلی" runat="server" OnClick="btnDelOldSupplierMenuPic_Click" OnClientClick="return confirm(' آیا مطمئن هستید ؟ ')" />
                                                <asp:Image runat="server" ID="imgOldSupplierMenuPic" CssClass="img-responsive" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btnSaveSupplierMenuPic" class="btn btn-Add" Style="float: right" runat="server" Text="ثبت" Width="80px" OnClick="btnSaveSupplierMenuPic_Click" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div id="Catalog" class="tab-pane">
                                    <div class="col-md-6">
                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                            <ContentTemplate>
                                                <div class="form-group">
                                                    <label for="txtCatalouge">نام کاتالوگ:</label>
                                                    <asp:TextBox ID="txtCatalougeName" class=" form-control" runat="server" MaxLength="200" Width="100%"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label for="txtCatalogSort">ترتیب نمایش:</label>
                                                    <asp:TextBox runat="server" ID="txtCatalogSort" CssClass="form-control" TextMode="Number" Width="100%"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-6">
                                                    <label for="fulCatalogImage">تصویر اصلی کاتالوگ:</label><div class="clearfix"></div>
                                                    <asp:ImageButton runat="server" ID="btnCatalogPic" ImageUrl="/Admin/images/DefaultImage.png" Style="width: 100px; border: dashed 3px lightgray; padding: 3px" OnClick="btnCatalogPic_Click" />
                                                    <asp:Label runat="server" ID="lblCatalogPicName"></asp:Label>
                                                    <%--<asp:Button runat="server" ID="btDeleteCatalogPic" Visible="false" CssClass="btn btn-Delete" OnClientClick="return confirm(' آیا مطمئن هستید ؟ ')" OnClick="btDeleteCatalogPic_Click" />--%>
                                                    <asp:HiddenField runat="server" ID="hfIDCatalogPic" />
                                                </div>
                                                <div class="form-group col-md-6">
                                                    <label for="fulCatalogImage">فایل کاتالوگ:</label>
                                                    <div class="clearfix"></div>
                                                    <asp:ImageButton runat="server" ID="btnAttachCatalogFile" ImageUrl="/Admin/images/DefaultAttachFile.png" Style="width: 100px; border: dashed 3px lightgray; padding: 3px" OnClick="btnAttachCatalogFile_Click" />
                                                    <asp:Label runat="server" ID="lblAttachCatalogFileName"></asp:Label>
                                                    <asp:HiddenField runat="server" ID="hfIDAttachCatalogFile" />
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <asp:Button runat="server" ID="btnSaveCatalog" CssClass="btn btn-Add-FullWidth" Text="ذخیره کاتالوگ" OnClick="btnSaveCatalog_Click" />
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:UpdatePanel runat="server" ID="upadtepanelcatalog">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdSupplierCatalog" runat="server" CssClass="display table table-bordered table-striped dataTable" AutoGenerateColumns="false"
                                                    DataKeyNames="IDCatalog" OnRowCommand="grdSupplierCatalog_RowCommand" OnRowEditing="grdSupplierCatalog_RowEditing" OnRowDeleting="grdSupplierCatalog_RowDeleting">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="عملیات">
                                                            <ItemTemplate>
                                                                <asp:Button runat="server" CommandName="Delete" ID="btDelete" CssClass="btn btn-Delete" OnClientClick="return confirm(' آیا مطمئن هستید ؟ ')" CommandArgument='<%#Eval("IDCatalog")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="نام">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Name_En" runat="server" Text=' <%#Eval("Name_En") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="تصویر">
                                                            <ItemTemplate>
                                                               <a href="/<%#Eval("PDFUrl") %>" target="_blank"> <img src="/<%#Eval("PicUrl") %>" style="width: 70px" /></a>
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
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>
                                </div>
                            </div>
                        </div>


                    </section>
                </div>

            </div>
        </div>
    </div>

    <uc1:AttachSite runat="server" ID="FileUploadAttachSiteForCatalog" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script type="text/javascript">
        function pageLoad(sender, args) {
            $(".tool").tooltip();
            $("#<%=txtBaseCompany.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../WebService/AutoComplete.asmx/getLegalSupplier",
                        data: "{ 'prefix': '" + request.term + "'}",
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
                    $("#<%=hfParentCompany.ClientID %>").val(i.item.val);
                    $("#<%=txtBaseCompany.ClientID %>").prop('disabled', true);
                },
                minLength: 1
            });
            $("#clearBaseCompany").click(function () {
                $("#<%=txtBaseCompany.ClientID %>").val("");
                $("#<%=hfParentCompany.ClientID %>").val("");
                $("#<%=txtBaseCompany.ClientID %>").prop('disabled', false);
            });
        };
    </script>
    <script type="text/javascript" src="js/bootstrap-fileupload/bootstrap-fileupload.js"></script>
</asp:Content>
