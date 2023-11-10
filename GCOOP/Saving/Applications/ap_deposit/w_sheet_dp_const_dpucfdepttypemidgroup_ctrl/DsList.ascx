<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_const_dpucfdepttypemidgroup_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="8%">
            รหัส
        </th>
        <th>
            คำอธิบาย
        </th>
        <th width="18%">
            รูปแบบเลขบัญชี
        </th>
        <th width="14%">
            จัดพิมพ์ปก
        </th>
        <th width="14%">
            พิมพ์รายการเคลื่อไหว
        </th>
        <th width="5%">
            ลบ!
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="depttype_group" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="depttype_desc" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="deptcode_format" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="dw_first_page" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="dw_movement" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="b_del" runat="server" Text="ลบ" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
