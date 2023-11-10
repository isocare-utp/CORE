<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsSum.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_cremation_detail_ctrl.DsSum" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
    <center>
        <table class="FormStyle" style="width: 550px;">
         <tr>
            <td style="text-align: right;">
                <strong>ยอดรวม:</strong>
            </td>
            <td width="25%">
                <asp:TextBox ID="sum_amt" runat="server" Style="font-size: 11px; text-align: right;
                    font-weight: bold;" ToolTip="#,##0.00"></asp:TextBox>
            </td>
            <td width="10%">
            </td>
        </tr>
        
        </table>
        </center>
    </EditItemTemplate>
</asp:FormView>
