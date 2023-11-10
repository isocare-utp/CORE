<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.app_finance.ws_fin_pay_mnrt_person_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" Width="406px">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 770px;">
            <%--<tr>
                <td colspan="8">
                    <div align="right">
                        <asp:CheckBox ID="print_status" runat="server" />
                        บันทึก&พิมพ์ใบเสร็จ
                    </div>
                </td>
            </tr>--%>
            <tr>
                <td width="12%">
                    <div>
                        <span style="font-size: 12px;">เลขที่ใบเสร็จ :</span>
                    </div>
                </td>
                <td width="12%">
                    <asp:TextBox ID="payoutslip_no" runat="server" Style="font-size: 12px; text-align: center;
                        background-color: #FFFF99;" ReadOnly="true"></asp:TextBox>
                </td>
                <td width="12%">
                    <div>
                        <span style="font-size: 12px;">วันที่ใบเสร็จ :</span>
                    </div>
                </td>
                <td width="12%">
                    <asp:TextBox ID="slip_date" runat="server" Style="font-size: 12px; text-align: center;
                        background-color: #FFFF99;" ReadOnly="true"></asp:TextBox>
                </td>
                <td width="12%">
                    <div>
                        <span style="font-size: 12px;">วันที่รายการ :</span>
                    </div>
                </td>
                <td width="12%">
                    <asp:TextBox ID="operate_date" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                </td>
                <td width="10%">
                    <div>
                        <span style="font-size: 12px;">รหัสรายการ :</span>
                    </div>
                </td>
                <td width="14%">
                    <asp:DropDownList ID="sliptype_code" runat="server" Enabled="false">
                        <asp:ListItem Text="LRT:จ่ายคืนเงินต้นพร้อมดอกเบี้ย" Value="LRT"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px;">ทะเบียนสมาชิก :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="member_no" runat="server" Style="width: 65px; text-align: center;"></asp:TextBox>
                    <asp:Button ID="b_memsearch" runat="server" Text="..." Style="width: 20px; margin-right: 0px;" />
                </td>
                <td>
                    <div>
                        <span style="font-size: 12px;">ชื่อ-ชื่อสกุล :</span>
                    </div>
                </td>
                <td colspan="5">
                    <asp:TextBox ID="fullname" runat="server" Style="font-size: 12px; text-align: left;
                        background-color: #FFFF99;" Width="99%" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px;">ทำรายการโดย :</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="moneytype_code" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span style="font-size: 12px;">หน่วย :</span>
                    </div>
                </td>
                <td colspan="5">
                    <asp:TextBox ID="membgroup" runat="server" Style="font-size: 12px; text-align: left;
                        background-color: #FFFF99;" Width="99%" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
            <td>
                    <div>
                        <span>ธนาคาร:</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="expense_bank" runat="server" Style="width: 200px">
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span>สาขา :</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="expense_branch" runat="server" Style="width: 200px">
                    </asp:DropDownList>
                </td>
                <td colspan="2">
                    <strong style="margin-left: 5px; font-size: 18px;">รวมยอดชำระ : </strong>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px;">เลขบัญชี :</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="expense_accid" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span style="font-size: 12px;">คู่บัญชี : </span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="tofrom_accid" runat="server" Style="width: 200px">
                    </asp:DropDownList>
                </td>
                <td colspan="2" rowspan="2">
                    <asp:TextBox ID="payoutnet_amt" runat="server" Style="background-color: Black; margin-left: 5px;
                        text-align: right; font-size: 24px;" Width="180" Height="45" ToolTip="#,##0.00"
                        ForeColor="GreenYellow"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สถานะรายการ:</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="compute1" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ผู้ทำรายการ :</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="entry_id" runat="server" Style="width: 198px;"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
