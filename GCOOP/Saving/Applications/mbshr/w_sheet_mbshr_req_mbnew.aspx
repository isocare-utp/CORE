<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_mbshr_req_mbnew.aspx.cs" Inherits="Saving.Applications.mbshr.w_sheet_mbshr_req_mbnew" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=changeDistrict%>
    <%=jsSalary%>
    <%=jsIdCard%>
    <%=jsCallRetry%>
    <%=jsRefresh%>
    <%=jsGetPostcode%>
    <%=jsmembgroup_code%>
    <%=jsSalary_id%>
    <%=jsChanegValue %>
    <%=jsMemberNo %>
    <%=jsGetdocno%>
    <%=jsgetpicMember_no%>
    <%=newclear %>
    <%=jsRunMemberNo%>
    <%=jsGetCurrDistrict %>
    <%=jsGetCurrPostcode %>
    <%=jsLinkAddress %>
    <%=jsChangmidgroupcontrol%>
    <%=jsChangmembsection %>
    <%=jsGainInsertRow %>
    <%=jsGainDeleteRow%>
    <%=jsRefreshCoop %>
    <%=jsChangSex %>
    <%=jsCheckNation %>
    <%=jsChk_NameSurname%>
    <%=jsPostBankBranch%>
    <%=jsPostBank%>
    <%=jsMoneyTrInsertRow%>
    <%=jsMoneyTrDeleteRow%>
    <%=jsGetMateDistrict%>
    <%=jsGetMatePostcode%>
    <%=jsRefreshExpense%>
    <%=jsMembdatefix%>
    <%=jsExpenseBank%>
    <%=jsMateCard%>
    <%=jsMateSalary%>
    <%=jsChkperiodshare_value%>
    <script type="text/javascript">
        function MenubarOpen() {
            Gcoop.OpenIFrame("580", "720", "w_dlg_sl_member_new_search.aspx", "")
        }
        function Validate() {
            var result = false;
            result = ValidateForm();
            if (result) {

                objdw_main.AcceptText();
                //                objdw_data.AcceptText();
                // return confirm("ยืนยันการบันทึกข้อมูล");
                return true;
            }
            else
                return false;
        }

        function ValidateForm() {
            var alertstr = "";
            var memb_no = objdw_main.GetItem(1, "member_no");
            var memb_name = objdw_main.GetItem(1, "memb_name");
            var memb_surname = objdw_main.GetItem(1, "memb_surname");
            var membgroup_code = objdw_main.GetItem(1, "membgroup_code");
            var salary_amount = objdw_main.GetItem(1, "salary_amount");
            var member_type = objdw_main.GetItem(1, "member_type");
            var membdatefix_date = objdw_main.GetItem(1, "membdatefix_date");
            //            if (memb_no == "" || memb_no == null) { alertstr = alertstr + "- กรุณากรอกเลขทะเบียน\n"; }
            if (memb_name == "" || memb_name == null) { alertstr = alertstr + "- กรุณากรอกชื่อผู้สมัคร\n"; }
            if (memb_surname == "" || memb_surname == null) { alertstr = alertstr + "- กรุณากรอกนามสกุลผู้สมัคร\n"; }
            if (membgroup_code == "" || membgroup_code == null) { alertstr = alertstr + "- กรุณากำหนดสังกัดให้กับสมาชิก\n"; }
            // if (membdatefix_date == "" || membdatefix_date == null) { alertstr = alertstr + "- กรุณากรอกวันที่เป็นสมาชิก\n"; }

            if (member_type == 1) {
                if (salary_amount == "" || salary_amount == null
            ) { alertstr = alertstr + "- กรุณากรอกเงินเดือนให้กับผู้สมัคร\n"; }
            }

            if (alertstr == "") {

                return true;

            } else {
                alert(alertstr);
                return false;
            }
        }

        function MenubarNew() {
            newclear();
        }
        function GetValueFromDlg(strvalue) {

            if (Gcoop.GetEl("HidBtnClick").value == "btn_membsearch") {
                objdw_main.SetItem(1, "member_ref", strvalue);
                Gcoop.GetEl("HidBtnClick").value = "Hdoc_no"
            }
            else {
                Gcoop.GetEl("Hdoc_no").value = strvalue;
                jsGetdocno();
            }
        }

        function itemChange(sender, rowNumber, columnName, newValue) {
            if (columnName == "province_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidlast_focus").value = "province_code_0";
                changeDistrict();

            }
            else if (columnName == "district_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidlast_focus").value = "district_code_0";
                jsGetPostcode();

            }
            if (columnName == "currprovince_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidlast_focus").value = "currprovince_code_0";
                jsGetCurrDistrict();

            }
            else if (columnName == "curramphur_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidlast_focus").value = "curramphur_code_0";
                jsGetCurrPostcode();

            }

            if (columnName == "mateprovince_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidlast_focus").value = "mateprovince_code_0";
                jsGetMateDistrict();

            }
            else if (columnName == "mateamphur_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                // alert(newValue);
                Gcoop.GetEl("Hidlast_focus").value = "mateamphur_code_0";
                jsGetMatePostcode();

            }

            else if (columnName == "memb_surname") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                //                jsChk_NameSurname();
            }
            else if (columnName == "salary_amount") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidlast_focus").value = "salary_amount_0";
                jsSalary();

            }
            else if (columnName == "incomeetc_amt") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidlast_focus").value = "incomeetc_amt_0";
                jsSalary();

            }
            else if (columnName == "card_person") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                //                Gcoop.GetEl("Hidlast_focus").value = "card_person_0";
                jsIdCard();

            }
            else if (columnName == "membgroup_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidgroup").value = newValue;
                Gcoop.GetEl("Hidlast_focus").value = "membgroup_code_0";
                jsmembgroup_code();

            }
            else if (columnName == "salary_id") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidlast_focus").value = "salary_id_0";
                jsSalary_id();

            }
            else if (columnName == "birth_tdate") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidlast_focus").value = "birth_tdate_0";
                jsCallRetry();

            }
            else if (columnName == "appltype_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("HidColname").value = "appltype_code";
                Gcoop.GetEl("Hidlast_focus").value = "appltype_code_0";
                jsRefresh();

            }
            else if (columnName == "member_no") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidmem_no").value = newValue;
                Gcoop.GetEl("Hidlast_focus").value = "member_no_0";
                jsMemberNo();

            }
            else if (columnName == "membsection_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidmembsection").value = newValue;
                Gcoop.GetEl("Hidlast_focus").value = "membsection_code_0";
                jsChangmidgroupcontrol();

            }
            else if (columnName == "membtype_code") {
                objdw_main.SetItem(rowNumber, columnName, Gcoop.StringFormat(newValue, "00"));
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidlast_focus").value = "membtype_code_0";
                // Gcoop.GetEl("Hidgroup").value = newValue;
                //jsChangmembtype();
                //                alert("membtype_code");
                //                Gcoop.GetEl("Hidlast_focus").value = "prename_code_0";
            }
            else if (columnName == "prename_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                // Gcoop.GetEl("Hidgroup").value = newValue;
                Gcoop.GetEl("Hidlast_focus").value = "prename_code_0";
                jsChangSex();

            }
            else if (columnName == "nationality") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                // Gcoop.GetEl("Hidgroup").value = newValue;
                Gcoop.GetEl("Hidlast_focus").value = "nationality_0";
                jsCheckNation();

            }
            else if (columnName == "membdatefix_flag") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                // Gcoop.GetEl("Hidgroup").value = newValue;
                Gcoop.GetEl("Hidlast_focus").value = "membdatefix_flag_0";
                jsMembdatefix();

            }
            else if (columnName == "expense_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                // Gcoop.GetEl("Hidgroup").value = newValue;
                Gcoop.GetEl("Hidlast_focus").value = "expense_code_0";
                jsRefreshExpense();

            }
            else if (columnName == "expense_bank") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidexpbank").value = newValue;
                Gcoop.GetEl("Hidlast_focus").value = "expense_bank_0";
                jsExpenseBank();

            }
            else if (columnName == "mate_cardperson") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidlast_focus").value = "mate_cardperson_0";
                jsMateCard();
            }
            else if (columnName == "mate_salaryid") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hidlast_focus").value = "mate_salaryid_0";
                jsMateSalary();
            }
            else if (columnName == "periodshare_value") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
