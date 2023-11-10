<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_mg_export_mrtgmast.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_mg_export_mrtgmast_ctrl.ws_sl_mg_export_mrtgmast" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#ctl00_ContentPlace_dsList_cb_all").click(function () {
                var chk = $('#ctl00_ContentPlace_dsList_cb_all').is(':checked');

                if (chk) {
                    for (var i = 0; i < dsList.GetRowCount(); i++) {
                        dsList.SetItem(i, "operate_flag", 1);
                    }
                } else {
                    for (var i = 0; i < dsList.GetRowCount(); i++) {
                        dsList.SetItem(i, "operate_flag", 0);
                    }
                }
            });
        });

        function OnDsMainClicked(s, r, c, v) {
            if (c == "b_retrieve") {
                PostRetrieve();
            }
        }

        function OnDsMainItemChanged(s, r, c, v) {

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:Button ID="Button1" runat="server" Text="Export Excel" OnClick="PostExportExcel"
        Style="margin-left: 630px; width: 100px" />
    <uc1:DsMain ID="dsMain" runat="server" />
    <uc2:DsList ID="dsList" runat="server" />
</asp:Content>
