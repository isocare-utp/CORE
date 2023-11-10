<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMaster.ascx.cs" Inherits="Saving.Applications.divavg.ws_divsrv_detail_day_ctrl.DsMaster" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <span style="font-size: 12px;"><font color="#00AA00"><u><strong>รายละเอียด</strong></u></font></span>
        <table class="DataSourceFormView">
            <tr>
                <td width="25%">
                    <div>
                        <span style="text-align: center;">ยอดปันผล</span>
                    </div>
                </td>
                <td width="25%">
                    <div>
                        <span style="text-align: center;">ปันผลคงเหลือ</span>
                    </div>
                </td>
                <td width="25%">
                    <div>
                        <span style="text-align: center;">ยอดเฉลี่ยคืน</span>
                    </div>
                </td>
                <td width="25%">
                    <div>
                        <span style="text-align: center;">เฉลี่ยคืนคงเหลือ</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="div_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="div_balamt" runat="server" Style="text-align: right;" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="avg_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="avg_balamt" runat="server" Style="text-align: right;" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
