<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsIntspc.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_cfloantype_ctrl.DsIntspc" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater">
        <tr>
            <th rowspan="2" width="4%">
            </th>
            <th rowspan="2" width="10%">
                การครบกำหนด
            </th>
            <th rowspan="2" width="10%">
                ช่วงเวลา (เดือน)
            </th>
            <th colspan="4">
                การคิดดอกเบี้ย
            </th>
            <th rowspan="2" width="4%">
            </th>
        </tr>
        <tr>
            <th width="20%">
                รูปแบบ
            </th>
            <th width="9%">
                ด/บคงที่
            </th>
            <th width="34%">
                รหัสตารางด/บ
            </th>
            <th width="9%">
                เพิ่ม/ลด
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
                    <td width="10%">
                        <asp:DropDownList ID="inttime_type" runat="server">
                            <asp:ListItem Value="1">ชนวัน</asp:ListItem>
                            <asp:ListItem Value="2">ชนเดือน</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="inttime_amt_1" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:DropDownList ID="intrate_type" runat="server">
                            <asp:ListItem Value="0">ไม่คิด</asp:ListItem>
                            <asp:ListItem Value="1">อัตราคงที่</asp:ListItem>
                            <asp:ListItem Value="2">ตามตาราง ด/บ</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td width="9%">
                        <asp:TextBox ID="intratefix_rate" runat="server" Style="text-align: right;" ToolTip="#,##0.0000"></asp:TextBox>
                    </td>
                    <td width="34%">
                        <asp:DropDownList ID="intratetab_code" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td width="9%">
                        <asp:TextBox ID="intrate_increase" runat="server" Style="text-align: right;" ToolTip="#,##0.0000"></asp:TextBox>
                    </td>
                    <td width="4%">
                    <asp:Button ID="b_del" runat="server" Text="-"/>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
