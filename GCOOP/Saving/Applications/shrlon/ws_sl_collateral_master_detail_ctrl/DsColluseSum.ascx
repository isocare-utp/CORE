<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsColluseSum.ascx.cs"
    Inherits="Saving.Applications.shrlon.ws_sl_collateral_master_detail_ctrl.DsColluseSum" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 700px;">
            <tr>
                <td colspan="5" style="text-align: right;">
                    <strong>รวม:</strong>
                </td>
                <td width="20%">
                    <asp:TextBox ID="sum_cp_colluse" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
