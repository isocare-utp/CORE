<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsWins.ascx.cs" Inherits="Saving.Applications.admin.w_sheet_am_amsecwins_ctrl.DsWins" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <thead>
        <tr>
            <th width="11%">
                ไอดี
            </th>
            <th width="23%">
                ชื่อไฟล์
            </th>
            <th width="20%">
                ชื่อหน้าจอ
            </th>
            <th width="9%">
                พารามิเตอร์
            </th>
            <th width="8%">
                option
            </th>
            <th width="13%">
                กลุ่ม
            </th>
            <th width="5%">
                ลำดับ
            </th>
            <th width="2%">
                ประเภท
            </th>
            <th width="5%">
                ใช้
            </th>
            <th width="5%">
                C1/E2
            </th>
            <th width="3%">
                ลบ
            </th>
        </tr>
    </thead>
    <tbody>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:TextBox ID="WINDOW_ID" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="WIN_OBJECT" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="WIN_DESCRIPTION" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="WIN_PARAMETER" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="WIN_TOOLBAR" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="GROUP_CODE" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="WIN_ORDER" runat="server" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="OPEN_TYPE" runat="server" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:CheckBox ID="USED_FLAG" runat="server" />
                    </td>
                    <td align="center">
                        <asp:TextBox ID="CORE_FLAG" runat="server"  Style="text-align: center"/></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:Button ID="B_DEL" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>
