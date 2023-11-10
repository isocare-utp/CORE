<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_bankbranch.aspx.cs"
    Inherits="Saving.Applications.keeping.dlg.w_dlg_bankbranch" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>สาขาธนาคาร</title>
    <%=postSave%>
    <%=insertRow %>
    <%=changebankbranch%>
<%=getbranch%>
    <script type="text/javascript">
        function DialogLoadComplete() {
            var check = Gcoop.GetEl("HdCloseDlg").value;
            if (check == "true") {
                window.close();
            }
        }
        function OnInsert() {
            objdw_main.AcceptText();
            insertRow();
        }
        function OnSave() {
            if (confirm("ต้องการบันทึกข้อมูลใช่ หรือไม่?")) {
                objdw_main.AcceptText();
                objdw_main.Update();
                // postSave();

            }
            else {
                window.close();
            }
        }
        function OnButtonClick(sender, row, name) {
            if (name == "b_delete") {
                var detail = "รหัส " + objdw_main.GetItem(row, "branch_id");
                detail += " : " + objdw_main.GetItem(row, "branch_name");
                if (confirm("คุณต้องการลบรายการ " + detail + " ใช่หรือไม่?")) {
                    objdw_main.DeleteRow(row);
                }
            }
            return 0;
        }
        function OnSearchClick(s, r, c) {
            if (c == "cb_find") {
                //alert(c);
                Gcoop.GetEl("Hbank_no").value = objdw_search.GetItem(1, "bank_no");
                Gcoop.GetEl("Hbank_name").value = objdw_search.GetItem(1, "bank_name");
                getbranch();
            }
        }
        //        function ProvinceChanged(sender, rowNumber, columnName, newValue) {
        //            if (columnName == "bank_code") {
        //                objdw_main.SetItem(rowNumber, columnName, newValue);
        //                Gcoop.GetEl("Hprovince_code").value = objdw_main.GetItem(rowNumber, "province_code");
        //                objdw_main.AcceptText();
        //                changebankbranch();
        //            }
        //        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="Hbank_no" runat="server" />
    <asp:HiddenField ID="Hbank_name" runat="server" />
    <asp:HiddenField ID="HSql" runat="server" />
    <asp:HiddenField ID="Hprovince_code" runat="server" />
    <div>
        <dw:WebDataWindowControl ID="dw_search" runat="server" ClientEventButtonClicked="OnSearchClick"
            ClientScriptable="True" DataWindowObject="d_search_bank" LibraryList="~/DataWindow/keeping/admin_cm_constant_config.pbl" AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True">
        </dw:WebDataWindowControl>
    </div>
    <div>
        <dw:WebDataWindowControl ID="dw_main" runat="server" ClientEventButtonClicked="OnButtonClick"
            ClientScriptable="True" DataWindowObject="d_cmucfbankbranch" LibraryList="~/DataWindow/keeping/admin_cm_constant_config.pbl"
            RowsPerPage="20" Style="top: 0px; left: 0px" Width="570px" Height="550px">
            <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
            </PageNavigationBarSettings>
        </dw:WebDataWindowControl>
        <span class="linkSpan" onclick="OnInsert()" style="font-size: small; color: Red;
            float: left">เพิ่มแถว</span> <span class="linkSpan" onclick="OnSave()" style="font-size: small;
                color: Green; float: right">บันทึก</span>
    </div>
    <asp:HiddenField ID="HdCloseDlg" runat="server" />
    </form>
</body>
</html>
