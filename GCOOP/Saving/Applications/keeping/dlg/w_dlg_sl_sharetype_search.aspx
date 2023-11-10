<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_sharetype_search.aspx.cs"
    Inherits="Saving.Applications.keeping.dlg.w_dlg_sl_sharetype_search" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>เลขทีรายการเงินกู้</title>

    <script type="text/javascript">
        function OnClick(sender, rowNumber, objectName) {
            try {
                var sharecode = objdw_data.GetItem(rowNumber, "sharetype_code");
                var sharedesc = objdw_data.GetItem(rowNumber, "sharetype_desc");
                alert(sharecode + " : " + sharedesc);
                window.opener.GetShareType(sharecode, sharedesc);
                window.close();
            } catch (ex) {
                alert("Can't get ShareNo. r: " + rowNumber);
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dw:WebDataWindowControl ID="dw_data" runat="server" ClientScriptable="True" DataWindowObject="d_sl_sharetype_search"
            LibraryList="~/DataWindow/shrlon/sl_sharetype_detail.pbl" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventClicked="OnClick"
            Style="top: 0px; left: 0px; height: 400px; width: 325px">
        </dw:WebDataWindowControl>
    </div>
    </form>
</body>
</html>
