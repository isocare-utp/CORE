<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_template.aspx.cs"
    Inherits="Saving.Applications.account.dlg.w_dlg_template" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<%=jsPostFilter%>
<script type="text/javascript">
    function OnDwlistClick(sender, row, col) {
        var vcauto_code = objDwlist.GetItem(row, "vcauto_code");
        var vcauto_desc = objDwlist.GetItem(row, "vcauto_desc");
        var vcauto_type = objDwlist.GetItem(row, "voucher_type");
        window.opener.GetTemplate(vcauto_code, vcauto_desc, vcauto_type);
        //window.parent.GetTemplate(vcauto_code);
        window.close();
    }

    function OnFillter(sender, row, col, value) {
        sender.SetItem(row, col, value);
        sender.AcceptText();
        jsPostFilter();
    }
</script>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <dw:WebDataWindowControl ID="Dwmain" runat="server" DataWindowObject="d_dlg_vc_template_fillter"
            LibraryList="~/DataWindow/account/vc_voucher_edit.pbl" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
            ClientScriptable="True" ClientEventItemChanged="OnFillter">
        </dw:WebDataWindowControl>
        <dw:WebDataWindowControl ID="Dwlist" runat="server" DataWindowObject="d_dlg_vc_template"
            LibraryList="~/DataWindow/account/vc_voucher_edit.pbl" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
            ClientScriptable="True" ClientEventClicked="OnDwlistClick">
        </dw:WebDataWindowControl>
    </div>
    </form>
</body>
</html>
