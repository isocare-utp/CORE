<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_share_withdraw_partial.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_sheet_sl_share_withdraw_partial"
    Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=initDataWindow%>
    <%=newclear %>
    <%=loanCalInt %>
    <%=calculateAmt%>
    <%=calculateitempayamt%>
    <%=initLnRcvlist%>
    <%=fittermoneytype%>
    <script type="text/javascript">

        function Ondw_mainItemChanged(sender, rowNumber, columnName, newValue) {
            if (columnName == "member_no") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                initDataWindow();
            } else if (columnName == "moneytype_code") {
                //alert(columnName);
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hfmoneytype_code").value = objdw_main.GetItem(rowNumber, "moneytype_code");
                fittermoneytype();

            } else if (columnName == "payout_amt") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                // var payoutclr_amt = Gcoop.ParseFloat(objdw_main.GetItem(rowNumber, "payoutclr_amt"));
                //  var payout_amt = Gcoop.ParseFloat(objdw_main.GetItem(rowNumber, "payout_amt"));
                // var payoutnet_amt = payout_amt - payoutclr_amt;
                var item_payamtloan = 0;
                var item_payamt = 0;
                var item_payamtetc = 0;
                for (var i = 1; i <= objDwOperateLoan.RowCount(); i++) {
                    var operateflag = objDwOperateLoan.GetItem(i, "operate_flag");
                    if (operateflag == 1) {
                        item_payamt = Gcoop.ParseFloat(objDwOperateLoan.GetItem(i, "item_payamt"));
                        item_payamtloan += item_payamt;

                    }
                }

                for (var j = 1; j <= objDwOperateEtc.RowCount(); j++) {
                    var operateflag = objDwOperateEtc.GetItem(j, "operate_flag");
                    if (operateflag == 1) {
                        item_payamt = Gcoop.ParseFloat(objDwOperateEtc.GetItem(j, "item_payamt"));
                        item_payamtetc += item_payamt;

                    }
                }

                objdw_main.SetItem(1, "payoutclr_amt", item_payamtetc + item_payamtloan);
                var payout_amt = Gcoop.ParseFloat(objdw_main.GetItem(1, "payout_amt"));
                var bfshrcont_balamt = Gcoop.ParseFloat(objdw_main.GetItem(1, "bfshrcont_balamt"));
                var payoutnet_amt = bfshrcont_balamt - payout_amt - (item_payamtetc + item_payamtloan);
                var payoutnet_amt = payout_amt - (item_payamtetc + item_payamtloan);

                objdw_main.SetItem(rowNumber, "payoutnet_amt", payoutnet_amt);
                objdw_main.AcceptText();
                // sumpayoutclr_amt();

            }
        }

        function OnDwLoanItemClicked(sender, rowNumber, objectName) {
            if (objectName == "operate_flag") {
                //กำหนดค่า ให้ Check Box
                Gcoop.CheckDw(sender, rowNumber, objectName, "operate_flag", 1, 0);
                var flag = objDwOperateLoan.GetItem(rowNumber, "operate_flag");
                if (flag == 1) {
                    var bfshrcontbalamt = objdw_main.GetItem(1, "bfshrcont_balamt");
                    var payoutclramt = objdw_main.GetItem(1, "payoutclr_amt");
                    var amttotal = bfshrcontbalamt - payoutclramt;
                    if (amttotal <= 0) {
                        alert("ไม่สามารถทำรายการได้ เนื่องจาก หุ้นคงเหลือ น้อยกว่า หนี้คงเหลือ");
                        objDwOperateLoan.SetItem(rowNumber, "operate_flag", 0);
                        return false;
                    } else {
                        objDwOperateLoan.SetItem(rowNumber, "principal_payamt", objDwOperateLoan.GetItem(rowNumber, "item_balance"));
                        objdw_main.AcceptText();
                        objDwOperateLoan.AcceptText();
                        loanCalInt();
                    }
                } else {
                    objDwOperateLoan.SetItem(rowNumber, "principal_payamt", 0);
                    objDwOperateLoan.SetItem(rowNumber, "interest_payamt", 0);
                    objDwOperateLoan.SetItem(rowNumber, "item_payamt", 0);
                    objDwOperateLoan.SetItem(rowNumber, "item_balance", objDwOperateLoan.GetItem(rowNumber, "bfshrcont_balamt"));
                    objdw_main.AcceptText();
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
                    objdw_main.AcceptText();
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



        function operate_flag_Click(sender, rowNumber, objectName) {
            if (objectName == "operate_flag") {
                Gcoop.CheckDw(sender, rowNumber, objectName, "operate_flag", "1", "0");

            }
        }

        function OnInsert() {
            objDwOperateEtc.InsertRow(objDwOperateEtc.RowCount() + 1);
        }

        function MenubarOpen() {
            Gcoop.OpenDlg('570', '590', 'w_dlg_sl_member_search.aspx', '')
        }

        function GetValueFromDlg(strvalue) {
            objdw_main.SetItem(1, "member_no", Gcoop.StringFormat(strvalue, '000000'));
            objdw_main.AcceptText();
            initDataWindow();
        }
        function Validate() {
            objdw_main.SetItem(1, "payoutnet_amt", objdw_main.GetItem(1, "payout_amt"));
            objdw_main.AcceptText();
            return confirm("ต้องการบันทึกข้อมูล?");
        }

        function MenubarNew() {
            Gcoop.SetLastFocus("member_no_0");
            Gcoop.Focus();
            newclear();
        }
        function SheetLoadComplete() {
            if (Gcoop.GetEl("HdIsPostBack").value != "true") {
                // alert(Gcoop.GetEl("HdIsPostBack").value);
                Gcoop.SetLastFocus("member_no_0");
                Gcoop.Focus();
            }
        }

        function dw_list_Click(sender, rowNumber, objectName) {

            Gcoop.GetEl("Hfpayoutorder_no").value = objdw_list.GetItem(rowNumber, "payoutorder_no");

            initLnRcvlist();

        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="HdIsPostBack" runat="server" Value="false" />
    <asp:HiddenField ID="Hfmoneytype_code" runat="server" Value="false" />
    <asp:HiddenField ID="Hfpayoutorder_no" runat="server" />
    <asp:TextBox ID="TextBox2" runat="server" Height="31px" Width="159px" Visible="false"></asp:TextBox>
     <asp:CheckBox ID="CheckBox1" runat="server" 
        oncheckedchanged="CheckBox1_CheckedChanged" /><asp:Label ID="Label1" runat="server"
            Text="ออกใบเสร็จ"></asp:Label>
    <dw:WebDataWindowControl ID="dw_list" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientEventClicked="dw_list_Click" Visible="false"
        ClientFormatting="True" ClientScriptable="True" DataWindowObject="d_sl_listbymem_finshrwtd"
        LibraryList="~/DataWindow/Shrlon/sl_slipall.pbl">
    </dw:WebDataWindowControl>
    <dw:WebDataWindowControl ID="dw_main" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="Ondw_mainItemChanged"
        ClientFormatting="True" ClientScriptable="True" DataWindowObject="d_sl_payoutslip_shrwtdpartial"
        LibraryList="~/DataWindow/Shrlon/sl_slipall.pbl">
    </dw:WebDataWindowControl>
    <br />
    <dw:WebDataWindowControl ID="DwOperateLoan" runat="server" DataWindowObject="d_sl_payinslip_loan"
        LibraryList="~/DataWindow/Shrlon/sl_slipall.pbl" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" Height="100px" Width="750px" ClientScriptable="True"
        ClientEventClicked="OnDwLoanItemClicked" ClientFormatting="True" ClientEventItemChanged="OnDwLoanItemChanged">
    </dw:WebDataWindowControl>
    <br />
    <dw:WebDataWindowControl ID="DwOperateEtc" runat="server" DataWindowObject="d_sl_payinslip_etc"
        LibraryList="~/DataWindow/Shrlon/sl_slipall.pbl" AutoRestoreContext="False" Height="80px"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        ClientEventClicked="operate_flag_Click">
    </dw:WebDataWindowControl>
    <span class="linkSpan" onclick="OnInsert()" style="font-size: small; color: Red;
        float: left">เพิ่มแถว</span>
</asp:Content>
