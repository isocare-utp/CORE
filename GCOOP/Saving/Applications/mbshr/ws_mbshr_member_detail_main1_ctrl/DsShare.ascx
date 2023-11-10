<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsShare.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_main_ctrl.DsShare" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<span style="font-size: 13px;"><font color="#cc0000"><u><strong>รายการหุ้น</strong></u></font></span>
<table class="TbStyle">
    <tr>
        <th width="15%">
            รายละเอียด
        </th>
        <th width="18%">
            ยกมาต้นปี
        </th>
        <th width="5%">
            งวด
        </th>
        <th width="8%">
            หุ้นฐาน
        </th>
        <th width="14%">
            หุ้น/เดือน
        </th>
        <th width="9%">
            สถานะส่ง
        </th>
        <th width="17%">
            ทุนเรือนหุ้น
        </th>
        <th width="10%">
            สถานะหุ้น
        </th>
        <th width="4%">
        </th>
    </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:TextBox ID="nameshare" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="cp_sharebegin_unit" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="last_period" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="cp_periodbase_unit" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="cp_periodshare_unit" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="cp_payment_status" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                        <%-- <asp:DropDownList ID="payment_status" runat="server" Enabled="false">
                            <asp:ListItem Value="0" Text=""></asp:ListItem>
                            <asp:ListItem Value="1">ปกติ</asp:ListItem>
                            <asp:ListItem Value="-1">งดส่ง</asp:ListItem>
                        </asp:DropDownList>--%>
                    </td>
                    <td>
                        <asp:TextBox ID="cp_sharestk_unit" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="cp_sharemaster_status" runat="server" Style="text-align: center;"
                            ReadOnly="true"></asp:TextBox>
                        <%--<asp:DropDownList ID="sharemaster_status" runat="server" Enabled="false">
                            <asp:ListItem Value="0" Text=""></asp:ListItem>
                            <asp:ListItem Value="1">ปกติ</asp:ListItem>
                            <asp:ListItem Value="5">หุ้นค้าง</asp:ListItem>
                            <asp:ListItem Value="-1">ปิดบัญชีหุ้น</asp:ListItem>
                            <asp:ListItem Value="8">หุ้นรอจัดสรร</asp:ListItem>
                        </asp:DropDownList>--%>
                    </td>
                    <td>
                        <asp:Button ID="bshr_detail" runat="server" Text="..." Width="25px" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
</table>
