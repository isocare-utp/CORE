<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_cmucfslipitemtype.ascx.cs"
    Inherits="Saving.Applications.mbshr.u_cmucfslipitemtype" %>
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
           
                <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_cmucfslipitemtype"
                    LibraryList="~/DataWindow/mbshr/admin_cm_constant_config.pbl"
                    ClientEventButtonClicked="OnButtonClick" ClientScriptable="True" 
                     OnBeginUpdate="PreUpdate" OnEndUpdate="PostUpdate">
                </dw:WebDataWindowControl>
                <span class="linkSpan" onclick="OnInsert()"style="font-size: small; ccolor:Green;
    float:right">เพิ่มแถว</span> <span style="font-size: small; color: #808080;">(หมายเหตุ
        - หลังจาก เพิ่มแถว/ลบแถว  แล้วกดปุ่ม save อีกครั้ง )</span>  
            </td>
        </tr>
    </table>
</center>
