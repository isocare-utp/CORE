<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCollreqgrt.ascx.cs"
    Inherits="Saving.Applications.shrlon.ws_sl_cfloantype_ctrl.DsCollreqgrt" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater">
        <tr>
            <th rowspan="2" width="4%">
            </th>
            <th colspan="2">
                วงเงินกู้
            </th>
            <th rowspan="2" width="7%">
                บังคับใช้หุ้นค้ำ
            </th>
            <th colspan="5">
                การใช้สมาชิกค้ำ(คน)
            </th>
            <th width="4%" rowspan="2">
            </th>
        </tr>
        <tr>
            <th width="19%">
                ตั้งแต่
            </th>
            <th width="19%">
                ถึง
            </th>
            <th width="22%">
                ประเภทสมาชิกที่ค้ำได้
            </th>
            <th width="6%">
                จำนวน
            </th>
            <th width="6%">
                ปกติ
            </th>
            <th width="7%">
                เงื่อนไข
            </th>
            <th width="6%">
                สบทบ
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="110px" Width="750px" ScrollBars="Auto">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="4%">
                        <asp:TextBox ID="running_number" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="19%">
                        <asp:TextBox ID="money_from" runat="server" ToolTip="#,##0.00" Style="text-align: right;"></asp:TextBox>
                    </td>
                    <td width="19%">
                        <asp:TextBox ID="money_to" runat="server" ToolTip="#,##0.00" Style="text-align: right;"></asp:TextBox>
                    </td>
                    <td width="7%" align="center">
                        <asp:CheckBox ID="useshare_flag" runat="server" />
                    </td>
                    <td width="22%">
                        <asp:DropDownList ID="useman_type" runat="server">
                            <asp:ListItem Value="0">ไม่ตรวจคนค้ำ</asp:ListItem>
                            <asp:ListItem Value="1">ค้ำได้ทุกประเภทสมาชิก</asp:ListItem>
                            <asp:ListItem Value="2">ค้ำตามประเภท</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td width="6%">
                        <asp:TextBox ID="useman_amt" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="6%">
                        <asp:TextBox ID="usememmain_amt" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="7%">
                        <asp:DropDownList ID="usemem_operation" runat="server">
                        <asp:ListItem Value="1">และ</asp:ListItem>
                        <asp:ListItem Value="2">หรือ</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td width="6%">
                        <asp:TextBox ID="usememco_amt" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="4%">
                        <asp:Button ID="b_del" runat="server" Text="-" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
