<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_insurfire_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<div align="left">
    <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
        <table class="DataSourceRepeater" style="width: 720px;">
            <tr>
                <th width="5%">
                </th>
                <th>
                    เลขที่กรมธรรม์
                </th>
                <th width="30%">
                    ระยะเวลาคุ้มครอง
                </th>
                <th width="15%">
                    เลขที่หลักทรัพย์
                </th>
                <th width="12%">
                    เบี้ยประกัน
                </th>
                <th width="12%">
                    การชำระ
                </th>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" Height="200px" ScrollBars="Auto"
        HorizontalAlign="Left">
        <table class="DataSourceRepeater" style="width: 720px;">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="5%">
                            <asp:TextBox ID="running_number" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="insurance_no" runat="server" ReadOnly="true" Style="text-align: left;"></asp:TextBox>
                        </td>
                        <td width="30%">
                            <asp:TextBox ID="insure_date" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                        </td>
                        <td width="15%">
                            <asp:TextBox ID="collmast_no" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                        </td>
                        <td width="12%">
                            <asp:TextBox ID="insurance_amt" runat="server" ReadOnly="true" ToolTip="#,##0.00"
                                Style="text-align: right;"></asp:TextBox>
                        </td>
                        <td width="12%">
                            <asp:TextBox ID="pay_status" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </asp:Panel>
</div>
