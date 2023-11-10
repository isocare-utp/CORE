<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_recvpay_search.aspx.cs"
    Inherits="Saving.Applications.app_finance.dlg.w_dlg_recvpay_search" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <%=jsSearch%>
    <script type="text/javascript">

        function DwMainClick(sender, rowNumber, objectName) {
          
            Gcoop.GetEl("HdDetailRow").value = rowNumber + "";
            for (var i = 1; i <= sender.RowCount(); i++) {
                sender.SelectRow(i, false);
            }
            sender.SelectRow(rowNumber, true);
            sender.SetRow(rowNumber);
            return;
        }
        function DwMainSummitClick() {
            var rowNumber = Gcoop.GetEl("HdDetailRow").value;
            if (rowNumber == "") {
                alert("ยังไม่ได้เลือกรายการ");
            }
            else {
                var slipitemtype_code = objDwMain.GetItem(rowNumber, "slipitemtype_code");
                var item_desc = objDwMain.GetItem(rowNumber, "item_desc");
                parent.GetValueFromDlgItemType(slipitemtype_code, item_desc);
                parent.RemoveIFrame();
            }
        }

        function DwMainCancelClick() {
            parent.RemoveIFrame();
            return;
        }

        function DwSearch_bt_Click(sender, rowNumber, objectName) {
            if (objectName == "b_search") {
                objDwSearch.SetItem(sender, rowNumber, objectName);
                objDwSearch.AcceptText();
                jsSearch();

            }
        }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:Panel runat="server" ScrollBars="Vertical" Height="340px">
        <div>
            <dw:WebDataWindowControl ID="DwSearch" runat="server" DataWindowObject="d_fin_itemtype"
                AutoRestoreContext="False" LibraryList="~/DataWindow/app_finance/finslip_spc.pbl"
                AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" AutoRestoreDataCache="True"
                ClientEventButtonClicked="DwSearch_bt_Click">
            </dw:WebDataWindowControl>
            <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="dddw_fin_itemtype"
                AutoRestoreContext="False" LibraryList="~/DataWindow/app_finance/finslip_spc.pbl"
                AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" AutoRestoreDataCache="True"
                ClientEventClicked="DwMainClick">
            </dw:WebDataWindowControl>
        </div>
        <asp:HiddenField ID="HdDetailRow" runat="server" />
    </asp:Panel>
    <table width="100%" border="0">
        <tr>
            <td align="center">
                <asp:Button ID="Button1" runat="server" Text="ตกลง" OnClientClick="DwMainSummitClick()" />
                <asp:Button ID="Button2" runat="server" Text="ยกเลิก" OnClientClick="DwMainCancelClick()" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
