<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    ValidateRequest="false" CodeBehind="ws_sl_contract_adjust.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_contract_adjust_ctrl.ws_sl_contract_adjust" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsPayment.ascx" TagName="DsPayment" TagPrefix="uc2" %>
<%@ Register Src="DsLoanpay.ascx" TagName="DsLoanpay" TagPrefix="uc3" %>
<%@ Register Src="DsColl.ascx" TagName="DsColl" TagPrefix="uc4" %>
<%@ Register Src="DsInt.ascx" TagName="DsInt" TagPrefix="uc5" %>
<%@ Register Src="DsIntspc.ascx" TagName="DsIntspc" TagPrefix="uc6" %>
<%@ Register Src="DsSum.ascx" TagName="DsSum" TagPrefix="uc7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        var dsPayment = new DataSourceTool();
        var dsLoanpay = new DataSourceTool();
        var dsColl = new DataSourceTool();
        var dsInt = new DataSourceTool();
        var dsIntspc = new DataSourceTool();

        function Validate() {
            var principal_balance = 0;
            var collreturn_status = dsMain.GetItem(0, "collreturnval_status");
            if (collreturn_status == 1) {
                principal_balance = Number(dsMain.GetItem(0, "loanapprove_amt"));
            } else {
                principal_balance = Number(dsMain.GetItem(0, "principal_balance"));
            }
            var coop_id = dsMain.GetItem(0, "memcoop_id");
            var sumcollactive_amt = Number($('input[name="ctl00$ContentPlace$dsColl$sum_collactive_amt"]').val());
            var sumcollactive_percent = Number($('input[name="ctl00$ContentPlace$dsColl$sum_collactive_percent"]').val());
            if (coop_id != "040001") {
                var chk = $('#ctl00_ContentPlace_chktable').is(':checked');
                if (chk) {
                    if (sumcollactive_percent < 100 || sumcollactive_amt < principal_balance) {
                        alert("ยอดค้ำประกันน้อยกว่ายอดคงเหลือ กรุณาแก้ไข");
                        return;
                    }
                }
            }
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostMemberNo();
            } else if (c == "loancontract_no") {
                PostLoanContractNo();
            }
        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_search") {
                Gcoop.OpenIFrame2("630", "720", "w_dlg_sl_member_search_tks.aspx", "")
            }
            else if (c == "b_contsearch") {
                var member_no = dsMain.GetItem(0, "member_no");
                Gcoop.OpenIFrameExtend('630', '600', 'w_dlg_sl_loancontract_search_memno.aspx', "?memno=" + member_no);
            }
        }

        function GetValueFromDlg(member_no) {
            dsMain.SetItem(0, "member_no", member_no);
            PostMemberNo();
        }

        function GetContFromDlg(loancontract_no) {
            dsMain.SetItem(0, "loancontract_no", loancontract_no);
            PostLoanContractNo();
        }

        function OnDsLoanpayItemChanged(s, r, c, v) {
            if (c == "loanpay_code") {
                PostLoanpay();
            }
            else if (c == "loanpay_bank") {
                PostBank();
            }
        }

        function OnDsIntItemChanged(s, r, c, v) {
            if (c == "int_continttype") {
                PostRefresh();
            }
        }

        function OnDsCollItemChanged(s, r, c, v) {
            dsColl.SetItem(r, c, v);
            Gcoop.GetEl("HdCollRow").value = r;
            if (c == "loancolltype_code") {
                dsColl.SetItem(r, "ref_collno", "");
                dsColl.SetItem(r, "description", "");
                dsColl.SetItem(r, "collbase_amt", 0);
                dsColl.SetItem(r, "collbalance_amt", 0);
                dsColl.SetItem(r, "collactive_amt", 0);
                dsColl.SetItem(r, "collactive_percent", 0);

                if (v == "02") {
                    var coop_id = dsMain.GetItem(0, "memcoop_id");

                    dsColl.SetItem(r, "ref_collno", dsMain.GetItem(0, "member_no"));

                    if (coop_id == "013001") {
                        dsColl.SetItem(r, "description", "ใช้สิทธิตนเองค้ำประกัน");
                    }
                    else {
                        dsColl.SetItem(r, "description", "ทุนเรือนหุ้น" + dsMain.GetItem(0, "cp_name"));
                    }

                    PostGetCollPermiss();
                }
                if (v == "55") {
                    dsColl.SetItem(r, "ref_collno", dsMain.GetItem(0, "member_no"));
                    dsColl.SetItem(r, "description", dsMain.GetItem(0, "cp_name"));
                    PostGetCollPermiss();
                }

            } else if (c == "ref_collno") {
                var loancolltype_code = dsColl.GetItem(r, "loancolltype_code");

                if (loancolltype_code == "" || loancolltype_code == null) {
                    alert("กรุณาเลือกประเภทหลักประกัน");
                    dsColl.SetItem(r, "ref_collno", "");
                } else {
                    PostGetCollPermiss();
                }

            } else if (c == "collactive_amt") {
                var principal_balance = 0;
                var collreturn_status = dsMain.GetItem(0, "collreturnval_status");
                if (collreturn_status == 0) {
                    principal_balance = Number(dsMain.GetItem(0, "loanapprove_amt"));
                } else {
                    principal_balance = Number(dsMain.GetItem(0, "principal_balance"));
                }
                var collactive_amt = Number(dsColl.GetItem(r, "collactive_amt"));
                var collactive_percent = Numberfixed((collactive_amt / principal_balance) * 100, 2);
                dsColl.SetItem(r, "collactive_percent", collactive_percent);
                SumDsColl_Change();
            } else if (c == "collactive_percent") {
                var principal_balance = 0;
                var collreturn_status = dsMain.GetItem(0, "collreturnval_status");
                if (collreturn_status == 0) {
                    principal_balance = Number(dsMain.GetItem(0, "loanapprove_amt"));
                } else {
                    principal_balance = Number(dsMain.GetItem(0, "principal_balance"));
                }
                var collactive_percent = Number(dsColl.GetItem(r, "collactive_percent"));
                var collactive_amt = Numberfixed((collactive_percent * principal_balance) / 100, 2);
                dsColl.SetItem(r, "collactive_amt", collactive_amt);
                SumDsColl_Change();
            }
        }

        function SumDsColl_Change() {
            var count = dsColl.GetRowCount();
            var sumcollactive_amt = 0, sumcollactive_percent = 0;
            for (var i = 0; i < count; i++) {
                var tmpcollactive_amt = Number(dsColl.GetItem(i, "collactive_amt"));
                var tmpcollactive_percent = Number(dsColl.GetItem(i, "collactive_percent"));
                sumcollactive_amt += tmpcollactive_amt;
                sumcollactive_percent += tmpcollactive_percent;
            }

            $('input[name="ctl00$ContentPlace$dsColl$sum_collactive_amt"]').val(addCommas(sumcollactive_amt, 2));
            $('input[name="ctl00$ContentPlace$dsColl$sum_collactive_percent"]').val(addCommas(sumcollactive_percent, 2));
        }

        function OnDsCollClicked(s, r, c) {
            if (c == "b_del") {
                dsColl.SetRowFocus(r);
                PostDeleteRowColl();
            }
            else if (c == "b_search") {
                var loancolltype_code = dsColl.GetItem(r, "loancolltype_code");
                var member_no = dsMain.GetItem(0, "member_no");
                var loantype_code = dsMain.GetItem(0, "loantype_code");

                if (loancolltype_code == "01") {
                    //01คนค้ำ ค้นหาทะเบียนหลักทรัพย์
                    Gcoop.GetEl("HdCollRow").value = r;
                    var coop_id = dsMain.GetItem(0, "memcoop_id");
                    Gcoop.OpenIFrame2('600', '600', 'w_dlg_sl_loanmember_search.aspx', "?coopId=" + coop_id);

                } else if (loancolltype_code == "03") {
                    //03 เงินฝากหลักประกัน
                    Gcoop.GetEl("HdCollRow").value = r;
                    Gcoop.OpenIFrame(860, 250, "w_dlg_dp_account_search.aspx", "?member=" + member_no);

                } else if (loancolltype_code == "04") {
                    //04 หลักทรัพย์ค้ำประกัน
                    Gcoop.GetEl("HdCollRow").value = r;
                    Gcoop.OpenIFrame('700', '300', 'w_dlg_sl_collmaster_search_req.aspx', "?member=" + member_no + "&loantype_code=" + loantype_code);

                } else if (loancolltype_code == "" || loancolltype_code == null) {
                    alert("กรุณาเลือกประเภทหลักประกัน");
                }
            }
            else if (c == "b_show") {
                var loantype_code = dsMain.GetItem(0, "loantype_code");
                var ref_collno = dsColl.GetItem(r, "ref_collno");

                if ((ref_collno != "") && (ref_collno != null)) {

                    var member_no = dsMain.GetItem(0, "member_no");
                    var coop_id = dsColl.GetItem(r, "coop_id");
                    var loancolltype_code = dsColl.GetItem(r, "loancolltype_code");
                    var collbase_amt = dsColl.GetItem(r, "collbase_amt");
                    var collused_amt = dsColl.GetItem(r, "collused_amt");
                    var collbalance_amt = dsColl.GetItem(r, "collbalance_amt");
                    var collbase_percent = dsColl.GetItem(r, "collbase_percent");
                    var description = dsColl.GetItem(r, "description");

                    Gcoop.OpenIFrameExtend('700', '450', 'w_dlg_sl_loanrequest_coll.aspx', "?refCollNo=" + ref_collno + "&coop_id=" + coop_id + "&coll_amt=" + collbase_amt + "&coll_use=" + collused_amt + "&coll_blance=" + collbalance_amt + "&collType=" + loancolltype_code + "&description=" + description + "&loancolltype_code=" + loancolltype_code + "&base_percent= " + collbase_percent + "&row=" + r + "&loanmemno=" + member_no + "&loantype_code=" + loantype_code);
                    return;
                }
            }
        }

        //w_dlg_sl_loanmember_search
