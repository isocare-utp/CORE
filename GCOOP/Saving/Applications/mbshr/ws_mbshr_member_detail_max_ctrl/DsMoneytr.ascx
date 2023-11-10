<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMoneytr.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_max_ctrl.DsMoneytr" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<span style="font-size: 13px;"><font color="#cc0000"><u><strong>รายการรับจ่าย</strong></u></font></span>
<table class="TbStyle">
    <tr>
        <th width="5%">
        </th>
        <th width="25%">
            ประเภทรายการ
        </th>
        <th width="20%">
            ทำรายการโดย
        </th>
        <th>
            รายละเอียด
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="running_number" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="trtype_code" runat="server" ReadOnly="true">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="moneytype_code" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="cp_detail" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
