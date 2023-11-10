<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" 
Inherits="Saving.Applications.shrlon.ws_sl_fundcoll_payment_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="14%">
                    <div>
                        <span>เลขที่ใบสำคัญ :</span></div>
                </td>
                <td width="11%">
                    <asp:TextBox ID="document_no" runat="server" Style="text-align:center;"></asp:TextBox>
                </td>
                <td width="14%">
                    <div>
                        <span>วันที่ใบจ่าย :</span></div>
                </td>
                <td width="11%">
                    <asp:TextBox ID="slip_date" runat="server" Style="text-align:center;"></asp:TextBox>
                </td>
                <td width="14%">
                    <div>
                        <span>วันที่รายการ :</span></div>
                </td>
                <td width="11%">
                    <asp:TextBox ID="operate_date" runat="server" Style="text-align:center;"></asp:TextBox>
                </td>
                <td width="14%">
                    <div>
                        <span>รหัสการจ่าย :</span></div>
                </td>
                <td width="11%">
                    <asp:DropDownList ID="sliptype_code" runat="server">
                        <asp:ListItem Value="FRT" Text="จ่ายคืนเงินกองทุน, ดอกเบี้ยกองทุน"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ทะเบียนสมาชิก :</span></div>
                </td>
                <td>
                    <asp:TextBox ID="member_no" runat="server" Style="text-align:center;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ชื่อ-ชื่อสกุล :</span></div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="fullname" runat="server" Width="258px"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>สังกัด :</span></div>
                </td>
                <td>
                    <asp:TextBox ID="membgroup_code" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table class="DataSourceRepeater">
            <tr>
                <th colspan="5">
                    ประเภทเงินทำรายการ
                </th>
                <th>
                    เงินจ่ายสุทธิ
                </th>
            </tr>
            <tr>
                <th>
                    ประเภทเงิน
                </th>
                <th>
                    ธนาคาร
                </th>
                <th>
                    สาขา
                </th>
                <th>
                    เลขที่บัญชี
                </th>
                <th>
                    คู่บัญชี
                </th>
                <td rowspan="2" style="background-color: #000000">
                    <asp:TextBox ID="payoutnet_amt" runat="server" BackColor="Black" ForeColor="Lime"
                        Font-Size="20px" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="moneytype_code" runat="server"></asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="expense_bank" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="expense_branch" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="expense_accid" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="tofrom_accid" runat="server"></asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>

