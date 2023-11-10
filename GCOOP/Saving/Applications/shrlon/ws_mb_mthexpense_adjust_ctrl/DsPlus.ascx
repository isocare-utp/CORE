<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsPlus.ascx.cs" Inherits="Saving.Applications.shrlon.ws_mb_mthexpense_adjust_ctrl.DsPlus" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<div align="left">
    <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
        <table class="DataSourceRepeater" style="width: 360px;">
            <tr>
                <th width="10%">
                    ลำดับ
                </th>
                <th>
                    รายละเอียดรายได้
                </th>
                <th width="30%">
                    จำนวนเงิน
                </th>
                <th width="10%">
                </th>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" Height="200px" ScrollBars="Auto" HorizontalAlign="Left">
        <table class="DataSourceRepeater" style="width: 360px;">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="10%">
                            <asp:TextBox ID="running_number" runat="server" Style="text-align: center;"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="mthexpense_desc" runat="server" Style="text-align: left;"></asp:TextBox>
                        </td>
                        <td width="30%">
                            <asp:TextBox ID="mthexpense_amt" runat="server" Style="text-align: center;"></asp:TextBox>
                        </td>
                        <td width="10%">
                            <asp:Button ID="b_delete" runat="server" Text="ลบ" />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </asp:Panel>
    <table class="DataSourceFormView" style="width: 360px;">
        <tr>
            <td style="text-align: right;">
                <strong>รวม:</strong>
            </td>
            <td width="30%">
                <asp:TextBox ID="cpsum_mthexpense_amt" runat="server" Style="font-size: 11px; text-align: right;
                    font-weight: bold;" ToolTip="#,##0.00"></asp:TextBox>
            </td>
            <td width="10%">
            </td>
        </tr>
    </table>
</div>
