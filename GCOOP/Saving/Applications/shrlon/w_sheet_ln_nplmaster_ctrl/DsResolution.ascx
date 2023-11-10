<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsResolution.ascx.cs"
    Inherits="Saving.Applications.shrlon.w_sheet_ln_nplmaster_ctrl.DsResolution" %>
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <asp:TextBox ID="RESOLUTION" runat="server" TextMode="MultiLine" Style="margin-left: 10px;
            width: 695px; height: 80px; border: none;"></asp:TextBox>
    </EditItemTemplate>
</asp:FormView>
