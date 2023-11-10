<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_auditloan_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel2" runat="server" Width="220px">
    <strong style="font-size: 12px;">รายการสัญญา</strong>
     <asp:Panel ID="Panel1" runat="server" Height="400px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater" style="width: 200px;">
        <tr>
            <th width="12%">
            </th>
            <th width="40%">
                สัญญา
            </th>
            <th width="48%">
                คงเหลือ
            </th>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:TextBox ID="loantype_code" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="loancontract_no" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="principal_balance" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
   </asp:Panel>
    <br />
    <table class="DataSourceFormView" style="width: 200px;">
        <tr>
            <td width="30%">
                <div>
                    <span>รวม:</span>
                </div>
            </td>
            <td width="70%">
                <asp:TextBox ID="cp_sumprincipal" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                    ForeColor="Red" Font-Bold="True"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Panel>
