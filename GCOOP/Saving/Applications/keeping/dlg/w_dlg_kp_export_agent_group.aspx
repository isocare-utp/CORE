<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_kp_export_agent_group.aspx.cs" Inherits="Saving.Applications.keeping.dlg.w_dlg_kp_export_agent_group" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ส่งออกข้อมูลสังกัดลูกหนี้ตัวแทน</title>
    <%= exportTxt  %>
    <script type="text/javascript">
         function CloseClick()
        {
            window.close();
        }
        function ExportClick()
        {
            exportTxt();
        }
        function DialogLoadComplete()
       {
         
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table>
            <tr>
                <td>
                
                <dw:WebDataWindowControl ID="dw_main" runat="server" AutoRestoreContext="False" 
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                    ClientScriptable="True" 
                    DataWindowObject ="d_kp_agent_group_list" 
                    LibraryList ="~/DataWindow/keeping/kp_agent_group.pbl" 
                    ClientFormatting="True" 
                    RowsPerPage="15" 
                    style="top: 0px; left: 0px" >
                    <PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="Numeric">
                    <BarStyle HorizontalAlign="Center" />
                    <NumericNavigator FirstLastVisible="True" />
                    </PageNavigationBarSettings>
                    </dw:WebDataWindowControl>
                    <input id="btnExport" type="button" value="ส่งออกข้อมูล" onclick="ExportClick()" />
                    <input id="btnClose" type="button" value="ปิด" onclick="CloseClick()"  />
                    </td>
            </tr>
        </table>
    </form>
</body>
</html>
