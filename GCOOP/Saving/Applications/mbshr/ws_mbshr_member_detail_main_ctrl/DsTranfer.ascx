<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsTranfer.ascx.cs" 
Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_main_ctrl.DsTranfer" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="TbStyle" style="width: 710px">
    <tr>
        <th width="16%">
            วันที่โอนย้าย
        </th>
        <th width="10%">
            โอนงวดหุ้น
        </th>
        <th width="17%">
            ยอดหุ้นรับโอน
        </th>
        <th width="17%">
            ยอดหนี้รับโอน
        </th>
        <th width="40%">
            รายละเอียด
        </th>
    </tr>
</table>
<asp:Panel ID="Panel2" runat="server" Height="500px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="TbStyle" style="width: 710px">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="16%">
                        <asp:TextBox ID="TRAN_DATE" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="TRAN_PERIOD" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="17%">
                        <asp:TextBox ID="TRAN_SHARE" runat="server" Style="text-align: right;" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="17%">
                        <asp:TextBox ID="TRAN_LOAN" runat="server" Style="text-align: right;" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="40%">
                        <asp:TextBox ID="TRAN_DETAIL" runat="server" Style="text-align: left;" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
