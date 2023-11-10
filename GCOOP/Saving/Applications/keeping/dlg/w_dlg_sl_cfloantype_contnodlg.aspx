<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_cfloantype_contnodlg.aspx.cs"
    Inherits="Saving.Applications.keeping.dlg.w_dlg_sl_cfloantype_contnodlg" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>เลขทีสัญญาอ้างอิงจากรหัส</title>

    <script type="text/javascript">
        function OnClick(sender, rowNumber, objectName) {
            try {
                var cont_code = objdw_data.GetItem(rowNumber, "document_code");
                var cont_desc = objdw_data.GetItem(rowNumber, "document_name");
                alert(cont_code + " : " + cont_desc);
                window.opener.GetContNo(cont_code);
                window.close();
            } catch (ex) {
                alert("Can't get ContNo. r: " + rowNumber);
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dw:WebDataWindowControl ID="dw_data" runat="server" ClientScriptable="True" DataWindowObject="d_sl_cflntype_contdlg"
            LibraryList="~/DataWindow/shrlon/sl_cfloantype.pbl" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventClicked="OnClick"
            Height="400px" Width="450px">
        </dw:WebDataWindowControl>
    </div>
    </form>
</body>
</html>
