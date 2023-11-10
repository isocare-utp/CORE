<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_loanrequest_monthpay.aspx.cs"
    Inherits="Saving.Applications.keeping.dlg.w_dlg_sl_loanrequest_monthpay" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>เงินเดือนคงเหลือ</title>
    <%=closeWebDialog%>

    <script type="text/javascript">
    function DialogLoadComplete(){
         var chkStatus = Gcoop.GetEl("HfChkStatus").value;
         if (chkStatus == "1" ) 
         {
            try{
            Gcoop.GetEl("HfChkStatus").value = "";
             if(confirm ("ต้องการปิดหน้าจอนี้ " )){
                    parent.CloseDLG();
                }
                
            }catch(Err){
                alert("Error Dlg");
                //window.close();
                parent.RemoveIFrame();
            }
//            window.opener.CloseDLG();
//            window.close();
         }
     }
     function OnCloseClick(){
//        parent.removeIFrame();
//        alert(".........");
        //closeWebDialog();
        parent.RemoveIFrame();
     }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table>
        <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_head" runat="server" DataWindowObject="d_sl_loanrequest_monthpay"
                    LibraryList="~/DataWindow/shrlon/sl_loan_requestment.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    Style="top: 0px; left: 0px">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_detail" runat="server" DataWindowObject="d_sl_loanrequest_monthpaydet"
                    LibraryList="~/DataWindow/shrlon/sl_loan_requestment.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    Style="top: 0px; left: 0px">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="HfChkStatus" runat="server" />
                <input id="btnClose" type="button" value="ปิด" onclick="OnCloseClick()" />
                <%--<asp:Button ID="btnClose" runat="server" Text="ปิด" onclick="btnClose_Click" />--%>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
