<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsSlipadjust.ascx.cs"
    Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_main_ctrl.DsSlipadjust" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<span style="font-size: 13px;"><font color="#cc0000"><u><strong>รายการค้างชำระ</strong></u></font></span>
<table class="TbStyle">
    <tr>
        <th width="20%">
            รหัส
        </th>
        <th width="20%">
            เลขที่สัญญา
        </th>
        <th width="20%">
            ชำระเงินต้น
        </th>
        <th width="20%">
            ชำระดอกเบี้ย
        </th>
        <th width="20%">
            รวมชำระ
        </th>
    </tr>
</table>
<asp:Panel ID="Panel2" runat="server" Height="200px" ScrollBars="Auto">
    <table class="TbStyle">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="20%">
                        <asp:TextBox ID="SLIPITEMTYPE_CODE" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="loancontract_no" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="principal_adjamt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="interest_adjamt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="item_adjamt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td width="20%">
            </td>
            <td width="20%" align="center">
            รวม
            </td>
            <td width="20%">
                <asp:TextBox ID="sumprinc" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                    ReadOnly="true"></asp:TextBox>
            </td>
            <td width="20%">
                <asp:TextBox ID="sumint" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                    ReadOnly="true"></asp:TextBox>
            </td>
            <td width="20%">
                <asp:TextBox ID="sumall" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                    ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Panel>
