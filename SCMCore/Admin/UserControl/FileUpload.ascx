<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileUpload.ascx.cs" Inherits="SCMCore.Admin.UserControl.FileUpload" %>
<%@ Import Namespace="SCMCore.ExtensionMethod" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:HiddenField runat="server" ID="hfFilelName" />
<asp:HiddenField runat="server" ID="hfControlID" />
<div class="last" style="margin-top: 10px;">
    <div class="fileupload fileupload-new" data-provides="fileupload">
        <asp:FileUpload runat="server" ID="CustomFileUpload" />
        <asp:Label runat="server" ID="lblFileUploadValidate" Style="color: red"></asp:Label>
        <progress style="display: none" runat="server" id="Uploadprogress"></progress>
        <a runat="server" id="clearFile" style="display: none; color: red; cursor: pointer">X</a>
        <asp:Label runat="server" ID="lblPercentLoaded" Style="color: green"></asp:Label>
        <div class="fileupload-preview fileupload-exists thumbnail" style="max-width: 200px; max-height: 200px;  border: solid 1px lightgray"></div>
    </div>
</div>

<asp:UpdatePanel runat="server" ID="up1">
    <ContentTemplate>
        <asp:Button runat="server" ID="btnUpload" OnClick="btnUpload_Click" Text="upload" Style="display: none" />
    </ContentTemplate>
</asp:UpdatePanel>
<script>
    function <%=ControlID%>CustomFileUploadOnChange() {
        $('#<%=clearFile.ClientID%>').hide();
        var validation = <%=ControlID%>ValidateFile('<%=CustomFileUpload.ClientID %>', $('#<%=CustomFileUpload.ClientID %>').val().split('\\').pop());
        if (validation) {
            document.getElementById("<%=btnUpload.ClientID %>").click();
        }
    }
    function <%=ControlID%>ClearFileUpload() {
        $('#<%=clearFile.ClientID%>').hide();
        $("#<%=hfFilelName.ClientID %>").val("");
        $("#<%=CustomFileUpload.ClientID %>").val("");
        $('#<%=Uploadprogress.ClientID%>').hide();
        var lblPercentLoaded = document.getElementById("<%=lblPercentLoaded.ClientID%>");
        lblPercentLoaded.innerHTML = '';
    }
    function <%=ControlID%>UploadFiles(FileUploadName, FileName) {
        var XHR = new window.XMLHttpRequest();
        var fileUpload = $('#' + FileUploadName).get(0);
        var files = fileUpload.files;
        var data = new FormData();
        data.append(files[0].name, files[0]);
        $('#<%=Uploadprogress.ClientID%>').show();
        $.ajax({
            url: 'Handler/FileUpload.ashx?FileName=' + FileName,
            type: "POST",
            data: data,
            xhr: function () {
                var XHR = $.ajaxSettings.xhr();
                if (XHR.upload) {
                    XHR.upload.addEventListener('progress', <%=ControlID%>progress, false);
                }
                return XHR;
            },
            cache: false,
            contentType: false,
            processData: false,
            success: function (result) {
                if ($('#<%=hfFilelName.ClientID%>').val() != '') {
                    $.ajax({
                        url: 'Handler/DeleteFile.ashx?FileName=' + $("#<%=hfFilelName.ClientID %>").val(),
                        type: "POST",
                        cache: false,
                        contentType: false,
                        processData: false,
                    });
                }
                var fileInput = document.getElementById(FileUploadName);
                var ext = fileInput.value.substring(fileInput.value.lastIndexOf(".") + 1, fileInput.value.length).toLowerCase();
                $('#<%=hfFilelName.ClientID%>').val(FileName + '.' + ext);
            },
            error: function (err) {
                $('#' + FileUploadName).val('');
                alert(err);
            }
        });
    }
    function <%=ControlID%>progress(e) {
        if (e.lengthComputable) {
            var Max = e.total;
            var current = e.loaded;
            var Percentage = (current * 100) / Max;
            $('#<%=Uploadprogress.ClientID%>').attr({ value: current, max: Max });
            var lblPercentLoaded = document.getElementById("<%=lblPercentLoaded.ClientID%>");
            lblPercentLoaded.innerHTML = Math.round(Percentage) + '%';
            if (Percentage == 100) {
                $('#<%=clearFile.ClientID%>').show();
                lblPercentLoaded.innerHTML = 'Upload Successful!'
            }
        }
    }

    function <%=ControlID%>ValidateFile(FileUpload, hfFileName) {
        var validFilesTypes = ["jpg", "png", "gif", "bmp","vss"];
        var fileInput = document.getElementById(FileUpload);
        var label = document.getElementById("<%=lblFileUploadValidate.ClientID%>");
        var path = fileInput.value;
        if (typeof (fileInput.files) != "undefined") {
            //kb 
            var size = parseFloat(fileInput.files[0].size / 1024).toFixed(2);
            if (size >= 3072) { //yani 3mb 
                label.innerHTML = "سایز فایل باید کم تر از 3 مگابایت باشد";
                $("#" + hfFileName).val('');
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
            $("#" + hfFileName).val('');
        }
        else {
            label.innerHTML = "";
        }
        return isValidFile;
    }
    function <%=ControlID%>ClearFile() {
        $.ajax({
            url: 'Handler/DeleteFile.ashx?FileName=' + $("#<%=hfFilelName.ClientID %>").val(),
            type: "POST",
            cache: false,
            contentType: false,
            processData: false,
        });
        $("#<%=hfFilelName.ClientID %>").val("");
        $("#<%=CustomFileUpload.ClientID %>").val("");
        $('#<%=Uploadprogress.ClientID%>').hide();
        var lblPercentLoaded = document.getElementById("<%=lblPercentLoaded.ClientID%>");
        lblPercentLoaded.innerHTML = '';
        $('#<%=clearFile.ClientID%>').hide();
        $('.fileupload-preview.fileupload-exists.thumbnail').find('img').remove();
    }
    function <%=ControlID%>ResetControl() {
        $("#<%=hfFilelName.ClientID %>").val("");
        $("#<%=CustomFileUpload.ClientID %>").val("");
        $('#<%=Uploadprogress.ClientID%>').hide();
        var lblPercentLoaded = document.getElementById("<%=lblPercentLoaded.ClientID%>");
        lblPercentLoaded.innerHTML = '';
        $('#<%=clearFile.ClientID%>').hide();
        $('.fileupload-preview.fileupload-exists.thumbnail').find('img').remove();
    }
</script>
