<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_mbucfprovince.ascx.cs"
    Inherits="Saving.Applications.keeping.u_mbucfprovince" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<% Page_LoadComplete(); %>

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

<asp:HiddenField ID="Hprovince_code" runat="server" />
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>

        <table style="width: 100%;">
            <tr>
                <td colspan="3" valign="top" >
                  <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_mbucfprovince"
                LibraryList="~/DataWindow/keeping/admin_cm_constant_config.pbl" ClientEventButtonClicked="OnButtonClick"
                ClientScriptable="True" RowsPerPage="20"  ClientEventClicked="Seleted">
                <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
                </PageNavigationBarSettings>
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
        <%-- <td valign="top">
            <dw:WebDataWindowControl ID="dw_district" runat="server" DataWindowObject="d_mbucfdistrict"
                LibraryList="~/DataWindow/keeping/admin_cm_constant_config.pbl" ClientEventButtonClicked="OnButtonClick"
                ClientScriptable="True" RowsPerPage="20" OnBeginUpdate="PreUpdate" OnEndUpdate="PostUpdate">
                <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
                </PageNavigationBarSettings>
            </dw:WebDataWindowControl>
            <span class="linkSpan" onclick="OnInsertdistrict()" style="font-size: small; color: #808080;
                float: right">เพิ่มแถว</span>
        </td>--%>
   
