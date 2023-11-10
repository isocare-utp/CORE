<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_paytrnbank.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_sheet_paytrnbank" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postMemberNo%>
    <%=postRefresh %>
    <%=postInsertRow%>
    <%=postDeleteRow%>
    <script type="text/javascript">

        function OnClickDeleteRow() {
            if (objDwDetail.RowCount() > 0) {
                var currentRow = Gcoop.GetEl("HdDetailRow").value;
                var confirmText = "ยืนยันการลบแถวที่ " + currentRow;
                if (confirm(confirmText)) {
                    postDeleteRow();
                }
            } else {
                alert("ยังไม่มีการเพิ่มแถวสำหรับรายการเช็ค");
            }
        }

        function OnClickInsertRow() {
            var memberno = objDwMain.GetItem(1, "member_no");
            if (memberno != null) {
                postInsertRow();
            }
            else {
                alert("กรุณากรอกเลขสมาชิก");
            }
        }

        function GetValueFromDlg(memberNo) {
            objDwMain.SetItem(1, "member_no", memberNo);
            objDwMain.AcceptText();
            postMemberNo();
        }

        function GetDeptAcc(deptaccount_no) {
            var row = Gcoop.GetEl("HdDetailRow").value;
            objDwDetail.SetItem(row, "document_no", deptaccount_no);
            objDwDetail.SetItem(row, "paytrnbank_desc", "นำฝากเข้าบัญชี" + "  " + deptaccount_no);
            objDwDetail.AcceptText();
        }

        function GetLoanAcc(loancontract_no) {
            var row = Gcoop.GetEl("HdDetailRow").value;
            objDwDetail.SetItem(row, "document_no", loancontract_no);
            objDwDetail.SetItem(row, "paytrnbank_desc", "ชำระหนี้เลขที่สัญญา" + "  " + loancontract_no);
            objDwDetail.AcceptText();
        }

        function OnDwMainButtonClicked(sender, rowNumber, buttonName) {
            var memb_flag = objDwMain.GetItem(1, "member_flag");
            if (buttonName == "cb_member" && memb_flag == 1) {
                Gcoop.OpenDlg("695", "600", "w_dlg_fin_member_search.aspx", "");
            }
            else if (buttonName == "cb_member" && memb_flag == 0) {
                // ค้นหาบุคคลภายนอก
                Gcoop.OpenDlg(550, 400, "w_dlg_sl_extmember_search.aspx", "");
            }
        }

        function OnDwDetailButtonClicked(sender, rowNumber, buttonName) {
            var paytrnitemtype_code = objDwDetail.GetItem(rowNumber, "paytrnitemtype_code");
            var memberno = objDwMain.GetItem(1, "member_no");
            if (buttonName == "b_search" && paytrnitemtype_code == "DEP") {
                Gcoop.GetEl("HdDetailRow").value = rowNumber + "";
                Gcoop.OpenDlg("400", "350", "w_dlg_dept_acclist.aspx", "?memberno=" + memberno);
            }
            else if (buttonName == "b_search" && paytrnitemtype_code == "LON") {
                Gcoop.GetEl("HdDetailRow").value = rowNumber + "";
                Gcoop.OpenDlg("400", "350", "w_dlg_loan_acclist.aspx", "?memberno=" + memberno);
            }
        }

        function OnDwMainItemChanged(sender, rowNumber, colunmName, newValue) {
            if (colunmName == "member_no") {
                newValue = Gcoop.StringFormat(newValue, "000000");
                objDwMain.SetItem(rowNumber, colunmName, newValue);
                objDwMain.AcceptText();
                postMemberNo();
            }
            else if (colunmName == "payment_branch") {
                objDwMain.SetItem(rowNumber, colunmName, newValue);
                objDwMain.AcceptText();
                postRefresh();
            }
        }

        function OnDwDetailClicked(sender, rowNumber, objectName) {
            for (var i = 1; i <= sender.RowCount(); i++) {
                sender.SelectRow(i, false);
            }
            sender.SelectRow(rowNumber, true);
            sender.SetRow(rowNumber);
            Gcoop.GetEl("HdDetailRow").value = rowNumber + "";
        }

        function OnDwDetailItemChange(sender, rowNumber, colunmName, newValue) {
            if (colunmName == "paytrnitemtype_code") {
                objDwDetail.SetItem(rowNumber, "paytrnitemtype_code", newValue);
                var paytrnitemtype_code = objDwDetail.GetItem(rowNumber, "paytrnitemtype_code");
                var memberno = objDwMain.GetItem(1, "member_no");
                var prename = objDwMain.GetItem(1, "mbprename_desc");
                var name = objDwMain.GetItem(1, "mbmemb_name");
                var surname = objDwMain.GetItem(1, "mbmemb_surname");
                if (paytrnitemtype_code == "SHR") {
                    objDwDetail.SetItem(rowNumber, "paytrnbank_desc", "ชำระค่าหุ้น" + "  " + prename + name + surname);
                    objDwDetail.SetItem(rowNumber, "document_no", memberno);
                }
                else if (paytrnitemtype_code == "AGC") {
                    objDwDetail.SetItem(rowNumber, "paytrnbank_desc", "ชำระตัวแทน" + "  " + prename + name + surname);
                    objDwDetail.SetItem(rowNumber, "document_no", memberno);
                }
            }
            else if (colunmName == "moneytrn_amt") {
                objDwDetail.SetItem(rowNumber, colunmName, newValue);
                postRefresh();
            }
        }

        function MenubarNew() {
            if (confirm("ยืนยันการลบข้อมูล ??")) {
                window.location = Gcoop.GetUrl() + "Applications/app_finance/w_sheet_paytrnbank.aspx";
            }
        }

        function Validate() {
            return confirm("ต้องการบันทึกข้อมูล ใช่หรือไม่?");
        }
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_fn_paytrnbank_trn_master"
        LibraryList="~/DataWindow/app_finance/paytrnbank.pbl" ClientEventItemChanged="OnDwMainItemChanged"
        ClientFormatting="True" ClientEventButtonClicked="OnDwMainButtonClicked">
    </dw:WebDataWindowControl>
    <asp:Label ID="Label7" runat="server" Text="รายละเอียดธนาณัติ" Font-Bold="True" Font-Names="Tahoma"
        Font-Size="14px" ForeColor="#0099CC" Font-Overline="False" Font-Underline="True" />
    &nbsp; <span onclick="OnClickInsertRow()" style="cursor: pointer;">
        <asp:Label ID="LbInsert2" runat="server" Text="เพิ่มแถว" Font-Bold="False" Font-Names="Tahoma"
            Font-Size="14px" Font-Underline="True" ForeColor="#006600" /></span> &nbsp;&nbsp;
    <span onclick="OnClickDeleteRow()" style="cursor: pointer;">
        <asp:Label ID="LbDel2" runat="server" Text="ลบแถว" Font-Bold="False" Font-Names="Tahoma"
            Font-Size="14px" Font-Underline="True" ForeColor="Red" /></span>
    <dw:WebDataWindowControl ID="DwDetail" runat="server" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        DataWindowObject="d_fn_paytrnbank_trn_detail" LibraryList="~/DataWindow/app_finance/paytrnbank.pbl"
        ClientFormatting="True" ClientEventClicked="OnDwDetailClicked" ClientEventButtonClicked="OnDwDetailButtonClicked"
        ClientEventItemChanged="OnDwDetailItemChange">
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="HdDetailRow" runat="server" />
</asp:Content>
