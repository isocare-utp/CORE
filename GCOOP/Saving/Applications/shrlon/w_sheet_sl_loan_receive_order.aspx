<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_sl_loan_receive_order.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_loan_receive_order" %>

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
    <%=jspopupAgreeLoanReport%>
    <%=jspopupAgreeCollReport%>
    <%=jspopupCollReport%>
    <%=jspopupReportInvoice%>
    <%=jspopupLoanReport%>
    <%=jspopupDeptReport%>

      <%=jsPrintAppletPB%>
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

                objDwMain.SetItem(1, "member_no", newValue);
                objDwMain.AcceptText();
                Gcoop.GetEl("HfMemberNo").value = newValue;
                initDataWindow();
            }
           
        }
        function OnDwLoanItemClicked(sender, rowNumber, objectName) {
            if (objectName == "operate_flag") {
                Gcoop.CheckDw(sender, rowNumber, objectName, "operate_flag", 1, 0);
                var flag = objDwOperateLoan.GetItem(rowNumber, "operate_flag");
                if (flag == 1) {
                    Gcoop.GetEl("Hrow").value = rowNumber + "";
                    var bfshrcont_balamt = objDwOperateLoan.GetItem(rowNumber, "bfshrcont_balamt");
                    objDwOperateLoan.SetItem(rowNumber, "principal_payamt", bfshrcont_balamt);             
                    initLnRcvReCalInt();
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
            if (columnName == "principal_payamt") {
                objDwOperateLoan.SetItem(rowNumber, columnName, newValue);
                objDwOperateLoan.AcceptText();
                calculateitempayamt();
            }
            else if (columnName == "interest_payamt") {

                objDwOperateLoan.SetItem(rowNumber, columnName, newValue);
                objDwOperateLoan.AcceptText();    
                setPayoutclrAmt();
               
            }
        }


        function OnDwOperateEtcItemChanged(sender, rowNumber, columnName, newValue) {
            //Set ค่า ให้ HiddenField ว่า กดจาก Row ไหน
            //alert(columnName);
            Gcoop.GetEl("HfRowSelected").value = rowNumber;
            //ถ้ากรอกจำนวนเงิน
            if (columnName == "item_payamt") {
                //get ค่า จาก operate_flag ที่ คลิก
                var operateflagValue = objDwOperateEtc.GetItem(rowNumber, "operate_flag");
                //มีการเช็คถูก
                if (operateflagValue != 0) {
                    objDwOperateEtc.SetItem(rowNumber, "item_payamt", newValue);
                    objDwOperateEtc.AcceptText();
                    objDwMain.AcceptText();
                    calculateitempayamt();

                }
            }
        }

        function OnDwOperateEtcItemClicked(sender, rowNumber, columnName) {

            if (columnName == "operate_flag") {

                Gcoop.CheckDw(sender, rowNumber, columnName, "operate_flag", 1, 0);
                var flag = objDwOperateEtc.GetItem(rowNumber, "operate_flag");
                if (flag == 1) {

                    Gcoop.GetEl("Hrow").value = rowNumber + "";
                    // calculateitempayamt();
                } else {
                    objDwOperateEtc.SetItem(rowNumber, "item_payamt", 0);
                    objDwOperateEtc.AcceptText();
                    objDwMain.AcceptText();
                    setPayoutclrAmt();
                }
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
        if (Gcoop.GetEl("HdPrint").value == "true") {
            if (confirm("คุณต้องการพิมพ์หรือไม่")) {
                jsPrintAppletPB();
            }
            Gcoop.GetEl("HdPrint").value = "false";
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
        //ฟังก์ชั่นรายงาน**********************************************************************
        function OnClickLoan() {

            objDwMain.AcceptText();
            jspopupLoanReport();
        }

        function OnclickLinkNextinvoice() {
            objDwMain.AcceptText();
            jspopupReportInvoice();
        }
        function OnClickAgreeLoan() {

            objDwMain.AcceptText();
            jspopupAgreeLoanReport();
        }

        function OnclickColl() {
            objDwMain.AcceptText();
            jspopupCollReport();
        }
        function OnClickAgreeColl() {

            objDwMain.AcceptText();
            jspopupAgreeCollReport();
        }
        function OnClickdept() {

            objDwMain.AcceptText();
            jspopupDeptReport();
        }


        //*************************************************************************************
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
    <asp:HiddenField ID="HfRowSelected" runat="server" />
    <asp:HiddenField ID="Hrow" runat="server" />
    <asp:HiddenField ID="HdOpenIFrame" runat="server" Value="False" />
    <asp:HiddenField ID="HdcheckPdf" runat="server" Value="False" />

     <asp:HiddenField ID="HdPrint" runat="server" />
     <asp:HiddenField ID="HdSlipno" runat="server" Value="false" />
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    <%--<span style="cursor: pointer" onclick="OnClickdept();">-พิมพ์หักเงินฝาก</span>
    <br />
    <span style="cursor: pointer" onclick="OnClickLoan();">-พิมพ์สัญญาเงินกู้</span>
    <br />
    <span style="cursor: pointer" onclick="OnClickAgreeLoan();">-พิมพ์สัญญายินยอมผู้กู้</span>
    <br />
    <span style="cursor: pointer" onclick="OnclickColl();">-พิมพ์สัญญาผู้ค้ำ </span>
    <br />
    <span style="cursor: pointer" onclick="OnClickAgreeColl();">-พิมพ์สัญญายินยอมผู้ค้ำ</span>
--%>
    <br />
     <asp:LinkButton ID="LinkButton5" runat="server" Visible="false" OnClick="LinkButton5_Click">พิมพ์หักเงินฝาก</asp:LinkButton>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:LinkButton ID="LinkButton6" runat="server" Visible="false" OnClick="LinkButton6_Click">พิมพ์สัญญาเงินกู้ฉุกเฉิน</asp:LinkButton>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="LinkButton3" runat="server" Visible="false" OnClick="LinkButton3_Click">พิมพ์สัญญาเงินกู้</asp:LinkButton>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="LinkButton4" runat="server" Visible="false" OnClick="LinkButton4_Click">พิมพ์สัญญายินยอมผู้กู้</asp:LinkButton>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
    <br />
    <asp:LinkButton ID="LinkButton1" runat="server" Visible="false" OnClick="LinkButton1_Click">พิมพ์สัญญาผู้ค้ำ</asp:LinkButton>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="LinkButton2" runat="server" Visible="false" OnClick="LinkButton2_Click">พิมพ์สัญญายินยอมผู้ค้ำ</asp:LinkButton>
    <br />
    <br />
    <table>
        <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_list" runat="server" DataWindowObject="d_sl_listbymem_lnrcv"
                    LibraryList="~/DataWindow/shrlon/sl_loansrv_slip_all_cen.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    ClientFormatting="True" ClientEventClicked="dw_list_Click" Visible="false">
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
    <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" /><asp:Label
        ID="Label1" runat="server" Text="ออกใบเสร็จ"></asp:Label>
    <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_loansrv_lnpayout_slip"
        LibraryList="~/DataWindow/shrlon/sl_loansrv_slip_all_cen.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        ClientEventItemChanged="OnDwMainItemChanged" ClientFormatting="True" ClientEventButtonClicked="Create_loan_Click">
    </dw:WebDataWindowControl>
    <br />
    <dw:WebDataWindowControl ID="DwOperateLoan" runat="server" DataWindowObject="d_loansrv_lnpayin_slipdet"
        LibraryList="~/DataWindow/shrlon/sl_loansrv_slip_all_cen.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" Height="150px"
        Width="750px" ClientScriptable="True" ClientEventClicked="OnDwLoanItemClicked"
        ClientEventItemChanged="OnDwLoanItemChanged" ClientFormatting="True">
    </dw:WebDataWindowControl>
    <br />
    <dw:WebDataWindowControl ID="DwOperateEtc" runat="server" DataWindowObject="d_sl_payinslip_etc"
        LibraryList="~/DataWindow/shrlon/sl_loansrv_slip_all_cen.pbl" AutoRestoreContext="False"
        Height="150px" Width="750px" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        ClientScriptable="True" ClientFormatting="True" ClientEventClicked="OnDwOperateEtcItemClicked"
        ClientEventItemChanged="OnDwOperateEtcItemChanged">
    </dw:WebDataWindowControl>
    <span class="linkSpan" onclick="OnInsert()" style="font-size: small; color: Red;
        float: left">เพิ่มแถว</span>
</asp:Content>
