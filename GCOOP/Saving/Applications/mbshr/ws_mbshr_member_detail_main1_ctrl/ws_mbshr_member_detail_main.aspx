<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_mbshr_member_detail_main.aspx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_main_ctrl.ws_mbshr_member_detail_main" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsTrnhistory.ascx" TagName="DsTrnhistory" TagPrefix="uc2" %>
<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc3" %>
<%@ Register Src="DsExpense.ascx" TagName="DsExpense" TagPrefix="uc4" %>
<%@ Register Src="DsMoneytr.ascx" TagName="DsMoneytr" TagPrefix="uc5" %>
<%@ Register Src="DsStatus.ascx" TagName="DsStatus" TagPrefix="uc6" %>
<%@ Register Src="DsOtherkeep.ascx" TagName="DsOtherkeep" TagPrefix="uc7" %>
<%@ Register Src="DsGain.ascx" TagName="DsGain" TagPrefix="uc8" %>
<%@ Register Src="DsLoan.ascx" TagName="DsLoan" TagPrefix="uc9" %>
<%@ Register Src="DsPauseloan.ascx" TagName="DsPauseloan" TagPrefix="uc10" %>
<%@ Register Src="DsInsurance.ascx" TagName="DsInsurance" TagPrefix="uc11" %>
<%@ Register Src="DsShare.ascx" TagName="DsShare" TagPrefix="uc12" %>
<%@ Register Src="DsCollall.ascx" TagName="DsCollall" TagPrefix="uc13" %>
<%@ Register Src="DsCollwho.ascx" TagName="DsCollwho" TagPrefix="uc14" %>
<%@ Register Src="DsKeepdata.ascx" TagName="DsKeepdata" TagPrefix="uc15" %>
<%@ Register Src="DsEtcpaymonth.ascx" TagName="DsEtcpaymonth" TagPrefix="uc16" %>
<%@ Register Src="DsLoan2.ascx" TagName="DsLoan2" TagPrefix="uc17" %>
<%@ Register Src="DsWrt.ascx" TagName="DsWrt" TagPrefix="uc18" %>
<%@ Register src="DsSlipadjust.ascx" tagname="DsSlipadjust" tagprefix="uc19" %>
<%@ Register src="DsSlipadjmain.ascx" tagname="DsSlipadjmain" tagprefix="uc20" %>
<%@ Register src="DsDeposit.ascx" tagname="DsDeposit" tagprefix="uc21" %>
<%@ Register src="DsMthExpense.ascx" tagname="DsMthExpense" tagprefix="uc22" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsCollwho = new DataSourceTool;
        var dsCollall = new DataSourceTool;
        var dsShare = new DataSourceTool;
        var dsLoan = new DataSourceTool;
        var dsKeepdata = new DataSourceTool;
        var dsSlipadjust = new DataSourceTool();
        var dsSlipadjmain = new DataSourceTool();

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_search") {
                Gcoop.OpenIFrame2Extend(650, 600, 'w_dlg_sl_member_search.aspx', '')
            }
            else if (c == "b_print") {
                PopupReportshr();
            }
            else if (c == "b_pbreport") {
                PopupReportloan();
            }
        }
        function GetValueFromDlg(memberno) {
            dsMain.SetItem(0, "member_no", memberno.trim());
            PostMemberNo();
        }
        function OnDsMainItemChanged(s, r, c) {
            if (c == "member_no") {
                PostMemberNo();
            } else if (c == "salary_id") {
                PostSalary();
            }
        }
        function OnDsShareClicked(s, r, c) {
            if (c == "bshr_detail") {
                var ls_memno = dsMain.GetItem(0, "member_no");
                var ls_shr_typecode = dsShare.GetItem(r, "sharetype_code");
                Gcoop.OpenIFrame3Extend("800", "710", "w_dlg_sl_detail_share.aspx", "?memno=" + ls_memno + "&shrtype=" + ls_shr_typecode);
                //Gcoop.OpenIFrame3("650", "560", "w_dlg_sl_detail_share.aspx");
            }
        }
        function OnDsLoanClicked(s, r, c) {
            if (c == "bloan_detail") {
                var ls_lcontno = dsLoan.GetItem(r, "loancontract_no");
                Gcoop.OpenIFrame3Extend("900", "600", "w_dlg_sl_detail_contract.aspx", "?lcontno=" + ls_lcontno);
            }
        }

        function OnDsKeepdataClicked(s, r, c) {
            if (c == "b_detail") {
                var ls_memno = dsMain.GetItem(0, "member_no");
                var ls_recvperiod = dsKeepdata.GetItem(r, "recvperiod");
                Gcoop.OpenIFrame3Extend("700", "400", "w_dlg_mb_detail_keepdatadet.aspx", "?memno=" + ls_memno + "&recvperiod=" + ls_recvperiod);
                //Gcoop.OpenIFrame3("650", "590", "w_dlg_sl_detail_contract.aspx");
            }
        }

        function OnDsLoan2ItemChanged(s, r, c) {
            if (c == "check_loan") {
                PostCheckLoan();
            }
        }

        function OnDsDetailItemChanged(s, r, c) {

        }
        function SheetLoadComplete() {
            var ls_member_status = dsMain.GetItem(0, "member_status");
            var ls_resign_status = dsMain.GetItem(0, "resign_status");

            if (ls_member_status == -1) {
                dsMain.GetElement(0, "cp_name").style.background = "#FFFF33";

            } else if (ls_resign_status == 1) {
                dsMain.GetElement(0, "cp_name").style.background = "#FF0000";
                dsMain.GetElement(0, "cp_name").style.color = "#FFFFFF";
            }
            else {
                dsMain.GetElement(0, "cp_name").style.background = "#FFFFFF";
            }

            for (var i = 0; i < dsCollall.GetRowCount(); i++) {
                var ls_resigncoll_status = dsCollall.GetItem(i, "resign_status");
                if (ls_resigncoll_status == 1) {
                    dsCollall.GetElement(i, "loancontract_no").style.background = "#FF0000";
                    dsCollall.GetElement(i, "loancontract_no").style.color = "#FFFFFF";
                    dsCollall.GetElement(i, "loancolltype_desc").style.background = "#FF0000";
                    dsCollall.GetElement(i, "loancolltype_desc").style.color = "#FFFFFF";
                    dsCollall.GetElement(i, "ref_collno").style.background = "#FF0000";
                    dsCollall.GetElement(i, "ref_collno").style.color = "#FFFFFF";
                    dsCollall.GetElement(i, "description").style.background = "#FF0000";
                    dsCollall.GetElement(i, "description").style.color = "#FFFFFF";
                    dsCollall.GetElement(i, "prnbal_amt").style.background = "#FF0000";
                    dsCollall.GetElement(i, "prnbal_amt").style.color = "#FFFFFF";
                    dsCollall.GetElement(i, "collactive_percent").style.background = "#FF0000";
                    dsCollall.GetElement(i, "collactive_percent").style.color = "#FFFFFF";
                    dsCollall.GetElement(i, "collactive_amt").style.background = "#FF0000";
                    dsCollall.GetElement(i, "collactive_amt").style.color = "#FFFFFF";
                }
                else {
                    dsCollall.GetElement(i, "loancontract_no").style.background = "#FFFFFF";
                    dsCollall.GetElement(i, "loancolltype_desc").style.background = "#FFFFFF";
                    dsCollall.GetElement(i, "ref_collno").style.background = "#FFFFFF";
                    dsCollall.GetElement(i, "description").style.background = "#FFFFFF";
                    dsCollall.GetElement(i, "prnbal_amt").style.background = "#FFFFFF";
                    dsCollall.GetElement(i, "collactive_percent").style.background = "#FFFFFF";
                    dsCollall.GetElement(i, "collactive_amt").style.background = "#FFFFFF";
                }
            }

            for (var i = 0; i < dsCollwho.GetRowCount(); i++) {
                var ls_resign_collstatus = dsCollwho.GetItem(i, "resign_status");
                if (ls_resign_collstatus == 1) {
                    dsCollwho.GetElement(i, "member_no").style.background = "#FF0000";
                    dsCollwho.GetElement(i, "member_no").style.color = "#FFFFFF";
                    dsCollwho.GetElement(i, "memb_name").style.background = "#FF0000";
                    dsCollwho.GetElement(i, "memb_name").style.color = "#FFFFFF";
                    dsCollwho.GetElement(i, "sharestk_value").style.background = "#FF0000";
                    dsCollwho.GetElement(i, "sharestk_value").style.color = "#FFFFFF";
                    dsCollwho.GetElement(i, "loancontract_no").style.background = "#FF0000";
                    dsCollwho.GetElement(i, "loancontract_no").style.color = "#FFFFFF";
                    dsCollwho.GetElement(i, "prnbal_amt").style.background = "#FF0000";
                    dsCollwho.GetElement(i, "prnbal_amt").style.color = "#FFFFFF";
                    dsCollwho.GetElement(i, "collactive_percent").style.background = "#FF0000";
                    dsCollwho.GetElement(i, "collactive_percent").style.color = "#FFFFFF";
                    dsCollwho.GetElement(i, "collactive_amt").style.background = "#FF0000";
                    dsCollwho.GetElement(i, "collactive_amt").style.color = "#FFFFFF";
                }
                else {
                    dsCollwho.GetElement(i, "member_no").style.background = "#FFFFFF";
                    dsCollwho.GetElement(i, "memb_name").style.background = "#FFFFFF";
                    dsCollwho.GetElement(i, "sharestk_value").style.background = "#FFFFFF";
                    dsCollwho.GetElement(i, "loancontract_no").style.background = "#FFFFFF";
                    dsCollwho.GetElement(i, "prnbal_amt").style.background = "#FFFFFF";
                    dsCollwho.GetElement(i, "collactive_percent").style.background = "#FFFFFF";
                    dsCollwho.GetElement(i, "collactive_amt").style.background = "#FFFFFF";
                }
            }

            if (Gcoop.GetEl("HdSlipAdjust").value == "true") {
                var member_no = dsMain.GetItem(0, "member_no");
                alert("สมาชิกหมายเลข " + member_no + " ยังมียอดค้างชำระหักไม่ได้" + Gcoop.GetEl("HdSlipAdjustDet").value);
            }
        }
        function OnClickLinkNextReport() {
            //objdw_main.AcceptText();
            popupReportshr();
        }

        function OnDsSlipadjmainItemChanged(s, r, c, v) {
            if (c == "adjslip_no") {
                PostAdjPeriod();
            }
        }
        function OnDsSlipadjmainClicked(s, r, c) {

        }
        function OnDsSlipadjustItemChanged(s, r, c, v) {

        }
        function OnDsSlipadjustClicked(s, r, c) {

        }

      
    </script>
    <style type="text/css">
        .ui-tabs
        {
            font-family: Tahoma;
            font-size: 12px;
        }
        #tabs
        {
            width: 760px;
        }
    </style>
    <script type="text/javascript">
        Number.prototype.round = function (p) {
            p = p || 10;
            return parseFloat(this.toFixed(p));
        };

        $(function () {
            //            $("#tabs").tabs();

            var tabIndex = Gcoop.ParseInt($("#<%=hdTabIndex.ClientID%>").val());
            $("#tabs").tabs({
                active: tabIndex,
                activate: function (event, ui) {
                    $("#<%=hdTabIndex.ClientID%>").val(ui.newTab.index() + "");
                }
            });

            if ($("#ctl00_ContentPlace_dsSlipadjmain_FormView1_adjslip_no").val() != "") {
                var sumprinc = 0;
                var sumint = 0;
                var sumall = 0;
                for (var i = 0; i < dsSlipadjust.GetRowCount(); i++) {
                    sumprinc += dsSlipadjust.GetItem(i, "principal_adjamt");
                    sumint += dsSlipadjust.GetItem(i, "interest_adjamt");
                    sumall += dsSlipadjust.GetItem(i, "item_adjamt");
                }

                $("#ctl00_ContentPlace_dsSlipadjust_sumprinc").val(sumprinc.toFixed(2).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
                $("#ctl00_ContentPlace_dsSlipadjust_sumint").val(sumint.toFixed(2).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
                $("#ctl00_ContentPlace_dsSlipadjust_sumall").val(sumall.toFixed(2).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
                //ctl00_ContentPlace_dsSlipadjust_sumprinc
                //ctl00_ContentPlace_dsSlipadjust_sumint
                //ctl00_ContentPlace_dsSlipadjust_sumall
            }

        });</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:HiddenField ID="HfLncontno" runat="server" />
    <asp:HiddenField ID="HiddenFieldTab" runat="server" />
    <asp:HiddenField ID="Hloancheck" runat="server" />
    <asp:HiddenField ID="HfOpenLnContDlg" runat="server" />
    <asp:HiddenField ID="HdOpenIFrame" runat="server" Value="False" />
    <asp:HiddenField ID="HdcheckPdf" runat="server" Value="False" />
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <div id="tabs">
        <ul>
            <li><a href="#tabs-1">ข้อมูลทั่วไป</a></li>
            <li><a href="#tabs-2">บัญชีรับ-จ่าย</a></li>
            <li><a href="#tabs-3">หุ้น-หนี้</a></li>
            <li><a href="#tabs-4">การค้ำประกัน</a></li>
            <li><a href="#tabs-5">เรียกเก็บประจำเดือน</a></li>
            <li style = "display:none"><a href="#tabs-6">เบี้ยประกัน</a></li>
            <li style = "display:none"><a href="#tabs-7">กองทุนผู้ค้ำประกัน</a></li>
            <li><a href="#tabs-8">รูปภาพ</a></li>
            <li><a href="#tabs-9">รายการค้างชำระ</a></li>            
            <li><a href="#tabs-10">บัญชีเงินฝาก</a></li>
            <li><a href="#tabs-11">รายการหักอื่นๆ</a></li>
        </ul>
        <div id="tabs-1">
            <uc3:DsDetail ID="dsDetail" runat="server" />
            <br />
            <uc6:DsStatus ID="dsStatus" runat="server" />
            <br />
            <uc7:DsOtherkeep ID="dsOtherkeep" runat="server" />
            <br />
            <uc8:DsGain ID="dsGain" runat="server" />
            <br />
            <uc2:DsTrnhistory ID="dsTrnhistory" runat="server" />
        </div>
        <div id="tabs-2">
            <uc4:DsExpense ID="dsExpense" runat="server" />
            <br />
            <uc5:DsMoneytr ID="dsMoneytr" runat="server" />
        </div>
        <div id="tabs-3">
            <uc12:DsShare ID="dsShare" runat="server" />
            <br />
            <uc17:DsLoan2 ID="dsLoan2" runat="server" />
            <br />
            <uc9:DsLoan ID="dsLoan" runat="server" />
            <br />
            <uc10:DsPauseloan ID="dsPauseloan" runat="server" />
        </div>
        <div id="tabs-4">
            <uc13:DsCollall ID="dsCollall" runat="server" />
            <br />
            <uc14:DsCollwho ID="dsCollwho" runat="server" />
        </div>
        <div id="tabs-5">
            <uc16:DsEtcpaymonth ID="dsEtcpaymonth" runat="server" />
            <br />
            <uc15:DsKeepdata ID="dsKeepdata" runat="server" />
        </div>
        <div id="tabs-6">
            <uc11:DsInsurance ID="dsInsurance" runat="server" />
        </div>
        <div id="tabs-7">
            <uc18:DsWrt ID="dsWrt" runat="server" />
        </div>
        <div id="tabs-8" align="center">
            <asp:Image ID="Img_member_profile" runat="server" Height="200px" Width="150px" Style="float: none;" />
            <asp:Image ID="Img_member_signature" runat="server" Height="300px" Width="300px" />
        </div>
        <div id="tabs-9" >
            <uc20:DsSlipadjmain ID="dsSlipadjmain" runat="server" />
            <uc19:DsSlipadjust ID="dsSlipadjust" runat="server" />
        </div>
        <div id="tabs-10" >
            <uc21:DsDeposit ID="dsDeposit" runat="server" />
        </div>
        <div id="tabs-11" >
            <uc22:DsMthExpense ID="dsMthExpense" runat="server" />
        </div>
    </div>
    <asp:HiddenField ID="hdTabIndex" runat="server" />
    <asp:HiddenField ID="HdSlipAdjust" runat="server" Value="false" />
    <asp:HiddenField ID="HdSlipAdjustDet" runat="server" value=""/>
</asp:Content>
