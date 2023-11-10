<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_cmucfsliptype.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_cmucfsliptype" Title="Untitled Page" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function OnButtonClick(sender, row, name) {
        if (name == "b_delete") {
            var detail = "รหัส : " + objdw_main.GetItem(row, "sliptype_code");
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<table style="width: 100%;">
    <tr>
        <td align="center">
            <span class="linkSpan" onclick="OnInsert()">>> เพิ่มแถว</span><br />
            <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_cmucfsliptype"
                LibraryList="~/DataWindow/Shrlon/cm_constant_config.pbl" ClientScriptable="True"
                OnBeginUpdate="dw_main_BeginUpdate" OnEndUpdate="dw_main_EndUpdate" ClientEventButtonClicked="OnButtonClick">
            </dw:WebDataWindowControl>
            <span class="linkSpan" onclick="OnInsert()">>> เพิ่มแถว</span>
        </td>
    </tr>
</table>
</asp:Content>
