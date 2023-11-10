<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_cmucftofromaccid.ascx.cs"
    Inherits="Saving.Applications.keeping.u_cmucftofromaccid" %>
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

<dw:WebDataWindowControl ID="dw_data" runat="server" DataWindowObject="d_cmucftofromaccid"
    LibraryList="~/DataWindow/keeping/admin_cm_constant_config.pbl" ClientScriptable="True"
    ClientEventButtonClicked="OnButtonClick" OnBeginUpdate="PreUpdate"
    OnEndUpdate="PostUpdate" RowsPerPage="20">
    <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
    </PageNavigationBarSettings>
</dw:WebDataWindowControl>
<span class="linkSpan" onclick="OnInsert()" style="font-size: small;  color:Green;
    float:right">เพิ่มแถว</span> <span style="font-size: small; color: #808080;">(หมายเหตุ
        - หลังจาก เพิ่มแถว/ลบแถว  แล้วกดปุ่ม save อีกครั้ง )</span>  