<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_convert_txt2db_ctrl.dlg_sl_bankbranch_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />

<asp:Panel ID="Panel1" runat="server" Height="320px" Width="480px" ScrollBars="Auto"
    HorizontalAlign="Left">
    <table class="DataSourceRepeater" style="width: 460px;">
        <tr>
            <th style="width: 13%;">
                รหัส
            </th>
            <th style="width: 87%;">
                ธนาคาร
            </th>
        </tr>
    </table>
    <table class="DataSourceRepeater" class="tbVersion" style="width: 460px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td style="width: 13%;">
                        <asp:TextBox ID="bank_code" runat="server" ReadOnly="true" Style="text-align: center; cursor:pointer;"></asp:TextBox>
                    </td>
                    <td style="width: 87%;">
                        <asp:TextBox ID="bank_desc" runat="server" ReadOnly="true" Style="text-align: center; cursor:pointer;"></asp:TextBox>
                    </td>
                    <asp:HiddenField ID="bank_code_name" runat="server" />
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
