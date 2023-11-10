<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_loanrequest_otherclr.aspx.cs"
    Inherits="Saving.Applications.keeping.dlg.w_dlg_sl_loanrequest_otherclr" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>รายการหักอื่นๆ</title>
    <%=saveWebDialog%>
    <%=refreshDW %>

    <script type="text/javascript">
        function OnInsert(dwname) {
            if (dwname == "10") {
                var row = objdw_otherclr.RowCount() + 1;
                objdw_otherclr.InsertRow(objdw_otherclr.RowCount() + 1);
            }
        }
        function DialogLoadComplete() {
            var chkStatus = Gcoop.GetEl("HfChkStatus").value;
            if (chkStatus == "1") {
                window.opener.GetValueOtherClr();
                window.close();
            } else if (chkStatus == "2") {
                if (confirm('ไม่มีรายการหักอื่นๆ ต้องการปิดหน้าจอ กด ตกลง(OK) ไม่ต้องการปิดกด ยกเลิก(CANCEL)')) {
                    window.opener.GetValueOtherClr();
                    window.close();
                    //                try{
                    //                    parent.GetValueOtherClr();
                    //                }catch(Err){
                    //                    alert("Error Dlg");
                    //                    //window.close();
                    //                    parent.RemoveIFrame();
                    //                }
                } else {
                    Gcoop.GetEl("HfChkStatus").value = "";
                }
            }
        }
        function OnSaveClick() {
            //        if(this.event.keyCode != '13'){
            objdw_otherclr.AcceptText();
            saveWebDialog();
            //        }
        }
        function OnDwOtherClrButtonClicked(sender, rowNumber, buttonName) {
            if (buttonName == "b_delete") {
                objdw_otherclr.DeleteRow(rowNumber);

            }
        }
        function ItemChangeDwOtherClr(sender, rowNumber, columnName, newValue) {
            if (columnName == "clrother_amt") {
                objdw_otherclr.SetItem(rowNumber, "clrother_amt", newValue);
                objdw_otherclr.AcceptText();
                refreshDW();
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table>
        <tr>
            <td>
                <span class="linkSpan" style="cursor: pointer;" onclick="OnInsert('10');">เพิ่มแถว</span>
                <dw:WebDataWindowControl ID="dw_otherclr" runat="server" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    ClientEventButtonClicked="OnDwOtherClrButtonClicked" DataWindowObject="d_sl_loanrequest_otherclr"
                    LibraryList="~/DataWindow/shrlon/sl_loan_requestment.pbl" ClientFormatting="True"
                    ClientEventItemChanged="ItemChangeDwOtherClr">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td>
                <input id="btnSave" type="button" value="ปิด" onclick="OnSaveClick(this)" />
            </td>
        </tr>
    </table>
    <asp:Literal ID="xml_otherclr" runat="server"></asp:Literal>
    <asp:HiddenField ID="HfChkStatus" runat="server" />
    <asp:HiddenField ID="HdXmlOtherClr" runat="server" />
    <asp:HiddenField ID="HdFirst" runat="server" />
    </form>
</body>
</html>
