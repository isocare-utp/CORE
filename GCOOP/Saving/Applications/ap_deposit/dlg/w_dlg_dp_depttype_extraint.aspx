<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_dp_depttype_extraint.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.dlg.w_dlg_dp_depttype_extraint" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>อัตราดอกเบี้ยพิเศษ</title>
    <%=postUpdate%>
    <script type="text/javascript">
        function OnDwMainButtonClick(sender, rowNumber, buttonName){
            if (buttonName == "cb_insert")
            {
                objDwMain.InsertRow(objDwMain.RowCount() + 1); 
            }
            else if (buttonName == "cb_delete")
            {
                objDwMain.DeleteRow(rowNumber);
            }
            else if (buttonName == "cb_ok")
            {  
                var deptType = Gcoop.GetEl("HdDeptType").value ;
                
                for(var i = 1; i<=objDwMain.RowCount(); i++)
                {
                    objDwMain.SetItem(i,"depttype_code",deptType);
                }
                var con = confirm("ท่านต้องการบันทึกแก้ไข หรือไม่?");
                if (con == true){
                    postUpdate();
                }
                else{
                    window.close();
                }
            }
            else if (buttonName == "cb_cancel")
            {
                window.close();
            }
        }
        
        function OnDwMainItemChanged(sender, rowNumber, columnName, newValue){
            if(columnName == "dept_smonth")
            {
                objDwMain.SetItem(rowNumber, columnName, newValue);
                objDwMain.AcceptText();
            }
            else if(columnName == "dept_emonth")
            {
                objDwMain.SetItem(rowNumber, columnName, newValue);
                objDwMain.AcceptText();
                var beginAmt = parseInt(objDwMain.GetItem(rowNumber,"dept_smonth"),10);
                var endAmt = parseInt(objDwMain.GetItem(rowNumber,"dept_emonth"),10);
                if(beginAmt >= endAmt){
                    beginAmt = beginAmt + "";
                    alert("ค่าสิ้นสุดควรมีค่ามากกว่า : " + beginAmt);
                }   
            }
        }
        
        function DialogLoadComplete()
        {
            var check = Gcoop.GetEl("HdCloseDlg").value;
            if(check == "true"){
                window.close();
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_dp_depttype_extraint"
            LibraryList="~/DataWindow/ap_deposit/dpdepttypecond.pbl" ClientEventButtonClicked="OnDwMainButtonClick"
            ClientEventItemChanged="OnDwMainItemChanged" ClientFormatting="True">
        </dw:WebDataWindowControl>
    </div>
    <asp:HiddenField ID="HdDeptType" runat="server" />
    <asp:HiddenField ID="HdCloseDlg" runat="server" />
    </form>
</body>
</html>
