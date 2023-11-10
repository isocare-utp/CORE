<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_contract_adjust_coll_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" Style="width: 760px;">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td>
                    <div>
                        <span>เลขทะเบียน:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="member_no" runat="server" Style="width: 80px; text-align: center;"></asp:TextBox>
                        <asp:Button ID="b_search" runat="server" Text="..." Style="width: 20px; margin-left: 1px;" />
                    </div>
                </td>
                <td>
                    <div>
                        <span>ชื่อ-ชื่อสกุล:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="cp_name" runat="server" Style="width: 275px; background-color: #CCCCCC;" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>วันที่ปรับปรุง:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="contadjust_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขที่สัญญา:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:DropDownList ID="loancontract_no" runat="server" Style="width: 82px;">
                        </asp:DropDownList>
                        <asp:Button ID="b_contsearch" runat="server" Text="..." Style="width: 20px; margin-left: 1px;" />
                    </div>
                </td>
                <td>
                    <div>
                        <span>ประเภทเงินกู้:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="cp_loantype" runat="server" ReadOnly="true" Style="width: 275px; background-color: #CCCCCC;
                        "></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ประเภทสัญญา:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="contlaw_status" runat="server" Style="width: 90px;visibility" Enabled="false">
                        </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="12%">
                    <div>
                        <span>ยอดอนุมัติ:</span>
                    </div>
                </td>
                <td width="15%">
                    <asp:TextBox ID="loanapprove_amt" runat="server" Style="text-align: right; background-color: #CCCCCC;"
                        ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                </td>
                <td width="11%">
                    <div>
                        <span>ยอดรอเบิก:</span>
                    </div>
                </td>
                <td width="13%">
                    <asp:TextBox ID="withdrawable_amt" runat="server" Style="text-align: right; background-color: #CCCCCC;"
                        ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                </td>
                <td width="11%">
                    <div>
                        <span>ยอดคงเหลือ:</span>
                    </div>
                </td>
                <td width="13%">
                    <asp:TextBox ID="principal_balance" runat="server" Style="text-align: right; background-color: #CCCCCC;"
                        ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                </td>
                <td width="12%">
                    <div>
                        <span>การอนุมัติ:</span>
                    </div>
                </td>
                <td width="12%">
                    <asp:CheckBox ID="contadjust_status" Text="  อนุมัติ" runat="server" Style="text-align: left;" Enabled="false"/>
                </td>
            </tr>
            <tr>
                <td width="12%">
                    <div>
                        <span>หมายเหตุ:</span>
                    </div>
                </td>
                <td colspan="7">
                    <asp:DropDownList ID="adjustcause_code" runat="server"> </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
