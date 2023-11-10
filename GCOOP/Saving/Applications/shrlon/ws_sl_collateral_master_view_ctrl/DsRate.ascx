<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsRate.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_collateral_master_view_ctrl.DsRate" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="FormStyle" style="width: 200px">
            <tr>
                <td width="40%">
                    <asp:TextBox ID="txt1" runat="server" Style="border-color: White"></asp:TextBox>
                    <%--<div>
                        <span><asp:Label ID="lbl1" runat="server"></asp:Label></span></div>--%>
                </td>
                <td>
                    <asp:TextBox ID="dol_prince" runat="server" ToolTip="#,##0.00" Style="text-align: right;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ประเมินร้อยละ</span></div>
                </td>
                <td>
                    <asp:TextBox ID="est_percent" runat="server" ToolTip="#,##0.00" Style="text-align: right;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ราคาประเมิน</span></div>
                </td>
                <td>
                    <asp:TextBox ID="est_price" runat="server" ToolTip="#,##0.00" Style="text-align: right;"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
