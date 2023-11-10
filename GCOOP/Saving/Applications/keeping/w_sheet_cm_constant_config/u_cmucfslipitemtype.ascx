<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_cmucfslipitemtype.ascx.cs"
    Inherits="Saving.Applications.keeping.u_cmucfslipitemtype" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<script type="text/javascript">
    function OnButtonClick(sender, row, name) {
        if (name == "b_delete") {
            var detail = "รหัสรายการ :" + objdw_main.GetItem(row, "slipitemtype_code");
            if (confirm("คุณต้องการลบ " + detail + " ใช่หรือไม่?")) {
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

<center>
    <table style="width: 100%;">
        <tr>
            <td align="center">
            <span class="linkSpan" onclick="OnInsert()">>> เพิ่มแถว</span><br />
                <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_cmucfslipitemtype"
                    LibraryList="~/DataWindow/Shrlon/cm_constant_config.pbl" 
                    ClientEventButtonClicked="OnButtonClick" ClientScriptable="True" 
                    onbeginupdate="dw_main_BeginUpdate" onendupdate="dw_main_EndUpdate">
                </dw:WebDataWindowControl>
                <span class="linkSpan" onclick="OnInsert()">>> เพิ่มแถว</span>
            </td>
        </tr>
    </table>
</center>
