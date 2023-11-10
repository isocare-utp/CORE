<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_sl_loan_requestment.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_loan_requestment"
    ValidateRequest="false" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsPostmember%>
    <%=jsPostSetPeriodSend%>
    <%=jsPostSumClear %>
    <%=jsPostSetPeriodPay%>
    <%=jsPostChangePeriodPay %>
    <%=jsPostRePermiss%>
    <%=jsPostRightColl%>
    <%=initJavaScript %>
    <%=jsExpenseBank%>
    <%=jsExpenseCode%>
    <%=jsGetMemberCollno%>
    <%=jsReNewPage%>
    <%=jsOpenOldDocNo%>
    <%=jsPostSetZero%>
    <%=jsCancelRequest %>
    <%=jsRefresh%>
    <%=jsSetDataList%>
    <%=jsSetloantypechg%>
    <%=jsPostDelOtherclr%>
    <%=jsExpensebankbrRetrieve %>
    <%=jsPostGetCollPermiss%>
    <%=jsGetexpensememno%>
    <%=jsGetitemdescetc%>
    <%=jsPostRequestamt%>
    <%=jsPostDelMthexp%>
    <%=jsGetitemdescmthexp%>
    <%=jsPostPrtRpt%>
    <%=jsPostChgrqDate%>
    <%=jsChgReal%>
    <%=jsPostMthpay%>
    <script type="text/javascript">

        function MenubarNew() {
            jsReNewPage();
        }

        function MenubarOpen() {
            Gcoop.OpenIFrame('800', '600', 'w_dlg_sl_loanrequest_search.aspx', '');
        }

        //w_dlg_sl_loanrequest_search
        function GetValueLoanRequest(docNo) {
            objdw_main.SetItem(1, "loanrequest_docno", docNo);
            objdw_main.AcceptText();
            jsOpenOldDocNo();
        }

      

        function Validate() {
            objdw_main.AcceptText(); //main
            objdw_coll.AcceptText(); //หลักประกัน
            objdw_clear.AcceptText(); //หักกลบ     
            objdw_otherclr.AcceptText(); //หักอื่น

            var period_payamt = objdw_main.GetItem(1, "period_payamt");
            var period_payment = objdw_main.GetItem(1, "period_payment");
            var loanrequest_amt = objdw_main.GetItem(1, "loanrequest_amt");
            var chk = "";
            var sum_use_amt = 0;
            var colldep = "";
            var use_amt = 0;
            for (var i = 1; i <= objdw_coll.RowCount(); i++) {
                use_amt = parseFloat(objdw_coll.GetItem(i, "collactive_amt"));
                sum_use_amt = parseFloat(sum_use_amt) + parseFloat(use_amt.toFixed(2));
                var loancolltype_code = objdw_coll.GetItem(i, "loancolltype_code");
                if (loancolltype_code == "03") {
                    colldep += objdw_coll.GetItem(i, "ref_collno") + "  " + objdw_coll.GetItem(i, "description") + "\n";
                }
            }
            if (colldep != "") {
                colldep = "ระบบจะทำการอายัดบัญชีเงินฝากให้อัตโนมัติ\n" + colldep + "\n";
            }

            for (var i = 1; i <= objdw_coll.RowCount(); i++) {
                chk = objdw_coll.GetItem(i, "ref_collno");
                if (chk == null || chk == "") {
                    return false;
                }
            }

            return confirm(colldep + "ยอดขอกู้ = " + loanrequest_amt + " |  ต้นเงิน = " + period_payment + " |  จน.งวด = " + period_payamt + "   ยืนยันการบันทึกข้อมูล");
        }

        //**************** Start. 1.dw_main  ****************

        //Event----->ClientEventItemChanged="ItemDwMainChanged"
        function ItemDwMainChanged(sender, rowNumber, columnName, newValue) {
            if ((columnName == "loantype_code") || (columnName == "loantype_code_1")) {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                var member_no = objdw_main.GetItem(1, "member_no");
                if (member_no != null) {
                    jsPostmember();
                }
            } else if (columnName == "member_no") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("HdMemberNo").value = newValue;
                var loantype_code = objdw_main.GetItem(rowNumber, "loantype_code");
                var loanright_type = Gcoop.GetEl("Hdloanright_type").value;
                if (loanright_type == 1) {
                    collmastclick();
                }
                else if (loanright_type == 7) {
                    //เฉพาะเงินฝากหลัก
                    Gcoop.OpenDlg(720, 500, "w_dlg_sl_loanrequest_loanrightchoose_share.aspx", "?member_no=" + newValue + "&loantype_code=" + loantype_code);
                }
                else {
                    jsPostmember();
                }
            } else if (columnName == "period_payamt") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();

                var period_payamt = objdw_main.GetItem(rowNumber, "period_payamt");
                var maxsend_payamt = objdw_main.GetItem(rowNumber, "maxsend_payamt");

                if (period_payamt > maxsend_payamt) {
                    alert('จำนวนงวดเกินงวดที่สามารถส่งได้ ' + maxsend_payamt);
                    objdw_main.SetItem(rowNumber, columnName, maxsend_payamt);
                    objdw_main.AcceptText();
                    jsPostSetPeriodPay();
                } else {
                    jsPostSetPeriodPay();
                }

            } else if (columnName == "memcoop_id" || columnName == "memcoop_id_1") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("HdMemcoopId").value = newValue;

            } else if (columnName == "expense_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                jsExpenseCode();

            } else if (columnName == "expense_bank") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                jsExpenseBank();

            } else if (columnName == "expense_bank_1") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                jsExpenseBank();
                if (expense_bank != "") {
                    jsExpensebankbrRetrieve();
                }

            } else if (columnName == "loanrequest_amt") {
                var loanrequest_amt = newValue;
                var loancredit_amt = objdw_main.GetItem(1, "loancredit_amt");
                // ตรวจสอบถ้ายอดขอกู้มากกว่า ยอดตามสิทธิ์ให้กู้
                if (loanrequest_amt > loancredit_amt) {
                    //hardcode
                    // ช่วงคู่ขนานยังไม่lock
                    // alert("ยอดเงินขอกู้มากกว่ายอดเงินสิทธิ์ให้กู้ กรุณากรอกข้อมูลใหม่ สิทธิกู้สูงสุด #" + loancredit_amt.toString() + " คีย์ขอกู้ " + loanrequest_amt.toString());
                    if (confirm("ยอดเงินขอกู้มากกว่ายอดเงินสิทธิ์ให้กู้ กรุณากรอกข้อมูลใหม่ สิทธิกู้สูงสุด #" + loancredit_amt.toString() + " คีย์ขอกู้ " + loanrequest_amt.toString() + "   ยืนยันการขอกู้") == false) {
                        sender.SetItem(rowNumber, columnName, 0);
                        sender.AcceptText();
                        jsPostSetZero();
                    } else {
                        sender.SetItem(rowNumber, columnName, newValue);
                        sender.AcceptText();
                        jsPostRequestamt();
                    }
                }
                else {
                    objdw_main.SetItem(rowNumber, columnName, newValue);
                    objdw_main.AcceptText();
                    jsPostRequestamt();
                }
            } else if (columnName == "loanreqregis_amt") {
                var loanreqregis_amt = newValue;
                var loanrequest_amt = objdw_main.GetItem(1, "loanrequest_amt");
                if (loanreqregis_amt < loanrequest_amt) {
                    objdw_main.SetItem(1, "loanreqregis_amt", loanreqregis_amt);
                    objdw_main.SetItem(1, "loanrequest_amt", loanreqregis_amt);
                    objdw_main.AcceptText();
                    jsPostRequestamt();
                }
            } else if (columnName == "loanpayment_type") {
                objdw_main.SetItem(1, columnName, newValue);
                Gcoop.GetEl("Hdcheckrepermiss").value = 1;
                objdw_main.AcceptText();

                jsPostRePermiss();
            } else if (columnName == "paymonth_other") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                var memno = objdw_main.GetItem(rowNumber, "member_no");
                if (memno != null) {

                    jsPostRePermiss();
                }
            } else if (columnName == "incomemonth_other") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                var memno = objdw_main.GetItem(rowNumber, "member_no");
                if (memno != null) {

                    jsPostRePermiss();
                }
            } else if (columnName == "incomemonth_fixed") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                var memno = objdw_main.GetItem(rowNumber, "member_no");
                var memcoop_id = objdw_main.GetItem(rowNumber, "memcoop_id");
                var loantype_code = objdw_main.GetItem(rowNumber, "loantype_code");
                if (memno != null) {

                    //jsrecalloanpermiss();
                }
            }
            else if (columnName == "period_payment") {
                Gcoop.GetEl("Hdperiodpay").value = objdw_main.GetItem(rowNumber, "period_payment");
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                jsPostChangePeriodPay();
            }
            else if (columnName == "lnoverretry_status") {
                sender.SetItem(rowNumber, columnName, newValue);
                sender.AcceptText();
                jsPostSetPeriodSend();
            } else if (columnName == "loanrequest_tdate") {
                jsPostChgrqDate();
            }
        }

        //Event----->ClientEventButtonClicked="OnDwMainClicked"
        function OnDwMainClicked(sender, rowNumber, objectName) {
            Gcoop.GetEl("HdReturn").value = rowNumber + "";
            if ((objectName == "loanrcvfix_flag") || (objectName == "clearloan_flag") || (objectName == "otherclr_flag") || (objectName == "custompayment_flag")) {
                if (objectName == "loanrcvfix_flag") {
                    Gcoop.CheckDw(sender, rowNumber, objectName, "loanrcvfix_flag", 1, 0); // Edit By Bank เรื่อง การ set วันที่ระบุวันจ่ายเงินกู้
                    jsSetFixdate();
                }
                if (objectName == "custompayment_flag") {
                    Gcoop.CheckDw(sender, rowNumber, objectName, "custompayment_flag", 1, 0);
                    jsRefresh();
                }

            } else if (objectName == "b_setkeep2") {

                var memberNoVal = objdw_main.GetItem(1, "member_no");
                var expense_code = objdw_main.GetItem(1, "loanpay_code");
                var expense_bank = objdw_main.GetItem(1, "loanpay_bank");
                var expense_branch = objdw_main.GetItem(1, "loanpay_branch");
                var expense_accid = objdw_main.GetItem(1, "loanpay_accid");

                Gcoop.OpenIFrame(520, 250, "w_dlg_sl_lnreqloan_loanpay.aspx", "?member_no=" + memberNoVal + "&expense_code=" + expense_code + "&expense_bank=" + expense_bank + "&expense_branch=" + expense_branch + "&expense_accid=" + expense_accid + "&buttonc=kep");

            } else if (objectName == "b_cancel") {
                var loanreqDocNo = objdw_main.GetItem(1, "loanrequest_docno");
                if ((loanreqDocNo != "") && (loanreqDocNo != null) && (loanreqDocNo != "Auto")) {
                    jsCancelRequest();
                }
                else {
                    alert("ไม่สามารถยกเลิกรายการได้ กรุณาตรวจสอบข้อมูลใหม่อีกครั้ง");
                }
            } else if (objectName == "b_inttab") {
                Gcoop.OpenDlg('600', '400', 'w_dlg_sl_loanrequest_intratespc.aspx', '');
            } else if ((objectName == "b_expense_branch") || (objectName == "expense_branch_1" && (objdw_main.GetItem(1, "retrive_bk_branchflag") == 0))) {
                var expense_bank = objdw_main.GetItem(1, "expense_bank");

                if (expense_bank != "") {
                    jsExpensebankbrRetrieve();
                }

            } else if (objectName == "b_search") {
                Gcoop.GetEl("HdColumnName").value = objectName;
                var coop_id = objdw_main.GetItem(rowNumber, "memcoop_id")
//                Gcoop.OpenDlg('600', '600', 'w_dlg_sl_loanmember_search.aspx', "?coopId=" + coop_id);
                Gcoop.OpenIFrame2('600', '600', 'w_dlg_sl_loanmember_search.aspx', "?coopId=" + coop_id);

            } else if (objectName == "b_mthpay") {
                var memberNo = objdw_main.GetItem(1, "member_no");
                if ((memberNo != "") && (memberNo != null) && (memberNo != "00000000")) {
                    var member_no = objdw_main.GetItem(1, "member_no");
                    var salary_amt = objdw_main.GetItem(1, "salary_amt");
                    var income = objdw_main.GetItem(1, "incomemonth_other") + objdw_main.GetItem(1, "incomemonth_fixed");
                    var paymonth = objdw_main.GetItem(1, "paymonth_other") + objdw_main.GetItem(1, "paymonth_coop") + objdw_main.GetItem(1, "paymonth_lnreq") + objdw_main.GetItem(1, "paymonth_exp");
                    var paymonthoth = objdw_main.GetItem(1, "paymonth_other");
                    //หุ้น
                    var share_periodvalue = objdw_main.GetItem(1, "share_periodvalue");

                    //ใบคำขอ
                    var loantype_code = objdw_main.GetItem(1, "loantype_code");
                    var loanpayment_type = objdw_main.GetItem(1, "loanpayment_type");
                    var intestimate_amt = objdw_main.GetItem(1, "intestimate_amt");
                    var period_payment = objdw_main.GetItem(1, "period_payment");
                    var rkeep_interest = 0;
                    var rkeep_principal = 0;

                    //รายการหักกลบ
                    var loanclr = "";

                    //หักจากเงินเดือน
                    var mthexp = "";

                    if (mthexp != "") {
                        mthexp = mthexp.substring(1);
                    }

                    jsPostMthpay();
                }
                else {
                    alert("ไม่สามารถแสดงรายการได้ กรุณากรอกเลขทะเบียนก่อน");
                }
            } else if ((objectName == "b_remark")) {

                objdw_main.AcceptText();
                var reamrk = objdw_main.GetItem(1, "remark")

                Gcoop.OpenDlg(620, 450, "w_dlg_loanrequest_remark.aspx", "?reamrk=" + reamrk);
            } else if ((objectName == "b_showetcstatus")) {

                objdw_main.AcceptText();
                var memno = objdw_main.GetItem(1, "member_no")

                Gcoop.OpenDlg(620, 450, "w_dlg_sl_lnreqloan_show_etcstatus.aspx", "?member_no=" + memno);
            } else if (objectName == "b_permiss") {
                objdw_main.AcceptText();
                var memno = objdw_main.GetItem(1, "member_no")
                var ref_contmastno = objdw_main.GetItem(1, "ref_contmastno")
                var loangrpcredit_amt = objdw_main.GetItem(1, "loangrpcredit_amt")
                var loangrpuse_amt = objdw_main.GetItem(1, "loangrpuse_amt")
                var loancredit_amt = objdw_main.GetItem(1, "loancredit_amt")
                var loanmaxreq_amt = objdw_main.GetItem(1, "loanmaxreq_amt")
                var loanpermiss_amt = objdw_main.GetItem(1, "loanpermiss_amt")
                var maxperiod_payamt = objdw_main.GetItem(1, "maxsend_payamt")
                var maxperiod_payment = objdw_main.GetItem(1, "maxperiod_payamt")
                var loantype_code = objdw_main.GetItem(1, "loantype_code")
                Gcoop.OpenDlg(620, 450, "w_dlg_sl_loanrequest_permiss.aspx", "?member_no=" + memno + "&ref_contmastno=" + ref_contmastno + "&loangrpcredit_amt=" + loangrpcredit_amt + "&loangrpuse_amt=" + loangrpuse_amt + "&loancredit_amt=" + loancredit_amt + "&loanmaxreq_amt=" + loanmaxreq_amt + "&loanpermiss_amt=" + loanpermiss_amt + "&maxperiod_payamt=" + maxperiod_payamt + "&maxperiod_payment=" + maxperiod_payment + "&loantype_code=" + loantype_code);
                return
            } else if (objectName == 'b_chgaccid') {
                var memberNoVal = objdw_main.GetItem(1, "member_no");
                var expense_code = objdw_main.GetItem(1, "expense_code");
                if ((memberNoVal != null) && (memberNoVal != "")) {
                    if (expense_code == "TRN") {
                        Gcoop.OpenDlg(620, 250, "w_dlg_show_accid.aspx", "?member=" + memberNoVal);
                    } else if (expense_code == "CBT") {

                        var memberNoVal = objdw_main.GetItem(1, "member_no");
                        var expense_code = objdw_main.GetItem(1, "loanpay_code");
                        var expense_bank = objdw_main.GetItem(1, "loanpay_bank");
                        var expense_branch = objdw_main.GetItem(1, "loanpay_branch");
                        var expense_accid = objdw_main.GetItem(1, "loanpay_accid");
                        Gcoop.OpenDlg(520, 250, "w_dlg_sl_lnreqloan_expense.aspx", "?member_no=" + memberNoVal + "&expense_code=" + expense_code + "&expense_bank=" + expense_bank + "&expense_branch=" + expense_branch + "&expense_accid=" + expense_accid + "&buttonc=expense");

                    }
                }
            } else if (objectName == "buyshare_flag") {
                Gcoop.CheckDw(sender, rowNumber, objectName, "buyshare_flag", 1, 0);
                sender.SetItem(1, "buyshare_amt", 0);
                jsRefresh();
            }
            else if (objectName == "b_retrive") {

                jsGetexpensememno();
            }
            else if (objectName == "b_branch") {
                var bank_code = objdw_main.GetItem(1, "expense_bank");
                Gcoop.OpenIFrame('580', '590', 'w_dlg_kp_bankbranch_search.aspx', "?bank_code=" + bank_code);
            }
            else if (objectName == "insure_flag") {
                Gcoop.CheckDw(sender, rowNumber, objectName, "insure_flag", 1, 0);
                jsRefresh();
            }
            else if (objectName == "b_update") {
                jsChgReal();
            }
            else if (objectName == "b_dept") {
                var memno = objdw_main.GetItem(1, "member_no");
                Gcoop.OpenDlg('580', '590', 'w_dlg_dep.aspx', "?member_no=" + memno);  
            }
        }

        function TypeDEP(deptaccount_no, depttype_desc) {
            objdw_main.SetItem(1, "expense_accid", deptaccount_no);
            objdw_main.AcceptText();
        }

        function GetBankBranchFromDlg(branch_id, branch_name) {
            objdw_main.SetItem(1, "expense_branch", branch_id);
            objdw_main.SetItem(1, "expense_branch_1", branch_name);
            objdw_main.AcceptText();
        }

        function GetValueReamrk(remark) {
            objdw_main.SetItem(1, "remark", remark);
            objdw_main.AcceptText();
            jsRefresh();
        }
        function GetValueAccID(dept_no, deptaccount_name, prncbal) {
            var colunmName = Gcoop.GetEl("HdColumnName").value;
            var rowNumber = Gcoop.GetEl("HdRowNumber").value;
            if (colunmName == "b_chgaccid") {
                objdw_main.SetItem(1, "expense_accid", dept_no);
                objdw_main.AcceptText();
            } else if (colunmName == "b_deptother") {
                objdw_otherclr.SetItem(rowNumber, "deptaccount_no", dept_no);
                objdw_otherclr.AcceptText();
            } else {
                objdw_main.SetItem(1, "expense_accid", dept_no);
                objdw_main.AcceptText();
            }
        }
        function GetLoanpaymemno(expense_code, expense_bank, expense_branch, expense_accid, expense_bank_name, expense_branch_name, expense_desc) {

            objdw_main.SetItem(1, "loanpay_code", expense_code);
            objdw_main.SetItem(1, "loanpay_bank", expense_bank);
            objdw_main.SetItem(1, "loanpay_branch", expense_branch);
            objdw_main.SetItem(1, "loanpay_accid", expense_accid);
            objdw_main.SetItem(1, "paytoorder_desc", expense_desc);
            objdw_main.AcceptText();
            jsRefresh();
        }
        function GetLoanreceivememno(expense_code, expense_bank, expense_branch, expense_accid, expense_bank_name, expense_branch_name) {

            objdw_main.SetItem(1, "expense_code", expense_code);
            objdw_main.SetItem(1, "expense_bank", expense_bank);
            objdw_main.SetItem(1, "expense_branch", expense_branch);
            objdw_main.SetItem(1, "expense_accid", expense_accid);
            objdw_main.SetItem(1, "bank_name", expense_bank_name);
            objdw_main.SetItem(1, "branch_name", expense_branch_name);
            objdw_main.AcceptText();
            jsRefresh();
        }
        //**************** End. 1.dw_main  ****************

        //Event----->ClientEventItemChanged="ItemDwMthexpChanged"
        function ItemDwMthexpChanged(s, r, c, v) {
            if (c == "mthexp_code") {
                Gcoop.GetEl("Hdmthexprow").value = r;
                objdw_mthexp.SetItem(r, c, v);
                objdw_mthexp.AcceptText();
                jsGetitemdescmthexp();
            }
        }

        //Event----->ClientEventButtonClicked="OnDwMthexpClicked"
        function OnDwMthexpClicked(s, r, c) {
            if (c == "clear_status") {

            }
            else if (c == "b_del") {
                Gcoop.GetEl("HdRowNumber").value = r;
                jsPostDelMthexp();
            }
            else if (c == "b_addrow") {
                objdw_mthexp.InsertRow(objdw_mthexp.RowCount() + 1);
            }
        }

        function OnClickCalMthexp() {
            var sum_mthexp = 0;
            var mthexp_amt = 0;
            var paymonth_exp = 0;
            var clear_status = 0;
            for (var i = 1; i <= objdw_mthexp.RowCount(); i++) {
                clear_status = objdw_mthexp.GetItem(i, "clear_status");
                if (clear_status == 0) {
                    mthexp_amt = parseFloat(objdw_mthexp.GetItem(i, "mthexp_amt"));
                    sum_mthexp = parseFloat(sum_mthexp) + parseFloat(mthexp_amt.toFixed(2));
                }
            }
            //paymonth_exp = Gcoop.GetEl("Hdpaymouthexp").value;
            //paymonth_exp = parseFloat(paymonth_exp) + parseFloat(sum_mthexp);
            objdw_main.SetItem(1, "paymonth_exp", sum_mthexp);
            objdw_main.AcceptText();
            jsPostRePermiss();
        }

        //**************** Start. 2.dw_coll  ค้ำประกัน ****************

        //Event----->ClientEventItemChanged="ItemDwCollChanged"
        function ItemDwCollChanged(sender, rowNumber, columnName, newValue) {
            Gcoop.GetEl("HdRowNumber").value = rowNumber;

            if (columnName == "ref_collno") {

                objdw_coll.SetItem(rowNumber, columnName, newValue);
                Gcoop.GetEl("HdRefcoll").value = newValue;
                Gcoop.GetEl("HdRefcollrow").value = rowNumber;

                objdw_coll.AcceptText();
                var loancolltype_code = objdw_coll.GetItem(rowNumber, "loancolltype_code");
                if (Gcoop.GetEl("HdRefcoll").value == Gcoop.GetEl("HdMemberNo").value && loancolltype_code == "01") {
                    //Gcoop.GetEl("HdRefcoll").value = "";
                    alert("เลขทะเบียนผู้กู้และผู้ค้ำประกันเป็นเลขเดียวกัน");
                }
                else {
                    if (loancolltype_code == "01") {
                        //                        alert(0);
                        jsGetMemberCollno();
                    }
                    else if (loancolltype_code == "03") {
                        // alert(newValue);
                        //                        alert(1);
                        if ((newValue != null) && (newValue != "")) {
                            Gcoop.OpenDlg(620, 250, "w_dlg_show_accid_dept.aspx", "?dept=" + newValue);
                        }
                    }
                }
            } else if (columnName == "loancolltype_code") {
                objdw_coll.SetItem(rowNumber, columnName, newValue);
                objdw_coll.AcceptText();
                var loancolltype_code = objdw_coll.GetItem(rowNumber, "loancolltype_code");
                Gcoop.GetEl("HdRefcollrow").value = rowNumber + "";
                objdw_coll.SetItem(rowNumber, "ref_collno", "");
                objdw_coll.SetItem(rowNumber, "description", "");
                objdw_coll.SetItem(rowNumber, "collbalance_amt", 0);
                objdw_coll.SetItem(rowNumber, "collactive_amt", 0);
                //CollCondition();
                if (loancolltype_code == "02") {
                    jsGetMemberCollno();

                }
            }
            else if (columnName == "collactive_amt") {
                var collbalance_amt = Number(objdw_coll.GetItem(rowNumber, "collbalance_amt"));
                if (newValue <= collbalance_amt) {
                    var loanrequest_amt = Number(objdw_main.GetItem(1, "loanrequest_amt"));
                    newValue = CheckpermissPerson(rowNumber, newValue);
                    objdw_coll.SetItem(rowNumber, columnName, newValue);
                    var percent = calpercent(newValue, loanrequest_amt);
                    objdw_coll.SetItem(rowNumber, "collactive_percent", percent);

                    objdw_coll.AcceptText();
                    var str = "objdw_coll.SetItem(" + rowNumber + ", '" + columnName + "', " + newValue + ")";
                }
                SumColl_Change();
            }
            else if (columnName == "collactive_percent") {
                if (newValue <= 100) {
                    objdw_coll.SetItem(rowNumber, columnName, newValue);
                    var loanrequest_amt = Number(objdw_main.GetItem(1, "loanrequest_amt"));
                    var collactive_amt = Numberfixed((newValue * loanrequest_amt) / 100, 2);
                    objdw_coll.SetItem(rowNumber, "collactive_amt", collactive_amt);
                    objdw_coll.AcceptText();
                }
            }
            else if (columnName == "calcollright_amt") {
                objdw_coll.SetItem(rowNumber, columnName, newValue);
                objdw_coll.AcceptText();
            }
        }

        function CheckpermissPerson(rowNumber, newValue) {
            var collbalance_amt = objdw_coll.GetItem(rowNumber, "collbalance_amt");
            var collmax_amt = objdw_coll.GetItem(rowNumber, "collmax_amt");
            //if (newValue > collmax_amt && newValue < collbalance_amt) {
            //    if (confirm("ค้ำประกันเกินสิทธิการค้ำที่โดนจำกัดไว้(" + collmax_amt + "บาท)ต้องการทำรายการต่อหรือไม่")) {
            //        return newValue;
            //    } else {
            //        return collmax_amt;
            //    }
            //} else if (newValue > collmax_amt && newValue > collbalance_amt) {
            //    if (confirm("ค้ำประกันเกินสิทธิค้ำคงเหลือต้องการทำรายการต่อหรือไม่")) {
            //        return newValue;
            //    } else {
            //        return collmax_amt;
            //    }
            //}
            return newValue;
        }

        //Event----->ClientEventButtonClicked="OnDwCollClicked"
        function OnDwCollClicked(sender, rowNumber, buttonName) {
            var collTypeCode = objdw_coll.GetItem(rowNumber, "loancolltype_code");
            Gcoop.GetEl("HdReturn").value = rowNumber + "";
            Gcoop.GetEl("HdRowNumber").value = rowNumber + "";
            var memberNoVal = objdw_main.GetItem(1, "member_no");
            if (buttonName == "b_delrow") {
                objdw_coll.DeleteRow(rowNumber);
            }
            else if (buttonName == "b_detail") {
                var collNo = objdw_coll.GetItem(rowNumber, "ref_collno");
                var loantype_code = objdw_main.GetItem(1, "loantype_code");
                //  var requestDate = objdw_main.GetItem(1, "loanrequest_tdate");
                if ((collNo != "") && (collNo != null)) {

                    var loanmemno = objdw_main.GetItem(rowNumber, "member_no");
                    var refCollNo = objdw_coll.GetItem(rowNumber, "ref_collno");
                    var coop_id = objdw_coll.GetItem(rowNumber, "memcoop_id");
                    var collType = objdw_coll.GetItem(rowNumber, "loancolltype_code");
                    var coll_amt = objdw_coll.GetItem(rowNumber, "collbase_amt");
                    var coll_use = objdw_coll.GetItem(rowNumber, "collused_amt");
                    var coll_blance = objdw_coll.GetItem(rowNumber, "collbalance_amt");
                    var base_percent = objdw_coll.GetItem(rowNumber, "collbase_percent");
                    var description = objdw_coll.GetItem(rowNumber, "description");
                    var loancolltype_code = objdw_coll.GetItem(rowNumber, "loancolltype_code");
                    //alert("collNo: " + collNo + "\n loantype_code: " + loantype_code + "\n loanmemno: " + loanmemno + "\n refCollNo: " + refCollNo + "\n coop_id: " + coop_id + "\n collType: " + collType + "\n coll_amt: " + coll_amt + "\n coll_use: " + coll_use + "\n coll_blance: " + coll_blance + "\n base_percent: " + base_percent + "\n description: " + description + "\n loancolltype_code: " + loancolltype_code);
                    Gcoop.OpenDlg('700', '450', 'w_dlg_sl_loanrequest_coll.aspx', "?refCollNo=" + refCollNo + "&coop_id=" + coop_id + "&coll_amt=" + coll_amt + "&coll_use=" + coll_use + "&coll_blance=" + coll_blance + "&collType=" + collType + "&description=" + description + "&loancolltype_code=" + loancolltype_code + "&base_percent= " + base_percent + "&row=" + rowNumber + "&loanmemno=" + loanmemno + "&loantype_code=" + loantype_code);
                    return;
                }
                else {
                    alert("ไม่สามารถแสดงรายการได้ กรุณากรอกเลขที่อ้างอิงก่อน");
                }
            } else if ((buttonName == "b_searchc") && (collTypeCode == '01')) {

                Gcoop.GetEl("HdColumnName").value = buttonName;
                var coop_id = objdw_coll.GetItem(rowNumber, "coop_id");
                if (coop_id == null || coop_id == "") {
                    // ค้นหาคนค้ำประกัน
                    var memcoop_id = objdw_main.GetItem(1, "memcoop_id");
//                    Gcoop.OpenDlg('600', '600', 'w_dlg_sl_loanmember_search.aspx', "?coopId=" + memcoop_id);
                    Gcoop.OpenIFrame2('600', '600', 'w_dlg_sl_loanmember_search.aspx', "?coopId=" + memcoop_id);
                }
                else {
//                    Gcoop.OpenDlg('600', '600', 'w_dlg_sl_loanmember_search.aspx', "?coopId=" + coop_id);
                    Gcoop.OpenIFrame2('600', '600', 'w_dlg_sl_loanmember_search.aspx', "?coopId=" + coop_id);
                }
            }
            else if ((buttonName == "b_searchc") && (collTypeCode == '03')) {
                // ค้นหาบัญชีเงินฝาก 
                Gcoop.OpenDlg('950', '530', "w_dlg_dp_account_search.aspx", "?member=" + memberNoVal);
            }
            else if ((buttonName == "b_searchc") && (collTypeCode == '04')) {
                //ค้นหาหลักทรัพย์ค้ำประกัน
                var refCollNo2 = Gcoop.GetEl("HdMemberNo").value;
                var loantype_code = objdw_main.GetItem(1, "loantype_code");
                Gcoop.OpenIFrame2('750', '450', 'ws_dlg_sl_collsearch_req.aspx', "?member=" + memberNoVal);
            } else if ((buttonName == "b_searchc2") && (collTypeCode == '04')) {
                //ค้นหาหลักทรัพย์ค้ำประกัน
                var refCollNo2 = Gcoop.GetEl("HdMemberNo").value;
                var loantype_code = objdw_main.GetItem(1, "loantype_code");
                Gcoop.OpenDlg('640', '450', 'w_dlg_sl_collmaster_search_mb_.aspx', "?member=" + memberNoVal + "&loantype_code=" + loantype_code);
            }
            else if (buttonName == "b_addrow") {
                objdw_coll.InsertRow(objdw_coll.RowCount() + 1);
            }
            else if (buttonName == "b_recoll") {
                var percent = 0, sumcollactive_amt = 0, sumcollactive_percent = 0;
                var loanrequest_amt = objdw_main.GetItem(1, "loanrequest_amt");
                for (var i = 1; i <= objdw_coll.RowCount(); i++) {
                    sumcollactive_amt += parseFloat(objdw_coll.GetItem(i, "collactive_amt"));
                    sumcollactive_percent += parseFloat(objdw_coll.GetItem(i, "collactive_percent"));
                }
                if (loanrequest_amt == sumcollactive_amt) {
                    if (sumcollactive_percent > 100) {
                        percent = objdw_coll.GetItem(1, "collactive_percent") - (sumcollactive_percent - 100);
                        objdw_coll.SetItem(1, "collactive_percent", percent);
                    } else if (sumcollactive_percent < 100) {
                        percent = objdw_coll.GetItem(1, "collactive_percent") + (100 - sumcollactive_percent);
                        objdw_coll.SetItem(1, "collactive_percent", percent);
                    }
                    SumColl_Change();
                } else {
                    alert("ใบคำขอกู้เงิน ยอดค้ำประกันไม่เท่ากับยอดขอกู้ กรุณาตรวจสอบด้วย");
                }
            }
            else if (buttonName == "b_condition") {
                CollCondition();
            } else if (buttonName == "b_renew") {

            }
        }

        function recoll() {
            var sum = 0;
            var arr = new Array();
            for (var i = 1; i <= objdw_coll.RowCount(); i++) {
                if (objdw_coll.GetItem(i, "collactive_amt") != null) {
                    sum += Number(objdw_coll.GetItem(i, "collactive_amt"));
                    arr.push(i);
                }
            }
            var sum_percent = 0;
            for (var j = 0; j < (arr.length) - 1; j++) {
                var collactive_amt = Number(objdw_coll.GetItem(arr[j], "collactive_amt"));
                var percent = Numberfixed(collactive_amt / sum, 2);
                sum_percent += percent;
                objdw_coll.SetItem(arr[j], "collactive_percent", percent);
            }
            objdw_coll.SetItem(arr[j], "collactive_percent", 100 - Numberfixed(sum_percent, 2));
            objdw_coll.AcceptText();
        }

        function CollCondition() {
            var loanrequest_amt = objdw_main.GetItem(1, "loanrequest_amt");
            re_pole(loanrequest_amt);
        }

        function SumColl_Change() {
            var sumcollactive_amt = 0, sumcollactive_percent = 0;
            for (var i = 1; i <= objdw_coll.RowCount(); i++) {
                sumcollactive_amt += parseFloat(objdw_coll.GetItem(i, "collactive_amt"));
                sumcollactive_percent += parseFloat(objdw_coll.GetItem(i, "collactive_percent"));
            }

            // compute filed ไม่สามารถ setค่าได้จาก C# javascript ต้อง setด้วย jquery
            $("#objdw_coll_footer span:eq(1)").text(addCommas(sumcollactive_amt, 2));
            $("#objdw_coll_footer span:eq(2)").text(addCommas(sumcollactive_percent, 2));
            objdw_coll.AcceptText();
        }

        function re_pole(loanrequest_amt) {
            var arr_seq = new Array();
            var tmp_loanrequest_amt = loanrequest_amt;
            var arr_priority = ["04", "03", "02", "01"];
            var arr_idx = new Array();
            for (var j = 0; j < arr_priority.length; j++) {
                var arr_sort_ingroup = new Array();
                var chk_loancolltype_code = arr_priority[j];
                for (var i = 1; i <= objdw_coll.RowCount(); i++) {
                    var loancolltype_code = objdw_coll.GetItem(i, "loancolltype_code");
                    if (loancolltype_code == chk_loancolltype_code) {
                        arr_sort_ingroup.push(i);
                    }
                }
                var ar_sort = SortArray_asc(arr_sort_ingroup);

                for (var i = 0; i < ar_sort.length; i++) {
                    arr_seq.push(ar_sort[i]);
                    if (chk_loancolltype_code == "01") {  // ประเภทคนค้ำ
                        arr_idx.push(ar_sort[i]);
                    } else {
                        var collactive_amt = Number(objdw_coll.GetItem(ar_sort[i], "collactive_amt")); //old
                        var collmax_amt = Number(objdw_coll.GetItem(ar_sort[i], "collmax_amt"));
                        var collbal_amt = Number(objdw_coll.GetItem(ar_sort[i], "collbalance_amt"));
                        var coll_amt = 0;
                        if (collbal_amt > collmax_amt) {
                            coll_amt = collmax_amt;
                        } else {
                            coll_amt = collbal_amt;
                        }
                        if (tmp_loanrequest_amt > 0) {
                            if (coll_amt > tmp_loanrequest_amt) {
                                coll_amt = tmp_loanrequest_amt;
                            }
                            objdw_coll.SetItem(ar_sort[i], "collactive_amt", coll_amt);
                            objdw_coll.SetItem(ar_sort[i], "collactive_percent", calpercent(coll_amt, loanrequest_amt));
                            tmp_loanrequest_amt = tmp_loanrequest_amt - coll_amt;
                        } else {
                            objdw_coll.SetItem(ar_sort[i], "collactive_amt", 0);
                            objdw_coll.SetItem(ar_sort[i], "collactive_percent", 0);
                        }

                        objdw_coll.AcceptText();
                    }
                }
            }
            CalAmt_Person(arr_idx, tmp_loanrequest_amt, loanrequest_amt);
            ResultSum(arr_seq);
        }

        function ResultSum(arr_seq) {
            var sum_amt = 0;
            var sum_percent = 0;
            var i;
            var i_percent = -1;
            for (i = 0; i < (arr_seq.length) - 1; i++) {
                sum_amt = Numberfixed(sum_amt + Numberfixed(objdw_coll.GetItem(arr_seq[i], "collactive_amt"), 2), 2);
                sum_percent = Numberfixed(sum_percent + Numberfixed(objdw_coll.GetItem(arr_seq[i], "collactive_percent"), 2), 2);
            }
            var loanrequest_amt = Numberfixed(objdw_main.GetItem(1, "loanrequest_amt"), 2);
            var validate_sumamt = sum_amt + Numberfixed(objdw_coll.GetItem(arr_seq[i], "collactive_amt"), 2)
            var res_collactive_amt;
            if (validate_sumamt > loanrequest_amt) {
                var last_row = Numberfixed(loanrequest_amt - sum_amt, 2);
                objdw_coll.SetItem(arr_seq[i], "collactive_amt", Numberfixed(last_row, 2));
                res_collactive_amt = loanrequest_amt;
            } else if (validate_sumamt == (loanrequest_amt - 0.01)) {
                var flag = true;
                for (var r = (arr_seq.length) - 1; r >= 0; r--) {
                    var collbalance_amt = Number(objdw_coll.GetItem(arr_seq[r], "collbalance_amt"));
                    var collactive_amt = Number(objdw_coll.GetItem(arr_seq[r], "collactive_amt"));
                    if ((collactive_amt + 0.01) <= collbalance_amt && flag) {
                        objdw_coll.SetItem(arr_seq[r], "collactive_amt", collactive_amt + 0.01);
                        objdw_coll.AcceptText();
                        flag = false;
                        res_collactive_amt = loanrequest_amt;
                    }
                }
            } else if (validate_sumamt < 0) {
                res_collactive_amt = 0;
            } else {
                res_collactive_amt = Numberfixed(validate_sumamt, 2);
            }

            var validate_percent = Numberfixed(sum_percent + Numberfixed(objdw_coll.GetItem(arr_seq[i], "collactive_percent"), 2), 2);
            var res_percent;
            if (validate_percent > 100) {
                var last_row = 100 - sum_percent;
                objdw_coll.SetItem(arr_seq[i], "collactive_percent", Numberfixed(last_row, 2));
                res_percent = 100;

            } else if (validate_percent == 99.99) {
                i_percent = arr_seq.length - 1;
                var collactive_percent = Numberfixed(Number(objdw_coll.GetItem(arr_seq[i_percent], "collactive_percent")), 2);
                objdw_coll.SetItem(arr_seq[i_percent], "collactive_percent", Numberfixed(collactive_percent + 0.01, 2));
                objdw_coll.AcceptText();
                res_percent = 100;
            } else if (validate_percent < 0) {
                res_percent = 0;
            } else {
                res_percent = Numberfixed(validate_percent, 2);
            }
            // compute filed ไม่สามารถ setค่าได้จาก C# javascript ต้อง setด้วย jquery
            $("#objdw_coll_footer span:eq(1)").text(addCommas(res_collactive_amt, 2));
            $("#objdw_coll_footer span:eq(2)").text(addCommas(res_percent, 2));
            objdw_coll.AcceptText();
            jsRefresh();
        }

        function CalAmt_Person(arr_idx, Sum_amt, loanrequest_amt) {
            if (arr_idx.length <= 0) {
                return;
            }
            var count = arr_idx.length;
            var avg = Sum_amt / count;
            if (checkAvg(arr_idx, avg)) {
                for (var i = 0; i < arr_idx.length; i++) {
                    var collactive_amt = Sum_amt / (arr_idx.length - i);

                    collactive_amt = Numberfixed(collactive_amt, 2);
                    Sum_amt = Sum_amt - collactive_amt;

                    objdw_coll.SetItem(arr_idx[i], "collactive_amt", collactive_amt);
                    objdw_coll.SetItem(arr_idx[i], "collactive_percent", calpercent(avg, loanrequest_amt));
                }
                objdw_coll.AcceptText();

            } else {
                var arr_idx2 = new Array();
                var tmp_sum = 0;
                for (var i = 0; i < arr_idx.length; i++) {
                    var collbalance_amt = Number(objdw_coll.GetItem(arr_idx[i], "collbalance_amt"));
                    var val_log = Number(objdw_coll.GetItem(arr_idx[i], "collmax_amt"));
                    var min = collbalance_amt;
                    if (min > val_log) {
                        min = val_log;
                    }
                    if (min <= avg) {
                        objdw_coll.SetItem(arr_idx[i], "collactive_amt", Numberfixed(min, 2));
                        objdw_coll.SetItem(arr_idx[i], "collactive_percent", calpercent(min, loanrequest_amt));
                        tmp_sum += min;
                    } else {
                        arr_idx2.push(arr_idx[i]);
                    }
                }
                objdw_coll.AcceptText();
                CalAmt_Person(arr_idx2, Sum_amt - tmp_sum, loanrequest_amt);
            }
        }

        function SortArray_asc(arr) {
            var arr_amt = new Array();
            for (var i = 0; i < arr.length; i++) {
                var tmp = Number(objdw_coll.GetItem(arr[i], "collbalance_amt"));
                arr_amt.push(tmp);
            }
            for (var i = 0; i < (arr_amt.length) - 1; i++) {
                for (var j = i + 1; j < arr_amt.length; j++) {
                    if (arr_amt[j] < arr_amt[i]) {
                        var tmp1, tmp2;
                        tmp1 = arr_amt[j];
                        arr_amt[j] = arr_amt[i];
                        arr_amt[i] = tmp;

                        tmp2 = arr[j];
                        arr[j] = arr[i];
                        arr[i] = tmp2;
                    }
                }
            }
            return arr;
        }

        function calpercent(val, total) {
            if (total == 0) {
                return 0;
            }
            var percent = Numberfixed((val / total) * 100, 2);
            return percent;
        }

        function Numberfixed(val, fixed) {
            val = Number(val);
            val = (Math.round(val * 100) / 100).toFixed(2);
            return Number(val);
        }

        function addCommas(nStr, fixed) {
            nStr = parseFloat(nStr);
            nStr = nStr.toFixed(2);
            nStr = nStr.toString();
            nStr += '';
            x = nStr.split('.');
            x1 = x[0];
            if (x.length > 1) {
                //        var tmp = parseFloat('0.' + x[1]);
                //        var tmp2 = tmp.toFixed(fixed);
                //        var arr = tmp2.split('.');
                x2 = x[1];
            } else {
                var tmpdata = '';
                for (var i = 0; i < fixed; i++) {
                    tmpdata += '0';
                }
                x2 = tmpdata;
            }
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(x1)) {
                x1 = x1.replace(rgx, '$1' + ',' + '$2');
            }
            return x1 + "." + x2;
        }

        function checkAvg(arr, avg) {
            var flag = true;
            for (var i = 0; i < arr.length; i++) {
                var collbalance_amt = Number(objdw_coll.GetItem(arr[i], "collbalance_amt"));
                var val_log = Number(objdw_coll.GetItem(arr[i], "collmax_amt"));
                if (avg > collbalance_amt || avg > val_log)
                    flag = false;
            }
            return flag;
        }

        function ItemDwOtherclrChanged(sender, rowNumber, columnName, newValue) {
            objdw_otherclr.SetItem(rowNumber, columnName, newValue);
            objdw_otherclr.AcceptText();
            Gcoop.GetEl("Hdothercltrow").value = rowNumber + "";
            if (columnName == "clrothertype_code") {
                if (newValue == "FSV") {
                    objdw_otherclr.SetItem(rowNumber, "clrother_desc", "ค่าธรรมเนียมบริหารเงินกู้");
                } else if (newValue == "SHR") {
                    objdw_otherclr.SetItem(rowNumber, "clrother_desc", "หักซื้อหุ้นเพิ่ม");
                } else if (newValue == "INS") {
                    objdw_otherclr.SetItem(rowNumber, "clrother_desc", "ประกันชีวิต");
                } else if (newValue == "ETC") {
                    objdw_otherclr.SetItem(rowNumber, "clrother_desc", "อื่นๆ");
                } else {

                    jsGetitemdescetc();
                }
                //bee
            } else if (columnName == "clrother_amt") {
                jsPostSetZero();
            }
        }

        //Event----->ClientEventButtonClicked="OnDwOtherclrClicked"
        function OnDwOtherclrClicked(sender, rowNumber, buttonName) {

            Gcoop.GetEl("Hdothercltrow").value = rowNumber + "";
            if (buttonName == "b_delete") {
                //                objdw_otherclr.DeleteRow(rowNumber);
                Gcoop.GetEl("HdRowNumber").value = rowNumber;
                jsPostDelOtherclr();
            }
            else if (buttonName == "b_addrow") {
                objdw_otherclr.InsertRow(objdw_otherclr.RowCount() + 1);
            }
            else if (buttonName == "b_deptother") {
                Gcoop.GetEl("HdRowNumber").value = rowNumber + "";
                Gcoop.GetEl("HdColumnName").value = buttonName;
                // ค้นหาบัญชีเงินฝาก 
                var memberNoVal = objdw_main.GetItem(1, "member_no");
                if ((memberNoVal != null) && (memberNoVal != "")) {
                    //                    alert(3);
                    //Gcoop.OpenDlg(620, 250, "w_dlg_show_accid.aspx", "?member=" + memberNoVal);
                    Gcoop.OpenDlg('620', '250', 'w_dlg_show_accid.aspx', "?member=" + memberNoVal);
                }
            } else if (buttonName == "clrothertype_code") {
                alert(buttonName);
                if (newValue == "FSV") {
                    objdw_otherclr.SetItem(1, "clrother_desc", "ค่าธรรมเนียมบริหารเงินกู้");
                } else if (newValue == "SHR") {
                    objdw_otherclr.SetItem(1, "clrother_desc", "หักซื้อหุ้นเพิ่ม");
                } else if (newValue == "INS") {
                    objdw_otherclr.SetItem(1, "clrother_desc", "ประกันชีวิต");
                } else {

                    jsGetitemdescetc();
                }
            }
        }

        //End. 3.dw_otherclr ****************

        //**************** Start. 4.dw_clear ****************
        //Event----->ClientEventItemChanged="ItemDwClearChanged"
        function ItemDwClearChanged(sender, rowNumber, columnName, newValue) {
            //            alert(newValue);
            if (columnName == "clear_status") {
                objdw_clear.SetItem(rowNumber, columnName, newValue);
                objdw_clear.AcceptText();
                Gcoop.GetEl("Hdcheckrepermiss").value = 1;
                jsPostSumClear();
            }
        }

        //Event----->ClientEventButtonClicked="OnDwClearClicked"
        function OnDwClearClicked(sender, rowNumber, objectName) {
            if (objectName == "b_detail") {
                var loanContractNo = objdw_clear.GetItem(rowNumber, "loancontract_no");
                //                alert("loanContractNo=" + loanContractNo);
                if ((loanContractNo != null) && (loanContractNo != "")) {
                    jsSetDataList();
                    var contractNo = objdw_clear.GetItem(rowNumber, "loancontract_no");
                    Gcoop.OpenDlg(500, 400, 'w_dlg_sl_loanrequest_cleardet.aspx', '?contractNo=' + contractNo);

                }
                else {
                    alert("ไม่สามารถแสดงรายการได้ กรุณากรอกเลขทะเบียนก่อน");
                }
            }
        }

        //ws_dlg_rightcoll
        function GetValue(excludecont, exRightchoose, permiss_amt) {
            Gcoop.GetEl("Hdcoll").value = exRightchoose;
            Gcoop.GetEl("Hdcontclr").value = excludecont;
            objdw_main.SetItem(1, "loancredit_amt", permiss_amt);
            objdw_main.SetItem(1, "loanreqregis_amt", permiss_amt);
            objdw_main.SetItem(1, "loanrequest_amt", permiss_amt);
            jsPostRightColl();
        }

        //w_dlg_sl_loanmember_search
