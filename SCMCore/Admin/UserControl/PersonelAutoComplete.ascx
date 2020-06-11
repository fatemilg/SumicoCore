<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PersonelAutoComplete.ascx.cs" Inherits="SCMCore.Admin.UserControl.PersonelAutoComplete" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:TextBox ID="txtUserFullName" CssClass="form-control" runat="server" MaxLength="200" Width="94%" autocomplete="off"  Style="display: inline" ></asp:TextBox>
        <input type="button" id="clearPersonel" class="btn btn-Clean tool" data-placement="left" title="پاک کردن نام پرسنل" />
        <div class="clearfix"></div>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="نام را وارد کنید" Font-Size="Small" ForeColor="red" ControlToValidate="txtUserFullName" ValidationGroup="submitUserRole" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:HiddenField ID="hfIDPersonelAutoCompelete" runat="server" />

    </ContentTemplate>
</asp:UpdatePanel>
<script type="text/javascript">

    function AutoCompleteUserFullName() {
        $("#<%=txtUserFullName.ClientID %>").autocomplete({
            source: function (request, response) {

                $.ajax({
                    url: "../../WebService/AutoComplete.asmx/getPersonel",
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
                $("#<%=hfIDPersonelAutoCompelete.ClientID %>").val(i.item.val);
                $("#<%=txtUserFullName.ClientID %>").prop('disabled', true);

            },
            minLength: 1
        });
        $("#clearPersonel").click(function () {
            $("#<%=txtUserFullName.ClientID %>").val("");
            $("#<%=hfIDPersonelAutoCompelete.ClientID %>").val("");
            $("#<%=txtUserFullName.ClientID %>").prop('disabled', false);
        });
    }

</script>

