<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_cmucfsalarybalance.ascx.cs"
    Inherits="Saving.Applications.mbshr.u_cmucfsalarybalance" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<% Page_LoadComplete(); %>

<script type="text/javascript">
    function OnButtonClick(sender, row, name) {
        if (name == "b_delete") {
            var detail = "รหัส : " + objDwMain.GetItem(row, "salarybal_code");
            if (confirm("คุณต้องการลบรายการ " + detail + " ใช่หรือไม่?")) {
                objDwMain.DeleteRow(row);
            }
        }
        return 0;
    }

    function MenubarSave() {
        if (confirm("บันทึกการแก้ไขข้อมูล?"))
        { objDwMain.Update(); }
    }

    function OnInsert() {
        objDwMain.InsertRow(objDwMain.RowCount() + 1);
    }

</script>

<dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_cmucfsalarybalance"
    LibraryList="~/DataWindow/mbshr/admin_cm_constant_config.pbl" ClientScriptable="True"
    ClientEventButtonClicked="OnButtonClick" OnBeginUpdate="PreUpdate" OnEndUpdate="PostUpdate">
</dw:WebDataWindowControl>
<span class="linkSpan" onclick="OnInsert()" style="font-size: small; color:Green;
    float:right">เพิ่มแถว</span> <span style="font-size: small; color: #808080;">(หมายเหตุ
        - หลังจาก เพิ่มแถว/ลบแถว  แล้วกดปุ่ม save อีกครั้ง )</span>  