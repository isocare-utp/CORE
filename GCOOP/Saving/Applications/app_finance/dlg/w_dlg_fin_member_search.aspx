<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_fin_member_search.aspx.cs"
    Inherits="Saving.Applications.app_finance.dlg.w_dlg_fin_member_search" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ค้นหาสมาชิก</title>

    <script type="text/javascript">
        function selectRow(sender, rowNumber, objectName) {
            objDwList.AcceptText();
            var memberno = objDwList.GetItem(rowNumber, "member_no");
            window.opener.GetValueFromDlg(memberno);
            window.close();
        }
        // if (objectName != "datawindow") {
             //objDwList.AcceptText();
             //var memberno = objdw_detail.GetItem(rowNumber, "member_no");
             //parent.GetValueFromDlg(memberno);
            //parent.close();
          // }
       // }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="head" runat="server">
            <table>
                <tr>
                    <td>
                        <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_dp_member_search_criteria"
                            LibraryList="~/DataWindow/app_finance/fincomsrv.pbl" ClientEventButtonClicked="DwMainButtunOnClicked"
                            ClientScriptable="True" AutoRestoreContext="False" AutoRestoreDataCache="True"
                            AutoSaveDataCacheAfterRetrieve="True">
                        </dw:WebDataWindowControl>
                    </td>
                    <td>
                        <asp:Button ID="B_Search" runat="server" Text="ค้นหา..." Height="50" Font-Bold="True"
                            Font-Size="Small" OnClick="B_Search_Click" />
                    </td>
                </tr>
            </table>
            <hr />
            <br />
            <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        </asp:Panel>
        <asp:Panel ID="Panel1" runat="server">
            <dw:WebDataWindowControl ID="DwList" runat="server" DataWindowObject="d_dp_member_search_memno_list"
                LibraryList="~/DataWindow/app_finance/fincomsrv.pbl" ClientEventClicked="selectRow"
                ClientScriptable="True" Width="670px" RowsPerPage="15" HorizontalScrollBar="NoneAndClip"
                VerticalScrollBar="NoneAndClip" AutoRestoreContext="False" AutoRestoreDataCache="True"
                AutoSaveDataCacheAfterRetrieve="True">
                <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
                </PageNavigationBarSettings>
            </dw:WebDataWindowControl>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
