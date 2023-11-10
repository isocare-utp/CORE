<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_recvchq.aspx.cs"
    Inherits="Saving.Applications.app_finance.dlg.w_dlg_recvchq" ValidateRequest="false" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%=postBankBranch %>
    <title>รายละเอียดเช็ค</title>

    <script type="text/javascript">

        function DwMainItemChange(sender, rowNumber, columnName, newValue) {
            objDwMain.SetItem(rowNumber, columnName, newValue);
            if (columnName == "bank_code") {
                objDwMain.AcceptText();
                postBankBranch();
            }
        }

        function DwMainSummitClick() {
            var account_no = objDwMain.GetItem(1, "account_no");
            var dateon_chq = objDwMain.GetItem(1, "dateon_chq");
            var bank_code = objDwMain.GetItem(1, "bank_code");
            var bank_branch = objDwMain.GetItem(1, "bank_branch");
            if (account_no == null || dateon_chq == null || bank_code == null || bank_branch == null) {
                alert("กรุณากรอกข้อมูลให้ครับถ้วน");
            }
            else {
                parent.SetId(account_no, dateon_chq, bank_code, bank_branch);
                parent.RemoveIFrame();
            }
        }

        function DwMainCancelClick() {
            parent.RemoveIFrame();
            return;
        }
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_slip_recvchq"
            AutoRestoreContext="False" LibraryList="~/DataWindow/app_finance/finslip_spc.pbl"
            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" ClientEventItemChanged="DwMainItemChange"
            AutoRestoreDataCache="True">
        </dw:WebDataWindowControl>
        <table width="100%">
            <tr>
                <td align="center">
                    <asp:Button ID="Button1" runat="server" Text="ตกลง" OnClientClick="DwMainSummitClick()" />
                    <asp:Button ID="Button2" runat="server" Text="ยกเลิก" OnClientClick="DwMainCancelClick()" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="HfAccId" runat="server" />
    </form>
</body>
</html>
