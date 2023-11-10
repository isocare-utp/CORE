<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsListSum.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_collateral_master_detail_ctrl.DsListSum" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 750px;">
            <tr>
                <td width="50%">
                </td>
                <td width="50%">
                    <asp:TextBox ID="cp_sum_redeemflag" runat="server" Style="text-align: right; width: 370px; margin-left:373px;"
                        ToolTip="#,##0.00" ForeColor="#66FF33" BackColor="Black" ></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
