<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_dp_add_booktype.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_add_booktype" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsPostInsertRow %>
    <%=jsPostDelRow %>
    <script type="text/javascript">
        function Validate() {
            var IsConfirm = confirm("ยืนยันการบันทึกข้อมูล");

            if (!IsConfirm) {
                return false;
            }

            for (var i = 1; i <= objDwMain.RowCount(); i++) {
                book_type = objDwMain.GetItem(i, "book_type");
                book_grp = objDwMain.GetItem(i, "book_grp");
                book_desc = objDwMain.GetItem(i, "book_desc");
                if (book_type == "" || book_type == null || book_grp == "" || book_grp == null || book_desc == "" || book_desc == null) {
                    alert("กรุณาระบุข้อมูลให้ครบถ้วน");
                    return false;
                }
            }

            return true;
        }

        function InsertRow() {
            jsPostInsertRow();
        }

        function OnDwMainButtonClicked(sender, row, bName) {
            if (confirm("ต้องการลบข้อมูลแถวที่ " + row + " ใช่หรือไม่")) {
                Gcoop.GetEl("Hdrow").value = row + "";
                jsPostDelRow();
            }
        }
    
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <span onclick="InsertRow()" style="cursor: pointer;">
        <asp:Label ID="Label5" runat="server" Text="เพิ่มแถว" Font-Bold="False" Font-Names="Tahoma"
            Font-Size="14px" Font-Underline="True" ForeColor="Blue" /></span>
    <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="dp_add_booktype"
        LibraryList="~/DataWindow/ap_deposit/dp_ucfdptype.pbl" ClientScriptable="True"
        AutoSaveDataCacheAfterRetrieve="True" AutoRestoreDataCache="True" ClientEventItemChanged="OnDwListCoopItemChanged"
        ClientEventItemError="OnError" AutoRestoreContext="False" ClientFormatting="True"
        ClientEventButtonClicked="OnDwMainButtonClicked" RowsPerPage="20">
        <PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="Numeric">
          <BarStyle HorizontalAlign="Center" />
          <NumericNavigator FirstLastVisible="True" />
      </PageNavigationBarSettings>
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="Hdrow" runat="server" Value="" />
</asp:Content>
