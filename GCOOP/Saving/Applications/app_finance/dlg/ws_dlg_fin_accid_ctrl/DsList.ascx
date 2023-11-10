<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.app_finance.dlg.ws_dlg_fin_accid_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"    type="text/css" />
<div align="left">
    <asp:Panel ID="Panel1" runat="server"  HorizontalAlign="Left">
        <table class="DataSourceRepeater" style="width: 470px;">
            <tr>
                <th width="20%">
                    รหัส
                </th>
                <th width="60%">
                    คำอธิบาย
                </th>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" Height="390px" ScrollBars="Auto"  HorizontalAlign="Left">
        <table class="DataSourceRepeater" style="width: 470px;">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="20%">
                            <asp:TextBox ID="SLIPITEMTYPE_CODE" runat="server" ReadOnly="true" Style="text-align:center;cursor: pointer"></asp:TextBox>
                        </td>
                        <td width="60%">
                            <asp:TextBox ID="ITEM_DESC" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </asp:Panel>
</div>
