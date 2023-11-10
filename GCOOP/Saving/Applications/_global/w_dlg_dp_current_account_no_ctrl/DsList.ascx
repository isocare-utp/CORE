<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications._global.w_dlg_dp_current_account_no_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<div align="left">
    <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left" Width="450px">
        <table class="DataSourceRepeater" style="width: 450px;">
            <tr>
                <th width="20%">
                    รหัสประเภท
                </th>
                <th width="55%">
                    ชื่อประเภท
                </th>
                <th width="25%">
                    เลขที่บัญชีล่าสุด
                </th>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" Height="390px" ScrollBars="Auto" HorizontalAlign="Left"
        Width="470px">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <table class="DataSourceRepeater" style="width: 450px;">
                    <tr>
                        <td width="20%">
                            <asp:TextBox ID="document_code" runat="server" ReadOnly="true" Style="cursor: pointer;
                                text-align: center;"></asp:TextBox>
                        </td>
                        <td width="55%">
                            <asp:TextBox ID="document_name" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                        </td>
                        <td width="25%">
                            <asp:TextBox ID="last_documentno" runat="server" ReadOnly="true" Style="cursor: pointer;
                                text-align: right;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>
    </asp:Panel>
</div>
