<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_bank_and_branch.aspx.cs"
    Inherits="Saving.Applications.divavg.dlg.w_dlg_bank_and_branch" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ธนาคารและสาขา</title>
    <%=postSelectBank%>
    <script type="text/javascript">
        function OnDwBankClick(s, r, c) {
            Gcoop.GetEl("HdBankRow").value = r;
            postSelectBank();
        }

        function OnDwBranchClick(s, r, c) {
            var bankRow = Gcoop.GetEl("HdBankRow").value;
            var sheetRow = Gcoop.GetEl("HdSheetRow").value;
            var bankCode = objDwBank.GetItem(bankRow, "bank_code");
            var bankDesc = objDwBank.GetItem(bankRow, "bank_desc");
            var branchCode = objDwBranch.GetItem(r, "branch_id");
            var branchDesc = objDwBranch.GetItem(r, "branch_name");
            var expense_bank_type = objDw_main.GetItem(1, "expense_bank_type");
            var expense_accid = objDw_main.GetItem(1, "expense_accid");
            var moneytype_code = Gcoop.GetEl("Hdmoneytype").value;
            //กรณีเป็นโอนธนาคาร
            if (moneytype_code == "CBT") {
                if (bankCode == "" || bankCode == null) {
                    alert("กรุณาเลือกธนาคาร");
                }
                else if (branchCode == "" || branchCode == null) {
                    alert("กรุณาเลือกสาขาธนาคาร");
                }
                else if (expense_bank_type == "" || expense_bank_type == null) {
                    alert("กรุณาเลือกประเภทบัญชี");
                }
                else if (expense_accid == "" || expense_accid == null) {

                    alert("กรุณากรอกเลขที่บัญชี");
                }
                else {
                    parent.GetDlgBankAndBranch(bankCode, bankDesc, branchCode, branchDesc, expense_bank_type, expense_accid);
                    parent.RemoveIFrame();
                }
            }
            else {
                if (bankCode == "" || bankCode == null) {
                    alert("กรุณาเลือกธนาคาร");
                }
                else if (branchCode == "" || branchCode == null) {
                    alert("กรุณาเลือกสาขาธนาคาร");
                }
                else if (expense_bank_type == "" || expense_bank_type == null) {
                    alert("กรุณาเลือกประเภทบัญชี");
                }
                else {
                    parent.GetDlgBankAndBranch(bankCode, bankDesc, branchCode, branchDesc, expense_bank_type, expense_accid);
                    parent.RemoveIFrame();
                }
            }
            
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
        <div style="background-color: #FFFFFF; border: 1px solid #CCCCCC; width: 100%; height: 25px;
            text-align: center; vertical-align: middle;">
            <dw:WebDataWindowControl ID="Dw_main" runat="server" DataWindowObject="d_divsrv_bankaccid"
                LibraryList="~/DataWindow/divavg/divsrv_search_bank.pbl" AutoRestoreContext="False"
                AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                ClientScriptable="True" ClientFormatting="True" 
                style="top: 0px; left: 0px">
            </dw:WebDataWindowControl>
        </div>
        <table>
            <tr>
                <td valign="top">
                    <dw:WebDataWindowControl ID="DwBank" runat="server" DataWindowObject="d_cm_ucfbank"
                        LibraryList="~/DataWindow/divavg/divsrv_search_bank.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        ClientEventClicked="OnDwBankClick" RowsPerPage="20">
                        <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
                            <BarStyle Font-Bold="True" Font-Names="Tahoma" Font-Size="12px" />
                            <QuickGoNavigator GoToDescription="หน้า:" />
                        </PageNavigationBarSettings>
                    </dw:WebDataWindowControl>
                </td>
                <td valign="top">
                    <asp:Label ID="Label2" runat="server" Text="ชื่อสาขา :" Font-Size="Medium"></asp:Label>
                    &nbsp;<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" Text="ค้นหา" OnClick="Button1_Click" />
                    <dw:WebDataWindowControl ID="DwBranch" runat="server" DataWindowObject="d_cm_ucfbankbranch"
                        LibraryList="~/DataWindow/divavg/divsrv_search_bank.pbl" AutoRestoreContext="False"
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
    <asp:HiddenField ID="Hdmoneytype" runat="server" Value="" />
    </form>
</body>
</html>
