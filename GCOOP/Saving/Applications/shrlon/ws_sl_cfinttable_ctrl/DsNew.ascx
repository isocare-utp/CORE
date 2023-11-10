<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsNew.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_cfinttable_ctrl.DsNew" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel2" runat="server" Width="250px">
    <table class="DataSourceRepeater" style="width: 230px;">
        <tr>
            <th width="20%">
                รหัส
            </th>
            <th width="80%">
                ประเภทอัตราดอกเบี้ย
            </th>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="20%">
                        <asp:TextBox ID="loanintrate_code" runat="server"></asp:TextBox>
                    </td>
                    <td width="80%">
                        <asp:TextBox ID="loanintrate_desc" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
