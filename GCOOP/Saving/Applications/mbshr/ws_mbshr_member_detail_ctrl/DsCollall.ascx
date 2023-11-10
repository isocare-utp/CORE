<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCollall.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_ctrl.DsCollall" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<span style="font-size: 13px;"><font color="#cc0000"><u><strong>หลักประกันที่นำมาใช้ค้ำสัญญาเงินกู้</strong></u></font></span>
<table class="TbStyle" style="width: 710px;">
    <tr>
        <th width="10%">
            เลขที่สัญญา
        </th>
        <th width="16%">
            ประเภทหลักประกัน
        </th>
        <th width="10%">
            เลขหลักประกัน
        </th>
        <th width="27%">
            รายละเอียด
        </th>
        <th width="15%">
            เงินกู้คงเหลือ
        </th>
        <th width="7%">
            %การค้ำ
        </th>
        <th width="15%">
            ยอดเงินค้ำประกัน
        </th>
    </tr>
</table>
<asp:Panel ID="Panel2" runat="server" Height="200px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="TbStyle" style="width: 710px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="10%">
                        <asp:TextBox ID="loancontract_no" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="16%">
                        <asp:TextBox ID="loancolltype_desc" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="ref_collno" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td  width="27%">
                        <asp:TextBox ID="description" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="prnbal_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="7%">
                        <asp:TextBox ID="collactive_percent" runat="server" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="collactive_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
