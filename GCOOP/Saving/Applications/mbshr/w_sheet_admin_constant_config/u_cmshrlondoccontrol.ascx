<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_cmshrlondoccontrol.ascx.cs"
    Inherits="Saving.Applications.mbshr.u_cmshrlondoccontrol" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>

<script type="text/javascript">
    function OnButtonClick(sender, row, name) {
        if (name == "b_delete") {
            var detail = "รหัส " + objdw_main.GetItem(row, "document_code");
            detail += " : " + objdw_main.GetItem(row, "document_name");

            if (confirm("คุณต้องการลบรายการ " + detail + " ใช่หรือไม่?")) {
                objdw_main.DeleteRow(row);
            }
        }
        return 0;
    }

    function MenubarSave() {
        if (confirm("ยืนยันการบันทึกข้อมูลทั้งหมด?")) {
          
            objdw_main.Update();
        }
    }

    function OnInsert() {
        objdw_main.InsertRow(objdw_main.RowCount() + 1);

    }
</script>

<dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_cmshrlondoccontrol"
    LibraryList="~/DataWindow/mbshr/admin_cm_constant_config.pbl" ClientEventButtonClicked="OnButtonClick"
    ClientScriptable="True" RowsPerPage="20" OnBeginUpdate="dw_main_BeginUpdate"
    OnEndUpdate="dw_main_EndUpdate" >
    <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
    </PageNavigationBarSettings>
</dw:WebDataWindowControl>
<span class="linkSpan" onclick="OnInsert()" style="font-size: small; color: #808080;
    float: left">เพิ่มแถว</span> 