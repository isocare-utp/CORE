<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_transloan_collateral_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" Width="480px">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="13%">
                    <div>
                        <span>เลขทำรายการ:</span></div>
                </td>
                <td width="14%">
                    <asp:TextBox ID="trnlnreq_docno" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                </td>
                <td width="13%">
                    <div>
                        <span>ประเภทรายการ:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="trnlnreq_code" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
                <td width="13%">
                    <div>
                        <span>วันที่ทำรายการ:</span>
                    </div>
                </td>
                <td width="12%">
                    <asp:TextBox ID="trnlnreq_date" runat="server" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขสมาชิก:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="member_no" runat="server" Width="75px"></asp:TextBox><asp:Button
                        ID="b_searchmemb" runat="server" Text=".." Width="20px" />
                </td>
                <td>
                    <div>
                        <span>ชื่อ-สกุล:</span></div>
                </td>
                <td colspan="5">
                    <asp:TextBox ID="c_memb_name" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขสัญญา:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="loancontract_no" runat="server" Width="75px"></asp:TextBox><asp:Button
                        ID="b_searchcont" runat="server" Text=".." Width="20px" />
                </td>
                <td>
                    <div>
                        <span>ประเภทเงินกู้:</span></div>
                </td>
                <td colspan="5">
                    <asp:TextBox ID="c_loantype_desc" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เงินต้นคงเหลือ:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="bfprnbal_amt" runat="server" ToolTip="#,##0.00" Style="text-align: right" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ด/บ ค้างชำระ:</span></div>
                </td>
                <td width="12%">
                    <asp:TextBox ID="bfintarrear_amt" runat="server" ToolTip="#,##0.00" Style="text-align: right" ReadOnly="true"></asp:TextBox>
                </td>
                <td width="13%">
                    <div>
                        <span>คิด ด/บ ล่าสุด:</span>
                    </div>
                </td>
                <td width="10%">
                    <asp:TextBox ID="bflastcalint_date" runat="server" Style="text-align: center" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ชำระ/งวด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="bfperiod_payment" runat="server" ToolTip="#,##0.00" Style="text-align: right" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
