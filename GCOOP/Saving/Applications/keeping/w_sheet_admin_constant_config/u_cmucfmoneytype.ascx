<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_cmucfmoneytype.ascx.cs"
    Inherits="Saving.Applications.keeping.u_cmucfmoneytype" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<% Page_LoadComplete(); %>

<script type="text/javascript">
    function OnButtonClick(sender, row, name) {
        if (name == "b_delete") {
            var detail = "รหัส " + objDwMain.GetItem(row, "moneytype_code");
            detail += " : " + objDwMain.GetItem(row, "moneytype_desc");

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
</script>

<dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_cmucfmoneytype"
    LibraryList="~/DataWindow/keeping/admin_cm_constant_config.pbl" ClientEventButtonClicked="OnButtonClick"
    ClientScriptable="True" RowsPerPage="20" OnBeginUpdate="PreUpdate" OnEndUpdate="PostUpdate">
    <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
    </PageNavigationBarSettings>
</dw:WebDataWindowControl>
<span class="linkSpan" onclick="OnInsert()" style="font-size: small;  color:Green;
    float:right">เพิ่มแถว</span> <span style="font-size: small; color: #808080;">(หมายเหตุ
        - หลังจาก เพิ่มแถว/ลบแถว  แล้วกดปุ่ม save อีกครั้ง )</span>  