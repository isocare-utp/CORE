<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_reqdeposit_namethaieng.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.w_sheet_dp_reqdeposit_namethaieng" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=postChangeDeptType%>
    <%=postMemberNo%>
    <%=postTotalAmt%>
    <%=postDeptAccountNo%>
    <%=postPost%>
    <%=postDwGainAddRow%>
    <%=postGainMemberNo%>
    <%=postDwGainDelRow%>
    <%=postInsertRowCheque%>
    <%=postDeleteRowCheque%>
    <%=postBankCode%>
    <%=postBankBranchCode%>
    <%=postSaveNoCheckApv%>
    <%=postPostOffice%>
    <%=postDeptPassbookNo%>
    <%=CheckCoop%>
    <%=postBankname%>
    <%=postAccountStatus%>
    <script type="text/javascript">
        var temp_seqno = 0;
        function CommitPrintFirstPage() {
            var deptAccountNo = Gcoop.GetEl("HdAccoutNo").value;
            Gcoop.OpenIFrame(900, 300, "w_dlg_dp_printbook.aspx", "?deptAccountNo=" + deptAccountNo);
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

        function GetMemberNoFromDlg(Coopid, memberNo) {
            objDwMain.SetItem(1, "member_no", memberNo);
            Gcoop.GetEl("HfCoopid").value = Coopid + "";
            objDwMain.AcceptText();
            postMemberNo();
        }

        function GetValueFromDlg(deptmem_id) {
            objDwMain.SetItem(1, "member_no", deptmem_id);
            objDwMain.AcceptText();
            postMemberNo();
        }

        function GetDeptSlipNoFromDlg(deptSlipNo, deptSlipNetAmt) {
            objDwMain.SetItem(1, "deptslip_no", deptSlipNo);
            objDwMain.SetItem(1, "request_tranamt", deptSlipNetAmt);

            // set เงินสด/โอนภายนอก เปิดบัญชีให้เป็น 0 deptrequest_amt
            objDwMain.SetItem(1, "deptrequest_amt", 0.0);

            SumAllTotal();
            objDwMain.AcceptText();
        }

        function GetValueCheckApv(valueCheckApv, nameApv) {
            if (valueCheckApv == "success") {
                //           objDwMain.SetItem(1, "entry_id", nameApv);
                //           objDwMain.AcceptText();
                postSaveNoCheckApv();
            }
            else {
                Gcoop.RemoveIFrame();
            }
        }

        function MenubarNew() {
            window.location = Gcoop.GetUrl() + "Applications/ap_deposit/w_sheet_dp_reqdeposit.aspx";
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
            var member_no = objDwMain.GetItem(1, "member_no");
            if (member_no == "" || member_no == "CIF") {
                alert("กรุณากรอกเลขสมาชิกก่อน");
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
                SumAllTotal();
                if (newValue != oldValue) {
                    //postPost();
                }
            }
        }

        //        function OnDwDeptMonthItemChanged(s, r, c, v) {
        //            objDwMain.SetItem(1, c, v);
        //            objDwMain.AcceptText();
        //            if (c == "deptmonth_status") {
        //                if (v == 0) {
        //                    objDwMain.SetItem(1, "deptmonth_amt", 0);
        //                    s.SetItem(1, "deptmonth_amt", 0);
        //                    s.AcceptText();
        //                }
        //                s.SetItem(r, c, v);
        //                s.AcceptText();
        //                postPost();
        //            }
        //            return 0;
        //        }

        function OnDwGainClicked(s, r, c) {
            for (i = 1; i <= s.RowCount(); i++) {
                s.SelectRow(i, false);
            }
            s.SelectRow(r, true);
            s.SetRow(r);
            Gcoop.GetEl("HdDwGainCurrentRow").value = r + "";
            return 0;
        }

        function OnDwGainItemChanged(s, r, c, v) {
            Gcoop.GetEl("HdDwGainCurrentRow").value = r + "";
            if (c == "ref_id") {
                s.SetItem(r, c, v);
                s.AcceptText();
                postGainMemberNo();
            } else if (c == "ref_type") {
                if (v == 2) {
                    //alert(v);
                    s.SetItem(r, "name", "");
                    s.AcceptText();
                    s.SetItem(r, "ref_id", "");
                    s.AcceptText();
                    s.SetItem(r, "surname", "");
                    s.AcceptText();
                    s.SetItem(r, "prename_code", "");
                    s.AcceptText();
                    s.SetItem(r, "house_no", "");
                    s.AcceptText();
                    s.SetItem(r, "group_no", "");
                    s.AcceptText();
                    s.SetItem(r, "soi", "");
                    s.AcceptText();
                    s.SetItem(r, "tumbol", "");
                    s.AcceptText();
                    s.SetItem(r, "district", "");
                    s.AcceptText();
                    s.SetItem(r, "province", "");
                    s.AcceptText();
                    s.SetItem(r, "phone_no", "");
                    s.AcceptText();
                    s.SetItem(r, "post_code", "");
                    s.AcceptText();
                    s.SetItem(r, "road", "");
                    s.AcceptText();
                }
                s.SetItem(r, c, v);
                s.AcceptText();
                postPost();
            }
            return 0;
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
                objDwMain.SetItem(r, "memcoop_id", coopid);
                objDwMain.AcceptText();
            }
        }
        function OnDwMainClick(s, r, c) {

            if (c == "member_flag") {
                Gcoop.CheckDw(s, r, c, "member_flag", 1, 0);

            }

        }

        function OnDwMainItemChanged(s, r, c, v) {

            if (c == "tofdept_select") {
                s.SetItem(r, c, v);
                s.AcceptText();
                postChangeDeptType();
            }
            else if (c == "account_status") {
                s.SetItem(r, c, v);
                s.AcceptText();
                Gcoop.SetLastFocus("deptaccount_no_0");
                Gcoop.Focus();
                postAccountStatus();
              
                
            }
            else if (c == "deptmonth_status") {
                if (v == 0) {
                    objDwMain.SetItem(1, "deptmonth_amt", 0);
                    s.SetItem(1, "deptmonth_amt", 0);
                    s.AcceptText();
                }
                s.SetItem(r, c, v);
                s.AcceptText();
                postPost();
            }
            else if (c == "acctype_select") {
                s.SetItem(r, c, v);
                s.AcceptText();
                alert("กรุณาเลือกประเภทบัญชี..");
            }
            else if (c == "member_no") {
                s.SetItem(r, c, v);
                s.AcceptText();
                postMemberNo();
            } else if (c == "deptrequest_amt") {
                s.SetItem(r, c, v);
                s.AcceptText();
                SumAllTotal();
            } else if (c == "request_tranamt") {
                s.SetItem(r, c, v);
                s.AcceptText();
                SumAllTotal();
            } else if (c == "monthintpay_meth") {
                var monthIntPay1 = s.GetItem(r, c);
                s.SetItem(1, "tran_deptacc_no", "");
                s.SetItem(1, "tran_bankacc_no", "");
                s.SetItem(1, "dept_tranacc_name", "");
                s.SetItem(r, c, v);
                var monthIntPay = s.GetItem(r, c);
                if (monthIntPay == 3) {
                    var deptType = s.GetItem(r, "depttype_select");
                    if (deptType == "10" || deptType == "11") {
                        alert("รายการนี้แยกได้เฉพาะ ประจำเท่านั้น");
                        //setTimeout("objDwMain.SetItem("+r+", '"+c+"', '"+monthIntPay1+"')", 500);
                    }
                    else {
                        s.AcceptText();
                        postPost();
                    }
                }
                else {
                    s.AcceptText();
                    postPost();
                }
            } else if (c == "tran_deptacc_no") {
                var monthIntPay = s.GetItem(r, "monthintpay_meth");
                if (monthIntPay == 5) {
                    s.SetItem(r, c, v);
                    s.AcceptText();
                    postPostOffice();
                }
                else if (monthIntPay == 2) {
                    s.SetItem(r, c, v);
                    s.AcceptText();
                    // postDeptAccountNo();
                }
                else {
                    s.SetItem(r, c, v);
                    s.AcceptText();
                    postDeptAccountNo();
                }
            } else if (c == "bank_code") {
                s.SetItem(r, c, v);
                s.AcceptText();
                s.SetItem(r, "bank_branch", "");
                s.AcceptText();
                postPost();
            } else if (c == "recppaytype_code") {
                s.SetItem(r, c, v);
                s.SetItem(r, "tofrom_accid", "");
                s.AcceptText();
                postPost();

            } else if (c == "deptpassbook_no") {
                s.SetItem(r, c, v);
                s.AcceptText();
                postDeptPassbookNo();
            }
            else if (c == "tran_bankacc_no") {
                s.SetItem(r, "tran_deptacc_no", v);
                s.SetItem(r, c, v);
                s.AcceptText();
                postBankname();
            }


            return 0;
        }

        //
        function OnDwGainButtonClicked(sender, rowNumber, buttonName) {
            if (buttonName == "b_codep") {
                temp_seqno = rowNumber;
                Gcoop.OpenIFrame(600, 500, "w_dlg_dp_reqdetail.aspx", "?seq_no=" + objDwGain.GetItem(rowNumber, "seq_no"));
            }
        }

        function GetPersonal(ref_id, prename_code, name, surname, seq_no, house_no, group_no, soi, road, tumbol, district, province, post_code, phone_no, fax_no, coop_id) {
            objDwGain.SetItem(temp_seqno, "ref_id", ref_id);
            objDwGain.SetItem(temp_seqno, "prename_code", prename_code);
            objDwGain.SetItem(temp_seqno, "name", name);
            objDwGain.SetItem(temp_seqno, "surname", surname);
            objDwGain.SetItem(temp_seqno, "seq_no", seq_no);
            objDwGain.SetItem(temp_seqno, "house_no", house_no);
            objDwGain.SetItem(temp_seqno, "group_no", group_no);
            objDwGain.SetItem(temp_seqno, "soi", soi);
            objDwGain.SetItem(temp_seqno, "road", road);
            objDwGain.SetItem(temp_seqno, "tumbol", tumbol);
            objDwGain.SetItem(temp_seqno, "district", district);
            objDwGain.SetItem(temp_seqno, "province", province);
            objDwGain.SetItem(temp_seqno, "post_code", post_code);
            objDwGain.SetItem(temp_seqno, "phone_no", phone_no);
            objDwGain.SetItem(temp_seqno, "fax_no", fax_no);
            objDwGain.SetItem(temp_seqno, "coop_id", coop_id);
            objDwGain.AcceptText();
            postPost();
            //  postMemberNo();
        }

        function OnDwMainButtonClicked(sender, rowNumber, buttonName) {
            if (buttonName == "b_member") {
                //                Gcoop.OpenDlg(530, 500, "w_dlg_dp_member_search.aspx", "?coopid=" + objDwMain.GetItem(1, "memcoop_id"));
                //                var deptType = "";
                //                try {
                //                    deptType = objDwMain.GetItem(rowNumber, "depttype_select");
                //                } catch (err) { deptType = ""; }

                //                if (deptType != "" && deptType != null) {
                var memberFlag = objDwMain.GetItem(rowNumber, "member_flag");
                if (memberFlag == 1) {
                    Gcoop.OpenDlg(530, 500, "w_dlg_dp_member_search.aspx", "?coopid=" + objDwMain.GetItem(1, "memcoop_id"));
                }
                else {
                    Gcoop.OpenDlg(530, 500, "w_dlg_dp_extmember_search.aspx", "");
                }
                //                }
            }

            else if (buttonName == "b_lastdept") {
                Gcoop.OpenDlg(485, 489, "w_dlg_dp_current_account_no.aspx", "");
            }

            else if (buttonName == "cb_referslip") {
                var recpPayType = objDwMain.GetItem(rowNumber, "recppaytype_code");
                if (recpPayType == "OTR") {
                    Gcoop.OpenDlg(650, 600, "w_dlg_dp_slip_search_tran.aspx", "?member_no=" + objDwMain.GetItem(1, "member_no"));
                }
            }
            else if (buttonName == "b_search") {
                var memberFlag = objDwMain.GetItem(rowNumber, "member_flag");
                if (memberFlag == 1) {
                    Gcoop.OpenDlg(550, 590, "w_dlg_dp_deptaccount_search.aspx", "?member_no=" + objDwMain.GetItem(1, "member_no"));
                } else {
                    Gcoop.OpenDlg(580, 590, "w_dlg_dp_deptaccount_search.aspx", "");
                }
            }

        }

        function SheetLoadComplete() {
            if (Gcoop.GetEl("HdIsInsertCheque").value == "true") {
                try {
                    Gcoop.Focus("cheque_no_" + (objDwCheque.RowCount() - 1));
                } catch (err) { }
            }
            if (Gcoop.GetEl("HdCheckApvAlert").value == "true") {
                var processId = Gcoop.GetEl("HdProcessId").value;
                var itemType = Gcoop.GetEl("HdItemType").value;
                var avpCode = Gcoop.GetEl("HdAvpCode").value;
                var avpAmt = Gcoop.GetEl("HdAvpAmt").value;
                Gcoop.OpenIFrame(240, 170, "w_iframe_dp_addapv_task.aspx", "?processId=" + processId + "&avpCode=" + avpCode + "&itemType=" + itemType + "&avpAmt=" + avpAmt);
            }
            if (Gcoop.GetEl("HdSaveAccept").value == "true") {
                var deptAccountNo = Gcoop.GetEl("HdAccoutNo").value;
                var deptPassBookNo = Gcoop.GetEl("HdPassBookNo").value;
                Gcoop.OpenIFrame(450, 150, "w_iframe_dp_printfirstpage.aspx", "?deptAccountNo=" + deptAccountNo + "&deptPassBookNo=" + deptPassBookNo);
            }
            if (Gcoop.GetEl("HdIsPostBack").value != "true") {
                Gcoop.SetLastFocus("member_no_0");
                Gcoop.Focus();
            }
        }

        function SumAllTotal() {
            var inAcc = 0;
            var inCash = 0;
            var inCheque = 0;
            var inTotal = 0;
            try {
                inAcc = Gcoop.ParseFloat(objDwMain.GetItem(1, "request_tranamt") + "");
            } catch (err) { }
            try {
                inCash = Gcoop.ParseFloat(objDwMain.GetItem(1, "deptrequest_amt") + "");
            } catch (err) { }
            inTotal = inAcc + inCash;
            try {
                if (objDwCheque.RowCount() > 0) {
                    var inCheque2 = 0;
                    for (i = 1; i <= objDwCheque.RowCount(); i++) {
                        try {
                            inCheque2 += Gcoop.ParseFloat(objDwCheque.GetItem(i, "cheque_amt") + "");
                            inCash = 0;
                        } catch (err2) { }
                    }
                    inCheque = Gcoop.ParseFloat(inCheque2 + "");
                    inTotal = inCheque;
                    objDwMain.SetItem(1, "deptrequest_amt", 0);
                }
            } catch (err) {
                inCheque = 0;
            }

            inTotal = inAcc + inCash + inCheque;
            // var inTotal
            objDwMain.SetItem(1, "deptreq_sumamt", inTotal);
            // objDwMain.SetItem(1, "deptrequest_amt", inTotal);

            objDwMain.AcceptText();
        }

        function GetDeptNoFromDlg(deptaccount_no) {
            objDwMain.SetItem(1, "tran_deptacc_no", deptaccount_no);
            objDwMain.AcceptText();
            Gcoop.GetEl("HfDeptaccount").value = deptaccount_no;
            postDeptAccountNo();
        }

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล?");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:Panel ID="Panel1" runat="server" Width="680px">
        <dw:WebDataWindowControl ID="DwListCoop" runat="server" DataWindowObject="d_dp_dept_cooplist"
            LibraryList="~/DataWindow/ap_deposit/cm_constant_config.pbl" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
            ClientEventItemChanged="OnDwListCoopItemChanged" ClientFormatting="True" TabIndex="1"
            ClientEventClicked="OnDwListCoopClick" Visible="False">
        </dw:WebDataWindowControl>
        <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_dp_reqdepoist_namethaieng"
            LibraryList="~/DataWindow/ap_deposit/dp_reqdeposit.pbl" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
            ClientEventItemChanged="OnDwMainItemChanged" ClientFormatting="True" TabIndex="1"
            ClientEventButtonClicked="OnDwMainButtonClicked" ClientEventClicked="OnDwMainClick">
        </dw:WebDataWindowControl>
    </asp:Panel>
    <br />
    <table>
        <tr>
            <td valign="top">
                <asp:Label ID="LbGain1" runat="server" Text="ผู้ฝากร่วม" Font-Bold="True" Font-Names="Tahoma"
                    Font-Size="14px" ForeColor="#0099CC" Font-Overline="False" Font-Underline="True" />
                &nbsp;&nbsp; <span onclick="postDwGainAddRow()" style="cursor: pointer;">
                    <asp:Label ID="LbGain2" runat="server" Text="เพิ่มแถว" Font-Bold="False" Font-Names="Tahoma"
                        Font-Size="14px" Font-Underline="True" ForeColor="#006600" /></span> &nbsp;&nbsp;
                <span onclick="postDwGainDelRow()" style="cursor: pointer;">
                    <asp:Label ID="Label1" runat="server" Text="ลบแถว" Font-Bold="False" Font-Names="Tahoma"
                        Font-Size="14px" Font-Underline="True" ForeColor="Red" /></span>
                <dw:WebDataWindowControl ID="DwGain" runat="server" DataWindowObject="d_dp_reqcodeposit"
                    LibraryList="~/DataWindow/ap_deposit/dp_reqdeposit.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="OnDwGainItemChanged"
                    ClientFormatting="True" ClientScriptable="True" ClientEventClicked="OnDwGainClicked"
                    ClientEventButtonClicked="OnDwGainButtonClicked" TabIndex="600">
                </dw:WebDataWindowControl>
            </td>
            <td valign="top">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label2" runat="server" Text="ส่งฝากรายงวด" Font-Bold="True" Font-Names="Tahoma"
                    Font-Size="14px" ForeColor="#0099CC" Font-Overline="False" Font-Underline="True" />
                <%--<dw:WebDataWindowControl ID="DwDeptMonth" runat="server" DataWindowObject="d_dp_reqdepoist_main_other"
                    LibraryList="~/DataWindow/ap_deposit/dp_reqdeposit.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    ClientFormatting="True" ClientEventItemChanged="OnDwDeptMonthItemChanged">
                </dw:WebDataWindowControl>--%>
            </td>
        </tr>
    </table>
    <asp:Label ID="Label7" runat="server" Text="รายการเช็ค" Font-Bold="True" Font-Names="Tahoma"
        Font-Size="14px" ForeColor="#0099CC" Font-Overline="False" Font-Underline="True" />
    &nbsp; <span onclick="OnClickInsertRowCheque()" style="cursor: pointer;">
        <asp:Label ID="LbInsert2" runat="server" Text="เพิ่มแถว" Font-Bold="False" Font-Names="Tahoma"
            Font-Size="14px" Font-Underline="True" ForeColor="#006600" /></span> &nbsp;&nbsp;
    <span onclick="OnClickDeleteRowCheque()" style="cursor: pointer;">
        <asp:Label ID="LbDel2" runat="server" Text="ลบแถว" Font-Bold="False" Font-Names="Tahoma"
            Font-Size="14px" Font-Underline="True" ForeColor="Red" /></span>
    <dw:WebDataWindowControl ID="DwCheque" runat="server" DataWindowObject="d_cheque_operate_external"
        LibraryList="~/DataWindow/ap_deposit/dp_reqdeposit.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
        ClientScriptable="True" ClientEventClicked="OnDwChequeClick" ClientEventItemChanged="OnDwChequeItemChanged"
        TabIndex="900">
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="HdDayPassCheq" runat="server" />
    <asp:HiddenField ID="HdDwGainCurrentRow" runat="server" />
    <asp:HiddenField ID="HdIsInsertCheque" runat="server" />
    <asp:HiddenField ID="HdDwChequeRow" runat="server" />
    <asp:HiddenField ID="HdCheckApvAlert" runat="server" />
    <asp:HiddenField ID="HdProcessId" runat="server" />
    <asp:HiddenField ID="HdAvpCode" runat="server" />
    <asp:HiddenField ID="HdItemType" runat="server" />
    <asp:HiddenField ID="HdAvpAmt" runat="server" />
    <asp:HiddenField ID="HdSaveAccept" runat="server" />
    <asp:HiddenField ID="HdAccoutNo" runat="server" />
    <asp:HiddenField ID="HdPassBookNo" runat="server" />
    <asp:HiddenField ID="HdDueDate" runat="server" />
    <asp:HiddenField ID="HfCoopid" runat="server" />
    <asp:HiddenField ID="Hficoop" runat="server" />
    <asp:HiddenField ID="HfMemno" runat="server" />
    <asp:HiddenField ID="HfReset" runat="server" />
    <asp:HiddenField ID="HdIsPostBack" runat="server" />
    <asp:HiddenField ID="HfDeptaccount" runat="server" />
    <asp:HiddenField ID="Hfbooktype" runat="server" />
    <asp:HiddenField ID="Hdas_apvdoc" runat="server" />
</asp:Content>
