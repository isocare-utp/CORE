<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.app_finance.ws_fin_finitemtype_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
   <table class="DataSourceRepeater">
    <tr>
        <th width="25%">
            รหัสบัญชี
        </th>
        <th width="45%">
            ชื่อบัญชี
        </th>
        <th width="25%">
            คู่บัญชี
        </th>
        <th width="5%">
            ลบ
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="SLIPITEMTYPE_CODE" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="ITEM_DESC" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="ACCOUNT_ID" runat="server"></asp:TextBox>
                </td>
                <td >
                    <asp:Button ID="b_del" runat="server" Text="ลบ" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
