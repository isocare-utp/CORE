<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.account_const.w_sheet_vmmapaccid_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Width="750px">
    <table class="DataSourceRepeater">
        <tr>
            <th width="5%">
                ลำดับ
            </th>
            <th width="8%">
                ระบบ
            </th>
            <th width="8%">
                รหัส
            </th>
            <th width="8%">
                ประเภท
            </th>
            <th width="5%">
                สถานะ
            </th>
            <th width="13%">
                รหัสบัญชี
            </th>
            <th width="13%">
                ดอกเบี้ยรับ/จ่าย
            </th>
            <th width="13%">
                ดอกเบี้ยค้างรับ/จ่าย
            </th>
            <th width="13%">
                รหัสภาษี
            </th>
            <th width="4%">
                ลบ
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Width="750px" Height="495px" ScrollBars="Auto">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="5%">
                        <asp:TextBox ID="running_number" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:TextBox ID="system_code" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:TextBox ID="slipitemtype_code" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:TextBox ID="shrlontype_code" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="5%">
                        <asp:TextBox ID="shrlontype_status" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="13%">
                        <asp:TextBox ID="account_id" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="13%">
                        <asp:TextBox ID="accint_id" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="13%">
                        <asp:TextBox ID="accintarr_id" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="13%">
                        <asp:TextBox ID="acctax_id" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="4%">
                        <asp:Button ID="b_del" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
