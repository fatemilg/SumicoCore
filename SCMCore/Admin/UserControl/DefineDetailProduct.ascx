<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DefineDetailProduct.ascx.cs" Inherits="SCMCore.Admin.UserControl.DefineDetailProduct" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="SCMCore.ExtensionMethod" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Src="~/Admin/UserControl/AttachSite.ascx" TagPrefix="uc1" TagName="AttachSiteDefine" %>
<%@ Register Src="~/Admin/UserControl/FileUpload.ascx" TagPrefix="uc1" TagName="FileUpload" %>
<%@ Register Src="~/Admin/UserControl/TechnicalProperty.ascx" TagPrefix="uc1" TagName="TechnicalProperty" %>
<%@ Register Src="~/Admin/UserControl/TreeProduct.ascx" TagPrefix="uc1" TagName="TreeProduct" %>
<%@ Register Src="~/Admin/UserControl/TreeAccessory.ascx" TagPrefix="uc1" TagName="TreeAccessory" %>
<%@ Register Src="~/Admin/UserControl/TreeCategoryProduct.ascx" TagPrefix="uc1" TagName="TreeCategoryProduct" %>




<uc1:AttachSiteDefine runat="server" ID="AttachSiteDefines" />
<uc1:TechnicalProperty runat="server" ID="TechnicalProperty" />
<!-- Modal Rule -->
<div class="modal fade" id="ModalPropertyProductCategoryEvents" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" runat="server" style="z-index: 1500">
    <div class="position modal-dialog" style="width: 98%">
        <div class="modal-content" dir="rtl">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">مقداردهی ویژگی ظاهری برای   
                            <asp:Label ID="lblHeaderOfModal" runat="server" ForeColor="Red" Style="font-size: 14px"></asp:Label></h4>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="modal-body row" dir="rtl">
                <section class="panel">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hfIDDefineDetailProduct" runat="server" />
                            <asp:HiddenField ID="hfDefineDetailPicUrl" runat="server" />
                            <asp:HiddenField ID="hfIndexDescriptionPicUrl" runat="server" />
                            <asp:HiddenField ID="hfModeDefineDetailProduct" runat="server" />
                            <asp:HiddenField ID="hfIDProduct" runat="server" />
                            <asp:HiddenField ID="hfCheckProduct" runat="server" />
                            <asp:HiddenField ID="hfIDSupplier" runat="server" />

                            <div class="panel-body">
                                <div class="col-lg-12 col-md-12">
                                    <div class="col-lg-7 col-md-7" style="direction: rtl">
                                        <asp:Image ID="imgMasterProduct" runat="server" Width="150" />
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <asp:Button ID="btnNewRuleProperty" CssClass="btnClean tool" Style="float: right" runat="server" OnClick="btnNewRuleProperty_Click" data-placement="top" title="UnSelect ویژگی های انتخاب شده و مشاهده لیست ترکیب ها بدون اعمال هیچ فیلتری!" />
                                <asp:Button ID="btnAddRulePropertyAllRow" CssClass="btn btn-Sum tool" Style="float: right; margin-right: 5px; background-color: lightgreen" runat="server" OnClick="btnAddRulePropertyAllRow_Click" data-placement="bottom" title="ثبت ترکیب های دلخواه!" />
                                <asp:Button ID="btnSearchRuleProperty" runat="server" CssClass="btnSearch tool" Style="float: right; margin-right: 5px" OnClick="btnSearchRuleProperty_Click" data-placement="top" title="ویژگی های مورد نظر را انتخاب کنید و در لیست ترکیب های ثبت شده جست و جو کنید !" />
                                <asp:Button ID="btnSaveRuleChanges" runat="server" CssClass="btn btn-Add tool" Style="float: right; margin-right: 5px" OnClick="btnSaveRuleChanges_Click" data-placement="top" title="ویرایش ترکیب انتخاب شده" Text="ویرایش ترکیب" Visible="false" />
                                <asp:Button ID="btnSaveGenerateCode" runat="server" CssClass="btn btn-Grid tool" Style="float: right; margin-right: 5px;display:none" OnClick="btnSaveGenerateCode_Click" data-placement="top" title="تولید کد واسط با نرم افزار کارگاه" Text="تولید کد " />

                                <div class="clearfix"></div>
                                <asp:Repeater ID="rptParentProperty" runat="server" OnItemDataBound="rptParentProperty_ItemDataBound" ClientIDMode="AutoID">
                                    <ItemTemplate>
                                        <div class="col-lg-3 col-md-3" style="float: left; direction: ltr; margin-bottom: 10px">
                                            <asp:HiddenField ID="hfIDParentProperty" runat="server" Value='<%#Eval("IDProperty") %>' />
                                            <asp:HiddenField ID="hfMaterialType" runat="server" Value='<%#Eval("MaterialType") %>' />
                                            <label><b><%#Eval("PropertyName_En") %> :</b></label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="drpChildSubProperty" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="--Select--" Selected="True" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div class="clearfix"></div>
                                <br />
                                <header class="panel-heading">
                                    <span style="font-size: 20px;">لیست ترکیب ها --- آیتم های یافت شده :
                                             <asp:Label ID="lblGrdDefineRowsCount" runat="server" ForeColor="Red" Font-Size="20px"></asp:Label>
                                    </span>
                                </header>
                                <br />
                                <div class="col-lg-12 col-md-12">
                                    <asp:Button ID="btnSearchDefineDetailProduct" runat="server" CssClass="btnSearch" OnClick="btnSearchDefineDetailProduct_Click" />
                                    <div class="col-md-2 form-group" style="float: left">
                                        <asp:TextBox ID="txtSearchDefineDetailProduct" runat="server" Width="100%" CssClass="form-control" placeholder="Part Number"></asp:TextBox>
                                    </div>
                                </div>

                                <asp:GridView ID="grdDefineDetailProduct" runat="server" CssClass="display table table-bordered table-striped dataTable" AutoGenerateColumns="false"
                                    DataKeyNames="IDProductDefineDetailProduct" PageSize="5" AllowPaging="true" OnPageIndexChanging="grdDefineDetailProduct_PageIndexChanging" OnRowCommand="grdDefineDetailProduct_RowCommand" OnRowEditing="grdDefineDetailProduct_RowEditing" OnRowDeleting="grdDefineDetailProduct_RowDeleting" OnRowDataBound="grdDefineDetailProduct_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="عملیات " ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30%">
                                            <ItemTemplate>
                                                <asp:Button ID="btnDel" CssClass="btn btn-Delete tool" runat="server" CommandName="Delete" OnClientClick="return confirm(' آیا مطمئن هستید ؟ ')" CommandArgument='<%#Eval("IDDefineDetailProduct")%>' data-placement="right" title="حذف" />
                                                <asp:Button ID="btnEdit" CssClass="btn btn-edit tool" runat="server" CommandName="Edit" CommandArgument='<%#Eval("IDDefineDetailProduct")%>' data-placement="top" title="ویرایش" />
                                                <asp:Button ID="btnEditRules" CssClass="btn btn-editRulse tool" runat="server" CommandName="EditRules" CommandArgument='<%#Eval("IDDefineDetailProduct")%>' data-placement="top" title="ویرایش ویژگی های ظاهری" />
                                                <asp:Button ID="btnTechnicalProperty" CssClass="btn btn-TechnicalProperty tool" ToolTip=" ویژگی فنی " runat="server" CommandName="TechnicalProperty" CommandArgument='<%#Eval("IDDefineDetailProduct")%>' />
                                                <asp:Button ID="btnAttachSite" CssClass="btn btn-File tool" runat="server"
                                                    CommandName="AttachSite" CommandArgument='<%#Eval("IDDefineDetailProduct")%>' UseSubmitBehavior="false" data-placement="top" title="ثبت فایل ضمیمه" />
                                                <asp:Button ID="btnRelation" CssClass="btn btn-Relation tool" runat="server"
                                                    CommandName="Relation" CommandArgument='<%#Eval("IDDefineDetailProduct")%>' UseSubmitBehavior="false" data-placement="left" title="محصولات مرتبط" />
                                                <asp:Button ID="btnAccessory" CssClass="btn btn-Accessory tool" runat="server" Visible='<%#CheckVisibleForProduct(Eval("IDDefineDetailProduct").ToString().StringToGuid()) %>' CommandName="Accessory" CommandArgument='<%#Eval("IDDefineDetailProduct")%>' data-placement="left" title="لوازم جانبی" />
                                                <asp:Button ID="btnAssignMaster" CssClass="btn btn-Assign tool" runat="server" Visible='<%#CheckVisibleForProduct(Eval("IDDefineDetailProduct").ToString().StringToGuid()) %>' CommandName="AssignMasterProduct" CommandArgument='<%#Eval("IDDefineDetailProduct")%>' data-placement="left" title="انتساب به سایر محصولات" />

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ردیف" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="عنوان ترکیب" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div dir="ltr" style="text-align: left; font-family: Tahoma; font-size: 12px; direction: ltr">
                                                    <asp:Label ID="CombinationDescription" dir="ltr" runat="server" Text='<%#Eval("CombinationDescription") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PartNumber" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div dir="ltr" style="text-align: left; font-family: Tahoma; font-size: 12px">
                                                    <asp:Label ID="PartNumber" dir="rtl" runat="server" Text='<%#Eval("PartNumber") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="تامین کننده" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div dir="ltr" style="text-align: left; font-family: Tahoma; font-size: 12px">
                                                    <asp:Label ID="LegalUserName_En" dir="rtl" runat="server" Text='<%#Eval("LegalUserName_En") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="خلاصه توضیحات" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <i data-original-title="شرح" data-content=' <%#Eval("ShortTechnicalDescription") %>' dir="ltr" style="font-size: 30px;"
                                                    data-placement="bottom" data-trigger="hover" class="fa fa-pencil-square-o popovers pop"></i>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="توضیحات" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <i data-original-title="شرح" data-content='<%#Eval("TechnicalDescription") %>' dir="ltr" style="font-size: 30px;"
                                                    data-placement="bottom" data-trigger="hover" class="fa fa-pencil-square-o popovers pop"></i>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="تصویر" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Image Width="50px" Height="50px" ID="FileType" runat="server" ImageUrl=' <%#@"..\..\"+ Eval("DefinePicUrl")%>' Visible='<%#ShowImage(Eval("DefinePicUrl").ToString()) %>'></asp:Image>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Under Spot" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lbUnderSpot" CommandArgument='<%#Eval("IDDefineDetailProduct")%>' Visible='<%#CheckVisibleForProduct(Eval("IDDefineDetailProduct").ToString().StringToGuid()) %>' CommandName="UnderSpot"><%#Eval("UnderSpot").ToString().StringToBool()==true ? "<i class='fa fa-check-square-o' style='font-size:16px'></i>" : "<i class='fa fa-square-o' style='font-size:16px'></i>" %></asp:LinkButton>
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
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </section>
            </div>

        </div>
    </div>
