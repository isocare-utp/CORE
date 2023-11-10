<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_as_request.aspx.cs" Inherits="Saving.Applications.assist.ws_as_request_ctrl.ws_as_request" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsEducation.ascx" TagName="DsEducation" TagPrefix="uc2" %>
<%@ Register Src="DsDecease.ascx" TagName="DsDecease" TagPrefix="uc3" %>
<%@ Register Src="DsFamilydecease.ascx" TagName="DsFamilydecease" TagPrefix="uc4" %>
<%@ Register Src="DsDisaster.ascx" TagName="DsDisaster" TagPrefix="uc5" %>
<%@ Register Src="DsPatronize.ascx" TagName="DsPatronize" TagPrefix="uc6" %>
<%@ Register Src="DsMedical.ascx" TagName="DsMedical" TagPrefix="uc7" %>
<%@ Register Src="DsGain.ascx" TagName="DsGain" TagPrefix="uc8" %>
<%@ Register Src="DsAmount.ascx" TagName="DsAmount" TagPrefix="uc9" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc10" %>
<%@ Register Src="DsBonus.ascx" TagName="DsBonus" TagPrefix="uc11" %>
<%@ Register Src="DsBonus_instead.ascx" TagName="DsBonus_instead" TagPrefix="uc12" %>
<%@ Register Src="DsBonus_address.ascx" TagName="DsBonus_address" TagPrefix="uc13" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        //ประกาศตัวแปร ควรอยู่บริเวณบนสุดใน tag <script>
        var dsMain = new DataSourceTool();
        var dsDecease = new DataSourceTool();
        var dsFamilydecease = new DataSourceTool();
        var dsEducation = new DataSourceTool();
        var dsDisaster = new DataSourceTool();
        var dsPatronize = new DataSourceTool();
        var dsGain = new DataSourceTool();
        var dsAmount = new DataSourceTool();
        var dsList = new DataSourceTool();
        var dsBonus = new DataSourceTool();
        var dsBonus_instead = new DataSourceTool();
        var dsBonus_address = new DataSourceTool();
        //เช็คบัตรประชาชน//////////////////
        function checkID(id) {
            if (id.length != 13) return false;
//            for (i = 0, sum = 0; i < 12; i++) {
//                sum += parseFloat(id.charAt(i)) * (13 - i); if ((11 - sum % 11) % 10 != parseFloat(id.charAt(12)))
//                return false; return true;

//                    
//            } 
            return true;
        }

        function checkForm() {
            if (!checkID(dsFamilydecease.GetItem(0, "card_person"))) {
                alert("รหัสบัตรประชาชนไม่ถูกต้อง");
                dsFamilydecease.SetItem(0, "card_person", "");
                return;
            }
        }

        function MenubarOpen() {
            Gcoop.OpenIFrame2(750, 600, 'wd_as_search_request_old.aspx', '')
        }
        ////////////////////////////////

        //ประกาศฟังก์ชันสำหรับ event ItemChanged
        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                var ls_memberno = dsMain.GetItem(0, "member_no");
                Gcoop.GetEl("HdMbChkmemdate").value = v;

                dsMain.SetItem(0, "member_no", ls_memberno);
                PostMemberNo();
            }
            else if (c == "assisttype_code") {
                PostAssistType();       
            } else if (c == "assist_year") {
                PostAssistYear();
            } else if (c == "calage_date") {
                PostCalage();
            }
        }

        function OnDsBonus_insteadItemChanged(s, r, c, v) {
            if (c == "member_no_ref") {
                Gcoop.GetEl("Hd_row").value = r;
                Gcoop.GetEl("HdMbChkmemdate").value = v;
                Postmembname_ref();
            }
            else if (c == "bonus_type") {
            JsPostunitdeatil();
            }
        }

        //ประกาศฟังก์ชันสำหรับ event Clicked
        function OnDsMainClicked(s, r, c) {
            if (c == "b_search") {
                Gcoop.OpenIFrame2(650, 600, 'wd_as_member_search.aspx', '')
            }
            else if (c == "b_add") {
                var ass_docno = dsMain.GetItem(0, "assist_docno");
                if (ass_docno != null) {
                    Gcoop.OpenIFrameUploadImg("", "001", "assist_docno", ass_docno);
                } else if (Gcoop.GetEl("HdTokenIMG").value != "") {
                    Gcoop.OpenIFrameUploadImg(Gcoop.GetEl("HdTokenIMG").value, "001", "", "");
                }
//                var token = dsMain.GetItem(0, "tokenmain");
////                alert(token);
//                if (ass_docno == "" || ass_docno == null) {
//                    Gcoop.OpenIFrameUploadImg(token, "001", "", "");
//                }
//                else {
//                    Gcoop.OpenIFrameUploadImg("", "001", "assist_docno", ass_docno);
//                }
            }
        }
        function OnDsBonus_insteadClicked(s, r, c) {
            if (c == "b_search") {
                Gcoop.GetEl("Hd_membno").value = 1;
                Gcoop.GetEl("Hd_row").value = r;
                Gcoop.OpenIFrame2(650, 600, 'wd_as_member_search.aspx', '')
            }
            if (c == "b_del") {
                Jspostdel();
            }
        }

        function OnDsBonus_addressClicked(s, r, c) {
            if (c == "b_linkaddress") {
                PostLinkAddress();
            }
        }

        function OnDsListClicked(s, r, c) {
            var ls_assreqno = dsList.GetItem(r, "assistreq_docno");
            Gcoop.GetEl("hdAssreqno").value = ls_assreqno;
            InitHistory();
        }
        

        function OnDsAmountItemChanged(s, r, c, v) {
            if (c == "expense_bank") {
                var ls_expensebank = dsAmount.GetItem(0, "expense_bank");
                dsAmount.SetItem(0, "expense_bank", ls_expensebank);
                dsAmount.SetItem(0, "expense_branch", "");
                PostRetriveBankBranch();
            } else if (c == "moneytype_code") {
                var ls_montype = dsAmount.GetItem(0, "moneytype_code");
                if (ls_montype == "CSH") {
                    dsAmount.SetItem(0, "expense_bank", "");
                    dsAmount.SetItem(0, "expense_accid", "");
                    dsAmount.SetItem(0, "expense_branch", "");
                    dsAmount.SetItem(0, "send_system", "");
                    dsAmount.SetItem(0, "deptaccount_no", "");
                    dsAmount.GetElement(0, "expense_bank").disabled = true;
                    dsAmount.GetElement(0, "expense_accid").disabled = true;
                    dsAmount.GetElement(0, "send_system").disabled = true;
                    dsAmount.GetElement(0, "expense_branch").disabled = true;
                    dsAmount.GetElement(0, "deptaccount_no").disabled = true;
                    dsAmount.GetElement(0, "expense_bank").style.background = "#CCCCCC";
                    dsAmount.GetElement(0, "expense_accid").style.background = "#CCCCCC";
                    dsAmount.GetElement(0, "expense_branch").style.background = "#CCCCCC";
                    dsAmount.GetElement(0, "send_system").style.background = "#CCCCCC";
                    dsAmount.GetElement(0, "deptaccount_no").style.background = "#CCCCCC";
                } else if (ls_montype == "TRN") {
                    dsAmount.SetItem(0, "expense_bank", "");
                    dsAmount.SetItem(0, "expense_accid", "");
                    dsAmount.SetItem(0, "expense_branch", "");
                    dsAmount.SetItem(0, "send_system", "");
                    dsAmount.SetItem(0, "deptaccount_no", "");
                    dsAmount.GetElement(0, "expense_bank").disabled = true;
                    dsAmount.GetElement(0, "expense_accid").disabled = true;
                    dsAmount.GetElement(0, "expense_branch").disabled = true;
                    dsAmount.GetElement(0, "send_system").disabled = false;
                    dsAmount.GetElement(0, "deptaccount_no").disabled = true;
                    dsAmount.GetElement(0, "expense_bank").style.background = "#CCCCCC";
                    dsAmount.GetElement(0, "expense_accid").style.background = "#CCCCCC";
                    dsAmount.GetElement(0, "expense_branch").style.background = "#CCCCCC";
                    dsAmount.GetElement(0, "send_system").style.background = "#FFFFFF";
                    dsAmount.GetElement(0, "deptaccount_no").style.background = "#CCCCCC";
                } else {
                    dsAmount.SetItem(0, "send_system", "");
                    dsAmount.SetItem(0, "deptaccount_no", "");
                    dsAmount.GetElement(0, "expense_bank").disabled = false;
                    dsAmount.GetElement(0, "expense_accid").disabled = false;
                    dsAmount.GetElement(0, "expense_branch").disabled = false;
                    dsAmount.GetElement(0, "send_system").disabled = true;
                    dsAmount.GetElement(0, "deptaccount_no").disabled = true;
                    dsAmount.GetElement(0, "expense_bank").style.background = "#FFFFFF";
                    dsAmount.GetElement(0, "expense_accid").style.background = "#FFFFFF";
                    dsAmount.GetElement(0, "expense_branch").style.background = "#FFFFFF";
                    dsAmount.GetElement(0, "send_system").style.background = "#CCCCCC";
                    dsAmount.GetElement(0, "deptaccount_no").style.background = "#CCCCCC";
                }
            } else if (c == "send_system") {
                var ls_sendsys = dsAmount.GetItem(0, "send_system");
                if (ls_sendsys == "DEP") {
                    dsAmount.SetItem(0, "deptaccount_no", "");
                    dsAmount.GetElement(0, "deptaccount_no").disabled = false;
                    dsAmount.GetElement(0, "deptaccount_no").style.background = "#FFFFFF";
                } else if (ls_sendsys == "LON") {
                    dsAmount.SetItem(0, "deptaccount_no", "");
                    dsAmount.GetElement(0, "deptaccount_no").disabled = true;
                    dsAmount.GetElement(0, "deptaccount_no").style.background = "#CCCCCC";
                }
                else {
                    dsAmount.SetItem(0, "deptaccount_no", "");
                    dsAmount.GetElement(0, "deptaccount_no").disabled = true;
                    dsAmount.GetElement(0, "deptaccount_no").style.background = "#CCCCCC";
                }

            }
        }

        function GetMembNoFromDlg(memberno) {
            var memb_no = Gcoop.GetEl("Hd_membno").value;
            var r = Gcoop.GetEl("Hd_row").value;
            if (memb_no == "1") {
                dsBonus_instead.SetItem(r, "member_no_ref", memberno.trim());
                Postmembname_ref();
            }
            else {
                dsMain.SetItem(0, "member_no", memberno.trim());
                PostMemberNo();
            }

        }

        function GetMembNoFromDlgReqOld(memberno, reqdoc_no, assisttype_code) {
            dsMain.SetItem(0, "member_no", memberno.trim());
            dsMain.SetItem(0, "assist_docno", reqdoc_no.trim());
            dsMain.SetItem(0, "assisttype_code", assisttype_code.trim());
                PostReqOld();
            }


        function Validate() {

            var err_text = "";

            if (Gcoop.GetEl("hdassgrp").value == "01") {
                var ass_rcvcardid = dsEducation.GetItem(0, "ass_rcvcardid");
                var ass_rcvname = dsEducation.GetItem(0, "ass_rcvname");
                if (ass_rcvcardid == "" || ass_rcvcardid == null) {
                    err_text += "กรุณากรอกเลขที่บัตรบุตร ";
                }

                if (ass_rcvname == "" || ass_rcvname == null) {
                    err_text += "กรุณากรอกชื่อสมาชิกครอบครัว";
                }
            }
            else if (Gcoop.GetEl("hdassgrp").value == "02") {
                var dec_deaddate = dsDecease.GetItem(0, "dec_deaddate");
                if (dec_deaddate == "" || dec_deaddate == null || dec_deaddate == "1500-01-01 00:00:00" ) {
                    err_text += "กรุณากรอกวันที่ถึงแก่กรรม";
                }
            }
            else if (Gcoop.GetEl("hdassgrp").value == "03") {
                var fam_docdate = dsFamilydecease.GetItem(0, "fam_docdate");
                var ass_rcvname = dsFamilydecease.GetItem(0, "ass_rcvname");
                var ass_rcvcardid = dsFamilydecease.GetItem(0, "ass_rcvcardid");
                if (fam_docdate == "" || fam_docdate == null || fam_docdate == "1500-01-01 00:00:00" ) {
                    err_text += "กรุณากรอกวันที่ตามเอกสารราชการ ";
                }

                if (ass_rcvname == "" || ass_rcvname == null) {
                    err_text += "กรุณากรอกชื่อสมาชิกครอบครัว ";
                }

                if (ass_rcvcardid == "" || ass_rcvcardid == null) {
                    err_text += "กรุณากรอกเลขที่บัตรประชาชน ";
                }
            }
            else if (Gcoop.GetEl("hdassgrp").value == "04") {
                var dis_disdate = dsDisaster.GetItem(0, "dis_disdate");
                if (dis_disdate == "" || dis_disdate == null || dis_disdate == "1500-01-01 00:00:00" ) {
                    err_text += "กรุณากรอกวันที่เริ่มประสบภัย ";
                }
            }
            else if (Gcoop.GetEl("hdassgrp").value == "05") {
                var fam_docdate = dsPatronize.GetItem(0, "fam_docdate");
                var coop_id = dsMain.GetItem(0, "coop_id");
                var assisttype_code = dsMain.GetItem(0, "assisttype_code");
                var dis_docdate = dsPatronize.GetItem(0, "dis_disdate");

                if (assisttype_code == "76" && coop_id == "050001")
                {
                    if ((fam_docdate == "" || fam_docdate == null || fam_docdate == "1500-01-01 00:00:00") && (dis_docdate == "" || dis_docdate == null || dis_docdate == "1500-01-01 00:00:00"))
                    {
                        err_text += "กรุณากรอกวันที่ตามเอกสาร / วันที่โอนกรรมสิทธิ์ ";
                    }
                }
                else{

                    if (fam_docdate == "" || fam_docdate == null || fam_docdate == "1500-01-01 00:00:00" ) {
                        err_text += "กรุณากรอกวันที่ตามเอกสาร ";
                    }
                }
            }
            else if (Gcoop.GetEl("hdassgrp").value == "06") {
            }
            else if (Gcoop.GetEl("hdassgrp").value == "07") {
            }





            ////////////////////////////////////////////
            if (Gcoop.GetEl("hdassgrp").value != "07") {
                var a = dsAmount.GetItem(0, "assist_amt");
                if (a <= 0)
                {
                    err_text += "ไม่พบยอดเงินตามสิทธิ์ กรุณาตรวจสอบ";
                }
            }

            if (err_text != "") {
                confirm(err_text);
                return false;
            }
            else {
                return confirm("ยืนยันการบันทึกข้อมูล");
            }

            
        }

        function OnDsEducationItemChanged(s, r, c, v) {
            if (c == "assistpay_code") {
                PostAssistPay();
            } else if (c == "edu_gpa") {
                if (v > 4 || v < 0 ) {
                    alert("ตรวจสอบเกรดเฉลี่ย");
                    dsEducation.SetItem(0, "edu_gpa", 0);
                    return;
                }

                if (Gcoop.GetEl("hdasscondition").value == "1") {
                    PostInitPermiss();
                }
            } else if (c == "edu_childbirthdate") {
                PostGetChildAge();
            } else if (c == "ass_rcvcardid") {

                var cardperson = dsMain.GetItem(0, "card_person");
                if (!checkID(v)) {
                    alert("รหัสบัตรประชาชนไม่ถูกต้อง");
                    dsEducation.SetItem(0, "ass_rcvcardid", "");
                    return;
                }

                if (cardperson == v) {
                    alert("รหัสบัตรประชาชนตรงกับผู้ขอรับทุน กรุณาตรวจสอบ");
                    dsEducation.SetItem(0, "ass_rcvcardid", "");
                    return;
                }
                PostCardPerson();

            } else if (c == "ass_prcardid") {
                PostCardPersonParent();
            }
        }

        function OnDsDeceaseItemChanged(s, r, c, v) {
            if (c == "assistpay_code") {
                PostAssistPay();
            } else if (c == "dec_deaddate") {
                CheckDocdate();
            }
        }

        function OnDsFamilyItemChanged(s, r, c, v) {
            if (c == "ass_rcvcardid") {

                var cardperson = dsMain.GetItem(0, "card_person");
                if (!checkID(v)) {
                    alert("รหัสบัตรประชาชนไม่ถูกต้อง");
                    dsFamilydecease.SetItem(0, "fam_cardid", "");
                    return;
                }

                if (cardperson == v) {
                    alert("รหัสบัตรประชาชนตรงกับผู้ขอรับทุน กรุณาตรวจสอบ");
                    dsFamilydecease.SetItem(0, "ass_rcvcardid", "");
                    return;
                }
                PostCardPerson();
            } else if (c == "assistpay_code") {
                PostAssistPay();
            } else if (c == "fam_docdate") {
                CheckDocdate();
            }
        }

        function OnDsDisasterClicked(s, r, c) {
            if (c == "b_linkaddress") {
                PostLinkAddress();
            }
        }

        function OnDsDisasterItemChanged(s, r, c) {

            if (c == "dis_disamt") {
                if (Gcoop.GetEl("hdasscondition").value == "5") {
                    PostInitPermiss();
                }
                else {
                    JsSetdisamt();
                }
            } else if (c == "assistpay_code") {
                PostAssistPay();
            }
            else if (c == "dis_disdate") {
                CheckDocdate();
            }
            else if (c == "dis_house_status") {
            JsPostDispay();
            }
        }

        function OnDsPatronizeItemChanged(s, r, c, v) {
            if (c == "assistpay_code") {
                PostAssistPay();
            }
            else if (c == "fam_docdate") {
                CheckDocdate();
            }
            else if (c == "dis_disdate") {
                CheckDocdate2();
            }
        }

        function OnDsMedicalItemChanged(s, r, c, v) {
            if (c == "assistpay_code") {
                PostAssistPay();
            } else if (c == "med_startdate" || c == "med_enddate") {
                PostCalMedDay();
            }
        }

        function OnDsGainClicked(s, r, c, v) {
            if (c == "b_del") {
                dsGain.SetRowFocus(r);
                if (confirm("ลบข้อมูลผู้รับผลประโยชน์แถวที่ " + (r + 1) + " ?") == true) {
                    PostDelRow();
                }
            }
        }

        function PostInsertRow(s, r, c, v) {
            Gcoop.GetEl("Hd_row").value = r;
            PostInsertRow();
        }

        function SheetLoadComplete() {
            var stmflag = dsAmount.GetItem(0, "stm_flag");
            var assisttype_code = dsMain.GetItem(0, "assisttype_code");
            var coop_id = dsMain.GetItem(0, "coop_id");

            if (stmflag == 1) {
                dsAmount.GetElement(0, "");
            }
            for (var i = 0; i < dsList.GetRowCount(); i++) {
                if (i == r) {
                    dsList.GetElement(i, "assisttype_code").style.background = "#FFFF99";
                    dsList.GetElement(i, "asscontract_no").style.background = "#FFFF99";
                    dsList.GetElement(i, "approve_date").style.background = "#FFFF99";
                }
                else {
                    dsList.GetElement(i, "assisttype_code").style.background = "#FFFFFF";
                    dsList.GetElement(i, "asscontract_no").style.background = "#FFFFFF";
                    dsList.GetElement(i, "approve_date").style.background = "#FFFFFF";
                }
            }
//            var ls_contsts = dsCont.GetItem(0, "asscont_status");
//            if (ls_contsts == -9) {
//                dsCont.GetElement(0, "statusdesc").style.background = "#FF0000";
//                dsCont.GetElement(0, "statusdesc").style.color = "#FFFFFF";
//            }

            if (assisttype_code != "76" && coop_id == "050001") {
                $('#dis_disdatetx').hide();
                dsPatronize.GetElement(0, "dis_disdate").style.display = 'none';
            }
        }

        function OnDsBonusItemChanged(s, r, c, v) {
            if (c == "bonus_methpay") {
            var bonus = dsBonus.GetItem(0,"bonus_methpay");
            if (bonus == 3 || bonus== 2) {
                JsInsertRowBonus();
                }
                else {
                    JsClearrow();
                }
            }
            if (c == "bonus_type") {
                JsPostunit();
            }
//            if (c == "check_date") {

//                Gcoop.GetEl("HdMbChkmemdate").value = dsMain.GetItem(0, "member_no");
//                JsCheckMemdate_date();
            }
