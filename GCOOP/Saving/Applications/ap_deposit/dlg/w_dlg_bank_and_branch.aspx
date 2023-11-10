<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_bank_and_branch.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.dlg.w_dlg_bank_and_branch" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ธนาคารและสาขา</title>
    <%=postSelectBank%>

    <script type="text/javascript">
    function OnDwBankClick(s, r, c){
        Gcoop.GetEl("HdBankRow").value = r + "";
        postSelectBank();
    }
    
    function OnDwBranchClick(s, r, c){
        var bankRow = Gcoop.GetEl("HdBankRow").value;
        var sheetRow = Gcoop.GetEl("HdSheetRow").value;
        var bankCode = objDwBank.GetItem(bankRow, "bank_code");
        var bankDesc = objDwBank.GetItem(bankRow, "bank_desc");
        var branchCode = objDwBranch.GetItem(r, "branch_id");
        var branchDesc = objDwBranch.GetItem(r, "branch_name");
        opener.GetDlgBankAndBranch(sheetRow, bankCode, bankDesc, branchCode, branchDesc);
        window.close();
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="background-color: #DDDDDD; border: 1px solid #CCCCCC; width: 100%; height: 25px;
            text-align: center; vertical-align: middle;">
            <asp:Label ID="Label1" runat="server" Text="กรุณาเลือกธนาคาร"></asp:Label>
        </div>
        <table>
            <tr>
                <td valign="top">
                    <dw:WebDataWindowControl ID="DwBank" runat="server" DataWindowObject="d_cm_ucfbank"
                        LibraryList="~/DataWindow/ap_deposit/dp_slip.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        ClientEventClicked="OnDwBankClick" RowsPerPage="20">
                        <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
                            <BarStyle Font-Bold="True" Font-Names="Tahoma" Font-Size="12px" />
                            <QuickGoNavigator GoToDescription="หน้า:" />
                        </PageNavigationBarSettings>
                    </dw:WebDataWindowControl>
                </td>
                <td valign="top">
                    <dw:WebDataWindowControl ID="DwBranch" runat="server" DataWindowObject="d_cm_ucfbankbranch"
                        LibraryList="~/DataWindow/ap_deposit/dp_slip.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        ClientEventClicked="OnDwBranchClick" RowsPerPage="20">
                        <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
                            <BarStyle Font-Bold="True" Font-Names="Tahoma" Font-Size="12px" />
                            <QuickGoNavigator GoToDescription="หน้า:" />
                        </PageNavigationBarSettings>
                    </dw:WebDataWindowControl>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="HdBankRow" runat="server" Value="" />
    <asp:HiddenField ID="HdSheetRow" runat="server" Value="" />
    </form>
</body>
</html>
