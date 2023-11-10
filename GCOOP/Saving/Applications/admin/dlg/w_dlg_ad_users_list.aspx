<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_ad_users_list.aspx.cs" 
Inherits="Saving.Applications.admin.dlg.w_dlg_ad_users_list" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
 <script type="text/javascript">
     function OnDwMainClick(s, r, c) {
         user_name = objDwMain.GetItem(r, "user_name");
         parent.ReceiveUserName(user_name);
         parent.RemoveIFrame();


             }

    
    </script>
</head>
<body >
    <form id="form1" runat="server">
      <center>รายละเอียด</center> 
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    <div>
        <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_ad_users_list"
            LibraryList="~/DataWindow/admin/ad_user.pbl" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
            ClientFormatting="True" RowsPerPage="10" ClientEventClicked="OnDwMainClick" Position="Center" >
            <PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="Numeric">
                <BarStyle HorizontalAlign="Center" />
                <NumericNavigator FirstLastVisible="True" />
            </PageNavigationBarSettings>
        </dw:WebDataWindowControl> 
       </div>
     </form>
</body>
</html>
