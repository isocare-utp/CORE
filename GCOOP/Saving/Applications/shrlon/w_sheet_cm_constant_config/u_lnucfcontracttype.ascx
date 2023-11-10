<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_lnucfcontracttype.ascx.cs"
    Inherits="Saving.Applications.shrlon.u_lnucfcontracttype" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>

<script type="text/javascript">
    function OnButtonClick(sender, row, name) {
        if (name == "b_delete") {
            var detail = "รหัส : " + objdw_main.GetItem(row, "contract_type");
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
            <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_lnucfcontracttype"
                LibraryList="~/DataWindow/Shrlon/cm_constant_config.pbl" 
                ClientScriptable="True" onbeginupdate="dw_main_BeginUpdate" 
                onendupdate="dw_main_EndUpdate" ClientEventButtonClicked="OnButtonClick" Width="520px" Height="500px">
            </dw:WebDataWindowControl>
            <span class="linkSpan" onclick="OnInsert()">>> เพิ่มแถว</span>
        </td>
    </tr>
</table>
