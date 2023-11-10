<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_shrpayment_adjust.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_shrpayment_adjust" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=memNoItemChange%>
    <%=initJavaScript%>
    <%= memNoFromDlg%>
    <%=newClear%>

    <script type="text/javascript">
        function itemChanged(sender, rowNumber, columnName, newValue) {
            if (columnName == "member_no") {
                objdw_data.SetItem(rowNumber, columnName, Gcoop.StringFormat(newValue, "000000"));
                objdw_data.AcceptText();
                Gcoop.GetEl("Hfmember_no").value = objdw_data.GetItem(rowNumber, "member_no");
                memNoItemChange();
            }
            return 0;
        }
        function Validate() {

            objdw_data.AcceptText();

            return confirm("ยืนยันการบันทึกข้อมูล");
        }
        function DwButtonClick(sender, rowNumber, buttonName) {
            if (buttonName == "b_memsearch") {
                Gcoop.OpenDlg("570", "590", "w_dlg_member_search.aspx", "")
            }
            return 0;
        }

        function GetMemDetFromDlg(memberno) {
            objdw_data.SetItem(1, "member_no", memberno);
            objdw_data.AcceptText();
            Gcoop.GetEl("Hfmember_no").value = memberno;
            memNoFromDlg();
        }
        function MenubarNew() {
            if (confirm("ยืนยันการล้างข้อมูลบนหน้าจอ")) {
                newClear();
            }
        }
        function MenubarOpen() {
            Gcoop.OpenDlg('580', '590', 'w_dlg_member_search.aspx', '');
        }
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="Hfmember_no" runat="server" />
    <dw:WebDataWindowControl ID="dw_data" runat="server" DataWindowObject="d_sl_shrpayment_adjust"
        LibraryList="~/DataWindow/shrlon/sl_shrpayment_adjust.pbl" ClientScriptable="True"
        ClientEventItemChanged="itemChanged" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientEventButtonClicked="DwButtonClick">
    </dw:WebDataWindowControl>
</asp:Content>
