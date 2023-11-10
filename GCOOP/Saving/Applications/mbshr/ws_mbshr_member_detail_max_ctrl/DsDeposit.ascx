<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDeposit.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_max_ctrl.DsDeposit" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
    <span style="font-size: 13px;"><font color="#cc0000"><u><strong>บัญชีเงินฝาก</strong></u></font></span>
    <table class="TbStyle">
    <tr>
        <th width="10%">
        ลำดับ
        </th>
        <th width="30%">
            เลขที่บัญชี
        </th>
        <th width="30%">
            ชื่อบัญชี
        </th>
        <th width="30%">
            คงเหลือ
        </th>
       
    </tr>
</table>
<asp:Panel ID="Panel2" runat="server" Height="200px" ScrollBars="Auto">
    <table class="TbStyle">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="10%">
                        <asp:TextBox ID="running_number" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="DEPTACCOUNT_NO" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                     <td width="30%">
                        <asp:TextBox ID="DEPTACCOUNT_NAME" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="PRNCBAL" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                   
                   
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
