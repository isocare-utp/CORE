<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_loan_receive_order.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_loan_receive_list_ctrl.w_dlg_loan_receive_order_ctrl.w_dlg_loan_receive_order" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc2" %>
<%@ Register Src="DsAdd.ascx" TagName="DsAdd" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsDetail = new DataSourceTool;
        var dsAdd = new DataSourceTool;


        function Validate() {
        }
        function OnDsDetailItemChanged(s, r, c, v) {
            if (c == "operate_flag") {
                dsDetail.SetRowFocus(r);
                PostOperateFlag();

            }

            else if (c == "principal_payamt" || c == "interest_payamt") {
                dsDetail.SetRowFocus(r);
                var prin = dsDetail.GetItem(r, "principal_payamt");
                var inter = dsDetail.GetItem(r, "interest_payamt");
                var bfshrcont_balamt = dsDetail.GetItem(r, "bfshrcont_balamt");
                var total = prin + inter;
                var item_balance = bfshrcont_balamt - prin;
                dsDetail.SetItem(r, "item_payamt", total);
                dsDetail.SetItem(r, "item_balance", item_balance);

                var sum_total = 0;
                for (var i = 0; i < dsDetail.GetRowCount(); i++) {
                    sum_total = sum_total + dsDetail.GetItem(i, "item_payamt");
                }
                dsMain.SetItem(0, "payoutclr_amt", sum_total);

            } else if (c == "item_payamt") {
                dsDetail.SetRowFocus(r);
                var item = dsDetail.GetItem(r, "item_payamt");
                var inter = dsDetail.GetItem(r, "interest_payamt");
                var total = item - inter;
                if (item <= inter) {
                    dsDetail.SetItem(r, "interest_payamt", item);
                    dsDetail.SetItem(r, "principal_payamt", 0);

                } else {
                    dsDetail.SetItem(r, "principal_payamt", total);
                }
                var sum_total = 0;
                for (var i = 0; i < dsDetail.GetRowCount(); i++) {
                    sum_total = sum_total + dsDetail.GetItem(i, "item_payamt");
                }
                dsMain.SetItem(0, "payoutclr_amt", sum_total);

            }

        }
        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "bank_code") {
                PostBank();
            } else if (c == "rcvperiod_flag") {
                PostRcvperiod();
            } else if (c == "payout_amt" || c == "payoutclr_amt") {

                dsMain.SetRowFocus(r);
                var payout_amt = dsMain.GetItem(r, "payout_amt")
                var payoutclr_amt = dsMain.GetItem(r, "payoutclr_amt");
                var sum = payout_amt - payoutclr_amt;
                dsMain.SetItem(r, "payoutnet_amt", sum);
            }
        }

        function OnDsAddItemChanged(s, r, c, v) {
            if (c == "operate_flag") {
                PostOperateFlagAdd();
            } else if (c == "item_payamt") {
                PostItem();
            } else if (c == "slipitemtype_code") {
            dsAdd.SetRowFocus(r);
                PostSlipItem();
            }
        }
        function DialogLoadComplete() {
            //alert('test');

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <br />
    <div>
        <asp:Label ID="lbCurrentLoan" runat="server" Text="Label" Style="margin-left: 90px;"></asp:Label>
        <input type="button" value="บันทึก000" onclick="PostSave()" style="margin-left: 630px;" />
        <input type="button" value="ยกเลิก" onclick="PostCancel()" style="margin-left: 2px;" />
    </div>
    <div align="center">
        <uc1:DsMain ID="dsMain" runat="server" />
        <br />
        <uc2:DsDetail ID="dsDetail" runat="server" />
        <br />
        <div align="right" style="margin-right: 1px; width: 750px;">
            <span class="NewRowLink" onclick="PostInsertRow()">เพิ่มแถว</span></div>
        <uc3:DsAdd ID="dsAdd" runat="server" />
        <asp:HiddenField ID="HdIndex" runat="server" />
        <asp:HiddenField ID="HdLnrcvfrom" runat="server" />
        <br />
    </div>
</asp:Content>
