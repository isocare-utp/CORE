<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_ad_user_name.aspx.cs" Inherits="Saving.Applications.admin.w_sheet_ad_user_name" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsinitSearch %>
    <%=jsinitPageSearch %>
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
        function itemChange(s, r, c, v) {
            s.SetItem(r, c, v);
            s.AcceptText();
            if (c == "user_name") {
                Gcoop.GetEl("userID").value = objDwDetail.GetItem(r, "user_name");
                jsinitSearch();
            }

        }
       
        function MenubarOpen() {
            Gcoop.OpenIFrame(700, 350, "w_dlg_ad_users_list.aspx", '')
        }
        function ReceiveUserName(user_name) {
            Gcoop.GetEl("userID").value = user_name;
            jsinitSearch();
        }
//        function selectRow(sender, rowNumber, objectName) {
//            Gcoop.GetEl("userID").value = objDwUserName.GetItem(rowNumber, "user_name");
//            jsinitSearch();
//        }

        function selectAppRow(sender, rowNumber, objectName) {
            Gcoop.GetEl("AppID").value = objDwApplication.GetItem(rowNumber, "amsecuseapps_application");
            jsinitPageSearch();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table class="tb_user_name" cellpadding="0" cellspacing="0">
        <tr>
<%--            <td rowspan="2" valign="top">
                <asp:Panel ID="Panel1" runat="server" Height="450px" ScrollBars="Auto" Direction="LeftToRight"
                    HorizontalAlign="Center">
                    <dw:WebDataWindowControl ID="DwUserName" runat="server" DataWindowObject="d_um_users"
                        LibraryList="~/DataWindow/admin/ad_user.pbl;" ClientScriptable="True" ClientEventClicked="selectRow"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True" TabIndex="1" RowsPerPage="15">
                        <PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="QuickGo">
                            <BarStyle HorizontalAlign="Center" />
                            <NumericNavigator FirstLastVisible="True" />
                        </PageNavigationBarSettings>
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>--%>
            <td colspan="2">
                <asp:Panel ID="Panel2" runat="server" Height="200px" ScrollBars="Auto">
                    <dw:WebDataWindowControl ID="DwDetail" runat="server" DataWindowObject="d_um_user"
                        LibraryList="~/DataWindow/admin/ad_user.pbl;" ClientScriptable="True" ClientEventItemChanged="itemChange"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True" TabIndex="500">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="Panel4" runat="server" Height="250px" ScrollBars="Auto" Width="150px">
                    <dw:WebDataWindowControl ID="DwApplication" runat="server" DataWindowObject="d_um_userapps"
                        LibraryList="~/DataWindow/admin/ad_user.pbl;" ClientScriptable="True" ClientEventClicked="selectAppRow"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True" TabIndex="650">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
            <td>
                <asp:Panel ID="Panel3" runat="server" Height="250px" ScrollBars="Auto" Width="500px">
                    <dw:WebDataWindowControl ID="DwPages" runat="server" DataWindowObject="d_um_userpages"
                        LibraryList="~/DataWindow/admin/ad_user.pbl;" ClientScriptable="True"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True" TabIndex="900">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="userID" runat="server" />
    <asp:HiddenField ID="AppID" runat="server" />
</asp:Content>
