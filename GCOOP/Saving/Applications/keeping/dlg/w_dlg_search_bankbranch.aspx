<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_search_bankbranch.aspx.cs" Inherits="Saving.Applications.keeping.dlg.w_dlg_search_bankbranch" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ค้นหาสาขาธนาคาร</title>
    <script type="text/javascript">
        function selectRow(sender, rowNumber, objectName) {
            if (objectName != "datawindow") {
                var branch_id = objdw_detail.GetItem(rowNumber, "branch_id");
                var branch_name = objdw_detail.GetItem(rowNumber, "branch_name");
                try {
                    window.opener.GetBankBranchFromDlg(branch_id, branch_name);
                    window.close();
                } catch (err) {
                    parent.GetBankBranchFromDlg(branch_id, branch_name);
                    parent.RemoveIFrame();
                }
            }
        }
        function IFrameClose() {
            parent.RemoveIFrame();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    รายการค้นหา<span style="float:right; cursor:pointer;color:Red;" onclick="IFrameClose();">[X]</span>
    <table style="width: 530px;">
        <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_data" runat="server" ClientScriptable="True" DataWindowObject="d_search_bank"
                    LibraryList="~/DataWindow/keeping/mb_chgdetail.pbl">
                </dw:WebDataWindowControl>
            </td>
            <td>
                <asp:Button ID="cb_find" runat="server" Text="ค้นหา" Height="80px" Width="55px" OnClick="cb_find_Click"  style="width:55px; height:80px;" />
            </td>
        </tr>
    </table>
    รายละเอียด
    <asp:HiddenField ID="hidden_search" runat="server" />
        <table style="width: 530px;" >
        <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_detail" runat="server" ClientEventClicked="selectRow"
                    DataWindowObject="d_cmucfbankbranch" LibraryList="~/DataWindow/keeping/mb_chgdetail.pbl"
                    ClientScriptable="True"  Height="450px" Width="450px"  >
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
