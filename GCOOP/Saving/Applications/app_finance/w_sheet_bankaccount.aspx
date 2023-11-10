<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_bankaccount.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_sheet_bankaccount" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postBankBranch%>
    <%=postRetrieve%>

    <script type="text/javascript">

        function Validate() {
            objDwMain.AcceptText();
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function MenubarNew() {
            if (confirm("ยืนยันการลบข้อมูล ??")) {
                window.location = state.SsUrl + "Applications/app_finance/w_sheet_bankaccount.aspx";
            }
        }

        function MenubarOpen() {
            Gcoop.OpenIFrameExtend(700, 250, "w_dlg_bankaccount_search.aspx", "");
        }

        function GetDlgBankAccount(account_no, account_name) {
            objDwMain.SetItem(1, "account_no", account_no);
            objDwMain.AcceptText();
            postRetrieve();
        }

        function DwMainItemChanged(sender, rowNumber, columnName, newValue) {
            objDwMain.SetItem(rowNumber, columnName, newValue);
            objDwMain.AcceptText();
            if (columnName == "bank_code") {
                postBankBranch();
            }
            else if (columnName == "account_no") {
                postRetrieve();
            }
            else {
                postRefrash();
            }
        }
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:Panel ID="Panel1" runat="server" Width="720px" ScrollBars="Auto">
        <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_fin_bankaccount2"
            LibraryList="~/DataWindow/App_finance/bankaccount.pbl" ClientScriptable="True"
            AutoRestoreContext="false" AutoRestoreDataCache="true" AutoSaveDataCacheAfterRetrieve="true"
            ClientEventItemChanged="DwMainItemChanged">
        </dw:WebDataWindowControl>
    </asp:Panel>
    <hr />
    <asp:Panel ID="Panel2" runat="server" Width="720px" ScrollBars="Auto">
        <dw:WebDataWindowControl ID="DwList" runat="server" DataWindowObject="d_banklist_statement"
            LibraryList="~/DataWindow/App_finance/bankaccount.pbl" ClientScriptable="True"
            AutoRestoreContext="false" AutoRestoreDataCache="true" AutoSaveDataCacheAfterRetrieve="true">
        </dw:WebDataWindowControl>
    </asp:Panel>
</asp:Content>
