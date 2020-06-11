<%@ Page Title="تعریف سطح دسترسی" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AccessLevel.aspx.cs" Inherits="SCMCore.Admin.AccessLevel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

             <div class="col-lg-12" id="divInfo" runat="server">
                <section class="panel" dir="rtl">
                    <header class="panel-heading">
                        <span style="font-size: 20px;">سطح دسترسی کاربران
                        </span>
                    </header>

                    <div class="panel-body">
                        <div class="position-center">
                            <div class="col-md-12">
                                <label for="FName">نام کاربر :</label>
                                <asp:DropDownList ID="drpUser" CssClass=" form-control" runat="server" OnSelectedIndexChanged="drpUser_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>

                            </div>

                            <div class="clearfix"></div>
                            <br />
                            <section id="unseen">
                                <asp:TreeView ID="tvAccesslevel" runat="server" ImageSet="BulletedList" ExpandDepth="0" NodeIndent="15"
                                    OnSelectedNodeChanged="tvAccesslevel_SelectedNodeChanged" SelectAction="None">
                                    <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                    <NodeStyle Font-Size="10pt" ForeColor="Black" HorizontalPadding="2px"
                                        NodeSpacing="0px" VerticalPadding="2px"></NodeStyle>
                                    <ParentNodeStyle Font-Bold="true" ForeColor="red" />
                                    <SelectedNodeStyle HorizontalPadding="0px" VerticalPadding="0px" BackColor="#B5B5B5" Font-Underline="False"></SelectedNodeStyle>
                                    <SelectedNodeStyle />
                                </asp:TreeView>
                            </section>


                        </div>
                    </div>
                </section>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="modal fade" id="ModalAssignEvent" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 30%">
            <div class="modal-content" dir="rtl">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" style="direction: rtl; text-align: right; margin-left: 30px">تنظیمات برای منوی   
                            <asp:Label ID="lblMenuName" runat="server" ForeColor="Red"></asp:Label></h4>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="modal-body " dir="rtl" style="padding: 10px">
                    <section class="panel">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <div class="panel-body" style="padding: 0px">
                                    <asp:HiddenField ID="hfIDMenuForEvent" runat="server" />
                                    <div class="col-md-4">
                                        <asp:CheckBox ID="chkSelectMenu" runat="server" OnCheckedChanged="chkSelectMenu_CheckedChanged" Text="نمایش منو" AutoPostBack="true" />
                                    </div>

                                    <div class="col-md-12">
                                        <label>پرسنل انتخاب شده دسترسی به رویداد های زیر  در این منو را دارد :</label>
                                        <section id="unseen2">
                                            <asp:Panel ID="panelEvent" runat="server" Style="height:500px;overflow-y: scroll;" >
                                                <asp:TreeView ID="tvEvent" runat="server" ImageSet="BulletedList" ExpandDepth="0" NodeIndent="15"
                                                    OnSelectedNodeChanged="tvEvent_SelectedNodeChanged" SelectAction="None">
                                                    <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                                    <NodeStyle Font-Size="10pt" ForeColor="Black" HorizontalPadding="2px"
                                                        NodeSpacing="0px" VerticalPadding="2px"></NodeStyle>
                                                    <ParentNodeStyle Font-Bold="true" ForeColor="red" />
                                                </asp:TreeView>
                                            </asp:Panel>

                                        </section>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </section>
                </div>

            </div>
        </div>
    </div>
</asp:Content>

