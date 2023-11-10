<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsIntspc.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_contract_adjust_ctrl.DsIntspc" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 760px;">
    <tr>
        <th style="font-size: 12px">
            วันที่เริ่มใช้
        </th>
        <th style="font-size: 12px">
            วันที่สิ้นสุด
        </th>
        <th style="font-size: 12px">
            รูปแบบดอกเบี้ย
        </th>
        <th style="font-size: 12px">
            อัตราคงที่
        </th>
        <th style="font-size: 12px">
            ตารางดอกเบี้ย
        </th>
        <th style="font-size: 12px">
            อัตราเพิ่ม/ลด
        </th>
        <th>
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="effective_date" runat="server" Style="text-align: center; font-size: 12px;" ></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="expire_date" runat="server" Style="text-align: center; font-size: 12px;" ></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="int_continttype" runat="server" style="font-size: 12px;">
                        <asp:ListItem Value="0">ไม่คิด ด/บ</asp:ListItem>
                        <asp:ListItem Value="1">อัตราคงที่</asp:ListItem>
                        <asp:ListItem Value="2">ดูจากตาราง</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="int_contintrate" runat="server" Style="text-align: right; font-size: 12px;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="int_continttabcode" runat="server" style="font-size: 12px;">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="int_contintincrease" runat="server" Style="width: 50px; text-align: right; font-size: 12px;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:Button ID="b_del" runat="server" Text="-" Style="width: 25px; font-size: 12px;" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>