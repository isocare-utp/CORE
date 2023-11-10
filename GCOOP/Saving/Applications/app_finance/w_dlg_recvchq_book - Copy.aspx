<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_dlg_recvchq_book.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_dlg_recvchq_book" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postInitChqBook%>
    <%=postChangeChqNo%>
    <%=postBankBranch %>

    <script type="text/javascript">

        function Validate() {
            objDwMain.AcceptText();
            return confirm("ยืนยันการบันทึกลงรับเล่มเช็ค");
        }

        function MenubarNew() {
            window.location = state.SsUrl + "Applications/app_finance/w_dlg_recvchq_book.aspx";
        }

        function OnDwMainChanged(sender, rowNumber, columnName, newValue) {
            objDwMain.SetItem(rowNumber, columnName, newValue);
            if (columnName == "start_chqno") {
                Gcoop.GetEl("HfColumnName").value = columnName;
                Gcoop.GetEl("HfNewValue").value = newValue;
                objDwMain.AcceptText();
                postChangeChqNo();
            }
            else if (columnName == "chqslip_amt") {
                objDwMain.SetItem(rowNumber, "chqslip_remain", newValue);
                objDwMain.SetItem(rowNumber, "chqslip_amt", newValue);
                Gcoop.GetEl("HfColumnName").value = columnName;
                Gcoop.GetEl("HfNewValue").value = newValue;
                objDwMain.AcceptText();
                postChangeChqNo();
            }
            else if (columnName == "bank_code") {
                objDwMain.AcceptText();
                postBankBranch();
            }
            else if (columnName == "bank_branch") {
                objDwMain.AcceptText();
                postInitChqBook();
            }
        }

        function DwMainClicked(sender, row, oName) {
            if (oName == "b_bankbranch") {
                Gcoop.OpenDlg(700, 550, "w_dlg_bank_and_branch.aspx", "");
            }
        }

        function GetDlgBankAndBranch(bankCode, bankDesc, branchCode, branchDesc) {
            objDwMain.SetItem(1, "bank_code", bankCode);
            objDwMain.SetItem(1, "bank_branch", branchCode);
            objDwMain.AcceptText();
            postInitChqBook();
        }
    
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table width="100%">
        <tr align="center">
            <td align="center">
                <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_recv_chequebook"
                    LibraryList="~/DataWindow/App_finance/recvchq_book.pbl" ClientScriptable="True"
                    ClientEventClicked="selectRow" ClientEventItemChanged="OnDwMainChanged" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventButtonClicked="DwMainClicked">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HfFormat" runat="server" />
    <asp:HiddenField ID="HfColumnName" runat="server" />
    <asp:HiddenField ID="HfNewValue" runat="server" />
</asp:Content>
