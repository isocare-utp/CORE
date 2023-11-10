<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_as_req_decrepitude.aspx.cs" Inherits="Saving.Applications.assist.ws_as_req_decrepitude_ctrl.ws_as_req_decrepitude" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsPatronize.ascx" TagName="DsPatronize" TagPrefix="uc2" %>
<%@ Register Src="DsAmount.ascx" TagName="DsAmount" TagPrefix="uc3" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        //ประกาศตัวแปร ควรอยู่บริเวณบนสุดใน tag <script>
        var dsMain = new DataSourceTool();
        var dsPatronize = new DataSourceTool();
        var dsAmount = new DataSourceTool();
        var dsList = new DataSourceTool();

        //เช็คบัตรประชาชน//////////////////
        function checkID(id) {
            if (id.length != 13) return false;
            return true;
        }

        function MenubarOpen() {
            Gcoop.OpenIFrame2(750, 600, 'wd_as_search_request_old.aspx', '')
        }
        ////////////////////////////////

        //ประกาศฟังก์ชันสำหรับ event ItemChanged
        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                var ls_memberno = dsMain.GetItem(0, "member_no");

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
                Postmembname_ref();
            }
        }

        //ประกาศฟังก์ชันสำหรับ event Clicked
        function OnDsMainClicked(s, r, c) {
            if (c == "b_search") {
                Gcoop.OpenIFrame2(650, 600, 'wd_as_member_search.aspx', '')
            }
            else if (c == "b_add") {
                Gcoop.OpenIFrameUploadImg(Gcoop.GetEl("HdTokenIMG").value, "000", "", "");
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

        function GetMembNoFromDlgReqOld(memberno, reqdoc_no) {
            dsMain.SetItem(0, "member_no", memberno.trim());
            dsMain.SetItem(0, "assist_docno", reqdoc_no.trim());
            PostReqOld();
        }


        function Validate() {

            var err_text = "";

           if (Gcoop.GetEl("hdassgrp").value == "05") {
                var fam_docdate = dsPatronize.GetItem(0, "fam_docdate");
                if (fam_docdate == "" || fam_docdate == null || fam_docdate == "1500-01-01 00:00:00") {
                    err_text += "กรุณากรอกวันที่ตามเอกสาร ";
                }
            }
          
            ////////////////////////////////////////////
            if (Gcoop.GetEl("hdassgrp").value != "07") {
                var a = dsAmount.GetItem(0, "assist_amt");
                if (a <= 0) {
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


        function OnDsPatronizeItemChanged(s, r, c, v) {
            if (c == "assistpay_code") {
                PostAssistPay();
            }
            else if (c == "fam_docdate") {
                CheckDocdate();
            }
        }

        function PostInsertRow(s, r, c, v) {
            Gcoop.GetEl("Hd_row").value = r;
            PostInsertRow();
        }

        function SheetLoadComplete() {
            var stmflag = dsAmount.GetItem(0, "stm_flag");

            if (stmflag == 1) {
                dsAmount.GetElement(0, "")
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
        }

        function PostInsertRowss() {
            JsInsertrow();

        }

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
           
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <div id="tabs">
        <ul style="font-size: 12px;">
            <li><a href="#tabs-1">เกื้อกูลสมาชิก</a></li>
        </ul>
        <div id="tabs-1">
            <uc2:DsPatronize ID="dsPatronize" runat="server" />
        </div>
    
    </div>
    <br />
    <div align="right" style="margin-right: 20px;">
        <span id="insertRow" style="display: none" class="NewRowLink" onclick="PostInsertRow()">
            เพิ่มแถว</span></div>
    <uc3:DsAmount ID="dsAmount" runat="server" />
    <uc4:DsList ID="dsList" runat="server" />
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
</asp:Content>
