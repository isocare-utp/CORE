<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_chqformat_add.aspx.cs"
    Inherits="Saving.Applications.app_finance.dlg.w_dlg_chqformat_add" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%=postAddFormat %>

    <script type="text/javascript">

        function DwMainButtonClick(sender, rowNumber, buttonName) {
            if (buttonName == "b_add") {
                objDwMain.AcceptText();
                if (confirm("บันทึกการแก้ไขข้อมูล?"))
                { objDwMain.Update(); }
                //postAddFormat();
            }
            else if (buttonName == "b_cancel") {
                window.close();
            }
        }
        
    </script>

    <title></title>
</head>
<body>
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <form id="form1" runat="server">
    <div>
        <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_chequeformat_add"
            LibraryList="~/DataWindow/app_finance/setcheque_format.pbl" AutoRestoreContext="False"
            AutoRestoreDataCache="True" ClientFormatting="False" ClientScriptable="True"
            AutoSaveDataCacheAfterRetrieve="True" ClientEventButtonClicked="DwMainButtonClick"
            OnBeginUpdate="dw_main_BeginUpdate" OnEndUpdate="dw_main_EndUpdate">
        </dw:WebDataWindowControl>
    </div>
    </form>
</body>
</html>
