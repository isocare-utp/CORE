<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_mbshr_ccl_mbresign.aspx.cs"
    Inherits="Saving.Applications.mbshr.w_sheet_mbshr_ccl_mbresign" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=jsMemberNo %>
    <%=newClear%>

    <script type="text/javascript">
        function Validate() {
            objdw_main.AcceptText();
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function ItemChangedMain(sender, rowNumber, columnName, newValue) {
            if (columnName == "member_no") {
                objdw_main.SetItem(rowNumber, "member_no", newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("HMember_no").value = objdw_main.GetItem(1, "member_no");          
                jsMemberNo();
            }
        }
        function MenubarNew() {
            if (confirm("ยืนยันการล้างข้อมูลบนหน้าจอ")) {
                newClear();
            }
        }
        function MenubarOpen() {
            Gcoop.OpenIFrame('650', '590', 'w_dig_sl_member_search_resign.aspx', '');
        }

        function GetMemDetFromDlg(memberno) {
            objdw_main.SetItem(1, "member_no", memberno);
            objdw_main.AcceptText();
            Gcoop.GetEl("HMember_no").value = memberno;
            jsMemberNo();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="HMember_no" runat="server" />
    <table style="width: 100%;">
        <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_mbsrv_ccl_resign"
                    LibraryList="~/DataWindow/mbshr/sl_cancelresign.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    ClientEventItemChanged="ItemChangedMain" ClientFormatting="True">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
</asp:Content>
