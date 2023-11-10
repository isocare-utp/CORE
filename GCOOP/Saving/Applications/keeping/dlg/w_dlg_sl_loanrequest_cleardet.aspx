<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_loanrequest_cleardet.aspx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_loanrequest_cleardet" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>รายละเอียดหักกลบ</title>
    <%=closeWebDialog  %>
    <%=refresh %>
    <script type="text/javascript">
    function DialogLoadComplete(){
         var chkStatus = Gcoop.GetEl("HfChkStatus").value;
         if (chkStatus == "1" ) 
         {
//            try{
//                parent.GetValueResumloanClear();
//            }catch(Err){
//                alert("Error Dlg");
//                //window.close();
//                parent.RemoveIFrame();
//            }
            window.opener.GetValueResumloanClear();
            window.close();
         }
     }
     function OnCloseClick(){
            closeWebDialog();
     }
     function ItemChangeDwClearDet(sender, rowNumber, columnName, newValue)
     {
        if (columnName != "DataWindow")
        {
            objdw_cleardet.SetItem( 1, columnName, newValue);
            refresh();
        }
     }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table >
        <tr>
            <td>
            <dw:WebDataWindowControl ID="dw_cleardet" runat="server" 
                    DataWindowObject="d_sl_loanrequest_cleardet" 
                    LibraryList="~/DataWindow/shrlon/sl_loan_requestment.pbl" 
                    AutoRestoreContext="False" AutoRestoreDataCache="True" 
                    AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" ClientEventItemChanged="ItemChangeDwClearDet">
            </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr >
            <td>
             <asp:HiddenField ID="HfChkStatus" runat="server" />
             <input id="btnClose" type="button" value="ปิด" onclick="OnCloseClick(this)" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
