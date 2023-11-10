<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_mbucfprovince.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_mbucfprovince" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <%=initJavaScript %>

    <script type="text/javascript">
        function OnButtonClick(sender, row, name) {
            if (name == "b_delete") {
                var detail = "รหัส " + objdw_main.GetItem(row, "province_code");
                detail += " : " + objdw_main.GetItem(row, "province_desc");

                if (confirm("คุณต้องการลบรายการ " + detail + " ใช่หรือไม่?")) {
                    objdw_main.DeleteRow(row);
                }
            }
            return 0;
        }

        function Seleted(s, r, c) {
            if (c == "province_code" || c == "province_desc") {
                var province_code = objdw_main.GetItem(r, "province_code");
                if (province_code != null) {
                    Gcoop.OpenDlg(570, 650, "w_dlg_d_mbucfdistrict.aspx", "?province=" + province_code);
                }
            }
        }
        function MenubarSave() {
            if (confirm("ยืนยันการบันทึกข้อมูลทั้งหมด?")) {
                objdw_main.Update();
            }
        }

        function OnInsertprovince() {
            objdw_main.InsertRow(objdw_main.RowCount() + 1);
        }
        function OnInsertDistrict() {
            Gcoop.OpenDlg(650, 650, "w_dlg_d_mbucfdistrict.aspx", "");
        }
        function OnInsertTambol() {

            Gcoop.OpenDlg(650, 650, "w_dlg_mbucftambol.aspx", "");
        }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:HiddenField ID="Hprovince_code" runat="server" />
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>

        <table style="width: 100%;">
            <tr>
                <td colspan="3" valign="top" >
                  <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_mbucfprovince"
                LibraryList="~/DataWindow/shrlon/admin_cm_constant_config.pbl" ClientEventButtonClicked="OnButtonClick"
                ClientScriptable="True" ClientEventClicked="Seleted">
            </dw:WebDataWindowControl> 
                </td>
               
            </tr>
            <tr>
                <td align="center">
                    <span class="linkSpan" onclick="OnInsertprovince()" style="font-size: small; color:Red;
                float: left">เพิ่มแถว</span> 
                </td>
               <td align="center"> <span style="font-size: small; color: #808080;">(หมายเหตุ
        - หลังจาก เพิ่มแถว/ลบแถว  แล้วกดปุ่ม save อีกครั้ง )</span>  &nbsp;
                
                </td>
                <td align="center">
                    <span class="linkSpan" onclick="OnInsertTambol()"
                    style="font-size: small; color:Green; float: right">เพิ่มตำบล</span>&nbsp;
                </td>
            </tr>
          
        </table>
</asp:Content>
