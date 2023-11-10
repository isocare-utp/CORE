<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_cmucftofromaccid.ascx.cs"
    Inherits="Saving.Applications.shrlon.u_cmucftofromaccid" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>

<script type="text/javascript">
    function OnButtonClick(sender, row, name) {
        if (name == "b_delete") {
            var detail = "\nรหัสเงิน : " + objdw_data.GetItem(row, "moneytype_code");
            detail = detail + "\nรหัสบัญชี : " + objdw_data.GetItem(row, "account_id");
            if (confirm("คุณต้องการลบรายการ " + detail + "\nใช่หรือไม่?")) {
                objdw_data.DeleteRow(row);
            }
        }
        return 0;
    }

    function MenubarSave() {
        if (confirm("บันทึกการแก้ไขข้อมูล?"))
        { objdw_data.Update(); }
    }

    function OnInsert() {
        objdw_data.InsertRow(objdw_data.RowCount() + 1);
    }

</script>

<table style="width: 100%;">
    <tr>
        <td align="center">
            <span class="linkSpan" onclick="OnInsert()">>> เพิ่มแถว</span><br />
            <dw:WebDataWindowControl ID="dw_data" runat="server" DataWindowObject="d_cmucftofromaccid"
                LibraryList="~/DataWindow/Shrlon/cm_constant_config.pbl" ClientScriptable="True"
                Height="520px" ClientEventButtonClicked="OnButtonClick" 
                onbeginupdate="dw_data_BeginUpdate" onendupdate="dw_data_EndUpdate" Width="520px" Height="500px">
            </dw:WebDataWindowControl>
            <span class="linkSpan" onclick="OnInsert()">>> เพิ่มแถว</span>
        </td>
    </tr>
</table>
