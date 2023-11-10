<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wd_list.ascx.cs" Inherits="Saving.Applications.mbshr.ws_sl_share_edit_ctrl.wd_list" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 200px;">
    <tr>
        <th width="20%" style="font-size: 12px;">
            รหัส
        </th>
        <th width="40%" style="font-size: 12px;">
            ประเภท
        </th>
        <th width="40%" style="font-size: 12px;">
            ทุนเรือนหุ้น
        </th>
    </tr>
</table>
<table class="DataSourceRepeater" style="width: 200px;">
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td width="20%">
                    <asp:TextBox ID="sharetype_code" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                </td>
                <td width="40%">
                    <asp:TextBox ID="sharetype_desc" runat="server" Style="font-size: 12px;"></asp:TextBox>
                </td>
                <td width="40%">
                    <asp:TextBox ID="cp_shrstk" runat="server" Style="font-size: 12px; text-align: right;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
