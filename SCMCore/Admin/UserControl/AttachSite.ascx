<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AttachSite.ascx.cs" Inherits="SCMCore.Admin.UserControl.AttachSite" %>
<%@ Import Namespace="SCMCore.ExtensionMethod" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!-- Modal attach site -->
<div class="modal fade" id="ModalAttachSite" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" runat="server" style="z-index: 1501">
    <div class="modal-dialog position-center">
        <div class="modal-content" dir="rtl">
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" style="font-size: 16px">ضمیمه کردن فایل جهت نمایش در سایت برای   
                            <asp:Label ID="lblHeaderOfModal" runat="server" ForeColor="Red" Style="font-size: 16px"></asp:Label></h4>

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div class="modal-body row">
                <section class="panel" dir="rtl">
                    <header class="panel-heading tab-bg-dark-navy-blue tab-right " style="direction: ltr; text-align: right">
                        <ul class="nav nav-tabs pull-right" style="margin: 0">
                            <li class="">
                                <a data-toggle="tab" href="#<%=TabAttachInterfaceCategory.ClientID %>">دسته بندی فایل ها  
                                                         <i class="fa fa-bars"></i>
                                </a>
                            </li>
                           <%-- <li class="">
                                <a data-toggle="tab" href="#<%=TabLastFiles.ClientID %>">انتخاب از فایلهای قبلی  
                                                         <i class="fa fa-bars"></i>
                                </a>
                            </li>--%>
                            <li class="active">
                                <a data-toggle="tab" href="#<%=TabNewFile.ClientID %>">افزودن فایل جدید  
                                                         <i class="fa fa-bars"></i>
                                </a>
                            </li>

                        </ul>
                    </header>
                    <div class="panel-body">
                        <div class="tab-content">
                            <div id="TabNewFile" class="tab-pane active" runat="server">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel11">
                                    <ContentTemplate>
                                        <asp:HiddenField runat="server" ID="hfControlIDAttachSite" />
                                        <asp:HiddenField runat="server" ID="hfFilelName" />
                                        <asp:HiddenField runat="server" ID="hfIDRet" />
                                        <asp:HiddenField runat="server" ID="hfPartName" />
                                        <asp:HiddenField runat="server" ID="hfIDUser" />
                                        <asp:HiddenField runat="server" ID="hfModeAttachSite" />
                                        <asp:HiddenField runat="server" ID="hfIDAttachCrmInterface" />
                                        <asp:HiddenField ID="hfSituation" runat="server" />
                                        <asp:HiddenField ID="hfNewIDAttachSite" runat="server" />

                                        <div class="btn-group-justified" dir="rtl">
                                            <asp:Button ID="btnNewAttachSite" runat="server" CssClass="btn btn-New " Style="float: right; width: 120px" Text="جدید" OnClick="btnNewAttachSite_Click" />
                                            <asp:Button ID="btnAddAttachSite" runat="server" CssClass="btn btn-Add " Style="float: right; width: 120px" Text="ثبت" OnClick="btnAddAttachSite_Click" ValidationGroup="attachSite" />
                                        </div>
                                        <div class="col-md-6 form-group">
                                            <label for="Title">نام فایل :</label><br />
                                            <asp:TextBox ID="txtTitle" runat="server" class="form-control" Style="width: 100%"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6 form-group">
                                            <label for="Title">دسته فایل :</label><br />
                                            <asp:DropDownList runat="server" ID="drpAttachInterfaceCategory" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-2 form-group">
                                            <label>ترتیب نمایش :</label><br />
                                            <asp:TextBox ID="txtOrder" runat="server" class="form-control" Style="width: 100%" TextMode="Number"></asp:TextBox>
                                        </div>
                                        <div class="col-md-12 form-group">
                                            <label for="Name">توضیحات :</label>
                                            <asp:TextBox ID="txtDescription" runat="server" class="form-control" Style="width: 100%" placeholder="توضیحات" MaxLength="500" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:Panel class="col-md-6 form-group" runat="server" ID="PanelUploadAttachSite">
                                    <label>آپلود فایل :</label>

                                    <asp:FileUpload runat="server" ID="fileUploadAttachSite" />
                                    <asp:Image runat="server" ID="imgUploadLoader" ImageUrl="~/Admin/images/loader.gif" Style="height: 8px; display: none" />
                                    <asp:Label runat="server" ID="lblFileUploadAttachSiteValidate" ForeColor="Red"></asp:Label>
                                </asp:Panel>
                                <div class="clearfix"></div>
                                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                    <ContentTemplate>
                                        <div id="AttachGrid">
                                            <header class="panel-heading">
                                                <span style="font-size: 20px;">لیست فایل های ضمیمه شده برای داده مورد نظر
                                                </span>
                                            </header>
                                            <asp:GridView ID="grdAttachCrmInterface" runat="server" CssClass="table table-bordered table-striped table-condensed" AutoGenerateColumns="false"
                                                DataKeyNames="IDAttachCrmInterface" PageSize="5" AllowPaging="true" OnRowCommand="grdAttachCrmInterface_RowCommand" OnRowEditing="grdAttachCrmInterface_RowEditing"
                                                OnRowDeleting="grdAttachCrmInterface_RowDeleting" OnPageIndexChanging="grdAttachCrmInterface_PageIndexChanging" OnRowDataBound="grdAttachCrmInterface_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="عملیات" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnDelete" CssClass="btn btn-Delete tool" runat="server" CommandName="Delete" CommandArgument='<%#Eval("IDAttachCrmInterface")%>' UseSubmitBehavior="false" data-placement="right" title="حذف" Visible='<%#ShowVisible(Eval("IDUser").ToString()) %>' />
                                                            <asp:Button ID="btnEdit" CssClass="btn btn-edit tool" runat="server" CommandName="Edit" CommandArgument='<%#Eval("IDAttachCrmInterface")%>' data-placement="left" title="ویرایش" Visible='<%#ShowVisible(Eval("IDUser").ToString()) %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ثبت کننده" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="FullName" runat="server" Text=' <%#Eval("FullName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="عنوان سیستمی فایل" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Title" runat="server" Text=' <%#Eval("Title") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="نام دسته" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="categoryName" runat="server" Text='<%#Eval("CategoryName_En") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="نوع فایل" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <div style="direction: ltr">
                                                                <asp:Label ID="FileType" runat="server" Text=' <%#Eval("FileType") %>'></asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ترتیب نمایش" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Order" runat="server" Text=' <%#Eval("order") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="تاریخ ثبت" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CreateDate" runat="server" Text='<%#DateTime.Parse(Eval("CreateDate").ToString()).ToShamsiDateYMD() + " - " + DateTime.Parse(Eval("CreateDate").ToString()).ToString("HH:mm")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="دانلود" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="Url" runat="server" NavigateUrl='<%#"/"+Eval("Url") %>' class="fa fa-paperclip" Style="font-size: x-large" Target="_blank">

                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="توضیحات" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <i data-original-title="شرح" data-content=' <%#Eval("Description") %>' dir="rtl" style="font-size: 30px"
                                                                data-placement="bottom" data-trigger="hover" class="fa fa-pencil-square-o popovers pop"></i>
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
                                            <asp:Button ID="btnJustCallForEvent" runat="server" Style="display: none" OnClick="btnJustCallForEvent_Click" />
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                            <div id="TabLastFiles" class="tab-pane" runat="server" visible="false">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel12">
                                    <ContentTemplate>
                                        <div class="btn-group-justified" dir="rtl">
                                            <asp:Button ID="btnAddAttachSiteFromLastFiles" runat="server" CssClass="btn btn-Add " Style="float: right; width: 120px" Text="ثبت" OnClick="btnAddAttachSiteFromLastFiles_Click" />
                                        </div>
                                        <div class="clearfix"></div>
                                        <br />
                                        <div class="col-md-12 form-group">

                                            <div style="max-height: 500px; overflow-y: scroll">
                                                <asp:GridView ID="grdAttachSiteFiles" runat="server" CssClass="table table-bordered table-striped table-condensed"
                                                    AutoGenerateColumns="false" DataKeyNames="IDAttachSite">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="انتخاب" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                                <asp:HiddenField ID="hfIDAttachSite" runat="server" Value='<%#Eval("IDAttachSite") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ثبت کننده" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="FullName" runat="server" Text=' <%#Eval("FullName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="عنوان سیستمی فایل" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Title" runat="server" Text='<%#Eval("Title") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="نام دسته" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="categoryName" runat="server" Text='<%#Eval("categoryName_En") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="نوع فایل" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="FileType" runat="server" Text='<%#Eval("FileType") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="دانلود" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="Url" runat="server" NavigateUrl='<%#"/"+Eval("Url") %>' ImageHeight="40" Font-Size="X-Large" Target="_blank"><i class="fa fa-paperclip"></i></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="توضیحات" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <i data-original-title="شرح" data-content=' <%#Eval("Description") %>' dir="rtl" style="font-size: 30px"
                                                                    data-placement="bottom" data-trigger="hover" class="fa fa-pencil-square-o popovers pop"></i>
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
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div id="TabAttachInterfaceCategory" class="tab-pane" runat="server">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                    <ContentTemplate>
                                        <asp:HiddenField runat="server" ID="hfAttachInterfaceCategoryMode" />
                                        <asp:HiddenField runat="server" ID="hfIDAttachInterfaceCategory" />
                                        <div class="col-md-12 form-group">
                                            <div class="btn-group-justified" dir="rtl">
                                                <asp:Button ID="btnNewAttachInterfaceCategory" runat="server" CssClass="btn btn-New " Style="float: right; width: 120px" Text="جدید" OnClick="btnNewAttachInterfaceCategory_Click" />
                                                <asp:Button ID="btnAddAttachInterfaceCategory" runat="server" CssClass="btn btn-Add " Style="float: right; width: 120px" Text="ثبت" OnClick="btnAddAttachInterfaceCategory_Click" UseSubmitBehavior="false" />
                                            </div>
                                            <div class="col-md-6 form-group">
                                                <label for="AttachInterfaceCategoryNam_En">نام دسته انگلیسی :</label><br />
                                                <asp:TextBox ID="txtAttachInterfaceCategoryNam_En" runat="server" class="form-control" Style="width: 100%"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="نام دسته انگلیسی را وارد کنید" Font-Size="Small" ForeColor="red" ControlToValidate="txtAttachInterfaceCategoryNam_En" ValidationGroup="AttachInterfaceCategory" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-6 form-group">
                                                <label for="AttachInterfaceCategoryNam_Fa">نام دسته فارسی :</label><br />
                                                <asp:TextBox ID="txtAttachInterfaceCategoryNam_Fa" runat="server" class="form-control" Style="width: 100%"></asp:TextBox>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div style="max-height: 500px; overflow-y: scroll">
                                                <asp:GridView ID="grdAttachInterfaceCategory" runat="server" CssClass="table table-bordered table-striped table-condensed" OnRowDeleting="grdAttachInterfaceCategory_RowDeleting" OnPageIndexChanging="grdAttachInterfaceCategory_PageIndexChanging"
                                                    AutoGenerateColumns="false" DataKeyNames="IDAttachInterfaceCategory" OnRowCommand="grdAttachInterfaceCategory_RowCommand" OnRowEditing="grdAttachInterfaceCategory_RowEditing">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="انتخاب" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" TargetControlID="btnDelete" ConfirmText="آیا مطمئن هستید؟" runat="server" />
                                                                <asp:Button ID="btnDelete" CssClass="btn btn-Delete tool" runat="server" CommandName="Delete" CommandArgument='<%#Eval("IDAttachInterfaceCategory")%>' UseSubmitBehavior="false" data-placement="right" title="حذف" />
                                                                <asp:Button ID="btnEdit" CssClass="btn btn-edit tool" runat="server" CommandName="Edit" CommandArgument='<%#Eval("IDAttachInterfaceCategory")%>' data-placement="left" title="ویرایش" />
                                                                <asp:HiddenField ID="hfIDAttachInterfaceCategory" runat="server" Value='<%#Eval("IDAttachInterfaceCategory") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="عنوان انگلیسی" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Name_En" runat="server" Text=' <%#Eval("Name_En") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="عنوان فارسی" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Name_Fa" runat="server" Text=' <%#Eval("Name_Fa") %>'></asp:Label>
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
                                        </div>
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
<!-- Modal attach site -->


