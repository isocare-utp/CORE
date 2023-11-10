<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_mg_mrtgmast_edit_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table>
    <tr>
        <td>
            <strong style="font-size: 14px;">รายการจำนอง</strong>
        </td>
    </tr>
</table>
<table class="DataSourceRepeater" style="width: 720px;">
    <tr>
        <th width="30%">
            ทะเบียนจำนอง
        </th>
        <th width="30%">
            ประเภทหลักทรัพย์
        </th>
        <th width="40%">
            ประเภทจำนอง
        </th>
    </tr>
</table>
<asp:Panel ID="Panel2" runat="server" Height="115px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater" style="width: 720px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="30%">
                        <asp:TextBox ID="mrtgmast_no" runat="server"></asp:TextBox>
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="assettype_desc" runat="server"></asp:TextBox>
                    </td>
                    <td width="40%">
                        <asp:TextBox ID="cp_mrtgtype" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