</div>
<!-- Modal Rule -->




<!-- ModalDefineDetailPartNumber -->
<div class="modal fade" id="ModalDefineDetailPartNumber" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false" style="z-index: 1502">
    <div class="position-center modal-dialog" style="width: 90%">
        <div class="modal-content" dir="rtl">
            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                <ContentTemplate>
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" style="font-size: 14px">
                            <asp:Label ID="lblDefineDetailCombinationDescription" runat="server" ForeColor="Red"></asp:Label>
                        </h4>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="modal-body row" dir="rtl">
                <section class="panel">
                    <div class="panel-body">
                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hfGenrateCode" runat="server" />
                                <asp:HiddenField ID="hfCombinationDescription" runat="server" />
                                <asp:HiddenField ID="hfIDPropertyList" runat="server" />

                                <div class="btn-group-justified" dir="rtl">
                                    <asp:Button ID="btnAddRulePropertyOneRowByPartNumber" runat="server" Text="ثبت" CssClass="btn btn-success" OnClick="btnAddRulePropertyOneRowByPartNumber_Click" Width="80px" ValidationGroup="DefineDetailSubmit" />
                                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" TargetControlID="btnAddRulePropertyOneRowByPartNumber" ConfirmText=" آیا مطمئن هستید؟" runat="server" />
                                </div>
                                <br />
                                <div class="clearfix"></div>
                                <div style="margin-top: 5px;">
                                    <div class="col-lg-4" runat="server" style="float: right">
                                        <label>نمایش در سایت :</label>
                                        <asp:CheckBox ID="chkShowInSite" runat="server" Checked="true" />
                                    </div>
                                    <div class="col-lg-4" runat="server" style="float: right">
                                        <label>محصول ویژه:</label>
                                        <asp:CheckBox ID="chkSpecialDefine" runat="server" />
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div style="margin-top: 5px; margin-bottom: 10px;">
                                    <div class="col-lg-3  form-group" style="float: right">
                                        <label>واحد :</label>
                                        <asp:DropDownList ID="drpUnit" runat="server" CssClass="form-control" Width="100%"></asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3  form-group" style="float: right">
                                        <label>کشور سازنده :</label>
                                        <asp:DropDownList ID="drpMadeCountry" runat="server" CssClass="form-control" Width="100%"></asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3  form-group" style="float: right">
                                        <label>Part Number :</label>
                                        <asp:TextBox ID="txtPartNumber" class=" form-control" runat="server" Style="width: 100%; text-align: left"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-3  form-group" style="float: right">
                                        <label>Sort :</label>
                                        <asp:TextBox ID="txtSort" class=" form-control" runat="server" Style="width: 100%; text-align: left" TextMode="Number"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-lg-6  form-group" style="margin-top: 5px">
                                    <label>خلاصه توضیحات فنی انگلیسی  :</label>
                                    <asp:TextBox ID="txtShortTechnicalDescription" class=" form-control" runat="server" Style="width: 100%; text-align: left" MaxLength="1000" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <div class="col-lg-6  form-group" style="margin-top: 5px">
                                    <label>خلاصه توضیحات فنی فارسی  :</label>
                                    <asp:TextBox ID="txtShortTechnicalDescription_Fa" class=" form-control" runat="server" Style="width: 100%; text-align: left" MaxLength="1000" TextMode="MultiLine"></asp:TextBox>
                                </div>

                                <div class="col-lg-6  form-group" style="margin-top: 5px">
                                    <label>توضیحات فنی انگلیسی (Short Description En) :</label>
                                    <asp:TextBox ID="txtTechnicalDescription" class=" form-control" runat="server" Style="width: 100%; text-align: left" MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <div class="col-lg-6  form-group" style="margin-top: 5px">
                                    <label>توضیحات فنی فارسی (Short Description Fa) :</label>
                                    <asp:TextBox ID="txtTechnicalDescription_Fa" class=" form-control" runat="server" Style="width: 100%; text-align: left" MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <div class="col-lg-6  form-group" style="margin-top: 5px">
                                    <label for="MetaTags">MetaTag :</label>
                                    <asp:TextBox ID="txtMetaTag" class=" form-control" runat="server" MaxLength="1000" Style="width: 100%; text-align: left"></asp:TextBox>
                                </div>
                                <div class="col-lg-6  form-group" style="margin-top: 5px">
                                    <label for="MetaDescriptions">MetaDescription :</label>
                                    <asp:TextBox ID="txtMetaDescription" class=" form-control" runat="server" MaxLength="1000" Style="width: 100%; text-align: left"></asp:TextBox>
                                </div>
                                <div class="col-lg-12  form-group" runat="server" style="margin-top: 5px">
                                    <label>توضیحات-نمایش در سایت (En) :</label>
                                    <CKEditor:CKEditorControl ID="txtDescriptionInSite_En" runat="server"></CKEditor:CKEditorControl>
                                </div>
                                <div class="col-lg-12  form-group" runat="server" style="margin-top: 5px">
                                    <label>توضیحات-نمایش در سایت (Fa) :</label>
                                    <CKEditor:CKEditorControl ID="txtDescriptionInSite_Fa" runat="server"></CKEditor:CKEditorControl>
                                </div>
                                <div class="col-md-4 form-group">
                                    <label for="IndexDescriptionText">IndexDescription Text :</label>
                                    <asp:TextBox ID="txtIndexDescriptionText" CssClass=" form-control" runat="server" MaxLength="100" ToolTip="حداکثر 30 کاراکتر" Width="100%"></asp:TextBox>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="col-md-4 form-group">
                            <label for="Pic">تصویر IndexDescription : </label>
                            <uc1:FileUpload runat="server" ID="fulIndexDescription" />
                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                <ContentTemplate>
                                    <div id="DivIndexDescriptionPic" runat="server" visible="false">
                                        <asp:Button ID="btnDelOldIndexDescriptionPic" runat="server" CssClass="btn btn-Delete tool" OnClick="btnDelOldIndexDescriptionPic_Click" data-placement="bottom" title=" حذف عکس قبلی" />
                                        <div class="clearfix"></div>
                                        <asp:Image ID="imgIndexDescription" ToolTip="عکس ثبت شده" Style="max-width: 200px; max-height: 150px; float: right" runat="server" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-lg-4  form-group">
                            <label>عکس :</label>
                            <uc1:FileUpload runat="server" ID="FileUploadForDefineDetail" />

                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                <ContentTemplate>
                                    <div id="DivDeleteOldPic" runat="server" visible="false">
                                        <asp:Button ID="btnDeleteOldPic" runat="server" CssClass="btn btn-Delete tool" OnClick="btnDeleteOldPic_Click" data-placement="bottom" title=" حذف عکس قبلی" />
                                        <div class="clearfix"></div>
                                        <asp:Image ID="imgOldDefineDetailPic" runat="server" Width="50px" Height="50px" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </section>
            </div>
        </div>
    </div>
