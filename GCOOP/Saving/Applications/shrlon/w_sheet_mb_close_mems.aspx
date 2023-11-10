<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_mb_close_mems.aspx.cs"
    Inherits="Saving.Applications.mbshr.w_sheet_mb_close_mems" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <script type="text/javascript">

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล?");
        }
      

        function MenubarNew() {
            if (confirm("ยืนยันการล้างข้อมูลบนหน้าจอ")) {

                newClear();
            }
        }

        function OnDwMainButtonClick(s, r, b) {
            var member_no = objDw_main.GetItem(r, "member_no");
            if (b == "b_dept") {
                var count_dept = objDw_main.GetItem(r,"count_dept");
                if(count_dept != 0)
                {
                    Gcoop.OpenDlg(700, 300, "w_dlg_deptdetail.aspx", "?member_no=" + member_no);
                }
                else 
                {
                    alert("ไม่มีข้อมูลรายการเงินฝากคงเหลือ");
                }
            }
            else if(b == "b_loan")
            {
                var count_loan = objDw_main.GetItem(r, "count_loan");
                if(count_loan != 0) {
                    Gcoop.OpenDlg(500, 300, "w_dlg_loandetail.aspx", "?member_no=" + member_no);
                }
                else
                {
                    alert("ไม่มีข้อมูลรายการเงินกู้คงเหลือ");
                }
            }
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:Panel ID="Panel1" runat="server" Height="500px" ScrollBars="Auto" 
        Width="700px">
        <dw:WebDataWindowControl ID="Dw_main" runat="server" DataWindowObject="d_mbsrv_list_clsmember"
                    LibraryList="~/DataWindow/mbshr/mb_close_mems.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" 
    AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    Width="720px" TabIndex="1" ClientFormatting="True" 
            RowsPerPage="15" ClientEventButtonClicked="OnDwMainButtonClick">
            <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
            </PageNavigationBarSettings>
        </dw:WebDataWindowControl>
    </asp:Panel>
    <br />
    <table style="width: 100%;">
        <tr>
            <td colspan="2" valign="top">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
