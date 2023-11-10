<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.deposit.w_dlg_dp_slip_recppaytype_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<div align="left">
    <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left" Width="450px">
        <table class="DataSourceRepeater" style="width: 430px;">
            <tr>
                <th width="30%">
                    รหัสรายการ
                </th>
                <th width="70%">
                    รายการ
                </th>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" Height="390px" ScrollBars="Auto" HorizontalAlign="Left"
        Width="450px">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <table class="DataSourceRepeater" style="width: 430px;">
                    <tr>
                        <td width="30%">
                            <asp:TextBox ID="recppaytype_code" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                        </td>
                        <td width="70%">
                            <asp:TextBox ID="recppaytype_desc" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>
    </asp:Panel>
</div>
