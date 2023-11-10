<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_sl_searchmembgroup.aspx.cs" Inherits="Saving.Applications.mbshr_const.w_sheet_mb_mbucfmembgroup_ctrl.w_dlg_sl_searchmembgroup_ctrl.w_dlg_sl_searchmembgroup" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsList = new DataSourceTool;
        function Validate() {
        }
        function OnDsMainClicked(s, r, c) {
            if (c == "b_search") {
                PostSearch();
            }
        }
        function OnDsListClicked(s, r, c) {
            if (c == "membgroup_code" || c == "membgroup_desc" || c == "membgroup_control" || c == "membgroup_agentgrg" ) {
                dsList.SetRowFocus(r);
                var membgroup_code = dsList.GetItem(r, "membgroup_code");
                try {
                    window.opener.GetIMembgroup(membgroup_code);
                    window.close();
                } catch (err) {
                    parent.GetIMembgroup(membgroup_code);
                    window.close();
                }
                //PostRetive();
            }
        }
  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <div align="center">
        <uc1:DsMain ID="dsMain" runat="server" />
        <br />
        <uc2:DsList ID="dsList" runat="server" />
    </div>
    <br />
</asp:Content>
