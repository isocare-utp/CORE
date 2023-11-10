<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_mb_req_chggroup.aspx.cs"
    Inherits="Saving.Applications.shrlon.w_sheet_mb_req_chggroup" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsPostMember%>
    <%=jsPostGroup%>
    <%=initJavaScript%>
    <%=newClear%>

    <script type="text/javascript">
        function Validate() {
            objdw_main.AcceptText();
            objdw_history.AcceptText();
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
        function MenubarNew() {
            if (confirm("ยืนยันการล้างข้อมูลบนหน้าจอ")) {
                newClear();
            }
        }
        function MenubarOpen() {
            Gcoop.OpenDlg('580', '590', 'w_dlg_sl_member_search.aspx', '');
        }

        function Click_search(s, r, c) {
            if (c == "b_search") {
                Gcoop.OpenDlg('580', '590', 'w_dlg_sl_member_search.aspx', '');
            }
        }
        function GetValueFromDlg(memberno) {
            objdw_main.SetItem(1, "member_no", memberno);
            objdw_main.AcceptText();
            Gcoop.GetEl("Hfmember_no").value = memberno;
            jsPostMember();
        }
        function itemChanged(sender, rowNumber, columnName, newValue) {
            if (columnName == "member_no") {
                objdw_main.SetItem(rowNumber, columnName, Gcoop.StringFormat(newValue, "000000"));
                objdw_main.AcceptText();
                Gcoop.GetEl("Hfmember_no").value = objdw_main.GetItem(rowNumber, "member_no");
                jsPostMember();
            }
            return 0;
        }
        function checkMain(s, r, c) {
            Gcoop.CheckDw(s, r, c, "apvflag", 1, 0);

        }
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="Hfmember_no" runat="server" />
    <asp:TextBox ID="TextDwmain" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="Textdwhistory" runat="server" Visible="False"></asp:TextBox>
    <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_mb_reqchggroup"
        LibraryList="~/DataWindow/Shrlon/mb_req_chggroup.pbl" ClientScriptable="True"
        ClientEventItemChanged="itemChanged" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientEventButtonClicked="Click_search"
        ClientEventClicked="checkMain">
    </dw:WebDataWindowControl>
    <dw:WebDataWindowControl ID="dw_history" runat="server" DataWindowObject="d_mb_chggroup_history"
        LibraryList="~/DataWindow/Shrlon/mb_req_chggroup.pbl" ClientScriptable="True"
        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        ClientFormatting="True">
    </dw:WebDataWindowControl>
</asp:Content>
