<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsListSlip.ascx.cs"
    Inherits="Saving.Applications.keeping.w_sheet_kp_adjust_kptp_ctrl.DsListSlip" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width:195px" >
    <tr>
        <th width="10%">
        </th>
        <th >
            งวดเก็บ
        </th>
        <th width="35%">
            อ้างอิงสมาชิก
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="number" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="recv_period" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="member_no" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
