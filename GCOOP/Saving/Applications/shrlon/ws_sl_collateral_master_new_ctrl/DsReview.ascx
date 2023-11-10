<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsReview.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_collateral_master_new_ctrl.DsReview" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table>
    <tr>
        <td>
            <strong>ทบทวนราคา</strong>
        </td>
    </tr>
</table>
<table class="DataSourceRepeater" style="width: 710px;">
    <tr>
        <th>
            ครั้งที่
        </th>
        <th>
            วันที่
        </th>
        <th>
            ราคาประเมิน
        </th>
        <th>
            หมายเหตุ
        </th>
        <th>
        </th>
    </tr>
    <asp:Repeater ID="Repeater2" runat="server">
        <ItemTemplate>
            <tr>
                <td width="10%">
                    <asp:TextBox ID="review_no" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="20%">
                    <asp:TextBox ID="review_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="20%">
                    <asp:TextBox ID="estreview_amt" runat="server" Style="text-align: right;" ToolTip="#,##.00"></asp:TextBox>
                </td>
                <td width="45%">
                    <asp:TextBox ID="remark" runat="server"></asp:TextBox>
                </td>
                <td width="5%">
                    <asp:Button ID="b_delreview" runat="server" Text="ลบ" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
