<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_search_generalreg.aspx.cs"
    Inherits="Saving.Applications.lawsys.dlg.w_dlg_search_generalreg" ValidateRequest="false" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ค้นหาเลขทะเบียน</title>
    <%=SearchGenRalReg%>
    <%=GenRegNo%>
    <script type ="text/javascript">
        function OnDwMainButtonClicked(sender, rowNumber, buttonName){
            if(buttonName == "cb_search"){
                SearchGenRalReg();
            }
        }
        
        function OnDwMainItemChanged(sender, rowNumber, columnName, newValue){
            objDwMain.SetItem(rowNumber, columnName, newValue);
            objDwMain.AcceptText();
            if(columnName == "genreg_no"){
                var genReg = objDwMain.GetItem(rowNumber , colunmName);
                if(genReg.length > 2){
                    GenRegNo();
                }
            }
            return 0;
        }
        
        function selectRow(sender, rowNumber, objName){
            try{
                var genRegNo = objDwList.GetItem(rowNumber, "genreg_no");
                window.opener.GetGenRegNoFromDlg(genRegNo);
                window.close(); 
            }
            catch(err){window.close();} 
            return;        
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="เงื่อนไข" Font-Bold="True" Font-Names="Tahoma"
            Font-Size="14px" Font-Underline="True" /><br />
        <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_lw_search_generalreg_main"
            LibraryList="~/DataWindow/lawsys/dlg_lw_search_generalreg.pbl" ClientScriptable="True" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="OnDwMainItemChanged" ClientEventButtonClicked="OnDwMainButtonClicked">
        </dw:WebDataWindowControl>
        <br />
        <asp:Label ID="Label2" runat="server" Text="รายการ" Font-Bold="True" Font-Names="Tahoma"
            Font-Size="14px" Font-Underline="True" />
        <dw:WebDataWindowControl ID="DwList" runat="server" DataWindowObject="d_lw_search_generalreg_list"
            LibraryList="~/DataWindow/lawsys/dlg_lw_search_generalreg.pbl" ClientEventClicked="selectRow"
            ClientScriptable="True" RowsPerPage="15" HorizontalScrollBar="NoneAndClip" VerticalScrollBar="NoneAndClip"
            AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
            ClientFormatting="True">
            <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
            </PageNavigationBarSettings>
        </dw:WebDataWindowControl>
    </div>
    </form>
</body>
</html>
