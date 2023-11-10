<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_ad_memberof.aspx.cs"
    Inherits="Saving.Applications.admin.dlg.w_dlg_ad_memberof" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <br />
    <br />
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" ClientValidation="False"
            DataWindowObject="d_um_memberof" LibraryList="~/DataWindow/admin/ad_user.pbl">
        </dw:WebDataWindowControl>
        <br />
        <asp:HiddenField ID="HdRow" runat="server" Value="" />
        <input type="button" value="ปิดหน้าจอ" onclick="parent.RemoveIFrame();" />
        <asp:HiddenField ID="HdValue" runat="server" Value="" />
    </div>
    </form>
</body>
</html>
