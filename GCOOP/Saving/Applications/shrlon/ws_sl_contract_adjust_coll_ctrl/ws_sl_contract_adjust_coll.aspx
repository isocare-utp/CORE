<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    ValidateRequest="false" CodeBehind="ws_sl_contract_adjust_coll.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_contract_adjust_coll_ctrl.ws_sl_contract_adjust_coll" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsColl.ascx" TagName="DsColl" TagPrefix="uc2" %>
<%@ Register Src="DsCollOld.ascx" TagName="DsCollOld" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        var dsColl = new DataSourceTool();
        var dsCollOld = new DataSourceTool();

        function Validate() {
            var principal_balance = 0;
            var collreturn_status = dsMain.GetItem(0, "collreturnval_status");
            if (collreturn_status == 1) {
                //principal_balance = Number(dsMain.GetItem(0, "loanapprove_amt"));
                principal_balance = Number(dsMain.GetItem(0, "principal_balance"));
            } else {
                principal_balance = Number(dsMain.GetItem(0, "principal_balance"));
            }
            var count = dsColl.GetRowCount();
            if (count > 0) {
                var coop_id = dsMain.GetItem(0, "memcoop_id");
                var sumcollactive_amt = ReSumDsColl_Change();  //$('input[name="ctl00$ContentPlace$dsColl$sum_collactive_amt"]').val();
                //Math.round(sumcollactive_percent);
                var sumcollactive_percent = Number($('input[name="ctl00$ContentPlace$dsColl$sum_collactive_percent"]').val());
                //if (sumcollactive_percent != 100 || sumcollactive_amt != principal_balance) { //sumcollactive_amt != principal_balance) 
                //    alert("ค้ำประกันเกินยอดคงเหลือ กรุณาแก้ไข");
                //    return;
                //}
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
                Gcoop.OpenIFrame('630', '600', 'w_dlg_sl_loancontract_search_memno.aspx', "?memno=" + member_no);
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
                    dsColl.SetItem(r, "ref_collno", dsMain.GetItem(0, "member_no"));
                    dsColl.SetItem(r, "description", "ทุนเรือนหุ้น" + dsMain.GetItem(0, "cp_name"));
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
                var principal_balance = 0 ;
                var collreturn_status = dsMain.GetItem(0, "collreturnval_status");
                if (collreturn_status == 0) {
                    principal_balance = Number(dsMain.GetItem(0, "loanapprove_amt"));
                } else {
                    if(Number(dsMain.GetItem(0, "principal_balance")) == 0 ){
                       principal_balance =  Number(dsMain.GetItem(0, "loanapprove_amt"));
                    }else{
                       principal_balance =  Number(dsMain.GetItem(0, "principal_balance"));
                    }
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
                    if (Number(dsMain.GetItem(0, "principal_balance")) == 0) {
                        principal_balance = Number(dsMain.GetItem(0, "loanapprove_amt"));
                    } else {
                        principal_balance = Number(dsMain.GetItem(0, "principal_balance"));
                    }
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
            sumcollactive_percent = Math.round(sumcollactive_percent);
            sumcollactive_amt = Math.round(sumcollactive_amt);
            var principal_balance = 0;

            if (Number(dsMain.GetItem(0, "principal_balance")) == 0) {
                principal_balance = Number(dsMain.GetItem(0, "loanapprove_amt"));
            } else {
                principal_balance = Number(dsMain.GetItem(0, "principal_balance"));
            }

            if( sumcollactive_percent == 100 && sumcollactive_amt > principal_balance )
            {
                sumcollactive_amt = dsMain.GetItem(0, "principal_balance");
            }
            $('input[name="ctl00$ContentPlace$dsColl$sum_collactive_amt"]').val(addCommas(sumcollactive_amt, 2));
            $('input[name="ctl00$ContentPlace$dsColl$sum_collactive_percent"]').val(addCommas(sumcollactive_percent, 2));
        }

        function ReSumDsColl_Change() {
            var count = dsColl.GetRowCount();
            var sumcollactive_amt = 0, sumcollactive_percent = 0;
            for (var i = 0; i < count; i++) {
                var tmpcollactive_amt = Number(dsColl.GetItem(i, "collactive_amt"));
                var tmpcollactive_percent = Number(dsColl.GetItem(i, "collactive_percent"));
                sumcollactive_amt += tmpcollactive_amt;
                sumcollactive_percent += tmpcollactive_percent;
            }
            sumcollactive_amt = Math.round(sumcollactive_amt);
            return Number(sumcollactive_amt);
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
                    Gcoop.OpenIFrame('600', '600', 'w_dlg_sl_loanmember_search.aspx', "?coopId=" + coop_id);

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

                    Gcoop.OpenDlg('700', '450', 'w_dlg_sl_loanrequest_coll.aspx', "?refCollNo=" + ref_collno + "&coop_id=" + coop_id + "&coll_amt=" + collbase_amt + "&coll_use=" + collused_amt + "&coll_blance=" + collbalance_amt + "&collType=" + loancolltype_code + "&description=" + description + "&loancolltype_code=" + loancolltype_code + "&base_percent= " + collbase_percent + "&row=" + r + "&loanmemno=" + member_no + "&loantype_code=" + loantype_code);
                    return;
                }
            }
        }

        //w_dlg_sl_loanmember_search
        function GetValueFromDlgloanMemberSearch(member_no) {
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
                SumDsColl_Change();
            } else {
                alert("ใบคำขอกู้เงิน ยอดค้ำประกันไม่เท่ากับยอดขอกู้ กรุณาตรวจสอบด้วย");
            }
        }

        //check diff 0.01
        function RePercentDiff() {
            var percent = 0, sumcollactive_amt = 0, sumcollactive_percent = 0;
            var principal_balance = 0;
            principal_balance = Number(dsMain.GetItem(0, "principal_balance"));
            var sumcollactive_amt = Number($('input[name="ctl00$ContentPlace$dsColl$sum_collactive_amt"]').val());
            var sumcollactive_percent = Number($('input[name="ctl00$ContentPlace$dsColl$sum_collactive_percent"]').val());
            if (sumcollactive_percent != 100 || sumcollactive_amt > principal_balance) {
                if (sumcollactive_percent > 100) {
                    percent = dsColl.GetItem(0, "collactive_percent") - (sumcollactive_percent - 100);
                    dsColl.SetItem(0, "collactive_percent", NumberRound(percent,2));
                } else if (sumcollactive_percent < 100) {
                    percent = dsColl.GetItem(0, "collactive_percent") + (100 - sumcollactive_percent);
                    dsColl.SetItem(0, "collactive_percent", percent);
                }
            }
            var collactive_percent = Number(dsColl.GetItem(0, "collactive_percent"));
            var collactive_amt = Numberfixed((collactive_percent * principal_balance) / 100, 2);
            dsColl.SetItem(0, "collactive_amt", collactive_amt);
            SumDsColl_Change();
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


        function SheetLoadComplete() {
            Open_tabledsColl();
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

        function ReCollCondition() {
            var principal_balance = dsMain.GetItem(0, "principal_balance");
            if (principal_balance == 0) {
                principal_balance = Number(dsMain.GetItem(0, "loanapprove_amt"));
            } else {
                principal_balance = Number(dsMain.GetItem(0, "principal_balance"));
            }
            var rows = dsColl.GetRowCount();
            
            var percent_who = (100 / rows).toFixed(2);
            var precent_last = 100 - (percent_who * (rows - 1));
            var collactive_amt = (principal_balance / rows).toFixed(2);
            alert(collactive_amt);
            var collactive_last = principal_balance - (collactive_amt * (rows - 1));
            for (var i = 0; i < rows-1; i++) {
                dsColl.SetItem(i, "collactive_percent", percent_who);
                dsColl.SetItem(i, "collactive_amt", collactive_amt);
            }
            dsColl.SetItem(rows - 1, "collactive_percent", precent_last.toFixed(2));
            dsColl.SetItem(rows - 1, "collactive_amt", collactive_last.toFixed(2));

            var sum_percent = (percent_who * (rows - 1)) + precent_last;
            var sum_collavtive = (collactive_amt * (rows - 1)) + collactive_last; 
            $('input[name="ctl00$ContentPlace$dsColl$sum_collactive_amt"]').val(addCommas(sum_collavtive, 2));
            $('input[name="ctl00$ContentPlace$dsColl$sum_collactive_percent"]').val(addCommas(sum_percent, 2));

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="HdCollRow" runat="server" Value="" />
    <asp:HiddenField ID="HdColl" runat="server" Value="" />
    <asp:HiddenField ID="HdCollOld" runat="server" Value="" />
    <asp:HiddenField ID="HdSpc" runat="server" Value="" />
    <table>
        <tr>
            <td colspan="2">
                <uc1:DsMain ID="dsMain" runat="server" />
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
                   <!-- <span id="Span1" class="NewRowLink" onclick="CollCondition()">re ค้ำ</span> -->
                  <span id="Span1" class="NewRowLink" onclick="ReCollCondition()">re ค้ำ</span> 
                    <!-- <span id="Span2" class="NewRowLink" onclick="RePercent()">re %</span> -->
                     <!--  <span id="Span3" class="NewRowLink" onclick="RePercentDiff()">re diff 0.01</span>-->
                    <span id="insertdsColl" class="NewRowLink" onclick="Check_PostInsertRowColl()">&nbsp;เพิ่มแถว</span>
                </div>
                <uc2:DsColl ID="dsColl" runat="server" />
                <br />
            </td>
        </tr>
        <uc3:DsCollOld ID="dsCollOld" runat="server" Visible="false" />
    </table>
</asp:Content>
