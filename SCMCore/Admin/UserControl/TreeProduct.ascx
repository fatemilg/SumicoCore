﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TreeProduct.ascx.cs" Inherits="SCMCore.Admin.UserControl.TreeProduct" %>
<%@ Import Namespace="SCMCore.ExtensionMethod" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<div style="direction: ltr">
    <asp:UpdatePanel runat="server" ID="updatepanelPopover">
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="hfSelectedDefineDetail" />
            <asp:Repeater runat="server" ID="rptProductCategory" ClientIDMode="AutoID">
                <ItemTemplate>
                    <asp:HiddenField runat="server" ID="hfIDProductCategory" Value='<%#Eval("IDProductCategory") %>' />
                    <asp:HiddenField runat="server" ID="hfExpand" />
                    <asp:LinkButton runat="server" ID="lbExpan" OnClick="lbExpand_Click" Style="display: inline-block"><i class=" fa fa-plus" style="font-size:15px"></i></asp:LinkButton>

                    <span><%#Eval("Name_En") %></span>
                    <div class="clearfix"></div>

                    <asp:Repeater runat="server" ID="rptMasterProduct" ClientIDMode="AutoID">
                        <ItemTemplate>
                            <div style="margin-left: 15px">
                                <asp:HiddenField runat="server" ID="hfIDProduct" Value='<%#Eval("IDProduct") %>' />
                                <asp:HiddenField runat="server" ID="hfExpandMasterProduct" />
                                <asp:LinkButton runat="server" ID="lbExpanMasterProduct" OnClick="lbExpanMasterProduct_Click" Style="display: inline-block"><i class=" fa fa-plus" style="font-size:15px"></i></asp:LinkButton>
                                <span><%#Eval("Name_En") %></span>
                                <div class="clearfix"></div>
                                <div style="margin-left: 15px">
                                    <asp:Repeater runat="server" ID="rptDefineDetailProduct" ClientIDMode="AutoID">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="lbSelectDefineDetail" OnClick="lbSelectDefineDetail_Click"><%#CheckIDInSelectedList(Eval("IDDefineDetailProduct").ToString())==true ? "<i class='fa fa fa-check-square-o'></i>" : "<i class='fa fa fa-square-o'></i>" %></asp:LinkButton>
                                            <span><%#Eval("PartNumber") %></span>
                                            <asp:HiddenField runat="server" ID="hfIDDefineDetailProduct" Value='<%#Eval("IDDefineDetailProduct") %>' />
                                            <div class="clearfix"></div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Repeater runat="server" ID="rptProductCategory" ClientIDMode="AutoID">
                        <ItemTemplate>
                            <div style="margin-left: 15px">
                                <asp:HiddenField runat="server" ID="hfIDProductCategory" Value='<%#Eval("IDProductCategory") %>' />
                                <asp:HiddenField runat="server" ID="hfExpand" />
                                <asp:LinkButton runat="server" ID="lbExpand" OnClick="lbExpand_Click" Style="display: inline-block"><i class=" fa fa-plus" style="font-size:15px"></i></asp:LinkButton>
                                <span><%#Eval("Name_En") %></span>
                                <div class="clearfix"></div>
                                <asp:Repeater runat="server" ID="rptMasterProduct" ClientIDMode="AutoID">
                                    <ItemTemplate>
                                        <div style="margin-left: 15px">
                                            <asp:HiddenField runat="server" ID="hfIDProduct" Value='<%#Eval("IDProduct") %>' />
                                            <asp:HiddenField runat="server" ID="hfExpandMasterProduct" />
                                            <asp:LinkButton runat="server" ID="lbExpanMasterProduct" OnClick="lbExpanMasterProduct_Click" Style="display: inline-block"><i class=" fa fa-plus" style="font-size:15px"></i></asp:LinkButton>
                                            <span><%#Eval("Name_En") %></span>
                                            <div class="clearfix"></div>
                                            <div style="margin-left: 15px">
                                                <asp:Repeater runat="server" ID="rptDefineDetailProduct" ClientIDMode="AutoID">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lbSelectDefineDetail" OnClick="lbSelectDefineDetail_Click"><%#CheckIDInSelectedList(Eval("IDDefineDetailProduct").ToString())==true ? "<i class='fa fa fa-check-square-o'></i>" : "<i class='fa fa fa-square-o'></i>" %></asp:LinkButton>
                                                        <span><%#Eval("PartNumber") %></span>
                                                        <asp:HiddenField runat="server" ID="hfIDDefineDetailProduct" Value='<%#Eval("IDDefineDetailProduct") %>' />
                                                        <div class="clearfix"></div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Repeater runat="server" ID="rptProductCategory" ClientIDMode="AutoID">
                                    <ItemTemplate>
                                        <div style="margin-left: 15px">
                                            <asp:HiddenField runat="server" ID="hfIDProductCategory" Value='<%#Eval("IDProductCategory") %>' />
                                            <asp:HiddenField runat="server" ID="hfExpand" />
                                            <asp:LinkButton runat="server" ID="lbExpand" OnClick="lbExpand_Click" Style="display: inline-block"><i class=" fa fa-plus" style="font-size:15px"></i></asp:LinkButton>
                                            <span><%#Eval("Name_En") %></span>
                                            <div class="clearfix"></div>
                                            <asp:Repeater runat="server" ID="rptMasterProduct" ClientIDMode="AutoID">
                                                <ItemTemplate>
                                                    <div style="margin-left: 15px">
                                                        <asp:HiddenField runat="server" ID="hfIDProduct" Value='<%#Eval("IDProduct") %>' />
                                                        <asp:HiddenField runat="server" ID="hfExpandMasterProduct" />
                                                        <asp:LinkButton runat="server" ID="lbExpanMasterProduct" OnClick="lbExpanMasterProduct_Click" Style="display: inline-block"><i class=" fa fa-plus" style="font-size:15px"></i></asp:LinkButton>
                                                        <span><%#Eval("Name_En") %></span>
                                                        <div class="clearfix"></div>
                                                        <div style="margin-left: 15px">
                                                            <asp:Repeater runat="server" ID="rptDefineDetailProduct" ClientIDMode="AutoID">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="lbSelectDefineDetail" OnClick="lbSelectDefineDetail_Click"><%#CheckIDInSelectedList(Eval("IDDefineDetailProduct").ToString())==true ? "<i class='fa fa fa-check-square-o'></i>" : "<i class='fa fa fa-square-o'></i>" %></asp:LinkButton>
                                                                    <span><%#Eval("PartNumber") %></span>
                                                                    <asp:HiddenField runat="server" ID="hfIDDefineDetailProduct" Value='<%#Eval("IDDefineDetailProduct") %>' />
                                                                    <div class="clearfix"></div>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </ItemTemplate>
            </asp:Repeater>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
