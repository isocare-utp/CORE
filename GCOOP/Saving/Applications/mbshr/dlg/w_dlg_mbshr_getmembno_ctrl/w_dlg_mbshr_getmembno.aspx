<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_mbshr_getmembno.aspx.cs" Inherits="Saving.Applications.mbshr.dlg.w_dlg_mbshr_getmembno_ctrl.w_dlg_mbshr_getmembno" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();

        function OnDsMainItemChanged(s, r, c, v) {

        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_save") {
                PostSave();
            } else if (c == "b_search") {
                PostSearch();
            }
        }

    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <uc1:DsMain ID="dsMain" runat="server" />
</asp:Content>
