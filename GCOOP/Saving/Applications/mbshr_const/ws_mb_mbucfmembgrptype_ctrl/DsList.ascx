<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.mbshr_const.ws_mb_mbucfmembgrptype_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="4%">
            ที่
        </th>
        <th width="12%">
            รหัส
        </th>
        <th width="80%">
            ภูมิภาคสังกัด
        </th>
        <th width="4%">
        </th>
    </tr>
</table>
<asp:Panel ID="Panel1" runat="server" Height="500px" Width="750px" ScrollBars="Auto">
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <table class="DataSourceRepeater">
                <tr>
                    <td width="4%">
                        <asp:TextBox ID="running_number" runat="server" Style="text-align: center" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="membgrptype_code" runat="server" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td width="80%">
                        <asp:TextBox ID="membgrptype_desc" runat="server"></asp:TextBox>
                    </td>
                    <td width="4%">
                        <asp:Button ID="b_del" runat="server" Text="-" />
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:Repeater>
</asp:Panel>
