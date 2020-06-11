<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CasscadDropDownCountry.ascx.cs" Inherits="SCMCore.Admin.UserControl.CasscadDropDownCountry" %>

<div class="col-md-4 form-group">
    <label for="Name">کشور :</label>
    <asp:DropDownList ID="drpCountry" runat="server" class=" form-control" Width="100%" OnSelectedIndexChanged="drpCountry_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
    </asp:DropDownList>
</div>
<div class="col-md-4 form-group">
    <label for="Name">استان :</label>
    <asp:DropDownList ID="drpState" runat="server" class=" form-control" Width="100%" OnSelectedIndexChanged="drpState_SelectedIndexChanged" AutoPostBack="true" Enabled="false" >
    </asp:DropDownList>
</div>
<div class="col-md-4 form-group">
    <label for="Name">شهر :</label>
    <asp:DropDownList runat="server" ID="drpCity" class="form-control" Width="100%" Enabled="false"  AppendDataBoundItems="true">
    </asp:DropDownList>
</div>

