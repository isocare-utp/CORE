<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_cfingtable_detail.aspx.cs"
    Inherits="Saving.Applications.keeping.dlg.w_dlg_cfingtable_detail" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CF Int Table Page</title>
    <%=postAddData%>
    <%=postSaveData%>
    <%=postDeleteData%>

    <script type="text/jscript">
    function OnAddRow(){
        postAddData();
    }
    
    function OnButtonCloseClick(bs){
        parent.IntRateType();
    }
    
    function OnButtonSaveClick(bs){
        var isConfirm = confirm("ยืนยันการบันทึกข้อมูล");
        if(isConfirm){
            bs.disabled = true;
            postSaveData();
        }
    }
    
    function OnDwDetailButtonClicked(s, r, c){
        if(c == "b_delete" && r > 0){
            if(Gcoop.GetEl("HdCommand").value == "insert"){
                alert("--- ไม่สามารถทำการลบแถว ---");
            }else{
                Gcoop.GetEl("HdDeleteRow").value = r + "";
                postDeleteData();
            }
        }
        return 0;
    }
    
    function OnDwDetailItemChanged(s, r, c, v){
        if(r > 0){
            var entryId = Gcoop.GetEl("HdEntryId").value;
            var entryDate = Gcoop.GetEl("HdEntryDate").value;
            
            s.SetItem(r, "entry_id", entryId);
            s.SetItem(r, "entry_date", entryDate);
        }
        return 0;
    }
    
    function DialogLoadComplete(){
        Gcoop.GetEl("buttonSaveData").value = Gcoop.GetEl("HdCommand").value == "insert" ? "บันทึกข้อมูล" : "บันทึกข้อมูล";
        Gcoop.GetEl("buttonSaveData").disabled = Gcoop.GetEl("HdIsSaved").value == "true";
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <div>
        <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_sl_cfloan_inttable_mast_dlg"
            LibraryList="~/DataWindow/keeping/sl_cfinttable.pbl" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
            ClientScriptable="True" TabIndex="1">
        </dw:WebDataWindowControl>
        <br />
        <dw:WebDataWindowControl ID="DwDetail" runat="server" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
            ClientScriptable="True" DataWindowObject="d_sl_cfloan_inttable_detail_dlg" LibraryList="~/DataWindow/keeping/sl_cfinttable.pbl"
            ClientEventButtonClicked="OnDwDetailButtonClicked" ClientEventItemChanged="OnDwDetailItemChanged"
            TabIndex="25">
        </dw:WebDataWindowControl>
        <br />
        <div align="center">
            <input id="buttonSaveData" style="width: 80px;" type="button" value="บันทึก" onclick="OnButtonSaveClick(this)" />
            &nbsp;
            <input id="buttonCloseIFrame" type="button" value="ปิดหน้าจอ" onclick="OnButtonCloseClick(this)"
                style="width: 80px;" />
        </div>
    </div>
    <asp:HiddenField ID="HdIsSaved" runat="server" />
    <asp:HiddenField ID="HdDeleteRow" runat="server" />
    <asp:HiddenField ID="HdEntryId" runat="server" />
    <asp:HiddenField ID="HdEntryDate" runat="server" />
    <asp:HiddenField ID="HdCommand" runat="server" />
    </form>
</body>
</html>
