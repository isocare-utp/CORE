<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsSpare.ascx.cs" Inherits="Saving.Applications.admin.w_sheet_am_replication_detail_ctrl.DsSpare" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 320px;">
            <tr>
                <td width="45%">
                    <div>
                        <span>ใบเสร็จเงินฝาก:</span>
                    </div>
                </td>
                <td width="55%">
                    <asp:TextBox ID="DEPT_SLIP" runat="server" Style="width: 100px;"></asp:TextBox>
                    <asp:TextBox ID="DEPT_COUNT" runat="server" Style="width: 55px; text-align: right;"
                        ToolTip="#,##0"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ใบเสร็จรับชำระพิเศษ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="SL_SLIP" runat="server" Style="width: 100px;"></asp:TextBox>
                    <asp:TextBox ID="SL_COUNT" runat="server" Style="width: 55px; text-align: right;"
                        ToolTip="#,##0"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ใบเสร็จการเงิน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="FIN_SLIP" runat="server" Style="width: 100px;"></asp:TextBox>
                    <asp:TextBox ID="FIN_COUNT" runat="server" Style="width: 55px; text-align: right;"
                        ToolTip="#,##0"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
