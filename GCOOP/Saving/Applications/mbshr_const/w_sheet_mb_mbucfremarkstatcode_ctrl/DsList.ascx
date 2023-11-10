<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.mbshr_const.w_sheet_mb_mbucfremarkstatcode_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<span class="NewRowLink" onclick="PostInsertRow()" style="padding-left: 30px;">เพิ่มแถว</span>
<table class="DataSourceRepeater">
    <tr>
        <th width="15%">
            รหัส
        </th>
        <th whit="70%">
            หมายเหตุสถานะ
        </th>
        <th width="15%">
            ลบ
        </th>
       
    </tr>
    <tbody>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:TextBox ID="REMARKSTATTYPE_CODE" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="REMARKSTATTYPE_DESC" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="b_del" runat="server" Text="ลบ" />
                    </td>
                    
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>
