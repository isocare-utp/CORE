<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_slip_deposit.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.w_sheet_dp_slip_deposit" Title="Untitled Page"
    ValidateRequest="false" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postPost%>
    <%=postNewAccount%>
    <%=postDeptWith%>
    <%=postRecpPayTypeCode%>
    <%=newClear%>
    <%=postTotalWidthFixed%>
    <%=postItemSelect%>
    <%=postSaveNoCheckApv%>
    <%=postInsertRowCheque%>
    <%=postDeleteRowCheque%>
    <%=postBankCode%>
    <%=postBankBranchCode%>
    <%=postpassbook%>
    <%=CheckCoop %>
    <%=setCoopname%>
    <%=postchgremark%>
    <%=PostPrint%>
    <style type="text/css">
        .test
        {
            background: #EBCBC7;
            width: 99%;
            height: 66px;
            text-align: center;
            color: #990000;
            font-weight: bold;
            vertical-align: middle;
            border: 1px solid BB0000;
        }
        .linkInsertRow
        {
            font-family: Tahoma, Sans-Serif;
            font-weight: bold;
            font-size: 12px;
            cursor: pointer;
            color: #005599;
            text-align: left;
            text-decoration: underline;
        }
    </style>
    <script type="text/javascript">
        var isDwItemEdit = false;

        function CallPrintBook(slipNo) {
            Gcoop.OpenDlg(900, 500, "w_dlg_dp_printbook.aspx", "");
        }

        function GetDlgBankAndBranch(sheetRow, bankCode, bankDesc, branchCode, branchDesc) {
            sheetRow = Gcoop.ParseInt(sheetRow);
            objDwCheque.SetItem(sheetRow, "bank_code", bankCode);
            objDwCheque.AcceptText();

            objDwCheque.SetItem(sheetRow, "bank_name", bankDesc);
            objDwCheque.AcceptText();

            objDwCheque.SetItem(sheetRow, "branch_code", branchCode);
            objDwCheque.AcceptText();

            objDwCheque.SetItem(sheetRow, "branch_name", branchDesc);
            objDwCheque.AcceptText();
        }

        function GetValueCheckApv(valueCheckApv, nameApv) {
            if (valueCheckApv == "success") {
                objDwMain.SetItem(1, "authorize_id", nameApv);
                objDwMain.AcceptText();
                postSaveNoCheckApv();
            } else {
                Gcoop.SetLastFocus("deptformat_0");
                Gcoop.RemoveIFrame();
            }
        }

        function MenubarNew() {
            if (confirm("ยืนยันการล้างข้อมูลบนหน้าจอ")) {
                window.location = "";
            }
        }

        function MenubarOpen() {
            Gcoop.GetEl("HfCheck").value = "True";
            Gcoop.OpenIFrame(900, 600, "w_dlg_dp_account_search.aspx", "");
        }

        function NewAccountNo(coopid, accNo) {
            objDwMain.SetItem(1, "deptformat", Gcoop.Trim(accNo));
            Gcoop.GetEl("HfCoopid").value = coopid + "";
            objDwMain.AcceptText();

            if (Gcoop.GetEl("HfCheck").value == "True") {
                setCoopname();
            }
            else if (Gcoop.GetEl("HfCheck").value == "False") {
                postNewAccount();
            }
        }

        function OnDwListCoopClick(s, r, c) {
            if (c == "cross_coopflag") {
                Gcoop.CheckDw(s, r, c, "cross_coopflag", 1, 0);
                CheckCoop();
            }
        }

        function OnDwListCoopItemChanged(s, r, c, v) {
            if (c == "dddwcoopname") {
                s.SetItem(r, c, v);
                s.AcceptText();
                var coopid = s.GetItem(r, "dddwcoopname");
                Gcoop.GetEl("HfCoopid").value = coopid + "";
                objDwMain.SetItem(r, "deptcoop_id", coopid);
            }
        }

        function OnButtonClicked(s, row, oName) {
            if (oName == "b_account") {
                Gcoop.OpenIFrame(900, 600, "w_dlg_dp_account_search.aspx", "?coopid=" + objDwMain.GetItem(1, "deptcoop_id"));
            } else if (oName == "b_recppaytype") {
                TryOpenDlgDeptWith();
            } else if (oName == "b_movement") {
                Gcoop.OpenDlg(800, 460, "w_dlg_dp_deptstatement.aspx", "?accountNo=" + objDwMain.GetItem(1, "deptaccount_no"));
            } else if (oName == "cb_referslip") {
                var recpPayType = objDwMain.GetItem(row, "recppaytype_code");
                if (recpPayType == "DTR" || recpPayType == "WTR") {
                    Gcoop.OpenDlg(650, 600, "w_dlg_dp_slip_search_tran.aspx", "?accountNo=" + objDwMain.GetItem(1, "deptaccount_no"));
                }

            } else if (oName == "b_signature") {
                var dpdept_no = objDwMain.GetItem(1, "deptformat");
                var coopid    = Gcoop.GetEl("HfCoopid").value ;
                if (coopid == "027001") {
                    var dpdept_no = objDwMain.GetItem(1, "deptaccount_no");
                }
                Gcoop.OpenIFrame2Extend("918", "533", "w_dlg_dp_signature.aspx", "?dpdept_no=" + dpdept_no);

            } 
            return 0;
        }

        function OnClickCalInt() {
            Gcoop.GetEl("HdRequireCalInt").value = "false";
            postItemSelect();
        }

        function OnClickDeleteRowCheque() {
            if (objDwCheque.RowCount() > 0) {
                var currentRow = Gcoop.GetEl("HdDwChequeRow").value;
                var checkNo = "";
                try {
                    checkNo = Gcoop.Trim(objDwCheque.GetItem(currentRow, "cheque_no"));
                } catch (err) { }
                var confirmText = checkNo == "" || checkNo == null ? "ยืนยันการลบแถวที่ " + currentRow : "ยืนยันการลบเช็คเลขที่ " + checkNo;
                if (confirm(confirmText)) {
                    postDeleteRowCheque();
                }
            } else {
                alert("ยังไม่มีการเพิ่มแถวสำหรับรายการเช็ค");
            }
        }

        function OnClickInsertRowCheque() {
            var deptwith_flag = objDwMain.GetItem(1, "deptwith_flag");
            if (deptwith_flag == "" || deptwith_flag == null) {
                alert("กรุณากรอกเลขบัญชีก่อน");
                return;
            }
            postInsertRowCheque();
        }

        function OnDwChequeClick(s, r, c) {
            Gcoop.CheckDw(s, r, c, "late_flag", 1, 0);
            if (r > 0) {
                Gcoop.GetEl("HdDwChequeRow").value = r + "";
            }
            if (c == "bank_name" || c == "branch_name") {
                var bankCode = "";
                try {
                    bankCode = s.GetItem(r, "bank_code");
                    if (bankCode == null) throw "";
                } catch (Err) { bankCode = ""; }
                Gcoop.OpenDlg(860, 600, "w_dlg_bank_and_branch.aspx", "?sheetRow=" + r + "&bankCodeQuery=" + bankCode);
            } else if (c == "late_flag") {
                var v = s.GetItem(r, c);
                var dayPass = Gcoop.ParseInt(Gcoop.GetEl("HdDayPassCheq").value);
                if (v == 1) {
                    s.SetItem(r, "day_float", dayPass + 1);
                    s.AcceptText();
                } else {
                    s.SetItem(r, "day_float", dayPass);
                    s.AcceptText();
                }
            }
        }

        function OnDwChequeItemChanged(s, r, c, v) {
            if (r > 0) {
                Gcoop.GetEl("HdDwChequeRow").value = r + "";
            }
            if (c == "cheque_no") {
                s.SetItem(r, c, v);
                s.AcceptText();
                postPost();
            } else if (c == "cheque_type") {
                s.SetItem(r, c, v);
                s.AcceptText();
                postChequeType();
            } else if (c == "bank_code") {
                s.SetItem(r, c, v);
                s.SetItem(r, "bank_name", "");
                s.SetItem(r, "branch_code", "");
                s.SetItem(r, "branch_name", "");
                s.AcceptText();
                postBankCode();
            } else if (c == "branch_code") {
                var bankName = "";
                try {
                    bankName = Gcoop.Trim(s.GetItem(r, "bank_name"));
                } catch (err) { bankName = ""; }
                if (bankName != "") {
                    s.SetItem(r, c, v);
                    s.SetItem(r, "branch_name", "");
                    s.AcceptText();
                    postBankBranchCode();
                } else {
                    alert("กรุณากรอกเลขที่ธนาคารก่อน!");
                }
            } else if (c == "cheque_amt2") {
                s.SetItem(r, c, v);
                s.AcceptText();
                postPost();
            } else if (c == "cheque_amt") {
                var oldValue = -1;
                var newValue = -1;
                try {
                    oldValue = Gcoop.ParseFloat(s.GetItem(r, c));
                } catch (err) { }
                try {
                    newValue = Gcoop.ParseFloat(v + "");
                } catch (err) { }
                var chequeTotal = 0;
                s.SetItem(r, c, v);
                s.AcceptText();
                for (i = 0; i < s.RowCount(); i++) {
                    chequeTotal += Gcoop.ParseFloat(s.GetItem(i + 1, "cheque_amt"));
                }
                if (objDwMain.GetItem(1, "deptwith_flag") != "/") {
                    objDwMain.SetItem(1, "deptslip_amt", chequeTotal);
                    objDwMain.SetItem(1, "deptslip_netamt", chequeTotal);
                    objDwMain.AcceptText();
                }

                s.SetItem(r, c, v);
                s.AcceptText();
                if (newValue != oldValue) {
                    //postPost();
                    //แก้ไขเรื่องกรณีเป็น เช็ค เมื่อทำการคำนวณดอกเบี้ย ให้เซ็ตค่า กลับที่ยอดเช็คใหม่ 23/06/2558
                    postTotalWidthFixed();
                }
                postTotalWidthFixed();
            }
        }

        function OnDwItemChange(s, r, c, v) {
            Gcoop.GetEl("HdRequireCalInt").value = "true";
            if (c == "prncslip_amt") {
                //var v = objDwItem.GetItem(r, c);
                s.SetItem(r, c, v);
                s.AcceptText();
                Gcoop.GetEl("HdItemSelectRow").value = r + "";
                var totalAmt = 0;
                for (i = 1; i <= objDwItem.RowCount(); i++) {
                    if (objDwItem.GetItem(i, "select_flag") == 1) {
                        totalAmt += Gcoop.ParseFloat(objDwItem.GetItem(i, "prncslip_amt"));
                    } else {
                    }
                }
                objDwMain.SetItem(1, "deptslip_amt", totalAmt);
                objDwMain.SetItem(1, "deptslip_netamt", totalAmt);
            }
            return 0;
        }

        // get deptslip value
        function GetDeptSlipNoFromDlg(deptSlipNo, deptSlipNetAmt) {
            objDwMain.SetItem(1, "refer_slipno", deptSlipNo);
            objDwMain.SetItem(1, "deptslip_netamt", deptSlipNetAmt);
            objDwMain.SetItem(1, "deptslip_amt", deptSlipNetAmt);

            objDwMain.AcceptText();
        }


        function OnDwItemClick(s, r, c) {
            Gcoop.CheckDw(s, r, c, "select_flag", 1, 0);
            if (c == "select_flag") {
                Gcoop.GetEl("HdRequireCalInt").value = "true";
                var v = objDwItem.GetItem(r, c);
                objDwItem.SetItem(r, c, v);
                objDwItem.AcceptText();
                Gcoop.GetEl("HdItemSelectRow").value = r + "";
                var totalAmt = 0;
                if (v == 1) {
                    objDwItem.SetItem(r, "prncslip_amt", objDwItem.GetItem(r, "prnc_bal"));
                    objDwItem.AcceptText();
                } else {
                    objDwItem.SetItem(r, "prncslip_amt", 0);
                    objDwItem.AcceptText();
                }
                for (i = 1; i <= objDwItem.RowCount(); i++) {
                    //alert(i);
                    //alert(objDwItem.GetItem(i, "select_flag") == 1);
                    if (objDwItem.GetItem(i, "select_flag") == 1) {
                        //alert(i);
                        totalAmt += objDwItem.GetItem(i, "prncslip_amt");
                        //alert(totalAmt);
                    } else {
                    }
                }
                objDwMain.SetItem(1, "deptslip_amt", totalAmt);
                objDwMain.SetItem(1, "deptslip_netamt", totalAmt);
            }
            return 0;
        }

        function OnDwMainClick(s, r, c) {
            Gcoop.CheckDw(s, r, c, "send_gov", 1, 0);
            if (c == "deptwith_flag") {
                var accNo = objDwMain.GetItem(1, "deptaccount_no");
                if (accNo != null && accNo != "") {
                    TryOpenDlgDeptWith();
                }
            }
            return 0;
        }

        function OnDwMainItemChanged(s, row, c, v) {
            if (c == "tofrom_accid") {
                s.SetItem(row, c, v);
                s.AcceptText();
                postchgremark();
            }
            if (c == "nobook_flag") {
                s.SetItem(row, c, v);
                s.AcceptText();
                Gcoop.GetEl("HdNoBook_flag").value = v;
            }
            if (c == "deptslip_date") {
                s.SetItem(row, c, v);
                s.AcceptText();
                postchgremark();
            }
            
            if (c == "deptformat") {
                // s.SetItem(1, "deptformat", v);

                objDwMain.SetItem(1, "deptformat", Gcoop.Trim(v));
                var coopid = s.GetItem(1, "deptcoop_id");
                var accNo = s.GetItem(1, "deptformat");
                s.AcceptText();
                NewAccountNo(coopid, accNo);
                return 0;
            } else if (c == "passbook") {
                s.SetItem(row, c, v);
                s.AcceptText();
                postpassbook();
                return 0;
            }else if (c == "deptwith_flag") {
                s.SetItem(1, "deptwith_flag", v);
                s.AcceptText();
                postDeptWith();
                return 0;
            } else if (c == "recppaytype_code") {
                if (s.GetItem(1, "deptwith_flag") == null || s.GetItem(1, "deptwith_flag") == "") {
                    alert("กรุณาเลือก \"ทำรายการ\" ก่อน");
                    return 2;
                }
                s.SetItem(row, c, v);
                s.AcceptText();
                postRecpPayTypeCode();
                return 0;
            } else if (c == "deptslip_amt" || c == "payfee_meth") {
                try {
                    var deptwithFlag = s.GetItem(1, "deptwith_flag");
                    var otherAmt = s.GetItem(1, "other_amt");
                    try {
                        otherAmt = Gcoop.ParseFloat(s.GetItem(1, "other_amt") + "");
                    } catch (err) {
                        otherAmt = 0;
                    }
                    otherAmt = Gcoop.IsNum(otherAmt) ? otherAmt : 0;
                    var valueAmt = 0;
                    try {
                        valueAmt = Gcoop.ParseFloat(v + "");
                    } catch (err) {
                        valueAmt = 0;
                    }
                    valueAmt = Gcoop.IsNum(valueAmt) ? valueAmt : 0;
                    var totalNet = valueAmt + otherAmt;
                    if (totalNet == 0) {
                        totalNet = parseFloat(v + "");
                    }
                    var flag = s.GetItem(1, "deptwith_flag");
                    if (flag == "+") {
                        s.SetItem(1, "deptslip_netamt", totalNet);
                    }
                    if (flag == "-") {
                        //s.SetItem(1, "deptslip_netamt", totalNet);
                        var withdrawable = s.GetItem(1, "withdrawable_amt");
                        if (totalNet > withdrawable) {

                            alert("ไม่สามารถทำรายการได้ เนื่องจาก ยอดทำรายการมากกว่า ยอดถอนได้");
                            s.SetItem(1, "deptslip_netamt", 0);
                            s.SetItem(1, "deptslip_amt", 0);
                            // JsNewClear();
                        }
                        else {
                            s.SetItem(1, "deptslip_netamt", totalNet);
                        }
                    }
                    if (flag == "/") {
                        s.SetItem(1, "deptslip_netamt", totalNet);
                    }

                    s.AcceptText();
                    if (true) {
                        s.SetItem(row, c, v);
                        s.AcceptText();
                        postTotalWidthFixed();
                    }
                    return 0;
                } catch (err) { return 0; }

            }
            //            else if (c == "payfee_meth") {
            //                var ohter = 0;
            //                var deptslip_amt = 0;

            //            }
            else {
                var deptAccountNo = s.GetItem(1, "deptformat");
                var deptWithFlag = s.GetItem(1, "deptwith_flag");
                var recpPayTypeCode = s.GetItem(1, "recppaytype_code");
                if (deptAccountNo == null || deptAccountNo == "") {
                    alert("กรุณากรอก \"เลขที่บัญชี\" ก่อน");
                    return 1;
                } else if (deptWithFlag == null || deptWithFlag == "") {
                    alert("กรุณาเลือก \"ทำรายการ\" ก่อน");
                    return 1;
                } else if (recpPayTypeCode == null || recpPayTypeCode == "") {
                    alert("กรุณาเลือก \"รายการ\" ก่อน");
                    return 1;
                }
                return 0;
            }

        }

        function OnItemChequeError(s, row, c, v) {
            if (c == "cheque_no" || c == "cheque_tdate") {
                return 2;
            }
            return 0;
        }

        function OnKeyUpEnd(e) {
            if (e.keyCode == "115") {
                TryOpenDlgDeptWith();
            } else if (e.keyCode == "123") {
            }
        }

        function ReDlgDeptWith(objDlg, itemType, recpPayType) {
            try {
                objDlg.close();
            } catch (Err) { alert("ErrWin"); }
            objDwMain.SetItem(1, "deptwith_flag", itemType);
            objDwMain.AcceptText();
            objDwMain.SetItem(1, "recppaytype_code", recpPayType);
            objDwMain.AcceptText();
            if (recpPayType == "DTO" || recpPayType == "WTO") {
                Gcoop.SetLastFocus("deptslip_date_tdate_0");
            }
            else if (recpPayType == "CHQ") {
                Gcoop.SetLastFocus("cheque_no_0");
                Gcoop.SetLastFocus("deptslip_date_tdate_0");
                //   objDwMain.SetItem(1, "deptslip_date_tdate", $('#ctl00_ContentPlace_Hf_nextdate').val());
                //   objDwMain.AcceptText();
            }
            else if (recpPayType == "WCT" || recpPayType == "WDT" || recpPayType == "CDT" || recpPayType == "CCT") {
                Gcoop.SetLastFocus("cheque_no_0");
                //    $('input[name=deptslip_date_tdate_0]').val($('#ctl00_ContentPlace_Hd_slipdate').val());
                objDwMain.SetItem(1, "deptslip_date_tdate", $('#ctl00_ContentPlace_Hf_nextdate').val());
                objDwMain.AcceptText();
            }

            else {
                Gcoop.SetLastFocus("deptslip_amt_0");
            }



            postDeptWith();
        }

        function SheetLoadComplete() {
            //alert(this.__doPostBack);
            if (Gcoop.GetEl("HdCheckApvAlert").value == "true") {
                var processId = Gcoop.GetEl("HdProcessId").value;
                var itemType = Gcoop.GetEl("HdItemType").value;
                var avpCode = Gcoop.GetEl("HdAvpCode").value;
                var avpAmt = Gcoop.GetEl("HdAvpAmt").value;
                Gcoop.OpenIFrame(240, 170, "w_iframe_dp_addapv_task.aspx", "?processId=" + processId + "&avpCode=" + avpCode + "&itemType=" + itemType + "&avpAmt=" + avpAmt);
                //Gcoop.OpenDlg(240, 170, "w_iframe_dp_addapv_task.aspx", "?processId=" + processId + "&avpCode=" + avpCode + "&itemType=" + itemType + "&avpAmt=" + avpAmt);

            }
            if (Gcoop.GetEl("HdIsPostBack").value != "true") {
                Gcoop.SetLastFocus("deptformat_0");
                Gcoop.Focus();
            }
            if (Gcoop.GetEl("HdNewAccountNo").value == "true") {
                var chec = Gcoop.GetEl("CheckBox1").checked;
                if (chec) {
                  //  TryOpenDlgDeptWith();
                }
            }
            //adding print finslip [new]
            var printFlag = "";
            try {
                printFlag = Gcoop.GetEl("HdPrintFlag").value;
            } catch (exc) {
                printFlag = "";
            }
            if (printFlag == "true") {
                var argPrintSlip = Gcoop.GetEl("HdPrintSlip").value;
                var argPrintBook = Gcoop.GetEl("HdPrintBook").value;

                if (argPrintBook != "") {
                    //PRINTBOOK-DOYS
                    Gcoop.OpenIFrame(900, 550, "w_dlg_dp_printbook.aspx", "?deptAccountNo=" + argPrintBook + "&from_dp_slip=true");
                }
            }
            if (Gcoop.GetEl("HdIsInsertCheque").value == "true") {
                try {
                    Gcoop.Focus("cheque_no_" + (objDwCheque.RowCount() - 1));
                } catch (err) { }
            }
        }

        function TryOpenDlgDeptWith() {
            try {
                var accNo = objDwMain.GetItem(1, "deptaccount_no");
                if (accNo == null || accNo == "") {
                    return;
                }
                var idw = objDwMain.GetItem(1, "deptwith_flag");
                if (idw == null || idw == "" || idw == undefined) {
                    idw = "";
                } else if (idw == "+") {
                    idw = "d";
                } else if (idw == "-") {
                    idw = "w";
                } else if (idw == "/") {
                    idw = "c";
                }
                objDwMain.SetItem(1, "remark", "");
                objDwMain.AcceptText();
                var acc_name = Gcoop.GetEl("HdAccName").value;

                //Gcoop.OpenDlg(600,450, "w_dlg_deptwith.aspx","?itemDeptWith=" + idw);
                Gcoop.OpenIFrame(600, 650, "w_dlg_deptwith.aspx", "?itemDeptWith=" + idw+"&accname="+acc_name);
                return;
            } catch (Err) {
                alert(Err);
            }
        }

        function Validate() {
            Gcoop.GetEl("hfSave").value = "true"
            if (objDwMain.GetItem(1, "tofrom_accid") == "") {
                alert("กรุณาเลือก \"รหัสคู่บัญชี\" ก่อนบันทึก");
                Gcoop.GetEl("hfSave").value = "false";
                return false;
            }
            if (Gcoop.GetEl("HdRequireCalInt").value == "true") {
                alert("กรุณากด \"คำนวนดอกเบี้ย\" ก่อนบันทึก");
                Gcoop.GetEl("hfSave").value = "false";
                return false;
            }
            if (objDwMain.GetItem(1, "depttype_code") == "01" && objDwMain.GetItem(1, "other_amt") > 0) {
                if (confirm("เดือนนี้มีการถอนแล้ว ไม่สามารถทำรายการได้ ต้องการถอนหรือไม่")) {

                    Gcoop.GetEl("hfSave").value = "true";
                } else {
                    Gcoop.GetEl("hfSave").value = "false";
                    return false;
                }
            }
            if (Gcoop.GetEl("hfSave").value = "true") {
                if (confirm("ยืนยันการบันทึกข้อมูล")) {
                    Gcoop.SetLastFocus("deptformat_0");
                    return true;
                }
            }
        }

        function setcursor() {
            Gcoop.SetLastFocus("deptformat_0");
            Gcoop.Focus();
            Gcoop.RemoveIFrame();
            return;
        }
        //beereceive
        function OnPostPrint() {
            PostPrint();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <div id="testDiv1">
    </div>    
    <table>
        <tr>
            <td>
                <div align="left" style="width:580px;display:none">
                     <asp:CheckBox ID="CheckBox1" runat="server" Font-Bold="True" Font-Names="Tahoma"
                    Font-Size="14px" Font-Underline="True" ForeColor="#3399FF" Text="เปิดวินโดว์หน้าทำรายการอัตโนมัติเมื่อคีย์เลขที่บัญชี"
                    AutoPostBack="True" Checked="True" OnCheckedChanged="CheckBox1_CheckedChanged" />
                </div>
            </td>
            <td>
            <%--beereceive--%>
                <div align="right" style="width:150px;display:none"">
                    <input type="button" value="พิมพ์ใบเสร็จ" style="width: 80px" onclick="OnPostPrint()" />
                </div>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel1" runat="server" TabIndex="1">
        <dw:WebDataWindowControl ID="DwListCoop" runat="server" DataWindowObject="d_dp_dept_cooplist"
            LibraryList="~/DataWindow/ap_deposit/cm_constant_config.pbl" ClientScriptable="True"
            AutoSaveDataCacheAfterRetrieve="True" AutoRestoreDataCache="True" ClientEventItemChanged="OnDwListCoopItemChanged"
            ClientEventItemError="OnError" AutoRestoreContext="False" ClientEventClicked="OnDwListCoopClick"
            ClientFormatting="True" Visible="False">
        </dw:WebDataWindowControl>
        <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_dp_slip_master_usebkbal"
            LibraryList="~/DataWindow/ap_deposit/dp_slip.pbl" ClientEventButtonClicked="OnButtonClicked"
            ClientScriptable="True" AutoSaveDataCacheAfterRetrieve="True" AutoRestoreDataCache="True"
            ClientEventItemChanged="OnDwMainItemChanged" ClientEventItemError="OnError" AutoRestoreContext="False"
            ClientEventClicked="OnDwMainClick" ClientFormatting="True">
        </dw:WebDataWindowControl>
    </asp:Panel>
    <br />
    <asp:Label ID="Label7" runat="server" Text="รายการเช็ค" Font-Bold="True" Font-Names="Tahoma"
        Font-Size="14px" ForeColor="#0099CC" Font-Overline="False" Font-Underline="True" />
    &nbsp; <span onclick="OnClickInsertRowCheque()" style="cursor: pointer;">
        <asp:Label ID="LbInsert2" runat="server" Text="เพิ่มแถว" Font-Bold="False" Font-Names="Tahoma"
            Font-Size="14px" Font-Underline="True" ForeColor="#006600" /></span> &nbsp;&nbsp;
    <span onclick="OnClickDeleteRowCheque()" style="cursor: pointer;">
        <asp:Label ID="LbDel2" runat="server" Text="ลบแถว" Font-Bold="False" Font-Names="Tahoma"
            Font-Size="14px" Font-Underline="True" ForeColor="Red" /></span>
    <asp:Panel ID="Panel2" runat="server" TabIndex="2">
        <dw:WebDataWindowControl ID="DwCheque" runat="server" DataWindowObject="d_cheque_operate_external"
            LibraryList="~/DataWindow/ap_deposit/dp_slip.pbl" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
            TabIndex="500" ClientEventItemChanged="OnDwChequeItemChanged" ClientEventClicked="OnDwChequeClick"
            ClientFormatting="True">
        </dw:WebDataWindowControl>
    </asp:Panel>
    <br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="รายละเอียดต้นเงิน" Font-Bold="True" Font-Names="Tahoma"
        Font-Size="14px" ForeColor="#0099CC" Font-Overline="False" Font-Underline="True" />
    &nbsp; <span onclick="OnClickCalInt()" style="cursor: pointer;">
        <asp:Label ID="Label4" runat="server" Text="คำนวนดอกเบี้ย" Font-Bold="False" Font-Names="Tahoma"
            Font-Size="14px" Font-Underline="True" ForeColor="#006600" /></span>
    <asp:Panel ID="Panel3" runat="server" Width="740" ScrollBars="Auto">
        <dw:WebDataWindowControl ID="DwItem" runat="server" DataWindowObject="d_dp_slip_item"
            LibraryList="~/DataWindow/ap_deposit/dp_slip.pbl" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
            ClientEventItemChanged="OnDwItemChange" ClientEventClicked="OnDwItemClick" ClientFormatting="True"
            TabIndex="900">
        </dw:WebDataWindowControl>
    </asp:Panel>
    <asp:HiddenField ID="HdLastFocus" runat="server" />
    <asp:HiddenField ID="HdPrintBook" runat="server" />
    <asp:HiddenField ID="HdPrintSlip" runat="server" />
    <asp:HiddenField ID="HdDayPassCheq" runat="server" />
    <asp:HiddenField ID="HdItemSelectRow" runat="server" />
    <asp:HiddenField ID="HdMemberNo" runat="server" />
    <asp:HiddenField ID="HdNewAccountNo" runat="server" />
    <asp:HiddenField ID="HdPrintFlag" runat="server" Value="false" />
    <asp:HiddenField ID="HdRequireCalInt" runat="server" Value="false" />
    <asp:HiddenField ID="HdIsPostBack" runat="server" Value="false" />
    <asp:HiddenField ID="HdCheckApvAlert" runat="server" Value="false" />
    <asp:HiddenField ID="HdProcessId" runat="server" />
    <asp:HiddenField ID="HdAvpCode" runat="server" />
    <asp:HiddenField ID="HdItemType" runat="server" />
    <asp:HiddenField ID="HdAvpAmt" runat="server" />
    <asp:HiddenField ID="HdIsInsertCheque" runat="server" />
    <asp:HiddenField ID="HdDwChequeRow" runat="server" />
    <asp:HiddenField ID="hfSave" runat="server" />
    <asp:HiddenField ID="HfCoopid" runat="server" />
    <asp:HiddenField ID="HfCheck" runat="server" />
    <asp:HiddenField ID="Hdas_apvdoc" runat="server" />
    <asp:HiddenField ID="Hf_nextdate" runat="server" />
    <asp:HiddenField ID="Hd_slipdate" runat="server" />
    <asp:HiddenField ID="HdNoBook_flag" runat="server" Value="0" />
    <asp:HiddenField ID="HdAccName" runat="server" Value="" />
    <asp:HiddenField ID="HdPrintFin" runat="server" />
    <asp:HiddenField ID="Hdintreturn" runat="server" />
    <asp:HiddenField ID="Hdprncbal385" runat="server" />
    <asp:HiddenField ID="Hdprncbal475" runat="server" />
    <asp:HiddenField ID="HdPrintDep" runat="server" />
    <asp:HiddenField ID="HdDepno" runat="server" />
</asp:Content>
