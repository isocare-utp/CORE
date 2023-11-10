<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_ad_adduser.aspx.cs"
    Inherits="Saving.Applications.admin.dlg.w_dlg_ad_adduser" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">

        function OnDwMainClick(s, r, c, v) {
            switch (c) {
                case "user_name":
                    Gcoop.GetEl("HdRow").value = r;
                    username = objDwMain.GetItem(r, "user_name");
                    full_name = objDwMain.GetItem(r, "full_name");
                    parent.AddUser(username, full_name);
                    parent.RemoveIFrame();
                    break;
                case "full_name":
                    Gcoop.GetEl("HdRow").value = r;
                    username = objDwMain.GetItem(r, "user_name");
                    full_name = objDwMain.GetItem(r, "full_name");
                    parent.AddUser(username, full_name);
                    parent.RemoveIFrame();
                    break;
            }

        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" ClientValidation="False"
            DataWindowObject="d_um_users_name" LibraryList="~/DataWindow/admin/ad_user.pbl" ClientEventClicked="OnDwMainClick">
        </dw:WebDataWindowControl>
    
    <asp:HiddenField ID="HdRow" runat="server" Value="" />
    <input type="button" value="ปิดหน้าจอ" onclick="parent.RemoveIFrame();" />
    <asp:HiddenField ID="HdValue" runat="server" Value="" />
    </div>
    </form>
</body>
</html>
