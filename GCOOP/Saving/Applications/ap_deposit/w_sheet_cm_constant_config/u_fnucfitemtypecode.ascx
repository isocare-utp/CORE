<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_fnucfitemtypecode.ascx.cs"
    Inherits="Saving.Applications.ap_deposit.u_fnucfitemtypecode" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<% Page_LoadComplete(); %>
<script type="text/javascript">
    function OnButtonClick(sender, row, name) {
        if (name == "b_delete") {
            var detail = "รหัส " + objdw_main.GetItem(row, "tax_code");
            detail += " : " + objdw_main.GetItem(row, "tax_desc");

            if (confirm("คุณต้องการลบรายการ " + detail + " ใช่หรือไม่?")) {
                objdw_main.DeleteRow(row);
            }
        }
        return 0;
    }

    function MenubarSave() {
        if (confirm("ยืนยันการบันทึกข้อมูลทั้งหมด?")) {
            var a = objdw_main.GetItem(1, "coop_id");
            for (var j = 1; j <= objdw_main.RowCount(); j++) {
                objdw_main.SetItem(j, "coop_id", a);
            }
            objdw_main.AcceptText();
            objdw_main.Update();
        }
    }

    function OnInsert() {
        objdw_main.InsertRow(0);
    }

</script>
<span class="linkSpan" onclick="OnInsert()">เพิ่มแถว</span>
<br />
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
<dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_fnucfitemtype"
    LibraryList="~/DataWindow/App_finance/cm_constant_config.pbl" ClientScriptable="True"
    OnBeginUpdate="dw_main_BeginUpdate" OnEndUpdate="dw_main_EndUpdate" BorderStyle="None"
    ClientEventButtonClicked="OnButtonClick">
</dw:WebDataWindowControl>
<span class="linkSpan" onclick="OnInsert()">เพิ่มแถว</span> 