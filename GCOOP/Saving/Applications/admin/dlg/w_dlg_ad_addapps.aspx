<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_ad_addapps.aspx.cs" Inherits="Saving.Applications.admin.dlg.w_sheet_ad_addapps" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">

        function OnDwMainClick(s, r, c, v) {
            switch (c) {
                case "application":
                    Gcoop.GetEl("HdRow").value = r;
                    apps = objDwMain.GetItem(r, "application");
                    desc = objDwMain.GetItem(r, "description");

                    parent.AddApp(apps,desc);
                    parent.RemoveIFrame();
                    break;
                case "description":
                    Gcoop.GetEl("HdRow").value = r;
                    apps = objDwMain.GetItem(r, "application");
                    desc = objDwMain.GetItem(r, "description");

                    parent.AddApp(apps, desc);
                    parent.RemoveIFrame();
                    break;
            }

        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" ClientValidation="False"
            DataWindowObject="d_um_apps" LibraryList="~/DataWindow/admin/ad_group.pbl" ClientEventClicked="OnDwMainClick">
        </dw:WebDataWindowControl>
        <input type="button" value="ปิดหน้าจอ" onclick="parent.RemoveIFrame();" />
    </div>
    <asp:HiddenField ID="HdRow" runat="server" Value="" />
    <asp:HiddenField ID="HdValue" runat="server" Value="" />
    </form>
</body>
</html>

