<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_getyear.aspx.cs"
    Inherits="Saving.Applications.account.dlg.w_dlg_getyear" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<script type="text/javascript">
    function OnDwDateClicked(sender, row, bName) {
        var year = objDwYear.GetItem(1, "year") - 543;
        if (year != "" || year != null) {
            parent.GetYear(year);
            parent.RemoveIFrame();
        }
        else {
            confirm("กรุณาระบุปีบัญชีก่อน");
        }
    }
</script>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <dw:WebDataWindowControl ID="DwYear" runat="server" DataWindowObject="dlg_getyear"
            LibraryList="~/DataWindow/account/budget.pbl" AutoRestoreContext="False" AutoRestoreDataCache="True"
            AutoSaveDataCacheAfterRetrieve="True" ClientEventButtonClicked="OnDwDateClicked"
            ClientScriptable="True" ClientFormatting="True">
        </dw:WebDataWindowControl>
    </div>
    </form>
</body>
</html>
