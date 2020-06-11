<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TreeDropDown.ascx.cs" Inherits="SCMCore.Admin.UserControl.TreeDropDown" %>
<asp:UpdatePanel runat="server" ID="updatepanelPopover">
    <ContentTemplate>
        <asp:HiddenField runat="server" ID="hfIDSelected" />
        <asp:Label ID="txtTitle" ReadOnly="true" CssClass="form-control txtTreeDropDown" runat="server" onclick="OpenTreeDropDown(this)" Text="گروه های تعریف شده"></asp:Label>
        <div class="divTreeDropDown" style="overflow: scroll !important; display: none; max-height: 400px;">
            <asp:TreeView ID="tvDropDown" runat="server" ExpandDepth="0" NodeIndent="15" CollapseImageUrl="../images/details_close.png" ExpandImageUrl="../images/details_open.png"
                SelectAction="None" OnSelectedNodeChanged="tvDropDown_SelectedNodeChanged">
                <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                <NodeStyle Font-Size="13px" Font-Bold="False" ForeColor="Black" HorizontalPadding="0px"
                    NodeSpacing="0px" VerticalPadding="0px"></NodeStyle>
                <ParentNodeStyle Font-Bold="False" />
                <SelectedNodeStyle Font-Bold="False" Font-Size="13px" HorizontalPadding="0px" VerticalPadding="0px" BackColor="#B5B5B5" Font-Underline="False"></SelectedNodeStyle>
            </asp:TreeView>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script type="text/javascript">
    function OpenTreeDropDown(obj) {
        $(obj).parent().find('.divTreeDropDown').slideToggle();
        $(obj).parent().find('.divTreeDropDown').css({ 'overflow-y': 'scroll' });
    }
</script>
