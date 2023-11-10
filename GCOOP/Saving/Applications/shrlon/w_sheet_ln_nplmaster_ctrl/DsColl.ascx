<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsColl.ascx.cs" Inherits="Saving.Applications.shrlon.w_sheet_ln_nplmaster_ctrl.DsColl" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server">
    <table class="DataSourceRepeater" style="width: 300px;">
        <tr>
            <th width="25%">
                เลขที่
            </th>
            <th width="75%">
                รายการ
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Width="320px" Height="208px" ScrollBars="Vertical">
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <table class="DataSourceRepeater" style="width: 300px;">
                <tr>
                    <td width="25%">
                        <asp:TextBox ID="ref_collno" runat="server" Style="background: #E7E7E7; cursor: pointer;"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="75%">
                        <asp:TextBox ID="description" runat="server" Style="background: #E7E7E7; cursor: pointer;"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:Repeater>
</asp:Panel>
