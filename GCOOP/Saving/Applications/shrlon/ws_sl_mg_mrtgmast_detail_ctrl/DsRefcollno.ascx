<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsRefcollno.ascx.cs"
    Inherits="Saving.Applications.shrlon.ws_sl_mg_mrtgmast_detail_ctrl.DsRefcollno" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 720px;">
    <tr>
        <th>
            ทะเบียนหลักทรัพย์
        </th>
        <th>
            รายละเอียดหลักทรัพย์
        </th>
        <th>
            ราคาประเมิน
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td width="20%">
                    <asp:TextBox ID="collmast_no" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="60%">
                    <asp:TextBox ID="collmast_desc" runat="server"></asp:TextBox>
                </td>
                <td width="20%">
                    <asp:TextBox ID="estimate_price" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
