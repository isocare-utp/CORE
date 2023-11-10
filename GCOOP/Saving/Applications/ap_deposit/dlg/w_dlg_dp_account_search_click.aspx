<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_dp_account_search_click.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.dlg.w_dlg_dp_account_search_click" ValidateRequest="false" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ค้นหาเลขบัญชีเงินฝาก</title>
    <%=postMemberNo%>
    <script type="text/javascript">
        function OnDwMainItemChanged(s, r, c, v) {
            alert(c + ":" + v);
        }

        function OnItemChanged(s, r, c, v) {
            if (c == "member_no") {
                s.SetItem(r, c, v);
                s.AcceptText();
                postMemberNo();
            }
        }

        function OnDetailClick(s, row, n) {
            var dept_no = objdw_detail.GetItem(row, "deptaccount_no");
            var coopid = Gcoop.GetEl("HdCoopid").value;
            var acc = objdw_detail.GetItem(row, "deptaccount_name");
            window.opener.NewAccountNo(coopid, dept_no);
            window.close();
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
                <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
                    <dw:WebDataWindowControl ID="DwMain" runat="server" ClientScriptable="True" DataWindowObject="d_dp_depmaster_criteria_click"
                        LibraryList="~/DataWindow/ap_deposit/dp_slip.pbl" Width="530px" HorizontalScrollBar="NoneAndClip"
                        VerticalScrollBar="NoneAndClip" UseCurrentCulture="True" AutoRestoreContext="False"
                        AutoSaveDataCacheAfterRetrieve="True" 
                        ClientEventItemChanged="OnItemChanged">
                    </dw:WebDataWindowControl>
                </td>
                <td valign="top">
                    <asp:Button ID="BSearch" runat="server" Text="ค้น..." OnClick="BSearch_Click" Height="75px" />
                </td>
            </tr>
        </table>
        <br />
        <dw:WebDataWindowControl ID="dw_detail" runat="server" DataWindowObject="d_dp_account_list_click"
            LibraryList="~/DataWindow/ap_deposit/dp_slip.pbl" Width="600px" Style="top: 0px;
            left: 0px" RowsPerPage="10" HorizontalScrollBar="NoneAndClip" VerticalScrollBar="NoneAndClip"
            ClientEventClicked="OnDetailClick" ClientScriptable="True" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True">
            <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
            </PageNavigationBarSettings>
        </dw:WebDataWindowControl>
        <asp:HiddenField ID="HSqlTemp" Value="0" runat="server" />
        <asp:HiddenField ID="HdCoopid" Value="0" runat="server" />
        <br />
    </div>
    </form>
</body>
</html>