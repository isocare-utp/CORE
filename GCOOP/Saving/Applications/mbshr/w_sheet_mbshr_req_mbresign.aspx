<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_mbshr_req_mbresign.aspx.cs"
    Inherits="Saving.Applications.mbshr.w_sheet_mbshr_req_mbresign" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=memNoItemChange%>
    <%=memNoFromDlg %>
    <%=newClear %>
    <%=dateChange%>
    <%=jsCoopSelect %>
    <%=jsResignCause %>
    <%=postSalaryId%>
    <%=postResigndtfalg %>
    <%=postApv%>
    <script type="text/javascript">
        function Validate() {
            objdw_sum.AcceptText();
            objdw_share.AcceptText();
            objdw_loan.AcceptText(); ;
            objdw_grt.AcceptText();
            objdw_head.AcceptText();
            objdw_deposit.AcceptText();

            var ls_resc = objdw_head.GetItem(1, "resigncause_code");
            if (ls_resc.trim().length == 0) {
                alert("กรุณาระบุเหตุผลการลาออกด้วย");
                return false;
            }

            var warning = "";
            var dep = 0, loan = 0, coll = 0;
            dep = objdw_sum.GetItem(1, "dep_count");
            if (dep > 0) {
                warning = "มีบัญชีเงินฝาก " + dep + " บัญชี\n";
            }
            loan = objdw_sum.GetItem(1, "loan_count");
            if (loan > 0) {
                warning += "มีสัญญาเงินกู้ " + loan + " สัญญา\n";
            }
            coll = objdw_sum.GetItem(1, "coll_count");
            if (coll > 0) {
                warning += "มีการค้ำประกัน " + coll + " สัญญา\n";
            }
            if (warning != "") { warning = "สมาชิกท่านนี้\n" + warning; }
            return confirm(warning + "\nยืนยันการบันทึกข้อมูล");
        }

        function showTabPage(sender, rowNumber, buttonName) {
            if (buttonName == "b_share") {
                document.getElementById("tab_1").style.visibility = "visible";
                document.getElementById("tab_2").style.visibility = "hidden";
                document.getElementById("tab_3").style.visibility = "hidden";
                document.getElementById("tab_4").style.visibility = "hidden";
            } else if (buttonName == "b_deposit") {
                document.getElementById("tab_1").style.visibility = "hidden";
                document.getElementById("tab_2").style.visibility = "hidden";
                document.getElementById("tab_3").style.visibility = "hidden";
                document.getElementById("tab_4").style.visibility = "visible";
            } else if (buttonName == "b_loan") {
                document.getElementById("tab_1").style.visibility = "hidden";
                document.getElementById("tab_2").style.visibility = "visible";
                document.getElementById("tab_3").style.visibility = "hidden";
                document.getElementById("tab_4").style.visibility = "hidden";
            } else if (buttonName == "b_coll") {
                document.getElementById("tab_1").style.visibility = "hidden";
                document.getElementById("tab_2").style.visibility = "hidden";
                document.getElementById("tab_3").style.visibility = "visible";
                document.getElementById("tab_4").style.visibility = "hidden";
            }
        }
        function DwItemChange(s, r, c, v) {
            if (c == "member_no") {
                objdw_head.SetItem(r, c, v);
                objdw_head.AcceptText();
                Gcoop.GetEl("Hfmember_no").value = objdw_head.GetItem(r, "member_no");
                memNoItemChange();
            }
            else if (c == "approve_tdate") {
                objdw_head.SetItem(r, c, v);
                objdw_head.AcceptText();
                dateChange();
            }
            else if (c == "check_resigncause") {
                objdw_head.SetItem(r, c, v);
                objdw_head.AcceptText();
                jsResignCause();
            }
            else if (c == "salary_id") {
                var str_temp = window.location.toString();
                var str_arr = str_temp.split("?", 2);
                s.SetItem(r, c, v);
                s.AcceptText();
                postSalaryId();
            }
            else if (c == "resigndtfix_flag") {
                s.SetItem(r, c, v);
                s.AcceptText();
                postResigndtfalg();
            }
            else if (c == "apvflag") {
                s.SetItem(r, c, v);
                s.AcceptText();
                postApv();
            }
            //            return 0;
        }
        function DwButtonClick(sender, rowNumber, buttonName) {
            if (buttonName == "b_open") {
                Gcoop.OpenDlg("600", "590", "w_dlg_sl_member_search.aspx", "")
            }
            return 0;
        }
        function OnDwCoopClick(s, r, c) {
            if (c == "check_flag") {
                Gcoop.CheckDw(s, r, c, "check_flag", 1, 0);
                jsCoopSelect();
            }
        }
        function GetValueFromDlg(memberno) {
            objdw_head.SetItem(1, "member_no", memberno);
            objdw_head.AcceptText();
            Gcoop.GetEl("Hfmember_no").value = memberno;
            memNoItemChange();
        }
        function GetDocNoFromDlg(docno) {
            Gcoop.GetEl("HdDocno").value = docno;
            memNoFromDlg();
        }

        function MenubarNew() {
            Gcoop.SetLastFocus("member_no_0");
            Gcoop.Focus();
            newClear();
        }
        function MenubarOpen() {
            Gcoop.OpenDlg('580', '590', 'w_dlg_sl_member_resign_search.aspx', '');
        }
        function checkStatus(s, r, c) {
            //            if (c == "apvflag") {
            //                Gcoop.CheckDw(s, r, c, "apvflag", 1, 0);
            //                postApv();
            //            }
            //            if (c == "resigndtfix_flag") {
            //                //Gcoop.CheckDw(s, r, c, "resigndtfix_flag", 1, 0);
            //                postResigndtfalg();

            ////                objdw_head.SetItem(r, c, v);
            ////                objdw_head.AcceptText();
            ////                postResigndtfalg();
            //            }
        }
        function SheetLoadComplete() {

            if (Gcoop.GetEl("HdIsPostBack").value != "true") {
                Gcoop.SetLastFocus("member_no_0");
                Gcoop.Focus();
            }
            Disable_ALL();
        }

        function Disable_ALL() {
            DisabledTable(objdw_head, "resignreq_status", "dw_head", null);
            DisabledTable(objdw_head, "resignreq_status", "dw_sum", null);
            DisabledTable(objdw_head, "resignreq_status", "dw_share", null);
            DisabledTable(objdw_head, "resignreq_status", "dw_loan", null);
            DisabledTable(objdw_head, "resignreq_status", "dw_grt", null);
            DisabledTable(objdw_head, "resignreq_status", "dw_deposit", null);
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
    <asp:HiddenField ID="Hfmember_no" runat="server" />
    <asp:HiddenField ID="HdIsPostBack" runat="server" Value="false" />
    <asp:HiddenField ID="HdCheckSave" runat="server" Value="false" />
    <asp:HiddenField ID="HdDocno" runat="server" />
    รายละเอียด
    <dw:WebDataWindowControl ID="dw_head" runat="server" DataWindowObject="d_mbsrv_req_resign"
        LibraryList="~/DataWindow/mbshr/sl_member_resign.pbl" ClientScriptable="True"
        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        ClientEventItemChanged="DwItemChange" ClientEventButtonClicked="DwButtonClick"
        ClientFormatting="True" TabIndex="1" ClientEventClicked="checkStatus" ClientEvents="True"
        Style="top: 0px; left: 0px">
    </dw:WebDataWindowControl>
    <table style="width: 100%;" border="1">
        <tr>
            <td width="160px">
                <br />
                <dw:WebDataWindowControl ID="dw_sum" runat="server" DataWindowObject="d_mbsrv_req_resignsum"
                    LibraryList="~/DataWindow/mbshr/sl_member_resign.pbl" ClientScriptable="True"
                    ClientEventButtonClicked="showTabPage" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" TabIndex="100">
                </dw:WebDataWindowControl>
            </td>
            <td valign="top" style="padding-left: 5px;">
                <div id="tab_1" style="visibility: visible; position: absolute;">
                    &nbsp;&nbsp;รายละเอียดหุ้น
                    <dw:WebDataWindowControl ID="dw_share" runat="server" DataWindowObject="d_mbsrv_req_resignshare"
                        LibraryList="~/DataWindow/mbshr/sl_member_resign.pbl" Width="540px" ClientScriptable="True"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        TabIndex="200">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab_2" style="visibility: hidden; position: absolute;">
                    &nbsp;&nbsp;รายละเอียดสัญญาเงินกู้
                    <dw:WebDataWindowControl ID="dw_loan" runat="server" DataWindowObject="d_mbsrv_req_resignloan"
                        LibraryList="~/DataWindow/mbshr/sl_member_resign.pbl" Width="540px" Height="298px"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientScriptable="True" TabIndex="300">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab_3" style="visibility: hidden; position: absolute;">
                    &nbsp;&nbsp;รายละเอียดการค้ำประกัน
                    <dw:WebDataWindowControl ID="dw_grt" runat="server" DataWindowObject="d_mbsrv_req_resigngrt"
                        LibraryList="~/DataWindow/mbshr/sl_member_resign.pbl" Width="540px" Height="298px"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientScriptable="True" TabIndex="400">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab_4" style="visibility: hidden; position: absolute;">
                    &nbsp;&nbsp;รายละเอียดเงินฝาก
                    <dw:WebDataWindowControl ID="dw_deposit" runat="server" DataWindowObject="d_mbsrv_req_resigndeposit"
                        LibraryList="~/DataWindow/mbshr/sl_member_resign.pbl" Width="540px" ClientScriptable="True"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        TabIndex="600">
                    </dw:WebDataWindowControl>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
