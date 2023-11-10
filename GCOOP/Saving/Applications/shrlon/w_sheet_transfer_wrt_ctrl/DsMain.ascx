<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.w_sheet_transfer_wrt_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 350px;">
            <tr>
                <td colspan="2" style="text-align:center;">
                    <span style="text-align:center;">โอนรายการกองทุน ก.ส.ส.</span>
                </td>
            </tr>
            <tr>
                <td width="35%">
                    <asp:TextBox ID="operate_date" runat="server" style="text-align:center;" ReadOnly="true"></asp:TextBox>
                </td>
                <td width="65%">
                    <asp:DropDownList ID="acc_id" runat="server" BackColor="#FFFFCC">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="tacc_id" runat="server" ReadOnly="true" style="text-align:center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="50%">
                <span style="width:60px;">ยอดถอน</span>
                    <asp:TextBox ID="wss" runat="server" ReadOnly="true" style="text-align:right; width:100px;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="50%">
                <span style="width:60px;">ยอดฝาก</span>
                    <asp:TextBox ID="dss" runat="server" ReadOnly="true" style="text-align:right; width:100px;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
