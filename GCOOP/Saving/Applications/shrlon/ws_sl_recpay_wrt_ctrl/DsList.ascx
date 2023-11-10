<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_recpay_wrt_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 770px;">
    <tr>
        <th>
        </th>
        <th>
            รายละเอียดการทำรายการ
        </th>
        <th>
            จำนวนเงิน
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:CheckBox ID="operate_flag" runat="server" />
                </td>
                <td>
                    <asp:TextBox ID="payment_desc" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="itempay_amt" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
