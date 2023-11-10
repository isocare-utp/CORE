<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_dp_interest_prewithdraw.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.dlg.w_dlg_dp_interest_prewithdraw" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>อัตราดอกเบี้ยถอนก่อนกำหนด/ระบุเอง</title>
    <%=postTDwMain%>
    <%=postSave%>
    <%=InsertRow%>

    <script type="text/javascript">
    
    function DialogLoadComplete()
    {
        var check = Gcoop.GetEl("HdCloseDlg").value;
        if(check == "true"){
            window.close();
        }
    }
    function DwMainItemChanged(s, r, c, v){
        if(c == "effective_tdate"){
            objDwMain.SetItem(r,c,v);
            objDwMain.AcceptText();
        }
    }
    
    function DwMainButtonClick(sender, row, buttonName){
        if(buttonName == "cb_insert"){
            InsertRow();
        }
        else if(buttonName == "cb_ok"){
            if(confirm("ต้องการบันทึกข้อมูลใช่ หรือไม่?")){
                postSave();
            }
            else{
                window.close();
            }
        }
        else if(buttonName == "cb_cancel"){
            window.close();
        }
        
        else if(buttonName == "cb_delete"){
            objDwMain.DeleteRow(row);
        }
    }
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:Panel ID="Panel1" runat="server" ScrollBars="Horizontal" Height="480px">
        <div>
            <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_dp_depttype_previous_int"
                LibraryList="~/DataWindow/ap_deposit/dpdepttypecond.pbl" ClientEventItemChanged="DwMainItemChanged"
                Height="480px" ClientEventButtonClicked="DwMainButtonClick" ClientFormatting="True">
            </dw:WebDataWindowControl>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="HdCloseDlg" runat="server" />
    </form>
</body>
</html>
