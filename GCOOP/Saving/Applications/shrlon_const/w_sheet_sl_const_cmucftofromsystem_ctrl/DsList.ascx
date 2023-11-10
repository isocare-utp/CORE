<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon_const.w_sheet_sl_const_cmucftofromsystem_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="10%">
            รหัสระบบ
        </th>
        <th width="40%">
            ชื่อระบบที่ทำรายการโอนภายใน
        </th>
        <th width="40%">
            UO_NAME
        </th>
        <th width="5%" >
            ลบ
        </th>
    </tr>
   
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="SYSTEM_CODE" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="SYSTEM_DESC" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="UO_NAME" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="b_del" runat="server" Text="ลบ" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
