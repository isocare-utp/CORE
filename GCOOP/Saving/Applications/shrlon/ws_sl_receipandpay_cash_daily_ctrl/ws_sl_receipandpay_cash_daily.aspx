<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_receipandpay_cash_daily.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_receipandpay_cash_daily_ctrl.ws_sl_receipandpay_cash_daily" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsReceipt.ascx" TagName="DsReceipt" TagPrefix="uc2" %>
<%@ Register Src="DsPayloan.ascx" TagName="DsPayloan" TagPrefix="uc3" %>
<%@ Register Src="DsShare.ascx" TagName="DsShare" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }
        function OnDsMainClicked(s, r, c) {
            if (c == "b_sum") {
                PostBack();
            }
        }
        function OnDsMainItemChanged(s, r, c, v) {

            if (c == "allentry_flag") {
                var allentry_flag = dsMain.GetItem(0, "allentry_flag");

                if (allentry_flag == 0) {
                    dsMain.GetElement(0, "entry_id").disabled = true;
                } else {
                    dsMain.GetElement(0, "entry_id").disabled = false;
                }
            } else if (c == "receipt_coop") {
                var receipt_coop = dsMain.GetItem(0, "receipt_coop");
                var sumreceipt_loan = dsMain.GetItem(0, "sumreceipt_loan");
                var sumpay_loan = dsMain.GetItem(0, "sumpay_loan");
                var netpay_loan = (receipt_coop + sumreceipt_loan) - sumpay_loan;
                dsMain.SetItem(0, "netpay_loan", netpay_loan);
            }
        }
        function SheetLoadComplete() {
            var receipt_coop = dsMain.GetItem(0, "receipt_coop");
            var sumreceipt_loan = dsMain.GetItem(0, "sumreceipt_loan");
            var sumpay_loan = dsMain.GetItem(0, "sumpay_loan");
            var netpay_loan = (receipt_coop + sumreceipt_loan) - sumpay_loan;
            dsMain.SetItem(0, "netpay_loan", netpay_loan);

            var allentry_flag = dsMain.GetItem(0, "allentry_flag");

            if (allentry_flag == 0) {
                dsMain.GetElement(0, "entry_id").disabled = true;
            } else {
                dsMain.GetElement(0, "entry_id").disabled = false;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 750px;">
        <tr>
            <td colspan="2">
                <uc1:DsMain ID="dsMain" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
        <tr>
            <td width="50%" valign="top">
                <uc2:DsReceipt ID="dsReceipt" runat="server" />
            </td>
            <td width="50%" valign="top">
                <uc3:DsPayloan ID="dsPayloan" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <uc4:DsShare ID="dsShare" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
