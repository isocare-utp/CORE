<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_bank_and_branch.aspx.cs"
    Inherits="Saving.Applications.app_finance.dlg.w_dlg_bank_and_branch" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ธนาคารและสาขา</title>
    <%=postSelectBank%>

    <script type="text/javascript">
        function OnDwBankClick(s, r, c) {
            document.getElementById("HdBankRow").value = r + "";
            postSelectBank();
        }

        function OnDwBranchClick(sender, row, collumn) {
            objDwBank.AcceptText();
            objDwBranch.AcceptText();
            var bankRow = document.getElementById("HdBankRow").value;
            var bankCode = objDwBank.GetItem(bankRow, "bank_code");
            var bankDesc = objDwBank.GetItem(bankRow, "bank_desc");
            var branchCode = objDwBranch.GetItem(row, "branch_id");
            var branchDesc = objDwBranch.GetItem(row, "branch_name");
            parent.GetDlgBankAndBranch(bankCode, bankDesc, branchCode, branchDesc);
            parent.close();
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
                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                        <dw:WebDataWindowControl ID="DwBank" runat="server" DataWindowObject="d_cm_ucfbank"
                            LibraryList="~/DataWindow/ap_deposit/dp_slip.pbl" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                            Height="500px" Width="430px" ClientEventClicked="OnDwBankClick">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
                <td valign="top">
                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto">
                        <dw:WebDataWindowControl ID="DwBranch" runat="server" DataWindowObject="d_cm_ucfbankbranch"
                            LibraryList="~/DataWindow/ap_deposit/dp_slip.pbl" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                            Height="500px" Width="260px" ClientEventClicked="OnDwBranchClick">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="HdBankRow" runat="server" Value="" />
    <asp:HiddenField ID="HdSheetRow" runat="server" Value="" />
    </form>
</body>
</html>
