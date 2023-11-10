<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsPauseloan.ascx.cs"
    Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_ctrl.DsPauseloan" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<span style="font-size: 13px;"><font color="#cc0000"><u><strong>ห้ามกู้</strong></u></font></span>
<table class="TbStyle">
    <tr>
        <th width="10%">
            รหัส
        </th>
        <th width="30%">
            ประเภทที่งดกู้
        </th>
        <th width="60%">
            หมายเหตุ
        </th>
    </tr>
</table>
<asp:Panel ID="Panel2" runat="server" Height="200px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="TbStyle">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="10%">
                        <asp:TextBox ID="loantype_code" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="loantype_desc" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="60%">
                        <asp:TextBox ID="pauseloan_cause" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