//                  var periodbase_value = Number( objdw_main.GetItem(1, "periodbase_value"));
//                  var salary_amount = Number(objdw_main.GetIteml(1, "salary_amount"));
//                  var periodshare_value = Number(objdw_main.GetItem(1, "periodshare_value"));
//                  if (periodshare_value >= periodbase_value && periodshare_value < salary_amount) {
//                      objdw_main.SetItem(rowNumber, columnName, newValue);
//                  } else {
//                      jsChkperiodshare_value();
                //                  }
                jsChkperiodshare_value();
                
            }
        }

        function OnKeyUpEnd(e) {
            if (e.keyCode == "115") {
                TryOpenDlgDeptWith();
            } else if (e.keyCode == "123") {
            }
        }
        function dw_mainClick(s, r, c) {
            if (c == "memnofix_flag") {
                Gcoop.CheckDw(s, r, c, "check_coop", 1, 0);
                jsRunMemberNo();

            }
            if (c == "blinkaddress") {
                Gcoop.CheckDw(s, r, c, "blinkaddress", 1, 0);

                jsLinkAddress();

            } if (c == "b_copyaddr") {
                Gcoop.CheckDw(s, r, c, "b_copyaddr", 1, 0);

                jsLinkAddress();
            }
        }
        function b_searchClick(s, r, c) {
            if (c == "b_group") {
                Gcoop.OpenIFrame('580', '590', 'w_dlg_search_memgroup.aspx', '');

            }
            if (c == "btn_membsearch") {
                Gcoop.OpenIFrame('580', '590', 'w_dlg_sl_member_search.aspx', '');
                Gcoop.GetEl("HidBtnClick").value = "btn_membsearch";
            }
            if (c == "b_bank") {
                Gcoop.OpenIFrame('580', '590', 'w_dlg_kp_bank_search.aspx', '');
            }
            if (c == "b_branch") {
                var bank_code = objdw_main.GetItem(1, "expense_bank");
                Gcoop.OpenIFrame('580', '590', 'w_dlg_kp_bankbranch_search.aspx', "?bank_code=" + bank_code);
            }

        }

        function GetBankFromDlg(bank_code) {
            objdw_main.SetItem(1, "expense_bank", bank_code);
            objdw_main.AcceptText();
            Gcoop.GetEl("Hidexpbank").value = bank_code;
            Gcoop.GetEl("Hidlast_focus").value = "expense_bank_0";
            jsExpenseBank();

        }

        function GetBankBranchFromDlg(branch_id) {
            objdw_main.SetItem(1, "expense_branch", branch_id);
            objdw_main.AcceptText();
        }

        function GetMemGroupFromDlg(membgroup_code) {
            objdw_main.SetItem(1, "membgroup_code", membgroup_code);
            objdw_main.AcceptText();
            Gcoop.GetEl("Hidgroup").value = membgroup_code;
            jsmembgroup_code();

        }
        function dw_dataClick(s, r, c) {
            if (c == "recv_shrstatus") {
                Gcoop.CheckDw(s, r, c, "recv_shrstatus", 0, 1);
            }
        }
        function ChangedValue(s, r, c, v) {
            Gcoop.GetEl("Hidlast_focus").value = "periodshare_value_0";
            if (c == "periodshare_value") {
                objdw_data.SetItem(r, "periodshare_value", v);
                objdw_data.AcceptText();
                Gcoop.GetEl("H_periodshare_value").value = v;
                Gcoop.GetEl("Max_periodshare_value").value = v;
                var periodbase_value = objdw_data.GetItem(r, "periodbase_value");
                var max_periodbase_value = objdw_data.GetItem(r, "maxshare_value");
                if (v % 10 != 0) {
                    //  alert("มูลค่าหุ้นต่อ 1 หน่วย ไม่ตรงตามฐาน");
                    //  jsChanegValue();
                }
                else {
                    if (v < periodbase_value) {
                        //   alert(v + "<" + "  " + periodbase_value + "มูลค่าหุ้นต่ำกว่าหุ้นตามฐาน");
                        //    jsChanegValue();
                    } else {
                        if (v > max_periodbase_value) {
                            //       alert(v + ">" + "  " + max_periodbase_value + "มูลค่าหุ้นสูงกว่าหุ้นตามฐาน");
                            //       jsChanegValue();
                        }
                    }
                }

            }
        }
        function dw_moneytrButtonClicked(sender, row, buttonmame) {
            if (buttonmame == "btn_addtrtype") {
                Gcoop.GetEl("Hidlast_focus").value = "bank_desc_0";
                jsMoneyTrInsertRow();
            }
            if (buttonmame == "btn_deltrtype") {
                if (confirm("คุณต้องการลบรายการแถว " + row + " ใช่หรือไม่?")) {
                    Gcoop.GetEl("HdChkRowDel").value = row;
                    jsMoneyTrDeleteRow();
                }
            }

        }

        function dw_moneytrItemChanged(sender, row, col, newValue) {
            sender.SetItem(row, col, newValue);
            sender.AcceptText();

            if (col == "bank_accid") {
                sender.SetItem(row, col, newValue);
                sender.AcceptText();
            }
            else if (col == "trtype_code") {
                sender.SetItem(row, col, newValue);
                sender.AcceptText();
                Gcoop.GetEl("HdTrTypeCode").value = newValue;
            }
            else if (col == "moneytype_code") {
                sender.SetItem(row, col, newValue);
                sender.AcceptText();
                Gcoop.GetEl("HdMoneyType").value = newValue;

            }
            else if (col == "bank_branch") {
                sender.SetItem(row, col, newValue);
                sender.AcceptText();
                Gcoop.GetEl("HdRow").value = row;
                Gcoop.GetEl("HdBankBranchCode").value = newValue;
                Gcoop.GetEl("Hidlast_focus").value = "branch_name_0";
                jsPostBankBranch();
            }
            else if (col == "bank_code") {
                sender.SetItem(row, col, newValue);
                sender.AcceptText();
                Gcoop.GetEl("HdRow").value = row;
                Gcoop.GetEl("HdBankCode").value = newValue;
                Gcoop.GetEl("Hidlast_focus").value = "bank_accid_0";
                jsPostBank();
            }
            else if (col == "bank_desc") {
                sender.SetItem(row, col, newValue);
                sender.AcceptText();
                Gcoop.GetEl("HdRow").value = row;
                Gcoop.GetEl("Hidlast_focus").value = "branch_name_0";
                Gcoop.OpenDlg("450", "600", "w_dlg_mb_search_bank.aspx", "?seach_key=" + newValue);
            }
            else if (col == "branch_name") {
                sender.SetItem(row, col, newValue);
                sender.AcceptText();
                Gcoop.GetEl("HdRow").value = row;
                var BankCode = objdw_moneytr.GetItem(row, "bank_code");
                Gcoop.GetEl("Hidlast_focus").value = "bank_accid";
                Gcoop.OpenDlg("450", "600", "w_dlg_mb_search_bankbranch.aspx", "?seach_key=" + newValue + "&bankCode=" + BankCode);
            }
            else {
                //alert("ไม่พบรายเปลี่ยนเเปลง");
            }
        }
        function GetValueFromDlgSeachBank(Bank_code, Bank_desc) {
            var row = Gcoop.GetEl("HdRow").value;
            objdw_moneytr.SetItem(row, "bank_branch", "");
            objdw_moneytr.SetItem(row, "branch_name", "");
            objdw_moneytr.SetItem(row, "bank_code", Gcoop.Trim(Bank_code));
            objdw_moneytr.SetItem(row, "bank_desc", Gcoop.Trim(Bank_desc));
            objdw_moneytr.AcceptText();
        }
        function GetValueFromDlgSeachBankBranch(BankBranch_code, BankBranch_desc) {
            var row = Gcoop.GetEl("HdRow").value;
            objdw_moneytr.SetItem(row, "bank_branch", Gcoop.Trim(BankBranch_code));
            objdw_moneytr.SetItem(row, "branch_name", Gcoop.Trim(BankBranch_desc));
            objdw_moneytr.AcceptText();
        }

        function OnUpLoad() {
            if (Gcoop.GetEl("Hidmem_no").value != "") {
                var member_no = Gcoop.GetEl("Hidmem_no").value;
                Gcoop.OpenDlg("570", "590", "w_dlg_picture.aspx", "?member=" + member_no);

            }
            else {
                alert("ไม่พบเลขสมาชิก");
            }
        }
        function GetShow() { jsgetpicMember_no(); }


        function OnDwGainClick(s, r, c) {
            if (c == "b_add") {
                Gcoop.CheckDw(s, r, c, "b_add", 0, 1);
                Gcoop.GetEl("Hidlast_focus").value = "gain_name_0";
                jsGainInsertRow();
            }
            if (c == "b_del") {
                Gcoop.CheckDw(s, r, c, "b_del", 0, 1);
                if (confirm("คุณต้องการลบรายการแถว " + r + " ใช่หรือไม่?")) {
                    Gcoop.GetEl("HdChkRowDel").value = r;
                    jsGainDeleteRow();
                }
            }

        }
        function SheetLoadComplete() {
            if (Gcoop.GetEl("HdIsPostBack").value != "true") {

            }

            Gcoop.SetLastFocus(Gcoop.GetEl("Hidlast_focus").value);
            Gcoop.Focus();
            Disable_ALL();
        }

        function Disable_ALL() {
            DisabledTable(objdw_main, "appl_status", "dw_main", null);
            DisabledTable(objdw_main, "appl_status", "dw_moneytr", null);
            DisabledTable(objdw_main, "appl_status", "dw_gain", null);
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
            if (chk == '8') {
                status = false;
            } else {
                status = true;
            }
            $('#obj' + namedw + '_datawindow').find('input,select,button' + findname).attr('disabled', status)
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="HidColname" runat="server" />
    <asp:HiddenField ID="Hidmem_no" runat="server" />
    <asp:HiddenField ID="Hdoc_no" runat="server" />
    <asp:HiddenField ID="Hidgroup" runat="server" />
    <asp:HiddenField ID="Hidlast_focus" runat="server" />
    <asp:HiddenField ID="Hidcheckgain" runat="server" />
    <asp:HiddenField ID="Hidmembsection" runat="server" />
    <asp:HiddenField ID="HidBtnClick" runat="server" />
    <asp:HiddenField ID="H_periodshare_value" runat="server" />
    <asp:HiddenField ID="H_periodbase_value" runat="server" />
    <asp:HiddenField ID="HdChkRepteName" runat="server" />
    <asp:HiddenField ID="HdChkSalaryId" runat="server" />
    <asp:HiddenField ID="Max_periodshare_value" runat="server" />
    <asp:HiddenField ID="HdIsPostBack" runat="server" Value="false" />
    <asp:HiddenField ID="HdChkRowDel" runat="server" Value="false" />
    <asp:HiddenField ID="HdBankCode" runat="server" Value="false" />
    <asp:HiddenField ID="HdBankBranchCode" runat="server" Value="false" />
    <asp:HiddenField ID="HdTrTypeCode" runat="server" Value="false" />
    <asp:HiddenField ID="HdMoneyType" runat="server" Value="false" />
    <asp:HiddenField ID="Hidexpbank" runat="server" Value="false" />
    <asp:HiddenField ID="HdRow" runat="server" Value="false" />
    <asp:HiddenField ID="Hdtextage" runat="server" Value="" />
    <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="TextBox2" runat="server" Visible="false"></asp:TextBox>
    <dw:WebDataWindowControl ID="dw_main" runat="server" ClientScriptable="True" DataWindowObject="d_mbsrv_req_appl"
        LibraryList="~/DataWindow/mbshr/sl_member_new.pbl" ClientEventItemChanged="itemChange"
        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        ClientEvents="True" ClientFormatting="True" TabIndex="1" ClientEventClicked="dw_mainClick"
        ClientEventButtonClicked="b_searchClick">
    </dw:WebDataWindowControl>
    <%--    <table style="width: 90%;">
        <tr>
            <td valign="top" style="width: 40%;">
                <br />
                <asp:Label ID="lbl_dw_share" runat="server" Text="การส่งหุ้น" CssClass="font14px"></asp:Label>
                <br />
                <dw:WebDataWindowControl ID="dw_data" runat="server" DataWindowObject="d_sl_reqapplshare"
                    LibraryList="~/DataWindow/mbshr/sl_member_new.pbl" ClientScriptable="True" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" TabIndex="400"
                    ClientEventClicked="dw_dataClick" ClientFormatting="True" ClientEventItemChanged="ChangedValue">
                </dw:WebDataWindowControl>
            </td>
            <!--<td valign="middle" style="width: 50%;" align="center">
                <span class="linkSpan" onclick="OnUpLoad()">อัปโหลดรูป </span>
                <asp:Image ID="Image1" runat="server" ImageAlign="Middle" Height="182px" Width="163px" />
            </td>-->
        </tr>
    </table>  --%>
    <dw:WebDataWindowControl ID="dw_moneytr" runat="server" ClientScriptable="True" DataWindowObject="d_mbsrv_req_applmoneytr"
        LibraryList="~/DataWindow/mbshr/sl_member_new.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEvents="True"
        ClientFormatting="True" TabIndex="500" ClientEventButtonClicked="dw_moneytrButtonClicked"
        ClientEventItemChanged="dw_moneytrItemChanged" ClientValidation="False">
    </dw:WebDataWindowControl>
    <br />
    <dw:WebDataWindowControl ID="dw_gain" runat="server" ClientScriptable="True" DataWindowObject="d_mbsrv_req_applgain"
        LibraryList="~/DataWindow/mbshr/sl_member_new.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEvents="True"
        ClientFormatting="True" TabIndex="500" ClientEventClicked="OnDwGainClick">
    </dw:WebDataWindowControl>
</asp:Content>
