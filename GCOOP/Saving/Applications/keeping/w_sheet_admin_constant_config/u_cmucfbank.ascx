<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_cmucfbank.ascx.cs"
    Inherits="Saving.Applications.keeping.u_cmucfbank" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>

<script type="text/javascript">
    function OnButtonClick(sender, row, name) {
        if (name == "b_delete") {
            var detail = "รหัส " + objDwMain.GetItem(row, "bank_code");
            detail += " : " + objDwMain.GetItem(row, "bank_desc");

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
        objDwMain.InsertRow(objDwMain.RowCount() + 1);
    }
   function Selected(sender, row, name) {
        if (name == "b_branch") {
            var bank_code = objDwMain.GetItem(row, "bank_code");
            Gcoop.OpenDlg(690, 650, "w_dlg_bankbranch.aspx", "?bank_code=" + bank_code);
        }
    }
</script>

<dw:WebDataWindowControl ID="DwMain" runat="server" ClientScriptable="True" DataWindowObject="d_cmucfbank"
    LibraryList="~/DataWindow/keeping/admin_cm_constant_config.pbl" ClientEventButtonClicked="OnButtonClick"
    RowsPerPage="20" OnBeginUpdate="PreUpdate" OnEndUpdate="PostUpdate" ClientEventClicked="Selected">
    <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
    </PageNavigationBarSettings>
</dw:WebDataWindowControl>
<span class="linkSpan" onclick="OnInsert()" style="font-size: small; color: #808080;
    float: left">เพิ่มแถว</span> 