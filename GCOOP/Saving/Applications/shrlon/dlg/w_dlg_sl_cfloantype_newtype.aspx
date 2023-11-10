<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_cfloantype_newtype.aspx.cs"
    Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_cfloantype_newtype" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>เงินกู้ประเภทใหม่</title>
    <script type="text/javascript">
        function OnButtonClick(sender, rowNumber, buttonName)
        {
            if(buttonName == "b_ok"){
                try{
                objdw_data.Update();
                var id = objdw_data.GetItem(1, "loantype_code");
                var desc = objdw_data.GetItem(1, "loantype_desc");
                alert("เพิ่มประเภทเงินกู้ : " + id + " " + desc + " แล้ว");
                window.opener.NewConfigCode(id);
                window.close();
                }catch(ex){
                    alert("ไม่สามารถเพิ่มข้อมูลได้");
                }
            }
            else if (buttonName == "b_cancel"){
                window.close();
            }
        }
        function CheckValue(s, r, c, v){
            if(c=="loantype_code" || c=="prefix"){
                if(v.length > 2){
                    alert("ป้อนรหัสได้ไม่เกิน สอง ตัวอักษร"); 
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dw:WebDataWindowControl ID="dw_data" runat="server" ClientScriptable="True" DataWindowObject="d_sl_cflntype_newtype"
            LibraryList="~/DataWindow/shrlon/sl_cfloantype.pbl" 
            AutoRestoreContext="False" AutoRestoreDataCache="True" 
            AutoSaveDataCacheAfterRetrieve="True"
            ClientEventButtonClicking="OnButtonClick" ClientEventItemChanged="CheckValue">
        </dw:WebDataWindowControl>
    </div>
</form>
</body>
</html>
