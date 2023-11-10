<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_del.aspx.cs" Inherits="Saving.Applications.account.dlg.w_dlg_del" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<%=jsPostSearch%>
<script type="text/javascript">
    function OnDwHeadButtonClicked(sender, row, bName) {
        jsPostSearch();
    }

    function OnDwMainClicked(sender, row, col, value) {
        var asset_doc = objDwMain.GetItem(row, "asset_doc");
        if (confirm("ต้องการเลือกเลขทะเบียนสินทรัพย์ " + asset_doc + " ใช่หรือไม่")) {
            parent.SelectAssetDoc(asset_doc);
            parent.RemoveIFrame();
        }
    }
</script>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <dw:WebDataWindowControl ID="DwHead" runat="server" DataWindowObject="d_acc_del_search_head"
            LibraryList="~/DataWindow/account/asset.pbl" AutoRestoreContext="False" AutoRestoreDataCache="True"
            AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientScriptable="True"
            ClientEventButtonClicked="OnDwHeadButtonClicked">
        </dw:WebDataWindowControl>
        <asp:RadioButton ID="Radio1" runat="server" AutoPostBack="True" OnCheckedChanged="Radio1_CheckChanged"
            Text="รายการสินทรัพย์" Checked="True" Font-Size="Medium" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<%--        <asp:RadioButton ID="Radio2" runat="server" AutoPostBack="True" OnCheckedChanged="Radio2_CheckChanged"
            Text="รายการสินทรัพย์ไม่มีตัวตน" Font-Size="Medium" />--%>
        <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_acc_del_search"
            LibraryList="~/DataWindow/account/asset.pbl" AutoRestoreContext="False" AutoRestoreDataCache="True"
            AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientScriptable="True"
            ClientEventClicked="OnDwMainClicked" RowsPerPage="10">
            <PageNavigationBarSettings NavigatorType="NextPrevWithQuickGo" Visible="True">
            </PageNavigationBarSettings>
        </dw:WebDataWindowControl>
    </div>
    </form>
</body>
</html>
