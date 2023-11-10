<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_mg_export_mrtgmast_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Height="450px" ScrollBars="Auto" HorizontalAlign="Left">
    <table class="DataSourceRepeater">
        <tr>
            <th width="5%">
                <asp:CheckBox ID="cb_all" runat="server" />
            </th>
            <th width="15%">
                ทะเบียนจำนอง
            </th>
            <th width="20%">
                ประเภทหลักทรัพย์
            </th>
            <th width="15%">
                ประเภทจำนอง
            </th>
            <th width="12%">
                ทะเบียน
            </th>
            <th width="33%">
                ชื่อ-สกุล
            </th>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <asp:CheckBox ID="operate_flag" runat="server" Style="text-align: center" />
                    </td>
                    <td align="center">
                        <asp:TextBox ID="mrtgmast_no" runat="server" ReadOnly="True" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="assettype_desc" runat="server" ReadOnly="True" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="cp_mrtgtype" runat="server" ReadOnly="True" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="member_no" runat="server" ReadOnly="True" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="full_name" runat="server" ReadOnly="True" Style="text-align: center"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
