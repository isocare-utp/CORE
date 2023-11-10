<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_loan_receive_order_coll_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <thead>
        <tr>
            <th width="4%" style="height: 40px;">
            </th>
            <th width="8%">
                จ่ายเงินกู้จาก
            </th>
            <th width="13%">
                เลขที่สัญญา / เลขใบ
            </th>
            <th width="6%">
                ตัวย่อเงินกู้
            </th>
            <th width="11%">
                ทะเบียนสมาชิก
            </th>
            <th width="35%">
                ชื่อ-ชื่อสกุล
            </th>
            <th width="10%">
                สังกัด
            </th>
            <th width="13%">
                ยอดรอจ่าย
            </th>
        </tr>
    </thead>
    <tbody>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <asp:CheckBox ID="operate_flag" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="lnrcvfrom_code" runat="server" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="loancontract_no" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="lntype_prefix" runat="server" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="member_no" runat="server" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="compute_1" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="membgroup_code" runat="server" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="withdrawable_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>
