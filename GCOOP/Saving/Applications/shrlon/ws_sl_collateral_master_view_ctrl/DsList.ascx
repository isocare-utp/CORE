<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_collateral_master_view_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table>
    <tr>
        <td>
            <strong style="font-size: 14px;">รายการทะเบียนหลักทรัพย์</strong>
        </td>
    </tr>
</table>
<table class="DataSourceRepeater" style="width: 720px;">
    <tr>
        <th width="25%">
            ทะเบียนหลักทรัพย์
        </th>
        <th width="25%">
            ประเภทหลักทรัพย์
        </th>
        <th width="30%">
            ราคากรมที่ดิน
        </th>
         <th width="20%">
            ราคาประเมิน
        </th>
    </tr>
</table>
<asp:Panel ID="Panel2" runat="server" Height="115px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater" style="width: 720px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="25%">
                        <asp:TextBox ID="collmast_no" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="collmasttype_desc" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="dol_prince" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="est_price" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
