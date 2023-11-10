<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_loanrequest_loanrightchoose.aspx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_loanrequest_loanrightchoose" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>หลักทรัพย์ค้ำประกัน</title>
    <%=refreshDW%>
    <script type="text/javascript">

    function DialogLoadComplete(){
            var chkConfirm = Gcoop.GetEl("HdChkConfirm").value;
            if (chkConfirm =="0"){
                alert ("กรุณาเลือกรายการอย่างน้อย 1 รายการ");
                Gcoop.GetEl("HdChkConfirm").value = "";
            }else if(chkConfirm == "1")
            {
                
                window.opener.LoadDWColl();
                window.close();
//                try{
//                Gcoop.GetEl("HdChkConfirm").value = "";
//                parent.LoadDWColl();
//            }catch(Err){
//                alert("Error Dlg");
//                //window.close();
//                parent.RemoveIFrame();
//            }
            }
    }
    function OnDwCollRightClick(sender, rowNumber, objectName){
        if(objectName == "operate_flag"){
                Gcoop.CheckDw(sender, rowNumber, objectName, "operate_flag", 1, 0 );
                refreshDW();
        }
    }
   
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_collright" runat="server" AutoRestoreContext="False" 
                AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                ClientScriptable="True" ClientEventClicked="OnDwCollRightClick"
                 DataWindowObject ="d_sl_loanrequest_loanrightchoose" 
                LibraryList ="~/DataWindow/shrlon/sl_loan_requestment.pbl" ClientEventItemChanged="ItemChangeDwCollR">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td>
            <asp:Button ID="btn_confirm" runat="server" Text="ยืนยัน" 
             onclick="btn_confirm_Click" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdChkConfirm" runat="server" />

    </form>
</body>
</html>
