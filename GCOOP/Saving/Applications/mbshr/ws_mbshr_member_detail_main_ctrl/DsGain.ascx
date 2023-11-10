<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsGain.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_main_ctrl.DsGain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<span style="font-size: 13px;"><font color="#cc0000"><u><strong>ข้อมูลผู้รับผลประโยชน์</strong></u></font></span>
<table class="TbStyle">
    <tr>
        <th width="5%">
            ลำดับ
        </th>
        <th width="20%">
            ชื่อ-สกุล
        </th>
        <th width="35%">
            ที่อยู่ผู้รับโอน
        </th>
        <th width="15%">
            ความสัมพันธ์
        </th>
        <th width="25%">
            หมายเหตุ
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="running_number" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="cp_name" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="gain_addr" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="gain_relation" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="remark" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
