<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_ad_groupname.aspx.cs" Inherits="Saving.Applications.admin.w_sheet_ad_groupname" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsinitSearch %>
    <%=jsinitPageSearch %>
    <%=jsMemberClick %>
    <style type="text/css">
        .tb_user_name
        {
            border: 1;
        }
        .tb_user_name td
        {
            border: solid 1px black;
        }
    </style>
    <script type="text/javascript">
        function selectRow(sender, rowNumber, objectName) {
            Gcoop.GetEl("GroupID").value = objDwUserName.GetItem(rowNumber, "user_name");
            jsinitSearch();
        }

        function selectAppRow(sender, rowNumber, objectName) {
            Gcoop.GetEl("AppID").value = objDwApplication.GetItem(rowNumber, "amsecuseapps_application");
            jsinitPageSearch();
        }
        function OnDwCashClicked(s, r, c) {
            jsMemberClick();
        }
        function OnDwButtonClicked(s, r, c, v) {
            switch (c) {
                case "b_1":
                    group_name = objDwDetail.GetItem(1, "user_name");
                    Gcoop.OpenIFrame("560", "250", "w_dlg_ad_groupusers.aspx", "?group_name=" + group_name);
                    break;
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table class="tb_user_name" cellpadding="0" cellspacing="0">
        <tr>
            <td rowspan="2">
                <asp:Panel ID="Panel1" runat="server" Height="400px" ScrollBars="Auto" Direction="LeftToRight"
                    HorizontalAlign="Center">
                    <dw:WebDataWindowControl ID="DwUserName" runat="server" DataWindowObject="d_um_groups"
                        LibraryList="~/DataWindow/admin/ad_group.pbl;" ClientScriptable="True" ClientEventClicked="selectRow"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True" TabIndex="30" RowsPerPage="15">
                        <PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="QuickGo">
                            <BarStyle HorizontalAlign="Center" />
                            <NumericNavigator FirstLastVisible="True" />
                        </PageNavigationBarSettings>
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
            <td colspan="2">
                <asp:Panel ID="Panel2" runat="server" Height="70px" ScrollBars="Auto">
                    <dw:WebDataWindowControl ID="DwDetail" runat="server" DataWindowObject="d_um_group"
                        LibraryList="~/DataWindow/admin/ad_group.pbl;" ClientScriptable="True" ClientEventButtonClicked="OnDwButtonClicked"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True" TabIndex="1">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:Panel ID="Panel4" runat="server" Height="326px" ScrollBars="Auto" Width="150px">
                    <dw:WebDataWindowControl ID="DwApplication" runat="server" DataWindowObject="d_um_groupapps"
                        LibraryList="~/DataWindow/admin/ad_group.pbl;" ClientScriptable="True" ClientEventClicked="selectAppRow"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True" TabIndex="500">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
            <td valign="top">
                <asp:Panel ID="Panel3" runat="server" Height="326px" ScrollBars="Auto" Width="500px">
                    <dw:WebDataWindowControl ID="DwPages" runat="server" DataWindowObject="d_um_grouppages"
                        LibraryList="~/DataWindow/admin/ad_group.pbl;" ClientScriptable="True" ClientEventItemChanged="itemChange"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="GroupID" runat="server" />
    <asp:HiddenField ID="AppID" runat="server" />
</asp:Content>
