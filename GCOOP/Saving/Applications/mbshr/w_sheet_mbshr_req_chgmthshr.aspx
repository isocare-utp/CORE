<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_mbshr_req_chgmthshr.aspx.cs"
    Inherits="Saving.Applications.mbshr.w_sheet_mbshr_req_chgmthshr" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=memNoItemChange%>
    <%=initJavaScript%>
    <%= memNoFromDlg%>
    <%=newClear%>
    <%=jsChanegValue %>
    <%=jsGetShareBase %>
    <%=jsCheckStopShare %>
    <%=postSalaryId%>
    <script type="text/javascript">
        function itemChanged(sender, rowNumber, columnName, newValue) {
            if (columnName == "member_no") {
                objdw_data.SetItem(rowNumber, columnName, newValue);
                objdw_data.AcceptText();
                Gcoop.GetEl("Hfmember_no").value = newValue;
                memNoItemChange();

                Gcoop.SetLastFocus("member_no_0");
                Gcoop.Focus();
            }
            else if (columnName == "salary_id") {
                var str_temp = window.location.toString();
                var str_arr = str_temp.split("?", 2);
                sender.SetItem(rowNumber, columnName, newValue);
                sender.AcceptText();
                postSalaryId();
            }
            else if (columnName == "new_periodvalue") {
                objdw_data.SetItem(rowNumber, columnName, newValue);
                var oldValue = objdw_data.GetItem(1, "old_periodvalue");
                objdw_data.AcceptText();
                jsChanegValue();
            }

            else if (columnName == "new_paystatus") {
                objdw_data.SetItem(rowNumber, columnName, newValue);
                objdw_data.AcceptText();
                jsCheckStopShare();

            }
            return 0;
        }

        function ValidateForm() {
            var alertstr = "";
            var shrlast_period = objdw_data.GetItem(1, "shrlast_period");
            var new_paystatus = objdw_data.GetItem(1, "new_paystatus");

            if (new_paystatus == -1) {
                if (shrlast_period <= 179) { alertstr = alertstr + "- งวดส่งหุ้นไม่ถึง 180\n"; }
            }

            if (alertstr != "") {
                // alert(alertstr);
                return confirm("งวดส่งหุ้นไม่ถึง 180 งวด ต้องการบันทึกรายการหรือไม่");
            } else {

                return true;
            }
        }

        function Validate() {
            var result = true;
            result = ValidateForm();
            if (result) {
                objdw_data.AcceptText();
                return confirm("ยืนยันการบันทึกข้อมูล");

            }
        }

        function DwButtonClick(sender, rowNumber, buttonName) {
            if (buttonName == "b_memsearch") {
                Gcoop.OpenDlg("600", "590", "w_dlg_sl_member_search.aspx", "")
            }
            return 0;
        }

        function GetValueFromDlg(memberno) {
            objdw_data.SetItem(1, "member_no", memberno);
            objdw_data.AcceptText();
            Gcoop.GetEl("Hfmember_no").value = memberno;
            memNoItemChange();
        }

        function GetDocNoFromDlg(docno) {
            Gcoop.GetEl("HdDocno").value = docno;
            memNoFromDlg();
        }
        //        function GetMemDetFromDlg(memberno) {
        //            objdw_data.SetItem(1, "member_no", memberno);
        //            objdw_data.AcceptText();
        //            Gcoop.GetEl("Hfmember_no").value = memberno;
        //            memNoFromDlg();
        //        }
        function MenubarNew() {
            Gcoop.SetLastFocus("member_no_0");
            Gcoop.Focus();
            newClear();

        }
        function MenubarOpen() {
            Gcoop.OpenDlg('580', '590', 'w_dlg_sl_member_chgshr_search.aspx', '');
        }
        function SheetLoadComplete() {
            if (Gcoop.GetEl("HdIsPostBack").value != "true") {
                // alert(Gcoop.GetEl("HdIsPostBack").value);
                Gcoop.SetLastFocus("member_no_0");
                Gcoop.Focus();
            }
        }

        function checkStatus(s, r, c) {
            if (c == "apvimmediate_flag") {
                Gcoop.CheckDw(s, r, c, "apvimmediate_flag", 1, 0);
            }
        }
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="Hfmember_no" runat="server" />
    <asp:HiddenField ID="H_periodshare_value" runat="server" />
    <asp:HiddenField ID="Max_periodshare_value" runat="server" />
    <asp:HiddenField ID="HdIsPostBack" runat="server" Value="false" />
    <asp:HiddenField ID="HidMemcoopid" runat="server" Value="false" />
    <asp:HiddenField ID="HdDocno" runat="server" Value="false" />
    <dw:WebDataWindowControl ID="dw_data" runat="server" DataWindowObject="d_mbsrv_req_chgmthshr"
        LibraryList="~/DataWindow/mbshr/sl_shrpayment_adjust.pbl" ClientScriptable="True"
        ClientEventItemChanged="itemChanged" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientEventButtonClicked="DwButtonClick"
        ClientFormatting="True" ClientEventClicked="checkStatus">
    </dw:WebDataWindowControl>
</asp:Content>
