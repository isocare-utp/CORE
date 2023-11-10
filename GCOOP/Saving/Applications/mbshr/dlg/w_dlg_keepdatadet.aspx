<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_keepdatadet.aspx.cs" Inherits="Saving.Applications.mbshr.dlg.w_dlg_keepdatadet" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>รายละเอียดรายหักประจำเดือน</title>
    <script type="text/javascript">
       
        function IFrameClose() {
            parent.RemoveIFrame();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    
                รายละเอียด<span style="float:right; cursor:pointer;color:Red;" onclick="IFrameClose();">[X]</span>
                <dw:WebDataWindowControl ID="dw_data" runat="server" ClientScriptable="True" DataWindowObject="d_mb_detail_keepdatadet"
                    LibraryList="~/DataWindow/mbshr/sl_member_detail.pbl">
                </dw:WebDataWindowControl>
            
       
    </form>
</body>
</html>
