<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="wd_as_assistpay.aspx.cs" Inherits="Saving.Applications.assist.dlg.wd_as_assistpay_ctrl.wd_as_assistpay" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsPayto.ascx" TagName="DsPayto" TagPrefix="uc2" %>
<%@ Register Src="DsLoan.ascx" TagName="DsLoan" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsPayto = new DataSourceTool;
        var dsLoan = new DataSourceTool;
        var ChkRow;

        function chkNumber(ele) {
            var vchar = String.fromCharCode(event.keyCode);
            if ((vchar < '0' || vchar > '9') && (vchar != '.')) return false;
            ele.onKeyPress = vchar;
        }

        function Validate() {
        }

        function OnDsMainClicked(s, r, c) {
        }

        function OnDsLoanClicked(s, r, c, v) {
            if (c == "b_delloan") {
                dsLoan.SetRowFocus(r);
                if (confirm("ลบรายการหัก แถวที่ " + (r + 1) + " ?") == true) {
                    PostDelRowLoan();
                }
            }
        }

        function OnDsPaytoClicked(s, r, c, v) {
            if (c == "b_delpayto") {
                dsPayto.SetRowFocus(r);
                if (confirm("ลบรายการจ่าย แถวที่ " + (r + 1) + " ?") == true) {
                    PostDelRowPayto();
                }
            } else if (c == "b_searchbank") {
                var memno = dsMain.GetItem(0, "member_no");
                var exp_code = dsPayto.GetItem(r, "moneytype_code");
                var exp_bank = dsPayto.GetItem(r, "expense_bank");
                var exp_branch = dsPayto.GetItem(r, "expense_branch");
                var exp_accid = dsPayto.GetItem(r, "expense_accid");
                var chqpayname = dsPayto.GetItem(r, "chq_payname");
                var tofromaccid = dsPayto.GetItem(r, "tofrom_accid");
                var exp_clearing = dsPayto.GetItem(r, "expense_clearing");
                var deptacc_no = dsPayto.GetItem(r, "deptaccount_no");

                if (exp_code != "TRN") {
                    deptacc_no = "";
                }

                ChkRow = r;

                var arg = "?memno=" + memno + "&exp_code=" + exp_code + "&exp_bank=" + exp_bank + "&exp_branch=" + exp_branch + "&exp_accid=" + exp_accid + "&chq_payname=" + chqpayname + "&tofrom_accid=" + tofromaccid + "&exp_clr=" + exp_clearing + "&deptaccount_no=" + deptacc_no;
                Gcoop.OpenDlg2("500", "350", "wd_as_expense_detail.aspx", arg);
            }
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "payout_amt") {
                var payamt = dsMain.GetItem(0, "payout_amt");
                var apvamt = dsMain.GetItem(0, "bfapv_amt");
                if (payamt > apvamt) {
                    alert('ยอดอนุมัติมากกว่ายอดจ่าย!!');
                    dsMain.SetItem(0, "payout_amt", apvamt);
                }
            }
        }
        function OnDsPaytoItemChanged(s, r, c, v) {
            if (c == "itempay_amt") {
                if (v < 0) {
                    alert('ตรวจสอบยอดเงินรายการจ่าย ยอดจ่ายต้องมีค่าไม่น้อยกว่า 0');
                    return;
                }

                PostGetFee();
            }
            else if (c == "moneytype_code") {
                var rc = dsPayto.GetRowCount();
                var moneytype_code = dsPayto.GetItem(r, "moneytype_code");
                var count_type = 0;
                if (rc > 1 && (moneytype_code == "CSH" || moneytype_code == "CHQ")) {
                    for (var i = 0; i < rc; i++) {
                        if (r != i) {
                            var moneycode = dsPayto.GetItem(i, "moneytype_code");
                            if (moneytype_code == moneycode) {
                                count_type = count_type + 1; 
                            }
                        }
                    }
                    if (count_type == 0) {
                        dsPayto.SetItem(r, "description", "");
                        PostGetFee();
                    }
                    else {
                        alert('ประเภทเงินซ้ำ กรุณาตรวจสอบ !!!');
                        dsPayto.SetItem(r, "description", "");
                        dsPayto.SetItem(r, "moneytype_code", "");
                    }
                }
                else {
                    dsPayto.SetItem(r, "description", "");
                    PostGetFee();
                }
                
            }
        }


        function OnDsLoanItemChanged(s, r, c, v) {
            if (c == "itempay_amt") {

                if (v < 0) {
                    alert('ยอดเงินรายการหักชำระน้อยกว่า 0 กรุณาป้อนใหม่');
                    dsLoan.SetItem(r, "itempay_amt", 0);
                    return;
                }

                SumAmt();
            } else if (c == "methpaytype_code") {
                if (v == "LON") {
                    dsLoan.SetItem(r, "description", "หักชำระหนี้");
                } else if (v == "ETC") {
                    dsLoan.SetItem(r, "description", "หักอื่นๆ");
                }
            }
        }

        function SumAmt() {
            var sumclr = 0;
            var clramt = 0;
            var apvamt = 0;

            apvamt = dsMain.GetItem(0, "payout_amt");

            for (ii = 0; ii < dsLoan.GetRowCount(); ii++) {
                clramt = dsLoan.GetItem(ii, "itempay_amt");
                sumclr = sumclr + parseFloat(clramt.toFixed(2));
            }

            dsMain.SetItem(0, "payoutclr_amt", sumclr);
            dsMain.SetItem(0, "payoutnet_amt", apvamt - sumclr);

            if (apvamt - sumclr < 0) {
                alert("ยอดหักชำระมีค่ามากกว่ายอดอนุมัติ กรุณาตรวจสอบ");
            }
        }


        function OnClickNewRowPayto() {
            PostNewRowPayto();
        }
        function OnClickNewRowLoan() {
            PostNewRowLoan();
        }
        function GetValueFromDlg(bank_code, branch_code, expense_accid, pay_name, tofrom_accid, exp_clr, deptacc_no) {
            var show_text = "";

            dsPayto.SetItem(ChkRow, "expense_bank", bank_code);
            dsPayto.SetItem(ChkRow, "expense_branch", branch_code);
            dsPayto.SetItem(ChkRow, "expense_accid", expense_accid);
            dsPayto.SetItem(ChkRow, "tofrom_accid", tofrom_accid);
            dsPayto.SetItem(ChkRow, "chq_payname", pay_name);
            dsPayto.SetItem(ChkRow, "expense_clearing", exp_clr);
            dsPayto.SetItem(ChkRow, "deptaccount_no", deptacc_no);

            PostPayinDesc();
        }

        function SavePay() {
            var apvamt = 0, paynet = 0;
            var sumclr = 0, clramt = 0;
            var sumpay = 0, payamt = 0;
            var moneytype_code = "";

            for (ii = 0; ii < dsLoan.GetRowCount(); ii++) {
                clramt = dsLoan.GetItem(ii, "itempay_amt");
                if (clramt <= 0) {
                    alert('ตรวจสอบรายการหัก มีรายการหักที่มีค่าเป็น 0 หรือน้อยกว่า 0');
                    return;
                }
                sumclr = sumclr + parseFloat(clramt.toFixed(2));
            }

            for (ii = 0; ii < dsPayto.GetRowCount(); ii++) {
                payamt = dsPayto.GetItem(ii, "itempay_amt");
                moneytype_code = dsPayto.GetItem(ii, "moneytype_code");
                if (payamt < 0) {
                    alert('ตรวจสอบรายการจ่าย มีรายการจ่ายที่มีค่าเป็น 0 หรือน้อยกว่า 0');
                    return;
                }
                if (moneytype_code == "" || moneytype_code == null) {
                    alert('ตรวจสอบรายการจ่าย กรุณาเลือกประเภทเงิน');
                    return;
                }
                sumpay = sumpay + parseFloat(payamt.toFixed(2));
            }

            apvamt = dsMain.GetItem(0, "payout_amt");
            paynet = dsMain.GetItem(0, "payoutnet_amt")

            if (sumclr > apvamt) {
                alert('ยอดหักชำระมากกว่ายอดอนุมัติไม่ได้ กรุณาตรวจสอบ');
                return;
            }

            if (paynet != sumpay) {
                alert('ยอดเงินจ่ายโดย ไม่เท่ากับยอดจ่ายสุทธิ');
                return;
            }

            if (confirm("ยืนยันการจ่ายสวัสดิการ")) {
                PostSave();
            }
        }

        function GetShowData() {
            alert('บันทึกข้อมูลสำเร็จ');
            parent.GetRetrivedata();
            parent.RemoveIFrame();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <br />
    <div>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <asp:Label ID="lbCurrentAssist" runat="server" Text="Label" Style="margin-left: 40px;"></asp:Label>
        <input type="button" value="บันทึก" id="btnSave" onclick="SavePay()" style="margin-left: 550px;" />
        <input type="button" value="ยกเลิก" onclick="PostCancel()" style="margin-left: 2px;" />
    </div>
    <div align="center">
        <uc1:DsMain ID="dsMain" runat="server" />
        <br />
        <div id="insertPayto" class="NewRowLink" onclick="OnClickNewRowPayto()" style="display: block;
            margin-left: 680px; margin-bottom: -20px;">
            เพิ่มแถว</div>
        <uc2:DsPayto ID="dsPayto" runat="server" />
        <br />
        <br />
        <div id="insertLoan" class="NewRowLink" onclick="OnClickNewRowLoan()" style="display: block;
            margin-left: 680px; margin-bottom: -20px;">
            เพิ่มแถว</div>
        <uc3:DsLoan ID="dsLoan" runat="server" />
    </div>
    <asp:HiddenField ID="HdIndex" runat="server" />
    <asp:HiddenField ID="Hdbank" runat="server" />
    <asp:HiddenField ID="Hdbankbranch" runat="server" />
    <asp:HiddenField ID="Hdbankaccid" runat="server" />
    <asp:HiddenField ID="Hdtofrom" runat="server" />
    <asp:HiddenField ID="HdPaytoRow" runat="server" />
</asp:Content>
