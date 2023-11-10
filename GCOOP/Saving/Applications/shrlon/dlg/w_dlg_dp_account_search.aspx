<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_dp_account_search.aspx.cs"
    Inherits="Saving.Applications.shrlon.dlg.w_dlg_dp_account_search" ValidateRequest="false" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ค้นหาเลขบัญชีเงินฝาก</title>

    <script type="text/javascript">
    function OnDwMainItemChanged(sender, row, columnName, newValue){
        alert(columnName + ":" + newValue);
    }
    function selectRow(sender, rowNumber, objectName) {
    
        var dept_no = objdw_detail.GetItem(rowNumber, "deptaccount_no");
        var deptaccount_name = objdw_detail.GetItem(rowNumber, "deptaccount_name");
        var prncbal = Gcoop.ParseFloat(objdw_detail.GetItem(rowNumber, "prncbal"));
        try {
            window.opener.NewAccountNo(dept_no, deptaccount_name, prncbal);
            window.close();
        } catch (err) {
            parent.NewAccountNo(dept_no, deptaccount_name, prncbal);
            parent.RemoveIFrame();
        }
    
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <table>
            <tr>
                <td valign="top" width="550">
                    <dw:WebDataWindowControl ID="DwMain" runat="server" ClientScriptable="True" DataWindowObject="d_dp_depmaster_criteria1"
                        LibraryList="~/DataWindow/ap_deposit/dp_slip.pbl" Width="530px" HorizontalScrollBar="NoneAndClip"
                        VerticalScrollBar="NoneAndClip" UseCurrentCulture="True" AutoRestoreContext="False"
                        AutoSaveDataCacheAfterRetrieve="True">
                    </dw:WebDataWindowControl>
                </td>
                <td valign="top">
                    <asp:Button ID="BSearch" runat="server" Text="ค้น..." OnClick="BSearch_Click" Height="75px" />
                </td>
            </tr>
        </table>
        <br />
        <dw:WebDataWindowControl ID="dw_detail" runat="server" DataWindowObject="d_dp_account_list"
            LibraryList="~/DataWindow/ap_deposit/dp_slip.pbl" Width="600px" Style="top: 0px;
            left: 0px" RowsPerPage="10" HorizontalScrollBar="NoneAndClip" VerticalScrollBar="NoneAndClip"
            ClientEventClicked="selectRow" ClientScriptable="True" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True">
            <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
            </PageNavigationBarSettings>
        </dw:WebDataWindowControl>
        <asp:HiddenField ID="HSqlTemp" Value="0" runat="server" />
        <br />
    </div>
    </form>
</body>
</html>