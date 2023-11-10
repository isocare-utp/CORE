<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_collateral_master_view_ctrl.ws_dlg_sl_editcollateral_master_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table>
    <tr>
        <td colspan="5">
            <strong style="font-size: 14px;">รายการหลักทรัพย์ </strong>
        </td>
    </tr>
</table>
<table class="DataSourceRepeater" style="width: 600px;">
    <tr>
        <th width="20%">
            เลขที่หลักทรัพย์
        </th>
        <th width="15%">
            ประเภท
        </th>
        <th width="45%">
            ชื่อ - สกุล
        </th>
        <th width="20%">
            ราคาประเมิน
        </th>
    </tr>
    <asp:Repeater ID="Repeater3" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="collmast_no" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="cp_colltype" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="cp_memdetail" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="est_price" runat="server" ToolTip="#,##0.00" Style="text-align: right;"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