//        function GetValueFromDlgloanMemberSearch(member_no) {
            function GetLoanMemberFromDlg(member_no , full_name) {
            var CollRow = Gcoop.GetEl("HdCollRow").value;
            dsColl.SetItem(CollRow, "ref_collno", member_no);
            dsColl.SetRowFocus(CollRow);
            PostGetCollPermiss();
        }

        //w_dlg_sl_collmaster_search_req
        function GetValueFromDlgCollmast(collRefNo, collmast_desc, mortgage_price, base_percent) {
            if (collmast_desc == null) {
                collmast_desc = "";
            }
            var desc = collRefNo + ":" + collmast_desc;
            var CollRow = Gcoop.GetEl("HdCollRow").value;
            dsColl.SetItem(CollRow, "ref_collno", collRefNo);
            dsColl.SetItem(CollRow, "description", collmast_desc);
            dsColl.SetItem(CollRow, "collbase_amt", mortgage_price);
            dsColl.SetItem(CollRow, "collbalance_amt", mortgage_price * base_percent);
            dsColl.SetItem(CollRow, "collactive_amt", mortgage_price * base_percent);
            dsColl.SetItem(CollRow, "collactive_percent", base_percent);
            PostGetCollPermiss();
        }

        //w_dlg_dp_account_search
        function NewAccountNo(dept_no, deptaccount_name, prncbal) {
            var CollRow = Gcoop.GetEl("HdCollRow").value;
            dsColl.SetItem(CollRow, "ref_collno", dept_no);
            dsColl.SetItem(CollRow, "description", deptaccount_name);
            dsColl.SetItem(CollRow, "collbalance_amt", prncbal);
            dsColl.SetItem(CollRow, "collactive_amt", prncbal);
            //dsColl.SetItem(CollRow, "collactive_amt", prncbal);
            //Gcoop.GetEl("HUseamt").value = prncbal;

        }

        function OnDsIntspcClicked(s, r, c) {
            if (c == "b_del") {
                dsIntspc.SetRowFocus(r);
                PostDeleteRowSpc();
            }
        }
        //ทำ
        function RePercent() {
            var percent = 0, sumcollactive_amt = 0, sumcollactive_percent = 0;
            var principal_balance = 0;
            var collreturn_status = dsMain.GetItem(0, "collreturnval_status");
            if (collreturn_status == 0) {
                principal_balance = Number(dsMain.GetItem(0, "loanapprove_amt"));
            } else {
                principal_balance = Number(dsMain.GetItem(0, "principal_balance"));
            }
            for (var i = 0; i < dsColl.GetRowCount(); i++) {
                sumcollactive_amt += parseFloat(dsColl.GetItem(i, "collactive_amt"));
                sumcollactive_percent += parseFloat(dsColl.GetItem(i, "collactive_percent"));
            }
            if (principal_balance == sumcollactive_amt) {
                if (sumcollactive_percent > 100) {
                    percent = dsColl.GetItem(0, "collactive_percent") - (sumcollactive_percent - 100);
                    dsColl.SetItem(0, "collactive_percent", percent);
                } else if (sumcollactive_percent < 100) {
                    percent = dsColl.GetItem(0, "collactive_percent") + (100 - sumcollactive_percent);
                    dsColl.SetItem(0, "collactive_percent", percent);
                }
                SumDsColl_Change()();
            } else {
                alert("ใบคำขอกู้เงิน ยอดค้ำประกันไม่เท่ากับยอดขอกู้ กรุณาตรวจสอบด้วย");
            }
        }
        //ทำ
        function CollCondition() {
            var principal_balance = 0;
            var collreturn_status = dsMain.GetItem(0, "collreturnval_status");
            if (collreturn_status == 0) {
                principal_balance = Number(dsMain.GetItem(0, "loanapprove_amt"));
            } else {
                principal_balance = Number(dsMain.GetItem(0, "principal_balance"));
            }
            re_pole(principal_balance);
        }

        function re_pole(principal_balance) {
            var arr_seq = new Array();
            var tmp_principal_balance = principal_balance;
            var arr_priority = ["04", "03", "02", "01"];

            for (var j = 0; j < arr_priority.length; j++) {
                var arr_sort_ingroup = new Array();
                var chk_loancolltype_code = arr_priority[j];
                for (var i = 0; i < dsColl.GetRowCount(); i++) {
                    var loancolltype_code = dsColl.GetItem(i, "loancolltype_code");
                    if (loancolltype_code == chk_loancolltype_code) {
                        arr_sort_ingroup.push(i);
                    }
                }
                var ar_sort = SortArray_asc(dsColl, arr_sort_ingroup);
                var arr_idx = new Array();
                for (var i = 0; i < ar_sort.length; i++) {

                    arr_seq.push(ar_sort[i]);
                    if (chk_loancolltype_code == "01") {  // ประเภทคนค้ำ
                        arr_idx.push(ar_sort[i]);
                    } else if (chk_loancolltype_code == "04") {
                        arr_idx.push(ar_sort[i]);
                    } else {
                        var collactive_amt = Number(dsColl.GetItem(ar_sort[i], "collactive_amt"));
                        var collmax_amt = Number(dsColl.GetItem(ar_sort[i], "collmax_amt"));
                        var collbal_amt = Number(dsColl.GetItem(ar_sort[i], "collbalance_amt"));
                        var coll_amt = 0;
                        if (collbal_amt > collmax_amt) {
                            coll_amt = collmax_amt;
                        } else {
                            coll_amt = collbal_amt;
                        }

                        if (tmp_principal_balance > 0) {
                            if (coll_amt > tmp_principal_balance) {
                                coll_amt = tmp_principal_balance;
                            }
                            dsColl.SetItem(ar_sort[i], "collactive_amt", coll_amt);
                            dsColl.SetItem(ar_sort[i], "collactive_percent", calpercent(coll_amt, principal_balance));
                            tmp_principal_balance = tmp_principal_balance - coll_amt;
                        } else {
                            dsColl.SetItem(ar_sort[i], "collactive_amt", 0);
                            dsColl.SetItem(ar_sort[i], "collactive_percent", 0);
                        }
                        //ของเดิม
                        //                        if (tmp_principal_balance >= collactive_amt) {
                        //                            tmp_principal_balance = tmp_principal_balance - collactive_amt;
                        //                            dsColl.SetItem(ar_sort[i], "collactive_percent", calpercent(collactive_amt, principal_balance));
                        //                        } else {
                        //                            if (tmp_principal_balance > 0) {
                        //                                dsColl.SetItem(ar_sort[i], "collactive_amt", tmp_principal_balance);
                        //                                dsColl.SetItem(ar_sort[i], "collactive_percent", calpercent(tmp_principal_balance, principal_balance));
                        //                                tmp_principal_balance = 0;
                        //                            } else {
                        //                                dsColl.SetItem(ar_sort[i], "collactive_amt", 0);
                        //                                dsColl.SetItem(ar_sort[i], "collactive_percent", 0);
                        //                                tmp_principal_balance = 0;
                        //                            }
                        //                        }
                    }
                }
                //                if (chk_loancolltype_code == "01") {
                //                    CalAmt_Person(arr_idx, tmp_principal_balance, principal_balance);
                //                    tmp_principal_balance = tmp_principal_balance - Sumcollactive_amtByloancolltype_code(arr_idx);
                //                } else if (chk_loancolltype_code == "04") {
                //                    CalAmt_Person(arr_idx, tmp_principal_balance, principal_balance);
                //                    tmp_principal_balance = tmp_principal_balance - Sumcollactive_amtByloancolltype_code(arr_idx);
                //                }
                CalAmt_Person(arr_idx, tmp_principal_balance, principal_balance);
            }

            ResultSum(arr_seq);
        }

        function Sumcollactive_amtByloancolltype_code(arr_idx) {
            var sum = 0;
            for (var i = 0; i < arr_idx.length; i++) {
                var collbalance_amt = Number(dsColl.GetItem(arr_idx[i], "collbalance_amt"));
                sum += collbalance_amt;
            }
            return sum;
        }
        //ทำ
        function cal_collactive(arr_seq) {
            var sum = 0;
            //var principal_balance = Numberfixed(dsMain.GetItem(0, "principal_balance"), 2);
            var principal_balance = 0;
            var collreturn_status = dsMain.GetItem(0, "collreturnval_status");
            if (collreturn_status == 0) {
                principal_balance = Numberfixed(dsMain.GetItem(0, "loanapprove_amt"), 2);
            } else {
                principal_balance = Numberfixed(dsMain.GetItem(0, "principal_balance"), 2);
            }
            for (var i = 0; i < (arr_seq.length); i++) {
                var loancolltype_code = dsColl.GetItem(arr_seq[i], "loancolltype_code");
                if (loancolltype_code == "01") {
                    if (i == (arr_seq.length) - 1) {
                        var dif_collactive_amt = principal_balance - sum;
                        dsColl.SetItem(arr_seq[i], "collactive_amt", dif_collactive_amt);
                    } else {
                        var collactive_percent = Numberfixed(dsColl.GetItem(arr_seq[i], "collactive_percent"), 2);
                        var collactive_amt = Numberfixed((collactive_percent * principal_balance) / 100, 2);
                        dsColl.SetItem(arr_seq[i], "collactive_amt", collactive_amt);
                        sum += collactive_amt;
                    }

                }
            }
        }

        function ResultSum(arr_seq) {

            //cal_collactive(arr_seq);
            var sum_amt = 0;
            var sum_percent = 0;
            var i;
            var i_percent = -1;

            for (i = 0; i < (arr_seq.length) - 1; i++) {
                sum_amt = Numberfixed(sum_amt + Numberfixed(dsColl.GetItem(arr_seq[i], "collactive_amt"), 2), 2);
                sum_percent = Numberfixed(sum_percent + Numberfixed(dsColl.GetItem(arr_seq[i], "collactive_percent"), 2), 2);
            }

            var validate_percent = Numberfixed(sum_percent + Numberfixed(dsColl.GetItem(arr_seq[i], "collactive_percent"), 2), 2);
            var res_percent;
            if (validate_percent > 100) {
                var last_row = 100 - sum_percent;
                dsColl.SetItem(arr_seq[i], "collactive_percent", Numberfixed(last_row, 2));
                res_percent = 100;

            } else if (validate_percent == 99.99) {
                i_percent = arr_seq.length - 1;
                var collactive_percent = Numberfixed(Number(dsColl.GetItem(arr_seq[i_percent], "collactive_percent")), 2);
                dsColl.SetItem(arr_seq[i_percent], "collactive_percent", Numberfixed(collactive_percent + 0.01, 2));
                res_percent = 100;
            } else if (validate_percent < 0) {
                res_percent = 0;
            } else {
                res_percent = Numberfixed(validate_percent, 2);
            }

            //var loanrequest_amt = Numberfixed(dsMain.GetItem(0, "principal_balance", 20), 2);
            var loanrequest_amt = 0;
            var collreturn_status = dsMain.GetItem(0, "collreturnval_status");
            if (collreturn_status == 0) {
                loanrequest_amt = Numberfixed(dsMain.GetItem(0, "loanapprove_amt", 20), 2);
            } else {
                loanrequest_amt = Numberfixed(dsMain.GetItem(0, "principal_balance", 20), 2);
            }
            var validate_sumamt = sum_amt + Numberfixed(dsColl.GetItem(arr_seq[i], "collactive_amt"), 2)
            var res_collactive_amt;
            if (validate_sumamt > loanrequest_amt) {
                var last_row = Numberfixed(loanrequest_amt - sum_amt, 2);
                dsColl.SetItem(arr_seq[i], "collactive_amt", Numberfixed(last_row, 2));
                res_collactive_amt = loanrequest_amt;
            } else if (validate_sumamt == (loanrequest_amt - 0.01)) {
                var flag = true;
                for (var r = (arr_seq.length) - 1; r >= 0; r--) {
                    var collbalance_amt = Number(dsColl.GetItem(arr_seq[r], "collbalance_amt"));
                    var collactive_amt = Number(dsColl.GetItem(arr_seq[r], "collactive_amt"));
                    if ((collactive_amt + 0.01) <= collbalance_amt && flag) {
                        dsColl.SetItem(arr_seq[r], "collactive_amt", collactive_amt + 0.01);

                        flag = false;
                        res_collactive_amt = loanrequest_amt;
                    }
                }
            } else if (validate_sumamt < 0) {
                res_collactive_amt = 0;
            } else {
                res_collactive_amt = Numberfixed(validate_sumamt, 2);
            }

            $('input[name="ctl00$ContentPlace$dsColl$sum_collactive_amt"]').val(addCommas(res_collactive_amt, 2));
            $('input[name="ctl00$ContentPlace$dsColl$sum_collactive_percent"]').val(addCommas(sum_collactive_percent, 2));
            //            dsSum.SetItem(0, "sum_collactive_amt", res_collactive_amt);
            //            dsSum.SetItem(0, "sum_collactive_percent", res_percent);

        }

        function CalAmt_Person(arr_idx, Sum_amt, loanrequest_amt) {
            if (arr_idx.length <= 0) {
                return;
            }
            var count = arr_idx.length;
            var avg = Sum_amt / count;
            if (checkAvg(dsColl, arr_idx, avg)) {
                for (var i = 0; i < arr_idx.length; i++) {
                    var collactive_amt = Sum_amt / (arr_idx.length - i);

                    collactive_amt = Numberfixed(collactive_amt, 2);
                    Sum_amt = Sum_amt - collactive_amt;
                    dsColl.SetItem(arr_idx[i], "collactive_amt", Numberfixed(collactive_amt, 2));
                    dsColl.SetItem(arr_idx[i], "collactive_percent", calpercent(avg, loanrequest_amt));
                }

            } else {
                var arr_idx2 = new Array();
                var tmp_sum = 0;
                for (var i = 0; i < arr_idx.length; i++) {
                    var collbalance_amt = Number(dsColl.GetItem(arr_idx[i], "collbalance_amt"));
                    var val_log = Number(dsColl.GetItem(arr_idx[i], "collmax_amt"));
                    var min = collbalance_amt;
                    if (min > val_log) {
                        min = val_log;
                    }
                    if (min <= avg) {
                        dsColl.SetItem(arr_idx[i], "collactive_amt", Numberfixed(min, 2));
                        dsColl.SetItem(arr_idx[i], "collactive_percent", calpercent(min, loanrequest_amt));
                        tmp_sum += min;
                    } else {
                        arr_idx2.push(arr_idx[i]);
                    }
                }
                CalAmt_Person(arr_idx2, Sum_amt - tmp_sum, loanrequest_amt);
            }

        }

        function SortArray_asc(s, arr) {
            var arr_amt = new Array();
            for (var i = 0; i < arr.length; i++) {
                var tmp = Number(s.GetItem(arr[i], "collbalance_amt"));
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
            var percent = Numberfixed((val / total) * 100, 2);
            return percent;
        }

        function Numberfixed(val, fixed) {
            val = Number(val);
            val = fncToFixed(val, fixed);
            return Number(val);
        }

        function fncToFixed(num, decimals) {
            num *= Math.pow(10, decimals);
            num = (Math.round(num, decimals) + (((num - Math.round(num, decimals)) >= 0.4) ? 1 : 0)) / Math.pow(10, decimals);
            return num.toFixed(decimals);
        }

        function addCommas(nStr, fixed) {
            nStr = parseFloat(nStr);
            nStr = fncToFixed(nStr, fixed);
            nStr = nStr.toString();
            nStr += '';
            x = nStr.split('.');
            x1 = x[0];
            if (x.length > 1) {
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

        function checkAvg(s, arr, avg) {
            var flag = true;
            for (var i = 0; i < arr.length; i++) {
                var collbalance_amt = Number(s.GetItem(arr[i], "collbalance_amt"));
                var val_log = Number(s.GetItem(arr[i], "collmax_amt"));
                if (avg > collbalance_amt || avg > val_log)
                    flag = false;
            }
            return flag;
        }

        function SetdsSum() {
            dsSum.SetItem(0, "sum_collactive_amt", 0);
            dsSum.SetItem(0, "sum_collactive_percent", 0);
            CollCondition();
        }

        function Open_tabledsPayment() {
            DisabledTableFormView1('chkdsPayment', 'dsPayment', null);
        }

        function Open_tabledsLoanpay() {
            DisabledTableFormView1('chkdsLoanpay', 'dsLoanpay', null);
        }

        function Open_tabledsColl() {
            DisabledTableRepeater('chktable', 0, null);
            DisabledButton('chktable', 'btntbdsColl');
        }

        function Check_PostInsertRowColl() {
            var chk = $('#ctl00_ContentPlace_chktable').is(':checked');
            if (chk) {
                PostInsertRowColl();
            }
        }

        function Open_tabledsInt() {
            DisabledTableFormView1('chkdsInt', 'dsInt', null);
            DisabledTableRepeater('chkdsInt', 1, null);
        }

        function Check_PostInsertRowIntspc() {
            var chk = $('#ctl00_ContentPlace_chkdsInt').is(':checked');
            if (chk) {
                PostInsertRowIntspc();
            }
        }

        function SheetLoadComplete() {
            Open_tabledsPayment();
            Open_tabledsLoanpay();
            Open_tabledsColl();
            Open_tabledsInt();
        }

        function DisabledTableFormView1(namecheckbox, nameDw, findname) {
            var chk = $('#ctl00_ContentPlace_' + namecheckbox).is(':checked');
            if (findname == null || findname == '') {
                findname = '';
            } else {
                findname = ',' + findname;
            }
            var status;
            if (chk) {
                status = false;
            } else {
                status = true;
            }
            $('#ctl00_ContentPlace_' + nameDw + '_FormView1').find('input,select,button' + findname).attr('disabled', status)
        }

        function DisabledTableRepeater(namecheckbox, numberRepeater, findname) {
            var chk = $('#ctl00_ContentPlace_' + namecheckbox).is(':checked');
            if (findname == null || findname == '') {
                findname = '';
            } else {
                findname = ',' + findname;
            }
            var status;
            if (chk) {
                status = false;
            } else {
                status = true;
            }
            $('.DataSourceRepeater').eq(numberRepeater).find('input,select,button' + findname).attr('disabled', status)
        }

        function DisabledButton(namecheckbox, idbtn) {
            var chk = $('#ctl00_ContentPlace_' + namecheckbox).is(':checked');
            var status;
            if (chk) {
                status = false;
            } else {
                status = true;
            }
            $('#' + idbtn).find('button').prop('disabled', status);
        }

        function OnDsPaymentItemChanged(s, r, c, v) {
            if (c == "period_payment") {
                PostPeriodPayment();
            }
            else if (c == "period_payamt") {
                PostPeriodPayamt();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="HdCollRow" runat="server" Value="" />
    <asp:HiddenField ID="HdColl" runat="server" Value="" />
    <asp:HiddenField ID="HdSpc" runat="server" Value="" />
    <table>
        <tr>
            <td colspan="2">
                <uc1:DsMain ID="dsMain" runat="server" />
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <u><b>การเรียกเก็บ</b></u>&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="chkdsPayment" Checked="false" runat="server" onclick="Open_tabledsPayment()" />&nbsp;<span
                    style="font-size: 15px">แก้ไขการเรียกเก็บ</span>
                <uc2:DsPayment ID="dsPayment" runat="server" />
            </td>
            <td valign="top">
                <u><b>บัญชีเรียกเก็บ</b></u>&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="chkdsLoanpay" Checked="false" runat="server" onclick="Open_tabledsLoanpay()" />&nbsp;
                <span style="font-size: 15px">แก้ไขบัญชีเรียกเก็บ</span>
                <uc3:DsLoanpay ID="dsLoanpay" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
                <asp:Literal ID="LtServerMessagecoll" runat="server"></asp:Literal>
                <u><b>หลักประกันเงินกู้</b></u> &nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="chktable" Checked="false" runat="server" onclick="Open_tabledsColl()" />&nbsp;<span
                    style="font-size: 15px">แก้ไขหลักประกันเงินกู้</span>
                <div id="btntbdsColl" align="right">
                    <span id="Span1" class="NewRowLink" onclick="CollCondition()">re ค้ำ</span> <span
                        id="Span2" class="NewRowLink" onclick="RePercent()">re %</span> <span id="insertdsColl"
                            class="NewRowLink" onclick="Check_PostInsertRowColl()">&nbsp;เพิ่มแถว</span>
                </div>
                <uc4:DsColl ID="dsColl" runat="server" />
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <u><b>อัตราดอกเบี้ยของสัญญา</b></u>&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="chkdsInt" Checked="false" runat="server" onclick="Open_tabledsInt()" />&nbsp;<span
                    style="font-size: 15px">แก้ไขอัตราดอกเบี้ยของสัญญา</span>
                <uc5:DsInt ID="dsInt" runat="server" />
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <u><b>อัตราดอกเบี้ยพิเศษเป็นช่วง</b></u>
                <div align="right">
                    <span class="NewRowLink" onclick="Check_PostInsertRowIntspc()">เพิ่มแถว</span>
                </div>
                <uc6:DsIntspc ID="dsIntspc" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
