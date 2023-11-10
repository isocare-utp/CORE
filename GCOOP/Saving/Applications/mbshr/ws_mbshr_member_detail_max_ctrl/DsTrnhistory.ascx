<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsTrnhistory.ascx.cs"
    Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_max_ctrl.DsTrnhistory" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<span style="font-size: 13px;"><font color="#cc0000"><u><strong>ประวัติการโอนสถานภาพสมาชิก</strong></u></font></span>
<table class="TbStyle" >
    <tr>
        <th>
            เลขสมาชิกเดิม
        </th>
        <th>
            เลขสมาชิกใหม่
        </th>
        <th width="20%">
            วันอนุมัติ
        </th>
        <th width="25%">
            ผู้อนุมัติ
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="memold_no" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="memnew_no" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="apv_date" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="apv_id" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
