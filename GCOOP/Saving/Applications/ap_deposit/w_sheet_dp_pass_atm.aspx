<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_pass_atm.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.w_sheet_dp_pass_atm" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=jsSearch%>
    <%=jsTransactionCancel%>
    <script type="text/javascript">

        function Validate() {
            return confirm("ต้องการยืนยัน ผ่านรายการหรือไม่?");
        }

        function DwMainItemChanged(s, r, c, v) {
            s.SetItem(r, c, v);
            s.AcceptText();
        }

        function DwMainButtonClicked(s, r, c) {
            if (c == "b_search") {
                jsSearch();
            }
        }
        function DwDetailButtonClicked(s, r, c) {
            if (c == "b_cancel") {
                Gcoop.GetEl("HdRowPass").value = r;
                jsTransactionCancel();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_pass_header"
        LibraryList="~/DataWindow/ap_deposit/dp_pass_atm.pbl" AutoSaveDataCacheAfterRetrieve="True"
        AutoRestoreDataCache="True" AutoRestoreContext="False" ClientScriptable="True"
        ClientEventItemChanged="DwMainItemChanged" ClientEventButtonClicked="DwMainButtonClicked">
    </dw:WebDataWindowControl>
    <dw:WebDataWindowControl ID="DwDetail" runat="server" DataWindowObject="d_pass_detail"
        LibraryList="~/DataWindow/ap_deposit/dp_pass_atm.pbl" AutoSaveDataCacheAfterRetrieve="True"
        AutoRestoreDataCache="True" AutoRestoreContext="False" ClientScriptable="True"
        ClientEventItemChanged="DwDetailItemChanged" ClientEventButtonClicked="DwDetailButtonClicked">
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="HdRowPass" runat="server" Value="" />
    <asp:HiddenField ID="HdOperate_Date" runat="server" Value="" />
</asp:Content>
