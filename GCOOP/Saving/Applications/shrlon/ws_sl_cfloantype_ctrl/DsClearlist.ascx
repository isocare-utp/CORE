<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsClearlist.ascx.cs"
    Inherits="Saving.Applications.shrlon.ws_sl_cfloantype_ctrl.DsClearlist" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server">
    <table class="DataSourceRepeater">
        <tr>
            <th colspan="2" rowspan="2" width="30%">
                เงินกู้ที่ต้องหักกลบ
            </th>
            <th colspan="2">
                ชำระมาแล้ว
            </th>
            <th rowspan="2" width="5%">
                ตรวจ<br />
                ส.ญ<br />
                หลัก
            </th>
            <th colspan="4">
                ชำระไม่ครบคิดค่าปรับ
            </th>
            <th rowspan="2" width="4%">
            </th>
        </tr>
        <tr>
            <th width="4%">
                งวด
            </th>
            <th width="6%">
                %
            </th>
            <th width="15%">
                เงื่อนไขปรับ
            </th>
            <th width="6%">
                %
            </th>
            <th width="15%">
                ขั้นต่ำ
            </th>
            <th width="15%">
                ไม่เกิน
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="200px" Width="750px" ScrollBars="Auto">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="3%">
                        <asp:TextBox ID="loantype_clear1" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="27%">
                        <asp:DropDownList ID="loantype_clear" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td width="4%">
                        <asp:TextBox ID="minperiod_pay" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="6%">
                        <asp:TextBox ID="minpercent_pay" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="5%" align="center">
                        <asp:CheckBox ID="chkcontcredit_flag" runat="server" />
                    </td>
                    <td width="15%">
                        <asp:DropDownList ID="finecond_type" runat="server">
                            <asp:ListItem Value="0">ไม่มีปรับ</asp:ListItem>
                            <asp:ListItem Value="1">ไม่ครบอันใดอันหนึง</asp:ListItem>
                            <asp:ListItem Value="2">ไม่ครบทั้งสองอัน</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td width="6%">
                        <asp:TextBox ID="fine_percent" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="fine_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="fine_maxamt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="4%">
                        <asp:Button ID="b_del" runat="server" Text="-" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
