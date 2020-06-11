<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RealCustomerAutoComplete.ascx.cs" Inherits="SCMCore.Admin.UserControl.RealCustomerAutoComplete" %>
<asp:UpdatePanel ID="UpdatePanel22" runat="server">
    <ContentTemplate>
        <asp:TextBox ID="txtRealCustomer" Style="width: 90%; display: inline" CssClass="form-control" runat="server" placeholder="مشتری حقیقی" autocomplete="off" OnTextChanged="txtRealCustomer_TextChanged" AutoPostBack="true"></asp:TextBox>
        <asp:HiddenField ID="hfIDRealCustomer" runat="server"></asp:HiddenField>
        <input type="button" id="clearRealCustomer" class="btn btn-Clean tool" data-placement="left" title="پاک کردن نام مشتری حقیقی" />
    </ContentTemplate>
</asp:UpdatePanel>
<script type="text/javascript">
    function AutoCompleteRealCustomer() {

        $("#<%=txtRealCustomer.ClientID %>").autocomplete({
            source: function (request, response) {

                $.ajax({
                    url: "../../WebService/AutoComplete.asmx/getRealCustomer",
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
                $("#<%=hfIDRealCustomer.ClientID %>").val(i.item.val);
                $("#<%=txtRealCustomer.ClientID %>").prop('disabled', true);
                $("#<%=txtRealCustomer.ClientID %>").change();

            },
            minLength: 1
        });
        $("#clearRealCustomer").click(function () {
            $("#<%=txtRealCustomer.ClientID %>").val("");
                $("#<%=hfIDRealCustomer.ClientID %>").val("");
                $("#<%=txtRealCustomer.ClientID %>").prop('disabled', false);
            });
        }
</script>
