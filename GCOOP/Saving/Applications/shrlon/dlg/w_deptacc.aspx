<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_deptacc.aspx.cs" Inherits="Saving.Applications.shrlon.dlg.w_deptacc" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>เลขที่บัญชีสหกรณ์</title>
     <%=setDocNo %>
    <%=refresh  %>
    <script type="text/javascript">

        function selectRow(sender, rowNumber, objectName) {
            var dept_no = objdw_detail.GetItem(rowNumber, "deptaccount_no");
            var deptaccount_name = objdw_detail.GetItem(rowNumber, "deptaccount_name");
            //  var prncbal = Gcoop.ParseFloat(objdw_detail.GetItem(rowNumber, "prncbal"));
            //  alert(prncbal);
            //  window.opener.GetValueAccID(dept_no, deptaccount_name, prncbal);
            window.close();

        }
        function DialogLoadComplete() {

            //         var dept_no = objdw_detail.GetItem(rowNumber, "deptaccount_no");
            //         var deptaccount_name = objdw_detail.GetItem(rowNumber, "deptaccount_name");
            //         var prncbal = Gcoop.ParseFloat(objdw_detail.GetItem(rowNumber, "prncbal"));
            //         window.opener.GetValueAccID(dept_no, deptaccount_name, prncbal);
            //         window.close();
            //         var accID = Gcoop.GetEl("HfAccID").value;
            //         if ((accID != null ) && (accID != "" ))
            //         {
            //            window.opener.GetValueAccID(accID);
            //            window.close();
            //         }
        }
    </script>
</head>
<body>
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <form id="form1" runat="server">
    <table >
        <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_detail" runat="server" DataWindowObject="d_dp_account_dept"
                LibraryList="~/DataWindow/Shrlon/sl_loan_requestment_cen.pbl" Width="600px" Style="top: 0px;
                left: 0px"  
                ClientEventClicked="selectRow" ClientScriptable="True" AutoRestoreContext="False"
                AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True">
              
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
     <asp:HiddenField ID="HfAccID" runat="server" />
    <asp:HiddenField ID="HfRow" runat="server" />
    </form>
</body>
</html>
