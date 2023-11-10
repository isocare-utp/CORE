<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_loanrequest_duplicate.aspx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_loanrequest_duplicate" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>เลขที่ใบสัญญาเดิม</title>
     <script type="text/javascript">
    function OnDetailClick(sender, rowNumber, objectName){
        if(objectName!= "datawindow" && rowNumber > 0){
            var docno = objdw_return.GetItem(rowNumber , "loanrequest_docno");
            window.opener.OldDocNo(docno);
            window.close();
//            try{
//                var docno = objdw_return.GetItem(rowNumber , "loanrequest_docno");
////                if(confirm ("ยืนยันการเลือกเลขใบคำขอกู้ " + docno )){
////                    
////                }
//                parent.OldDocNo(docno);
//            }catch(Err){
//                //alert("Error Dlg");
//                //window.close();
//                parent.RemoveIFrame();
//            }
       }
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dw:WebDataWindowControl ID="dw_return" runat="server" 
        AutoRestoreContext="False" AutoRestoreDataCache="True" 
        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
         DataWindowObject="d_sl_loanrequest_duplicate" 
         LibraryList="~/DataWindow/shrlon/sl_loan_requestment.pbl" ClientEventClicked="OnDetailClick">
        </dw:WebDataWindowControl>
    </div>
    </form>
    
</body>
</html>
