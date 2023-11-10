<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.admin.w_sheet_am_coopmaster_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th>
            coop_id
        </th>
        <th>
            COOP_NAME
        </th>
        <th>
            COOP_CONTROL
        </th>
        <th>
            COOP_SUBTYPE
        </th>
        <th>
            PREFIX_COOP
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="COOP_ID" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="COOP_NAME" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="COOP_CONTROL" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="COOP_SUBTYPE" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="PREFIX_COOP" runat="server"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
