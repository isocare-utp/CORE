<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_dp_extmember_search.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.dlg.w_dlg_dp_extmember_search" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ค้นหาบุคคลภายนอก</title>
    <%=postExtramem %>
    <script type ="text/javascript" >
        function OnDwDataButtonClicked(sender, rowNumber, buttonName){
            postExtramem();
        }
        
        function selectRow(sender, rowNumber, objectName)
        {
             var deptmem_id = objDwList.GetItem(rowNumber, "deptmem_id");
             window.opener.GetValueFromDlg(deptmem_id);
             window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dw:WebDataWindowControl ID="DwData" runat="server" DataWindowObject="d_dp_extmembsrch_criteria"
            LibraryList="~/DataWindow/ap_deposit/dp_extramem.pbl" 
            ClientScriptable="True" AutoRestoreContext="False" 
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
            ClientEventButtonClicked="OnDwDataButtonClicked">
        </dw:WebDataWindowControl>
        <dw:WebDataWindowControl ID="DwList" runat="server" DataWindowObject="d_dp_extmember_search_memno_list"
            LibraryList="~/DataWindow/ap_deposit/dp_extramem.pbl" ClientScriptable="True"
            ClientEventClicked="selectRow" AutoRestoreContext="False" 
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True">
        </dw:WebDataWindowControl>
    </div>
    </form>
</body>
</html>
