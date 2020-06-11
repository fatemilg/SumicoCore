<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TextEditor.ascx.cs" Inherits="SCMCore.Admin.UserControl.TextEditor" %>
<asp:Panel runat="server" ID="pnlSummerNoteEditor">
    <textarea class="summernotes" id="<%=pnlSummerNoteEditor.ClientID%>txtSummerNoteEditor"></textarea>
    <asp:UpdatePanel runat="server" ID="up1" ClientIDMode="AutoID">
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="hfContentOfSummerNote" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Panel>



