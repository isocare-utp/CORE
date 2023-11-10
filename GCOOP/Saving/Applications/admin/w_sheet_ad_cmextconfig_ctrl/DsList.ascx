<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.admin.w_sheet_ad_cmextconfig_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Width="750px">
    <table class="DataSourceRepeater">
        <tr>
            <th width="5%">
                NO.
            </th>
            <th width="10%">
                COOP_ID
            </th>
            <th width="20%">
                KEY
            </th>
            <th width="30%">
                DESCRIPTION
            </th>
            <th width="30%">
                VALUE
            </th>
            <th width="5%">
               
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Width="750px" Height="500px" ScrollBars="Auto">
    <table class="DataSourceRepeater">
        <tbody>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="5%">
                            <asp:TextBox ID="running_number" runat="server" Style="text-align: center;"></asp:TextBox>
                        </td>
                        <td width="10%">
                            <asp:TextBox ID="coop_id" runat="server"></asp:TextBox>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="ext_key" runat="server"></asp:TextBox>
                        </td>
                        <td width="30%">
                            <asp:TextBox ID="description" runat="server"></asp:TextBox>
                        </td>
                        <td width="30%">
                            <asp:TextBox ID="ext_value" runat="server" Style="text-align: center;"></asp:TextBox>
                        </td>
                        <td width="5%">
                            <asp:Button ID="b_del" runat="server" Text="ลบ" />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</asp:Panel>
