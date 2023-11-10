<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_slipbank.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_sheet_slipbank" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postBankAccSlip %>
    <%=postBankBranch %>
    <%=postGetBank %>
    <%=postChange %>

    <script type="text/javascript">

        function Validate() {
            objDwMain.AcceptText();
            return confirm("ยืนยันการบันทึกรายการธนาคาร");
        }

        function MenubarOpen() {
            Gcoop.OpenIFrameExtend(700, 250, "w_dlg_bankaccount_search.aspx", "");
            
        }

        function MenubarNew() {
            if (confirm("ยืนยันการลบข้อมูล ??")) {
                window.location = Gcoop.GetUrl() + "Applications/app_finance/w_sheet_slipbank.aspx";
            }
        }

        function DwMainButtonClick(sender, rowNumber, buttonName) {
            if (buttonName == "b_bankbranch") {
                Gcoop.OpenIFrameExtend(700, 550, "w_dlg_bank_and_branch.aspx", "");
            }
        }

        function DwMainItemChange(sender, rowNumber, columnName, newValue) {
            objDwMain.SetItem(rowNumber, columnName, newValue);
            if (columnName == "item_code" && newValue == "OCA") {
                objDwMain.AcceptText();
                if (confirm("ต้องการเปิดบัญชี ??") == true) {
                    Gcoop.GetEl("HdItemCode").value = newValue;
                    postChange();
                }
            }
            else if (columnName == "item_code") {
                Gcoop.GetEl("HdItemCode").value = newValue;
                objDwMain.AcceptText();
                var balance = objDwMain.GetItem(rowNumber, "withdraw_amt");
                var ItemAmt = objDwMain.GetItem(rowNumber, "item_amt");
                Gcoop.GetEl("HdBalance").value = balance;
                Gcoop.GetEl("HdItemAmt").value = ItemAmt;
                postChange();
            }
            else if (columnName == "bank_code") {
                objDwMain.AcceptText();
                postBankBranch();
            }
        }

        function GetDlgBankAndBranch(bankCode, bankDesc, branchCode, branchDesc) {
            objDwMain.SetItem(1, "bank_code", bankCode);
            objDwMain.SetItem(1, "bank_branch", branchCode);
            objDwMain.SetItem(1, "bank_desc", bankDesc);
            objDwMain.SetItem(1, "branch_name", branchDesc);
            objDwMain.AcceptText();
            postGetBank();
        }

        function GetDlgBankAccount(account_no, account_name, bank_code, bankbranch_code) {
            objDwMain.SetItem(1, "account_no", account_no);
            objDwMain.SetItem(1, "account_name", account_name);
            objDwMain.SetItem(1, "bank_code", bank_code);
            objDwMain.SetItem(1, "bank_branch", bankbranch_code);
            objDwMain.AcceptText();
            postBankAccSlip();
        }
    
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <b>บันทึกรายการธนาคาร</b>
    <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_slipbank"
        LibraryList="~/DataWindow/App_finance/bankaccount.pbl" ClientScriptable="True"
        AutoRestoreContext="false" AutoRestoreDataCache="true" AutoSaveDataCacheAfterRetrieve="true"
        ClientEventButtonClicked="DwMainButtonClick" ClientEventItemChanged="DwMainItemChange">
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="HdItemCode" runat="server" />
    <asp:HiddenField ID="HdBalance" runat="server" />
    <asp:HiddenField ID="HdItemAmt" runat="server" />
</asp:Content>
