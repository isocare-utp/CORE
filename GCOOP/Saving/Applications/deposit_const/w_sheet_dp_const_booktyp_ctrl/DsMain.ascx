<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.deposit_const.w_sheet_dp_const_booktyp_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <thead>
        <tr>
            <th width="10%">
                <div>
                    <span>ประเภทของสมุด</span>
                </div>
            </th>
            <th width="10%">
                <div>
                    <span>ประเภทกลุ่มสมุด</span>
                </div>
            </th>
            <th width="30%">
                <div>
                    <span>ชื่อประเภทกลุ่มสมุด</span>
                </div>
            </th>
            <th width="10%">
                <div>
                    <span>ลบ</span>
                </div>
            </th>
        </tr>
    </thead>
    <tbody>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="30%">
                        <asp:DropDownList ID="book_type" runat="server">
                        <asp:ListItem Text="กรุณาวเลือกประเภทของสมุด" Value=""/>
                        <asp:ListItem Text="Card" Value="C"/>
                        <asp:ListItem Text="Book" Value="B"/>
                        </asp:DropDownList>
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="book_grp" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="book_desc" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:Button ID="B_DEL" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>