//        }

        function PostInsertRowss() {
            JsInsertrow();

        }

        function OnDsAmountClicked(s, r, c, v) {
            if (c == "b_search") {
                Gcoop.OpenIFrame2(600, 500, "wd_as_search_deptaccount.aspx", "");
            }
        }

        function GetDeptNoFromDlg(deptno) {
            dsAmount.SetItem(0, "deptaccount_no", deptno);
            JsPostDlgdeptno();
//            var isCheck = true
//            var ddl = document.getElementById('ctl00_ContentPlace_dsAmount_FormView1_deptaccount_no');
//            for (i = 0; i < ddl.options.length; i++) {
//                if (deptno == ddl.options[i].value) {
//                    isCheck = false
//                }
//            }

//            if (isCheck) {
//                var x = document.getElementById("ctl00_ContentPlace_dsAmount_FormView1_deptaccount_no");
//                var option = document.createElement("option");
//                option.text = deptno;
//                x.add(option);
//            }
//            document.getElementById('ctl00_ContentPlace_dsAmount_FormView1_deptaccount_no').value = deptno;
        }

//        function PostInsertRowsbonus() {
//            JsInsertRowBonus();
//        }

        Number.prototype.round = function (p) {
            p = p || 10;
            return parseFloat(this.toFixed(p));
        };

        $(function () {
            //alert($("#tabs").tabs()); //ชื่อฟิวส์

            var tabIndex = Gcoop.GetEl("hdTabIndex").value; // Gcoop.ParseInt($("#<%=hdTabIndex.ClientID%>").val());
            $("#tabs").tabs({
                active: tabIndex,
                activate: function (event, ui) {
                    $("#<%=hdTabIndex.ClientID%>").val(ui.newTab.index() + "");
                }
            });
            if (tabIndex == "0") {
                $('#tabs').tabs({ active: 0, disabled: [1, 2, 3, 4, 5, 6] });
            }
            else if (tabIndex == "1") {
                $('#tabs').tabs({ active: 1, disabled: [0, 2, 3, 4, 5, 6] });
            }
            else if (tabIndex == "2") {
                $('#tabs').tabs({ active: 2, disabled: [0,1, 3, 4, 5, 6] });
            }
            else if (tabIndex == "3") {
                $('#tabs').tabs({ active: 3, disabled: [0, 1,2, 4, 5, 6] });
            }
            else if (tabIndex == "4") {
                $('#tabs').tabs({ active: 4, disabled: [0, 1, 2,3,5, 6] });
            }
            else if (tabIndex == "5") {
                $('#tabs').tabs({ active: 5, disabled: [0, 1, 2, 3,4, 6] });
            }
            else if (tabIndex == "6") {
                $('#tabs').tabs({ active: 6, disabled: [0, 1, 2, 3, 4,5] });
            }
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <div id="tabs">
        <ul style="font-size: 12px;">
            <li><a href="#tabs-1">ทุนการศึกษาบุตร</a></li>
            <li><a href="#tabs-2">สมาชิกถึงแก่กรรม</a></li>
            <li><a href="#tabs-3">ครอบครัวสมาชิก</a></li>
            <li><a href="#tabs-4">ประสบภัยพิบัติ</a></li>
            <li><a href="#tabs-5">เกื้อกูลสมาชิก</a></li>
            <li><a href="#tabs-6">รักษาพยาบาล</a></li>
            <li><a href="#tabs-7">ของขวัญ</a></li>
        </ul>
        <div id="tabs-1">
            <uc2:DsEducation ID="dsEducation" runat="server" />
        </div>
        <div id="tabs-2">
            <uc3:DsDecease ID="dsDecease" runat="server" />
        </div>
        <div id="tabs-3">
            <uc4:DsFamilydecease ID="dsFamilydecease" runat="server" />
        </div>
        <div id="tabs-4">
            <uc5:DsDisaster ID="dsDisaster" runat="server" />
        </div>
        <div id="tabs-5">
            <uc6:DsPatronize ID="dsPatronize" runat="server" />
        </div>
        <div id="tabs-6">
            <uc7:DsMedical ID="dsMedical" runat="server" />
        </div>
        <div id="tabs-7">
            <uc11:DsBonus ID="dsBonus" runat="server" />
            <uc13:DsBonus_address ID="dsBonus_address" runat="server" />
            <br />
            
            <uc12:DsBonus_instead ID="dsBonus_instead" runat="server" />
        </div>
    </div>
    <br />
    <div align="right" style="margin-right: 20px;">
        <span id="insertRow" style="display: none" class="NewRowLink" onclick="PostInsertRow()">
            เพิ่มแถว</span></div>
    <uc8:DsGain ID="dsGain" runat="server" />
    <uc9:DsAmount ID="dsAmount" runat="server" />
    <uc10:DsList ID="dsList" runat="server" />
    <asp:HiddenField ID="hdTabIndex" Value="0" runat="server" />
    <asp:HiddenField ID="hdSaveChk_GPA" runat="server" />
    <asp:HiddenField ID="hdInertRow" runat="server" />
    <asp:HiddenField ID="hdCalDay" runat="server" />
    <asp:HiddenField ID="hdActMemno" runat="server" />
    <asp:HiddenField ID="hdassgrp" runat="server" />
    <asp:HiddenField ID="hdasscondition" runat="server" />
    <asp:HiddenField ID="HdTokenIMG" runat="server" />
    <asp:HiddenField ID="Hd_membno" runat="server" />
    <asp:HiddenField ID="Hd_row" runat="server" />
    <asp:HiddenField ID="hdAssreqno" runat="server" />
    <asp:HiddenField ID="hdDateflag" runat="server" />
    <asp:HiddenField ID="hdsetpagebonus" runat="server" />
    <asp:HiddenField ID="HdMbChkmemdate" runat="server" />
</asp:Content>
