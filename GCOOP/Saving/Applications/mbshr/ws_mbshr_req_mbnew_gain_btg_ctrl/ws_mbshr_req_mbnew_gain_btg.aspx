<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_mbshr_req_mbnew_gain_btg.aspx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_req_mbnew_gain_btg_ctrl.ws_mbshr_req_mbnew_gain_btg" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsMoneytr.ascx" TagName="DsMoneytr" TagPrefix="uc2" %>
<%@ Register Src="DsGain.ascx" TagName="DsGain" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsMoneytr = new DataSourceTool;
        var dsGain = new DataSourceTool;

        function MenubarOpen() {
            Gcoop.OpenIFrameExtend("600", "580", "w_dlg_sl_member_new_search.aspx", "")
        }

        function GetValueFromDlg(strvalue) {
            dsMain.SetItem(0, "appl_docno", strvalue);
            PostDocno();
        }

        function Validate() {
            var alertstr = "";
            var memb_no = dsMain.GetItem(0, "member_no");
            var memb_name = dsMain.GetItem(0, "memb_name");
            var memb_surname = dsMain.GetItem(0, "memb_surname");
            var membgroup_code = dsMain.GetItem(0, "membgroup_code");
            var membtype_code = dsMain.GetItem(0, "membtype_code");
            var salary_amount = dsMain.GetItem(0, "salary_amount");
            var member_type = dsMain.GetItem(0, "member_type");
            var membdatefix_date = dsMain.GetItem(0, "membdatefix_date");
            var work_date = dsMain.GetItem(0, "work_date");
            if (memb_name == "" || memb_name == null) { alertstr = alertstr + "- กรุณากรอกชื่อผู้สมัคร\n"; }
            if (memb_surname == "" || memb_surname == null) { alertstr = alertstr + "- กรุณากรอกนามสกุลผู้สมัคร\n"; }
            if (membgroup_code == "" || membgroup_code == null) { alertstr = alertstr + "- กรุณากำหนดสังกัดให้กับสมาชิก\n"; }
            if (membtype_code == "" || membtype_code == null) { alertstr = alertstr + "- กรุณากำหนดประเภทสมาชิก\n"; }

            if (member_type == 1) {
                if (salary_amount == "" || salary_amount == null) { alertstr = alertstr + "- กรุณากรอกเงินเดือนให้กับผู้สมัคร\n"; }
                if (work_date == "1500-01-01 00:00:00" || work_date == null) { alertstr = alertstr + "- กรุณากรอกวันบรรจุให้กับผู้สมัคร\n"; }
            }

            if (alertstr == "") {
                return confirm("ยืนยันการบันทึกข้อมูล")

            } else {
                alert(alertstr);
                return false;
            }
        }

        function MenubarNew() {
            newclear();
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "membdatefix_flag") {
                PostSetMembdate();
            }
            else if (c == "memnofix_flag") {
                if (v == 1) {
                    PostSetMemberno();
                } else {
                    dsMain.GetElement(0, "member_no").readOnly = true;
                    dsMain.SetItem(0, "member_no", null);
                }
            }
            else if (c == "prename_code") {
                PostChangSex();
            }
            else if (c == "membgroup") {
                var membcode = $('#ctl00_ContentPlace_dsMain_FormView1_membgroup').find(":selected").val();
                dsMain.SetItem(0, "membgroup_code", membcode.Trim());
            }
            else if (c == "membgroup_code") {
                PostMembgroupCodeFind();
            }
            else if (c == "birth_date") {
                PostSetRetrydate();
            }
            else if (c == "card_person") {
                Gcoop.GetEl("HdCheckIDCard").value = c;
                PostCardPerson();
            }
            else if (c == "salary_id") {
                PostCheckSalaryId();
            }
            else if (c == "salary_amount") {

                PostSalary();
            }
            else if (c == "province_code") {
                PostGetDistrict();
                dsMain.Focus(0, c);
            }
            else if (c == "currprovince_code") {
                PostGetCurrDistrict();
            }
            else if (c == "mateprovince_code") {
                PostGetMateDistrict();
            }
            else if (c == "district_code") {
                PostGetPostcode();
            }
            else if (c == "curramphur_code") {
                PostGetCurrPostcode();
            }
            else if (c == "mateamphur_code") {
                PostGetMatePostcode();
            }
            else if (c == "bank_desc") {
                dsMain.SetItem(0, "expense_bank", v);

                dsMain.SetItem(0, "expense_branch", "");
                dsMain.SetItem(0, "branch_name", "");
                dsMain.SetItem(0, "expense_accid", "");
                PostGetBranch();
            }
            else if (c == "expense_bank") {
                dsMain.SetItem(0, "bank_desc", v);

                dsMain.SetItem(0, "expense_branch", "");
                dsMain.SetItem(0, "branch_name", "");
                dsMain.SetItem(0, "expense_accid", "");
                PostGetBranch();
            }
            else if (c == "branch_name") {
                dsMain.SetItem(0, "expense_branch", v);
            }
            else if (c == "expense_branch") {
                dsMain.SetItem(0, "branch_name", v);
            }
            else if (c == "expense_code") {
                if (v == "CSH") {
                    dsMain.GetElement(0, "expense_bank").readOnly = true;
                    dsMain.GetElement(0, "bank_desc").disabled = true;
                    dsMain.GetElement(0, "expense_branch").readOnly = true;
                    dsMain.GetElement(0, "branch_name").disabled = true;
                    dsMain.GetElement(0, "expense_accid").readOnly = true;
                    dsMain.GetElement(0, "b_bank").disabled = true;
                    dsMain.GetElement(0, "b_branch").disabled = true;
                    dsMain.SetItem(0, "expense_bank", "");
                    dsMain.SetItem(0, "bank_desc", "");
                    dsMain.SetItem(0, "expense_branch", "");
                    dsMain.SetItem(0, "branch_name", "");
                    dsMain.SetItem(0, "expense_accid", "");

                    var row = dsMoneytr.GetRowCount();
                    for (var i = 0; i < row; i++) {
                        dsMoneytr.SetItem(i, "moneytype_code", "");
                    }
                }
                else if (v == "CBT") {
                    dsMain.SetItem(0, "expense_bank", "");
                    dsMain.SetItem(0, "bank_desc", "");
                    dsMain.SetItem(0, "expense_branch", "");
                    dsMain.SetItem(0, "branch_name", "");
                    dsMain.SetItem(0, "expense_accid", "");
                    dsMain.GetElement(0, "expense_bank").readOnly = false;
                    dsMain.GetElement(0, "bank_desc").disabled = false;
                    dsMain.GetElement(0, "expense_branch").readOnly = false;
                    dsMain.GetElement(0, "branch_name").disabled = false;
                    dsMain.GetElement(0, "expense_accid").readOnly = false;
                    dsMain.GetElement(0, "b_bank").disabled = false;
                    dsMain.GetElement(0, "b_branch").disabled = false;

                    var row = dsMoneytr.GetRowCount();
                    for (var i = 0; i < row; i++) {
                        var trtype = dsMoneytr.GetItem(i, "trtype_code");
                        if (dsMoneytr.GetItem(i, "trtype_code") == "KEEP1") {
                            dsMoneytr.SetItem(i, "moneytype_code", "SAL");
                        }
                    }

                }
                else if (v == "TRN") {
                    dsMain.SetItem(0, "expense_bank", "");
                    dsMain.SetItem(0, "bank_desc", "");
                    dsMain.SetItem(0, "expense_branch", "");
                    dsMain.SetItem(0, "branch_name", "");
                    dsMain.SetItem(0, "expense_accid", "");
                    dsMain.GetElement(0, "expense_bank").readOnly = true;
                    dsMain.GetElement(0, "bank_desc").disabled = true;
                    dsMain.GetElement(0, "expense_branch").readOnly = true;
                    dsMain.GetElement(0, "branch_name").disabled = true;
                    dsMain.GetElement(0, "expense_accid").readOnly = false;
                    dsMain.GetElement(0, "b_bank").disabled = true;
                    dsMain.GetElement(0, "b_branch").disabled = true;

                    var row = dsMoneytr.GetRowCount();
                    for (var i = 0; i < row; i++) {
                        dsMoneytr.SetItem(i, "moneytype_code", "");
                    }
                }
            } else if (c == "expense_accid") {
                var expenseacc = $('#ctl00_ContentPlace_dsMain_FormView1_expense_accid').val();
                var nlen = expenseacc.length;
                if (nlen > 13) {
                    alert("เลขบัญชีที่กรอก " + expenseacc + " มีความยาวเกิน 13 หลัก กรุณตรวจสอบความถูกต้อง");
                    dsMain.SetItem(0, "expense_accid", "");
                }                
            } else if (c == "mariage") {
                if (v == "1") {
                    dsMain.GetElement(0, "mate_name").readOnly = true;
                    dsMain.GetElement(0, "mate_name").style.background = "#DDDDDD";
                    dsMain.SetItem(0, "mate_name", "");
                    dsMain.GetElement(0, "mate_cardperson").readOnly = true;
                    dsMain.GetElement(0, "mate_cardperson").style.background = "#DDDDDD";
                    dsMain.SetItem(0, "mate_cardperson", "");
                    dsMain.GetElement(0, "mate_salaryid").readOnly = true;
                    dsMain.GetElement(0, "mate_salaryid").style.background = "#DDDDDD";
                    dsMain.SetItem(0, "mate_salaryid", "");
                    dsMain.GetElement(0, "mateaddr_no").readOnly = true;
                    dsMain.GetElement(0, "mateaddr_no").style.background = "#DDDDDD";
                    dsMain.SetItem(0, "mateaddr_no", "");
                    dsMain.GetElement(0, "mateaddr_moo").readOnly = true;
                    dsMain.GetElement(0, "mateaddr_moo").style.background = "#DDDDDD";
                    dsMain.SetItem(0, "mateaddr_moo", "");
                    dsMain.GetElement(0, "mateaddr_soi").readOnly = true;
                    dsMain.GetElement(0, "mateaddr_soi").style.background = "#DDDDDD";
                    dsMain.SetItem(0, "mateaddr_soi", "");
                    dsMain.GetElement(0, "mateaddr_village").readOnly = true;
                    dsMain.GetElement(0, "mateaddr_village").style.background = "#DDDDDD";
                    dsMain.SetItem(0, "mateaddr_village", "");
                    dsMain.GetElement(0, "mateaddr_road").readOnly = true;
                    dsMain.GetElement(0, "mateaddr_road").style.background = "#DDDDDD";
                    dsMain.SetItem(0, "mateaddr_road", "");
                    dsMain.GetElement(0, "mateaddr_postcode").readOnly = true;
                    dsMain.GetElement(0, "mateaddr_postcode").style.background = "#DDDDDD";
                    dsMain.SetItem(0, "mateaddr_postcode", "");
                    dsMain.GetElement(0, "mateaddr_phone").readOnly = true;
                    dsMain.GetElement(0, "mateaddr_phone").style.background = "#DDDDDD";
                    dsMain.SetItem(0, "mateaddr_phone", "");
                    dsMain.GetElement(0, "b_linkaddress2").disabled = true;
                    dsMain.GetElement(0, "mateprovince_code").disabled = true;
                    dsMain.SetItem(0, "mateprovince_code", "");
                    dsMain.GetElement(0, "mateamphur_code").disabled = true;
                    dsMain.SetItem(0, "mateamphur_code", "");
                    dsMain.GetElement(0, "matetambol_code").disabled = true;
                    dsMain.SetItem(0, "matetambol_code", "");
                } else {
                    dsMain.GetElement(0, "mate_name").readOnly = false;
                    dsMain.GetElement(0, "mate_name").style.background = "#FFFFFF";
                    dsMain.GetElement(0, "mate_cardperson").readOnly = false;
                    dsMain.GetElement(0, "mate_cardperson").style.background = "#FFFFFF";
                    dsMain.GetElement(0, "mate_salaryid").readOnly = false;
                    dsMain.GetElement(0, "mate_salaryid").style.background = "#FFFFFF";
                    dsMain.GetElement(0, "mateaddr_no").readOnly = false;
                    dsMain.GetElement(0, "mateaddr_no").style.background = "#FFFFFF";
                    dsMain.GetElement(0, "mateaddr_moo").readOnly = false;
                    dsMain.GetElement(0, "mateaddr_moo").style.background = "#FFFFFF";
                    dsMain.GetElement(0, "mateaddr_soi").readOnly = false;
                    dsMain.GetElement(0, "mateaddr_soi").style.background = "#FFFFFF";
                    dsMain.GetElement(0, "mateaddr_village").readOnly = false;
                    dsMain.GetElement(0, "mateaddr_village").style.background = "#FFFFFF";
                    dsMain.GetElement(0, "mateaddr_road").readOnly = false;
                    dsMain.GetElement(0, "mateaddr_road").style.background = "#FFFFFF";
                    dsMain.GetElement(0, "mateaddr_postcode").readOnly = false;
                    dsMain.GetElement(0, "mateaddr_postcode").style.background = "#FFFFFF";
                    dsMain.GetElement(0, "mateaddr_phone").readOnly = false;
                    dsMain.GetElement(0, "mateaddr_phone").style.background = "#FFFFFF";
                    dsMain.GetElement(0, "b_linkaddress2").disabled = false;
                    dsMain.GetElement(0, "mateprovince_code").disabled = false;
                    dsMain.GetElement(0, "mateamphur_code").disabled = false;
                    dsMain.GetElement(0, "matetambol_code").disabled = false;
                }
            } else if (c == "mate_cardperson") {
                Gcoop.GetEl("HdCheckIDCard").value = c;
                PostCardPerson();
            }
        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_linkaddress") {
                PostLinkAddress();
            } else if (c == "b_linkaddress2") {
                PostLinkAddress2();
            }
            else if (c == "b_search") {
                Gcoop.OpenIFrameExtend('580', '590', 'w_dlg_search_memgroup.aspx', '');
            } else if (c == "b_membsearch") {
                Gcoop.OpenIFrame2Extend('630', '600', 'w_dlg_mbshr_mem_search.aspx', '');
            } else if (c == "b_bank") {
                Gcoop.OpenIFrameExtend('580', '590', 'w_dlg_kp_bank_search.aspx', '');
            } else if (c == "b_branch") {
                var bank_code = dsMain.GetItem(0, "expense_bank");
                Gcoop.OpenIFrameExtend('580', '590', 'w_dlg_kp_bankbranch_search.aspx', "?bank_code=" + bank_code);
            }
            else if (c == "membgroup") {
                var membgroup_code = $('#ctl00_ContentPlace_dsMain_FormView1_membgroup').find(":selected").val();
                dsMain.SetItem(0, "membgroup_code", membgroup_code);
            }
        }

        function GetMembNoFromDlg(memb_no) {
            dsMain.SetItem(0, "member_ref", memb_no);
        }

        function GetMemGroupFromDlg(membgroup_code) {
            dsMain.SetItem(0, "membgroup_code", membgroup_code);
            PostMembgroupCodeFromDlg();
        }
        function GetMemberNoFromDlg(memberno) {
            dsMain.SetItem(0, "member_ref", memberno);
        }
        function GetBankFromDlg(bank_code) {
            dsMain.SetItem(0, "expense_bank", bank_code);
            dsMain.SetItem(0, "bank_desc", bank_code);
            PostGetBranch();
        }

        function GetBankBranchFromDlg(branch_id) {
            dsMain.SetItem(0, "expense_branch", branch_id);
            dsMain.SetItem(0, "branch_name", branch_id);
        }

        function OnDsMoneytrItemChanged(s, r, c, v) {
            if (c == "moneytype_code") {
                if (v == "CBT") {
                    dsMoneytr.GetElement(r, "bank_code").disabled = false;
                    dsMoneytr.GetElement(r, "bank_branch").readOnly = false;
                    dsMoneytr.GetElement(r, "branch_name").readOnly = false;
                    dsMoneytr.GetElement(r, "bank_accid").readOnly = false;
                    dsMoneytr.SetItem(r, "bank_code", "");
                    dsMoneytr.SetItem(r, "bank_branch", "");
                    dsMoneytr.SetItem(r, "branch_name", "");
                    dsMoneytr.SetItem(r, "bank_accid", "");
                    dsMoneytr.SetItem(r, "bank_code", dsMain.GetItem(0, "expense_bank"));
                    dsMoneytr.SetItem(r, "bank_branch", dsMain.GetItem(0, "expense_branch"));
                    // dsMoneytr.SetItem(r, "branch_name", dsMain.GetItem(0, "branch_name2"));
                    dsMoneytr.SetItem(r, "bank_accid", dsMain.GetItem(0, "expense_accid"));
                    dsMoneytr.SetRowFocus(r);
                    PostBankBranch();
                }
                else if (v == "TRN") {
                    dsMoneytr.GetElement(r, "bank_code").disabled = true;
                    dsMoneytr.GetElement(r, "bank_branch").readOnly = true;
                    dsMoneytr.GetElement(r, "branch_name").readOnly = true;
                    dsMoneytr.GetElement(r, "bank_accid").readOnly = false;
                    dsMoneytr.GetElement(r, "bank_branch").style.background = "#DDDDDD";
                    dsMoneytr.GetElement(r, "branch_name").style.background = "#DDDDDD";
                    dsMoneytr.GetElement(r, "bank_accid").style.background = "#FFFFFF";
                    dsMoneytr.SetItem(r, "bank_code", "");
                    dsMoneytr.SetItem(r, "bank_branch", "");
                    dsMoneytr.SetItem(r, "branch_name", "");
                    dsMoneytr.SetItem(r, "bank_accid", "");
                }
                else {
                    dsMoneytr.GetElement(r, "bank_code").disabled = true;
                    dsMoneytr.GetElement(r, "bank_branch").readOnly = true;
                    dsMoneytr.GetElement(r, "branch_name").readOnly = true;
                    dsMoneytr.GetElement(r, "bank_accid").readOnly = true;
                    dsMoneytr.GetElement(r, "bank_branch").style.background = "#DDDDDD";
                    dsMoneytr.GetElement(r, "branch_name").style.background = "#DDDDDD";
                    dsMoneytr.GetElement(r, "bank_accid").style.background = "#DDDDDD";
                    dsMoneytr.SetItem(r, "bank_code", "");
                    dsMoneytr.SetItem(r, "bank_branch", "");
                    dsMoneytr.SetItem(r, "branch_name", "");
                    dsMoneytr.SetItem(r, "bank_accid", "");
                }
            } else if (c == "bank_branch") {
                dsMoneytr.SetRowFocus(r);
                PostBankBranch();
            }
        }
        function OnDsMoneytrClicked(s, r, c, v) {
            if (c == "b_del") {
                dsMoneytr.SetRowFocus(r);
                PostDeleteRow();
            }
        }

        function OnDsGainClicked(s, r, c, v) {
            if (c == "b_del") {
                dsGain.SetRowFocus(r);
                PostDeleteRowGain();
            }
        }

        function SheetLoadComplete() {
            var expense_code = dsMain.GetItem(0, "expense_code");
            if (expense_code == "CSH") {
                dsMain.GetElement(0, "expense_bank").readOnly = true;
                dsMain.GetElement(0, "bank_desc").disabled = true;
                dsMain.GetElement(0, "expense_branch").readOnly = true;
                dsMain.GetElement(0, "branch_name").disabled = true;
                dsMain.GetElement(0, "expense_accid").readOnly = true;
                dsMain.GetElement(0, "b_bank").disabled = true;
                dsMain.GetElement(0, "b_branch").disabled = true;
                dsMain.SetItem(0, "expense_bank", "");
                dsMain.SetItem(0, "bank_desc", "");
                dsMain.SetItem(0, "expense_branch", "");
                dsMain.SetItem(0, "branch_name", "");
                dsMain.SetItem(0, "expense_accid", "");
            }
            var row = dsMoneytr.GetRowCount();
            for (var i = 0; i < row; i++) {
                if (dsMoneytr.GetItem(i, "moneytype_code") == null) {
                    dsMoneytr.GetElement(i, "bank_code").disabled = false;
                    dsMoneytr.GetElement(i, "bank_branch").readOnly = true;
                    dsMoneytr.GetElement(i, "branch_name").readOnly = true;
                    dsMoneytr.GetElement(i, "bank_accid").readOnly = true;

                } else if (dsMoneytr.GetItem(i, "moneytype_code") == "CSH") {
                    dsMoneytr.GetElement(i, "bank_code").disabled = true;
                    dsMoneytr.GetElement(i, "bank_branch").readOnly = true;
                    dsMoneytr.GetElement(i, "branch_name").readOnly = true;
                    dsMoneytr.GetElement(i, "bank_accid").readOnly = true;
                    dsMoneytr.GetElement(i, "bank_branch").style.background = "#DDDDDD";
                    dsMoneytr.GetElement(i, "branch_name").style.background = "#DDDDDD";
                    dsMoneytr.GetElement(i, "bank_accid").style.background = "#DDDDDD";
                    dsMoneytr.SetItem(i, "bank_code", "");
                    dsMoneytr.SetItem(i, "bank_branch", "");
                    dsMoneytr.SetItem(i, "branch_name", "");
                    dsMoneytr.SetItem(i, "bank_accid", "");
                }
            }

            if (dsMain.GetItem(0, "mariage") == "1") {
                dsMain.GetElement(0, "mate_name").readOnly = true;
                dsMain.GetElement(0, "mate_name").style.background = "#DDDDDD";
                dsMain.SetItem(0, "mate_name", "");
                dsMain.GetElement(0, "mate_cardperson").readOnly = true;
                dsMain.GetElement(0, "mate_cardperson").style.background = "#DDDDDD";
                dsMain.SetItem(0, "mate_cardperson", "");
                dsMain.GetElement(0, "mate_salaryid").readOnly = true;
                dsMain.GetElement(0, "mate_salaryid").style.background = "#DDDDDD";
                dsMain.SetItem(0, "mate_salaryid", "");
                dsMain.GetElement(0, "mateaddr_no").readOnly = true;
                dsMain.GetElement(0, "mateaddr_no").style.background = "#DDDDDD";
                dsMain.SetItem(0, "mateaddr_no", "");
                dsMain.GetElement(0, "mateaddr_moo").readOnly = true;
                dsMain.GetElement(0, "mateaddr_moo").style.background = "#DDDDDD";
                dsMain.SetItem(0, "mateaddr_moo", "");
                dsMain.GetElement(0, "mateaddr_soi").readOnly = true;
                dsMain.GetElement(0, "mateaddr_soi").style.background = "#DDDDDD";
                dsMain.SetItem(0, "mateaddr_soi", "");
                dsMain.GetElement(0, "mateaddr_village").readOnly = true;
                dsMain.GetElement(0, "mateaddr_village").style.background = "#DDDDDD";
                dsMain.SetItem(0, "mateaddr_village", "");
                dsMain.GetElement(0, "mateaddr_road").readOnly = true;
                dsMain.GetElement(0, "mateaddr_road").style.background = "#DDDDDD";
                dsMain.SetItem(0, "mateaddr_road", "");
                dsMain.GetElement(0, "mateaddr_postcode").readOnly = true;
                dsMain.GetElement(0, "mateaddr_postcode").style.background = "#DDDDDD";
                dsMain.SetItem(0, "mateaddr_postcode", "");
                dsMain.GetElement(0, "mateaddr_phone").readOnly = true;
                dsMain.GetElement(0, "mateaddr_phone").style.background = "#DDDDDD";
                dsMain.SetItem(0, "mateaddr_phone", "");
                dsMain.GetElement(0, "b_linkaddress2").disabled = true;
                dsMain.GetElement(0, "mateprovince_code").disabled = true;
                dsMain.SetItem(0, "mateprovince_code", "");
                dsMain.GetElement(0, "mateamphur_code").disabled = true;
                dsMain.SetItem(0, "mateamphur_code", "");
                dsMain.GetElement(0, "matetambol_code").disabled = true;
                dsMain.SetItem(0, "matetambol_code", "");
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <div align="right">
        <span class="NewRowLink" onclick="PostInsertRow()">เพิ่มแถว</span>
        <uc2:DsMoneytr ID="dsMoneytr" runat="server" />
    </div>
    <br />
    <div align="left"> ข้อมูลผู้รับผลประโยชน์ </div>
    <div align="right"> 
        <span class="NewRowLink" onclick="PostInsertRowGain()">เพิ่มแถว</span>
        <uc3:DsGain ID="dsGain" runat="server" />
    </div>
   
    <asp:HiddenField ID="HdIdgroup" runat="server" />
    <asp:HiddenField ID="HdCheckIDCard" runat="server" />
</asp:Content>