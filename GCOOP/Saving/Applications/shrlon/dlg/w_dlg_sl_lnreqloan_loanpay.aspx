<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_lnreqloan_loanpay.aspx.cs"
    Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_lnreqloan_loanpay" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ใบคำขอกู้</title>
    <%=jsExpenseBank%>
    <%=jsExpensecode%>
    <%=jsExpensebankbrRetrieve %>
    <%=jsGetexpensememno%>
    <%=jsGetbankname%>
    <%=jsGetbranchname%>
    <script type="text/javascript">


        function DialogLoadComplete() {

        }

        function loanpayok(sender, rowNumber, columnName, objectName) {

            var expense_code = objdw_detail.GetItem(1, "expense_code");
            var expense_bank = objdw_detail.GetItem(1, "expense_bank");
            var expense_branch = objdw_detail.GetItem(1, "expense_branch");
            var expense_accid = objdw_detail.GetItem(1, "expense_accid");
            var buttonc = objdw_detail.GetItem(1, "buttonc");

            var expense_bank_name = Gcoop.GetEl("Hdbankname").value;
            var expense_branch_name = Gcoop.GetEl("Hdbranchname").value;

            var expense_desc = expense_bank_name; // + "สาขา" + expense_branch_name;

            if (buttonc == "expense") {

                window.opener.GetLoanreceivememno(expense_code, expense_bank, expense_branch, expense_accid, expense_bank_name, expense_branch_name);
            } else {

                window.opener.GetLoanpaymemno(expense_code, expense_bank, expense_branch, expense_accid, expense_bank_name, expense_branch_name, expense_desc);
            }
            window.close();

        }
        function OnCloseClick() {
            window.close();
        }
        function ItemDwMainChanged(sender, rowNumber, columnName, newValue) {
            objdw_detail.SetItem(rowNumber, columnName, newValue);
            objdw_detail.AcceptText();
            //alert(columnName);
            if (columnName == "expense_code") {
                objdw_detail.SetItem(rowNumber, columnName, newValue);
                objdw_detail.AcceptText();
                //                alert("expensecode22");
                jsExpensecode();

            } else if (columnName == "expense_bank") {

                objdw_detail.SetItem(rowNumber, columnName, newValue);
                objdw_detail.AcceptText();
                jsExpenseBank();

            } else if (columnName == "expense_bank_1") {

                objdw_detail.SetItem(rowNumber, columnName, newValue);
                objdw_detail.AcceptText();
                jsExpenseBank();

            } else if (columnName == "expense_branch") {

                objdw_detail.SetItem(rowNumber, columnName, newValue);
                objdw_detail.SetItem(rowNumber, "expense_branch_1", newValue);
                objdw_detail.AcceptText();
//                jsGetbranchname();
            }
        }

        function OnDwMainClicked(sender, rowNumber, objectName) {
            if (objectName == "b_expense_branch") {
                var expense_bank = objdw_detail.GetItem(1, "expense_bank");

                if (expense_bank != "") {
                    // objdw_main.Setitem(1, "retrive_bk_branchflag", 1);
                    jsExpensebankbrRetrieve();
                }

            } else if (objectName == 'b_retrive') {

                jsGetexpensememno();

            } else if (objectName == 'b_ok') {
                loanpayok();
                //   jsGetbankname();



            }
            else if (objectName == 'b_chgaccid') {
                var memberNoVal = objdw_detail.GetItem(1, "member_no");
                var expense_code = objdw_detail.GetItem(1, "expense_code");
                if ((memberNoVal != null) && (memberNoVal != "")) {
                    if (expense_code == "TRN") {
                        Gcoop.OpenDlg(620, 250, "w_dlg_show_accid.aspx", "?member=" + memberNoVal);
                    } else if (expense_code == "CBT") {
                        Gcoop.OpenDlg(620, 250, "w_dlg_show_accid.aspx", "?member=" + memberNoVal);
                    }
                }
            }
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_detail" runat="server" DataWindowObject="d_sl_dlg_loanrequest_loanpay"
                    LibraryList="~/DataWindow/Shrlon/sl_loan_requestment_cen.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    ClientEventItemChanged="ItemDwMainChanged" ClientEventClicked="OnDwMainClicked"
                    ToolTip=" " ClientFormatting="True" TabIndex="1">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td>
                <input id="btnClose" type="button" value="ปิด" onclick="OnCloseClick()" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HfChkStatus" runat="server" />
    <asp:HiddenField ID="Hdbuttonc" runat="server" />
    <asp:HiddenField ID="Hdbankname" runat="server" />
    <asp:HiddenField ID="Hdbranchname" runat="server" />
    </form>
</body>
</html>