</div>
<!-- ModalTechnicalProperty -->

<div class="modal fade" id="ModalRelatedDefineDetailProduct" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="z-index: 1503">
    <div class="position-center modal-dialog">
        <div class="modal-content" dir="rtl">
            <asp:UpdatePanel ID="UpdatePanel115" runat="server">
                <ContentTemplate>
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" style="font-size: 16px">محصولات مرتبط با 
                            <asp:Label ID="lblBaseRelatedDefineName" runat="server" ForeColor="Red"></asp:Label>
                        </h4>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="modal-body row" dir="rtl">
                <section class="panel">
                    <div class="panel-body">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="col-lg-8">
                                    <uc1:TreeProduct runat="server" ID="TreeProduct" OnlbSelectedDefineClick="TreeProduct_lbSelectedDefineClick" />
                                </div>
                                <div class="col-lg-4">
                                    <asp:GridView ID="grdRelatedDefineDetailProduct" runat="server" CssClass="display table table-bordered table-striped dataTable" AutoGenerateColumns="false"
                                        DataKeyNames="IDRelatedDefineDetailProduct" PageSize="5" AllowPaging="true" OnPageIndexChanging="grdRelatedDefineDetailProduct_PageIndexChanging"
                                        OnRowCommand="grdRelatedDefineDetailProduct_RowCommand" OnRowEditing="grdRelatedDefineDetailProduct_RowEditing" OnRowDeleting="grdRelatedDefineDetailProduct_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="عملیات " ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnDel" CssClass="btn btn-Delete tool" runat="server" CommandName="Delete" OnClientClick="return confirm(' آیا مطمئن هستید ؟ ')" CommandArgument='<%#Eval("IDRelatedDefineDetailProduct")%>' data-placement="right" title="حذف" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PartNumber" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div dir="ltr" style="text-align: left; font-family: Tahoma; font-size: 12px">
                                                        <asp:Label ID="PartNumber" dir="rtl" runat="server" Text='<%#Eval("PartNumber") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="تصویر" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Image Width="50px" Height="50px" ID="FileType" runat="server" ImageUrl=' <%#@"..\..\"+ Eval("PicUrl")%>' Visible='<%#ShowImage(Eval("PicUrl").ToString()) %>'></asp:Image>
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
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </section>
            </div>
        </div>
    </div>
