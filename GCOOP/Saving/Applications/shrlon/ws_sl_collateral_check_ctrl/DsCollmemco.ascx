<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCollmemco.ascx.cs"
    Inherits="Saving.Applications.shrlon.ws_sl_collateral_check_ctrl.DsCollmemco" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table>
    <tr>
        <td>
            <strong>ผู้กู้</strong>
        </td>
    </tr>
</table>
<table class="DataSourceRepeater" style="width: 270px;">
    <tr>
        <th>
            ทะเบียน
        </th>
        <th>
            ชื่อ-ชื่อสกุล
        </th>
        <th>
            หลัก
        </th>
    </tr>
    <asp:Repeater ID="Repeater3" runat="server">
        <ItemTemplate>
            <tr>
                <td width="30%">
                    <asp:TextBox ID="memco_no" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="cp_name" runat="server"></asp:TextBox>
                </td>
                <td width="10%" align="center">
                    <asp:CheckBox ID="collmastmain_flag" runat="server" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
