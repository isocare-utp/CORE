<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_lnreqloan_show_etcstatus.aspx.cs"
    Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_lnreqloan_show_etcstatus" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>เลขที่บัญชีสหกรณ์</title>
    <script type="text/javascript">


        function DialogLoadComplete() {
            var chkStatus = Gcoop.GetEl("HfChkStatus").value;
            if (chkStatus == "1") {

                window.close();
            }
        }
        function OnCloseClick() {

            closeWebDialog();
        }
    </script>
</head>
<body>
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_detail" runat="server" DataWindowObject="d_sl_loanrequest_memstatus_wa"
                    LibraryList="~/DataWindow/Shrlon/sl_loan_requestment_cen.pbl" Width="600px" Style="top: 0px;
                    left: 0px" RowsPerPage="10" HorizontalScrollBar="NoneAndClip" VerticalScrollBar="NoneAndClip"
                    ClientEventClicked="selectRow" ClientScriptable="True" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True">
                    <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
                    </PageNavigationBarSettings>
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="HfChkStatus" runat="server" />
                <input id="btnClose" type="button" value="ปิด" onclick="OnCloseClick()" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HfAccID" runat="server" />
    <asp:HiddenField ID="HfRow" runat="server" />
    </form>
</body>
</html>
