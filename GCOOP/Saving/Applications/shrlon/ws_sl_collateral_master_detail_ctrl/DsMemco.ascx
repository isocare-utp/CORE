<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMemco.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_collateral_master_detail_ctrl.DsMemco" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 700px;">
    <tr>
        <th>
        </th>
        <th>
            เลขประจำตัว
        </th>
        <th>
            ชื่อ- สกุลผู้กู้
        </th>
        <th>
            สถานะผู้กู้
        </th>
        <%--<th>
        </th>--%>
    </tr>
    <asp:Repeater ID="Repeater2" runat="server">
        <ItemTemplate>
            <tr>
                <td width="5%">
                    <asp:TextBox ID="running_number" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="20%">
                    <asp:TextBox ID="memco_no" runat="server" Style="width: 95px; text-align: center;"></asp:TextBox>
                    <asp:Button ID="b_search" runat="server" Text="..." Style="width: 25px; margin-left: 7px;" />
                </td>
                <td width="50%">
                    <asp:TextBox ID="mem_name" runat="server"></asp:TextBox>
                </td>
                <td width="20%">
                    <asp:DropDownList ID="collmastmain_flag" runat="server">
                        <asp:ListItem Value="1">กู้หลัก</asp:ListItem>
                        <asp:ListItem Value="0">กู้ร่วม</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <%--<td width="5%">
                    <asp:Button ID="b_delete" runat="server" Text="ลบ" />
                </td>--%>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
