<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.admin.w_sheet_am_amsecwinsgroup_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="25%">
            รหัสกลุ่ม
        </th>
        <th width="60%">
            ชื่อกลุ่ม
        </th>
        <th width="10%">
            ลำดับ
        </th>
        <th width="5%">
            &nbsp;
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="GROUP_CODE" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="GROUP_DESC" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="GROUP_ORDER" runat="server" Style="text-align: right"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="b_del" runat="server" Text="ลบ" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
