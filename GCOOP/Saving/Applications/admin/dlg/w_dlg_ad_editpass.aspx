<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_ad_editpass.aspx.cs" 
Inherits="Saving.Applications.admin.dlg.w_dlg_ad_editpass" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
     <%=jsChangePass%>
    <%=jsSearch%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        function UserCheck(s, r, c, v) {
            if (c == "user_name") {
                s.SetItem(1, "user_name", v);
                s.AcceptText();
                jsSearch();
            }
        }
        function ButtonClicked(s, r, c, v) {
            switch (c) {
                case "b_1":
                    jsChangePass();
                    break;
                case "b_2":
                    parent.RemoveIFrame();
                    break;
            }
        }
         
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <br />
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" ClientValidation="False"
            DataWindowObject="d_um_editpass" LibraryList="~/DataWindow/admin/ad_user.pbl" ClientEventItemChanged="UserCheck" 
            ClientEventButtonClicked="ButtonClicked">
        </dw:WebDataWindowControl>
        <br />
<%--        <asp:HiddenField ID="HdRow" runat="server" Value="" />
        <asp:HiddenField ID="HdValue" runat="server" Value="" />
        <asp:HiddenField ID="HdStatus" runat="server" Value="" />--%>
    </div>
    </form>
</body>
</html>

