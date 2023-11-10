<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_contcredit_new.aspx.cs"
    Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_contcredit_new" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%=jsGetMemberInfo%>
    <%=jsmaxcreditperiod%>
    <%=savewebsheet %>
    <%=jsContPeriod%>
    <%=jsChecksalabal%>
    <%=jsExpensebankbrRetrieve%>
    <%=jsSetBank%>
    <%=jsGetexpensememno%>
    <%=jsExpenseBank%>
    <%=jsSetMonthpayCoop%>>
    <script type="text/javascript">

        function ItemChangeDwMain(sender, rowNumber, columnName, newValue) {

            if ((columnName == "loantype_code") || (columnName == "loantype_code_1")) {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                jsmaxcreditperiod();
            } else if ((columnName == "maxperiod_payamt") || (columnName == "loancredit_amt")) {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                var maxperiod = Gcoop.GetEl("HdPeriodMax").value;
                var maxcredit = Gcoop.GetEl("HdCreditMax").value;
                if (columnName = "loancredit_amt") {
                    if (newValue > maxcredit) {
                        alert("กำหนดวงเงิน เกินสิทธิ์การกู้เงิน");
                    }
                }
                jsContPeriod();
            } else if (columnName == "paymonth_other") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                // alert("teee");
                jsChecksalabal();
            } else if ((columnName == "loanrcv_bank_1") || (columnName == "loanrcv_bank")) {
                objdw_main.SetItem(rowNumber, columnName, newValue);

                objdw_main.AcceptText();

                jsExpenseBank();

            } else if (columnName == "loanrcv_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                jsSetBank();
            }
        }

        function OnDwMainLnContSrc(s, r, c) {
            if (c == "b_save") {
                savewebsheet();
            }
            if (c == "b_setbank") {


                jsGetexpensememno();
            }
        }
        function selectRow(s, r, c) { 
            s.AcceptText();
            if (c == "operate_flag") {
                jsSetMonthpayCoop();
            }
        }
       
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    กำหนดวงเงินกู้
    <dw:WebDataWindowControl ID="dw_main" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_sl_contcredit_adj_new_w"
        LibraryList="~/DataWindow/Shrlon/sl_loancredit.pbl" ClientEventButtonClicked="OnDwMainLnContSrc"
        ClientEventItemChanged="ItemChangeDwMain">
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="Hdmembtype_code" runat="server" />
    รายละเอียด
    <dw:WebDataWindowControl ID="dw_detail" runat="server" DataWindowObject="d_sl_contcredit_adj_new_loanpay_w"
        LibraryList="~/DataWindow/Shrlon/sl_loancredit.pbl" RowsPerPage="18" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        ClientEventClicked="selectRow">
        <PageNavigationBarSettings Position="Bottom" Visible="True" NavigatorType="Numeric">
            <BarStyle HorizontalAlign="Center" />
            <NumericNavigator FirstLastVisible="True" />
        </PageNavigationBarSettings>
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="HdPeriodMax" runat="server" />
    <asp:HiddenField ID="HdCreditMax" runat="server" />
    </form>
</body>
</html>
