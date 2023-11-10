<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_collateral_detail_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 600px;">
    <tr>
        <th width="30%">
            เลขที่หลักทรัพย์
        </th>
        <th width="30%">
            ประเภท
        </th>
        <th width="40%">
            ราคาประเมิน
        </th>
    </tr>
    <asp:Repeater ID="Repeater3" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="collmast_no" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="cp_colltype" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="est_price" runat="server" ToolTip="#,##0.00" Style="text-align: right;"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