//        function GetValueFromDlgloanMemberSearch(memberno) {
        function GetLoanMemberFromDlg(memberno, full_name) {
            //ส่งกลับจาก หน้าค้นหาสมาชิก
            var rowNumber = Gcoop.GetEl("HdRowNumber").value;
            var colunmName = Gcoop.GetEl("HdColumnName").value;

            if (colunmName == "b_searchc") {
                objdw_coll.SetItem(rowNumber, "ref_collno", memberno);
                objdw_coll.AcceptText();
                Gcoop.GetEl("HdRefcoll").value = memberno;
                Gcoop.GetEl("HdRefcollrow").value = rowNumber + "";
                jsGetMemberCollno();
            }
            else if (colunmName == "b_search") {
                objdw_main.SetItem(1, "member_no", memberno);
                Gcoop.GetEl("HdMemberNo").value = memberno;
                objdw_main.AcceptText();
                jsPostmember();
            }
        }

        //w_dlg_dp_account_search เงินฝาก
        function NewAccountNo(dept_no, deptaccount_name, prncbal) {
            var rowNumber = Gcoop.GetEl("HdRowNumber").value;
            Gcoop.GetEl("HdRefcollrow").value = rowNumber + "";
            objdw_coll.SetItem(rowNumber, "ref_collno", dept_no);
            objdw_coll.SetItem(rowNumber, "description", "บัญชี" + deptaccount_name);
            objdw_coll.SetItem(rowNumber, "coll_amt", prncbal);
            objdw_coll.SetItem(rowNumber, "coll_balance", prncbal);
            objdw_coll.SetItem(rowNumber, "use_amt", prncbal);
            Gcoop.GetEl("HUseamt").value = prncbal;
            objdw_coll.AcceptText();
            jsPostGetCollPermiss();
        }
        //w_dlg_sl_collmaster_search_req หลักทรัพย์
        function GetValueFromDlgCollmast(collmast_no, collmast_desc, est_price, base_percent) {
            if (collmast_desc == null || collmast_desc == "") {
                collmast_desc = "";
            }
            var rowNumber = Gcoop.GetEl("HdRowNumber").value;
            Gcoop.GetEl("HdRefcollrow").value = rowNumber + "";
            var desc = collmast_no + ":" + collmast_desc;
            objdw_coll.SetItem(rowNumber, "ref_collno", collmast_no);
            objdw_coll.SetItem(rowNumber, "description", collmast_desc);
            Gcoop.GetEl("HUseamt").value = est_price;
            Gcoop.GetEl("HdRefcollNO").value = collmast_no;
            objdw_coll.AcceptText();
            jsPostGetCollPermiss();
        }

        //*************************************************************************************
        function LoadDWColl(loanpermiss, xmlcoll, xmlclear) {
            //alert("okkk");
            //   alert(xmlcoll);
            Gcoop.GetEl("HdLoanrightpermiss").value = loanpermiss;
            // alert(loanpermiss);

            //       Gcoop.GetEl("LtXmcoll").value = xmlcoll;
            Gcoop.GetEl("Hdxmlcoll").value = xmlcoll.toString();
            //   alert(xmlclear);
            Gcoop.GetEl("Hdxmlclear").value = xmlclear.toString();
        }
        function collmastclick() {
            Gcoop.GetEl("HdReturn").value = "";
            Gcoop.GetEl("HdColumnName").value = "";
            Gcoop.OpenDlg(720, 200, "w_dlg_sl_loanrequest_loanrightchoose.aspx", "");

        }

        //จ่ายเงินกู้
        function GetShowData() {
        }

        function Disable_ALL() {
            DisabledTable(objdw_main, "loanrequest_status", "dw_main", null);
            DisabledTable(objdw_main, "loanrequest_status", "dw_coll", null);
            DisabledTable(objdw_main, "loanrequest_status", "dw_otherclr", null);
            DisabledTable(objdw_main, "loanrequest_status", "dw_clear", null);
            DisabledTable(objdw_main, "loanrequest_status", "dw_message", null);
        }

        function DisabledTable(s, col, namedw, findname) {
            var chk = s.GetItem(1, col);
            chk = chk.toString();

            if (findname == null || findname == '') {
                findname = '';
            } else {
                findname = ',' + findname;
            }
            var status;
            if (chk == '8' || chk == '11') {
                status = false;
            } else {
                status = true;
            }
            $('#obj' + namedw + '_datawindow').find('input,select,button' + findname).attr('disabled', status)
        }

        function SheetLoadComplete() {
            if (Gcoop.GetEl("HdIsPostBack").value != "true") {
                Gcoop.SetLastFocus("member_no_0");
                Gcoop.Focus();
            }
            if (Gcoop.GetEl("HdCheckRemark").value == "true") {
                var member = Gcoop.GetEl("HdMemberNo").value;
                Gcoop.OpenDlg("460", "200", "w_dlg_ln_remarkstatus.aspx", "?MemberNo=" + member);
                Gcoop.GetEl("HdShowRemark").value = "true";

            }
            var returnVal = Gcoop.GetEl("HdReturn").value;
            var columnVal = Gcoop.GetEl("HdColumnName").value;
            var msgVal = Gcoop.GetEl("HdMsg").value;
            var memberNoVal = Gcoop.GetEl("HdMemberNo").value;
            var contno = Gcoop.GetEl("Hdcontno").value;
            var apvstatus = objdw_main.GetItem(1, "apvimmediate_status");
            var openIframe = Gcoop.GetEl("HdCheck").value;
            if (openIframe == "true") {
                var openedF = Gcoop.GetEl("HdOpened").value;
                if (openedF != "1") {
                    Gcoop.GetEl("HdOpened").value = "1";
                    var member_no = objdw_main.GetItem(1, "member_no");
                    var punishgroup_code = "(1000,3000,5000)";
                    Gcoop.OpenIFrame(720, 400, "w_iframe_punish_detail.aspx", "?member_no=" + member_no + "&punishgroup_code=" + punishgroup_code);
                }
            }
            if (apvstatus == 1 && returnVal == 11) {
                Gcoop.GetEl("Hdcontno").value = "";
                alert(" เลขที่สัญญาเงินกู้ " + contno);
            }
            if (returnVal == 8 && columnVal == "genbaseloancredit") {
                Gcoop.GetEl("HdReturn").value = "";
                Gcoop.GetEl("HdColumnName").value = "";
                Gcoop.OpenDlg(720, 200, "w_dlg_sl_loanrequest_loanrightchoose.aspx", "");
                //                Gcoop.OpenIFrame(720,200, "w_dlg_sl_loanrequest_loanrightchoose.aspx","" );
            } else if (returnVal == 11 && apvstatus == 3) {
                //กรณีบันทึกข้อมูล
                Gcoop.GetEl("HdReturn").value = "";
                //เรียกหน้าจ่ายเงินกู้
                Gcoop.OpenDlg(760, 570, 'w_dlg_sl_popup_loanreceive.aspx', '');
            } else if (returnVal == 11 && apvstatus == 1) {
                var coop_id = objdw_main.GetItem(1, "coop_id");
                var lnrcvfrom_code = "CON";

                Gcoop.GetEl("HdReturn").value = "";
                Gcoop.GetEl("Hdcontno").value = "";
                var word = contno + "@" + lnrcvfrom_code + "@" + coop_id;
                alert(" เลขที่สัญญาเงินกู้ " + contno);
                Gcoop.OpenIFrame2("830", "650", "w_dlg_loan_receive.aspx", "?loans=" + word);
            }
            //--Open IFrame---
            var member_no = objdw_main.GetItem(1, "member_no");
            var loanright_type = Gcoop.GetEl("Hdloanright_type").value;
            var loantype_code = objdw_main.GetItem(1, "loantype_code");
            var OIF = Gcoop.GetEl("HdOIF").value;
            //            var text = "member_no :" + member_no + " loanright_type :" + loanright_type + " loantype_code :" + loantype_code + " OIF :" + OIF;
            //            alert(text.fontsize(10));
            if (loanright_type == 6 && loantype_code == 54 && OIF == "true" && (member_no != "" || member_no != null)) {
                //เฉพาะหุ้นหลัก
                Gcoop.GetEl("HdOIF").value = "false";
                Gcoop.OpenDlg(720, 500, "w_dlg_sl_loanrequest_loanrightchoose_share.aspx", "?member_no=" + member_no + "&loantype_code=" + loantype_code);
            }
            //---
            if (Gcoop.GetEl("HdCondition").value == "true") {
                //CollCondition();
            }

            Disable_ALL();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:Literal ID="Ltdividen" runat="server"></asp:Literal>
    <asp:Literal ID="Ltjspopup" runat="server"></asp:Literal>
    <asp:Literal ID="Ltjspopupclr" runat="server"></asp:Literal>
    <asp:Panel ID="Panel1" runat="server">
        <!--     <span class="NewRowLink" onclick="jsPostPrtRpt()" style="font-size: small;">พิมพ์หนังสือเสนอคณะกรรมการ
        </span> -->
    </asp:Panel>
    <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_sl_loanrequest_master"
        LibraryList="~/DataWindow/Shrlon/sl_loan_requestment.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        ClientEventItemChanged="ItemDwMainChanged" ClientEventClicked="OnDwMainClicked"
        ToolTip=" " ClientFormatting="True" TabIndex="1">
    </dw:WebDataWindowControl>
    <br />
    <asp:Label ID="Label4" runat="server" Text="รายการหักจากเงินเดือน" Font-Bold="True"
        Visible="false">    </asp:Label>
    <%-- <input type="button" value="คำนวณ" style="width: 80px" onclick="OnClickCalMthexp()" />--%>
    <dw:WebDataWindowControl ID="dw_mthexp" runat="server" DataWindowObject="d_sl_loanrequest_mthexp"
        Visible="false" LibraryList="~/DataWindow/Shrlon/sl_loan_requestment.pbl"
        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        ClientScriptable="True" ClientEventItemChanged="ItemDwMthexpChanged" ClientEventClicked="OnDwMthexpClicked"
        ToolTip=" " ClientFormatting="True" TabIndex="1">
    </dw:WebDataWindowControl>
    <%--  <br /> --%>
    <asp:Label ID="Label3" runat="server" Text="หลักประกัน" Font-Bold="True">    </asp:Label>
    <%-- <asp:CheckBox ID="Checkcollloop" runat="server" AutoPostBack="True" OnCheckedChanged="GenpermissCollLoop"
        Text="สถานะแลกกันค้ำ" />--%>
    <asp:CheckBox ID="CbCheckcoop" runat="server" Visible="false" AutoPostBack="True"
        Text="ข้ามสาขา" />
    <asp:Literal ID="LtServerMessagecoll" runat="server"></asp:Literal>
    <dw:WebDataWindowControl ID="dw_coll" runat="server" DataWindowObject="d_sl_loanrequest_collateral"
        LibraryList="~/DataWindow/Shrlon/sl_loan_requestment.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        ClientEventItemChanged="ItemDwCollChanged" ClientEventButtonClicked="OnDwCollClicked"
        ClientFormatting="True" TabIndex="250">
    </dw:WebDataWindowControl>
    <asp:Label ID="Label1" runat="server" Text="รายละเอียดการกู้" Font-Bold="True"></asp:Label>
    <dw:WebDataWindowControl ID="dw_clear" runat="server" DataWindowObject="d_sl_loanrequest_clearlon"
        LibraryList="~/DataWindow/Shrlon/sl_loan_requestment.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        ClientFormatting="True" ClientEventButtonClicked="OnDwClearClicked" ClientEventItemChanged="ItemDwClearChanged"
        TabIndex="600"><%--ClientEventClicked="OnDwClearClicked"--%>
    </dw:WebDataWindowControl>
    <table style="width: 100%;" border="0">
        <tr>
            <td valign="top">
                <asp:Label ID="Label5" runat="server" Text="รายการหักอื่นๆ" Font-Bold="True"></asp:Label>
                <dw:WebDataWindowControl ID="dw_otherclr" runat="server" DataWindowObject="d_sl_loanrequest_otherclr"
                    LibraryList="~/DataWindow/Shrlon/sl_loan_requestment.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                    ClientScriptable="True" ClientEventItemChanged="ItemDwOtherclrChanged" ClientEventButtonClicked="OnDwOtherclrClicked"
                    ToolTip="ต้องการเพิ่มหักเงินฝากกด + แล้วเลือกเงินฝาก" BorderStyle="solid" BorderColor="white"
                    TabIndex="500">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <%--<asp:Label ID="Label2" runat="server" Text="รายการหักกลบ" Font-Bold="True"></asp:Label>
                <dw:WebDataWindowControl ID="dw_clear_hid" runat="server" DataWindowObject="d_sl_loanrequest_clear"
                    LibraryList="~/DataWindow/Shrlon/sl_loan_requestment_dol.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    ClientFormatting="True" ClientEventClicked="OnDwClearClicked" ClientEventItemChanged="ItemDwClearChanged"
                    TabIndex="600" >
                </dw:WebDataWindowControl>--%>
            </td>
            <td valign="top">
                <dw:WebDataWindowControl ID="dw_intspc" runat="server" DataWindowObject="d_sl_loanrequest_intratespc"
                    LibraryList="~/DataWindow/Shrlon/sl_loan_requestment.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    ClientFormatting="True" Visible="False">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <asp:Label ID="Label6" runat="server" Text="รายการหักไม่ได้" Font-Bold="True" Visible="false">    </asp:Label>
    <dw:WebDataWindowControl ID="dw_pmx" runat="server" DataWindowObject="d_sl_loanrequest_pmx"
        LibraryList="~/DataWindow/Shrlon/sl_loan_requestment.pbl" AutoRestoreContext="False"
        Visible="false" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        ClientScriptable="True" ToolTip=" " ClientFormatting="True" TabIndex="1">
    </dw:WebDataWindowControl>
    <%-- <asp:Button ID="cb_checksum" runat="server" Text="ตรวจสอบยอด" Height="80px" Width="55px" OnClick="JsChecksumClick"
                    Style="width: 55px; height: 80px;" />--%>
    <dw:WebDataWindowControl ID="dw_message" runat="server" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        DataWindowObject="d_ln_message" LibraryList="~/DataWindow/shrlon/sl_loan_requestment.pbl">
    </dw:WebDataWindowControl>
    <asp:Literal ID="LtXmlKeeping" runat="server" Visible="False"></asp:Literal>
    <asp:Literal ID="LtXmlReqloop" runat="server" Visible="False"></asp:Literal>
    <asp:Literal ID="LtXmlLoanDetail" runat="server" Visible="False"></asp:Literal>
    <asp:Literal ID="LtXmlOtherlr" runat="server" Visible="False"></asp:Literal>
    <asp:Literal ID="LtXmclear" runat="server" Visible="False"></asp:Literal>
    <asp:Literal ID="LtXmcoll" runat="server" Visible="False"></asp:Literal>
    <asp:TextBox ID="txt_reqNo" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txt_member_no" runat="server" Visible="false"></asp:TextBox>
    <asp:HiddenField ID="HdCondition" runat="server" Value="False" />
    <asp:HiddenField ID="Hdperiodpay" runat="server" />
    <asp:HiddenField ID="Hdcontclr" runat="server" />
    <asp:HiddenField ID="Hdcoll" runat="server" />
    <asp:HiddenField ID="HdReturn" runat="server" />
    <asp:HiddenField ID="Hdmembtype_code" runat="server" />
    <asp:HiddenField ID="HdColumnName" runat="server" />
    <asp:HiddenField ID="HdMemberNo" runat="server" />
    <asp:HiddenField ID="HdRefcoll" runat="server" />
    <asp:HiddenField ID="HdRefcollrow" runat="server" />
    <asp:HiddenField ID="HdRefcollNO" runat="server" />
    <asp:HiddenField ID="HdMemcoopId" runat="server" />
    <asp:HiddenField ID="HdMsg" runat="server" />
    <asp:HiddenField ID="HdXml" runat="server" />
    <asp:HiddenField ID="HdMsgWarning" runat="server" />
    <asp:HiddenField ID="HdRowNumber" runat="server" />
    <asp:HiddenField ID="HdIsPostBack" runat="server" Value="true" />
    <asp:HiddenField ID="Hdprincipal" runat="server" Value="false" />
    <asp:HiddenField ID="Hdobjective" runat="server" Value="false" />
    <asp:HiddenField ID="Hdcoopid" runat="server" Value="false" />
    <asp:HiddenField ID="HUseamt" runat="server" Value="false" />
    <asp:HiddenField ID="HdSelectReport" runat="server" Value="false" />
    <asp:HiddenField ID="HdPopupFlag" runat="server" Value="false" />
    <asp:HiddenField ID="HdCheckRemark" runat="server" Value="false" />
    <asp:HiddenField ID="HdShowRemark" runat="server" Value="true" />
    <asp:HiddenField ID="Hdreqround_factor" runat="server" Value="true" />
    <asp:HiddenField ID="Hdpayround_factor" runat="server" Value="true" />
    <asp:HiddenField ID="Hdlngrpcutright_flag" runat="server" Value="true" />
    <asp:HiddenField ID="Hdinttabrate_code" runat="server" Value="true" />
    <asp:HiddenField ID="Hdfixpaycal_type" runat="server" Value="true" />
    <asp:HiddenField ID="Hdrouninttype" runat="server" Value="true" />
    <asp:HiddenField ID="Hdcustomtime_type" runat="server" Value="true" />
    <asp:HiddenField ID="Hdloanright_type" runat="server" Value="true" />
    <asp:HiddenField ID="Hdloanrighttype_code" runat="server" Value="true" />
    <asp:HiddenField ID="Hdnotmoreshare_flag" runat="server" Value="true" />
    <asp:HiddenField ID="Hdmangrtpermgrp_code" runat="server" Value="true" />
    <asp:HiddenField ID="HdPaymonth" runat="server" />
    <asp:HiddenField ID="HdBalance" runat="server" />
    <asp:HiddenField ID="HdLoanrightpermiss" runat="server" />
    <asp:HiddenField ID="Hdxmlcoll" runat="server" />
    <asp:HiddenField ID="Hdxmlclear" runat="server" />
    <asp:HiddenField ID="Hdresign_timeadd" runat="server" />
    <asp:HiddenField ID="Hdloangrpcredit_type" runat="server" />
    <asp:HiddenField ID="Hdloangrploantype_code" runat="server" />
    <asp:HiddenField ID="Hdcontno" runat="server" />
    <asp:HiddenField ID="HdCheck" runat="server" />
    <asp:HiddenField ID="Hdothercltrow" runat="server" />
    <asp:HiddenField ID="Hdinttabfix_code" runat="server" />
    <asp:HiddenField ID="HdCollmaxval1" runat="server" />
    <asp:HiddenField ID="HdCollmaxval2" runat="server" />
    <asp:HiddenField ID="HdOIF" runat="server" />
    <asp:HiddenField ID="HdClearrow" runat="server" />
    <asp:HiddenField ID="HdFlag" runat="server" />
    <asp:HiddenField ID="HdDetailRow" runat="server" />
    <asp:HiddenField ID="HdCountPerson" runat="server" />
    <asp:HiddenField ID="Hdmthexprow" runat="server" />
    <asp:HiddenField ID="Hdpaymouthexp" runat="server" />
    <asp:HiddenField ID="HdPrtReqdocno" runat="server" />
    <asp:HiddenField ID="Hdcollarray" Value="0.00" runat="server" />
    <asp:HiddenField ID="Hdcheckrepermiss" runat="server" />
</asp:Content>
