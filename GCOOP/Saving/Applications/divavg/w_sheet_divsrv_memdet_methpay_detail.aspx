<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_divsrv_memdet_methpay_detail.aspx.cs" Inherits="Saving.Applications.divavg.w_sheet_divsrv_memdet_methpay_detail" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=postInit %>
    <%=postRefresh%>
    <%=postInsertRow %>
    <%=postInitMemberNo%>
    <%=postSetMoneytype %>
    <%=postSetDeptAccountNo %>
    <%=postSetSequestAmt%>
    <script type="text/JavaScript">

        //Function Default
        //=============================================================
        function Validate() {

            var sum_expenseamt = 0;
            var divavg_amt = 0;

            for (var i = 1; i <= objDw_detail.RowCount(); i++) {
                sum_expenseamt += parseFloat(objDw_detail.GetItem(i, "expense_amt"));
            }
            sum_expenseamt = parseFloat(sum_expenseamt).toFixed(2);
            divavg_amt = parseFloat(objDw_main.GetItem(1, "div_balamt") + objDw_main.GetItem(1, "avg_balamt")).toFixed(2);

            if (sum_expenseamt > divavg_amt) {
                alert("ยอดเงิน เกินยอดปันผล-เฉลี่ยคืน กรุณาตรวจสอบ");
            }
            else {
                if (sum_expenseamt < divavg_amt) {
                    alert("ยอดเงินเหลือ กรุณาตรวจสอบ");
                }
                return confirm("ยืนยันการบันทึกข้อมูล");
            }
        }

        function MenubarOpen() {

        }

        function MenubarNew() {
            postNewClear();
        }

        //Function Main
        //=============================================================
        function SetDeptAccount(coop_id, deptaccount_no) {
            Gcoop.GetEl("Hdcoopid").value = coop_id;
            Gcoop.GetEl("Hdacountno").value = deptaccount_no;
            postSetDeptAccountNo();
        }
        function GetMemberNoFromDialog(member_no) {
            Gcoop.GetEl("Hdmember_no").value = member_no;
            postInitMemberNo();
        }

        function SetExpense(expense_bank, expense_branch, expense_accid) {
            var row = Gcoop.GetEl("Hdrow").value;
            objDw_detail.SetItem(row, "expense_bank", expense_bank);
            objDw_detail.SetItem(row, "expense_branch", expense_branch);
            objDw_detail.SetItem(row, "expense_accid", expense_accid);
            objDw_detail.AcceptText();
        }

        function OnDwMainItemChange(s, r, c, v) {
            if (c == "member_no") {
                objDw_main.SetItem(1, "member_no", v);
                objDw_main.AcceptText();
                postInit();
            }
            else if (c == "div_year") {
                objDw_main.SetItem(1, "div_year", v);
                objDw_main.AcceptText();
                Gcoop.GetEl("Hddiv_year").value = v;
            }
        }

        function OnDwMainButtonClick(s, r, b) {
            if (b == "b_search_memno") {
                Gcoop.OpenIFrame("800", "500", "w_dlg_divsrv_search_mem.aspx", "");
            }
        }


        //Function Detail
        //=============================================================
        function AddRow() {
            postInsertRow();
        }

        function OnDwDetailItemChange(s, r, c, v) {
            Gcoop.GetEl("Hdrow").value = r + "";
            if (c == "paytype_code") {
                objDw_detail.SetItem(r, "paytype_code", v);
                objDw_detail.AcceptText();
                postRefresh();
            }
            else if (c == "methpaytype_code") {
                objDw_detail.SetItem(r, "methpaytype_code", v);
                objDw_detail.AcceptText();
                postSetMoneytype();
            }
            else if (c == "pay_amt") {
                objDw_detail.SetItem(r, "pay_amt", v);
                objDw_detail.AcceptText();

                var paytype_code = objDw_detail.GetItem(r, "paytype_code");
                if (paytype_code == "PEC") {
                    var pay_amt = objDw_detail.GetItem(r, "pay_amt");
                    if (pay_amt > 100) {
                        alert("ยอดเปอร์เซนต์ มากกว่า 100% กรุณากรอกข้อมูลใหม่");
                    }
                    else {
                        objDw_detail.SetItem(r, "pay_amt", v);
                        objDw_detail.AcceptText();
                    }
                }
            }
        }

        function OnDwDetailButtonClick(s, r, b) {
            if (b == "b_search_mthpaytype") {
                Gcoop.GetEl("Hdrow").value = r + "";
                var methpaytype_code = objDw_detail.GetItem(r, "methpaytype_code");
                var moneytype_code = objDw_detail.GetItem(r, "moneytype_code");
                if (methpaytype_code == "LON") {
                    Gcoop.OpenIFrame("300", "300", "w_dlg_divsrv_search_loan.aspx", "");
                }
                else if (methpaytype_code == "DEP") {
                    var member_no = objDw_main.GetItem(1, "member_no");
                    Gcoop.OpenIFrame("700", "300", "w_dlg_divsrv_search_dept.aspx", "?member_no=" + member_no);
                }
                else if (methpaytype_code == "CBT" || moneytype_code == "CHQ") {
                    var expense_bank = objDw_detail.GetItem(r, "expense_bank");
                    var expense_branch = objDw_detail.GetItem(r, "expense_branch");
                    var expense_accid = objDw_detail.GetItem(r, "expense_accid");
                    Gcoop.OpenIFrame2("600", "600", "w_dlg_search_bankbranch.aspx", "?expense_bank=" + expense_bank + "&expense_branch=" + expense_branch + "&expense_accid=" + expense_accid);
                }
                else if (methpaytype_code == "SHR") {
                    Gcoop.OpenIFrame("500", "300", "w_dlg_divsrv_search_share.aspx", "");
                }
            }
            else if (b == "b_del") {
                objDw_detail.DeleteRow(r);
            }
        }

        function OnDwDetailClick(s, r, c) {
            if (c == "sequest_flag") {
                Gcoop.GetEl("Hdrow").value = r + "";
                Gcoop.CheckDw(s, r, c, "sequest_flag", 1, 0);
                postSetSequestAmt();
            }
        }    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <br />
        <table style="width: 100%;">
            <tr>
                <td>
                    <dw:WebDataWindowControl ID="Dw_main" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientScriptable="True"
                        DataWindowObject="d_divsrv_memdet_methpay_main" LibraryList="~/DataWindow/divavg/divsrv_memdet_methpay_detail.pbl"
                        ClientEventItemChanged="OnDwMainItemChange" ClientEventButtonClicked="OnDwMainButtonClick">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <span class="linkSpan" onclick="AddRow()" style="font-family: Tahoma; font-size: small;
                        float: left; color: #3333CC;">เพิ่มแถว </span>
                </td>
            </tr>
            <tr>
                <td>
                    <dw:WebDataWindowControl ID="Dw_detail" runat="server" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                        ClientScriptable="True" DataWindowObject="d_divsrv_memdet_methpay_detail" LibraryList="~/DataWindow/divavg/divsrv_memdet_methpay_detail.pbl"
                        ClientEventButtonClicked="OnDwDetailButtonClick" ClientEventItemChanged="OnDwDetailItemChange"
                        ClientEventClicked="OnDwDetailClick">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="Hdrow" runat="server" />
                    <asp:HiddenField ID="hdbank" runat="server" />
                    <asp:HiddenField ID="hdbranch" runat="server" />
                    <asp:HiddenField ID="hdaccid" runat="server" />
                    <asp:HiddenField ID="Hdmember_no" runat="server" />
                    <asp:HiddenField ID="Hdcoopid" runat="server" />
                    <asp:HiddenField ID="Hdacountno" runat="server" />
                    <asp:HiddenField ID="Hddiv_year" runat="server" />
                </td>
            </tr>
        </table>
        <br />
    </p>
</asp:Content>
