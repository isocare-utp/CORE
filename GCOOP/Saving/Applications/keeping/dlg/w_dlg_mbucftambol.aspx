<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_mbucftambol.aspx.cs"
    Inherits="Saving.Applications.keeping.dlg.w_dlg_mbucftambol" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>แขวง/ตำบล</title>
    <%=postSave%>
    <%=insertRow %>
    <%=changeDistrict%>
    <%=jsGetTambol%>

    <script type="text/javascript">
        function DialogLoadComplete() {
            var check = Gcoop.GetEl("HdCloseDlg").value;
            if (check == "true") {
                window.close();
            }
        }
        function OnInsertdistrict() {
            insertRow();
        }
        function OnSave() {
            if (confirm("ต้องการบันทึกข้อมูลใช่ หรือไม่?")) {
                objdw_tambol.AcceptText();
                postSave();
                // objdw_tambol.Update();
            }
            else {
                window.close();
            }
        }
        function OnButtonClick(sender, row, name) {
            if (name == "b_delete") {
                var detail = "รหัส " + objdw_tambol.GetItem(row, "tambol_code");
                detail += " : " + objdw_tambol.GetItem(row, "tambol_desc");
                if (confirm("คุณต้องการลบรายการ " + detail + " ใช่หรือไม่?")) {
                    objdw_tambol.DeleteRow(row);
                }
            }
            return 0;
        }
        function ItemChanged(s, r, c, v) {

            if (c == "province_code") {
                objdw_search.SetItem(r, c, v);
                Gcoop.GetEl("Hprovince_code").value = v;
                objdw_search.AcceptText();
                changeDistrict();
            }
            //            else if (c == "district_code") {
            //                objdw_tambol.SetItem(r, c, v);
            //                objdw_tambol.AcceptText();
            //                jsGetTambol();
            //            }
        }
        function OnSearchClick(s, r, c) {
            if (c == "cb_find") {


                Gcoop.GetEl("Hdistrict_code").value = objdw_search.GetItem(1, "district_code");
                alert(Gcoop.GetEl("Hprovince_code").value + Gcoop.GetEl("Hdistrict_code").value);

                jsGetTambol();
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="Hprovince_code" runat="server" />
    <asp:HiddenField ID="Hdistrict_code" runat="server" />
    <div>
        <dw:WebDataWindowControl ID="dw_search" runat="server" ClientEventButtonClicked="OnSearchClick"
            ClientScriptable="True" DataWindowObject="d_search_tambol" LibraryList="~/DataWindow/keeping/admin_cm_constant_config.pbl"
            AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
            ClientFormatting="True" ClientEventItemChanged="ItemChanged">
        </dw:WebDataWindowControl>
    </div>
    <div>
        <dw:WebDataWindowControl ID="dw_tambol" runat="server" DataWindowObject="d_mbucftambol"
            LibraryList="~/DataWindow/keeping/admin_cm_constant_config.pbl" ClientEventButtonClicked="OnButtonClick"
            ClientScriptable="True" RowsPerPage="20" AutoRestoreContext="False" AutoRestoreDataCache="True"
            AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" OnBeginUpdate="PreUpdate"
            OnEndUpdate="PostUpdate">
            <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
            </PageNavigationBarSettings>
        </dw:WebDataWindowControl>
    </div>
    <table style="width: 100%;">
        <tr>
            <td align="center">
                <span class="linkSpan" onclick="OnInsertdistrict()" style="font-size: small; color: Red;
                    float: left">เพิ่มแถว</span>
            </td>
            <td align="center">
                <span style="font-size: small; color: #808080; ">(หมายเหตุ - หลังจาก เพิ่มแถว/ลบแถว
                    แล้วกดปุ่ม save อีกครั้ง )</span> &nbsp;
            </td>
            <td align="center">
                <span class="linkSpan" onclick="OnSave()" style="font-size: small; color: Green;
                    float: right">บันทึก</span>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdCloseDlg" runat="server" />
    </form>
</body>
</html>
