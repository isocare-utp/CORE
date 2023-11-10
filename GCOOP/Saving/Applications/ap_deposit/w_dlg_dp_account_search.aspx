<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_dp_account_search.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.w_dlg_dp_account_search" Culture="th-TH" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ค้นหาบัญชีเงินฝาก : GCOOP - Isocare</title>

    <script type="text/javascript">
    function OnDetailClick(s, row, n){
        var dept_no = objdw_detail.GetItem(row, "deptaccount_no");
        var acc = objdw_detail.GetItem(row, "deptaccount_name");
        var isConfirm = confirm("ต้องการเลือกบัญชี " + dept_no + ": " + acc + "  ใช่หรือไม่");
        if(isConfirm){
            window.opener.NewAccountNo(dept_no);
            window.close();
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
                    <dw:WebDataWindowControl ID="dw_data" runat="server" ClientScriptable="True" DataWindowObject="d_dp_depmaster_criteria1"
                        LibraryList="~/DataWindow/ap_deposit.pbl" Width="530px" HorizontalScrollBar="NoneAndClip"
                        VerticalScrollBar="NoneAndClip" UseCurrentCulture="True">
                    </dw:WebDataWindowControl>
                </td>
                <td valign="top">
                    <asp:Button ID="BSearch" runat="server" Text="ค้น..." Height="75px" UseSubmitBehavior="False" />
                </td>
            </tr>
        </table>
        <br />
        <dw:WebDataWindowControl ID="dw_detail" runat="server" DataWindowObject="d_dp_account_list"
            LibraryList="~/DataWindow/ap_deposit.pbl" Width="600px" Style="top: 0px; left: 0px"
            RowsPerPage="10" HorizontalScrollBar="NoneAndClip" VerticalScrollBar="NoneAndClip"
            ClientEventClicked="OnDetailClick" ClientScriptable="True">
            <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
            </PageNavigationBarSettings>
        </dw:WebDataWindowControl>
        <br />
    </div>
    <asp:HiddenField ID="HSqlTemp" runat="server" Value="0" Visible="False" />
    </form>
</body>
</html>
