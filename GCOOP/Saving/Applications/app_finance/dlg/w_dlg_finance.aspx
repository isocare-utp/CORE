<%@ Page Title="" Language="C#" AutoEventWireup="true" 
CodeBehind="w_dlg_finance.aspx.cs" Inherits="Saving.Applications.app_finance.dlg.w_dlg_finance" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>รายละเอียด</title>

<asp:Panel ID="Panel1" ContentPlaceHolderID="head" runat="server">
   <%=postSumMoney%>
    <script type="text/javascript">
        function dw_mainItemChanged(sender, rowNumber, columnName, newValue) {
            objdw_main.SetItem(rowNumber, columnName, newValue);
            Gcoop.GetEl("HColumName").value = columnName;
            objdw_main.AcceptText();
            postSumMoney();
        }
        function dw_mainCancle(sender, rowNumber, objectName) {            
            parent.RemoveIFrame();
            return;
        }
        function dw_mainSummitClick(sender, rowNumber, objectName) {
            var amount = objdw_main.GetItem(1, "amount");

                var b1000 = objdw_main.GetItem(1, "b1000");
                var b500 = objdw_main.GetItem(1, "b500");
                var b100 = objdw_main.GetItem(1, "b100");
                var b50 = objdw_main.GetItem(1, "b50");
                var b20 = objdw_main.GetItem(1, "b20");
                var c10 = objdw_main.GetItem(1, "c10");
                var c5 = objdw_main.GetItem(1, "c5");
                var c2 = objdw_main.GetItem(1, "c2");
                var c1 = objdw_main.GetItem(1, "c1");
                var c50 = objdw_main.GetItem(1, "c50");
                var c25 = objdw_main.GetItem(1, "c25");
                parent.SelectCash(amount, b1000, b500, b100, b50, b20, c10, c5, c2, c1, c50, c25);
                parent.RemoveIFrame();
                return;
        }
           
    </script>
    </asp:Panel>
</head>
<body>
    <form id="form1" runat="server">
    <div >
        <asp:Panel ID="Panel2" runat="server" width="100%" Height="100%" ScrollBars="Auto">
            <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="dlg_cashdetail"
                LibraryList="~/DataWindow/app_finance/finance.pbl" AutoRestoreContext="False"
                AutoRestoreDataCache="True" ClientFormatting="False" ClientEventItemChanged="dw_mainItemChanged"
                ClientScriptable="True" AutoSaveDataCacheAfterRetrieve="True" >
            </dw:WebDataWindowControl>
        <asp:HiddenField ID="HColumName" runat="server" />
        </asp:Panel>
    <table width="100%" border="0">
        <tr>
          <td align="center">
                <input id="Button1" type="button" value="ตกลง" onclick="return dw_mainSummitClick()"/>
                <input id="Button2" type="button" value="ยกเลิก" onclick="return dw_mainCancle()" />
                <%--<asp:Button ID="Button1" runat="server" Text="ตกลง" 
                    OnClientClick="dw_mainSummitClick()" onclick="Button1_Click"  />--%>
                    
                <%--<asp:Button ID="Button2" runat="server" Text="ยกเลิก" OnClientClick="return dw_mainCancle()" />--%>                
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>

