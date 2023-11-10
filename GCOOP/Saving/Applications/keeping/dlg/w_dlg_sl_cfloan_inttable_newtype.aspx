<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_cfloan_inttable_newtype.aspx.cs"
    Inherits="Saving.Applications.keeping.dlg.w_dlg_sl_cfloan_inttable_newtype" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>อัตราดอกเบี้ยประเภทใหม่</title>
   <%=saveData%>
    <script type="text/javascript">
    
     function OnDwMainButtonClick(sender, rowNumber, buttonName)
        {
            if(buttonName == "b_ok"){
                var loanintrate_code = "";
                var loanintrate_desc = "";
                             loanintrate_code = objdw_data.GetItem(1, "loanintrate_code");
                             loanintrate_desc = objdw_data.GetItem(1, "loanintrate_desc");
            
                if(confirm("ยืนยันการบันทึกข้อมูล?")){
                    try{
                        saveData();
                        setTimeout("alert('Finish')", 3500);
                    }catch(ex){
                        alert("can't update " + ex);
                    }
                    window.opener.NewRateCode(loanintrate_code,loanintrate_desc);
                    window.close();
                }
            }
            else if (buttonName == "b_cancel"){
                window.close();
            }
        }
    
    
//        function OnDwMainButtonClick(sender, rowNumber, buttonName)
//        {
//            if(buttonName == "b_ok"){
//                if(confirm("ยืนยันการบันทึกข้อมูล?")){
//                    var loanintrate_code = "";
//                    var loanintrate_desc = "";
//                    try{
//                         loanintrate_code = objDwMain.GetItem(1, "loanintrate_code");
//                         loanintrate_desc = objDwMain.GetItem(1, "loanintrate_desc");
//                    }catch(ex){
//                        alert("can't get");
//                        loanintrate_code = "";
//                        loanintrate_desc = "";
//                    }
//                    try{
//                        saveData();
//                        setTimeout("alert('Finish')", 3500);
//                    }catch(ex){
//                        alert("can't update " + ex);
//               // objDwMain.Update();
//               // alert("สร้างอัตราดอกเบี้ยใหม่ " + loanintrate_code + " : " + loanintrate_desc  + " แล้ว");
//            }
//                //objDwMain.Update();
//                window.opener.NewRateCode(loanintrate_code);
//                window.close();
//            }
//            else if (buttonName == "b_cancel"){
//                window.close();
//            }
//        }
               
        function OnDwMainItemChange(sender, rowNumber, columnName, newValue){
            if (columnName == "loanintrate_code")
            {    
               objDwMain.SetItem(rowNumber,columnName,newValue);
               var rateCode = objDwMain.GetItem(rowNumber, "loanintrate_code") + "";
               
               rateCode = rateCode.toUpperCase();
                             
               setTimeout("objDwMain.SetItem("+rowNumber+", '"+columnName+"', '"+rateCode+"')", 1000);
               objDwMain.AcceptText();   
            }
        }
        
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_sl_cfloan_inttable_newtype"
        LibraryList="~/DataWindow/shrlon/sl_cfinttable.pbl" ClientEventButtonClicked="OnDwMainButtonClick"
        ClientEventItemChanged="OnDwMainItemChange" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" >
    </dw:WebDataWindowControl>
    <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="บันทึก" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" onclick="OnClose" Text="ยกเลิก" />
        <br />
    </form>
</body>
</html>