</div>


<div class="modal fade" id="ModalAccessory" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="z-index: 1503">
    <div class="position-center modal-dialog">
        <div class="modal-content" dir="rtl">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" style="font-size: 16px">لوازم جانبی برای  
                            <asp:Label ID="lblBaseAccessoryDefineName" runat="server" ForeColor="Red"></asp:Label>
                        </h4>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="modal-body row" dir="rtl">
                <section class="panel">
                    <div class="panel-body">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <div class="col-lg-8">
                                    <uc1:TreeAccessory runat="server" ID="TreeAccessory" OnlbSelectedDefineClick="TreeAccessory_lbSelectedDefineClick" />
                                </div>
                                <div class="col-lg-4">
                                    <asp:GridView ID="grdAccessoryProduct" runat="server" CssClass="display table table-bordered table-striped dataTable" AutoGenerateColumns="false"
                                        DataKeyNames="IDAccessoryProduct" PageSize="5" AllowPaging="true" OnPageIndexChanging="grdAccessoryProduct_PageIndexChanging"
                                        OnRowCommand="grdAccessoryProduct_RowCommand" OnRowEditing="grdAccessoryProduct_RowEditing" OnRowDeleting="grdAccessoryProduct_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="عملیات " ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnDel" CssClass="btn btn-Delete tool" runat="server" CommandName="Delete" OnClientClick="return confirm(' آیا مطمئن هستید ؟ ')" CommandArgument='<%#Eval("IDAccessoryProduct")%>' data-placement="right" title="حذف" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PartNumber" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div dir="ltr" style="text-align: left; font-family: Tahoma; font-size: 12px">
                                                        <asp:Label ID="PartNumber" dir="rtl" runat="server" Text='<%#Eval("AccessoryPartNumber") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="تصویر" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Image Width="50px" Height="50px" ID="FileType" runat="server" ImageUrl=' <%#@"..\..\"+ Eval("AccessoryPicUrl")%>' Visible='<%#ShowImage(Eval("AccessoryPicUrl").ToString()) %>'></asp:Image>
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
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </section>
            </div>
        </div>
    </div>
