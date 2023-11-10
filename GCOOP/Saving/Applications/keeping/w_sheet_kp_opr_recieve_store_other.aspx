<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_kp_opr_recieve_store_other.aspx.cs"
    Inherits="Saving.Applications.keeping.w_sheet_kp_opr_recieve_store_other" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsPostMember%>
    <%=initJavaScript%>
    <%=newClear%>
    <%=postInsertDwDetail%>
    <script type="text/javascript">
        function Validate() {
            objdw_main.AcceptText();
            objdw_detail.AcceptText();
            return true; // confirm("ยืนยันการบันทึกข้อมูล");
        }
        function MenubarNew() {
            Gcoop.SetLastFocus("member_no_0");
            Gcoop.Focus();
            newclear();
        }
        function MenubarOpen() {
            Gcoop.OpenDlg('580', '590', 'w_dlg_kp_member_search.aspx', '');
        }

        function Click_search(s, r, c) {
            if (c == "b_search") {
                Gcoop.OpenDlg('580', '590', 'w_dlg_kp_member_search.aspx', '');
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
                objdw_main.SetItem(rowNumber, columnName, Gcoop.StringFormat(newValue, "00000000"));
                objdw_main.AcceptText();
                Gcoop.GetEl("Hfmember_no").value = objdw_main.GetItem(rowNumber, "member_no");
                jsPostMember();
            }

            return 0;

        }


        function OnDwdetailButtonClicked(sender, rowNumber, columnName) {


            if (columnName == "btn_del") {

                if (confirm("คุณต้องการลบรายการแถว " + rowNumber + " ใช่หรือไม่?")) {
                    objdw_detail.DeleteRow(rowNumber);
                    //alert("อย่าลืมบันทึกรายการ");
                }

            }
            if (columnName == "btn_add") {
                objdw_detail.InsertRow(0);
             //   Gcoop.GetEl("HdDetailRow").value = (objdw_detail.RowCount() + 1) + ""; // focus
             //   postInsertDwDetail();

            }
            return 0;
        }
        function SheetLoadComplete() {

            if (Gcoop.GetEl("HdIsPostBack").value != "true") {
                Gcoop.SetLastFocus("member_no_0");
                Gcoop.Focus();
            }
        }

        
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="Hfmember_no" runat="server" />
    <asp:HiddenField ID="Hidmembsection" runat="server" />
    <asp:HiddenField ID="Hidgroup" runat="server" />
    <asp:HiddenField ID="HdIsPostBack" runat="server" Value="false" />
    <asp:HiddenField ID="HdDetailRow" runat="server" Value="false" />
    <asp:TextBox ID="TextDwmain" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="Textdwhistory" runat="server" Visible="False"></asp:TextBox>
    <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_kp_keepother_main"
        LibraryList="~/DataWindow/keeping/kp_opr_receive_store_other.pbl" ClientScriptable="True"
        ClientEventItemChanged="itemChanged" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientEventButtonClicked="Click_search"
        ClientEventClicked="checkMain" TabIndex="1">
    </dw:WebDataWindowControl>
    <dw:WebDataWindowControl ID="dw_detail" runat="server" DataWindowObject="d_kp_keepother_detail"
        LibraryList="~/DataWindow/keeping/kp_opr_receive_store_other.pbl" ClientScriptable="True"
        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        ClientFormatting="True" TabIndex="500" ClientEventButtonClicked="OnDwdetailButtonClicked">
    </dw:WebDataWindowControl>
</asp:Content>
