<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_popup_withdraw.aspx.cs"
    Inherits="Saving.Applications.app_finance.dlg.w_dlg_sl_popup_withdraw" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%=initDataWindow %>
    <%=loanCalInt %>
    <%=calculateAmt%>
    <%=saveWithdraw%>
    <%=calculateitempayamt%>
    <%=checkvalue %>
    <title>Share Withdraw Page</title>
    <script type="text/javascript">

        function OnDwMainItemChanged(sender, rowNumber, columnName, newValue) {
            //เมื่อมีการเปลี่ยนวันที่
            if (columnName == "operate_tdate") {
                if (LoanChecked()) {
                    objDwMain.SetItem(1, "operate_tdate", newValue);
                    objDwMain.AcceptText();
                    objDwOperateLoan.AcceptText();
                    loanCalInt();
                }
            }
        }
        function OnDwLoanItemClicked(sender, rowNumber, objectName) {
            if (objectName == "operate_flag") {
                //กำหนดค่า ให้ Check Box
                Gcoop.CheckDw(sender, rowNumber, objectName, "operate_flag", 1, 0);
                var flag = objDwOperateLoan.GetItem(rowNumber, "operate_flag");
                if (flag == 1) {
                    var bfshrcontbalamt = objDwMain.GetItem(1, "bfshrcont_balamt");
                    var payoutclramt = objDwMain.GetItem(1, "payoutclr_amt");
                    var amttotal = bfshrcontbalamt - payoutclramt;
                    if (amttotal <= 0) {
                        alert("ไม่สามารถทำรายการได้ เนื่องจาก หุ้นคงเหลือ น้อยกว่า หนี้คงเหลือ");
                        objDwOperateLoan.SetItem(rowNumber, "operate_flag", 0);
                        return false;
                    } else {
                        objDwOperateLoan.SetItem(rowNumber, "principal_payamt", objDwOperateLoan.GetItem(rowNumber, "item_balance"));
                        objDwMain.AcceptText();
                        objDwOperateLoan.AcceptText();
                        loanCalInt();
                    }
                } else {
                    objDwOperateLoan.SetItem(rowNumber, "principal_payamt", 0);
                    objDwOperateLoan.SetItem(rowNumber, "interest_payamt", 0);
                    objDwOperateLoan.SetItem(rowNumber, "item_payamt", 0);
                    objDwOperateLoan.SetItem(rowNumber, "item_balance", objDwOperateLoan.GetItem(rowNumber, "bfshrcont_balamt"));
                    objDwMain.AcceptText();
                    objDwOperateLoan.AcceptText();
                    calculateAmt();
                }
            }
        }

        function OnDwLoanItemChanged(sender, rowNumber, columnName, newValue) {
            if (columnName == "principal_payamt" || columnName == "interest_payamt") {
                var flag = objDwOperateLoan.GetItem(rowNumber, "operate_flag");
                if (flag == 1) {
                    objDwOperateLoan.SetItem(rowNumber, columnName, newValue);
                    objDwMain.AcceptText();
                    objDwOperateLoan.AcceptText();
                    calculateAmt();
                }
            }
            else if (columnName == "item_payamt") {
                objDwOperateLoan.SetItem(rowNumber, columnName, newValue);
                objDwOperateLoan.AcceptText();
                calculateitempayamt();
            }
        }

        function LoanChecked() {
            //มีการเช็คถูกที่ Loan หรือไม่?
            var row = objDwOperateLoan.RowCount();
            var i = 1;
            for (i; i <= row; i++) {
                var flagValue = objDwOperateLoan.GetItem(i, "operate_flag");
                if (flagValue == 1) {
                    return true;
                    break;
                }
            }
            return false;
        }

        function DialogLoadComplete() {

            var indexVal = Gcoop.GetEl("HfIndex").value;
            var allIndexVal = Gcoop.ParseInt(Gcoop.GetEl("HfAllIndex").value);
            if (indexVal == allIndexVal) {
                window.opener.RefreshByDlg();
                window.close();
            }
            var bfshrcontbalamt = objDwMain.GetItem(1, "bfshrcont_balamt");
            var payoutclramt = objDwMain.GetItem(1, "payoutclr_amt");
            var amttotal = bfshrcontbalamt - payoutclramt;
            if (amttotal < 0) {
                //alert("ค่าหักชำระหุ้น ติดลบ กรุณา ระบุจำนวนเงินใหม่");
                return false;
            }
        }

        function SaveWithdraw() {
            var payoutnetamt = objDwMain.GetItem(1, "payoutnet_amt");
            var bfshrcontbalamt = objDwMain.GetItem(1, "bfshrcont_balamt");
            var payoutclramt = objDwMain.GetItem(1, "payoutclr_amt");
            var totalamt = bfshrcontbalamt - payoutclramt;
            //        if(payoutnetamt<=0){
            //            alert("ไม่สามารถ บันทึกได้ เนื่องจาก ไม่มียอดการชำระ ");
            //            return false;
            //        }
            //       objDwMain.SetItem(1,"payoutnet_amt",0);
            if (totalamt >= 0) {
                objDwMain.AcceptText();
                objDwOperateLoan.AcceptText();
                saveWithdraw();
            } else {
                alert("ไม่สามารถ บันทึกได้ เนื่องจาก ยอดชำระค่าหุ้น ติดลบ");
                return false;
            }
        }
        function OnItemdetailChanged(s, row, c, v) {
            if (c == "unit_price" || c == "quantiy") {
                var Total = 0;
                var total_amt = 0;
                var quantiy = 0;
                objdw_detail.SetItem(row, c, v);
                objdw_detail.AcceptText();
                for (i = 0; i < objdw_detail.RowCount(); i++) {
                    Total = Gcoop.ParseFloat(objdw_detail.GetItem(i + 1, "unit_price"));
                    quantiy = Gcoop.ParseFloat(objdw_detail.GetItem(i + 1, "quantiy"));
                    total_amt += (Total * quantiy);
                }
                objDw_main.SetItem(1, "total_amt", total_amt); //total_amt
                objDw_main.AcceptText();
            } else if (c == "product_id") {
                objdw_detail.SetItem(row, c, v);
                objdw_detail.AcceptText();
                getProduct();
            }

            return 0;
        }
        function OnDwListClicked(sender, rowNumber, objectName) {
            if (objectName == "setshrarr_flag") {
                Gcoop.CheckDw(sender, rowNumber, objectName, "setshrarr_flag", "1", "0");
                checkvalue();
            }
        }
        function OnInsert() {
            objDwOperateEtc.InsertRow(objDwOperateEtc.RowCount() + 1);
        }
        function operate_flag_Click(sender, rowNumber, objectName) {
            if (objectName == "operate_flag") {
                Gcoop.CheckDw(sender, rowNumber, objectName, "operate_flag", "1", "0");
             
            } 
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="HfIndex" runat="server" />
    <asp:HiddenField ID="HfAllIndex" runat="server" />
    <asp:HiddenField ID="HfFormType" runat="server" />
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:Label ID="LbSaveStatus" runat="server" Text="Label"></asp:Label>
    <table border="0">
        <tr>
            <td align="right">
                <input id="b_save" style="width: 100px; height: 35px; color: White; font-size: medium;
                    font-weight: bold; background-color: Green;" type="button" onclick="SaveWithdraw()"
                    value="บันทึก" />
            </td>
        </tr>
        <tr>
            <td>
                <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_sl_share_swd"
                    LibraryList="~/DataWindow/keeping/sl_share_withdraw.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    ClientEventItemChanged="OnDwMainItemChanged" ClientFormatting="True" ClientEventClicked="OnDwListClicked">
                </dw:WebDataWindowControl>
                <br />
                <dw:WebDataWindowControl ID="DwOperateLoan" runat="server" DataWindowObject="d_sl_slip_operate_loan"
                    LibraryList="~/DataWindow/keeping/sl_share_withdraw.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" Height="100px"
                    ClientScriptable="True" ClientEventClicked="OnDwLoanItemClicked" ClientFormatting="True"
                    ClientEventItemChanged="OnDwLoanItemChanged">
                </dw:WebDataWindowControl>
                <br />
                <dw:WebDataWindowControl ID="DwOperateEtc" runat="server" DataWindowObject="d_sl_slip_operate_etc"
                    LibraryList="~/DataWindow/keeping/sl_share_withdraw.pbl" AutoRestoreContext="False"
                    Height="80px" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                    ClientScriptable="True" ClientEventClicked="operate_flag_Click">
                </dw:WebDataWindowControl>
                <span class="linkSpan" onclick="OnInsert()" style="font-size: small; color: Red;
                    float: left">เพิ่มแถว</span>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
