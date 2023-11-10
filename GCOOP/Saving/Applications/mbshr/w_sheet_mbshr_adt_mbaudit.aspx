<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_mbshr_adt_mbaudit.aspx.cs" Inherits="Saving.Applications.mbshr.w_sheet_mbshr_adt_mbaudit" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=jsPostMember%>
    <%=changeDistrict%>
    <%=newClear%>
    <%=jsGetTambol%>
    <%=jsSetData%>
    <%=jsCallRetry%>
    <%=jsIdCard%>
    <%=jsGetShareBase%>
    <%=setpausekeep_date %>
    <%=jsChanegValue%>
    <%=jsGetCurrTambol%>
    <%=jsGetCurrDistrict%>
    <%=jsLinkAddress%>
    <%=jsExpense%>
    <%=jsBank%>
    <%=jsAddRow%>
    <%=jsMoneytypeCode%>
    <script type="text/JavaScript">

        function Validate() {
            objdw_main.AcceptText();
            //            objdw_status.AcceptText();
            //            objdw_moneytr.AcceptText();

            //            Gcoop.SetLastFocus("member_no_0");
            //            Gcoop.Focus();

            return true;
        }
        function showTabPage(tab) {
            var i = 1;
            var tabamount = 2;
            for (i = 1; i <= tabamount; i++) {
                document.getElementById("tab" + i).style.visibility = "hidden";
                document.getElementById("stab" + i).style.backgroundColor = "rgb(211,213,255)";
                if (i == tab) {
                    document.getElementById("tab" + i).style.visibility = "visible";
                    document.getElementById("stab" + i).style.backgroundColor = "rgb(200, 235, 255)";
                    Gcoop.GetEl("HiddenFieldTab").value = i + "";
                }
            }
        }
        function SheetLoadComplete() {
            var CurTab = Gcoop.ParseInt(Gcoop.GetEl("HiddenFieldTab").value);

            if (isNaN(CurTab)) {
                CurTab = 1;
            }
            showTabPage(CurTab);
            if (Gcoop.GetEl("HdIsPostBack").value != "true") {
                Gcoop.SetLastFocus("member_no_0");
                Gcoop.Focus();
                var member_no = objdw_main.GetItem(1, "member_no");
                objdw_main.SetItem(1, "member_no", member_no);
            }
        }

        function MenubarOpen() {
            Gcoop.OpenDlg('580', '590', 'w_dlg_sl_member_search.aspx', '');
        }

        function MenubarNew() {
            Gcoop.SetLastFocus("member_no_0");
            Gcoop.Focus();
            newClear();

        }
        function GetValueFromDlg(memberno) {
            objdw_main.SetItem(1, "member_no", memberno);
            objdw_main.AcceptText();
            Gcoop.GetEl("Hfmember_no").value = memberno;
            jsPostMember();
        }
        
        function ItemChangedMain(sender, rowNumber, columnName, newValue) {
            if (columnName == "member_no") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hfmember_no").value = objdw_main.GetItem(rowNumber, "member_no");
                jsPostMember();
            }
            else if (columnName == "province_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.SetLastFocus("amphur_code");
                changeDistrict();
            }
            else if (columnName == "amphur_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.SetLastFocus("tambol_code_0");
                jsGetTambol();
            }
            else if (columnName == "currprovince_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.SetLastFocus("curramphur_code");
                jsGetCurrDistrict();
            }
            else if (columnName == "curramphur_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.SetLastFocus("currtambol_code");
                jsGetCurrTambol();
            }
            else if (columnName == "birth_tdate") {
                objdw_main.SetItem(1, "birth_tdate", newValue);
                objdw_main.AcceptText();
                objdw_main.SetItem(1, "birth_date", Gcoop.ToEngDate(newValue));
                objdw_main.AcceptText();

                //                objdw_main.SetItem(rowNumber, columnName, newValue);
                //                objdw_main.AcceptText();
                Gcoop.GetEl("Hbirth_tdate").value = newValue;
                Gcoop.SetLastFocus("birth_tdate_0");
                jsCallRetry();
            }
            else if (columnName == "card_person") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.SetLastFocus("card_person_0");
                jsIdCard();
            }
            else if (columnName == "salary_amount") {
                var salaryAmt_old = parseFloat(Gcoop.GetEl("Hsalary_amount").value);
                var salaryAmt_new = parseFloat(newValue);

                if (salaryAmt_old < salaryAmt_new) {
                    objdw_main.SetItem(rowNumber, columnName, newValue);
                    objdw_main.AcceptText();
                    jsGetShareBase();
                } else {
                    if (confirm("เงินเดือนใหม่ที่ป้อนน้อยกว่า เงินเดือนปัจจุปัน \nท่านต้องการทำรายการต่อหรือไม่ ?")) {
                        objdw_main.SetItem(rowNumber, columnName, newValue);
                        objdw_main.AcceptText();                        
                        jsGetShareBase();
                    } else {
                        objdw_main.SetItem(rowNumber, columnName, salaryAmt_old);
                        objdw_main.AcceptText();
                    }
                }
            }
            else if (columnName == "incomeetc_amt") {
                var incomeetc_amt_old = parseFloat(Gcoop.GetEl("Hincomeetc_amt").value);
                var incomeetc_amt_new = parseFloat(newValue);

                if (incomeetc_amt_old < incomeetc_amt_new) {
                    objdw_main.SetItem(rowNumber, columnName, newValue);
                    objdw_main.AcceptText();
                    jsGetShareBase();
                } else {
                    if (confirm("เงินอื่นๆใหม่ที่ป้อนน้อยกว่า เงินอื่นๆปัจจุปัน \nท่านต้องการทำรายการต่อหรือไม่ ?")) {
                        objdw_main.SetItem(rowNumber, columnName, newValue);
                        objdw_main.AcceptText();
                        jsGetShareBase();
                    } else {
                        objdw_main.SetItem(rowNumber, columnName, incomeetc_amt_old);
                        objdw_main.AcceptText();
                    }
                }
            }

            else if (columnName == "expense_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                jsExpense();
            }
            else if (columnName == "expense_bank") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                jsBank();
            }
            else if (columnName == "branch_name") {
                sender.SetItem(rowNumber, columnName, newValue);
                sender.AcceptText();
                var bank_code = objdw_moneytr.GetItem(1, "bank_code");
                Gcoop.GetEl("Hidlast_focus").value = "bank_accid";
                Gcoop.OpenDlg("450", "600", "w_dlg_mb_search_bankbranch.aspx", "?seach_key=" + newValue + "&bankCode=" + bank_code);
            }

            if (Gcoop.GetEl("HdIsPostBack").value == "false") {
                SheetLoadComplete();
            }
        }

        function ItemChangedDwmoneytr(s, r, c, v){
            if (c == "branch_name") {
                s.SetItem(r, c, v);
                s.AcceptText();
                Gcoop.GetEl("HdRows").value = r;
                var BankCode = objdw_moneytr.GetItem(r, "bank_code");
                Gcoop.OpenIFrame("450", "600", "w_dlg_mb_search_bankbranch.aspx", "?seach_key=" + v + "&bankCode=" + BankCode);
            }
            else if (c == "moneytype_code") {
                s.SetItem(r, c, v);
                s.AcceptText();
                jsMoneytypeCode();
            }
        }

        function checkStatus(s, r, c) {
            Gcoop.CheckDw(s, r, c, "klongtoon_flag", 1, 0);
            Gcoop.CheckDw(s, r, c, "transright_flag", 1, 0);
            Gcoop.CheckDw(s, r, c, "allowloan_flag", 1, 0);
            Gcoop.CheckDw(s, r, c, "sequest_divavg", 1, 0);
            Gcoop.CheckDw(s, r, c, "dividend_flag", 1, 0);
            Gcoop.CheckDw(s, r, c, "average_flag", 1, 0);
            Gcoop.CheckDw(s, r, c, "divavgshow_flag", 1, 0);
            Gcoop.CheckDw(s, r, c, "insurance_flag", 1, 0);
        }

        function itemdw_status(sender, rowNumber, columnName, newValue) {
            if (columnName == "appltype_code") {
                objdw_status.SetItem(rowNumber, columnName, newValue);
                objdw_status.AcceptText();
            }
            else if (columnName == "pausekeep_flag") {
                objdw_status.SetItem(rowNumber, columnName, newValue);
                objdw_status.AcceptText();
                setpausekeep_date();
            }
        }

        function OnInsert() {
            jsInsertRow();
        }

        function OnDeleteMoneytr(s, r, c) {
            if (c == "b_delete") {
                var detail = "รหัส " + objdw_moneytr.GetItem(r, "moneytype_code");
                detail += " : " + objdw_moneytr.GetItem(r, "bank_accid");
                if (confirm("คุณต้องการลบรายการ " + detail + " ใช่หรือไม่?")) {
                    objdw_moneytr.DeleteRow(r);
                }
            }
            return 0;
        }

        function checkRemarkstat(s, r, c) {
            Gcoop.CheckDw(s, r, c, "operate_flag", 1, 0);
            if (c == "operate_flag") {
                if (objdw_remarkstat.GetItem(r, "operate_flag") == 1) {
                    Gcoop.GetEl("Hrow").value = r + "";
                    jsSetData();
                }
            }
        }
        function OnUpLoad() {
            if (Gcoop.GetEl("Hfmember_no").value != "") {
                var member_no = Gcoop.GetEl("Hfmember_no").value;
                Gcoop.OpenDlg("570", "590", "w_dlg_picture.aspx", "?member=" + member_no);
                // getpicture();
            }
            else {
                alert("ไม่พบเลขสมาชิก");
            }
        }
        function GetShow() { jsgetpicMember_no(); }

        function GetValueFromDlgSeachBankBranch(Bank_code, Bank_desc) {
            var row = Gcoop.GetEl("HdRows").value;
            objdw_moneytr.SetItem(row, "bank_branch", "");
            objdw_moneytr.SetItem(row, "branch_name", "");
            objdw_moneytr.SetItem(row, "bank_branch", Gcoop.Trim(Bank_code));
            objdw_moneytr.SetItem(row, "branch_name", Gcoop.Trim(Bank_desc));
            objdw_moneytr.AcceptText();
        }

        //        function SheetLoadComplete() {
        //            if (Gcoop.GetEl("HdIsPostBack").value != "true") {

        //                Gcoop.SetLastFocus("member_no_0");
        //                Gcoop.Focus();
        //            }
        //        } 
        function dw_mainClick(s, r, c) {
            if (c == "b_linkaddress") {
                Gcoop.CheckDw(s, r, c, "b_linkaddress", 1, 0);
                jsLinkAddress();
            }
            if (c == "b_search") {
                Gcoop.OpenDlg('580', '590', 'w_dlg_sl_member_search.aspx', '');
            }
        }

        function dw_moneytrButtonClick(s, r, c) {
//            if (c == "b_branch") {
//                Gcoop.GetEl("HdRows").value = r;
//                newValue = objdw_moneytr.GetItem(r, "branch_name");
//                var bank_code = objdw_moneytr.GetItem(r, "bank_code");
//                if (bank_code == "" || bank_code == null) {
//                    alert('กรุณาเลือกธนาคาร');
//                } else {
//                    Gcoop.OpenIFrame('580', '590', 'w_dlg_mb_search_bankbranch.aspx', "?seach_key=" + newValue + "&bankCode=" + bank_code);
//                }
//            }
             if (c == "b_del") {
                if (confirm("คุณต้องการลบรายการ ใช่หรือไม่?")) {
                    objdw_moneytr.DeleteRow(r);
                }
            }
        }

        function OnClickAddRow() {
            jsAddRow();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="Hfmember_no" runat="server" />
    <asp:HiddenField ID="Hbirth_tdate" runat="server" />
    <asp:HiddenField ID="Hperiodshare_value" runat="server" />
    <asp:HiddenField ID="Hsalary_amount" runat="server" />
    <asp:HiddenField ID="Hincomeetc_amt" runat="server" />
    <asp:HiddenField ID="HdIsPostBack" runat="server" Value="false" />
    <asp:HiddenField ID="HdRows" runat="server" />
    <asp:TextBox ID="TextDwmain" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="Textdwdata1" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="Textdwdata2" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="Textdwdata3" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="Textdwdata4" runat="server" Visible="False"></asp:TextBox>
    <asp:HiddenField ID="HiddenFieldTab" runat="server" />
    <asp:HiddenField ID="Hrow" runat="server" />
    <div>
        <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_mb_audit_membmaster"
            LibraryList="~/DataWindow/mbshr/mb_member_audit.pbl" ClientScriptable="True"
            ClientEventButtonClicked="ButtonClick" AutoRestoreContext="False" AutoRestoreDataCache="True"
            AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientEventItemChanged="ItemChangedMain"
            ClientEventClicked="dw_mainClick">
        </dw:WebDataWindowControl>
    </div>
    <br />
    <div align="right" style="margin-right: 1px; width: 765px;">
        <span class="NewRowLink" onclick="OnClickAddRow()" id="add_row" runat="server">เพิ่มแถว</span>
    </div>
    <div>
        <dw:WebDataWindowControl ID="dw_moneytr" runat="server" DataWindowObject="d_mb_audit_membmoneytr"
            LibraryList="~/DataWindow/mbshr/mb_member_audit.pbl" ClientScriptable="True"
            ClientEventButtonClicked="dw_moneytrButtonClick" AutoRestoreContext="False" AutoRestoreDataCache="True"
            AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientEventItemChanged="ItemChangedDwmoneytr"
            ClientEventClicked="dw_moneytrClick">
        </dw:WebDataWindowControl>
    </div>
</asp:Content>
