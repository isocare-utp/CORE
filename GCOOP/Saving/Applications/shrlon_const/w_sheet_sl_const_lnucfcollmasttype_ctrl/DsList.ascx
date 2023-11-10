<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon_const.w_sheet_sl_const_lnucfcollmasttype_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="12%">
            รหัสประเภทหลักทรัพย์
        </th>
        <th>
            ชื่อหลักทรัพย์
        </th>
        <th width="12%">
            ตัวย่อหลักทรัพย์
        </th>
        <th width="15%">
            รหัสเลขที่ล่าสุด
        </th>
        <th width="5%">
            ลบ
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="COLLMASTTYPE_CODE" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="COLLMASTTYPE_DESC" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="COLLDOC_PREFIX" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="DOCUMENT_CODE" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="b_del" runat="server" Text="ลบ" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
