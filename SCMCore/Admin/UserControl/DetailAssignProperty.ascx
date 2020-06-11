<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailAssignProperty.ascx.cs" Inherits="SCMCore.Admin.UserControl.DetailAssignProperty" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Admin/UserControl/TreeDropDown.ascx" TagPrefix="uc1" TagName="TreeDropDown" %>

<%@ Import Namespace="SCMCore.ExtensionMethod" %>



<!-- Modal Property Assign -->
<div class="modal fade" id="ModalPropertyAssign" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="position-center modal-dialog">
        <div class="modal-content" dir="rtl">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">اختصاص ویژگی ظاهری به   
                            <asp:Label ID="lblHeaderOfModal" runat="server" ForeColor="Red" Style="font-size: 18px"></asp:Label></h4>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="modal-body " dir="rtl">
                <section class="panel">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hfIDRet" runat="server" />
                            <div class="panel-body">
                                <div class="col-md-4 form-group">
                                    <label>ویژگی را انتخاب کنید :</label>

                                    <uc1:TreeDropDown runat="server" ID="TreeDropDownProperties" OntvDropDown_SelectedNode="TreeDropDownProperties_tvDropDown_SelectedNode"/>

                                  <%--  <asp:DropDownList ID="drpPropertyInAssign" runat="server" CssClass="form-control" dir="rtl" OnSelectedIndexChanged="drpPropertyInAssign_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>--%>
                                </div>
                                <div class="col-md-8">
                                    <label>اطلاعات بر حسب اولویت به صورت صعودی لیست شده اند :</label>
                                    <asp:GridView ID="grdDetailAssignProperty" runat="server" CssClass="display table table-bordered table-striped dataTable" AutoGenerateColumns="false" OnRowDataBound="grdDetailAssignProperty_RowDataBound"
                                        DataKeyNames="IDDetailAssignProperty" OnPageIndexChanging="grdDetailAssignProperty_PageIndexChanging" OnRowCommand="grdDetailAssignProperty_RowCommand" OnRowEditing="grdDetailAssignProperty_RowEditing" OnRowDeleting="grdDetailAssignProperty_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="حذف " ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnDel" CssClass="btn btn-Delete tool" runat="server" CommandName="Delete" OnClientClick="return confirm(' آیا مطمئن هستید ؟ ')" CommandArgument='<%#Eval("IDDetailAssignProperty")%>' data-placement="right" title="حذف" />
                                                    <asp:HiddenField runat="server" ID="hfIDDetailAssignPropertyInGrid" Value='<%#Eval("IDDetailAssignProperty")%>' />
                                                    <asp:HiddenField runat="server" ID="hfIDProperty" Value='<%#Eval("IDProperty")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="عنوان ویژگی" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="PropertyName_Fa" runat="server" Text=' <%#Eval("PropertyName_En") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="اولویت" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSort" runat="server" Text=' <%#Eval("Sort") %>' Style="text-align: center" OnTextChanged="txtSort_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtSort" FilterType="Numbers" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="مرجع دسته بندی" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lbSetForShowInPorductCategory" CommandArgument='<%#Eval("IDDetailAssignProperty")%>' CommandName="SetForShowInPorductCategory"><%#Eval("SetForShowInPorductCategory").ToString().StringToBool()==true ? "<i class='fa fa-check-square-o' style='font-size:16px'></i>" : "<i class='fa fa-square-o' style='font-size:16px'></i>" %></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>

                                            <div>
                                                اطلاعاتی برای نمایش موجود نیست

                                            </div>

                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                                <div class="col-md-12" style="font-size: 14px">
                                    <i class="fa fa-info-circle" style="color: blue"></i>
                                    <b>راهنما: </b>
                                    <span style="color: red">در صورتی که برای  محصولات این دسته قوانینی ثبت شده باشد ، تا زمانی که آنها را پاک نکنید نمی توانید تغییری در این قسمت ایجاد کنید</span>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </section>
            </div>

        </div>
    </div>
</div>
<!-- Modal Property Assign -->

