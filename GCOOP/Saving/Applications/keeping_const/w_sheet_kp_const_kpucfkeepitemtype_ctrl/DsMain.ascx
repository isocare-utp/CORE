<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.keeping_const.w_sheet_kp_const_kpucfkeepitemtype_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />

    <table class="DataSourceRepeater">
    <tr>
        <th width="10%">
            รหัสรายการ
        </th>
        <th>
            ชื่อประเภทการทำรายการจัดเก็บ
        </th>
        <th width="10%">
            รหัสกลุ่ม
        </th>
        <th width="4%">
            ประมวลผลเท่านั้น
        </th>
        <th width="10%">
            การเรียงในใบเสร็จ
        </th>
        <th width="10%">
            ฝั่งรายการ
        </th>
        <th width="10%">
            ระบบ
        </th>
        <th width="10%">
            เรียกเก็บจาก
        </th>
        <th width="4%">
            ลบ
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="KEEPITEMTYPE_CODE" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="KEEPITEMTYPE_DESC" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="KEEPITEMTYPE_GRP" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:CheckBox ID="SYSTEM_FLAG" runat="server"  />
                </td>
                <td>
                    <asp:TextBox ID="SORT_IN_RECEIVE" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="SIGN_FLAG" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="SYSTEM_CODE" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="TRTYPE_CODE" runat="server">
                    <asp:ListItem Value="" Text="ไม่ระบุ"></asp:ListItem>
                    <asp:ListItem Value="KEEP1" Text="KEEP1" ></asp:ListItem>
                    <asp:ListItem Value="KEEP2" Text="KEEP2" ></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="b_del" runat="server" Text="ลบ" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>

