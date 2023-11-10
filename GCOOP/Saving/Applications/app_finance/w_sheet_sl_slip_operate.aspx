<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_slip_operate.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_sheet_sl_slip_operate" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=initLnRcv%>
    <%=initSlipCalInt%>
    <%=getPeriodCont%>
    <%=calculateAmt%>
    <%=fittermoneytype%>
    <%=jsrefresh %>
    <%=directCalAmt%>
    <%=calinterest%>
    <%=changeMemCoop%>
    <%=jssetShare %>
    <%=jsSetLoan%>
    <%=JsGetPeriodContShr%>
    <%=JsPostPayspecChange%>>
    <style type="text/css">
        .style1
        {
            color: #009999;
            padding-left: 10px;
            font-family: Tahoma, 'Times New Roman' , Times;
            font-weight: bold;
            font-size: 15px;
            text-decoration: underline;
            padding-left: 10px;
        }
        .style2
        {
            color: #9900CC;
            text-decoration: underline;
            padding-left: 10px;
            font-family: Tahoma, 'Times New Roman' , Times;
            font-weight: bold;
            font-size: 15px;
        }
        .style3
        {
            color: #999966;
            text-decoration: underline;
            padding-left: 10px;
            font-family: Tahoma, 'Times New Roman' , Times;
            font-weight: bold;
            font-size: 15px;
        }
        .style4
        {
            font-family: Tahoma, 'Times New Roman' , Times;
            font-weight: bold;
            font-size: 15px;
            color: #0066BB;
            text-decoration: underline;
            padding-left: 10px;
        }
    </style>
    <script type="text/javascript">
        function GetValueFromDlg(strvalue) {
            // objdw_main.SetItem(1, "member_no", Gcoop.StringFormat(strvalue, "00000000"));
            objdw_main.SetItem(1, "member_no", strvalue);
            objdw_main.AcceptText();
            initLnRcv();
        }

        function IsInit() {
            try {
                var haveMem = objdw_main.GetItem(1, "member_no");
                if (haveMem != "") {
                    return true;
                }
            } catch (err) {
                return false;
            }
        }

        function IsLoanChecked() {
            var row = objdw_loan.RowCount();
            for (i; i <= row; i++) {
                var isCheck = objdw_loan.GetItem(i, "operate_flag");
                if (isCheck == "1") {
                    return true;
                }
            }
        }

       

        function IsShareChecked() {
            var row = objdw_share.RowCount();
            for (i; i <= row; i++) {
                var isCheck = objdw_share.GetItem(i, "operate_flag");
                if (isCheck == "1") {
                    return true;
                }
            }
        }

        function LoanUnChecked(sender, rowNumber, columnName, newValue) {
            objdw_loan.SetItem(rowNumber, "interest_payamt", 0);
            objdw_loan.SetItem(rowNumber, "principal_payamt", 0);
            objdw_loan.SetItem(rowNumber, "item_payamt", 0);
            objdw_loan.SetItem(rowNumber, "item_balance", objdw_loan.GetItem(rowNumber, "bfshrcont_balamt"));
        }

        function MenubarNew() {
            if (confirm("ยืนยันการล้างหน้าจอ")) {
                window.location = "";
            }
        }

        function OnDwLoanClicked(s, r, c) {
            Gcoop.CheckDw(s, r, c, "operate_flag", 1, 0);
            Gcoop.CheckDw(s, r, c, "periodcount_flag", 1, 0);
            var sliptype_code = objdw_main.GetItem(1, "sliptype_code");
            var v = s.GetItem(r, c);
            if (c == "periodcount_flag") {
                getPeriodCont();
            } else if (c == "operate_flag") {
                // alert(v);
                Gcoop.GetEl("HfRow").value = r + "";

                if (v == 0) {
                    objdw_loan.SetItem(r, "item_payamt", 0);
                    objdw_loan.SetItem(r, "principal_payamt", 0);
                    objdw_loan.SetItem(r, "interest_payamt", 0);
                    if (objdw_loan.GetItem(r, "periodcount_flag") == 1) {
                        objdw_loan.SetItem(r, "periodcount_flag", 0);
                        objdw_loan.SetItem(r, "period", objdw_loan.GetItem(r, "period") - 1);
                    }
                    objdw_loan.AcceptText();
                } else if (v == 1) {
                    if (sliptype_code=="PMP") {
                    objdw_loan.SetItem(r, "periodcount_flag", 1);
                    objdw_loan.SetItem(r, "period", objdw_loan.GetItem(r, "period") + 1);
                    objdw_loan.AcceptText();
                    }
                }
                jsSetLoan()
                //calculateAmt();
            }
        }

        function OnDwLoanItemChanged(sender, rowNumber, columnName, newValue) {
            //Set ค่า ให้ HiddenField ว่า กดจาก Row ไหน
            Gcoop.GetEl("HfRowSelected").value = rowNumber;
            //get ค่า จาก operate_flag ที่ คลิก
            var operateflagValue = objdw_loan.GetItem(rowNumber, "operate_flag");
            var bfpayspec_method = objdw_loan.GetItem(rowNumber, "bfpayspec_method");

            //ถ้ากรอกจำนวนเงิน
            if (columnName == "item_payamt" && bfpayspec_method == "1") {
                //มีการเช็คถูก
                if (operateflagValue != 0) {
                    objdw_loan.SetItem(rowNumber, "item_payamt", newValue);
                    Gcoop.GetEl("Hfitem_payamt").value = objdw_loan.GetItem(rowNumber, "item_payamt");
                    objdw_loan.AcceptText();
                    objdw_main.AcceptText();
                    calculateAmt();
                }
            } else if (columnName == "principal_payamt" && bfpayspec_method == "2") {
                // && bfpayspec_method == "2"
                if (newValue > 0) {
                    objdw_loan.SetItem(rowNumber, "operate_flag", 1);
                    objdw_loan.SetItem(rowNumber, "principal_payamt", newValue);
                    objdw_loan.SetItem(rowNumber, "item_payamt", newValue);

                    //Gcoop.GetEl("Hfitem_payamt").value = newValue;
                    objdw_loan.AcceptText();
                    Gcoop.GetEl("HdIsPostBack").value = "false";
                    calculateAmt();
                } else {
                    objdw_loan.SetItem(rowNumber, "operate_flag", 0);
                    objdw_loan.SetItem(rowNumber, "item_payamt", 0);
                    objdw_loan.SetItem(rowNumber, "principal_payamt", 0);
                    objdw_loan.SetItem(rowNumber, "interest_payamt", 0);

                    if (objdw_loan.GetItem(rowNumber, "periodcount_flag") == 1) {
                        objdw_loan.SetItem(rowNumber, "periodcount_flag", 0);
                        objdw_loan.SetItem(rowNumber, "period", objdw_loan.GetItem(rowNumber, "period") - 1);
                    }
                    objdw_loan.AcceptText();
                }
                calculateAmt();
                // jsrefresh();
            }
            else if (columnName == "principal_payamt" && bfpayspec_method == "1") {
                if (newValue >= 0) {
                    objdw_loan.SetItem(rowNumber, "operate_flag", 1);
                    objdw_loan.SetItem(rowNumber, "principal_payamt", newValue);
                    objdw_loan.SetItem(rowNumber, "item_payamt", newValue);

                    //Gcoop.GetEl("Hfitem_payamt").value = newValue;
                    objdw_loan.AcceptText();
                    Gcoop.GetEl("HdIsPostBack").value = "false";
                    calculateAmt();
                    //directCalAmt();
                }
                else {
                    objdw_loan.SetItem(rowNumber, "operate_flag", 0);
                    objdw_loan.SetItem(rowNumber, "item_payamt", 0);
                    objdw_loan.SetItem(rowNumber, "principal_payamt", 0);
                    objdw_loan.SetItem(rowNumber, "interest_payamt", 0);
                    if (objdw_loan.GetItem(rowNumber, "periodcount_flag") == 1) {
                        objdw_loan.SetItem(rowNumber, "periodcount_flag", 0);
                        objdw_loan.SetItem(rowNumber, "period", objdw_loan.GetItem(rowNumber, "period") - 1);
                    }
                    objdw_loan.AcceptText();
                }
            }
            else if (columnName == "interest_payamt") {
                // && bfpayspec_method == "2"
                objdw_loan.SetItem(rowNumber, "operate_flag", 1);
                objdw_loan.SetItem(rowNumber, "interest_payamt", newValue);
                //Gcoop.GetEl("Hfinterest_payamt").value = objdw_loan.GetItem(rowNumber, "interest_payamt");
                objdw_loan.AcceptText();
                //calinterest();
                directCalAmt();
                return 0;
            }
        }

        function loanError(s, r, c, v) {
            return 1;
        }

        function OnDwMainButtonClicked(sender, rowNumber, buttonName) {
            if (buttonName == "b_search") {
                //Gcoop.OpenDlg('570', '590', 'w_dlg_sl_member_search.aspx', '');
                Gcoop.OpenDlg('620', '590', 'w_dlg_sl_member_search.aspx', '');
            }
            if (buttonName == "b_acci") {

                var memberNoVal = objdw_main.GetItem(1, "member_no");
                if ((memberNoVal != null) && (memberNoVal != "")) {
                    Gcoop.OpenDlg(620, 250, "w_dlg_show_accid.aspx", "?member=" + memberNoVal);
                }
            }
        }

        function GetValueAccID(dept_no, deptaccount_name, prncbal) {
            objdw_main.SetItem(1, "expense_accid", dept_no);
            objdw_main.AcceptText();

        }

        function OnDwMainItemChanged(sender, rowNumber, columnName, newValue) {
            if (columnName == "member_no") {
                objdw_main.SetItem(1, "member_no", newValue);
                objdw_main.AcceptText();
                if (Gcoop.Trim(newValue) != "") {
                    initLnRcv();
                }
                return 0;
            } else if (columnName == "operate_tdate") {
                var memNo = Gcoop.Trim(objdw_main.GetItem(1, "member_no"));
                if (memNo != "") {
                    objdw_main.SetItem(1, "operate_tdate", newValue);
                    objdw_main.AcceptText();
                    objdw_main.SetItem(1, "operate_date", Gcoop.ToEngDate(newValue));
                    objdw_main.AcceptText();
                    initSlipCalInt();
                }
                return 0;
            } else if (columnName == "moneytype_code") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                fittermoneytype();
            } else if (columnName == "crosscoop_flag") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                jsrefresh();
            } else if (columnName == "memcoop_id") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                changeMemCoop();
            }

            if (columnName == "forword_flag") {
                objdw_main.SetItem(rowNumber, "forword_flag", newValue);
                objdw_main.AcceptText();
                JsPostPayspecChange();
            }
        }

