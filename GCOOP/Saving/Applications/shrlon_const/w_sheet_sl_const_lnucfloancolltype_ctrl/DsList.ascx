<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon_const.w_sheet_sl_const_lnucfloancolltype_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="30%">
            รหัสการค้า
        </th>
        <th>
            ประเภทการค้ำประกัน
        </th>
        <th width="5%">
            ลบ
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="LOANCOLLTYPE_CODE" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="LOANCOLLTYPE_DESC" runat="server"></asp:TextBox>
                </td>
                <td >
                    <asp:Button ID="b_del" runat="server" Text="ลบ" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
