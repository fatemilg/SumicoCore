<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TreeView.ascx.cs" Inherits="SCMCore.Admin.UserControl.TreeView" %>
<div dir="rtl">
        <asp:TreeView ID="TreeView1" runat="server" ImageSet="XPFileExplorer" ExpandDepth="0" NodeIndent="15"
             ShowLines="true" >
            <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
            <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px" 
                NodeSpacing="0px" VerticalPadding="2px"></NodeStyle>
            <ParentNodeStyle Font-Bold="False" />
            <SelectedNodeStyle Font-Underline="False" HorizontalPadding="0px"
                VerticalPadding="0px" BackColor="#B5B5B5" />
        </asp:TreeView>

</div>