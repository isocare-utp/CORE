<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_cmaccountyear.ascx.cs"
    Inherits="Saving.Applications.u_cmaccountyear" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<% Page_LoadComplete(); %>
<script type="text/javascript">
    function OnButtonClick(sender, row, name) {
        if (name == "b_delete") {
            var detail = "ปีบัญชี" + objDwMain.GetItem(row, "account_year");

            if (confirm("คุณต้องการลบรายการ " + detail + " ใช่หรือไม่?")) {
                objDwMain.DeleteRow(row);
            }
        }
        return 0;
    }

    function MenubarSave() {
        if (confirm("ยืนยันการบันทึกข้อมูลทั้งหมด?")) {
            objDwMain.Update();
        }
    }

    function OnInsert() {
        objDwMain.InsertRow(0);
    }
</script>
<dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_cmaccountyear"
    LibraryList="~/DataWindow/keeping/admin_cm_constant_config.pbl" ClientEventButtonClicked="OnButtonClick"
    ClientScriptable="True" RowsPerPage="20" ClientFormatting="True" AutoSaveDataCacheAfterRetrieve="True"
    AutoRestoreDataCache="True" AutoRestoreContext="False">
    <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
    </PageNavigationBarSettings>
</dw:WebDataWindowControl>
<span class="linkSpan" onclick="OnInsert()" style="font-size: small; color: #808080;
    float: left">เพิ่มแถว</span> 