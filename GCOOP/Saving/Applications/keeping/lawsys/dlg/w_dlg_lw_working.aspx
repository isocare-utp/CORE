<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_lw_working.aspx.cs"
    Inherits="Saving.Applications.lawsys.dlg.w_dlg_lw_working" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <%=postSave%>

    <script type="text/javascript">
    
    function ClickSave(){
        if(confirm("ยืนยันการบันทึกข้อมูล")){
            postSave();
        }
    }
    
    function DialogLoadComplete(){
        if(Gcoop.GetEl("HdIsFinish").value == "true"){
            parent.postWorkingEdit();
        }
    }
    
    function OnDwWorkingItemChanged(s, r, c, v){
        return 0;
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        &nbsp;
        <asp:Label ID="LbSaveCommand" runat="server" Text="เพิ่มแถว" Font-Bold="True" Font-Names="Tahoma"
            Font-Size="14px" Font-Underline="True" ForeColor="#0099CC"></asp:Label>
        <br />
        <dw:WebDataWindowControl ID="DwWorking" runat="server" DataWindowObject="d_lw_working_save"
            LibraryList="~/DataWindow/lawsys/lw_lawmaster.pbl" ClientScriptable="True" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="OnDwWorkingItemChanged"
            ClientFormatting="True">
        </dw:WebDataWindowControl>
        <div align="center">
            <input id="Button1" type="button" value="บันทึก" onclick="ClickSave()" />
            &nbsp;
            <input id="Button2" type="button" value="ปิด" onclick="parent.RemoveIFrame()" />
        </div>
        <asp:HiddenField ID="HdIsFinish" runat="server" Value="false" />
        <asp:HiddenField ID="HdSeqNo" runat="server" Value="0" />
    </div>
    </form>
</body>
</html>
