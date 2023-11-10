<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCollprop.ascx.cs"
    Inherits="Saving.Applications.shrlon.ws_sl_collateral_master_detail_ctrl.DsCollprop" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 700px;">
    <tr>
        <th>
        </th>
        <th>
            ชื่อ-ชื่อสกุลผู้มีกรรมสิทธิ์
        </th>
        <%--<th>
        </th>--%>
    </tr>
    <asp:Repeater ID="Repeater2" runat="server">
        <ItemTemplate>
            <tr>
                <td width="5%">
                    <asp:TextBox ID="running_number" runat="server"></asp:TextBox>
                </td>
                <td width="90%">
                    <asp:TextBox ID="prop_desc" runat="server"></asp:TextBox>
                </td>
               
                <%--<td width="5%">
                    <asp:Button ID="b_delprop" runat="server" Text="ลบ" />
                </td>--%>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
