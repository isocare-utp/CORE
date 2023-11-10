<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_auditloan_history_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="15%">
            เลขที่เอกสาร
        </th>
        <th width="20%">
            วันที่แก้ไข
        </th>
        <th width="15%">
            เลขสัญญา
        </th>
        <th width="10%">
            ทะเบียน
        </th>
        <th width="25%">
            ชื่อ - ชื่อสกุล
        </th>
        <th width="15%">
            ผู้แก้ไข
        </th>
    </tr>
</table>
<asp:Panel ID="Panel2" runat="server" Height="265px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="15%">
                        <asp:TextBox ID="modtbdoc_no" runat="server" Style="text-align: center; cursor: pointer"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="entry_date" runat="server" Style="text-align: center; cursor: pointer"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="clmkey_desc" runat="server" Style="text-align: center; cursor: pointer"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="member_no" runat="server" Style="text-align: center; cursor: pointer"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="memb_name" runat="server" Style="text-align: left; cursor: pointer"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="entry_id" runat="server" Style="text-align: center; cursor: pointer"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
