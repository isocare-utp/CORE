<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.admin_const.w_sheet_am_const_cmdocumentcontrol_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Width="750px">
    <table class="DataSourceRepeater">
        <tr>
            <th width="10%">
                รหัส
            </th>
            <th>
                ชื่อเอกสาร
            </th>
            <th width="10%">
                ปีเอกสาร
            </th>
            <th width="15%">
                เลขที่ล่าสุด
            </th>
            <th width="4%">
                clear ปลายปี
            </th>
            <th width="10%">
                syscode
            </th>
            <th width="10%">
                group code
            </th>
            <th width="4%">
                ลบ
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Width="750px" Height="435px" ScrollBars="Auto">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="10%">
                        <asp:TextBox ID="document_code" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="document_name" runat="server"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="document_year" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td align="center" width="15%">
                        <asp:TextBox ID="last_documentno" runat="server" Style="text-align: center;" /></asp:TextBox>
                    </td>
                    <td align="center" width="4%">
                        <asp:CheckBox ID="clear_type" runat="server" />
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="system_code" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="docgroup_code" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="4%">
                        <asp:Button ID="b_del" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
