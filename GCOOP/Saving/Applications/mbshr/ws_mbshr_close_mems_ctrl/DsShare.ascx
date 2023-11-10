<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsShare.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_close_mems_ctrl.DsShare" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 720px;">
    <tr>
        <th width="15%">
            รายละเอียด
        </th>
        <th width="10%">
            มูลค่า/หุ้น
        </th>
        <th width="15%">
            ยกมาต้นปี
        </th>
        <th width="5%">
            งวด
        </th>
        <th width="10%">
            หุ้นฐาน
        </th>
        <th width="10%">
            หุ้น/เดือน
        </th>
        <th width="10%">
            สถานะส่ง
        </th>
        <th width="10%">
            ทุนเรือนหุ้น
        </th>
        <th width="15%">
            สถานะหุ้น
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td align="center">
                    <asp:TextBox ID="cp_sharename" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:TextBox ID="unitshare_value" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                        ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="cp_sharebegin_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                        ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="last_period" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="cp_periodbase_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                        ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="cp_periodshare_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                        ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="payment_status" runat="server">
                        <asp:ListItem Value="0" Text=""></asp:ListItem>
                        <asp:ListItem Value="1">ปกติ</asp:ListItem>
                        <asp:ListItem Value="-1">งดส่ง</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="cp_sharestk_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                        ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="sharemaster_status" runat="server">
                        <asp:ListItem Value="0" Text=""></asp:ListItem>
                        <asp:ListItem Value="1">ปกติ</asp:ListItem>
                        <asp:ListItem Value="5">หุ้นค้าง</asp:ListItem>
                        <asp:ListItem Value="-1">ปิดบัญชีหุ้น</asp:ListItem>
                        <asp:ListItem Value="8">หุ้นรอจัดสรร</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
