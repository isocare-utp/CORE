<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMainDetail.ascx.cs" Inherits="Saving.Applications.mbshr.w_sheet_mb_constant_memgruop_ctrl.DsMainDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />

    <table class="DataSourceRepeater" style="width: 600px;">
        <tr>
            <th style="width: 0.2%;">
                #
            </th>
            <th style="width: 2%;">
                รหัสสังกัด
            </th>
            <th style="width: 3.5%;">
                ชื่อสังกัด
            </th>
        </tr>
    </table>
    <asp:Panel ID="Panel1" class = "Detail_H" runat="server" Height="200px" Width="620px" ScrollBars="Auto"
    HorizontalAlign="Left">
    <table class="DataSourceRepeater" style="width: 600px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr class="td_row">
                    <td style="width: 0.3%;">
                        <asp:TextBox class="num_row" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 2.1%;">
                        <asp:TextBox ID="ls_mbg_code" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 3.5%;">
                        <asp:TextBox ID="ls_mbg_name" runat="server" ReadOnly="true" Style="text-align: left;"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
