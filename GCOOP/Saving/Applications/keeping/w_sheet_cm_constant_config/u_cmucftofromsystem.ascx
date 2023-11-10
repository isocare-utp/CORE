﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_cmucftofromsystem.ascx.cs"
    Inherits="Saving.Applications.keeping.u_cmucftofromsystem" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>

<script type="text/javascript">
    function OnButtonClick(sender, row, name) {
        if (name == "b_delete") {
            var detail = "รหัสระบบ : " + objdw_main.GetItem(row, "system_code");
            if (confirm("คุณต้องการลบรายการ " + detail + " ใช่หรือไม่?")) {
                objdw_main.DeleteRow(row);
            }
        }
        return 0;
    }

    function MenubarSave() {
        if (confirm("บันทึกการแก้ไขข้อมูล?"))
        { objdw_main.Update(); }
    }

    function OnInsert() {
        objdw_main.InsertRow(objdw_main.RowCount() + 1);
    }

</script>

<table style="width: 100%;">
    <tr>
        <td align="center">
            <span class="linkSpan" onclick="OnInsert()">>> เพิ่มแถว</span><br />
            <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_cmucftofromsystem"
                LibraryList="~/DataWindow/Shrlon/cm_constant_config.pbl" 
                ClientScriptable="True" onbeginupdate="dw_main_BeginUpdate" 
                onendupdate="dw_main_EndUpdate" ClientEventButtonClicked="OnButtonClick">
            </dw:WebDataWindowControl>
            <span class="linkSpan" onclick="OnInsert()">>> เพิ่มแถว</span>
        </td>
    </tr>
</table>
