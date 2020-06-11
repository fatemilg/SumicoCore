<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LegalCustomerAutoComplete.ascx.cs" Inherits="SCMCore.Admin.UserControl.LegalCustomerAutoComplete" %>
<asp:UpdatePanel ID="UpdatePanel22" runat="server">
    <ContentTemplate>
        <asp:TextBox ID="txtLegalCustomer" Style="width: 90%; display: inline" CssClass="form-control" runat="server" placeholder="مشتری حقوقی" autocomplete="off" OnTextChanged="txtLegalCustomer_TextChanged" AutoPostBack="true"></asp:TextBox>
        <asp:HiddenField ID="hfIDLegalCustomer" runat="server"></asp:HiddenField>
        <input type="button" id="clearLegalCustomer" class="btn btn-Clean tool" data-placement="left" title="پاک کردن نام مشتری حقوقی" />
    </ContentTemplate>
</asp:UpdatePanel>
<script type="text/javascript">
    function AutoCompleteLegalCustomer() {

        $("#<%=txtLegalCustomer.ClientID %>").autocomplete({
            source: function (request, response) {

                $.ajax({
                    url: "../../WebService/AutoComplete.asmx/getLegalCustomer",
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
                $("#<%=hfIDLegalCustomer.ClientID %>").val(i.item.val);
                $("#<%=txtLegalCustomer.ClientID %>").prop('disabled', true);
                $("#<%=txtLegalCustomer.ClientID %>").change();

            },
            minLength: 1
        });
        $("#clearLegalCustomer").click(function () {
            $("#<%=txtLegalCustomer.ClientID %>").val("");
                $("#<%=hfIDLegalCustomer.ClientID %>").val("");
                $("#<%=txtLegalCustomer.ClientID %>").prop('disabled', false);
            });
        }
</script>

