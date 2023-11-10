<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_loan_receive.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_sheet_sl_loan_receive" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=saveSlipLnRcv %>
    <%=initDataWindow%>
    <%=initLnRcvlist%>
    <%=initLnRcvReCalInt%>
    <%=jsRefresh %>
    <%=GetNewLoan %>
    <%=calculateitempayamt %>
    <%=newClear %>
    <%=setPayoutclrAmt%>
    <%=fittermoneytype%>
    <script type="text/javascript">

        function Validate() {
            objDwMain.AcceptText();
            objDwOperateLoan.AcceptText();
            objDwOperateEtc.AcceptText();
            return true;
        }
        function RecalInt() {
            objDwMain.AcceptText();
            objDwOperateLoan.AcceptText();
            objDwOperateEtc.AcceptText();
            var count = objDwOperateLoan.RowCount();
            var haveChecked = false;
            var i = 1;
            for (i = 1; i <= count; i++) {
                if (objDwOperateLoan.GetItem(i, "operate_flag") == 1) {
                    haveChecked = true;
                }
            }
            if (haveChecked) {
                objDwMain.AcceptText();
                objDwOperateLoan.AcceptText();
                objDwOperateEtc.AcceptText();
                initLnRcvReCalInt();
            }
        }

        function OnDwMainItemChanged(sender, rowNumber, columnName, newValue) {
            if (columnName == "operate_tdate") {
                if (LoanChecked()) {
                    objDwMain.SetItem(1, "operate_tdate", newValue);
                    objDwMain.AcceptText();
                    objDwOperateLoan.AcceptText();
                    objDwOperateEtc.AcceptText();
                    initLnRcvReCalInt();
                }

            } else if ((columnName == "moneytype_code_1") || (columnName == "moneytype_code")) {
                objDwMain.SetItem(1, "moneytype_code_1", newValue);
                objDwMain.SetItem(1, "moneytype_code", newValue);
                objDwMain.AcceptText();
                Gcoop.GetEl("Hfmoneytype_code").value = objDwMain.GetItem(rowNumber, "moneytype_code");
                fittermoneytype();
            } else if (columnName == "member_no") {

                objDwMain.SetItem(1, "member_no", Gcoop.StringFormat(newValue, "00000000"));
                objDwMain.AcceptText();
                Gcoop.GetEl("HfMemberNo").value = objDwMain.GetItem(1, "member_no");
                initDataWindow();
            } 
//            else if (columnName == "moneytype_code") {

//                objDwMain.SetItem(1, columnName, newValue);
//                objDwMain.AcceptText();
//                Gcoop.GetEl("Hfmoneytype_code").value = objDwMain.GetItem(rowNumber, "moneytype_code");
//                fittermoneytype();
//            }
        }
        function OnDwLoanItemClicked(sender, rowNumber, objectName) {
            if (objectName == "operate_flag") {
                Gcoop.CheckDw(sender, rowNumber, objectName, "operate_flag", 1, 0);
                var flag = objDwOperateLoan.GetItem(rowNumber, "operate_flag");
                if (flag == 1) {

                    //                    objDwOperateLoan.SetItem(rowNumber, "principal_payamt", objDwOperateLoan.GetItem(rowNumber, "bfshrcont_balamt"));
                    //                    objDwOperateLoan.SetItem(rowNumber, "item_payamt", objDwOperateLoan.GetItem(rowNumber, "bfshrcont_balamt"));
                    //                    objDwOperateLoan.SetItem(rowNumber, "item_balance", 0);
                    setPayoutclrAmt();

                    //                   
                    //                    var principal_payamt = Gcoop.ParseFloat(objDwOperateLoan.GetItem(rowNumber, "principal_payamt"));
                    //                    var interest_payamt = Gcoop.ParseFloat(objDwOperateLoan.GetItem(rowNumber, "interest_payamt"));    

                    //                    objDwMain.AcceptText();
                    //                    objDwOperateLoan.AcceptText();
                    //                    objDwOperateEtc.AcceptText();
                    //  initLnRcvReCalInt();
                } else {

                    objDwOperateLoan.SetItem(rowNumber, "principal_payamt", 0);
                    objDwOperateLoan.SetItem(rowNumber, "interest_payamt", 0);
                    objDwOperateLoan.SetItem(rowNumber, "item_payamt", 0);

                    objDwMain.AcceptText();
                    objDwOperateLoan.AcceptText();
                    objDwOperateEtc.AcceptText();
                    setPayoutclrAmt();
                    //  initLnRcvReCalInt();
                }
            }
        }

        function OnDwLoanItemChanged(sender, rowNumber, columnName, newValue) {
            if (columnName == "item_payamt") {
                objDwOperateLoan.SetItem(rowNumber, columnName, newValue);
                objDwOperateLoan.AcceptText();
                calculateitempayamt();
            }
            else if (columnName == "interest_payamt") {

                objDwOperateLoan.SetItem(rowNumber, columnName, newValue);
                objDwOperateLoan.AcceptText();
                //                var principal_payamt = 0;
                //                var interest_payamt = 0;
                //                var payoutclr_amt = 0;
                //                var item_payamt = 0;
                //                for (i = 0; i < objDwOperateLoan.RowCount(); i++) {
                //                    principal_payamt = Gcoop.ParseFloat(objDwOperateLoan.GetItem(i + 1, "principal_payamt"));
                //                    interest_payamt = Gcoop.ParseFloat(objDwOperateLoan.GetItem(i + 1, "interest_payamt"));
                //                    payoutclr_amt += (principal_payamt + interest_payamt);
                //                   
                //                    objDwOperateLoan.SetItem(i + 1, "item_payamt", principal_payamt + interest_payamt);
                //                }
                //                for (i = 0; i < objDwOperateEtc.RowCount(); i++) {
                //                     item_payamt = Gcoop.ParseFloat(objDwOperateEtc.GetItem(i + 1, "item_payamt"));
                //                     item_payamt += item_payamt;
                //                 }
                //                 jsRefresh();
                //                 objDwOperateLoan.AcceptText();
                //                 Gcoop.GetEl("Hfpayoutclr_amt").value = payoutclr_amt + item_payamt;
                //                 alert(Gcoop.GetEl("Hfpayoutclr_amt").value);
                setPayoutclrAmt();
                //                 objDwMain.SetItem(1, "payoutclr_amt", Gcoop.ParseFloat(Gcoop.GetEl("Hfpayoutclr_amt").value));
                //                objDwMain.AcceptText();
            }
        }







        function Create_loan_Click(sender, rowNumber, objectName) {
            if (objectName == "b_loan") {

                GetNewLoan();

            }
        }
        function LoanChecked() {
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

        function Skip() {
            var hfindex = Gcoop.ParseInt(Gcoop.GetEl("HfIndex").value);
            var allindex = Gcoop.ParseInt(Gcoop.GetEl("HfAllIndex").value)
            if (hfindex != (allindex - 1)) {
                Gcoop.GetEl("HfIndex").value = hfindex + 1;
                initDataWindow();
            }
        }
        function Back() {
            var hfindex = Gcoop.ParseInt(Gcoop.GetEl("HfIndex").value);
            if (hfindex != 0) {
                Gcoop.GetEl("HfIndex").value = hfindex - 1;
                initDataWindow();
            }
        }
        function SheetLoadComplete() {

            var indexVal = Gcoop.GetEl("HfIndex").value;
            var allIndexVal = Gcoop.ParseInt(Gcoop.GetEl("HfAllIndex").value);
            if (indexVal == allIndexVal) {
                window.opener.RefreshByDlg();
                window.close();
            }
        }
        function MenubarOpen() {
            Gcoop.OpenDlg('790', '580', 'w_dlg_sl_popup_loanreceive.aspx', '');
        }
        function GetValueFromDlg(member_no) {
            Gcoop.GetEl("HfMemberNo").value = member_no;
            initDataWindow();
        }


        function dw_list_Click(sender, rowNumber, objectName) {

            Gcoop.GetEl("Hfloancontract_no").value = objdw_list.GetItem(rowNumber, "loancontract_no");
            Gcoop.GetEl("Hflnrcvfrom_code").value = objdw_list.GetItem(rowNumber, "lnrcvfrom_code");
            initLnRcvlist();

        }
        function MenubarNew() {

            newClear();

        }
        function OnInsert() {
            objDwOperateEtc.InsertRow(objDwOperateEtc.RowCount() + 1);
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="HfMemberNo" runat="server" />
    <asp:HiddenField ID="Hflnrcvfrom_code" runat="server" />
    <asp:TextBox ID="TextBox2" runat="server" Height="31px" Width="159px" Visible="false"></asp:TextBox>
    <asp:HiddenField ID="Hfloancontract_no" runat="server" />
    <asp:HiddenField ID="HfIndex" runat="server" />
    <asp:HiddenField ID="HfAllIndex" runat="server" />
    <asp:HiddenField ID="HfFormtype" runat="server" />
    <asp:HiddenField ID="Hfpayout_amt" runat="server" />
    <asp:HiddenField ID="Hfpayoutclr_amt" runat="server" />
    <asp:HiddenField ID="Hloancheck" runat="server" />
    <asp:HiddenField ID="Hfmoneytype_code" runat="server" />
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
  
   
    <table>
        <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_list" runat="server" DataWindowObject="d_loansrv_finlist_lnrcvbymem"
                    LibraryList="~/DataWindow/shrlon/sl_slipall.pbl" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" ClientFormatting="True"
                    ClientEventClicked="dw_list_Click" Visible="false">
                </dw:WebDataWindowControl>
            </td>
            <td align="right">
                <asp:Button ID="Button1" runat="server" Text="เลขสัญญาถัดไป" Height="36px" OnClick="Button1_Click"
                    Visible="false" />&nbsp;<asp:TextBox ID="TextBox1" runat="server" Height="31px" Width="159px"
                        Visible="false"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <br />
     <asp:CheckBox ID="CheckBox1" runat="server" 
        oncheckedchanged="CheckBox1_CheckedChanged" /><asp:Label ID="Label1" runat="server"
            Text="ออกใบเสร็จ"></asp:Label>
    <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_sl_payoutslip_lnrcv"
        LibraryList="~/DataWindow/shrlon/sl_slipall.pbl" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" ClientEventItemChanged="OnDwMainItemChanged"
        ClientFormatting="True" ClientEventButtonClicked="Create_loan_Click">
    </dw:WebDataWindowControl>
    <br />
    <dw:WebDataWindowControl ID="DwOperateLoan" runat="server" DataWindowObject="d_sl_payinslip_loan"
        LibraryList="~/DataWindow/shrlon/sl_slipall.pbl" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" Height="150px" Width="750px" ClientScriptable="True"
        ClientEventClicked="OnDwLoanItemClicked" ClientEventItemChanged="OnDwLoanItemChanged"
        ClientFormatting="True">
    </dw:WebDataWindowControl>
    <br />
    <dw:WebDataWindowControl ID="DwOperateEtc" runat="server" DataWindowObject="d_sl_payinslip_etc"
        LibraryList="~/DataWindow/shrlon/sl_slipall.pbl" AutoRestoreContext="False" Height="150px"
        Width="750px" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        ClientScriptable="True">
    </dw:WebDataWindowControl>
    <span class="linkSpan" onclick="OnInsert()" style="font-size: small; color: Red;
        float: left">เพิ่มแถว</span>
</asp:Content>
