<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_cmucfapvcclcause.ascx.cs"
    Inherits="Saving.Applications.u_cmucfapvcclcause" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<% Page_LoadComplete(); %>
<script type="text/javascript">
    function OnButtonClick(sender, row, name) {
        if (name == "b_delete") {
            var detail = "รหัส " + objDwMain.GetItem(row, "apvcclcause_code");
            detail += " : " + objDwMain.GetItem(row, "apvcclcause_desc");

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
<dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_cmucfapvcclcause"
    LibraryList="~/DataWindow/keeping/admin_cm_constant_config.pbl" ClientEventButtonClicked="OnButtonClick"
    ClientScriptable="True" RowsPerPage="20" OnBeginUpdate="PreUpdate" OnEndUpdate="PostUpdate">
    <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
    </PageNavigationBarSettings>
</dw:WebDataWindowControl>
<span class="linkSpan" onclick="OnInsert()" style="font-size: small; color: #808080;
    float: left">เพิ่มแถว</span> 