<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_duplicate_asset.aspx.cs"
    Inherits="Saving.Applications.account.dlg.w_dlg_duplicate_asset" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<%=jsPostSave%>
<%=jsPostDuplicate%>
<script type="text/javascript">
    function OnDwMainButtonClicked(sender, row, bName) {
        if (bName == "b_confirm") {
            var dup_amt = objDwMain.GetItem(1, "dup_amt");
            if (dup_amt == "" || dup_amt == null) {
                alert("กรุณาระบุจำนวนที่ต้องการคัดลอก");
            }
            else {
                jsPostDuplicate();
            }
        }
    }

    function SaveData() {
        if (confirm("ยืนยันการบันทึกข้อมูล")) {
            jsPostSave();
        }
    }

    function OnCloseDialog() {
        if (confirm("ยืนยันการออกจากหน้าจอ ")) {
            parent.RemoveIFrame();
        }
    }

    function DialogLoadComplete() {
        if (Gcoop.GetEl("SaveComplete").value == "true") {
            parent.DuplicateComplete();
            parent.RemoveIFrame();
        }
    }
</script>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table style="width: 100%;">
            <tr>
                <td valign="top">
                    <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_acc_dup_asset_head"
                        LibraryList="~/DataWindow/account/asset.pbl" ClientEventButtonClicked="OnDwMainButtonClicked">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Panel ID="Panel1" runat="server" BorderStyle="Ridge" Height="320px" Width="280px" ScrollBars="None">
                        <dw:WebDataWindowControl ID="DwList" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" ClientFormatting="True"
                            DataWindowObject="d_acc_dup_asset" LibraryList="~/DataWindow/account/asset.pbl"
                            RowsPerPage="10">
                            <PageNavigationBarSettings NavigatorType="NextPrevWithQuickGo" Visible="True">
                            </PageNavigationBarSettings>
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="B_save" type="button" value="บันทึก" onclick="SaveData()" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="B_close" type="button" value="ยกเลิก/ปิดหน้าจอ" onclick="OnCloseDialog()" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="SaveComplete" runat="server" />
    </div>
    </form>
</body>
</html>
