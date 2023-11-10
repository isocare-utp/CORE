<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_finslip_tranfer_bank.aspx.cs"
 Inherits="Saving.Applications.app_finance.w_sheet_finslip_tranfer_bank" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postInitMember%>
    <%=postInitSlip%>
    <%=postSlipTranfer%>
    <%=postPrint %>
   
    <script type="text/javascript">

      
        function DwMainItemChanged(sender, rowNumber, columnName, newValue) {
            objDwMain.SetItem(rowNumber, columnName, newValue);
            objDwMain.AcceptText();

            if (columnName == "member_no") {
                var memno = Gcoop.StringFormat(newValue, "00000000");
                objDwMain.SetItem(rowNumber, "member_no", memno);
                Gcoop.GetEl("HdMemberNo").value = memno;
                postInitMember();
            }
            else if (columnName == "account_no") {
                objDwMain.SetItem(rowNumber, "account_no", newValue);
                objDwMain.AcceptText();
                Gcoop.GetEl("HdAccountNo").value = newValue;
            }
            else if (columnName == "tranfer_desc") {
                objDwMain.SetItem(rowNumber, "tranfer_desc", newValue);
                objDwMain.AcceptText();
                Gcoop.GetEl("HdTranferDesc").value = newValue;
            }
            else if (columnName == "account_name") {
                objDwMain.SetItem(rowNumber, "account_name", newValue);
                objDwMain.AcceptText();
                Gcoop.GetEl("HdAccountName").value = newValue;
            }
            else if (columnName == "branch_code") {
                objDwMain.SetItem(rowNumber, "branch_code", newValue);
                objDwMain.AcceptText();
                Gcoop.GetEl("HdBankBranch").value = newValue;
            }
            else if (columnName == "slip_type") {
                objDwMain.SetItem(rowNumber, "slip_type", newValue);
                objDwMain.AcceptText();
                Gcoop.GetEl("HdSlipType").value = newValue;
            }
            else if (columnName == "tranfer_amt") {
                objDwMain.SetItem(rowNumber, "tranfer_amt", newValue);
                objDwMain.AcceptText();
                Gcoop.GetEl("HdTranferAmt").value = newValue;
            }
            else if (columnName == "tranfre_status") {
                objDwMain.SetItem(rowNumber, "tranfre_status", newValue);
                objDwMain.AcceptText();
                Gcoop.GetEl("HdStatus").value = newValue;
            }
            else if (columnName == "tranfer_type") {
                objDwMain.SetItem(rowNumber, "tranfer_type", newValue);
                objDwMain.AcceptText();
                Gcoop.GetEl("HdTranType").value = newValue;
            }
            

        }

 
        function DwMainButtonOnClicked(sender, rowNumber, buttonName) {
            objDwMain.AcceptText();
            if (buttonName == "b_find")
            { Gcoop.OpenDlg(610, 550, "w_dlg_fin_slip.aspx", ""); }
           
        }
        // รับค่าจากการค้นหารายการ
        function GetValueFromDlg(slip_no) {
            Gcoop.GetEl("HdSlipNo").value = slip_no;
            objDwMain.SetItem(1, "refslip_no", slip_no);
            objDwMain.AcceptText();
            postInitSlip();
        }

        // รับค่าจาก slip_tran
        function GetValueSlipFromDlg(sliptran_no) {
            Gcoop.GetEl("HdSlipTranNo").value = sliptran_no;
//            objDwMain.SetItem(1, "refslip_no", slip_no);
//            objDwMain.AcceptText();
            postSlipTranfer();
        }

        function MenubarOpen() {
            Gcoop.OpenDlg(670, 550, "w_dlg_fin_sliptranfer_bank.aspx", "");
        }

        function Validate() {
            var isconfirm = confirm("ยืนยันการบันทึกข้อมูล?");

            if (!isconfirm) {
                return false;
            }
            else {

                 var alertstr = "";
                var member_no = objDwMain.GetItem(1, "member_no");
                var account_no = objDwMain.GetItem(1, "account_no");
                var tranfer_desc = objDwMain.GetItem(1, "tranfer_desc");
                var member_name = objDwMain.GetItem(1, "member_name");
                var account_name = objDwMain.GetItem(1, "account_name");
                var tranfer_amt = objDwMain.GetItem(1, "tranfer_amt");
                var refslip_amt = objDwMain.GetItem(1, "refslip_amt");
                var tranfer_type = objDwMain.GetItem(1, "tranfer_type");

                if (tranfer_type == "01") {
                    if (tranfer_amt > refslip_amt) {
                        alertstr = alertstr + "ยอดทำรายการมีค่ามากกว่ายอดตามใบเสร็จ กรุณาตรวจสอบ\n";
                    }
                }

                if (account_no == "" || account_no == null) {
                    alertstr = alertstr + "กรุณากรอกเลขบัญชี\n";
                }
                if (tranfer_desc == "" || tranfer_desc == null) {
                    alertstr = alertstr + "กรุณากรอกรายละเอียดการโอน\n";
                }
                
                if (account_name == "" || account_name == null) {
                    alertstr = alertstr + "กรุณากรอกชื่อบัญชี\n";
                }
                if (alertstr == "") {
                    return true;

                }
                else {
                    alert(alertstr);
                    return false;
                }
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<table>
<tr>
<td>
<span class="linkSpan" id="print2" runat="server" onclick="postPrint()" 
      style="font-family: Tahoma; font-size: medium; float: right; color: #0000CC;">Print
 </span>
</td>
</tr>
<tr>
<td>
    <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_fin_sliptranferbank"
        LibraryList="~/DataWindow/app_finance/finslip_spc.pbl" AutoRestoreContext="false"
        ClientScriptable="true" ClientEventButtonClicked="DwMainButtonOnClicked" AutoRestoreDataCache="True"
        ClientEventItemChanged="DwMainItemChanged" AutoSaveDataCacheAfterRetrieve="True"
        Width="600px" ClientEventClicked="DwMainClick">
    </dw:WebDataWindowControl>
</td>
</tr>
</table>

    <asp:HiddenField ID="HdMemberNo" runat="server" />
    <asp:HiddenField ID="HdSlipNo" runat="server" />
    <asp:HiddenField ID="HdAccountNo" runat="server" />
    <asp:HiddenField ID="HdAccountName" runat="server" />
    <asp:HiddenField ID="HdTranferDesc" runat="server" />
    <asp:HiddenField ID="HdBankBranch" runat="server" />
    <asp:HiddenField ID="HdSlipType" runat="server" />
    <asp:HiddenField ID="HdTranferAmt" runat="server" />
    <asp:HiddenField ID="HdStatus" runat="server" />
    <asp:HiddenField ID="HdTranType" runat="server" />
    <asp:HiddenField ID="HdSlipTranNo" runat="server" /> 

      <%=outputProcess%>
</asp:Content>

