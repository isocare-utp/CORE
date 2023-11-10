<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_slipbank.aspx.cs"
    Inherits="Saving.Applications.app_finance.dlg.w_dlg_slipbank" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ระบบงานการเงิน</title>

    <script type="text/javascript">
        var item_amt = "";
        function DDLchanged(sender, row, columnName, newValue) {
            if (columnName == "bank_code") {
                var str_temp = window.location.toString();
                var str_arr = str_temp.split("&", 2);
                window.location = str_arr[0] + "&bank_code=" + newValue;
            }
        }
        function ButtonClear() {
            if (confirm("ยืนยันการสร้างข้อมูลใหม่?")) {
                var str_clear = window.location.toString();
                var str_clear2 = str_clear.split("&", 2);
                window.location = str_clear2[0] + "&cmd=new";
            }
        }
        function ButtonCancel() {
            window.close();
        }
        function ButtonSave() {
            if (confirm("ยืนยันการบันทึกข้อมูลทั้งหมด?")) {
                objd_slipbank.AcceptText();
                objd_slipbank.Update();
            }
            return 0;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dw:WebDataWindowControl ID="d_slipbank" runat="server" DataWindowObject="d_slipbank"
            LibraryList="~/DataWindow/App_finance/bankaccount.pbl" ClientScriptable="True"
            ClientEventItemChanged="DDLchanged" OnBeginUpdate="d_slipbank_BeginUpdate" OnEndUpdate="d_slipbank_EndUpdate">
        </dw:WebDataWindowControl>
    </div>
    <input style="float: left;" id="clear" type="button" value="ล้าง" onclick="ButtonClear()" />
    <input style="float: right;" id="cancle" type="button" value="ยกเลิก" onclick="ButtonCancel()" />
    <input style="float: right;" id="save" type="button" value="บันทึก" onclick="ButtonSave()" />
    </form>
</body>
</html>
