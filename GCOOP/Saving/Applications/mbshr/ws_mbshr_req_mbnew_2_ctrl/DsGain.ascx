<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsGain.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_req_mbnew_2_ctrl.DsGain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater" style="width: 760px;">
        <tr>
            <th rowspan="2" width="5%">
            </th>
            <th rowspan="2" width="10%">
                คำนำหน้า
            </th>
            <th rowspan="2" width="15%">
                ชื่อ
            </th>
            <th rowspan="2" width="15%">
                นามสกุล
            </th>
            <th colspan="2" width="20%">
                ที่อยู่ผู้รับโอน
            </th>
            <th rowspan="2" width="10%">
                ความสัมพันธ์
            </th>
            <th rowspan="2" width="20%">
                หมายเหตุ
            </th>
            <th rowspan="2" width="15%">
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="700px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater" style="width: 760px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td align="center" width="5%">
                        <asp:TextBox ID="running_number" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:DropDownList ID="prename_code" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="gain_name" runat="server" Style="text-align: center;"></asp:TextBox>

                    </td>
                    <td width="15%">
                        <asp:TextBox ID="gain_surname" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="bank_branch" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:DropDownList ID="gain_relation" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="remark" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:Button ID="b_del" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>