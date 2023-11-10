<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.mbshr_const.w_sheet_mb_shucfshritemtype_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<%--<span class="NewRowLink" onclick="PostInsertRow()" style="padding-left: 30px;">เพิ่มแถว</span>--%>
<table class="DataSourceRepeater">
    <tr>
        <th width="10%">
            รหัสรายการ
        </th>
        <th width="45%">
            ชื่อประเภทการทำรายการหุ้น
        </th>
        <th width="15%">
            รหัสพิมพ์
        </th>
        <th width="10%">
            รหัสกระทบ
        </th>
        <th width="10%">
            ฝั่งรายการ
        </th>
        <th width="10%">
            ลบ
        </th>
    </tr>
    <tbody>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:TextBox ID="SHRITEMTYPE_CODE" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="SHRITEMTYPE_DESC" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="SIGN_FLAG" runat="server">
                            <asp:ListItem Value="0">-</asp:ListItem>
                            <asp:ListItem Value="1">จ่าย(1)</asp:ListItem>
                            <asp:ListItem Value="-1">ไม่จ่าย(-1)</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="PRINT_CODE" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="REVERSE_ITEMTYPE" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="b_del" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>
