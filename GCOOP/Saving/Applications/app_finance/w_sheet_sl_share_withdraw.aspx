<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_share_withdraw.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_sheet_sl_share_withdraw" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <%=initJavaScript %>
    <%=initDataWindow %>
    <%=loanCalInt %>
    <%=calculateAmt%>
    <%=saveWithdraw%>
    <%=calculateitempayamt%>
    <%=checkvalue %>
    <%=getMemberNo %>
    <%=initLnRcvlist%>
    <%=fittermoneytype%>
    <%=newclear%>
    <script type="text/javascript">
        function Validate() {

            return confirm("ยืนยันการบันทึกข้อมูล?");
        }
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
            else if (columnName == "member_no") {
                objDwMain.SetItem(1, "columnName", Gcoop.StringFormat(newValue, "000000"));
                objDwMain.AcceptText();
                Gcoop.GetEl("HfMemberNo").value = Gcoop.StringFormat(newValue, "000000");
                getMemberNo();
            } else if (columnName == "moneytype_code") {
                //  alert(columnName);
                objDwMain.SetItem(rowNumber, columnName, newValue);
                objDwMain.AcceptText();
                Gcoop.GetEl("Hfmoneytype_code").value = objDwMain.GetItem(rowNumber, "moneytype_code");
                fittermoneytype();

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



        function SheetLoadComplete() {


            if (Gcoop.GetEl("HdIsPostBack").value != "true") {
                // alert(Gcoop.GetEl("HdIsPostBack").value);
                Gcoop.SetLastFocus("member_no_0");
                Gcoop.Focus();
            }

        }

        //        function SaveWithdraw() {
        //            var payoutnetamt = objDwMain.GetItem(1, "payoutnet_amt");
        //            var bfshrcontbalamt = objDwMain.GetItem(1, "bfshrcont_balamt");
        //            var payoutclramt = objDwMain.GetItem(1, "payoutclr_amt");
        //            var totalamt = bfshrcontbalamt - payoutclramt;
        //            //        if(payoutnetamt<=0){
        //            //            alert("ไม่สามารถ บันทึกได้ เนื่องจาก ไม่มียอดการชำระ ");
        //            //            return false;
        //            //        }
        //            //       objDwMain.SetItem(1,"payoutnet_amt",0);
        //            if (totalamt >= 0) {
        //                objDwMain.AcceptText();
        //                objDwOperateLoan.AcceptText();
        //                saveWithdraw();
        //            } else {
        //                alert("ไม่สามารถ บันทึกได้ เนื่องจาก ยอดชำระค่าหุ้น ติดลบ");
        //                return false;
        //            }
        //        }

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
        function dw_list_Click(sender, rowNumber, objectName) {

            Gcoop.GetEl("Hfpayoutorder_no").value = objdw_list.GetItem(rowNumber, "payoutorder_no");

            initLnRcvlist();

        }

        function ChangedwEtc(sender, rowNumber, columnName, newValue) {
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

                    calculateAmt();
                }
            }
        }
        function MenubarNew() {
            Gcoop.SetLastFocus("member_no_0");
            Gcoop.Focus();
            newclear();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="HfIndex" runat="server" />
    <asp:HiddenField ID="HfAllIndex" runat="server" />
    <asp:HiddenField ID="HfFormType" runat="server" />
    <asp:HiddenField ID="HfMemberNo" runat="server" />
    <asp:HiddenField ID="Hfpayoutorder_no" runat="server" />
    <asp:HiddenField ID="Hfmoneytype_code" runat="server" />
    <asp:HiddenField ID="HfRowSelected" runat="server" />
     <asp:HiddenField ID="HdIsPostBack" runat="server" Value="false" />
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
     <asp:CheckBox ID="CheckBox1" runat="server" 
        oncheckedchanged="CheckBox1_CheckedChanged" /><asp:Label ID="Label1" runat="server"
            Text="ออกใบเสร็จ"></asp:Label>
    <table border="0">
        <tr>
            <td>
                <asp:TextBox ID="TextBox2" runat="server" Height="31px" Width="159px" Visible="false"></asp:TextBox>
                <dw:WebDataWindowControl ID="dw_list" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" ClientEventClicked="dw_list_Click" Visible="false"
                    ClientFormatting="True" ClientScriptable="True" DataWindowObject="d_sl_listbymem_finshrwtd"
                    LibraryList="~/DataWindow/Shrlon/sl_slipall.pbl">
                </dw:WebDataWindowControl>
                <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_sl_payoutslip_shrwtd"
                    LibraryList="~/DataWindow/Shrlon/sl_slipall.pbl" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" ClientEventItemChanged="OnDwMainItemChanged"
                    ClientFormatting="True" ClientEventClicked="OnDwListClicked">
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
                    ClientEventClicked="operate_flag_Click" ClientEventItemChanged="ChangedwEtc">
                </dw:WebDataWindowControl>
                <span class="linkSpan" onclick="OnInsert()" style="font-size: small; color: Red;
                    float: left">เพิ่มแถว</span>
            </td>
        </tr>
    </table>
</asp:Content>
