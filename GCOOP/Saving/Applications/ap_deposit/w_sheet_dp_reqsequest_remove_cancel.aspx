<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_reqsequest_remove_cancel.aspx.cs" 
Inherits="Saving.Applications.ap_deposit.w_sheet_dp_reqsequest_remove_cancel" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postAccount%>
    <%=postSequestType%>
    <script type="text/javascript">
        function Validate() {
            return confirm("ต้องการบันทึกข้อมูลหรือไม่");
        }

        function NewAccountNo(coopid, accNo) {
            objDwMain.SetItem(1, "deptaccount_no", Gcoop.Trim(accNo));
            objDwMain.SetItem(1, "coop_id", coopid);
            objDwMain.AcceptText();
            postAccount();
        }

        function OnDwMainButtonClicked(sender, rowNumber, buttonName) {
            if (buttonName == "cb_search") {
                Gcoop.OpenIFrame(900, 600, "w_dlg_dp_account_search.aspx", "?coopid=" + objDwMain.GetItem(1, "coop_id"));
            }
        }

        function OnDwMainItemChanged(sender, rowNumber, columnName, newValue) {
            if (columnName == "deptaccount_no") {
                objDwMain.SetItem(rowNumber, columnName, newValue);
                objDwMain.AcceptText();
                postAccount();
            }
            else if (columnName == "sequest_type") {
                objDwMain.SetItem(rowNumber, columnName, newValue);
                objDwMain.AcceptText();
                postSequestType();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_dept_reqsequest_remove_cancel"
        LibraryList="~/DataWindow/ap_deposit/dp_reqsequest.pbl" ClientEventItemChanged="OnDwMainItemChanged"
        ClientEventButtonClicked="OnDwMainButtonClicked" ClientFormatting="True">
    </dw:WebDataWindowControl>

    <br />
    <dw:WebDataWindowControl ID="Dw_detail" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_detail_reqsequest"
        LibraryList="~/DataWindow/ap_deposit/dp_reqsequest.pbl" ClientFormatting="True">
    </dw:WebDataWindowControl>
</asp:Content>
