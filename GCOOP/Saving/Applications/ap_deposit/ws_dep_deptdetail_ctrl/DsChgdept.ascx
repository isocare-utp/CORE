<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsChgdept.ascx.cs" Inherits="Saving.Applications.ap_deposit.ws_dep_deptdetail_ctrl.DsChgdept" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Height="300px" HorizontalAlign="Left"
    ScrollBars="auto">
    <table class="DataSourceRepeater">
        <tr>
            <th style="width: 5%">
                ลำดับ
            </th>
            <th style="width: 10%">
                วันที่เปลี่ยนแปลง
            </th>
            <th style="width: 20%">
                จำนวนเงิน
            </th>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td style="height: 17px;">
                        <asp:TextBox ID="running_number" runat="server" Style="text-align: center;" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td style="height: 17px;">
                        <asp:TextBox ID="DEPTMONTHCHG_DATE" runat="server" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td style="height: 17px;">
                        <asp:TextBox ID="deptmonth_newamt" runat="server" Style="text-align: right;" ReadOnly="True" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
