<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_const_dpucfapvbook_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="7%">
            รหัส
        </th>
        <th width="22%">
            ชื่อ-นามสกุล (ไทย)
        </th>
        <th width="22%">
            ชื่อ-นามสกุล (Eng)
        </th>
        <th width="22%">
            ตำแหน่ง (ไทย)
        </th>
        <th width="22%">
            ตำแหน่ง (Eng)
        </th>
        <th width="5%">
            ลบ!
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="apv_code" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="name_th" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="name_en" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="position_th" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="position_en" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="b_del" runat="server" Text="ลบ" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
