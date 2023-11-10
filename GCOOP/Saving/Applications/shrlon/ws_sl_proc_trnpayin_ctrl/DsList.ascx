<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_proc_trnpayin_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 500px;">
    <tr>
        <th width="20%">
            รายละเอียด
        </th>
        <th width="10%">
            จำนวน
        </th>
        <th width="20%">
            จำนวนเงิน
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="slipitemtype_desc" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="tran_count" runat="server" Style="text-align: right;" ToolTip="#,##0"
                        ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="trans_amt" runat="server" ToolTip="#,##0.00" Style="text-align: right;"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
