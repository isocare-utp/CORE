<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.mbshr.w_sheet_mb_mbucfprename_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<span style="text-decoration: underline; font-size: 13px; font-weight: bold;">รายการคำนำหน้าชื่อ</span>
<span class="NewRowLink" onclick="PostInsertRow()" style="padding-left:30px;">เพิ่มแถว</span> 
<table class="DataSourceRepeater" style="width: 500px;">
    <tr>
        <th width="15%">
            รหัส
        </th>
        <th width="30%">
            คำอธิบาย
        </th>
        <th width="30%">
            อักษรย่อ
        </th>
        <th width="20%">
            เพศ
        </th>
        <th width="5%">
        
        </th>
    </tr>
    <tbody>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:TextBox ID="PRENAME_CODE" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="PRENAME_DESC" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="PRENAME_SHORT" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="SEX" runat="server">
                            <asp:ListItem Value="M">ชาย</asp:ListItem>
                            <asp:ListItem Value="F">หญิง</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="b_del" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>
