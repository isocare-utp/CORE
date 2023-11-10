<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_loanrequest_coll_me.aspx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_loanrequest_coll_me" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>รายละเอียดหลักประกัน</title>
    <%=closeWebDialog %>
    <script type="text/javascript">
        function DialogLoadComplete() {
            var chkStatus = Gcoop.GetEl("HfChkStatus").value;
            if (chkStatus == "1") {

                window.close();
            }
        }
        function OnCloseClick() {

            closeWebDialog();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table>
        <tr>
           
        <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_detail" runat="server" DataWindowObject="d_sl_loanrequest_collitem_me"
                    LibraryList="~/DataWindow/shrlon/sl_loan_requestment_cen.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="HfChkStatus" runat="server" />
                <input id="btnClose" type="button" value="ปิด" onclick="OnCloseClick()" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
