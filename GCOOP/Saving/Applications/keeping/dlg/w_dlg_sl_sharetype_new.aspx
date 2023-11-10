<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_sharetype_new.aspx.cs"
    Inherits="Saving.Applications.keeping.dlg.w_dlg_sl_sharetype_new" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>สร้างรายการเงินกู้ใหม่</title>
    <%=saveData %>

    <script type="text/javascript">
        function OnClose(){
            var sharetype_code = "";
            var sharetype_desc = "";
            try{
                sharetype_code = objdw_data.GetItem(1, "sharetype_code");
                sharetype_desc = objdw_data.GetItem(1, "sharetype_desc");
            }catch(ex){
               sharetype_code = "";
               sharetype_desc = ""; 
            }
            window.opener.NewShareType(sharetype_code,sharetype_desc);
            window.close();
        }
        function OnDwMainButtonClick(sender, rowNumber, buttonName)
        {
            if(buttonName == "b_ok"){
                var sharetype_code = "";
                var sharetype_desc = "";
                             sharetype_code = objdw_data.GetItem(1, "sharetype_code");
                             sharetype_desc = objdw_data.GetItem(1, "sharetype_desc");
            
                if(confirm("ยืนยันการบันทึกข้อมูล?")){
                    try{
                        saveData();
                        setTimeout("alert('Finish')", 3500);
                    }catch(ex){
                        alert("can't update " + ex);
                    }
                    window.opener.NewShareType(sharetype_code,sharetype_desc);
                    window.close();
                }
            }
            else if (buttonName == "b_cancel"){
                window.close();
            }
        }
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    
    <div>
        <dw:WebDataWindowControl ID="dw_data" runat="server" ClientScriptable="True" DataWindowObject="d_sl_sharetype_new"
            LibraryList="~/DataWindow/shrlon/sl_sharetype_detail.pbl" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
            ClientEventButtonClicked="OnDwMainButtonClick" 
            style="top: 0px; left: 0px; height: 56px; width: 324px">
        </dw:WebDataWindowControl>
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="บันทึก" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" onclick="OnClose" Text="ยกเลิก" />
        <br />
    </div>
    </form>
    </body>
</html>