<!-- ModalShowSelectedNodInProductCategory -->
<div class="modal fade" id="ModalDeleteWays" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="z-index: 1502" runat="server">
    <div class=" modal-dialog" style="width: 24%">
        <div class="modal-content" dir="rtl">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title" style="font-size: 16px">روش حذف را انتخاب کنید :  
                </h4>
            </div>
            <div class="modal-body" dir="rtl">
                <section class="panel">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <div class="col-md-6">
                                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" TargetControlID="btnDeleteAll" ConfirmText="آیا مطمئن هستید؟" runat="server" />
                                <asp:Button ID="btnDeleteAll" class="btn btn-New " runat="server" Style="float: right" UseSubmitBehavior="false" Width="120px" OnClick="btnDeleteAll_Click" Text="حذف از همه جا" />
                            </div>
                            <div class="col-md-6">
                                <asp:Button ID="btnDeleteJustThis" class="btn btn-Grid " runat="server" Style="float: right" UseSubmitBehavior="false" Width="130px" OnClick="btnDeleteJustThis_Click" Text="حذف فقط از همین جا " />
                                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" TargetControlID="btnDeleteJustThis" ConfirmText="آیا مطمئن هستید؟" runat="server" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </section>
            </div>
        </div>
    </div>
</div>
<!-- ShowProductcategoryInTree -->

