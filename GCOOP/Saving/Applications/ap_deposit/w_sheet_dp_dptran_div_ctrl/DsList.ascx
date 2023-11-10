<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_dptran_div_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel2" runat="server" HorizontalAlign="Left" Width="317px">
    <table class="DataSourceRepeater" style="width: 300px;">
        <tr>
            <th width="30%">
                สมาชิกเลขที่
            </th>
            <th>
                เลขที่บัญชี
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel1" runat="server" Style="width: 317px; height: 445px" ScrollBars="Auto">
    <table class="DataSourceRepeater" style="width: 300px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="30%">
                        <asp:TextBox ID="member_no" runat="server" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="deptaccount_no" runat="server" Style="text-align: center"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
