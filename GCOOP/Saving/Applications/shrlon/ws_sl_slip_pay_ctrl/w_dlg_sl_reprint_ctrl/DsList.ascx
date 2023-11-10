<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_slip_pay_ctrl.w_dlg_sl_reprint_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 600px;">
    <tr>
        <th>
        </th>
        <th>
            เลขเอกสาร
        </th>
        <th>
            วันที่ใบเสร็จ
        </th>
        <th>
            ทะเบียน
        </th>
        <th>
            ชื่อ-ชื่อสกุล
        </th>
        <th>
            หน่วย
        </th>
        <th>
            รายการ
        </th>
        <th>
            สถานะ
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td width="5%" align="center">
                    <asp:CheckBox ID="checkselect" runat="server" Style="text-align: center" />
                </td>
                <td width="15%">
                    <asp:TextBox ID="document_no" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td width="15%">
                    <asp:TextBox ID="slip_date" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td width="12%">
                    <asp:TextBox ID="member_no" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td width="25%">
                    <asp:TextBox ID="compute1" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td width="10%">
                    <asp:TextBox ID="membgroup_code" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td width="10%">
                    <asp:TextBox ID="sliptype_code" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td width="8%">
                    <asp:TextBox ID="slip_status" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