<script>
    // in this method fill hfFilelName with name of fileUploadAttachSite
    function <%=ControlIDAttachSite%>fileUploadAttachSiteOnChange() {
        $("#<%=hfFilelName.ClientID%>").val($('#<%=fileUploadAttachSite.ClientID%>').val().split('\\').pop());
        <%=ControlIDAttachSite%>ValidateFile();
    }

    // in this method get the file content 
    function <%=ControlIDAttachSite%>UploadFiles(FileUploadName, FilePath, IDUser) {

        var Title = $("#<%=txtTitle.ClientID%>").val();
        var Description = $("#<%=txtDescription.ClientID%>").val();
        var Order = $("#<%=txtOrder.ClientID%>").val();
        var IDRet = $("#<%=hfIDRet.ClientID%>").val();
        var UploadFileName = $("#<%=hfFilelName.ClientID%>").val();
        var IDAttachSite = $("#<%=hfNewIDAttachSite.ClientID%>").val();

        var IDAttachInterfaceCategory = jQuery("#<%=drpAttachInterfaceCategory.ClientID%> option:selected").val();
        var fileUpload = $("#" + FileUploadName).get(0);
        var files = fileUpload.files;
        var data = new FormData();
        for (var i = 0; i < files.length; i++) {
            data.append(files[i].name, files[i]);
        }
        
        $('#<%=imgUploadLoader.ClientID%>').show()
        $.ajax({
            url: 'Handler/HandlerAttachSite.ashx?FilePath=' + FilePath + '&IDAttachSite=' + IDAttachSite + '&Title=' + Title +
                '&Description=' + Description + '&IDRet=' + IDRet + '&Order=' + Order + '&IdUser=' + IDUser + '&UploadFileName=' +
                UploadFileName + '&IDAttachInterfaceCategory=' + IDAttachInterfaceCategory,
            type: "POST",
            data: data,
            contentType: false,
            processData: false,
            success: function (result) {
                // fileUploadAttachSite FileUpload doesn't empty becuase it isn't in UpdatePanel, so using this command to remove content of it.
                $('#' + FileUploadName).val('');
                $("#<%=btnJustCallForEvent.ClientID %>").click();
                alert(result);
                $('#<%=imgUploadLoader.ClientID%>').hide()
            },
            error: function (err) {
                $('#' + FileUploadName).val('');
                $("#<%=hfFilelName.ClientID%>").val('')
                alert(err);
            }
        });
    }



    function <%=ControlIDAttachSite%>ValidateFile() {

        var validFilesTypes = ["doc", "docx", "txt", "pdf", "rar", "zip", "jpg", "png", "gif", "mp4", "vss"];
        var fileInput = document.getElementById("<%=fileUploadAttachSite.ClientID%>");
        var label = document.getElementById("<%=lblFileUploadAttachSiteValidate.ClientID%>");
        var path = fileInput.value;
        if (typeof (fileInput.files) != "undefined") {
            //kb 
            var size = parseFloat(fileInput.files[0].size).toFixed(2);
            if (size >= 1000 * 1024 * 1024) { //yani 500 mg
                label.innerHTML = "سایز فایل باید کم تر از 500 مگابایت باشد";
                $("#<%=hfFilelName.ClientID%>").val('');
                return;
            }
        }
        var ext = path.substring(path.lastIndexOf(".") + 1, path.length).toLowerCase();
        var isValidFile = false;
        for (var i = 0; i < validFilesTypes.length; i++) {
            if (ext == validFilesTypes[i]) {
                isValidFile = true;
                break;
            }
        }
        if (!isValidFile) {
            label.innerHTML = "فرمت فایل انتخابی مناسب نیست -" +
             " فرمت های صحیح:\n\n" + validFilesTypes.join(", ");
            $("#<%=hfFilelName.ClientID%>").val('');
        }
        else {
            label.innerHTML = "";
        }
        return isValidFile;
    }

    function <%=ControlIDAttachSite%>SetEnableFalse() {
        $("#<%=fileUploadAttachSite.ClientID %>").prop('disabled', true);
    }
    function <%=ControlIDAttachSite%>SetEnableTrue() {
        $("#<%=fileUploadAttachSite.ClientID %>").prop('disabled', false);
    }


</script>
