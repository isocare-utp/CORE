<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_accid.aspx.cs" Inherits="Saving.Applications.app_finance.dlg.w_dlg_accid" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>คู่บัญชี</title>

    <script type="text/javascript">

        function DwMainOnClick(sender, row, collumn) {
            var accid = objDwMain.GetItem(row, "account_id");
            var accname = objDwMain.GetItem(row, "account_desc");
            opener.GetAccid(accid, accname);
            window.close();

        }
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="background-color: #DDDDDD; border: 1px solid #CCCCCC; width: 500px; height: 25px;
            text-align: center; vertical-align: middle;">
            <asp:Label ID="Label1" runat="server" Text="กรุณาเลือกคู่บัญชี"></asp:Label>
        </div>
        <table style="width: 500px; height: 700px;">
            <tr>
                <td valign="top">
                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="600px" Width="500px">
                        <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_fin_tofrom_accid"
                            AutoRestoreContext="false" LibraryList="~/DataWindow/app_finance/finslip_spc.pbl"
                            Width="450px" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" ClientEventClicked="DwMainOnClick">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="HsheetRow" runat="server" />
    </form>
</body>
</html>
