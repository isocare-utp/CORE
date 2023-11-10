<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_cmucfbankbranch.ascx.cs"
    Inherits="Saving.Applications.ap_deposit.u_cmucfbankbranch" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<% Page_LoadComplete(); %>
<script type="text/javascript">
    function OnButtonClick(sender, row, name) {
        if (name == "b_delete") {
            var detail = "รหัส " + objDwMain.GetItem(row, "bank_code");
            detail += " : " + objDwMain.GetItem(row, "branch_name");

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
<dw:WebDataWindowControl ID="DwMain" runat="server" ClientEventButtonClicked="OnButtonClick"
    ClientScriptable="True" DataWindowObject="d_cmucfbankbranch" LibraryList="~/DataWindow/ap_deposit/cm_constant_config.pbl"
    RowsPerPage="20" Style="top: 0px; left: 0px" OnBeginUpdate="PreUpdate" OnEndUpdate="PostUpdate">
    <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
    </PageNavigationBarSettings>
</dw:WebDataWindowControl>
<span class="linkSpan" onclick="OnInsert()">เพิ่มแถว</span>