<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_loanrequest_loanrightchoose_share.aspx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_loanrequest_loanrightchoose_share"   ValidateRequest="false"  %>



<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>หลักทรัพย์ค้ำประกัน</title>
    <%=refreshDW%>
    <%=jsLoanclrchg%>
    <%=jscolldetail%>
    <script type="text/javascript">

    function DialogLoadComplete(){
        var chkConfirm = Gcoop.GetEl("HdChkConfirm").value;
           // alert("ddd " + chkConfirm);
        if (chkConfirm == "0") {
            alert("กรุณาเลือกรายการอย่างน้อย 1 รายการ");
            Gcoop.GetEl("HdChkConfirm").value = "";
        } else if (chkConfirm == "1") {
            //  alert("sending");
            var loanpermiss = Gcoop.GetEl("HdLoanMiss").value;
            var xmlcoll = Gcoop.GetEl("Hdxmlcoll").value;
            var xmlclear = Gcoop.GetEl("Hdxmlclear").value;

            window.opener.LoadDWColl(loanpermiss, xmlcoll, xmlclear);
            window.close();

        }
        else if (chkConfirm == "2") {
            Gcoop.GetEl("HdChkConfirm").value = "";
        }
    }
    function OnDwCollRightClick(sender, rowNumber, objectName){
        if (objectName == "operate_flag") {
            Gcoop.GetEl("HdCollRowNumber").value = rowNumber;
            Gcoop.CheckDw(sender, rowNumber, objectName, "operate_flag", 1, 0);
           // refreshDW();
        }
   }
   function ItemDwClearChanged(sender, rowNumber, columnName, newValue) {
       objdw_clear.SetItem(rowNumber, columnName, newValue);
       objdw_clear.AcceptText();
       if (columnName == "clear_status") {
          
            jsLoanclrchg();
       }

   }

   //Event----->ClientEventButtonClicked="OnDwClearClicked"
   function OnDwClearClicked(sender, rowNumber, objectName) {
       if (objectName == "clear_status") {
           Gcoop.CheckDw(sender, rowNumber, objectName, "clear_status", 1, 0);
          
           jsrecalloanpermiss();
       }
       else if (objectName == "b_detail") {
           var loanContractNo = objdw_clear.GetItem(rowNumber, "loancontract_no");
           Gcoop.GetEl("HdContno").value = loanContractNo;
           jscolldetail();
          // alert(loanContractNo);
           //Gcoop.openDlg("650", "590", "w_dlg_sl_detail_contract.aspx", "?lcontno=" + loanContractNo);

       }
      
   }
   
    </script>
</head>
<body>
 <asp:Literal ID="Ltjspopupclr" runat="server"></asp:Literal>
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_collright" runat="server" AutoRestoreContext="False" 
                AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                ClientScriptable="True" ClientEventClicked="OnDwCollRightClick"
                 DataWindowObject ="d_sl_loanrequest_loanrightchoose_dep" 
                LibraryList ="~/DataWindow/shrlon/sl_loan_requestment_cen.pbl" ClientEventItemChanged="ItemChangeDwCollR" RowsPerPage="10" PageNavigationBarSettings-Visible="True" PageNavigationBarSettings-NavigatorType="NumericWithQuickGo">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
        <br />
        </tr>
        <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_clear" runat="server" AutoRestoreContext="False" 
                AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                ClientScriptable="True"  DataWindowObject ="d_sl_loanrequest_clear" 
                LibraryList ="~/DataWindow/shrlon/sl_loan_requestment_cen.pbl" ClientEventClicked="OnDwClearClicked" ClientEventItemChanged="ItemDwClearChanged"  >
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_colldetail" runat="server" AutoRestoreContext="False" 
                AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                ClientScriptable="True" ClientEventClicked="OnDwCollRightClick"
                 DataWindowObject ="d_sl_cont_detail_collateral" 
                LibraryList ="~/DataWindow/shrlon/sl_member_detail.pbl" ClientEventItemChanged="mm" RowsPerPage="10" PageNavigationBarSettings-Visible="True" PageNavigationBarSettings-NavigatorType="NumericWithQuickGo">
                </dw:WebDataWindowControl>
            </td>
        </tr>

         <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_coll" Visible = "false" runat="server" AutoRestoreContext="False" 
                AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                ClientScriptable="True" ClientEventClicked="OnDwClearClick"
                 DataWindowObject ="d_sl_loanrequest_collateral" 
                LibraryList ="~/DataWindow/shrlon/sl_loan_requestment_cen.pbl" ClientEventItemChanged="ItemChangeDwClear">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td>
            <br /><br />
            <asp:Button ID="btn_confirm" runat="server" Text="ยืนยัน" 
             onclick="btn_confirm_Click" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdChkConfirm" runat="server" />
    <asp:HiddenField ID="HdMemberNo" runat="server" />
    <asp:HiddenField ID="HdLoantype" runat="server" />  
    <asp:HiddenField ID="HdRowNumber" runat="server" />
    <asp:HiddenField ID="HdCollRowNumber" runat="server" />
    <asp:HiddenField ID="HdLoanMiss" runat="server" />
    <asp:HiddenField ID="Hdxmlcoll" runat="server" />
    <asp:HiddenField ID="Hdxmlclear" runat="server" />
    <asp:HiddenField ID="HdContno" runat="server" />
    </form>
</body>
</html>
