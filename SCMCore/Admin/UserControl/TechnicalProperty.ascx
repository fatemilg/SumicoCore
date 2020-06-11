<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TechnicalProperty.ascx.cs" Inherits="SCMCore.Admin.UserControl.TechnicalProperty" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!-- ModalTechnicalProperty -->
<div class="modal fade" id="ModalTechnicalProperty" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="z-index: 1000000">
    <div class=" modal-dialog" style="width: 75%">
        <div class="modal-content" dir="rtl">
            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                <ContentTemplate>
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" style="font-size: 16px">مقداردهی ویژگی فنی برای   
                            <asp:Label ID="lblHeaderOfModal" runat="server" ForeColor="Red" Style="font-size: 16px"></asp:Label></h4>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="modal-body" dir="rtl">
                <section class="panel">
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hfIDDetailTechnicalProperty" runat="server" />
                            <asp:HiddenField ID="hfModeDetailTechnicalProperty" runat="server" />
                            <asp:HiddenField ID="hfTechnicalPropertyAutoComplete" runat="server" />
                            <asp:HiddenField runat="server" ID="hfIDRet" />
                            <div class="btn-group-justified" dir="rtl">
                                <asp:Button ID="btnNewDetailTechnicalProperty" class="btn btn-New" runat="server" Text="جدید" Style="float: right" UseSubmitBehavior="false" Width="80px" OnClick="btnNewDetailTechnicalProperty_Click" />
                                <asp:Button ID="btnAddDetailTechnicalProperty" class="btn btn-Add" ValidationGroup="submitDetailTechnicalProperty" Style="float: right" runat="server" Text="ثبت" Width="80px" OnClick="btnAddDetailTechnicalProperty_Click" />
                            </div>

                            <div class="panel-body">
                                <div class="col-lg-6 ">
                                    <label>ترتیب برای نمایش :</label>
                                    <asp:TextBox ID="txtOrderTechnicalProperty" class=" form-control" runat="server" TextMode="Number"></asp:TextBox>

                                </div>
                                <div class="col-lg-6">
                                    <label>انتخاب ویژگی :</label>
                                    <br />
                                    <asp:TextBox ID="txtTechnicalProperty" class=" form-control" runat="server" MaxLength="200" autocomplete="off" Style="display: inline"></asp:TextBox>
                                    <input type="button" id="clearSelectTechnicalProperty" class="btn btn-Clean tool clearSelectTechnicalProperty" data-placement="left" title="پاک کردن ویژگی فنی" />

                                </div>
                                <div class="clearfix"></div>
                                <div class="col-lg-12" runat="server" visible="false">
                                    <label>مقدار ویژگی(Fa) :</label>
                                    <CKEditor:CKEditorControl ID="txtTechnicalValue_Fa" CssClass=" form-control" runat="server"></CKEditor:CKEditorControl>

                                </div>
                                <div class="col-lg-12 " runat="server">
                                    <label>مقدار ویژگی(En)  :</label>
                                    <CKEditor:CKEditorControl ID="txtTechnicalValue_En" CssClass=" form-control" runat="server"></CKEditor:CKEditorControl>

                                </div>

                                <div class="clearfix"></div>
                                <br />
                                <div class="col-lg-12 " dir="rtl">
                                    <asp:GridView ID="grdDetailTechnicalProperty" runat="server" CssClass="table table-bordered table-striped table-condensed" AutoGenerateColumns="false" DataKeyNames="IDDetailTechnicalProperty" PageSize="5"
                                        AllowPaging="true" OnRowCommand="grdDetailTechnicalProperty_RowCommand" OnRowEditing="grdDetailTechnicalProperty_RowEditing" OnRowDeleting="grdDetailTechnicalProperty_RowDeleting" OnPageIndexChanging="grdDetailTechnicalProperty_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="عملیات" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnDel" CssClass="btn btn-Delete" ToolTip="حذف" runat="server" CommandName="Delete" OnClientClick="return confirm(' آیا مطمئن هستید ؟ ')" CommandArgument='<%#Eval("IDDetailTechnicalProperty")%>' />
                                                    <asp:Button ID="btnEdit" CssClass="btn btn-edit" ToolTip="ویرایش" runat="server" CommandName="Edit" CommandArgument='<%#Eval("IDDetailTechnicalProperty")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="کد" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="Code" runat="server" Text=' <%#Eval("Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ترتیب نمایش " ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="Order" runat="server" Text=' <%#Eval("Order") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="عنوان ویژگی(Fa) " ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="TechnicalPropertyName_Fa" runat="server" Text=' <%#Eval("TechnicalPropertyName_Fa") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="عنوان ویژگی(En) " ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="TechnicalPropertyName_En" runat="server" Text=' <%#Eval("TechnicalPropertyName_En") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="مقدار(En)" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <i data-original-title="شرح" data-content=' <%#Eval("Value_En") %>' dir="rtl" style="font-size: 30px"
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
                </section>
            </div>

        </div>
    </div>
</div>
<!-- ModalTechnicalProperty -->

<script>
    function LoadJsTechnicalProperty() {
        $("#<%=txtTechnicalProperty.ClientID %>").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "../WebService/Auto.asmx/getTechnicalPropertyCodeAndName",
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
                $("#<%=hfTechnicalPropertyAutoComplete.ClientID %>").val(i.item.val);
                $("#<%=txtTechnicalProperty.ClientID %>").prop('disabled', true);
            },
            minLength: 1
        });
        $(".clearSelectTechnicalProperty").click(function () {
            $("#<%=txtTechnicalProperty.ClientID %>").val("");
            $("#<%=hfTechnicalPropertyAutoComplete.ClientID %>").val("");
            $("#<%=txtTechnicalProperty.ClientID %>").prop('disabled', false);
        });
    };
</script>
