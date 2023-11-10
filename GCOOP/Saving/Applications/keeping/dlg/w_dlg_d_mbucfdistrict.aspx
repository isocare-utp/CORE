<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_d_mbucfdistrict.aspx.cs"
    Inherits="Saving.Applications.keeping.dlg.w_dlg_d_mbucfdistrict" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>เขต/อำเภอ</title>
    <%=postSave%>
    <%=insertRow %>
    <%=changeDistrict%>
    <script type="text/javascript">
        function DialogLoadComplete() {
            var check = Gcoop.GetEl("HdCloseDlg").value;
            if (check == "true") {
                window.close();
            }
        }
        function OnInsertdistrict() {
            objdw_district.AcceptText();
            insertRow();
        }
        function Dwdistrict_Changed(s, r, c, v) {
            if (c == "district_desc") {

                objdw_district.SetItem(r, "district_desc", v);
                //   alert(v);
                Gcoop.GetEl("Hidrow").value = r + "";
                Gcoop.GetEl("Hdistrict_codec").value = objdw_district.GetItem(r, "district_code");
                Gcoop.GetEl("Hdistrict_desc").value = objdw_district.GetItem(r, "district_desc");
                Gcoop.GetEl("Hpostcode").value = objdw_district.GetItem(r, "postcode");
                objdw_district.AcceptText();

            }
            else if (c == "postcode") {
                //  alert(c);
                objdw_district.SetItem(r, "postcode", v)
                Gcoop.GetEl("Hidrow").value = r + "";
                Gcoop.GetEl("Hpostcode").value = v;
                Gcoop.GetEl("Hdistrict_codec").value = objdw_district.GetItem(r, "district_code");
                Gcoop.GetEl("Hdistrict_desc").value = objdw_district.GetItem(r, "district_desc");
                objdw_district.AcceptText();

            }
            else if (c == "district_code") {
                //alert(c);
                objdw_district.SetItem(r, "district_code", v)
                Gcoop.GetEl("Hidrow").value = r + "";
                Gcoop.GetEl("Hdistrict_code").value = v;
                Gcoop.GetEl("Hdistrict_desc").value = objdw_district.GetItem(r, "district_desc");
                Gcoop.GetEl("Hpostcode").value = objdw_district.GetItem(r, "postcode");
                objdw_district.AcceptText();

            }
            Gcoop.GetEl("Hprovince_code").value = objdw_district.GetItem(r, "province_code");



        }
        function OnSave() {
            if (confirm("ต้องการบันทึกข้อมูลใช่ หรือไม่?")) {
                var cccc = Gcoop.GetEl("Hdistrict_desc").value + Gcoop.GetEl("Hpostcode").value + Gcoop.GetEl("Hdistrict_code").value + Gcoop.GetEl("Hprovince_code").value;
                //  alert(cccc);
                objdw_district.AcceptText();
                //objdw_district.Update();
                postSave();
            }
            else {
                window.close();
            }
        }
        function OnButtonClick(sender, row, name) {
            if (name == "b_delete") {
                var detail = "รหัส " + objdw_district.GetItem(row, "district_code");
                detail += " : " + objdw_district.GetItem(row, "district_desc");
                if (confirm("คุณต้องการลบรายการ " + detail + " ใช่หรือไม่?")) {
                    objdw_district.DeleteRow(row);
                }
            }
            return 0;
        }
       
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="Hidrow" runat="server" />
    <asp:HiddenField ID="HSql" runat="server" />
    <asp:HiddenField ID="Hprovince_code" runat="server" />
    <asp:HiddenField ID="Hdistrict_code" runat="server" />
    <asp:HiddenField ID="Hdistrict_desc" runat="server" />
    <asp:HiddenField ID="Hpostcode" runat="server" />
    <div>
        <dw:WebDataWindowControl ID="dw_district" runat="server" DataWindowObject="d_mbucfdistrict"
            LibraryList="~/DataWindow/keeping/admin_cm_constant_config.pbl" ClientEventButtonClicked="OnButtonClick"
            ClientScriptable="True" RowsPerPage="20" AutoRestoreContext="False" AutoRestoreDataCache="True"
            AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientEventItemChanged="Dwdistrict_Changed">
            <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
            </PageNavigationBarSettings>
        </dw:WebDataWindowControl>
        <span class="linkSpan" onclick="OnInsertdistrict()" style="font-size: small; color: Red;
            float: left">เพิ่มแถว</span> <span style="font-size: small; color: #808080;">(หมายเหตุ
                - เพิ่มได้ครั้งล่ะ 1 แถว )</span><span class="linkSpan" onclick="OnSave()" style="font-size: small;
                    color: Green; float: right">บันทึก</span>
    </div>
    <asp:HiddenField ID="HdCloseDlg" runat="server" />
    </form>
</body>
</html>
