<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_mbshr_req_chggrp.aspx.cs" Inherits="Saving.Applications.mbshr.w_sheet_mbshr_req_chggrp" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postNewClear%>
    <%=postRefresh%>
    <%=postInit %>
    <%=postFilterSection %>
    <%=postInitMember%>
    <%=postSetNewGroup%>
    <%=postSetDocno%>
    <%=postAddRow %>
    <%=postClearText %>
    <%=postSalaryId%>
    <%=postExpenseCode %>
    <%=postApv%>
    <%=postExpenseBank%>
    <script type="text/javascript">
        function Validate() {
            objDw_main.AcceptText();
            confirm("ยืนยันการบันทึกข้อมูล");
            return true;  
            
        }

        function AddRow() {
            postAddRow();
        }

        //============================================
        //Function GET
        function GetBankFromDlg(bank_code) {
            objDw_expense.SetItem(Gcoop.GetEl("Hd_row").value, "expense_bank", bank_code);
            objDw_expense.AcceptText();

        }
        function GetBankBranchFromDlg(branch_id) {
            objDw_expense.SetItem(Gcoop.GetEl("Hd_row").value, "expense_branch", branch_id);
            objDw_expense.AcceptText();
        }

        function GetDeptNoFromDlg(deptaccount_no) {
            objDw_expense.SetItem(Gcoop.GetEl("Hd_row").value, "expense_accid", deptaccount_no);
            objDw_expense.AcceptText();
        }

        function GetDocNoFromDlg(docno) {
            Gcoop.GetEl("Hd_docno").value = docno;
            postSetDocno();
        }

        function GetValueFromDlg(memberno) {
            objDw_main.SetItem(1, "member_no", memberno);
            objDw_main.AcceptText();
            Gcoop.GetEl("Hdmember_no").value = memberno;
            postInit();
        }

        function GetMemGroupFromDlg(membgroup_code) {
            objDw_main.SetItem(1, "new_group", membgroup_code);
            objDw_main.AcceptText();
            Gcoop.GetEl("Hd_membgroup").value = membgroup_code;
            postSetNewGroup();
        }

        //============================================
        //Dw_expense
        function OnDwexpense_ItemChange(s, r, c, v) {
            Gcoop.GetEl("Hd_row").value = r + "";
            if (c == "chg_status") {
                objDw_expense.SetItem(r, "chg_status", v);
                objDw_expense.AcceptText();
                postRefresh();
            }
            else if (c == "moneytype_code") {
                objDw_expense.SetItem(r, "moneytype_code", v);
                objDw_expense.AcceptText();
                postClearText();
            }
            else if (c == "monthlycut_type") {
                objDw_expense.SetItem(r, "monthlycut_type", v);
                objDw_expense.AcceptText();
                postRefresh();
            }
        }

        function OnDwexpense_ButtonClick(s, r, b) {
            Gcoop.GetEl("Hd_row").value = r + "";
            if (b == "b_search_expbank") {
                Gcoop.OpenIFrame('580', '590', 'w_dlg_kp_bank_search.aspx', '');
            }
            else if (b == "b_search_expbranch") {
                var bank_code = objDw_expense.GetItem(r, "expense_bank");
                Gcoop.OpenIFrame(580, 590, "w_dlg_kp_bankbranch_search.aspx", "?bank_code=" + bank_code + "");
            }
            else if (b == "b_search_deptno") {
                var member_no = objDw_main.GetItem(1, "member_no");
                Gcoop.OpenIFrame(580, 590, "w_dlg_kp_deptaccount_search.aspx", "?member_no=" + member_no + "");
            }
            else if (b == "b_del") {
                if (confirm("คุณต้องการลบรายการแถว " + r + " ใช่หรือไม่?")) {
                    objDw_expense.DeleteRow(r);
                }
            }

        }
        //=========================================
        //DW_main
        function OnDwmain_ButtonClick(s, r, b) {
            if (b == "b_sch_memno") {
                Gcoop.OpenIFrame('600', '590', 'w_dlg_sl_member_search.aspx', '');
            }
            else if (b == "b_group") {
                Gcoop.OpenIFrame('580', '590', 'w_dlg_search_memgroup.aspx', '');
            }
            else if (b == "b_nw_expbank") {
                Gcoop.OpenIFrame('580', '590', 'w_dlg_kp_bank_search.aspx', '');                
            }
            else if (b == "b_nw_expbranch") {
                var bank_code = objDw_main.GetItem(1, "expense_bank");
                Gcoop.OpenIFrame('580', '590', 'w_dlg_kp_bankbranch_search.aspx', "?bank_code=" + bank_code);
            }
            else if (b == "b_nw_expaccid") {
                var member_no = objDw_main.GetItem(1, "member_no");
                var memcoop_id = objDw_main.GetItem(1, "memcoop_id");
                Gcoop.OpenIFrame('720', '420', 'w_dlg_divsrv_search_dept.aspx', "?member_no=" + member_no + "&memcoop_id=" + memcoop_id);
            }
        }

        function GetBankFromDlg(bank_code) {
            objDw_main.SetItem(1, "expense_bank", bank_code);
            objDw_main.AcceptText();
            postExpenseBank();
        }

        function GetBankBranchFromDlg(branch_id) {
            objDw_main.SetItem(1, "expense_branch", branch_id);
            objDw_main.AcceptText();
        }

        function SetDeptAccount(deptaccount_no) {
            objDw_main.SetItem(1, "expense_accid", Gcoop.Trim(deptaccount_no));
            objDw_main.AcceptText();
        }

        function OnDwmain_Click(s, r, c) {
            if (c == "expchg_flag") {
                Gcoop.CheckDw(s, r, c, "expchg_flag", 1, 0);
                postRefresh();
            }
            else if (c == "membtype_flag") {
                Gcoop.CheckDw(s, r, c, "membtype_flag", 1, 0);
                postRefresh();
            }
            else if (c == "apvflag") {
                Gcoop.CheckDw(s, r, c, "apvflag", 1, 0);
                postApv();
            }
        }
        function OnDwmain_ItemChange(s, r, c, v) {
            if (c == "member_no") {
                objDw_main.SetItem(1, "member_no", v);
                objDw_main.AcceptText();
                postInit();
            }
            else if (c == "new_group") {
                objDw_main.SetItem(1, "new_group", v);
                objDw_main.AcceptText();
                postFilterSection();
            }
            else if (c == "new_membsection") {
                objDw_main.SetItem(1, "new_membsection", v);
                objDw_main.AcceptText();
            }
            else if (c == "salary_id") {
                var str_temp = window.location.toString();
                var str_arr = str_temp.split("?", 2);                
                s.SetItem(r, c, v);
                s.AcceptText();
                postSalaryId();
            }
            else if (c == "expense_code") {
                objDw_main.SetItem(r, c, v);
                objDw_main.AcceptText();
                postExpenseCode();
            }
            else if (c == "expense_bank") {
                objDw_main.SetItem(r, c, v);
                objDw_main.AcceptText();
                postExpenseBank();
            }
        }
        //=============================================
        //Dw_coop
        function OnDwcoop_Click(s, r, c) {
            if (c == "check_flag") {
                Gcoop.CheckDw(s, r, c, "check_flag", 1, 0);
                postRefresh();
            }
        }
        //=========================================
        function MenubarNew() {
            postNewClear()
        }

        function MenubarNew() {
            Gcoop.SetLastFocus("member_no_0");
            Gcoop.Focus();
            newclear();
        }

        function MenubarOpen() {
            Gcoop.OpenIFrame('580', '590', 'w_dlg_sl_member_chggroup_search.aspx', '');
        }

        function Disable_ALL() {
            DisabledTable(objDw_main, "request_status", "Dw_main", null);
            DisabledTable(objDw_main, "request_status", "Dw_history", null);
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
    <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <dw:WebDataWindowControl ID="Dw_main" runat="server" DataWindowObject="d_mbsrv_req_chggrp"
            LibraryList="~/DataWindow/mbshr/mb_req_chggroup.pbl" ClientScriptable="True"
            AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
            ClientFormatting="True" TabIndex="1" ClientEventItemChanged="OnDwmain_ItemChange"
            ClientEventClicked="OnDwmain_Click" ClientEventButtonClicked="OnDwmain_ButtonClick">
        </dw:WebDataWindowControl>
        <br />
        <dw:WebDataWindowControl ID="Dw_history" runat="server" DataWindowObject="d_mbsrv_req_chggrphistory"
            LibraryList="~/DataWindow/mbshr/mb_req_chggroup.pbl" ClientScriptable="True"
            AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
            ClientFormatting="True" TabIndex="1">
        </dw:WebDataWindowControl>
        <asp:HiddenField ID="Hdmember_no" runat="server" />
        <asp:HiddenField ID="Hd_membgroup" runat="server" />
        <asp:HiddenField ID="Hd_docno" runat="server" />
        <asp:HiddenField ID="Hd_row" runat="server" />
        <br />
</asp:Content>