</div>


<div class="modal fade" id="ModalAssignMasterProduct" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="z-index: 1503">
    <div class="position-center modal-dialog">
        <div class="modal-content" dir="rtl">
            <asp:UpdatePanel ID="UpdatePanel105" runat="server">
                <ContentTemplate>
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" style="font-size: 16px">انتساب پارت نامبر   
                            <asp:Label ID="lblDefineInModalAssignMasterProduct" runat="server" ForeColor="Red"></asp:Label>
                            به محصولات دیگر
                        </h4>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="modal-body row" dir="rtl">
                <section class="panel">
                    <div class="panel-body">
                        <asp:UpdatePanel ID="UpdatePanel53" runat="server">
                            <ContentTemplate>
                                <div class="col-lg-8">
                                    <uc1:TreeCategoryProduct runat="server" ID="TreeCategoryProduct" />
                                </div>
                                <div class="col-lg-4">
                                    <asp:GridView ID="grdProductDefineDetailProduct" runat="server" CssClass="display table table-bordered table-striped dataTable" AutoGenerateColumns="false"
                                        DataKeyNames="IDProductDefineDetailProduct" PageSize="5" AllowPaging="true" OnRowCommand="grdProductDefineDetailProduct_RowCommand" OnRowEditing="grdProductDefineDetailProduct_RowEditing" OnRowDeleting="grdProductDefineDetailProduct_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="تصویر" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Image Width="50px" Height="50px" ID="FileType" runat="server" ImageUrl=' <%#@"..\..\"+ Eval("ProductUrl")%>' Visible='<%#ShowImage(Eval("ProductUrl").ToString()) %>'></asp:Image>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="عنوان محصول" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div dir="ltr" style="text-align: left; font-family: Tahoma; font-size: 12px">
                                                        <asp:Label ID="ProductName_En" dir="rtl" runat="server" Text='<%#Eval("ProductName_En") %>'></asp:Label>
                                                    </div>
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
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </section>
            </div>
        </div>
    </div>
</div>
