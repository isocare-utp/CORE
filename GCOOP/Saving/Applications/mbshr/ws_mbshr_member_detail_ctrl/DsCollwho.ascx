<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCollwho.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_ctrl.DsCollwho" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<span style="font-size: 13px;"><font color="#cc0000"><u><strong>ติดค้ำประกัน</strong></u></font></span>
<table class="TbStyle" style="width: 710px;">
    <tr>
        <th width="9%">
            เลขสมาชิก
        </th>
        <th width="26%">
            ชื่อ-ชื่อสกุล
        </th>
        <th width="15%">
            ทุนเรือนหุ้น
        </th>
        <th width="10%">
            เลขสัญญา
        </th>
        <th width="15%">
            ยอดคงเหลือ
        </th>
        <th width="7%">
            %ค้ำประกัน
        </th>
        <th width="15%">
            ยอดค้ำประกัน
        </th>
    </tr>
</table>
<asp:Panel ID="Panel2" runat="server" Height="200px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="TbStyle" style="width: 710px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="9%">
                        <asp:TextBox ID="member_no" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="26%">
                        <asp:TextBox ID="memb_name" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="sharestk_value" runat="server" Style="text-align: right;"
                            ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="loancontract_no" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="prnbal_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="7%">
                        <asp:TextBox ID="collactive_percent" runat="server" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="collactive_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
