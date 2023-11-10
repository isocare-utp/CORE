<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_const_dpucfdeptitemtype_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="8%">
            รหัส
        </th>
        <th width="30%">
            ชื่อประเภทการทำรายการเงินฝาก
        </th>
        <th width="8%">
            ฝั่งรายการ
        </th>
        <th width="8%">
            รหัสกระทบ
        </th>
        <th width="8%">
            รหัสการพิมพ์
        </th>
        <th width="8%">
            กลุ่มรายการ
        </th>
        <th width="5%">
            ลบ!
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="deptitemtype_code" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="deptitemtype_desc" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="sign_flag" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="reverse_itemtype" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="print_code" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="deptitem_group" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="b_del" runat="server" Text="ลบ" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
