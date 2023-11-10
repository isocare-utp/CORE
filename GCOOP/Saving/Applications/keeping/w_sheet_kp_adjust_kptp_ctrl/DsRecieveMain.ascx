<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsRecieveMain.ascx.cs"
    Inherits="Saving.Applications.keeping.w_sheet_kp_adjust_kptp_ctrl.DsRecieveMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width:720px">
            <tr>
                <td width="12%">
                    <div>
                        <span>เลขที่ใบเสร็จ :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="receipt_no" runat="server" Style="text-align: center" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                <td width="12%">
                    <div>
                        <span>วันที่ใบเสร็จ :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="receipt_date" runat="server" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>สถานะใบเสร็จ :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="keeping_status" runat="server" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="13%">
                    <div>
                        <span>วันที่ชำระ/คืน :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="operate_date" runat="server" Style="text-align: center" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>ทุนเรือนหุ้น :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="sharestkbf_value" runat="server" ReadOnly="true" Style="text-align: right"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>ด/บ สะสม :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="interest_accum" runat="server" ReadOnly="true" Style="text-align: right"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td></td><td></td><td></td><td></td>
                <td>
                    <div>
                        <span>ยอดรวมชำระ :</span>
                    </div>
                </td>
                <td><div><asp:TextBox ID="PAYIN_AMOUNT" runat="server" ReadOnly="true" Style="text-align: right"
                            ToolTip="#,##0.00"></asp:TextBox></div></td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
