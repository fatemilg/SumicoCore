<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="BaseSetting.aspx.cs" Inherits="SCMCore.Admin.BaseSetting" %>

<%@ Import Namespace="SCMCore.ExtensionMethod" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-lg-12">
        <section class="panel">
            <header class="panel-heading tab-bg-dark-navy-blue tab-right ">
                <ul class="nav nav-tabs pull-right" style="margin: 0">
                    <li class="active">
                        <a data-toggle="tab" href="#Currency" style="font-size: 18px">قیمت ارز 
                            <i class="fa fa-bars"></i>
                        </a>
                    </li>
                </ul>
            </header>
            <div class="panel-body">
                <div class="tab-content">
                    <div id="Currency" class="tab-pane active ">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                            <ContentTemplate>
                                <div class="panel-body">
                                    <div class="position-center">
                                        <div class="btn-group-justified" dir="rtl">
                                            <asp:Button ID="btnAddCurrency" class="btn btn-Add" Style="float: right" runat="server" Text="ثبت" Width="80px" ValidationGroup="AddCurrency" OnClick="btnAddCurrency_Click" />
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class="col-md-3 form-group ">
                                            <label for="Name">دلار امروز(ریال) :</label>
                                            <asp:TextBox ID="txtDolar" class="form-control" Width="100%" runat="server" Style="text-align: left"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="دلار امروز را وارد کنید" Font-Size="Small" ForeColor="red" ControlToValidate="txtDolar" ValidationGroup="AddCurrency" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <cc1:filteredtextboxextender id="FilteredTextBoxExtender1" runat="server" targetcontrolid="txtDolar" filtertype="Numbers" />
                                        </div>
                                        <div class="col-md-3 form-group ">
                                            <label for="Name">یورو امروز(ریال) :</label>
                                            <asp:TextBox ID="txtEuro" class="form-control" Width="100%" runat="server" Style="text-align: left"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="یورو امروز را وارد کنید" Font-Size="Small" ForeColor="red" ControlToValidate="txtEuro" ValidationGroup="AddCurrency" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <cc1:filteredtextboxextender id="FilteredTextBoxExtender2123" runat="server" targetcontrolid="txtEuro" filtertype="Numbers" />
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class="col-md-12">
                                            <asp:GridView ID="grdCurrency" runat="server" CssClass="table table-bordered table-striped table-condensed" AutoGenerateColumns="false"
                                                DataKeyNames="IDCurrency" PageSize="10" AllowPaging="true">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="دلار(بر حسب ریال)" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="DolarByRial" runat="server" Text='<%#string.Format("{0:n0}", Eval("DolarByRial").ToString().StringToInt())  %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="یورو(بر حسب ریال)" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="EuroByRial" runat="server" Text='<%#string.Format("{0:n0}", Eval("EuroByRial").ToString().StringToInt())  %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="تاریخ ثبت">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CreateDate" runat="server" Text='<%#DateTime.Parse(Eval("CreateDate").ToString()).ToShamsiDateString() +"-"+ DateTime.Parse(Eval("CreateDate").ToString()).ToString("HH:mm")%>'></asp:Label>
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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
</asp:Content>
