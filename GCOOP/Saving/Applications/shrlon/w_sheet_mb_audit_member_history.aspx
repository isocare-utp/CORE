<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_mb_audit_member_history.aspx.cs"
    Inherits="Saving.Applications.shrlon.w_sheet_mb_audit_member_history" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=jsGetData%>
    <%=jsGetDataList%>
    <%=jsGetDataselect %>

    <script type="text/javascript">
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
        function Clickb_search(s, r, c) {
            if (c == "b_search") {
                var ls_memno = objdw_search.GetItem(1, "member_no");
                var ls_apvid = objdw_search.GetItem(1, "apvid");
                var start_tdate = objdw_search.GetItem(1, "start_tdate");
                var end_tdate = objdw_search.GetItem(1, "end_tdate");
                if (ls_memno == "" && ls_apvid == "" && start_tdate == "" && end_tdate=="") {
                    jsGetData();
                } jsGetDataselect();
            }
        }

        function selectRow(s, r, c) {
            var audit_no = objdw_list.GetItem(r, "audit_no");
            Gcoop.GetEl("Hlist").value = audit_no;
            jsGetDataList();
        }
       
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="Hlist" runat="server" />
    <asp:HiddenField ID="hidden_search" runat="server" />
    <asp:Panel ID="Panel1" runat="server">
        <dw:WebDataWindowControl ID="dw_search" runat="server" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
            DataWindowObject="d_mb_audit_member_search_criteria" LibraryList="~/DataWindow/shrlon/mb_audit_member.pbl"
            ClientEventButtonClicked="Clickb_search">
        </dw:WebDataWindowControl>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server">
        <dw:WebDataWindowControl ID="dw_list" runat="server" DataWindowObject="d_mb_audit_member_list"
            LibraryList="~/DataWindow/shrlon/mb_audit_member.pbl" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
            ClientEventClicked="selectRow" ClientFormatting="True">
        </dw:WebDataWindowControl>
    </asp:Panel>
    <asp:Panel ID="Panel3" runat="server">
        <dw:WebDataWindowControl ID="dw_detail" runat="server" DataWindowObject="d_mb_audit_member_detail"
            LibraryList="~/DataWindow/shrlon/mb_audit_member.pbl" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True">
        </dw:WebDataWindowControl>
    </asp:Panel>
</asp:Content>
