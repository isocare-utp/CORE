<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsLoan.ascx.cs" Inherits="Saving.Applications.shrlon.w_sheet_ln_nplmaster_ctrl.DsLoan" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="7%">
                    <div>
                        <span>สัญญา</span>
                    </div>
                </td>
                <td width="15%">
                    <div>
                        <asp:TextBox ID="loancontract_no" runat="server" Style="width: 72px;"></asp:TextBox>
                        <asp:Button ID="b_search" runat="server" Style="width: 27px;" Text="..." />
                    </div>
                </td>
                <td width="15%">
                    <div>
                        <asp:TextBox ID="loantype_desc" runat="server" ReadOnly="true" Style="background-color: #E7E7E7"></asp:TextBox>
                    </div>
                </td>
                <td width="10%">
                    <div>
                        <span>ชำระล่าสุด</span>
                    </div>
                </td>
                <td width="10%">
                    <div>
                        <asp:TextBox ID="lastpayment_date" runat="server" ReadOnly="true" Style="background-color: #E7E7E7;
                            text-align: center;"></asp:TextBox>
                    </div>
                </td>
                <td width="10%">
                    <div>
                        <span>เงินต้นเหลือ</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="principal_balance" runat="server" ReadOnly="true" Style="background-color: #E7E7E7;
                            text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
                <td width="12%">
                    <div>
                        <span>ดอกเบี้ยเหลือ</span>
                    </div>
                </td>
                <td width="10%">
                    <div>
                        <asp:TextBox ID="intset_arrear" runat="server" ReadOnly="true" Style="background-color: #E7E7E7;
                            text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
