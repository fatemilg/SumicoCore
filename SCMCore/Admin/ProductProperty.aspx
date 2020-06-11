<%@ Page Title=" ویژگی ظاهری محصولات" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ProductProperty.aspx.cs" Inherits="SCMCore.Admin.ProductProperty" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Admin/UserControl/FileUpload.ascx" TagPrefix="uc1" TagName="FileUpload" %>

<%@ Import Namespace="SCMCore.ExtensionMethod" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfIdProperty" runat="server" />
            <asp:HiddenField ID="hfIdParentProperty" runat="server" />
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" dir="rtl">
                <section class="panel" dir="rtl">
                    <header class="panel-heading">
                        <span style="font-size: 20px;">لیست ویژگی های ظاهری محصولات
                        </span>
                    </header>

                    <div class="panel-body">
                        <section id="unseen">
                            <asp:TreeView ID="tvProperty" runat="server" ImageSet="BulletedList" ExpandDepth="0" NodeIndent="15"
                                OnSelectedNodeChanged="tvProperty_SelectedNodeChanged" SelectAction="None">
                                <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                <NodeStyle Font-Size="10pt" ForeColor="Black" HorizontalPadding="2px"
                                    NodeSpacing="0px" VerticalPadding="2px"></NodeStyle>
                                <ParentNodeStyle Font-Bold="False" ForeColor="red" />
                                <SelectedNodeStyle HorizontalPadding="0px" VerticalPadding="0px" BackColor="#B5B5B5" Font-Underline="False"></SelectedNodeStyle>
                                <SelectedNodeStyle />
                            </asp:TreeView>
                        </section>
                    </div>
                </section>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="modal fade" id="ModalPropertyEvents" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 50%">
            <div class="modal-content" dir="rtl">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">تغییرات برای 
                            <asp:Label ID="lblModalPropertyName" runat="server" ForeColor="Red"></asp:Label></h4>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="modal-body row" dir="rtl">
                    <section class="panel">
                        <header class="panel-heading tab-bg-dark-navy-blue tab-right ">
                            <ul class="nav nav-tabs pull-right" style="margin: 0">
                                <li class="">
                                    <a data-toggle="tab" href="#EditProperty" dir="ltr">ویرایش ویژگی
                                                <i class="fa fa-bars"></i>
                                    </a>
                                </li>
                                <li class="active">
                                    <a data-toggle="tab" href="#SubProperty" dir="ltr">ایجاد زیر دسته 
                                                 <i class="fa fa-bars"></i>
                                    </a>
                                </li>
                            </ul>
                        </header>
                        <div class="panel-body">
                            <div class="tab-content">
                                <div id="SubProperty" class="tab-pane active">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="form-group col-lg-12 col-md-12 col-sm-12 col-xs-12" dir="rtl">
                                                <asp:Button ID="btnAddSubProperty" CssClass="btn btn-Add" ValidationGroup="submitSubProperty" Style="float: right" runat="server" Text="ثبت" OnClick="btnAddSubProperty_Click" Width="80px" />
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-sm-12 col-xs-12" dir="rtl">
                                                <label for="txtPropertyTitle">عنوان(Fa) :</label>
                                                <asp:TextBox ID="txtPropertyTitle_Fa" CssClass="form-control" runat="server" MaxLength="50" Width="100%"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-sm-12 col-xs-12" dir="rtl">
                                                <label for="txtPropertyTitle">عنوان(En) :</label>
                                                <asp:TextBox ID="txtPropertyTitle_En" CssClass="form-control" runat="server" MaxLength="50" Width="100%"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-12 col-md-12 col-sm-12 col-xs-12" dir="rtl">
                                                <label>توضیحات(En) :</label>
                                                <CKEditor:CKEditorControl ID="txtPropertyDescription_En" runat="server"></CKEditor:CKEditorControl>

                                            </div>
                                            <div class="form-group col-lg-12 col-md-12 col-sm-12 col-xs-12" id="divPropertySort" runat="server">
                                                <label>ترتیب نمایش :</label>
                                                <asp:TextBox ID="txtPropertySort" CssClass="form-control" runat="server" MaxLength="3" Width="12%"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="ترتیب نمایش را وارد کنید" Font-Size="Small" ForeColor="red" ControlToValidate="txtPropertySort" ValidationGroup="submitSubProperty" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="ftbe2" runat="server" TargetControlID="txtPropertySort" FilterType="Numbers" />

                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="form-group col-lg-12 col-md-12 col-sm-12 col-xs-12" dir="rtl">
                                        <label>تصویر :</label>
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="delPicProperty" runat="server" CssClass="btn btn-Delete" ToolTip="حذف عکس" OnClick="delPicProperty_Click" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="form-group last">
                                            <div class="col-md-11">
                                                <div class="fileupload fileupload-new" data-provides="fileupload">

                                                    <div class="fileupload-preview fileupload-exists thumbnail AddPro" title="عکس جدید" style="max-width: 200px; max-height: 150px; line-height: 20px;">
                                                    </div>
                                                    <span>
                                                        <asp:FileUpload ID="fulProperty" runat="server" onchange="fulPropertyChange()" />
                                                        <asp:Label ID="lblfulPropertyValidate" runat="server" ForeColor="Red"></asp:Label>
                                                        <asp:HiddenField ID="hfFilelName" runat="server" />
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="EditProperty" class="tab-pane">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <div class="form-group col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <asp:Button ID="btnEditProperty" CssClass="btn btn-Add" ValidationGroup="submitEditProperty" Style="float: right" runat="server" Text="ثبت" OnClick="btnEditProperty_Click" Width="80px" />
                                                <ajaxToolkit:ConfirmButtonExtender ID="cbeAsk" TargetControlID="btnDeleteProperty" ConfirmText="آیا مطمئن هستید؟" runat="server" />
                                                <asp:Button ID="btnDeleteProperty" CssClass="btn btn-danger" Style="float: right" runat="server" Text="حذف" OnClick="btnDeleteProperty_Click" Width="80px" />
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                <label>عنوان(Fa) :</label>
                                                <asp:TextBox ID="txtPropertyEditTitle_Fa" CssClass="form-control" runat="server" MaxLength="50" Width="100%"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                <label>عنوان(En) :</label>
                                                <asp:TextBox ID="txtPropertyEditTitle_En" CssClass="form-control" runat="server" MaxLength="50" Width="100%"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-12 col-md-12 col-sm-12 col-xs-12" dir="rtl">
                                                <label>توضیحات(En) :</label>
                                                <CKEditor:CKEditorControl ID="txtPropertyEditDescription_En" runat="server"></CKEditor:CKEditorControl>

                                            </div>
                                            <div class="form-group col-lg-12 col-md-12 col-sm-12 col-xs-12" id="divPropertyEditSort" runat="server" >
                                                <label>ترتیب نمایش :</label>
                                                <asp:TextBox ID="txtPropertyEditSort" CssClass="form-control" runat="server" MaxLength="3" Width="12%"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="ترتیب نمایش را وارد کنید" Font-Size="Small" ForeColor="red" ControlToValidate="txtPropertyEditSort" ValidationGroup="submitEditProperty" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPropertyEditSort" FilterType="Numbers" />

                                            </div>

                                            <div class="form-group col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <label for="tvEditProperty">ویژگی بالادست: </label>
                                                <asp:TreeView ID="tvEditProperty" runat="server" ImageSet="BulletedList" ExpandDepth="0" NodeIndent="15"
                                                    SelectAction="None">
                                                    <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                                    <NodeStyle Font-Size="10pt" ForeColor="Black" HorizontalPadding="2px"
                                                        NodeSpacing="0px" VerticalPadding="2px"></NodeStyle>
                                                    <ParentNodeStyle Font-Bold="False" />
                                                    <SelectedNodeStyle HorizontalPadding="0px" VerticalPadding="0px" BackColor="#B5B5B5" Font-Underline="False"></SelectedNodeStyle>
                                                    <SelectedNodeStyle />
                                                </asp:TreeView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <br />
                                    <div class="form-group col-lg-12 col-md-12 col-sm-12 col-xs-12" dir="rtl">
                                        <label>تصویر :</label>
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="delPicPropertyEdit" runat="server" CssClass="btn btn-Delete" ToolTip="حذف عکس" OnClick="delPicPropertyEdit_Click" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="form-group last">
                                            <div class="col-md-11">
                                                <div class="fileupload fileupload-new" data-provides="fileupload">
                                                    <div class="fileupload-preview fileupload-exists thumbnail EditPro" title="عکس جدید" style="max-width: 200px; max-height: 150px; line-height: 20px;"></div>
                                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Image ID="imagePropertyEdit" ToolTip="عکس ثبت شده" Style="max-width: 200px; max-height: 150px; line-height: 20px;" runat="server" Visible="false" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <span>
                                                        <asp:FileUpload ID="FulPropertyEdit" runat="server" onchange="fulPropertyEditChange()" />
                                                        <asp:Label ID="lblfulPropertyEditValidate" runat="server" ForeColor="Red"></asp:Label>
                                                        <asp:HiddenField ID="hfFilelNameEdit" runat="server" />
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

            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script type="text/javascript" src="js/bootstrap-fileupload/bootstrap-fileupload.js"></script>
    <script>
        // in this method fill hfFilelName with name of fulProperty
        function fulPropertyChange() {
            $('.AddPro').css("display", "block");
            $("#<%=hfFilelName.ClientID%>").val($('#<%=fulProperty.ClientID%>').val().split('\\').pop());
            ValidateFileNew();
        }
        function fulPropertyEditChange() {
            $('.EditPro').css("display", "block");
            $("#<%=hfFilelNameEdit.ClientID%>").val($('#<%=FulPropertyEdit.ClientID%>').val().split('\\').pop());
                ValidateFileEdit();

            }
            function clearFulPropertyEdit() {
                $('input[type=file]').each(function () {
                    $(this).after($(this).clone(true)).remove();
                });
                $("#<%=hfFilelNameEdit.ClientID%>").val("");
            $('.EditPro').css("display", "none");

        }
        function clearFulProperty() {
            $('input[type=file]').each(function () {
                $(this).after($(this).clone(true)).remove();
            });
            $("#<%=hfFilelName.ClientID%>").val("");
            $('.AddPro').css("display", "none");
        }

        // in this method get the file content 

        function UploadFiles(FileUploadName, FilePath, FileName) {
            var fileUpload = $("#" + FileUploadName).get(0);
            var files = fileUpload.files;
            var data = new FormData();
            for (var i = 0; i < files.length; i++) {
                data.append(files[i].name, files[i]);
            }
            $.ajax({
                url: 'Handler/HandlerForPropertPic.ashx?FilePath=' + FilePath + '&FileName=' + FileName,
                type: "POST",
                data: data,
                contentType: false,
                processData: false,
                success: function (result) {
                    // fulProperty FileUpload doesn't empty becuase it isn't in UpdatePanel, so using this command to remove content of it.
                    $('#' + FileUploadName).val('');
                    $("#<%=hfFilelName.ClientID%>").val("");
                    $("<%=hfFilelNameEdit.ClientID%>").val("");
                },
                error: function (err) {
                    alert(err.statusText)
                }
            });
            //evt.preventDefault();
        }


        var validFilesTypes = ["png", "jpeg", "jpg"];
        function ValidateFileNew() {
            var file = document.getElementById("<%=fulProperty.ClientID%>");
            var label = document.getElementById("<%=lblfulPropertyValidate.ClientID%>");
            var path = file.value;
            var ext = path.substring(path.lastIndexOf(".") + 1, path.length).toLowerCase();
            var isValidFile = false;
            for (var i = 0; i < validFilesTypes.length; i++) {
                if (ext == validFilesTypes[i]) {
                    isValidFile = true;
                    break;
                }
            }

            var files = document.getElementById("<%=fulProperty.ClientID%>").files;
            var data = new FormData();
            var size = files[0].size;
            if (size > 3000000) {
                alert("فایل باید کوچک تر از 3 مگابایت باشد");
                clearFulProperty();
                return;
            }

            if (!isValidFile) {
                label.innerHTML = "فرمت فایل انتخابی مناسب نیست -" +
                 " فرمت های صحیح:\n\n" + validFilesTypes.join(", ");
            }
            else {
                label.innerHTML = "";
            }
            return isValidFile;
        }
        function ValidateFileEdit() {
            var file = document.getElementById("<%=FulPropertyEdit.ClientID%>");
            var label = document.getElementById("<%=lblfulPropertyEditValidate.ClientID%>");
            var path = file.value;
            var ext = path.substring(path.lastIndexOf(".") + 1, path.length).toLowerCase();
            var isValidFile = false;
            for (var i = 0; i < validFilesTypes.length; i++) {
                if (ext == validFilesTypes[i]) {
                    isValidFile = true;
                    break;
                }
            }

            var files = document.getElementById("<%=fulProperty.ClientID%>").files;
            var data = new FormData();
            var size = files[0].size;
            if (size > 3000000) {
                alert("فایل باید کوچک تر از 3 مگابایت باشد");
                clearFulPropertyEdit();
                return;
            }

            if (!isValidFile) {
                label.innerHTML = "فرمت فایل انتخابی مناسب نیست -" +
                 " فرمت های صحیح:\n\n" + validFilesTypes.join(", ");
            }
            else {
                label.innerHTML = "";
            }
            return isValidFile;
        }
    </script>
</asp:Content>
