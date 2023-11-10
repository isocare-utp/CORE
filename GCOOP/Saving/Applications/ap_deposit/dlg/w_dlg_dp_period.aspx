<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_dp_period.aspx.cs" Inherits="Saving.Applications.ap_deposit.dlg.w_dlg_dp_period" %>

<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>ระบบงานเงินฝาก</title>
        <%=postUpdate%>
        <script type ="text/javascript">
        
        function DialogLoadComplete()
        {
            var check = Gcoop.GetEl("HdCloseDlg").value;
            if(check == "true"){
                window.close();
            }
        }
        
        function OnDwMainButtonClick(sender, rowNumber, buttonName){

            if (buttonName == "cb_ok")
            {
                var con = confirm("ท่านต้องการบันทึกแก้ไข หรือไม่?");
                if (con == true){
                    postUpdate();
                }
                else{
                    window.close();
                }
            }
            else if (buttonName == "cb_cancel")
            {
                window.close();
            }
        }
        
    </script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" 
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
            ClientScriptable="True" DataWindowObject="d_dp_depttype_period" 
            LibraryList="~/DataWindow/ap_deposit/dpdepttypecond.pbl" 
            ClientEventButtonClicked="OnDwMainButtonClick" 
            ClientEventItemChanged="OnDwMainItemChanged">
        </dw:WebDataWindowControl>
    
    </div>
        <asp:HiddenField ID="HdDeptType" runat="server" />
        <asp:HiddenField ID="HdCloseDlg" runat="server" />
    </form>
</body>
</html>

