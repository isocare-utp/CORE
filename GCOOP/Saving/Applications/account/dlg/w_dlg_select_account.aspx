<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_select_account.aspx.cs"
    Inherits="Saving.Applications.account.dlg.d_dlg_select_account" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<%=jsPostSearch%>
<%=jsPostDelete%>
<script type="text/javascript">
    function SaveData() {
        if (confirm("ยืนยันการเลือกบัญชี")) {
            var rowcount = objDwselect.RowCount();
            var acc_id = "";
            for (var i = 1; i <= rowcount; i++) {
                if (i > 1) {
                    acc_id += ",";
                }
                acc_id += objDwselect.GetItem(i, "account_id");
            }
            parent.GetAccount(acc_id);
            parent.RemoveIFrame();
        }
    }

    //ฟังก์ชันในการปิด dialog
    function OnCloseDialog() {
        if (confirm("ยืนยันการออกจากหน้าจอ ")) {
            parent.RemoveIFrame();
        }
    }

    function OnClickSearchButton(sender, row, bName) {
        jsPostSearch();
    }

    function OnDeleteClickButton(sender, row, bName) {
        Gcoop.GetEl("Hdrow").value = row + "";
        jsPostDelete();
    }
</script>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table style="width: 100%;">
            <tr>
                <td valign="top">
                </td>
                <td valign="top">
                </td>
                <td valign="top">
                </td>
                <td>
                    <dw:WebDataWindowControl ID="Dwsearch" runat="server" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        DataWindowObject="d_dlg_acc_search" LibraryList="~/DataWindow/account/budget.pbl"
                        ClientEventButtonClicked="OnClickSearchButton">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
            <tr>
                <td valign="top" colspan="3">
                    <asp:Panel ID="Panel1" runat="server" BorderStyle="Ridge" Height="430px" Width="454px">
                        <dw:WebDataWindowControl ID="Dwselect" runat="server" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                            DataWindowObject="d_dlg_acc_select" LibraryList="~/DataWindow/account/budget.pbl"
                             ClientEventButtonClicked="OnDeleteClickButton"  Height="430px" Width="454px">
                            <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
                            </PageNavigationBarSettings>
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
                <td valign="top" class="style3">
                    <asp:Panel ID="Panel2" runat="server" BorderStyle="Ridge" Height="430px" Width="454px"
                        ScrollBars="Auto">
                        <dw:WebDataWindowControl ID="Dwlist" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                            AutoSaveDataCacheAfterRetrieve="True" ClientEventClicked="OnDwmasterClick" ClientFormatting="True"
                            ClientScriptable="True" DataWindowObject="d_dlg_acc_list" LibraryList="~/DataWindow/account/budget.pbl"
                            RowsPerPage="15">
                            <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
                            </PageNavigationBarSettings>
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td valign="top">
                </td>
                <td valign="top">
                </td>
                <td valign="top">
                </td>
                <td valign="top" class="style3">
                    &nbsp;<asp:Button ID="B_select" runat="server" Text="&lt; &lt;" UseSubmitBehavior="False"
                        Width="60px" OnClick="B_select_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="B_save" type="button" value="ตกลง" onclick="SaveData()" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="B_close" type="button" value="ยกเลิก/ปิดหน้าจอ" onclick="OnCloseDialog()" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="Hdrow" runat="server" />
    </div>
    </form>
</body>
</html>
