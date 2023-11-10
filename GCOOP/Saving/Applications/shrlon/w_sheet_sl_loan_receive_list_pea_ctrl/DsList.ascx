<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_loan_receive_list_pea_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater">
        <tr>
            <th width="5%">
                <input type="checkbox" id="chk_all" />
            </th>
            <th width="7%">
                จ่ายเงินกู้จาก
            </th>
            <th width="11%">
                เลขที่
                <br />
                สัญญา/เลขใบ
            </th>
            <th width="6%">
                ตัวย่อเงินกู้
            </th>
            <th width="10%">
                ทะเบียนสมาชิก
            </th>
            <th width="21%">
                ชื่อ-ชื่อสกุล
            </th>
            <th width="9%">
                สังกัด
            </th>
            <th width="14%">
                ยอดรอจ่าย
            </th>
            <th width="12%">
                วันที่พิมพ์จ่าย
            </th>
            <th width="5%">
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="700px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td align="center" width="5%">
                        <asp:CheckBox ID="operate_flag" runat="server" />
                    </td>
                    <td width="7%">
                        <asp:TextBox ID="lnrcvfrom_code" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="11%">
                        <asp:TextBox ID="loancontract_no" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="6%">
                        <asp:TextBox ID="prefix" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="member_no" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="21%">
                        <asp:TextBox ID="name" runat="server" Style="text-align: left;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="9%">
                        <asp:TextBox ID="membgroup_code" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="14%">
                        <asp:TextBox ID="withdrawable_amt" runat="server" ToolTip="#,##0.00" Style="text-align: right;"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="slip_date" runat="server"  Style="text-align: center;"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="5%">
                        <asp:Button ID="b_detail" runat="server" Text="..." />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
