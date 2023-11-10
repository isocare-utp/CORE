<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true" CodeBehind="ws_dlg_sl_shareucftype.aspx.cs" Inherits="Saving.Applications.mbshr.dlg.ws_dlg_sl_shareucftype_ctrl.ws_dlg_sl_shareucftype" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;

        //DsMain
        function OnDsMainClicked(s, r, c) {
            if (c == "b_add") {
                PostSave();
                window.close();
            }
            else if (c == "b_cancel") {
                parent.RemoveIFrame();
            }
        }
        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "checkbox") {
                if (v == "0") {
                    dsMain.GetElement(0, "usepattern_shrcode").disabled = true;
                    dsMain.SetItem(0, "usepattern_shrcode", "");
                } else {
                    dsMain.GetElement(0, "usepattern_shrcode").disabled = false;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <br />
    <center>
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <uc1:DsMain ID="dsMain" runat="server" />
    </center>
</asp:Content>
