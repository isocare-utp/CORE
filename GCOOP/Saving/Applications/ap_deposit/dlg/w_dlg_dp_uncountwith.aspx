<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_dp_uncountwith.aspx.cs" Inherits="Saving.Applications.ap_deposit.dlg.w_dlg_dp_uncountwith" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ระบบงานเงินฝาก</title>
    <%=postUpdate%>
    <script type ="text/javascript">
        
        function DialogLoadComplete()
        {
            var check = Gcoop.GetEl("HdCloseDlg").value;
            if(check == "true"){
                window.close();
            }
        }
        
        function OnDwMainButtonClick(sender, rowNumber, buttonName){
            if (buttonName == "cb_insert")
            {
                objDwMain.InsertRow(objDwMain.RowCount() + 1);      
            }
            else if (buttonName == "cb_delete")
            {
                var detail = "รหัส " + objDwMain.GetItem(rowNumber, "deptitem_code");
                
                if(confirm("คุณต้องการลบรายการ "+ detail +" ใช่หรือไม่?")){
                    objDwMain.DeleteRow(rowNumber);
                }
            }
            else if (buttonName == "cb_ok")
            {
                var arrDepCode = new Array(); 
                var checkDep = 1;
                var deptType = Gcoop.GetEl("HdDeptType").value ;
                 
                for(var i = 1; i<=objDwMain.RowCount(); i++)
                {
                    objDwMain.SetItem(i,"depttype_code",deptType);
                    arrDepCode[i] = objDwMain.GetItem(i,"deptitem_code");
                }

                for(var i = 1; i<objDwMain.RowCount(); i++)
                {
                    for(var j = i+1; j<=objDwMain.RowCount(); j++){
                        if(arrDepCode[i] == arrDepCode[j])
                        {
                            checkDep = 0;
                        }
                    }
                }                
                if(checkDep == 1){
                    postUpdate();
                }
                else{
                    alert("รายการซ้ำ กรุณาเลือกใหม่อีกครั้ง");
                }
            }
            else if (buttonName == "cb_cancel")
            {
                window.close();
            }
        }
        
        function OnDwMainItemChanged(sender, rowNumber, columnName, newValue){
            if(columnName == "deptitem_code")
            {
                objDwMain.SetItem(rowNumber, columnName, newValue);
                objDwMain.AcceptText();
            }
        }
        
    </script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" 
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
            ClientEventButtonClicked="OnDwMainButtonClick" ClientScriptable="True" 
            DataWindowObject="d_dp_depttype_uncountwith" 
            LibraryList="~/DataWindow/ap_deposit/dpdepttypecond.pbl" ClientEventItemChanged="OnDwMainItemChanged">
    </dw:WebDataWindowControl>
    </div>
    <asp:HiddenField ID="HdDeptType" runat="server" />
    <asp:HiddenField ID="HdCloseDlg" runat="server" />
    </form>
</body>
</html>
