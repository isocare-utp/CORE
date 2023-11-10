<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_dp_current_account_no.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.dlg.w_dlg_dp_current_account_no" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ตรวจสอบแก้ไขเลขที่บัญชีล่าสุด</title>
    <%=postUpdate%>

    <script type="text/javascript">
        function OnDwMainButtonClicked(sender, rowNumber, buttonName){
            if(buttonName == "cb_ok"){
                if(confirm("ต้องการบันทึกเลขที่บัญชีล่าสุด ใช่หรือไม่?")){
                    postUpdate();
                }
            }
            else if (buttonName == "cb_cancel"){
                window.close();
            }     
        }
        
        function OnDwMainItemChanged(sender, rowNumber, columnName, newValue){
            if(columnName == "last_documentno")
            {
                objDwMain.SetItem(rowNumber,columnName,newValue);
                objDwMain.AcceptText();
            }
        }
        
        function DialogLoadComplete()
        {
            var check = Gcoop.GetEl("HdCloseDlg").value;
            if(check == "true"){
                window.close();
            }
        }
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_dp_current_account_no"
        LibraryList="~/DataWindow/ap_deposit/dp_current_account_no.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        ClientEventButtonClicked="OnDwMainButtonClicked" ClientEventItemChanged="OnDwMainItemChanged">
    </dw:WebDataWindowControl>
    <div>
    </div>
    <asp:HiddenField ID="HdCloseDlg" runat="server" />
    </form>
</body>
</html>
