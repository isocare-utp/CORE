<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsLoanpay.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_contract_adjust_ctrl.DsLoanpay" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 365px;">
            <tr>
                <td width="30%">
                    <div>
                        <span style="font-size: 12px">รหัสเรียกเก็บ:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="loanpay_code" runat="server" Style="width: 245px; font-size: 12px;">
                        <asp:ListItem Value="KEP">หักรายเดือน(บัญชีเงินเดือน)</asp:ListItem>
                        <asp:ListItem Value="KOT">หักรายเดือน(บัญชีนอก)</asp:ListItem>
                        <asp:ListItem Value="TRN">หักจากเงินฝากสหกรณ์</asp:ListItem>
                        <asp:ListItem Value="CBT">หักจากบัญชีธนาคาร</asp:ListItem>
                        <asp:ListItem Value="CSH">ชำระเองเงินสด</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px">ธนาคาร:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="loanpay_bank" runat="server" Style="width: 245px; font-size: 12px;">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px">สาขา:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="loanpay_branch" runat="server" Style="width: 245px; font-size: 12px;">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px">เลขที่บัญชี:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="loanpay_accid" runat="server" Style="text-align: center; font-size: 12px;"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>