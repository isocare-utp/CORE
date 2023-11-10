<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_loan_receive.aspx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_loan_receive_ctrl.w_dlg_loan_receive" %>

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
                var rowcount = dsDetail.GetRowCount();
                var bfshrcont_balamt = 0;
                var interest_period = 0;
                var bfintarr_amt = 0;
                var sum = 0;

                for (var i = 0; i < rowcount; i++) {
                    if (dsDetail.GetItem(i, "operate_flag") == 1) {
                        bfshrcont_balamt = dsDetail.GetItem(i, "bfshrcont_balamt");
                        interest_period = dsDetail.GetItem(i, "interest_period");
                        bfintarr_amt = dsDetail.GetItem(i, "bfintarr_amt");
                        sum += bfshrcont_balamt + interest_period + bfintarr_amt;
                    }
                }
                var payout_amt = dsMain.GetItem(0, "payout_amt");
                if (sum > payout_amt) {
                    alert("ยอดจ่ายเงินกู้น้อยกว่ายอดหักชำระ กรุณาตรวจสอบ");
                }

                dsDetail.SetRowFocus(r);
                PostOperateFlag();
            }

            else if (c == "principal_payamt" || c == "interest_payamt") {
                dsDetail.SetRowFocus(r);
                var prin = dsDetail.GetItem(r, "principal_payamt");
                var inter = dsDetail.GetItem(r, "interest_payamt");
                var bfshrcont_balamt = dsDetail.GetItem(r, "bfshrcont_balamt");
                var rkeep_principal = dsDetail.GetItem(r, "rkeep_principal");
                var bfpxaftermthkeep_type = dsDetail.GetItem(r, "bfpxaftermthkeep_type");

                //เช็คว่ายอดชำระเกินยอดคงเหลือ - ยอดเรียกเก็บหรือไม่
                if (prin > (bfshrcont_balamt - rkeep_principal)) {
                    prin = bfshrcont_balamt - rkeep_principal;
                    dsDetail.SetItem(r, "principal_payamt", prin);

                    if (rkeep_principal == 0) {
                        alert("ชำระต้นเงินเกินยอดคงเหลือ");
                    } else {
                    alert("ชำระต้นเงินเกินยอดที่สามารถชำระได้ มียอดรอเรียกเก็บอยู่ " + rkeep_principal.format(2, 3, ',', '.'));
                    }
                }

                var total = prin + inter;
                var item_balance = bfshrcont_balamt - prin;
                dsDetail.SetItem(r, "item_payamt", total);
                dsDetail.SetItem(r, "item_balance", item_balance);

                var sum_total = 0;
                for (var i = 0; i < dsDetail.GetRowCount(); i++) {
                    sum_total = sum_total + dsDetail.GetItem(i, "item_payamt");
                }
                var payout_amt = dsMain.GetItem(0, "payout_amt");
                dsMain.SetItem(0, "payoutclr_amt", sum_total);
                dsMain.SetItem(0, "payoutnet_amt", payout_amt - sum_total);
                Postof_calbankfee();

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
                var bfshrcont_balamt = dsDetail.GetItem(r, "bfshrcont_balamt");
                var prin = dsDetail.GetItem(r, "principal_payamt");
                var item_balance = bfshrcont_balamt - prin;
                dsDetail.SetItem(r, "item_balance", item_balance);

                var sum_total = 0;
                for (var i = 0; i < dsDetail.GetRowCount(); i++) {
                    sum_total = sum_total + dsDetail.GetItem(i, "item_payamt");
                }
                dsMain.SetItem(0, "payoutclr_amt", sum_total);
                var payout_amt = dsMain.GetItem(0, "payout_amt");
                var payoutclr_amt = dsMain.GetItem(0, "payoutclr_amt");
                var total_pay = payout_amt - payoutclr_amt;
                dsMain.SetItem(0, "payoutnet_amt", total_pay);
                Postof_calbankfee();
            }
        }

        Number.prototype.format = function (n, x, s, c) {
            var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\D' : '$') + ')',
        num = this.toFixed(Math.max(0, ~ ~n));

            return (c ? num.replace('.', c) : num).replace(new RegExp(re, 'g'), '$&' + (s || ','));
        };

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "expense_bank") {
                PostBank();
            } else if (c == "rcvperiod_flag") {
                PostRcvperiod();
            }
            else if (c == "payout_amt" || c == "payoutclr_amt") {

                dsMain.SetRowFocus(r);
                var payout_amt = dsMain.GetItem(r, "payout_amt")
                var payoutclr_amt = dsMain.GetItem(r, "payoutclr_amt");
                if (payout_amt < payoutclr_amt) {
                    alert("ยอดจ่ายเงินกู้น้อยกว่ายอดหักชำระ กรุณาตรวจสอบ");

                    var sum = payout_amt - payoutclr_amt;
                    dsMain.SetItem(r, "payoutnet_amt", sum);
                }
                var sum = payout_amt - payoutclr_amt;
                dsMain.SetItem(r, "payoutnet_amt", sum);
                Postof_Payout();
            }
            else if (c == "moneytype_code") {
                PostMoneytype();
            }
            else if (c == "expense_branch") {
                PostCalBankFee();
            }
            else if (c == "slip_date") {
                dsMain.SetItem(r, "slip_date", v);
                PostSlip_date();
            }
        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_sbranch") {
                if (dsMain.GetItem(0, "txt_branch_name") != "" && dsMain.GetItem(0, "txt_branch_name") != null) {
                    dsMain.SetItem(0, "expense_branch", "");
                    PostRetrieveBranch();
                }
            }
            //tomy ค้นหาเลขบัญชี 9/3/2017
            else if (c == "b_deptacc") {
                var member_no = dsMain.GetItem(0, "member_no");
                //alert(member_no);
                Gcoop.OpenDlg('480', '300', 'w_deptacc.aspx', "?member=" + member_no);

            }
        }
        function GetValueAccID(deptaccout_no) {
            dsMain.SetItem(0, "expense_accid", deptaccout_no);
        }

        function OnDsAddItemChanged(s, r, c, v) {
            if (c == "operate_flag") {

                dsAdd.SetRowFocus(r);
                PostOperateFlagAdd();
            }
            else if (c == "item_payamt") {
                var rowcount = dsAdd.GetRowCount();
                var item_payamt = 0;

                var sum = 0;

                for (var i = 0; i < rowcount; i++) {
                    if (dsAdd.GetItem(i, "operate_flag") == 1) {
                        item_payamt = dsAdd.GetItem(i, "item_payamt");

                        sum += item_payamt;
                    }
                }
                var payout_amt = dsMain.GetItem(0, "payout_amt");
                if (sum > payout_amt) {
                    alert("ยอดจ่ายเงินกู้น้อยกว่ายอดหักชำระ กรุณาตรวจสอบ");
                }
                dsAdd.SetRowFocus(r);
                PostItem();
            }
            else if (c == "slipitemtype_code") {
                dsAdd.SetRowFocus(r);
                PostSlipItem();
            }
        }

        function OnDsAddClicked(s, r, c) {
            if (c == "b_del") {
                dsAdd.SetRowFocus(r);
                PostDeleteRow();
            }
        }

        function DialogLoadComplete() {

        }

        function SavePay() {
            
            PostSave();
     //       if (isConfirm) {
                window.opener.GetShowData();
                window.close();
      //      }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <br />
    <div>
        <asp:Label ID="lbCurrentLoan" runat="server" Text="Label" Style="margin-left: 40px;"></asp:Label>
        <input type="button" value="บันทึก" id="btnSave" onclick="SavePay()" style="margin-left: 550px;" />
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
        <asp:HiddenField ID="HdPayavd" runat="server" Value="0" />
        <asp:HiddenField ID="Hdslpayout" runat="server" />
        <asp:HiddenField ID="Hdslpayin" runat="server" />
        <br />
    </div>
</asp:Content>