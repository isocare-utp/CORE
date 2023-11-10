<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsSum.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_contract_adjust_ctrl.DsSum" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet" type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet" type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" Style="width: 150px;">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>

            <td width="10%"></td>
                <td width="12%">
                    
                </td>
                <td width="42%"> 

                </td>
                <td width="12%">
                   รวม:
                </td>
                <td width="12%">
                   <asp:TextBox ID="sum_collactive_amt" runat="server" Style="text-align: right; margin-left: 4px;
                        background-color: Black;" ForeColor="#66FF66" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="8%">
                    <asp:TextBox ID="sum_collactive_percent" runat="server" Style="text-align: right; margin-left: 4px;
                        background-color: Black;" ForeColor="#66FF66" ToolTip="#,##0.00"></asp:TextBox>
                <td align="center" width="3%">
                   
                </td>
                <td align="center" width="3%">
                   
                </td>
               
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
