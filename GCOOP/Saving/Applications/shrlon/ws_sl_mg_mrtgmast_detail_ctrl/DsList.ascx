<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_mg_mrtgmast_detail_ctrl.DsList" %>
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
        <th width="25%">
            ทะเบียนจำนอง
        </th>
        <th width="25%">
            ประเภทหลักทรัพย์
        </th>
        <th width="30%">
            ประเภทจำนอง
        </th>
         <th width="20%">
            สถานะ
        </th>
    </tr>
</table>
<asp:Panel ID="Panel2" runat="server" Height="115px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater" style="width: 720px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="25%">
                        <asp:TextBox ID="mrtgmast_no" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="assettype_desc" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="cp_mrtgtype" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="mrtgmast_status" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
