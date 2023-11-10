<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_dp_bsummery.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_bsummery" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=PostRetrive%>
    <%=jsPostBlank%>
    <script type="text/javascript">

        function OnDwMainItemChanged(sender, row, col, val) {
            if (col == "adtm_tdate") {
                sender.SetItem(row, col, val);
                sender.AcceptText();
                jsPostBlank();
            }
        }

        function OnDwMainButtonClick(sender, row, bName) {
            if (bName == "b_1") {
                PostRetrive();
            }
            return 0;
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <dw:WebDataWindowControl ID="Dw_Main" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_dept_sum_deptwith"
        LibraryList="~/DataWindow/ap_deposit/deposit.pbl" ClientEventButtonClicked="OnDwMainButtonClick"
        ClientEventItemChanged="OnDwMainItemChanged" ClientFormatting="True">
    </dw:WebDataWindowControl>
</asp:Content>