//         function PayspecChange() {
//             var row = objdw_loan.RowCount();
//             alert(row);
//             var i = 1;
//            for (i=1; i <= row; i++) {
//                var isCheck = objdw_loan.GetItem(i, "bfpayspec_method");
//                alert(isCheck);
//                if (isCheck == "1") {
//                     objdw_loan.SetItem(i, "bfpayspec_method", 2);
//                     objdw_loan.AcceptText();
//                }
//                if(isCheck == "2") {
//                     objdw_loan.SetItem(i, "bfpayspec_method", 1);
//                     objdw_loan.AcceptText();
//                }            

//            }

//        }

        function OnDwShareClicked(s, r, c) {
            Gcoop.CheckDw(s, r, c, "operate_flag", 1, 0);
            Gcoop.CheckDw(s, r, c, "periodcount_flag", 1, 0);
            var v = objdw_share.GetItem(r, c);

            if (c == "periodcount_flag") {
                var periodCount = objdw_share.GetItem(r, "period");
//                if (v == 1) {

//                    var totalAdd = periodCount + 1;
//                    objdw_share.SetItem(r, "period", totalAdd);
//                } else {
//                    var totalDel = periodCount - 1;
//                    objdw_share.SetItem(r, "period", totalDel);
                //                }
                objdw_share.AcceptText();
                JsGetPeriodContShr();

            } else if (c == "operate_flag") {
                Gcoop.SetLastFocus("item_payamt_" + (r - 1));
                if (v == 0) {

                    if (s.GetItem(r, "periodcount_flag") == 1) {
                        s.SetItem(r, "periodcount_flag", 0);
                        s.SetItem(r, "period", s.GetItem(r, "period") - 1);
                    }
                    s.SetItem(r, "operate_flag", 0);
                    s.SetItem(r, "item_payamt", 0);
                    s.AcceptText();

                } else {
                    s.SetItem(r, "operate_flag", 1);
                    jssetShare();
                }
                jsrefresh();

            }
        }

        function OnDwShareItemChanged(sender, rowNumber, columnName, newValue) {
            //Set ค่า ให้ HiddenField ว่า กดจาก Row ไหน
            //alert(columnName);
            Gcoop.GetEl("HfRowSelected").value = rowNumber;
            //ถ้ากรอกจำนวนเงิน
            if (columnName == "item_payamt") {
                //get ค่า จาก operate_flag ที่ คลิก
                var operateflagValue = objdw_share.GetItem(rowNumber, "operate_flag");
                //มีการเช็คถูก
                if (operateflagValue != 0) {
                    objdw_share.SetItem(rowNumber, "item_payamt", newValue);
                    objdw_share.SetItem(rowNumber, "operate_flag", operateflagValue);
                    objdw_share.AcceptText();
                    objdw_main.AcceptText();
                    //calculateAmt();
                    directCalAmt();
                }
            }
        }

        function ChangedwEtc(sender, rowNumber, columnName, newValue) {
            //Set ค่า ให้ HiddenField ว่า กดจาก Row ไหน
            //alert(columnName);
            Gcoop.GetEl("HfRowSelected").value = rowNumber;
            //ถ้ากรอกจำนวนเงิน
            if (columnName == "item_payamt") {
                //get ค่า จาก operate_flag ที่ คลิก
                var operateflagValue = objdw_etc.GetItem(rowNumber, "operate_flag");
                //มีการเช็คถูก
                if (operateflagValue != 0) {
                    objdw_etc.SetItem(rowNumber, "item_payamt", newValue);
                    objdw_etc.AcceptText();
                    objdw_main.AcceptText();
                    //calculateAmt();
                    directCalAmt();
                }
            }
        }

        function ShareUnChecked(sender, rowNumber, columnName, newValue) {
        }

        function Validate() {
            var csh = "";
            try {
                csh = objdw_main.GetItem(1, "moneytype_code");
                csh = Gcoop.Trim(csh);
            } catch (err) {
            }
            if (csh == null || csh == undefined || csh == "") {
                alert("กรุณาเลือกการทำรายการโดย");
                return false;
            }
            return confirm("บันทึกข้อมูล?");
        }

        function OnInsert() {
            objdw_etc.InsertRow(objdw_etc.RowCount() + 1);
        }

        function SheetLoadComplete() {
            if (Gcoop.GetEl("HdIsPostBack").value != "true") {

                Gcoop.SetLastFocus("member_no_0");
                Gcoop.Focus();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
    <asp:Label ID="Label1" runat="server" Text="การรับชำระจากสมาชิก" CssClass="style4"></asp:Label>
    <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_sl_payinslip"
        LibraryList="~/DataWindow/shrlon/sl_slipall.pbl" ClientScriptable="True" ClientEventButtonClicked="OnDwMainButtonClicked"
        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        ClientEventItemChanged="OnDwMainItemChanged" ClientFormatting="True" TabIndex="1">
    </dw:WebDataWindowControl>
    <br />
    <span class="style2">รายการหนี้</span>
    <dw:WebDataWindowControl ID="dw_loan" runat="server" DataWindowObject="d_sl_payinslip_loan"
        LibraryList="~/DataWindow/shrlon/sl_slipall.pbl" ClientScriptable="True" ClientEventItemChanged="OnDwLoanItemChanged"
        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        ClientFormatting="True" ClientEventClicked="OnDwLoanClicked" TabIndex="500" ClientEventItemError="loanError">
    </dw:WebDataWindowControl>
    <br />
    <asp:Label ID="Label2" runat="server" Text="รายการหุ้น" CssClass="style1"></asp:Label>
    <dw:WebDataWindowControl ID="dw_share" runat="server" DataWindowObject="d_sl_payinslip_share"
        LibraryList="~/DataWindow/shrlon/sl_slipall.pbl" ClientScriptable="True" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="OnDwShareItemChanged"
        ClientFormatting="True" ClientEventClicked="OnDwShareClicked" TabIndex="800">
    </dw:WebDataWindowControl>
    <br />
    <span class="style3">รายการอื่นๆ</span>
    <dw:WebDataWindowControl ID="dw_etc" runat="server" DataWindowObject="d_sl_payinslip_etc"
        LibraryList="~/DataWindow/shrlon/sl_slipall.pbl" ClientScriptable="True" Height="120px"
        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        ClientFormatting="True" TabIndex="900" ClientEventItemChanged="ChangedwEtc">
    </dw:WebDataWindowControl>
    <span class="linkSpan" onclick="OnInsert()" style="font-size: small; color: Red;
        float: left">เพิ่มแถว</span>
    <asp:HiddenField ID="HfRowSelected" runat="server" />
    <asp:HiddenField ID="HfRow" runat="server" />
    <asp:HiddenField ID="HfisCalInt" runat="server" />
    <asp:HiddenField ID="Hfitem_payamt" runat="server" />
    <asp:HiddenField ID="Hfinterest_payamt" runat="server" />
    <asp:HiddenField ID="HdIsPostBack" runat="server" Value="false" />
</asp:Content>
