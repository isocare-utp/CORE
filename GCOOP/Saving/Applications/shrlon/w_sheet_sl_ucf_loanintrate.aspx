<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_sl_ucf_loanintrate.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_ucf_loanintrate" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=postDeleteRow%>
    <%=postInsertRow%>
    <script type="text/javascript">
        function OnClickInsertRow() { //function เพิ่มแถวข้อมูล
            postInsertRow();
        }

        function Delete_ucf_loanintrate(sender, row, bName) { //function ลบแถวข้อมูล
            if (bName == "b_del") {
                var loanintrate_code = objDwMain.GetItem(row, "loanintrate_code");
                if (loanintrate_code == "" || loanintrate_code == null) {

                    objDwMain.DeleteRow(row);
                }
                else {
                    var isConfirm = confirm("ต้องการลบข้อมูลรหัสอัตราดอกเบี้ย " + loanintrate_code + " ใช่หรือไม่ ?");
                    if (isConfirm) {
                        Gcoop.GetEl("Hd_row").value = row + "";
                        postDeleteRow();
                    }
                }
            }
            return 0;
        }

        function Validate() { //function เช็คค่าข้อมูลก่อน save
            var isconfirm = confirm("ยืนยันการบันทึกข้อมูล ?");

            if (!isconfirm) {
                return false;
            }
            var RowDetail = objDwMain.RowCount();

            for (var i = 1; i <= RowDetail; i++) {
                var loanintrate_code = objDwMain.GetItem(i, "loanintrate_code");
                var loanintrate_desc = objDwMain.GetItem(i, "loanintrate_desc");

                if (loanintrate_code == "" || loanintrate_code == null || loanintrate_desc == "" || loanintrate_desc == null) {
                    alert("กรุณาระบุข้อมูลให้ครบถ้วน");
                    return false;
                }
            }
            return true;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>

    <input id="printBtn" type="button" value="พิมพ์หน้าจอ" onclick="printPage();"   />

    <span onclick="OnClickInsertRow()" style="cursor: pointer;">
        <asp:Label ID="LbInsert1" runat="server" Text="เพิ่มแถว" Font-Bold="False" Font-Names="Tahoma"
            Font-Size="14px" Font-Underline="True" ForeColor="Blue" /></span>
    <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="dw_lc_lccfloanintrate"
        LibraryList="~/DataWindow/shrlon/sl_cfinttable.pbl" ClientScriptable="True" ClientEventButtonClicked="Delete_ucf_loanintrate"
        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        RowsPerPage="20">
        <PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="Numeric">
            <BarStyle HorizontalAlign="Center" />
            <NumericNavigator FirstLastVisible="True" />
        </PageNavigationBarSettings>
    </dw:WebDataWindowControl>
    <asp:Label ID="Label2" runat="server" Text="หมายเหตุ : ห้ามแก้ไขรหัสอัตราดอกเบี้ย"
        Font-Bold="False" Font-Names="Tahoma" Font-Size="Small" Font-Underline="True"
        ForeColor="#000000" />
    <asp:HiddenField ID="Hd_row" runat="server" />
</asp:Content>
