<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_search_memgroup.aspx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_search_memgroup" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ค้นหาสังกัด</title>
    <script type="text/javascript">
        function selectRow(sender, rowNumber, objectName) {
         if(objectName!= "datawindow"){
             var membgroup_code = objdw_detail.GetItem(rowNumber, "membgroup_code");
                try{
                    window.opener.GetMemGroupFromDlg(membgroup_code);
                    window.close();
                }catch(err){
                parent.GetMemGroupFromDlg(membgroup_code);
                    parent.RemoveIFrame();
                }
          }
        }
        function IFrameClose(){
            parent.RemoveIFrame();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    รายการค้นหา<span style="float:right; cursor:pointer;color:Red;" onclick="IFrameClose();">[X]</span>
    <table style="width: 530px;">
        <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_data" runat="server" ClientScriptable="True" DataWindowObject="d_sl_memgroup_search_criteria"
                    LibraryList="~/DataWindow/keeping/sl_member_new.pbl">
                </dw:WebDataWindowControl>
            </td>
            <td>
                <asp:Button ID="cb_find" runat="server" Text="ค้นหา" Height="80px" Width="55px" OnClick="cb_find_Click"  style="width:55px; height:80px;" />
            </td>
        </tr>
    </table>
    รายละเอียด
    <asp:HiddenField ID="hidden_search" runat="server" />
    <dw:WebDataWindowControl ID="dw_detail" runat="server" ClientEventClicked="selectRow"
        DataWindowObject="d_sl_memgroup" LibraryList="~/DataWindow/keeping/sl_member_new.pbl"
        RowsPerPage="17" ClientScriptable="True">
        <PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="Numeric">
            <BarStyle HorizontalAlign="Center" />
            <NumericNavigator FirstLastVisible="True" />
        </PageNavigationBarSettings>
    </dw:WebDataWindowControl>
    </form>
</body>
</html>
