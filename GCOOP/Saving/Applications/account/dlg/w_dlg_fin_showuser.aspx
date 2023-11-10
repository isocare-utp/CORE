<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_fin_showuser.aspx.cs"
    Inherits="Saving.Applications.app_finance.dlg.w_dlg_fin_showuser" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>เลือก User </title>

    <script type="text/javascript">

        function DwMainOnSelected(sender, rowNumber, objectName) {
            var username = objdw_main.GetItem(rowNumber, "user_name");
            var full_name = objdw_main.GetItem(rowNumber, "full_name");
            parent.SelectUser(username, full_name);
            parent.RemoveIFrame();
        }

        function DwMainClick(sender, rowNumber, objectName) {
            for (var i = 1; i <= sender.RowCount(); i++) {
                sender.SelectRow(i, false);
            }
            sender.SelectRow(rowNumber, true);
            sender.SetRow(rowNumber);
            Gcoop.GetEl("HdDetailRow").value = rowNumber + "";
        }

        function DwMainSummitClick() {
            var rowNumber = Gcoop.GetEl("HdDetailRow").value;
            if (rowNumber == "") {
                alert("ยังไม่ได้เลือกผู้ใช้");
            }
            else {
                var username = objdw_main.GetItem(rowNumber, "user_name");
                var full_name = objdw_main.GetItem(rowNumber, "full_name");
                parent.SelectUser(username, full_name);
                parent.RemoveIFrame();
            }
        }

        function DwMainCancelClick() {
            parent.RemoveIFrame();
            return;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 350px">
        <asp:Panel ID="Panel1" runat="server" Width="470px" Height="420px" ScrollBars="Auto">
            <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_fn_showuser"
                LibraryList="~/DataWindow/app_finance/finquery.pbl" AutoRestoreContext="False"
                AutoRestoreDataCache="True" ClientFormatting="False" ClientEventClicked="DwMainClick"
                ClientScriptable="True" AutoSaveDataCacheAfterRetrieve="True" 
                Height="400px">
            </dw:WebDataWindowControl>
        </asp:Panel>

    </div>
    <table width="100%" border="0">
        <tr>
            <td align="center">
                <asp:Button ID="Button1" runat="server" Text="ตกลง" OnClientClick="DwMainSummitClick()" />
                <asp:Button ID="Button2" runat="server" Text="ยกเลิก" OnClientClick="DwMainCancelClick()" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdDetailRow" runat="server" />
    </form>
</body>
</html>
